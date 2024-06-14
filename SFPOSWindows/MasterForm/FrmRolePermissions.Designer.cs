namespace SFPOSWindows.MasterForm
{
    partial class FrmRolePermissions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRolePermissions));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBoxListBackend = new System.Windows.Forms.CheckedListBox();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.txtSearchRoleType = new MetroFramework.Controls.MetroTextBox();
            this.RoleGrdView = new MetroFramework.Controls.MetroGrid();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkBoxListFrontend = new System.Windows.Forms.CheckedListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkBoxListBackend);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 450);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Back-end Permissions";
            // 
            // chkBoxListBackend
            // 
            this.chkBoxListBackend.CheckOnClick = true;
            this.chkBoxListBackend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxListBackend.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBoxListBackend.Enabled = false;
            this.chkBoxListBackend.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxListBackend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxListBackend.FormattingEnabled = true;
            this.chkBoxListBackend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkBoxListBackend.Location = new System.Drawing.Point(3, 21);
            this.chkBoxListBackend.Name = "chkBoxListBackend";
            this.chkBoxListBackend.Size = new System.Drawing.Size(223, 422);
            this.chkBoxListBackend.TabIndex = 15;
            this.chkBoxListBackend.ThreeDCheckBoxes = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClear.Location = new System.Drawing.Point(253, 459);
            this.btnClear.MaximumSize = new System.Drawing.Size(94, 27);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 27);
            this.btnClear.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "CLEAR";
            this.btnClear.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnClear.UseCustomForeColor = true;
            this.btnClear.UseSelectable = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSave.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSave.Location = new System.Drawing.Point(153, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 27);
            this.btnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "SAVE";
            this.btnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSave.UseCustomForeColor = true;
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 44);
            this.panel1.TabIndex = 17;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(171, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Role Permission";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnShowAll);
            this.panel2.Controls.Add(this.txtSearchRoleType);
            this.panel2.Location = new System.Drawing.Point(5, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 47);
            this.panel2.TabIndex = 18;
            // 
            // btnShowAll
            // 
            this.btnShowAll.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnShowAll.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnShowAll.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnShowAll.Location = new System.Drawing.Point(300, 8);
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
            // txtSearchRoleType
            // 
            // 
            // 
            // 
            this.txtSearchRoleType.CustomButton.Image = null;
            this.txtSearchRoleType.CustomButton.Location = new System.Drawing.Point(258, 2);
            this.txtSearchRoleType.CustomButton.Name = "";
            this.txtSearchRoleType.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchRoleType.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchRoleType.CustomButton.TabIndex = 1;
            this.txtSearchRoleType.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchRoleType.CustomButton.UseSelectable = true;
            this.txtSearchRoleType.CustomButton.Visible = false;
            this.txtSearchRoleType.DisplayIcon = true;
            this.txtSearchRoleType.Icon = global::SFPOSWindows.Properties.Resources.magnifying_glass;
            this.txtSearchRoleType.IconRight = true;
            this.txtSearchRoleType.Lines = new string[0];
            this.txtSearchRoleType.Location = new System.Drawing.Point(12, 9);
            this.txtSearchRoleType.MaxLength = 20;
            this.txtSearchRoleType.Name = "txtSearchRoleType";
            this.txtSearchRoleType.PasswordChar = '\0';
            this.txtSearchRoleType.PromptText = "Role";
            this.txtSearchRoleType.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchRoleType.SelectedText = "";
            this.txtSearchRoleType.SelectionLength = 0;
            this.txtSearchRoleType.SelectionStart = 0;
            this.txtSearchRoleType.ShortcutsEnabled = true;
            this.txtSearchRoleType.Size = new System.Drawing.Size(282, 26);
            this.txtSearchRoleType.TabIndex = 14;
            this.txtSearchRoleType.UseSelectable = true;
            this.txtSearchRoleType.WaterMark = "Role";
            this.txtSearchRoleType.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchRoleType.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtSearchRoleType.TextChanged += new System.EventHandler(this.txtSearchRoleType_TextChanged);
            // 
            // RoleGrdView
            // 
            this.RoleGrdView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.RoleGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RoleGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoleGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RoleGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.RoleGrdView.BackgroundColor = System.Drawing.Color.White;
            this.RoleGrdView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RoleGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.RoleGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RoleGrdView.ColumnHeadersHeight = 30;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.RoleGrdView.EnableHeadersVisualStyles = false;
            this.RoleGrdView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.RoleGrdView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.RoleGrdView.Location = new System.Drawing.Point(17, 104);
            this.RoleGrdView.Name = "RoleGrdView";
            this.RoleGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.RoleGrdView.RowHeadersVisible = false;
            this.RoleGrdView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RoleGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RoleGrdView.Size = new System.Drawing.Size(312, 500);
            this.RoleGrdView.TabIndex = 20;
            this.RoleGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoleGrdView_CellClick);
            this.RoleGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoleGrdView_CellContentClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkBoxListFrontend);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox2.Location = new System.Drawing.Point(242, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 450);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Front-end Permissions";
            // 
            // chkBoxListFrontend
            // 
            this.chkBoxListFrontend.CheckOnClick = true;
            this.chkBoxListFrontend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxListFrontend.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBoxListFrontend.Enabled = false;
            this.chkBoxListFrontend.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxListFrontend.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxListFrontend.FormattingEnabled = true;
            this.chkBoxListFrontend.Location = new System.Drawing.Point(3, 21);
            this.chkBoxListFrontend.Name = "chkBoxListFrontend";
            this.chkBoxListFrontend.Size = new System.Drawing.Size(223, 422);
            this.chkBoxListFrontend.TabIndex = 15;
            this.chkBoxListFrontend.ThreeDCheckBoxes = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Location = new System.Drawing.Point(342, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(469, 503);
            this.panel3.TabIndex = 21;
            // 
            // FrmRolePermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(818, 616);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RoleGrdView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRolePermissions";
            this.Text = "Role Permission";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox chkBoxListBackend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroTextBox txtSearchRoleType;
        private MetroFramework.Controls.MetroButton btnSave;
        private MetroFramework.Controls.MetroButton btnClear;
        public MetroFramework.Controls.MetroGrid RoleGrdView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox chkBoxListFrontend;
        private System.Windows.Forms.Panel panel3;
    }
}