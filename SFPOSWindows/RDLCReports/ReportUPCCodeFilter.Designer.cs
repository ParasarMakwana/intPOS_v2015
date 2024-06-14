namespace SFPOSWindows.RDLCReports
{
    partial class ReportUPCCodeFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportUPCCodeFilter));
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.txtUPC = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Highlight = true;
            this.btnOK.Location = new System.Drawing.Point(270, 262);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtUPC
            // 
            this.txtUPC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtUPC.CustomButton.Image = null;
            this.txtUPC.CustomButton.Location = new System.Drawing.Point(304, 1);
            this.txtUPC.CustomButton.Name = "";
            this.txtUPC.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.txtUPC.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPC.CustomButton.TabIndex = 1;
            this.txtUPC.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPC.CustomButton.UseSelectable = true;
            this.txtUPC.CustomButton.Visible = false;
            this.txtUPC.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUPC.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtUPC.Lines = new string[0];
            this.txtUPC.Location = new System.Drawing.Point(23, 136);
            this.txtUPC.MaxLength = 20;
            this.txtUPC.Name = "txtUPC";
            this.txtUPC.PasswordChar = '\0';
            this.txtUPC.PromptText = "UPCCODE";
            this.txtUPC.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUPC.SelectedText = "";
            this.txtUPC.SelectionLength = 0;
            this.txtUPC.SelectionStart = 0;
            this.txtUPC.ShortcutsEnabled = true;
            this.txtUPC.Size = new System.Drawing.Size(332, 29);
            this.txtUPC.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPC.TabIndex = 5;
            this.txtUPC.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPC.UseSelectable = true;
            this.txtUPC.WaterMark = "UPCCODE";
            this.txtUPC.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUPC.WaterMarkFont = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPC_KeyPress);
            // 
            // ReportUPCCodeFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 314);
            this.Controls.Add(this.txtUPC);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportUPCCodeFilter";
            this.Text = "UPCCode";
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton btnOK;
        private MetroFramework.Controls.MetroTextBox txtUPC;
    }
}