namespace SFPOSWindows.Frontend
{
    partial class FrmSupervisor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSupervisor));
            this.label2 = new System.Windows.Forms.Label();
            this.btnExitWindow = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnTillStatus = new System.Windows.Forms.Button();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 43;
            this.label2.Text = "Supervisor";
            // 
            // btnExitWindow
            // 
            this.btnExitWindow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnExitWindow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnExitWindow.FlatAppearance.BorderSize = 2;
            this.btnExitWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExitWindow.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitWindow.ForeColor = System.Drawing.Color.Black;
            this.btnExitWindow.Location = new System.Drawing.Point(258, 186);
            this.btnExitWindow.Name = "btnExitWindow";
            this.btnExitWindow.Size = new System.Drawing.Size(169, 34);
            this.btnExitWindow.TabIndex = 44;
            this.btnExitWindow.Text = "0. EXIT WINDOW";
            this.btnExitWindow.UseVisualStyleBackColor = false;
            this.btnExitWindow.Click += new System.EventHandler(this.btnExitWindow_Click);
            // 
            // btnResume
            // 
            this.btnResume.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnResume.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnResume.FlatAppearance.BorderSize = 2;
            this.btnResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResume.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResume.ForeColor = System.Drawing.Color.Black;
            this.btnResume.Location = new System.Drawing.Point(46, 186);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(169, 34);
            this.btnResume.TabIndex = 45;
            this.btnResume.Text = "3.  RESUME";
            this.btnResume.UseVisualStyleBackColor = false;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnTillStatus
            // 
            this.btnTillStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTillStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnTillStatus.FlatAppearance.BorderSize = 2;
            this.btnTillStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTillStatus.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTillStatus.ForeColor = System.Drawing.Color.Black;
            this.btnTillStatus.Location = new System.Drawing.Point(46, 113);
            this.btnTillStatus.Name = "btnTillStatus";
            this.btnTillStatus.Size = new System.Drawing.Size(169, 34);
            this.btnTillStatus.TabIndex = 46;
            this.btnTillStatus.Text = "1. TILL STATUS";
            this.btnTillStatus.UseVisualStyleBackColor = false;
            this.btnTillStatus.Click += new System.EventHandler(this.btnTillStatus_Click);
            // 
            // btnSuspend
            // 
            this.btnSuspend.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSuspend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnSuspend.FlatAppearance.BorderSize = 2;
            this.btnSuspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuspend.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuspend.ForeColor = System.Drawing.Color.Black;
            this.btnSuspend.Location = new System.Drawing.Point(258, 113);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(169, 34);
            this.btnSuspend.TabIndex = 47;
            this.btnSuspend.Text = "2.  SUSPEND";
            this.btnSuspend.UseVisualStyleBackColor = false;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // FrmSupervisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 299);
            this.Controls.Add(this.btnSuspend);
            this.Controls.Add(this.btnTillStatus);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnExitWindow);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSupervisor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExitWindow;
        private System.Windows.Forms.Button btnTillStatus;
        private System.Windows.Forms.Button btnSuspend;
        public System.Windows.Forms.Button btnResume;
    }
}