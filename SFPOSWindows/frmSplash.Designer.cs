namespace SFPOSWindows
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.progressBar = new MetroFramework.Controls.MetroProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar.Location = new System.Drawing.Point(15, 347);
            this.progressBar.MinimumSize = new System.Drawing.Size(500, 23);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(698, 23);
            this.progressBar.TabIndex = 8;
            this.progressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressBar.Value = 100;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.BackgroundImage = global::SFPOSWindows.Properties.Resources.start1;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(635, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 37);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::SFPOSWindows.Properties.Resources.intPOSDemo_B;
            this.pictureBox1.Location = new System.Drawing.Point(1, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(722, 419);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 427);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(725, 427);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(725, 427);
            this.Name = "frmSplash";
            this.Text = "SF-ePOS";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.Shown += new System.EventHandler(this.frmSplash_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroProgressBar progressBar;
        private System.Windows.Forms.Timer timer1;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}

