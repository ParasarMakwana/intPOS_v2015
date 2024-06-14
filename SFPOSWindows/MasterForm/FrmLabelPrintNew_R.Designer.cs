namespace SFPOSWindows.MasterForm
{
    partial class FrmLabelPrintNew_R
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLabelPrintNew_R));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.btnBarcode = new MetroFramework.Controls.MetroButton();
            this.txtUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnBarcode);
            this.panel3.Controls.Add(this.txtUPCCode);
            this.panel3.Location = new System.Drawing.Point(1, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(733, 48);
            this.panel3.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnPrint.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPrint.Location = new System.Drawing.Point(294, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(124, 35);
            this.btnPrint.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "Print Label";
            this.btnPrint.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnPrint.UseCustomForeColor = true;
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnBarcode
            // 
            this.btnBarcode.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnBarcode.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnBarcode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnBarcode.Location = new System.Drawing.Point(606, 7);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(124, 35);
            this.btnBarcode.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnBarcode.TabIndex = 18;
            this.btnBarcode.Text = "Generate Barcode";
            this.btnBarcode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnBarcode.UseCustomForeColor = true;
            this.btnBarcode.UseSelectable = true;
            this.btnBarcode.Visible = false;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // txtUPCCode
            // 
            // 
            // 
            // 
            this.txtUPCCode.CustomButton.Image = null;
            this.txtUPCCode.CustomButton.Location = new System.Drawing.Point(248, 1);
            this.txtUPCCode.CustomButton.Name = "";
            this.txtUPCCode.CustomButton.Size = new System.Drawing.Size(33, 33);
            this.txtUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.CustomButton.TabIndex = 1;
            this.txtUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.CustomButton.UseSelectable = true;
            this.txtUPCCode.CustomButton.Visible = false;
            this.txtUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUPCCode.IconRight = true;
            this.txtUPCCode.Lines = new string[0];
            this.txtUPCCode.Location = new System.Drawing.Point(6, 7);
            this.txtUPCCode.MaxLength = 20;
            this.txtUPCCode.Name = "txtUPCCode";
            this.txtUPCCode.PasswordChar = '\0';
            this.txtUPCCode.PromptText = "UPC Code";
            this.txtUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUPCCode.SelectedText = "";
            this.txtUPCCode.SelectionLength = 0;
            this.txtUPCCode.SelectionStart = 0;
            this.txtUPCCode.ShortcutsEnabled = true;
            this.txtUPCCode.Size = new System.Drawing.Size(282, 35);
            this.txtUPCCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.TabIndex = 17;
            this.txtUPCCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.UseSelectable = true;
            this.txtUPCCode.WaterMark = "UPC Code";
            this.txtUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUPCCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPCCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPCCode_KeyPress);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.reportViewer1.Location = new System.Drawing.Point(1, 117);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(730, 295);
            this.reportViewer1.TabIndex = 14;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
            // 
            // FrmLabelPrintNew_R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 419);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLabelPrintNew_R";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rapid Label Print";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLabelPrintNew_R_FormClosed);
            this.Load += new System.EventHandler(this.FrmLabelPrintNew_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroButton btnBarcode;
        private MetroFramework.Controls.MetroTextBox txtUPCCode;
        private MetroFramework.Controls.MetroButton btnPrint;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}