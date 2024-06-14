namespace SFPOSWindows.MasterForm
{
    partial class FrmProductUoM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductUoM));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddEdit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtQtyPerUoM = new System.Windows.Forms.TextBox();
            this.lblQtyPerUoM = new System.Windows.Forms.Label();
            this.cmbUoM = new System.Windows.Forms.ComboBox();
            this.cmbProductName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiscription = new System.Windows.Forms.TextBox();
            this.lblDiscription = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.ProductUoMGrdView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductUoMGrdView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.btnAddEdit);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.txtQtyPerUoM);
            this.groupBox1.Controls.Add(this.lblQtyPerUoM);
            this.groupBox1.Controls.Add(this.cmbUoM);
            this.groupBox1.Controls.Add(this.cmbProductName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDiscription);
            this.groupBox1.Controls.Add(this.lblDiscription);
            this.groupBox1.Controls.Add(this.lblProductName);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(456, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 368);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product UoM Management";
            // 
            // btnAddEdit
            // 
            this.btnAddEdit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnAddEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddEdit.FlatAppearance.BorderSize = 0;
            this.btnAddEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEdit.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEdit.ForeColor = System.Drawing.Color.White;
            this.btnAddEdit.Location = new System.Drawing.Point(120, 244);
            this.btnAddEdit.Name = "btnAddEdit";
            this.btnAddEdit.Size = new System.Drawing.Size(88, 31);
            this.btnAddEdit.TabIndex = 4;
            this.btnAddEdit.Text = "ADD";
            this.btnAddEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddEdit.UseVisualStyleBackColor = false;
            this.btnAddEdit.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(219, 244);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 31);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "CLEAR";
            this.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtQtyPerUoM
            // 
            this.txtQtyPerUoM.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtyPerUoM.Location = new System.Drawing.Point(120, 192);
            this.txtQtyPerUoM.Name = "txtQtyPerUoM";
            this.txtQtyPerUoM.Size = new System.Drawing.Size(187, 25);
            this.txtQtyPerUoM.TabIndex = 3;
            this.txtQtyPerUoM.TextChanged += new System.EventHandler(this.txtQtyPerUoM_TextChanged);
            // 
            // lblQtyPerUoM
            // 
            this.lblQtyPerUoM.AutoSize = true;
            this.lblQtyPerUoM.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyPerUoM.ForeColor = System.Drawing.Color.Black;
            this.lblQtyPerUoM.Location = new System.Drawing.Point(4, 192);
            this.lblQtyPerUoM.Name = "lblQtyPerUoM";
            this.lblQtyPerUoM.Size = new System.Drawing.Size(115, 17);
            this.lblQtyPerUoM.TabIndex = 14;
            this.lblQtyPerUoM.Text = "Quantity Per UoM:";
            // 
            // cmbUoM
            // 
            this.cmbUoM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUoM.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUoM.FormattingEnabled = true;
            this.cmbUoM.Location = new System.Drawing.Point(120, 93);
            this.cmbUoM.Name = "cmbUoM";
            this.cmbUoM.Size = new System.Drawing.Size(187, 25);
            this.cmbUoM.TabIndex = 1;
            this.cmbUoM.TextChanged += new System.EventHandler(this.cmbUoM_TextChanged);
            // 
            // cmbProductName
            // 
            this.cmbProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductName.FormattingEnabled = true;
            this.cmbProductName.Location = new System.Drawing.Point(120, 46);
            this.cmbProductName.Name = "cmbProductName";
            this.cmbProductName.Size = new System.Drawing.Size(187, 25);
            this.cmbProductName.Sorted = true;
            this.cmbProductName.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Unit of Measure:";
            // 
            // txtDiscription
            // 
            this.txtDiscription.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscription.Location = new System.Drawing.Point(120, 140);
            this.txtDiscription.Name = "txtDiscription";
            this.txtDiscription.Size = new System.Drawing.Size(187, 25);
            this.txtDiscription.TabIndex = 2;
            this.txtDiscription.TextChanged += new System.EventHandler(this.txtDiscription_TextChanged);
            // 
            // lblDiscription
            // 
            this.lblDiscription.AutoSize = true;
            this.lblDiscription.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscription.ForeColor = System.Drawing.Color.Black;
            this.lblDiscription.Location = new System.Drawing.Point(4, 140);
            this.lblDiscription.Name = "lblDiscription";
            this.lblDiscription.Size = new System.Drawing.Size(77, 17);
            this.lblDiscription.TabIndex = 9;
            this.lblDiscription.Text = "Description:";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(4, 46);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(95, 17);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "Product Name:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.ProductUoMGrdView);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 461);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblSubHeading);
            this.panel2.Location = new System.Drawing.Point(3, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(768, 69);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.panel3.Location = new System.Drawing.Point(76, 63);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(689, 3);
            this.panel3.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            //this.pictureBox1.BackgroundImage = global::SFPOSWindows.Properties.Resources.Product_Unit_of_Measure;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 69);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.lblSubHeading.Location = new System.Drawing.Point(71, 29);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(162, 25);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Unit of Measure";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProductUoMGrdView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.ProductUoMGrdView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductUoMGrdView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductUoMGrdView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductUoMGrdView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ProductUoMGrdView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProductUoMGrdView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProductUoMGrdView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductUoMGrdView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductUoMGrdView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductUoMGrdView.ColumnHeadersHeight = 35;
            this.ProductUoMGrdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductUoMGrdView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductUoMGrdView.Location = new System.Drawing.Point(3, 87);
            this.ProductUoMGrdView.Margin = new System.Windows.Forms.Padding(5);
            this.ProductUoMGrdView.Name = "ProductUoMGrdView";
            this.ProductUoMGrdView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductUoMGrdView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ProductUoMGrdView.RowHeadersVisible = false;
            this.ProductUoMGrdView.RowHeadersWidth = 10;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.ProductUoMGrdView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            this.ProductUoMGrdView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.ProductUoMGrdView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductUoMGrdView.Size = new System.Drawing.Size(445, 368);
            this.ProductUoMGrdView.TabIndex = 15;
            this.ProductUoMGrdView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductUoMGrdView_CellClick);
            this.ProductUoMGrdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductUoMGrdView_CellContentClick);
            // 
            // FrmProductUoM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 459);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProductUoM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product-UoM ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductUoMGrdView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtQtyPerUoM;
        private System.Windows.Forms.Label lblQtyPerUoM;
        private System.Windows.Forms.ComboBox cmbUoM;
        private System.Windows.Forms.ComboBox cmbProductName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDiscription;
        private System.Windows.Forms.Label lblDiscription;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView ProductUoMGrdView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Button btnAddEdit;
        private System.Windows.Forms.Button btnClear;
    }
}