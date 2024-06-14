namespace SFPOSWindows.MasterForm
{
    partial class frmFunctionalPermission
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnShowAll = new MetroFramework.Controls.MetroButton();
            this.txtSearchRoleType = new MetroFramework.Controls.MetroTextBox();
            this.RoleGrdView = new MetroFramework.Controls.MetroGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.chkBoxList = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(5, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 44);
            this.panel1.TabIndex = 18;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSubHeading.Location = new System.Drawing.Point(6, 6);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(231, 32);
            this.lblSubHeading.TabIndex = 1;
            this.lblSubHeading.Text = "Functional Permission";
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
            this.panel2.TabIndex = 19;
            this.panel2.Visible = false;
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
            this.RoleGrdView.Size = new System.Drawing.Size(424, 400);
            this.RoleGrdView.TabIndex = 21;
            this.RoleGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoleGrdView_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.chkBoxList);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBox1.Location = new System.Drawing.Point(452, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 409);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Information";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnClear.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClear.Location = new System.Drawing.Point(246, 373);
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
            this.btnSave.Location = new System.Drawing.Point(146, 373);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 27);
            this.btnSave.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "SAVE";
            this.btnSave.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnSave.UseCustomForeColor = true;
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkBoxList
            // 
            this.chkBoxList.CheckOnClick = true;
            this.chkBoxList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBoxList.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkBoxList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkBoxList.FormattingEnabled = true;
            this.chkBoxList.Location = new System.Drawing.Point(3, 21);
            this.chkBoxList.Name = "chkBoxList";
            this.chkBoxList.ScrollAlwaysVisible = true;
            this.chkBoxList.Size = new System.Drawing.Size(353, 312);
            this.chkBoxList.TabIndex = 15;
            this.chkBoxList.ThreeDCheckBoxes = true;
            // 
            // frmFunctionalPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(818, 516);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RoleGrdView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFunctionalPermission";
            this.Text = "Functional Permission";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton btnShowAll;
        private MetroFramework.Controls.MetroTextBox txtSearchRoleType;
        public MetroFramework.Controls.MetroGrid RoleGrdView;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton btnClear;
        private MetroFramework.Controls.MetroButton btnSave;
        private System.Windows.Forms.CheckedListBox chkBoxList;
    }
}