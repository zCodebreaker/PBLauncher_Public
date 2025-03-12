namespace Updater
{
    partial class frmUpdating
    {
		   
     
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdating));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.info_label = new System.Windows.Forms.Label();
            this.file_label = new System.Windows.Forms.Label();
            this.size_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(32, 39);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(365, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Location = new System.Drawing.Point(31, 65);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(52, 13);
            this.info_label.TabIndex = 1;
            this.info_label.Text = "info_label";
            // 
            // file_label
            // 
            this.file_label.AutoSize = true;
            this.file_label.Location = new System.Drawing.Point(29, 23);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(48, 13);
            this.file_label.TabIndex = 2;
            this.file_label.Text = "file_label";
            // 
            // size_label
            // 
            this.size_label.AutoSize = true;
            this.size_label.BackColor = System.Drawing.Color.Transparent;
            this.size_label.Location = new System.Drawing.Point(344, 65);
            this.size_label.Name = "size_label";
            this.size_label.Size = new System.Drawing.Size(53, 13);
            this.size_label.TabIndex = 3;
            this.size_label.Text = "size_label";
            // 
            // frmUpdating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 97);
            this.Controls.Add(this.size_label);
            this.Controls.Add(this.file_label);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdating";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PBLauncher Updater";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmUpdating_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label info_label;
        private System.Windows.Forms.Label file_label;
        private System.Windows.Forms.Label size_label;
    }
}