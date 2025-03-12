namespace PBLauncher
{
    partial class frmPBLauncher
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPBLauncher));
            this.NEWS_UPDATE = new System.Windows.Forms.Timer(this.components);
            this.VERSION_LABEL = new System.Windows.Forms.Label();
            this.DW_LABEL = new System.Windows.Forms.Label();
            this.FILE_LABEL = new System.Windows.Forms.Label();
            this.TotalBar = new System.Windows.Forms.PictureBox();
            this.ArchiveBar = new System.Windows.Forms.PictureBox();
            this.BT_START = new System.Windows.Forms.Button();
            this.BT_CHECK = new System.Windows.Forms.Button();
            this.BT_UPDATE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SIZE_LABEL = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_end = new System.Windows.Forms.Panel();
            this.Minimize = new System.Windows.Forms.PictureBox();
            this.Fechar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TotalBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArchiveBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fechar)).BeginInit();
            this.SuspendLayout();
            // 
            // NEWS_UPDATE
            // 
            this.NEWS_UPDATE.Tick += new System.EventHandler(this.NEWS_UPDATE_Tick);
            // 
            // VERSION_LABEL
            // 
            this.VERSION_LABEL.AutoSize = true;
            this.VERSION_LABEL.BackColor = System.Drawing.Color.Transparent;
            this.VERSION_LABEL.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.VERSION_LABEL.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.VERSION_LABEL.Location = new System.Drawing.Point(2, 6);
            this.VERSION_LABEL.Name = "VERSION_LABEL";
            this.VERSION_LABEL.Size = new System.Drawing.Size(0, 16);
            this.VERSION_LABEL.TabIndex = 0;
            // 
            // DW_LABEL
            // 
            this.DW_LABEL.AutoSize = true;
            this.DW_LABEL.BackColor = System.Drawing.Color.Transparent;
            this.DW_LABEL.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.DW_LABEL.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DW_LABEL.Location = new System.Drawing.Point(11, 443);
            this.DW_LABEL.Name = "DW_LABEL";
            this.DW_LABEL.Size = new System.Drawing.Size(71, 16);
            this.DW_LABEL.TabIndex = 1;
            this.DW_LABEL.Text = "Non String";
            // 
            // FILE_LABEL
            // 
            this.FILE_LABEL.AutoSize = true;
            this.FILE_LABEL.BackColor = System.Drawing.Color.Transparent;
            this.FILE_LABEL.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.FILE_LABEL.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FILE_LABEL.Location = new System.Drawing.Point(11, 463);
            this.FILE_LABEL.Name = "FILE_LABEL";
            this.FILE_LABEL.Size = new System.Drawing.Size(27, 16);
            this.FILE_LABEL.TabIndex = 2;
            this.FILE_LABEL.Text = "File";
            // 
            // TotalBar
            // 
            this.TotalBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(172)))), ((int)(((byte)(240)))));
            this.TotalBar.Location = new System.Drawing.Point(14, 514);
            this.TotalBar.Name = "TotalBar";
            this.TotalBar.Size = new System.Drawing.Size(463, 9);
            this.TotalBar.TabIndex = 5;
            this.TotalBar.TabStop = false;
            // 
            // ArchiveBar
            // 
            this.ArchiveBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(172)))), ((int)(((byte)(240)))));
            this.ArchiveBar.Location = new System.Drawing.Point(14, 481);
            this.ArchiveBar.Name = "ArchiveBar";
            this.ArchiveBar.Size = new System.Drawing.Size(463, 9);
            this.ArchiveBar.TabIndex = 6;
            this.ArchiveBar.TabStop = false;
            // 
            // BT_START
            // 
            this.BT_START.BackColor = System.Drawing.Color.Transparent;
            this.BT_START.BackgroundImage = global::PBLauncher.Properties.Resources.Start_Normal;
            this.BT_START.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BT_START.Enabled = false;
            this.BT_START.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BT_START.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BT_START.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_START.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.BT_START.Location = new System.Drawing.Point(623, 450);
            this.BT_START.Name = "BT_START";
            this.BT_START.Size = new System.Drawing.Size(166, 76);
            this.BT_START.TabIndex = 10;
            this.BT_START.UseVisualStyleBackColor = false;
            this.BT_START.Click += new System.EventHandler(this.BT_START_Click);
            this.BT_START.MouseLeave += new System.EventHandler(this.BT_START_MouseLeave);
            this.BT_START.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BT_START_MouseMove);
            // 
            // BT_CHECK
            // 
            this.BT_CHECK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.BT_CHECK.BackgroundImage = global::PBLauncher.Properties.Resources.Check_Normal;
            this.BT_CHECK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BT_CHECK.Enabled = false;
            this.BT_CHECK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BT_CHECK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BT_CHECK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_CHECK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.BT_CHECK.Location = new System.Drawing.Point(507, 450);
            this.BT_CHECK.Name = "BT_CHECK";
            this.BT_CHECK.Size = new System.Drawing.Size(106, 76);
            this.BT_CHECK.TabIndex = 11;
            this.BT_CHECK.UseVisualStyleBackColor = false;
            this.BT_CHECK.Click += new System.EventHandler(this.BT_CHECK_Click);
            this.BT_CHECK.MouseLeave += new System.EventHandler(this.BT_CHECK_MouseLeave);
            this.BT_CHECK.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BT_CHECK_MouseMove);
            // 
            // BT_UPDATE
            // 
            this.BT_UPDATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.BT_UPDATE.BackgroundImage = global::PBLauncher.Properties.Resources.Start_Normal;
            this.BT_UPDATE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BT_UPDATE.Enabled = false;
            this.BT_UPDATE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BT_UPDATE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BT_UPDATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_UPDATE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(32)))), ((int)(((byte)(45)))));
            this.BT_UPDATE.Location = new System.Drawing.Point(623, 450);
            this.BT_UPDATE.Margin = new System.Windows.Forms.Padding(0);
            this.BT_UPDATE.Name = "BT_UPDATE";
            this.BT_UPDATE.Size = new System.Drawing.Size(166, 76);
            this.BT_UPDATE.TabIndex = 14;
            this.BT_UPDATE.UseVisualStyleBackColor = false;
            this.BT_UPDATE.Click += new System.EventHandler(this.BT_UPDATE_Click);
            this.BT_UPDATE.MouseLeave += new System.EventHandler(this.BT_UPDATE_MouseLeave);
            this.BT_UPDATE.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BT_UPDATE_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(11, 494);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Total";
            // 
            // SIZE_LABEL
            // 
            this.SIZE_LABEL.AutoSize = true;
            this.SIZE_LABEL.BackColor = System.Drawing.Color.Transparent;
            this.SIZE_LABEL.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.SIZE_LABEL.ForeColor = System.Drawing.Color.White;
            this.SIZE_LABEL.Location = new System.Drawing.Point(242, 463);
            this.SIZE_LABEL.Name = "SIZE_LABEL";
            this.SIZE_LABEL.Size = new System.Drawing.Size(61, 16);
            this.SIZE_LABEL.TabIndex = 17;
            this.SIZE_LABEL.Text = "0.00/0.00";
            this.SIZE_LABEL.Visible = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(342, 277);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(436, 146);
            this.webBrowser1.TabIndex = 22;
            this.webBrowser1.Url = new System.Uri("http://localhost/PBLauncher/Anuncio/", System.UriKind.Absolute);
            // 
            // panel_end
            // 
            this.panel_end.BackColor = System.Drawing.Color.Transparent;
            this.panel_end.Location = new System.Drawing.Point(0, 441);
            this.panel_end.Name = "panel_end";
            this.panel_end.Size = new System.Drawing.Size(501, 90);
            this.panel_end.TabIndex = 23;
            // 
            // Minimize
            // 
            this.Minimize.BackColor = System.Drawing.Color.Transparent;
            this.Minimize.BackgroundImage = global::PBLauncher.Properties.Resources.Hide_Normal;
            this.Minimize.Location = new System.Drawing.Point(735, 2);
            this.Minimize.Margin = new System.Windows.Forms.Padding(2);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(24, 24);
            this.Minimize.TabIndex = 30;
            this.Minimize.TabStop = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            this.Minimize.MouseEnter += new System.EventHandler(this.Minimize_MouseEnter);
            this.Minimize.MouseLeave += new System.EventHandler(this.Minimize_MouseLeave);
            // 
            // Fechar
            // 
            this.Fechar.BackColor = System.Drawing.Color.Transparent;
            this.Fechar.BackgroundImage = global::PBLauncher.Properties.Resources.Closed_Normal;
            this.Fechar.Location = new System.Drawing.Point(765, 2);
            this.Fechar.Margin = new System.Windows.Forms.Padding(2);
            this.Fechar.Name = "Fechar";
            this.Fechar.Size = new System.Drawing.Size(24, 24);
            this.Fechar.TabIndex = 29;
            this.Fechar.TabStop = false;
            this.Fechar.Click += new System.EventHandler(this.Fechar_Click);
            this.Fechar.MouseEnter += new System.EventHandler(this.Fechar_MouseEnter);
            this.Fechar.MouseLeave += new System.EventHandler(this.Fechar_MouseLeave);
            // 
            // frmPBLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PBLauncher.Properties.Resources.BG_PB_USA;
            this.ClientSize = new System.Drawing.Size(790, 531);
            this.Controls.Add(this.Minimize);
            this.Controls.Add(this.Fechar);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.SIZE_LABEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TotalBar);
            this.Controls.Add(this.ArchiveBar);
            this.Controls.Add(this.DW_LABEL);
            this.Controls.Add(this.FILE_LABEL);
            this.Controls.Add(this.BT_CHECK);
            this.Controls.Add(this.VERSION_LABEL);
            this.Controls.Add(this.BT_START);
            this.Controls.Add(this.panel_end);
            this.Controls.Add(this.BT_UPDATE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPBLauncher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PBLauncher";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPBLauncher_FormClosed);
            this.Load += new System.EventHandler(this.FrmPBLauncher_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmPBLauncher_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.TotalBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArchiveBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fechar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer NEWS_UPDATE;
        public System.Windows.Forms.Label VERSION_LABEL;
        public System.Windows.Forms.Label DW_LABEL;
        public System.Windows.Forms.Label FILE_LABEL;
        public System.Windows.Forms.PictureBox TotalBar;
        public System.Windows.Forms.PictureBox ArchiveBar;
        public System.Windows.Forms.Button BT_START;
        public System.Windows.Forms.Button BT_CHECK;
        public System.Windows.Forms.Button BT_UPDATE;
        private System.Windows.Forms.Label SIZE_LABEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel_end;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.PictureBox Fechar;
    }
}

