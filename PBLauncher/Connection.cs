using Ionic.Zip;
using Npgsql;
using PBLauncher.Properties;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PBLauncher
{
     
     
    public partial class Connection : Form
    {
        private NpgsqlConnection bdConn;
        string startupPath = Application.StartupPath;
        bool prosseguir = false;
        WebClient WebUserFile = new WebClient();
        public string FileExtract = null;

        public Connection()
        {
            InitializeComponent();
            Infos.PGSQL_CONNECTION = "Server=127.0.0.1;" + "Port=5432;" + "User Id=postgres;" + "Password=adryan12;" + "Database=postgres;";
            bdConn = new NpgsqlConnection(Infos.PGSQL_CONNECTION);
        }
        private void FrmConnection_Load(object sender, EventArgs e)
        {
            if (CheckFiles())
            {
                if (OpenDatabase() && CheckStatus())
                {
                    if (prosseguir == true)
                    {
                        PBLAUNCHER_UPDATES.Start();
                    }
                    else
                    {
                        Logger.Log("Could not connect to server. [0x0000001]");
                        MessageBox.Show("Não foi possível conectar ao servidor.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
            }
        }
        public bool CheckStatus()
        {
            switch (Infos.PBLAUNCHER_CONNECTION_TYPE)
            {
                case "ONLINE":
                    {    
                        return true;
                    }
                case "EVENT":
                    {
                        MessageBox.Show(Infos.PBLAUNCHER_MESSAGE_EVENT, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                case "OFFLINE":
                    {
                        Logger.Log("[##] Não foi possível conectar ao servidor.");
                        MessageBox.Show("Não foi possível conectar ao servidor.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return false;
                    }
                case "MAINTENANCE":
                    {
                        Logger.Log("PBLAUNCHER_MESSAGE_MAINTENANCE: Não foi possível conectar ao servidor.");
                        MessageBox.Show(Infos.PBLAUNCHER_MESSAGE_MAINTENANCE, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return false;
                    }
                default:
                    {
                        Logger.Log("[##] Não foi possível conectar ao servidor.");
                        MessageBox.Show("Não foi possível conectar ao servidor.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return false;
                    }
            }
        }

        public bool CheckFiles()
        {
            Logger.Log("Verificando arquivos importantes.");
            if (!File.Exists($"{startupPath}//DotNetZip.dll"))
            {
                MessageBox.Show($"DotNetZip não encontrado.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Log("[>>] Arquivo ausente: DotNetZip.dll.");
                Application.Exit();
                return false;
            }
            Logger.Log("[#] Config.zpt.");
            if (!File.Exists($"{startupPath}//Config.zpt"))
            {
                MessageBox.Show($"Arquivo de configurações não encontrado.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Log("[>>] Arquivo ausente: Config.zpt.");
                Application.Exit();
                return false;
            }
            Logger.Log("[#] UserFileList.dat.");
            if (!File.Exists($"{startupPath}//UserFileList.dat"))
            {
                MessageBox.Show($"Arquivo UserFileList.dat não encontrado.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Log("[>>] Arquivo ausente: UserFileList.dat.");
                Application.Exit();
                return false;
            }
            Logger.Log("[#] Npgsql.dll.");
            if (!File.Exists($"{startupPath}//Npgsql.dll"))
            {
                Logger.Log("[>>] Arquivo ausente:Npgsql.dll.");
    
                Application.Exit();
                return false;
            }
            Logger.Log("[#] Arquivos verificados.");
            return true;
        }
		public void DirectoryDelete()
        {
            if (Directory.Exists(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles")))
            {
                Directory.Delete(string.Concat(Application.StartupPath, "\\_DownloadPatchFiles"), true);
            }
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
            catch
            {
            } 
            return str;
        }

        private void PBLAUNCHER_UPDATES_Tick(object sender, EventArgs e)
        {
            Logger.Log("Verificando atualizações do PBLauncher.");
            WebClient web = new WebClient();
            int versionPBLauncher = int.Parse(Application.ProductVersion.Replace(".", ""));
            PBLAUNCHER_UPDATES.Stop();
            if (versionPBLauncher < Infos.PBLAUNCHER_VERSION)
            {
                Visible = false;
                ShowInTaskbar = false;
                Opacity = 0;
                Logger.Log("[#] Há uma atualização disponível do Launcher");
                File.WriteAllBytes($"{startupPath}//Updater.exe", Resources.Updater);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Logger.Log("Iniciando atualização.");
                if (File.Exists($"{startupPath}//Updater.exe"))
                {
                    Process.Start($"{startupPath}//Updater.exe", "0xxt");
                }
            }
            else if (versionPBLauncher == Infos.PBLAUNCHER_VERSION)
            {
                Logger.Log("Nenhuma atualização disponível foi encontrada.");
                Visible = false;
                ShowInTaskbar = false;
                Opacity = 0;
                new frmPBLauncher().Show();
            }
            else
            {
                Visible = false;
                ShowInTaskbar = false;
                Opacity = 0;
                Logger.Log("Há uma atualização disponível.");
                File.WriteAllBytes($"{startupPath}//Updater.exe", Resources.Updater);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Logger.Log("Iniciando atualização do PBLauncher.");
                if (File.Exists($"{startupPath}//Updater.exe"))
                {
                    Process.Start($"{startupPath}//Updater.exe");
                }
            }
        }

        #region Database Connection and Information Strings
        public bool OpenDatabase()
        {
            try
            {
                bdConn.Open();
            }
            catch
            {
                bdConn.Close();
                MessageBox.Show($"Não foi possível conectar ao banco de dados.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.Log("Não foi possível conectar ao banco de dado. [0x00000001]");
                Application.Exit();
            }
            if (bdConn.State == ConnectionState.Open)
            {
                try
                {             
                    LoadTables();
                    prosseguir = true;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Application.Exit();
                    return false;
                }
            }
            bdConn.Close();
            return false;
        }

        public void LoadTables()
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from Connection", bdConn);
                NpgsqlDataReader leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Infos.PBLAUNCHER_CONNECTION_TYPE = leitor["CONNECTION_TYPE"].ToString();
                    Infos.PBLAUNCHER_VERSION_CLIENT_ATT = int.Parse(leitor["LAST_CLIENT_VERSION"].ToString());
                    Infos.PBLAUNCHER_VERSION = int.Parse(leitor["LAST_PBLAUNCHER_VERSION"].ToString());
                    Infos.PBLAUNCHER_ADRESS = leitor["IP_ADRESS"].ToString();
                    Infos.PBLAUNCHER_KEY = leitor["LAUNCHER_KEY"].ToString();
                    Infos.PBLAUNCHER_USERFILELIST = leitor["USERFILELIST"].ToString();
                }
                leitor.Close();

                cmd = new NpgsqlCommand("SELECT * from Messages", bdConn);
                leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Infos.PBLAUNCHER_URL_ANNOUNCE = leitor["PBLAUNCHER_ANNOUNCE"].ToString();
                    Infos.PBLAUNCHER_MESSAGE_MAINTENANCE = leitor["PBLAUNCHER_MAINTENANCE_MESSAGE"].ToString();
                    Infos.PBLAUNCHER_MESSAGE_EVENT = leitor["PBLAUNCHER_EVENT_MESSAGE"].ToString();
                }
                leitor.Close();

                cmd = new NpgsqlCommand("SELECT * from Webs", bdConn);
                leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    Infos.PBLAUNCHER_URL_WEBSITE = leitor["WebSite"].ToString();
                    Infos.PBLAUNCHER_URL_DONATE = leitor["Donate"].ToString();
                    Infos.PBLAUNCHER_URL_PINCODE = leitor["Pincode"].ToString();
                    Infos.PBLAUNCHER_URL_FACEBOOK_GROUP = leitor["Facebook_Group"].ToString();
                    Infos.PBLAUNCHER_URL_FACEBOOK_FANPAGE = leitor["Facebook_FanPage"].ToString();
                }
                leitor.Close();

                cmd = new NpgsqlCommand("SELECT * from Updatesclient", bdConn);
                leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    if (int.Parse(leitor["Version"].ToString()) > int.Parse(frmPBLauncher.LerArquivo(string.Concat(Application.StartupPath, "\\Config.zpt"), "PBLauncher", "Public_Version", "0")))
                    {
                        PatchFiles.Patch_Files.Add(leitor["Patch"].ToString(), leitor["URL"].ToString());
                    }
                }
                leitor.Close();

                cmd = new NpgsqlCommand("SELECT * from Updatesuserfilelist", bdConn);
                leitor = cmd.ExecuteReader();
                while (leitor.Read())
                {
                    if (Infos.PBLAUNCHER_USERFILELIST != GetHashFile($"{startupPath}\\userfilelist.dat"))
                    {
                        UserFilePatch.UserPatchFile.Add(leitor["Patch"].ToString(), leitor["URL"].ToString());
                    }
                }
                leitor.Close();
            }
            catch (Exception ex) { Messages.ErrorMessage(ex.ToString(), Application.ProductName); }
        }
        #endregion
    }
}
