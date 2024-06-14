namespace SFPOSWindows.MasterForm
{
    partial class FrmStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStore));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.btnADD = new MetroFramework.Controls.MetroButton();
            this.txtSearchStoreName = new MetroFramework.Controls.MetroTextBox();
            this.StoreGrdView = new MetroFramework.Controls.MetroGrid();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StoreGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(792, 44);
            this.panel1.TabIndex = 13;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(67, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Store";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnExport);
            this.pnlSearch.Controls.Add(this.btnImport);
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Controls.Add(this.btnADD);
            this.pnlSearch.Controls.Add(this.txtSearchStoreName);
            this.pnlSearch.Location = new System.Drawing.Point(-2, 51);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(804, 47);
            this.pnlSearch.TabIndex = 14;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(600, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 27);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseCustomForeColor = true;
            this.btnExport.UseSelectable = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnImport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnImport.Location = new System.Drawing.Point(500, 9);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 27);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 19;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseCustomForeColor = true;
            this.btnImport.UseSelectable = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAll.Location = new System.Drawing.Point(307, 9);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(94, 27);
            this.btnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnShowAll.TabIndex = 16;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnShowAll.UseCustomForeColor = true;
            this.btnShowAll.UseSelectable = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnADD
            // 
            this.btnADD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnADD.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnADD.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnADD.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnADD.Location = new System.Drawing.Point(700, 9);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(94, 27);
            this.btnADD.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnADD.TabIndex = 15;
            this.btnADD.Text = "ADD";
            this.btnADD.UseCustomForeColor = true;
            this.btnADD.UseSelectable = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // txtSearchStoreName
            // 
            // 
            // 
            // 
            this.txtSearchStoreName.CustomButton.Image = null;
            this.txtSearchStoreName.CustomButton.Location = new System.Drawing.Point(258, 2);
            this.txtSearchStoreName.CustomButton.Name = "";
            this.txtSearchStoreName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchStoreName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchStoreName.CustomButton.TabIndex = 1;
            this.txtSearchStoreName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchStoreName.CustomButton.UseSelectable = true;
            this.txtSearchStoreName.CustomButton.Visible = false;
            this.txtSearchStoreName.DisplayIcon = true;
            this.txtSearchStoreName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchStoreName.IconRight = true;
            this.txtSearchStoreName.Lines = new string[0];
            this.txtSearchStoreName.Location = new System.Drawing.Point(19, 10);
            this.txtSearchStoreName.MaxLength = 20;
            this.txtSearchStoreName.Name = "txtSearchStoreName";
            this.txtSearchStoreName.PasswordChar = '\0';
            //this.txtSearchStoreName.PromptText = "Name/Address";
            this.txtSearchStoreName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchStoreName.SelectedText = "";
            this.txtSearchStoreName.SelectionLength = 0;
            this.txtSearchStoreName.SelectionStart = 0;
            this.txtSearchStoreName.ShortcutsEnabled = true;
            this.txtSearchStoreName.Size = new System.Drawing.Size(282, 26);
            this.txtSearchStoreName.TabIndex = 14;
            this.txtSearchStoreName.UseSelectable = true;
            this.txtSearchStoreName.WaterMark = "Name/Address";
            this.txtSearchStoreName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchStoreName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchStoreName.TextChanged += new System.EventHandler(this.txtSearchStoreName_TextChanged);
            // 
            // StoreGrdView
            // 
            this.StoreGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.StoreGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.StoreGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StoreGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.StoreGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.StoreGrdView.BackgroundColor = System.Drawing.Color.White;
            this.StoreGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StoreGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.StoreGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StoreGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.StoreGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.StoreGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.StoreGrdView.EnableHeadersVisualStyles = false;
            this.StoreGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.StoreGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.StoreGrdView.Location = new System.Drawing.Point(17, 104);
            this.StoreGrdView.Name = "StoreGrdView";
            this.StoreGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.StoreGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.StoreGrdView.RowHeadersVisible = false;
            this.StoreGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.StoreGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.StoreGrdView.Size = new System.Drawing.Size(775, 259);
            this.StoreGrdView.TabIndex = 14;
            this.StoreGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StoreGrdView_CellClick);
            this.StoreGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StoreGrdView_CellContentClick);
            this.StoreGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.StoreGrdView_CellDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 375);
            this.Controls.Add(this.StoreGrdView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmStore";
            this.Text = "Store Management";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StoreGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroButton btnADD;
        private MetroFramework.Controls.MetroTextBox txtSearchStoreName;
        public MetroFramework.Controls.MetroGrid StoreGrdView;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}