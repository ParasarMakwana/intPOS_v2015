namespace SFPOSWindows.MasterForm
{
    partial class FrmUnitofMeasure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUnitofMeasure));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExport = new MetroFramework.Controls.MetroButton();
            this.btnImport = new MetroFramework.Controls.MetroButton();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.btnADD = new MetroFramework.Controls.MetroButton();
            this.txtSearchUoMName = new MetroFramework.Controls.MetroTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.UoMGrdView = new MetroFramework.Controls.MetroGrid();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UoMGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 44);
            this.panel1.TabIndex = 13;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(175, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Unit of Measure";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.btnShowAll);
            this.panel2.Controls.Add(this.btnADD);
            this.panel2.Controls.Add(this.txtSearchUoMName);
            this.panel2.Location = new System.Drawing.Point(5, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(767, 47);
            this.panel2.TabIndex = 14;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExport.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnExport.Location = new System.Drawing.Point(558, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 27);
            this.btnExport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnExport.TabIndex = 24;
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
            this.btnImport.Location = new System.Drawing.Point(458, 9);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(94, 27);
            this.btnImport.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnImport.TabIndex = 23;
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
            this.btnShowAll.Location = new System.Drawing.Point(300, 10);
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
            this.btnADD.Location = new System.Drawing.Point(658, 9);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(94, 27);
            this.btnADD.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnADD.TabIndex = 15;
            this.btnADD.Text = "ADD";
            this.btnADD.UseCustomForeColor = true;
            this.btnADD.UseSelectable = true;
            this.btnADD.Click += new System.EventHandler(this.btnADD_Click);
            // 
            // txtSearchUoMName
            // 
            // 
            // 
            // 
            this.txtSearchUoMName.CustomButton.Image = null;
            this.txtSearchUoMName.CustomButton.Location = new System.Drawing.Point(258, 2);
            this.txtSearchUoMName.CustomButton.Name = "";
            this.txtSearchUoMName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchUoMName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchUoMName.CustomButton.TabIndex = 1;
            this.txtSearchUoMName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchUoMName.CustomButton.UseSelectable = true;
            this.txtSearchUoMName.CustomButton.Visible = false;
            this.txtSearchUoMName.DisplayIcon = true;
            this.txtSearchUoMName.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass1;
            this.txtSearchUoMName.IconRight = true;
            this.txtSearchUoMName.Lines = new string[0];
            this.txtSearchUoMName.Location = new System.Drawing.Point(12, 10);
            this.txtSearchUoMName.MaxLength = 20;
            this.txtSearchUoMName.Name = "txtSearchUoMName";
            this.txtSearchUoMName.PasswordChar = '\0';
            this.txtSearchUoMName.PromptText = "Unit of Measure";
            this.txtSearchUoMName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchUoMName.SelectedText = "";
            this.txtSearchUoMName.SelectionLength = 0;
            this.txtSearchUoMName.SelectionStart = 0;
            this.txtSearchUoMName.ShortcutsEnabled = true;
            this.txtSearchUoMName.Size = new System.Drawing.Size(282, 26);
            this.txtSearchUoMName.TabIndex = 14;
            this.txtSearchUoMName.UseSelectable = true;
            this.txtSearchUoMName.WaterMark = "Unit of Measure";
            this.txtSearchUoMName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchUoMName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchUoMName.TextChanged += new System.EventHandler(this.txtSearchUoMName_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.UoMGrdView);
            this.panel3.Location = new System.Drawing.Point(5, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(767, 276);
            this.panel3.TabIndex = 16;
            // 
            // UoMGrdView
            // 
            this.UoMGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.UoMGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.UoMGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UoMGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.UoMGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.UoMGrdView.BackgroundColor = System.Drawing.Color.White;
            this.UoMGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UoMGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.UoMGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UoMGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.UoMGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UoMGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.UoMGrdView.EnableHeadersVisualStyles = false;
            this.UoMGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.UoMGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.UoMGrdView.Location = new System.Drawing.Point(12, 4);
            this.UoMGrdView.Name = "UoMGrdView";
            this.UoMGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UoMGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.UoMGrdView.RowHeadersVisible = false;
            this.UoMGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.UoMGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.UoMGrdView.Size = new System.Drawing.Size(740, 269);
            this.UoMGrdView.TabIndex = 15;
            this.UoMGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UoMGrdView_CellClick);
            this.UoMGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UoMGrdView_CellContentClick);
            this.UoMGrdView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UoMGrdView_CellDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // FrmUnitofMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(779, 388);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUnitofMeasure";
            this.Text = "UoM";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UoMGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroButton btnADD;
        private MetroFramework.Controls.MetroTextBox txtSearchUoMName;
        private System.Windows.Forms.Panel panel3;
        public MetroFramework.Controls.MetroGrid UoMGrdView;
        private MetroFramework.Controls.MetroButton btnExport;
        private MetroFramework.Controls.MetroButton btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}