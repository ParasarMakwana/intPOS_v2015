namespace SFPOSWindows
{
    partial class frmDataSync2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataSync2));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar.Location = new System.Drawing.Point(23, 142);
            this.progressBar.MinimumSize = new System.Drawing.Size(50, 23);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(252, 23);
            this.progressBar.TabIndex = 9;
            this.progressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressBar.Value = 100;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(21, 116);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(147, 16);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Data sync in progress...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPre
            // 
            this.lblPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPre.AutoSize = true;
            this.lblPre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPre.Location = new System.Drawing.Point(236, 168);
            this.lblPre.Name = "lblPre";
            this.lblPre.Size = new System.Drawing.Size(20, 16);
            this.lblPre.TabIndex = 17;
            this.lblPre.Text = "%";
            this.lblPre.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmDataSync2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.lblPre);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataSync2";
            this.Text = "Data Sync...";
            this.Load += new System.EventHandler(this.frmDataSync_Load);
            this.Shown += new System.EventHandler(this.frmDataSync2_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPre;
    }
}