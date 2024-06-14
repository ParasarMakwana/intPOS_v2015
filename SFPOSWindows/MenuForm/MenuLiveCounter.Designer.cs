namespace SFPOSWindows.MenuForm
{
    partial class MenuLiveCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuLiveCounter));
            this.btnRefresh = new MetroFramework.Controls.MetroButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnLTSystem = new MetroFramework.Controls.MetroButton();
            this.PanelGrid = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnRefresh.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRefresh.Highlight = true;
            this.btnRefresh.Location = new System.Drawing.Point(763, 25);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 29);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseCustomBackColor = true;
            this.btnRefresh.UseSelectable = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnLTSystem
            // 
            this.btnLTSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLTSystem.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLTSystem.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnLTSystem.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnLTSystem.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLTSystem.Highlight = true;
            this.btnLTSystem.Location = new System.Drawing.Point(648, 25);
            this.btnLTSystem.Name = "btnLTSystem";
            this.btnLTSystem.Size = new System.Drawing.Size(109, 29);
            this.btnLTSystem.TabIndex = 29;
            this.btnLTSystem.Text = "LT SYSTEM";
            this.btnLTSystem.UseCustomBackColor = true;
            this.btnLTSystem.UseSelectable = true;
            this.btnLTSystem.Click += new System.EventHandler(this.btnLTSystem_Click);
            // 
            // PanelGrid
            // 
            this.PanelGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelGrid.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PanelGrid.Location = new System.Drawing.Point(1, 1);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Size = new System.Drawing.Size(883, 731);
            this.PanelGrid.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 23;
            this.label1.Text = "Live Counter";
            // 
            // MenuLiveCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.btnLTSystem);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PanelGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuLiveCounter";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.MenuLiveCounter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnRefresh;
        private System.Windows.Forms.Timer timer1;
        private MetroFramework.Controls.MetroButton btnLTSystem;
        private System.Windows.Forms.Panel PanelGrid;
        public System.Windows.Forms.Label label1;
    }
}