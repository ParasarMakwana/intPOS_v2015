namespace SFPOSWindows.MasterForm
{
    partial class FrmRolePermission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRolePermission));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.txtSearchRoleType = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFromAll = new System.Windows.Forms.Button();
            this.btnFromOne = new System.Windows.Forms.Button();
            this.btnToAll = new System.Windows.Forms.Button();
            this.btnToOne = new System.Windows.Forms.Button();
            this.lstTo = new System.Windows.Forms.ListBox();
            this.lstFrom = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.RoleGrdView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblSubHeading);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 69);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.DarkGreen;
            this.panel2.Location = new System.Drawing.Point(76, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 3);
            this.panel2.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.Location = new System.Drawing.Point(71, 24);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(161, 25);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Role Permission";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Controls.Add(this.txtSearchRoleType);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Location = new System.Drawing.Point(12, 87);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(859, 47);
            this.pnlSearch.TabIndex = 11;
            // 
            // btnShowAll
            // 
            this.btnShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnShowAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.ForeColor = System.Drawing.Color.White;
            this.btnShowAll.Location = new System.Drawing.Point(330, 7);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(88, 31);
            this.btnShowAll.TabIndex = 4;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.UseVisualStyleBackColor = false;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // txtSearchRoleType
            // 
            this.txtSearchRoleType.Font = new System.Drawing.Font("Segoe UI Symbol", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchRoleType.ForeColor = System.Drawing.Color.Silver;
            this.txtSearchRoleType.Location = new System.Drawing.Point(95, 8);
            this.txtSearchRoleType.Name = "txtSearchRoleType";
            this.txtSearchRoleType.Size = new System.Drawing.Size(231, 30);
            this.txtSearchRoleType.TabIndex = 2;
            this.txtSearchRoleType.Text = "Role Type";
            this.txtSearchRoleType.TextChanged += new System.EventHandler(this.txtSearchRoleType_TextChanged);
            this.txtSearchRoleType.Enter += new System.EventHandler(this.txtSearchRoleType_Enter);
            this.txtSearchRoleType.Leave += new System.EventHandler(this.txtSearchRoleType_Leave);
            // 
            // lblSearch
            // 
            this.lblSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(3, 7);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(88, 31);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search";
            this.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnFromAll);
            this.groupBox1.Controls.Add(this.btnFromOne);
            this.groupBox1.Controls.Add(this.btnToAll);
            this.groupBox1.Controls.Add(this.btnToOne);
            this.groupBox1.Controls.Add(this.lstTo);
            this.groupBox1.Controls.Add(this.lstFrom);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(342, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 233);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Role Permission";
            // 
            // btnFromAll
            // 
            this.btnFromAll.Location = new System.Drawing.Point(225, 146);
            this.btnFromAll.Name = "btnFromAll";
            this.btnFromAll.Size = new System.Drawing.Size(88, 31);
            this.btnFromAll.TabIndex = 16;
            this.btnFromAll.Text = "<<";
            this.btnFromAll.UseVisualStyleBackColor = true;
            this.btnFromAll.Click += new System.EventHandler(this.btnFromAll_Click);
            // 
            // btnFromOne
            // 
            this.btnFromOne.Location = new System.Drawing.Point(225, 109);
            this.btnFromOne.Name = "btnFromOne";
            this.btnFromOne.Size = new System.Drawing.Size(88, 31);
            this.btnFromOne.TabIndex = 15;
            this.btnFromOne.Text = "<";
            this.btnFromOne.UseVisualStyleBackColor = true;
            this.btnFromOne.Click += new System.EventHandler(this.btnFromOne_Click);
            // 
            // btnToAll
            // 
            this.btnToAll.Location = new System.Drawing.Point(225, 72);
            this.btnToAll.Name = "btnToAll";
            this.btnToAll.Size = new System.Drawing.Size(88, 31);
            this.btnToAll.TabIndex = 14;
            this.btnToAll.Text = ">>";
            this.btnToAll.UseVisualStyleBackColor = true;
            this.btnToAll.Click += new System.EventHandler(this.btnToAll_Click);
            // 
            // btnToOne
            // 
            this.btnToOne.Location = new System.Drawing.Point(225, 35);
            this.btnToOne.Name = "btnToOne";
            this.btnToOne.Size = new System.Drawing.Size(88, 31);
            this.btnToOne.TabIndex = 13;
            this.btnToOne.Text = ">";
            this.btnToOne.UseVisualStyleBackColor = true;
            this.btnToOne.Click += new System.EventHandler(this.btnToOne_Click);
            // 
            // lstTo
            // 
            this.lstTo.FormattingEnabled = true;
            this.lstTo.ItemHeight = 17;
            this.lstTo.Location = new System.Drawing.Point(365, 26);
            this.lstTo.Name = "lstTo";
            this.lstTo.Size = new System.Drawing.Size(153, 191);
            this.lstTo.TabIndex = 12;
            // 
            // lstFrom
            // 
            this.lstFrom.FormattingEnabled = true;
            this.lstFrom.ItemHeight = 17;
            this.lstFrom.Location = new System.Drawing.Point(15, 26);
            this.lstFrom.Name = "lstFrom";
            this.lstFrom.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstFrom.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstFrom.Size = new System.Drawing.Size(153, 191);
            this.lstFrom.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(271, 186);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 31);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "SAVE";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(174, 186);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 31);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "komel";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // RoleGrdView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.RoleGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.RoleGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoleGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RoleGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.RoleGrdView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RoleGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.RoleGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.RoleGrdView.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RoleGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.RoleGrdView.Location = new System.Drawing.Point(14, 142);
            this.RoleGrdView.Margin = new System.Windows.Forms.Padding(5);
            this.RoleGrdView.Name = "RoleGrdView";
            this.RoleGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RoleGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.RoleGrdView.RowHeadersVisible = false;
            this.RoleGrdView.RowHeadersWidth = 10;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.RoleGrdView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.RoleGrdView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RoleGrdView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoleGrdView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.RoleGrdView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.RoleGrdView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            this.RoleGrdView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.RoleGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RoleGrdView.Size = new System.Drawing.Size(320, 589);
            this.RoleGrdView.TabIndex = 12;
            this.RoleGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoleGrdView_CellClick);
            this.RoleGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RoleGrdView_CellContentClick);
            // 
            // FrmRolePermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RoleGrdView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "FrmRolePermission";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Role Permission";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RoleGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.TextBox txtSearchRoleType;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView RoleGrdView;
        private System.Windows.Forms.ListBox lstTo;
        private System.Windows.Forms.ListBox lstFrom;
        private System.Windows.Forms.Button btnFromAll;
        private System.Windows.Forms.Button btnFromOne;
        private System.Windows.Forms.Button btnToAll;
        private System.Windows.Forms.Button btnToOne;
    }
}