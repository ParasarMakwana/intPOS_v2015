namespace SFPOSWindows.MasterForm
{
    partial class FrmProduct_
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProduct_));
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearchProductName = new MetroFramework.Controls.MetroTextBox();
            this.metroBtnShowAll = new MetroFramework.Controls.MetroButton();
            this.backgroundWorkerLoadData = new System.ComponentModel.BackgroundWorker();
            this.PanelSubMenu = new System.Windows.Forms.Panel();
            this.picLoader = new System.Windows.Forms.PictureBox();
            this.ProductGrdView = new MetroFramework.Controls.MetroGrid();
            this.pnlSearch.SuspendLayout();
            this.PanelSubMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.txtSearchProductName);
            this.pnlSearch.Controls.Add(this.metroBtnShowAll);
            this.pnlSearch.Location = new System.Drawing.Point(5, 55);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(770, 44);
            this.pnlSearch.TabIndex = 24;
            // 
            // txtSearchProductName
            // 
            // 
            // 
            // 
            this.txtSearchProductName.CustomButton.Image = null;
            this.txtSearchProductName.CustomButton.Location = new System.Drawing.Point(257, 2);
            this.txtSearchProductName.CustomButton.Name = "";
            this.txtSearchProductName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchProductName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchProductName.CustomButton.TabIndex = 1;
            this.txtSearchProductName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchProductName.CustomButton.UseSelectable = true;
            this.txtSearchProductName.CustomButton.Visible = false;
            this.txtSearchProductName.DisplayIcon = true;
            this.txtSearchProductName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchProductName.IconRight = true;
            this.txtSearchProductName.Lines = new string[0];
            this.txtSearchProductName.Location = new System.Drawing.Point(18, 8);
            this.txtSearchProductName.MaxLength = 20;
            this.txtSearchProductName.Name = "txtSearchProductName";
            this.txtSearchProductName.PasswordChar = '\0';
            this.txtSearchProductName.PromptText = "Name/UPC Code";
            this.txtSearchProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchProductName.SelectedText = "";
            this.txtSearchProductName.SelectionLength = 0;
            this.txtSearchProductName.SelectionStart = 0;
            this.txtSearchProductName.ShortcutsEnabled = true;
            this.txtSearchProductName.Size = new System.Drawing.Size(281, 26);
            this.txtSearchProductName.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchProductName.TabIndex = 8;
            this.txtSearchProductName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchProductName.UseSelectable = true;
            this.txtSearchProductName.WaterMark = "Name/UPC Code";
            this.txtSearchProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchProductName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchProductName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchProductName_KeyPress);
            // 
            // metroBtnShowAll
            // 
            this.metroBtnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroBtnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.metroBtnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroBtnShowAll.Location = new System.Drawing.Point(305, 8);
            this.metroBtnShowAll.Name = "metroBtnShowAll";
            this.metroBtnShowAll.Size = new System.Drawing.Size(95, 26);
            this.metroBtnShowAll.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroBtnShowAll.TabIndex = 7;
            this.metroBtnShowAll.Text = "SHOW ALL";
            this.metroBtnShowAll.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroBtnShowAll.UseCustomForeColor = true;
            this.metroBtnShowAll.UseSelectable = true;
            this.metroBtnShowAll.Click += new System.EventHandler(this.metroBtnShowAll_Click);
            // 
            // PanelSubMenu
            // 
            this.PanelSubMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelSubMenu.BackColor = System.Drawing.Color.White;
            this.PanelSubMenu.Controls.Add(this.picLoader);
            this.PanelSubMenu.Controls.Add(this.ProductGrdView);
            this.PanelSubMenu.Location = new System.Drawing.Point(5, 100);
            this.PanelSubMenu.Name = "PanelSubMenu";
            this.PanelSubMenu.Size = new System.Drawing.Size(770, 408);
            this.PanelSubMenu.TabIndex = 22;
            // 
            // picLoader
            // 
            this.picLoader.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picLoader.BackColor = System.Drawing.Color.White;
            this.picLoader.BackgroundImage = global::SFPOSWindows.Properties.Resources.Loader;
            this.picLoader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLoader.Location = new System.Drawing.Point(305, 128);
            this.picLoader.Name = "picLoader";
            this.picLoader.Size = new System.Drawing.Size(135, 135);
            this.picLoader.TabIndex = 2;
            this.picLoader.TabStop = false;
            this.picLoader.Visible = false;
            // 
            // ProductGrdView
            // 
            this.ProductGrdView.AllowUserToAddRows = false;
            this.ProductGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.ProductGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProductGrdView.BackgroundColor = System.Drawing.Color.White;
            this.ProductGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductGrdView.EnableHeadersVisualStyles = false;
            this.ProductGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ProductGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ProductGrdView.Location = new System.Drawing.Point(7, 5);
            this.ProductGrdView.Name = "ProductGrdView";
            this.ProductGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProductGrdView.RowHeadersVisible = false;
            this.ProductGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ProductGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductGrdView.Size = new System.Drawing.Size(756, 395);
            this.ProductGrdView.TabIndex = 17;
            this.ProductGrdView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ProductGrdView_Scroll);
            // 
            // FrmProduct_
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 512);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.PanelSubMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProduct_";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProduct__FormClosing);
            this.Load += new System.EventHandler(this.FrmProduct_Load);
            this.pnlSearch.ResumeLayout(false);
            this.PanelSubMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlSearch;
        private MetroFramework.Controls.MetroTextBox txtSearchProductName;
        private MetroFramework.Controls.MetroButton metroBtnShowAll;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoadData;
        private System.Windows.Forms.Panel PanelSubMenu;
        private System.Windows.Forms.PictureBox picLoader;
        public MetroFramework.Controls.MetroGrid ProductGrdView;
    }
}