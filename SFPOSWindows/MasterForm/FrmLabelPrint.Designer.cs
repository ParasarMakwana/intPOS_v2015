namespace SFPOSWindows.MasterForm
{
    partial class FrmLabelPrint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLabelPrint));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrint = new MetroFramework.Controls.MetroButton();
            this.rBtnPoNumber = new MetroFramework.Controls.MetroRadioButton();
            this.btnBarcode = new MetroFramework.Controls.MetroButton();
            this.rBtnUpcCode = new MetroFramework.Controls.MetroRadioButton();
            this.txtUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.pnlUPC = new System.Windows.Forms.Panel();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.PoNumberGrdView = new MetroFramework.Controls.MetroGrid();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.pnlUPC.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoNumberGrdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.rBtnPoNumber);
            this.panel3.Controls.Add(this.btnBarcode);
            this.panel3.Controls.Add(this.rBtnUpcCode);
            this.panel3.Controls.Add(this.txtUPCCode);
            this.panel3.Location = new System.Drawing.Point(1, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(871, 48);
            this.panel3.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnPrint.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnPrint.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnPrint.Location = new System.Drawing.Point(531, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(124, 35);
            this.btnPrint.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "Print Barcode";
            this.btnPrint.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnPrint.UseCustomForeColor = true;
            this.btnPrint.UseSelectable = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rBtnPoNumber
            // 
            this.rBtnPoNumber.AutoSize = true;
            this.rBtnPoNumber.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rBtnPoNumber.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rBtnPoNumber.Location = new System.Drawing.Point(9, 23);
            this.rBtnPoNumber.Name = "rBtnPoNumber";
            this.rBtnPoNumber.Size = new System.Drawing.Size(98, 19);
            this.rBtnPoNumber.Style = MetroFramework.MetroColorStyle.Blue;
            this.rBtnPoNumber.TabIndex = 15;
            this.rBtnPoNumber.Text = "PO Number";
            this.rBtnPoNumber.Theme = MetroFramework.MetroThemeStyle.Light;
            this.rBtnPoNumber.UseSelectable = true;
            // 
            // btnBarcode
            // 
            this.btnBarcode.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnBarcode.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnBarcode.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnBarcode.Location = new System.Drawing.Point(401, 7);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(124, 35);
            this.btnBarcode.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnBarcode.TabIndex = 18;
            this.btnBarcode.Text = "Generate Barcode";
            this.btnBarcode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnBarcode.UseCustomForeColor = true;
            this.btnBarcode.UseSelectable = true;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // rBtnUpcCode
            // 
            this.rBtnUpcCode.AutoSize = true;
            this.rBtnUpcCode.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rBtnUpcCode.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.rBtnUpcCode.Location = new System.Drawing.Point(9, 4);
            this.rBtnUpcCode.Name = "rBtnUpcCode";
            this.rBtnUpcCode.Size = new System.Drawing.Size(88, 19);
            this.rBtnUpcCode.Style = MetroFramework.MetroColorStyle.Blue;
            this.rBtnUpcCode.TabIndex = 14;
            this.rBtnUpcCode.Text = "UPC Code";
            this.rBtnUpcCode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.rBtnUpcCode.UseSelectable = true;
            this.rBtnUpcCode.CheckedChanged += new System.EventHandler(this.rBtnUpcCode_CheckedChanged);
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
            this.txtUPCCode.Location = new System.Drawing.Point(113, 7);
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
            // 
            // pnlUPC
            // 
            this.pnlUPC.BackColor = System.Drawing.Color.White;
            this.pnlUPC.Controls.Add(this.lblPrice);
            this.pnlUPC.Controls.Add(this.lblName);
            this.pnlUPC.Location = new System.Drawing.Point(271, 445);
            this.pnlUPC.Name = "pnlUPC";
            this.pnlUPC.Size = new System.Drawing.Size(298, 138);
            this.pnlUPC.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(3, 3);
            this.lblPrice.MinimumSize = new System.Drawing.Size(182, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(182, 17);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.MinimumSize = new System.Drawing.Size(182, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(182, 17);
            this.lblName.TabIndex = 5;
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(-2, -7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 55);
            this.panel1.TabIndex = 13;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 13);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(121, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Label Print";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PoNumberGrdView
            // 
            this.PoNumberGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.PoNumberGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PoNumberGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PoNumberGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.PoNumberGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.PoNumberGrdView.BackgroundColor = System.Drawing.Color.White;
            this.PoNumberGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PoNumberGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PoNumberGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PoNumberGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.PoNumberGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PoNumberGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.PoNumberGrdView.EnableHeadersVisualStyles = false;
            this.PoNumberGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PoNumberGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.PoNumberGrdView.Location = new System.Drawing.Point(7, 117);
            this.PoNumberGrdView.Name = "PoNumberGrdView";
            this.PoNumberGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PoNumberGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.PoNumberGrdView.RowHeadersVisible = false;
            this.PoNumberGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.PoNumberGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PoNumberGrdView.Size = new System.Drawing.Size(865, 322);
            this.PoNumberGrdView.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(575, 517);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 134);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PoNumberGrdView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlUPC);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLabelPrint";
            this.Text = "Label Print";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlUPC.ResumeLayout(false);
            this.pnlUPC.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PoNumberGrdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlUPC;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private MetroFramework.Controls.MetroButton btnBarcode;
        private MetroFramework.Controls.MetroTextBox txtUPCCode;
        private MetroFramework.Controls.MetroRadioButton rBtnUpcCode;
        private MetroFramework.Controls.MetroRadioButton rBtnPoNumber;
        public MetroFramework.Controls.MetroGrid PoNumberGrdView;
        private MetroFramework.Controls.MetroButton btnPrint;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}