namespace PBLauncher
{
    partial class Connection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connection));
            this.STATUS_LABEL = new System.Windows.Forms.Label();
            this.PBLAUNCHER_UPDATES = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // STATUS_LABEL
            // 
            this.STATUS_LABEL.AutoSize = true;
            this.STATUS_LABEL.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.STATUS_LABEL.Location = new System.Drawing.Point(77, 13);
            this.STATUS_LABEL.Name = "STATUS_LABEL";
            this.STATUS_LABEL.Size = new System.Drawing.Size(103, 13);
            this.STATUS_LABEL.TabIndex = 0;
            this.STATUS_LABEL.Text = "Por favor, aguarde.";
            this.STATUS_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PBLAUNCHER_UPDATES
            // 
            this.PBLAUNCHER_UPDATES.Tick += new System.EventHandler(this.PBLAUNCHER_UPDATES_Tick);
            // 
            // frmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 41);
            this.ControlBox = false;
            this.Controls.Add(this.STATUS_LABEL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label STATUS_LABEL;
        private System.Windows.Forms.Timer PBLAUNCHER_UPDATES;
    }
}