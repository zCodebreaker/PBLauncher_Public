using Ionic.Zip;
using PBLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PBLauncher
{
    public partial class frmPBLauncher : Form
    {
        private Point NewPoint;
        public static WebClient WEB_UPDATE = new WebClient();
        public static WebClient CHECK_UPDATE = new WebClient();
        public string startupPath = Application.StartupPath;
        public int LastVersion = -1;
        int countCheck = 0;
        public int VersionAtual = int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0"));
        List<string> NOT_FOUND_FILE = new List<string>();
        readonly List<string> EXTRACT_FILES = new List<string>();
        public bool LoadingVerif;
        public bool LoadingUpdate;
        public bool LoadingStart;
        public static string FileExtract;
        public int CountNotFound;
        public static string RandomizedProcess;
        readonly string GameConection = $"http://{Infos.PBLAUNCHER_ADRESS}";

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int size, string lpFileName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        public frmPBLauncher()
        {
            InitializeComponent();
            WEB_UPDATE.DownloadFileCompleted += new AsyncCompletedEventHandler(WEB_UPDATE_DownloadCompleted);
            WEB_UPDATE.DownloadProgressChanged += new DownloadProgressChangedEventHandler(WEB_UPDATE_DownloadProgressChanged);
            CHECK_UPDATE.DownloadFileCompleted += new AsyncCompletedEventHandler(CHECK_UPDATE_DownloadCompleted);
            CHECK_UPDATE.DownloadProgressChanged += new DownloadProgressChangedEventHandler(CHECK_UPDATE_DownloadProgressChanged);
        }
        private void FrmPBLauncher_Load(object sender, EventArgs e)
        {
            TopMost = false;
            CheckNewsUpdate();
        }
        private void FrmPBLauncher_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                int x = NewPoint.X;
                Point mousePosition = MousePosition;
                Left = x + mousePosition.X;
                int y = NewPoint.Y;
                mousePosition = MousePosition;
                Top = y + mousePosition.Y;
            }
        }
        private void FrmPBLauncher_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (RandomizedProcess != null)
            {
                if (File.Exists($"{Application.StartupPath}\\{RandomizedProcess}.exe"))
                {
                    File.Delete($"{Application.StartupPath}\\{RandomizedProcess}.exe");
                }
            }
            Logger.Log("PBLauncher Finalized - [" + DateTime.Now.ToString("yyyy/MM/dd - hh:mm:ss") + "]");
            Environment.Exit(1);
            Application.Exit();
        }        
        private void WEB_UPDATE_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SIZE_LABEL.Visible = true;
            SetProgressBar((ulong)e.BytesReceived, (ulong)e.TotalBytesToReceive);
            string size = string.Format("{0} MB de {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            SIZE_LABEL.Text = $"Baixado: {size}";
        }
        private void CHECK_UPDATE_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SIZE_LABEL.Visible = true;
            SetProgressBar((ulong)e.BytesReceived, (ulong)e.TotalBytesToReceive);
            string size = string.Format("{0} MB de {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            SIZE_LABEL.Text = $"Baixado: {size}";
        }
        private void UnzipExtractProgressChanged(object sender, ExtractProgressEventArgs e)
        {
            try
            {
                if (e.TotalBytesToTransfer != 0)
                {
                    SetProgressBar((ulong)e.BytesTransferred, (ulong)e.TotalBytesToTransfer);
                }
                ArchiveBar.Refresh();
                ArchiveBar.Update();
            }
            catch
            {
                Logger.Log("There was an error extracting the update. [0x00000001]");
                MessageBox.Show("There was an error extracting the update. [0x00000001]", "PBLauncher");
            }
        }
        private void CHECK_UPDATE_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            foreach (string item in EXTRACT_FILES)
            {
                Application.DoEvents();
                string NotFoundItems = item;
                string file = NotFoundItems.Substring(NotFoundItems.LastIndexOf("\\") + 1);
                string dirFile = item.Substring(item.LastIndexOf(startupPath) + 2);
                string folders = NotFoundItems.Remove(NotFoundItems.LastIndexOf("\\") + 1);
                string objArray = string.Concat(Application.StartupPath, $"\\_DownloadPatchFiles\\{NotFoundItems}.zip");
                string destDir = string.Concat(Application.StartupPath, $"\\{folders}");
                Unzip(destDir, objArray);
                EXTRACT_FILES.Remove(item);
                NOT_FOUND_FILE.Remove(item);
                break;
            }
            CheckPatchFiles();
            Update();
            Refresh();
        }
        private void WEB_UPDATE_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            Update();
            Refresh();
            int num = int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0"));
            _ = num + 1;
            FILE_LABEL.Text = "File";
            FILE_LABEL.Visible = true;
            DW_LABEL.Text = "Descompactando os arquivos de patch.";
            ArchiveBar.Width = 0;
            string startupPath = Application.StartupPath;
            Unzip(startupPath, string.Concat(FileExtract));
            BT_UPDATE.SendToBack();
            SetButtonsEnable(false, false, false);
            NEWS_UPDATE.Start();
            ArchiveBar.Width = 463;
        }
        public void SetProgressBar(ulong received, ulong maximum)
        {
            if (ArchiveBar.Width <= 463)
            {
                ArchiveBar.Width = (int)(received * 463 / maximum);
            }
        }
        public void Set2ProgressBar(ulong received, ulong maximum)
        {
            if (TotalBar.Width <= 463)
            {
                TotalBar.Width = (int)(received * 463 / maximum);
            }
        }
        public void SetButtonsVisible(bool start, bool check, bool update)
        {
            BT_START.Visible = start;
            BT_CHECK.Visible = check;
            BT_UPDATE.Visible = update;
        }
        public void SetButtonsEnable(bool start, bool check, bool update)
        {
            BT_START.Enabled = start;
            BT_CHECK.Enabled = check;
            BT_UPDATE.Enabled = update;
        }
        #region APLICATIVO
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0201)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        #endregion    
        private void BT_CLOSE_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Você deseja sair?", "PBLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Environment.Exit(0);
          
            }
        }
        private void BT_MINIMIZE_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void BT_START_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath + $"\\PointBlank.exe"))
                {
                    LoadingStart = true;
                    DW_LABEL.Text = "Você pode iniciar o jogo.";
                    SetButtonsEnable(false, false, false);
                    BT_START.BackgroundImage = Resources.Start_Normal;
                    BT_CHECK.BackgroundImage = Resources.Check_Normal;
                    BT_UPDATE.BackgroundImage = Resources.Update_Normal;
                    Visible = false;
                    ShowInTaskbar = false;
                    Opacity = 0;
                    Process.Start(Application.StartupPath + $"\\PointBlank.exe", $"{Infos.PBLAUNCHER_KEY}");
                    Application.Exit();
                }
                else
                {
                    Logger.Log($"Não foi possível iniciar o jogo.");
                    MessageBox.Show($"Não foi possível iniciar o jogo.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Não foi possível iniciar o jogo. {ex}");
                MessageBox.Show($"Não foi possível iniciar o jogo.");
            }
        }
        private void BT_CHECK_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingVerif = true;
                SpeedCheck();
            }
            catch (Exception ex)
            {
                Logger.Log("[#] Ocorreu uma erro ao tentar verificar os arquivos do cliente.\n" + ex);
                MessageBox.Show("Ocorreu uma exceção ao tentar verificar os arquivos do cliente.");
                Application.Exit();
            }
        }
        #region SPEED CHECK
        private void SpeedCheck()
        {
            BT_CHECK.BackgroundImage = Resources.Check_Disable;
            BT_START.BackgroundImage = Resources.Start_Disable;
            Dictionary<string, string> USER_FILES = LoadUserFile($"{Application.StartupPath}\\UserFileList.dat");
            LoadingVerif = true;
            DW_LABEL.Text = $"Check";
            Logger.Log($"[#] Verificando a integridade dos arquivos.");
            FILE_LABEL.Visible = true;
            SetButtonsEnable(false, false, false);
            SetButtonsVisible(true, true, false);
            int count = 0;
            foreach (string item in USER_FILES.Keys)
            {
                count++;
                Application.DoEvents();
                string file = item.Substring(item.LastIndexOf("\\") + 1);
                string dirFile = item.Substring(item.LastIndexOf(startupPath) + 2);
                ArchiveBar.Width = 0;
                FILE_LABEL.Text = $"File  {dirFile}";
                if (USER_FILES.TryGetValue(item, out string str))
                {
                    if (!File.Exists(string.Concat(startupPath, item)))
                    {
                        ArchiveBar.Width = 168;
                        NOT_FOUND_FILE.Add(item);
                        Logger.Log($"Arquivo não encontrado: {item}");
                    }
                    else if (GetHashFile(string.Concat(startupPath, item)) != str)
                    {
                        ArchiveBar.Width = 242;
                        NOT_FOUND_FILE.Add(item);
                        Logger.Log($"Arquivo não encontrado: {item}");
                    }
                    Thread.Sleep(5);
                }
                ArchiveBar.Width = 463;
                Set2ProgressBar((ulong)count, (ulong)USER_FILES.Count);
                Update();
            }
            countCheck = 0;
            if (NOT_FOUND_FILE.Count > 0)
            {
                CountNotFound = NOT_FOUND_FILE.Count;
                ArchiveBar.Width = 0;
                TotalBar.Width = 463;
                Directory.CreateDirectory(Application.StartupPath + "\\_DownloadPatchFiles");
                Logger.Log($"Baixando [{CountNotFound}] arquivos inválidos.");
                MessageBox.Show($"Foi detectado [{CountNotFound}] arquivos inválidos .", "PBLauncher", MessageBoxButtons.OK, MessageBoxIcon.Question);
                DW_LABEL.Text = "Fazendo download dos arquivos de patch.";
                foreach (string item in NOT_FOUND_FILE)
                {
                    countCheck++;
                    Application.DoEvents();
                    Thread.Sleep(5);
                    string NotFoundItems = item;
                    string file = NotFoundItems.Substring(NotFoundItems.LastIndexOf("\\") + 1);
                    string dirFile = item.Substring(item.LastIndexOf(startupPath) + 1);
                    string folders = NotFoundItems.Remove(NotFoundItems.LastIndexOf("\\") + 1);
                    FILE_LABEL.Text = $"File {dirFile}";
                    try
                    {
                        Uri URL = new Uri($"{GameConection}/PBLauncher/Updates/Client/{NotFoundItems}.zip");
                        Directory.CreateDirectory($"{Application.StartupPath}\\_DownloadPatchFiles\\{folders}");
                        CHECK_UPDATE.DownloadFileAsync(URL, $"{Application.StartupPath}\\_DownloadPatchFiles\\{NotFoundItems}.zip");
                        if (File.Exists($"{Application.StartupPath}\\_DownloadPatchFiles\\{NotFoundItems}.zip"))
                        {
                            Thread.Sleep(5);
                            EXTRACT_FILES.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"O arquivo [{file}] não foi encontrado no servidor. Tente novamente mais tarde.");
                        Messages.WarningMessage($"O arquivo [{file}] não foi encontrado no servidor. Tente novamente mais tarde. \n\n {ex}", Application.ProductName);
                    }
                    Update();
                    ArchiveBar.Width = 463;
                    Set2ProgressBar((ulong)countCheck, (ulong)CountNotFound);
                    return;
                }
                TotalBar.Width = 463;
                Update();
            }
            else if (EXTRACT_FILES.Count == 0)
            {
                FILE_LABEL.Visible = true;
                SIZE_LABEL.Visible = false;
                LoadingVerif = false;
                Logger.Log($"Verificação concluida.");
                FILE_LABEL.Visible = true;
                SetButtonsEnable(true, true, false);
                SetButtonsVisible(true, true, false);
                DirectoryDelete();
                ArchiveBar.Width = 463;
                TotalBar.Width = 463;
                FILE_LABEL.Text = "File";
                DW_LABEL.Text = "Fim da Verificação. Você já pode jogar.";
                Logger.Log($"Foram detectados [{CountNotFound}] arquivos inválidos.");
                EXTRACT_FILES.Clear();
                NOT_FOUND_FILE.Clear();
                BT_START.BackgroundImage = Resources.Start_Normal;
                BT_CHECK.BackgroundImage = Resources.Check_Normal;
                BT_UPDATE.BackgroundImage = Resources.Update_Normal;
            }
        }
        #endregion SPEED CHECK

        #region Update
        private void BT_UPDATE_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingUpdate = true;
                int count = 0;
                foreach (string Patch in PatchFiles.Patch_Files.Keys)
                {
                    count++;
                    if (PatchFiles.Patch_Files.TryGetValue(Patch, out string URL))
                    {
                        int num = int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")) + 1;
                        Directory.CreateDirectory($"{Application.StartupPath}\\_DownloadPatchFiles");
                        FileExtract = string.Concat(Application.StartupPath, $"\\_DownloadPatchFiles\\{Patch}");
                        try
                        {
                            Uri uri = new Uri(URL);
                            WEB_UPDATE.DownloadFileAsync(uri, $"{Application.StartupPath}\\_DownloadPatchFiles\\{Patch}");
                            if (PatchFiles.Patch_Files.ContainsKey(Patch))
                            {
                                PatchFiles.Patch_Files.Remove(Patch);
                                PatchFiles.Patch_Files.Remove(URL);
                            }
                            Logger.Log($"[Patch] Fazendo download da nova atualização: {Patch}");
                        }
                        catch
                        {
                            Logger.Log($"Erro ao baixar o arquivo: [{Patch}]");
                            Messages.ErrorMessage($"Ocorreu um erro ao baixar o arquivo: {Patch}", Application.ProductName);
                        }
                        SetButtonsEnable(false, false, false);
                        BT_CHECK.Enabled = false;
                        FILE_LABEL.Visible = true;
                        Set2ProgressBar(0, (ulong)count);
                        Refresh();
                        Update();
                        DW_LABEL.Text = "Fazendo download dos arquivos de patch.";
                        FILE_LABEL.Text = $"File {Patch}";
                        return;
                    }
                }
                count = 0;
            }
            catch (Exception ex)
            {
                Logger.Log("[#] Ocorreu uma exceção ao tentar atualizar os arquivos do cliente ");
                MessageBox.Show("Ocorreu uma exceção ao tentar atualizar os arquivos do cliente \n" + ex);
            }
        }
        private void NEWS_UPDATE_Tick(object sender, EventArgs e)
        {
            LastVersion = Infos.PBLAUNCHER_VERSION_CLIENT_ATT;
            if (LastVersion == int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")))
            {
                SIZE_LABEL.Visible = false;
                LoadingUpdate = false;
                DirectoryDelete();
                ArchiveBar.Width = 463;
                TotalBar.Width = 463;
                SetButtonsEnable(false, true, false);
                SetButtonsVisible(true, true, false);
                FILE_LABEL.Text = "File";
                FILE_LABEL.Visible = true;
                FileDelete();
                NEWS_UPDATE.Stop();
                Thread.Sleep(500);
                DW_LABEL.Text = "Verifique os arquivos.";
                Logger.Log("[+] Aguardando verificação pós update.");
                BT_START.BackgroundImage = Resources.Start_Disable;
                BT_CHECK.BackgroundImage = Resources.Check_Normal;
                BT_UPDATE.BackgroundImage = Resources.Update_Normal;
            }
            else if (LastVersion > int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")))
            {
                LoadingUpdate = true;
                int count = 0;
                foreach (string Patch in PatchFiles.Patch_Files.Keys)
                {
                    count++;
                    if (PatchFiles.Patch_Files.TryGetValue(Patch, out string URL))
                    {
                        Directory.CreateDirectory(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles"));
                        FileExtract = string.Concat(startupPath, $"\\_DownloadPatchFiles\\{Patch}");
                        try
                        {
                            WebClient webClient = WEB_UPDATE;
                            Uri Uri = new Uri(URL);
                            string startupPath = string.Concat(Application.StartupPath, $"\\_DownloadPatchFiles\\{Patch}");
                            webClient.DownloadFileAsync(Uri, string.Concat(startupPath));
                            if (PatchFiles.Patch_Files.ContainsKey(Patch))
                            {
                                PatchFiles.Patch_Files.Remove(Patch);
                                PatchFiles.Patch_Files.Remove(URL);
                            }
                        }
                        catch
                        {
                            Messages.ErrorMessage($"Ocorreu um erro ao baixar a atualização.", Application.ProductName);
                            Logger.Log($"[#] Ocorreu um erro ao baixar a atualização. {Patch}");
                        }
                        SetButtonsEnable(false, false, false);
                        Set2ProgressBar((long)0, (ulong)count); // progressbar
                        DW_LABEL.Text = "Fazendo download dos arquivos de patch.";
                        FILE_LABEL.Text = $"File {Patch}";
                        NEWS_UPDATE.Stop();
                        return;
                    }
                }
            }
            else
            {
                DW_LABEL.Text = "Aguardando verificação dos arquivos.";
                SetButtonsEnable(false, true, false);
                SetButtonsVisible(true, true, false);
                Logger.Log("Launcher version greater than the server's version.");
                MessageBox.Show("Alteração inválida detectada.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        } 
        public void CheckPatchFiles()
        {
            if (NOT_FOUND_FILE.Count > 0)
            {
                ArchiveBar.Width = 0;
                Directory.CreateDirectory(Application.StartupPath + "\\_DownloadPatchFiles");
                DW_LABEL.Text = "Fazendo download dos arquivos ausentes.";
                foreach (string item in NOT_FOUND_FILE)
                {
                    countCheck++;
                    Application.DoEvents();
                    string NotFoundItems = item;
                    string file = NotFoundItems.Substring(NotFoundItems.LastIndexOf("\\") + 1);
                    string dirFile = item.Substring(item.LastIndexOf(startupPath) + 1);
                    string folders = NotFoundItems.Remove(NotFoundItems.LastIndexOf("\\") + 1);
                    Thread.Sleep(200);
                    FILE_LABEL.Text = $"File {dirFile}";
                    try
                    {
                        Uri URL = new Uri($"{GameConection}/PBLauncher/Updates/Client/{NotFoundItems}.zip");
                        Directory.CreateDirectory($"{Application.StartupPath}\\_DownloadPatchFiles\\{folders}");
                        CHECK_UPDATE.DownloadFileAsync(URL, $"{Application.StartupPath}\\_DownloadPatchFiles\\{NotFoundItems}.zip");
                        if (File.Exists($"{Application.StartupPath}\\_DownloadPatchFiles\\{NotFoundItems}.zip"))
                        {
                            EXTRACT_FILES.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log($"The file [{file}] was not found on the server. Try downloading again later!");
                        Messages.WarningMessage($"The file [{file}] was not found on the server. Try downloading again later! \n\n {ex}", Application.ProductName);
                    }
                    Update();
                    ArchiveBar.Width = 463;
                    Set2ProgressBar((ulong)countCheck, (ulong)CountNotFound);
                    return;
                }
            }
            else if (EXTRACT_FILES.Count == 0)
            {
                SIZE_LABEL.Visible = false;
                LoadingVerif = false;
                Logger.Log($"[#] Verifição da integridade concluida.");
                FILE_LABEL.Visible = false;
                //COUNT_FILES_LABEL.Visible = false;
                SetButtonsEnable(true, true, false);
                SetButtonsVisible(true, true, false);
                DirectoryDelete();
                ArchiveBar.Width = 463;
                TotalBar.Width = 463;
                DW_LABEL.Text = "Fim do Patch. Você já pode jogar";
                EXTRACT_FILES.Clear();
                NOT_FOUND_FILE.Clear();
                BT_START.BackgroundImage = Resources.Start_Normal;
                BT_CHECK.BackgroundImage = Resources.Check_Normal;
                BT_UPDATE.BackgroundImage = Resources.Update_Normal;
            }
        }

        public void CheckNewsUpdate()
        {
            LastVersion = Infos.PBLAUNCHER_VERSION_CLIENT_ATT;
            Update();
            if (LastVersion == int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")))
            {
                LoadingUpdate = false;
                DirectoryDelete();
                Update();
                FILE_LABEL.Text = "File";
                FILE_LABEL.Visible = true;
                DW_LABEL.Text = "Você pode iniciar o jogo.";
                BT_START.BackgroundImage = Resources.Start_Normal;
                BT_CHECK.BackgroundImage = Resources.Check_Normal;
                BT_UPDATE.BackgroundImage = Resources.Update_Normal;
                SetButtonsEnable(true, true, false);
                SetButtonsVisible(true, true, false);
                FileDelete();
                NEWS_UPDATE.Stop();
            }
            else if (LastVersion > int.Parse(LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")))
            {
                LoadingUpdate = true;
                Update();
                ArchiveBar.Width = 0;
                TotalBar.Width = 0;
                DW_LABEL.Text = "Há uma atualização disponível.";
                BT_UPDATE.BringToFront();
                BT_START.BackgroundImage = Resources.Start_Disable;
                BT_CHECK.BackgroundImage = Resources.Check_Disable;
                BT_UPDATE.BackgroundImage = Resources.Update_Normal;
                SetButtonsEnable(false, false, true);
                SetButtonsVisible(false, true, true);
            }
            else
            {
                DW_LABEL.Text = "Aguardando verificação dos arquivos.";
                SetButtonsEnable(false, true, false);
                SetButtonsVisible(true, true, false);
                Logger.Log("Launcher version greater than the server's version.");
                MessageBox.Show("Foi detectado uma alteração inválida.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        public bool CheckIntegrity()
        {
            bool flag = true;
            try
            {
                Dictionary<string, string> USER_FILE_LIST = LoadUserFile($"{Application.StartupPath}\\UserFileList.dat");
                if (USER_FILE_LIST != null)
                {
                    WindowState = FormWindowState.Minimized;
                    SetButtonsEnable(false, false, false);
                    List<string> NOT_FOUND_FILES = new List<string>();
                    DW_LABEL.Text = "Check";
                    string startupPath = Application.StartupPath;
                    foreach (string item in USER_FILE_LIST.Keys)
                    {
                        Application.DoEvents();
                        if (USER_FILE_LIST.TryGetValue(item, out string str) && (!File.Exists(string.Concat(startupPath, item)) || GetHashFile(string.Concat(startupPath, item)) != str))
                        {
                            NOT_FOUND_FILES.Add(item);
                        }
                    }
                    if (NOT_FOUND_FILES.Count > 0)
                    {
                        flag = false;
                    }
                    NOT_FOUND_FILES.Clear();
                }
            }
            catch
            {

            }
            return flag;
        }
        #endregion Update

        public void Unzip(string TargetDir, string ZipToUnpack)
        {
            string fileName = "";
            try
            {
                using (ZipFile zipFiles = ZipFile.Read(ZipToUnpack))
                {
                    zipFiles.ExtractProgress += new EventHandler<ExtractProgressEventArgs>(UnzipExtractProgressChanged);
                    int num = 0;
                    int num1 = 0;
                    foreach (ZipEntry zipEntry in zipFiles)
                    {
                        if (zipEntry.IsDirectory)
                        {
                            continue;
                        }
                        num1++;
                    }
                    FILE_LABEL.Visible = true;
                    foreach (ZipEntry zipEntry1 in zipFiles)
                    {
                        fileName = zipEntry1.FileName;
                        if (fileName.Contains("/"))
                        {
                            int test = fileName.LastIndexOf("/");
                            fileName = fileName.Substring(test + 1);
                        }
                        if (!zipEntry1.IsDirectory)
                        {
                            FILE_LABEL.Text = $"File {fileName}";
                            Update();
                            Refresh();
                            int num3 = num + 1;
                            num = num3;
                            Set2ProgressBar((ulong)num3, (ulong)num1);
                        }
                        zipEntry1.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch
            {
                Logger.Log($"There was an error extracting the update. [{fileName}] [0x00000001]");
                MessageBox.Show($"There was an error extracting the update. [0x00000001]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FileDelete()
        {
            try
            {
                if (File.Exists(string.Concat(Application.StartupPath, "\\FileDelete.xml")))
                {
                    List<XMLModel> xMLModels = Parse(string.Concat(Application.StartupPath, "\\FileDelete.xml"));
                    int num = 0;
                    SetButtonsEnable(false, false, false);
                    FILE_LABEL.Visible = true;
                    foreach (XMLModel xMLModel in xMLModels)
                    {
                        num++;
                        if (File.Exists(string.Concat(Application.StartupPath, xMLModel.local)))
                        {
                            FILE_LABEL.Text = $"File {xMLModel.local}";
                            File.Delete(string.Concat(Application.StartupPath, xMLModel.local));
                        }
                        Set2ProgressBar((ulong)num, (ulong)xMLModels.Count);
                    }
                    File.Delete(string.Concat(Application.StartupPath, "\\FileDelete.xml"));
                    DW_LABEL.Text = "Fim do Patch. Você já pode jogar";
                    FILE_LABEL.Visible = false;
                    SetButtonsEnable(true, true, false);
                }
            }
            catch
            {
                Messages.ErrorMessage("An error occurred with the FileDelete file.", Application.ProductName);
                Logger.Log("An error occurred with the FileDelete file.");
            }
        }

        public void DirectoryDelete()
        {
            if (Directory.Exists(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles")))
            {
                Directory.Delete(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles"), true);
            }
        }

        public static Dictionary<string, string> LoadUserFile(string path)
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            XmlDocument xmlDocument = new XmlDocument();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            if (fileStream.Length != (long)0)
            {
                try
                {
                    xmlDocument.Load(fileStream);
                    for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
                    {
                        if ("list".Equals(i.Name))
                        {
                            for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
                            {
                                if ("file".Equals(j.Name))
                                {
                                    XmlNamedNodeMap attributes = j.Attributes;
                                    string value = attributes.GetNamedItem("local").Value;
                                    if (!strs.ContainsKey(value))
                                    {
                                        strs.Add(value, attributes.GetNamedItem("hash").Value);
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Logger.Log($"An error occurred with the XML file.");
                    Messages.ErrorMessage("An error occurred with the XML file.", Application.ProductName);
                }
            }
            fileStream.Dispose();
            fileStream.Close();
            return strs;
        }

        private List<XMLModel> Parse(string path)
        {
            List<XMLModel> xMLModels = new List<XMLModel>();
            XmlDocument xmlDocument = new XmlDocument();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            if (fileStream.Length != (long)0)
            {
                try
                {
                    xmlDocument.Load(fileStream);
                    for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
                    {
                        if ("list".Equals(i.Name))
                        {
                            for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
                            {
                                if ("file".Equals(j.Name))
                                {
                                    XmlNamedNodeMap attributes = j.Attributes;
                                    xMLModels.Add(new XMLModel(attributes.GetNamedItem("local").Value));
                                }
                            }
                        }
                    }
                }
                catch
                {
                    Messages.ErrorMessage("An error occurred with the XML file.", Application.ProductName);
                    Logger.Log("An error occurred with the XML file.");
                }
            }
            fileStream.Dispose();
            fileStream.Close();
            return xMLModels;
        }

        public static string LerArquivo(string arquivo, string secao, string chave, string valorPadrao)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            GetPrivateProfileString(secao, chave, valorPadrao, stringBuilder, 500, arquivo);
            return stringBuilder.ToString();
        }

        public string GetHashFile(string fileName)
        {
            string str = "";
            try
            {
                if (File.Exists(fileName) && !string.IsNullOrEmpty(fileName))
                {
                    FileStream FS = File.OpenRead(fileName);
                    str = BitConverter.ToString((new MD5CryptoServiceProvider()).ComputeHash(FS)).Replace("-", string.Empty);
                    FS.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unknown error has occurred! [0x0000002]");
                Logger.Log("GetHashFile Exception Detected:\n" + ex);
            }
            return str;
        }
        #region BUTTONS
        private void BT_CHECK_MouseLeave(object sender, EventArgs e)
        {
            BT_CHECK.BackgroundImage = Resources.Check_Normal;
            BT_CHECK.BackColor = Color.Transparent;
        }
        private void BT_CHECK_MouseMove(object sender, MouseEventArgs e)
        {
            BT_CHECK.BackgroundImage = Resources.Check_Over;
            BT_CHECK.BackColor = Color.Transparent;
        }
        private void BT_START_MouseLeave(object sender, EventArgs e)
        {

            BT_START.BackgroundImage = Resources.Start_Normal;
            BT_START.BackColor = Color.Transparent;
        }
        private void BT_START_MouseMove(object sender, MouseEventArgs e)
        {
            BT_START.BackgroundImage = Resources.Start_Over;
            BT_START.BackColor = Color.Transparent;
        }
        private void BT_UPDATE_MouseLeave(object sender, EventArgs e)
        {
            BT_UPDATE.BackgroundImage = Resources.Update_Normal;
            BT_UPDATE.BackColor = Color.Transparent;
        }
        private void BT_UPDATE_MouseMove(object sender, MouseEventArgs e)
        {
            BT_UPDATE.BackgroundImage = Resources.Update_Over;
            BT_UPDATE.BackColor = Color.Transparent;
        }
        #endregion BUTTONS
        private void Fechar_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Você deseja sair?", "PBLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }
        private void Fechar_MouseEnter(object sender, EventArgs e)
        {
            Fechar.BackgroundImage = Resources.Closed_Over;
            Fechar.BackColor = Color.Transparent;
        }
        private void Fechar_MouseLeave(object sender, EventArgs e)
        {
            Fechar.BackgroundImage = Resources.Closed_Normal;
            Fechar.BackColor = Color.Transparent;
        }
        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void Minimize_MouseEnter(object sender, EventArgs e)
        {
            Minimize.BackgroundImage = Resources.Hide_Over;
            Minimize.BackColor = Color.Transparent;
        }
        private void Minimize_MouseLeave(object sender, EventArgs e)
        {
            Minimize.BackgroundImage = Resources.Hide_Normal;
            Minimize.BackColor = Color.Transparent;
        }
    }
}