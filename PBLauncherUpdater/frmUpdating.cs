using Ionic.Zip;
using Microsoft.VisualBasic.Devices;
using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Updater
{
    public partial class frmUpdating : Form
    {		         
        private NpgsqlConnection bdConn; //NpgSql
        public WebClient LauncherUpdater = new WebClient();
        public Computer MyComputer = new Computer();
        public static string Host = $"http://{Infos.IP}";
        public static bool prosseguir;
        public static string FileExtract;
        public frmUpdating()
        {
            InitializeComponent();
            Infos.PGSQL_CONNECTION = "Server=127.0.0.1;" + "Port=5432;" + "User Id=postgres;" + "Password=adryan12;" + "Database=postgres;";
            bdConn = new NpgsqlConnection(Infos.PGSQL_CONNECTION);
            LauncherUpdater.DownloadFileCompleted += new AsyncCompletedEventHandler(LauncherUpdateDownloadCompleted);
            LauncherUpdater.DownloadProgressChanged += new DownloadProgressChangedEventHandler(LauncherUpdateDownloadProgressChanged);
        }
        private void FrmUpdating_Load(object sender, EventArgs e)
        {
            ConnectionDb();
            timer1.Start();
        }
        public void ConnectionDb()
        {
            try
            {
                bdConn.Open();
            }
            catch
            {
                bdConn.Close();
                MessageBox.Show("Unable to connect. [0x00000001]", "PBLauncher");
                Logger.Log("Unable to connect. [0x00000001]");
                Application.Exit();
            }
            //Verifica se a conexão está aberta
            if (bdConn.State == ConnectionState.Open)
            {
                try
                {
                    LoadTables();
                    prosseguir = true;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); Application.Exit(); }
            }
            bdConn.Close();
        }
        void LoadTables()
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from Connection", bdConn);
                NpgsqlDataReader leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Infos.PBLAUNCHER_VERSION_ATUAL = leitor["LAST_PBLAUNCHER_VERSION"].ToString();
                    Infos.IP = leitor["IP_ADRESS"].ToString();
                }
                leitor.Close();
                FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, "PBLauncher.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\PBLauncher.exe");
                cmd = new NpgsqlCommand("SELECT * from updateslauncher", bdConn);
                leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    if (int.Parse(leitor["Version"].ToString()) > int.Parse(myFileVersionInfo.ProductVersion.ToString().Replace(".", "")))
                    {
                        FilePatch.Files.Add(leitor["Patch"].ToString(), leitor["URL"].ToString());
                    }
                }
                leitor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Application.ProductName);
            }
        }

        public void SetProgressBar(long received, long maximum)
        {
            if (progressBar1.Value <= 100)
                progressBar1.Value = (int)(received * 100 / maximum);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FileVersionInfo.GetVersionInfo(Path.Combine(Application.StartupPath, "PBLauncher.exe"));
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\PBLauncher.exe");
            if (int.Parse(myFileVersionInfo.ProductVersion.ToString().Replace(".", "")) < int.Parse(Infos.PBLAUNCHER_VERSION_ATUAL))
            {
                foreach (string Patch in FilePatch.Files.Keys)
                {
                    FileExtract = string.Concat(Application.StartupPath, $"\\_LauncherPatchFiles\\{Patch}");
                    if (FilePatch.Files.TryGetValue(Patch, out string URL))
                    {
                        try
                        {
                            MyComputer.FileSystem.CreateDirectory(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles"));
                            WebClient webClient = LauncherUpdater;
                            Uri uri = new Uri(URL);
                            string startupPath = string.Concat(Application.StartupPath, $"\\_LauncherPatchFiles\\{Patch}");
                            Logger.Log("[1] Baixando atualização do PBLauncher");
                            webClient.DownloadFileAsync(uri, string.Concat(startupPath));
                            if (FilePatch.Files.ContainsKey(Patch))
                            {
                                FilePatch.Files.Remove(Patch);
                                FilePatch.Files.Remove(URL);
                            }
                        }
                        catch
                        {
                            Logger.Log($"There was an error downloading the update. [{Patch}]");
                            MessageBox.Show($"There was an error downloading the update. [{Patch}]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                        }
                    }
                    info_label.Text = "[!] Descompactando arquivos da autalização.";
                    file_label.Text = $"File: {Patch}";
                    return;
                }
                timer1.Stop();
            }
            else
            {
                timer1.Stop();
                Logger.Log("[!] Atualização concluida.");
                Process.Start($"{Application.StartupPath}\\PBLauncher.exe");
                Application.Exit();
            }
        }

        private void LauncherUpdateDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            size_label.Visible = true;
            SetProgressBar(e.BytesReceived, e.TotalBytesToReceive);
            string size = string.Format("{0} MB's/{1} MB's", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            size_label.Text = $"Downloaded: {size}";
        }

        private void LauncherUpdateDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            info_label.Text = "Extraindo atualização.";
            progressBar1.Value = 0;
            Logger.Log("[!] Extraindo atualização.");
            Unzip(Application.StartupPath, FileExtract);
            size_label.Visible = false;
            Directory.Delete(string.Concat(Application.StartupPath, "\\_LauncherPatchFiles"), true);
            progressBar1.Value = 100;
            timer1.Start();
        }

        public void Unzip(string TargetDir, string ZipToUnpack)
        {
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
                    file_label.Visible = true;
                    foreach (ZipEntry zipEntry1 in zipFiles)
                    {
                        string fileName = zipEntry1.FileName;
                        if (fileName.Contains("/"))
                        {
                            int num2 = fileName.LastIndexOf("/");
                            fileName = fileName.Substring(num2 + 1);
                        }
                        if (!zipEntry1.IsDirectory)
                        {
                            file_label.Text = fileName;
                            Update();
                            Refresh();
                            int num3 = num + 1;
                            num = num3;
                        }
                        zipEntry1.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch
            {
                Logger.Log("[PBLAUNCHER_UPDATER] There was an error extracting the update. [0x00000001]");
                MessageBox.Show("There was an error extracting the update. [0x00000001]", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }
        }
        private void UnzipExtractProgressChanged(object sender, ExtractProgressEventArgs e)
        {
            try
            {
                if (e.TotalBytesToTransfer != 0)
                {
                    SetProgressBar(e.BytesTransferred, e.TotalBytesToTransfer);
                }
                progressBar1.Refresh();
                progressBar1.Update();
            }
            catch
            {
                Logger.Log("[PBLAUNCHER_UPDATER] There was an error extracting the update. [0x00000002]");
                MessageBox.Show("An error occurred in the update extraction progress.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}