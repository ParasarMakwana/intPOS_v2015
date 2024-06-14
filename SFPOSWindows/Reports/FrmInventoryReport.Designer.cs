namespace SFPOSWindows.Reports
{
    partial class FrmInventoryReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblSubHeading = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.datePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.datePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.InventoryGridView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InventoryGridView)).BeginInit();
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
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 69);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
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
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::SFPOSWindows.Properties.Resources.Product_Sales_Management1;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblSubHeading
            // 
            this.lblSubHeading.AutoSize = true;
            this.lblSubHeading.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSubHeading.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.lblSubHeading.Location = new System.Drawing.Point(71, 29);
            this.lblSubHeading.Name = "lblSubHeading";
            this.lblSubHeading.Size = new System.Drawing.Size(168, 25);
            this.lblSubHeading.TabIndex = 0;
            this.lblSubHeading.Text = "Inventory Report";
            this.lblSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSearch.Controls.Add(this.datePickerEndDate);
            this.pnlSearch.Controls.Add(this.datePickerStartDate);
            this.pnlSearch.Controls.Add(this.lblToDate);
            this.pnlSearch.Controls.Add(this.lblFromDate);
            this.pnlSearch.Controls.Add(this.btnShowAll);
            this.pnlSearch.Location = new System.Drawing.Point(13, 87);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(859, 47);
            this.pnlSearch.TabIndex = 10;
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.CalendarTitleBackColor = System.Drawing.Color.Green;
            this.datePickerEndDate.CalendarTitleForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.datePickerEndDate.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.datePickerEndDate.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerEndDate.Location = new System.Drawing.Point(374, 9);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(178, 29);
            this.datePickerEndDate.TabIndex = 9;
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.CalendarTitleBackColor = System.Drawing.Color.Green;
            this.datePickerStartDate.CalendarTitleForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.datePickerStartDate.CalendarTrailingForeColor = System.Drawing.SystemColors.Desktop;
            this.datePickerStartDate.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerStartDate.Location = new System.Drawing.Point(95, 8);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(179, 29);
            this.datePickerStartDate.TabIndex = 8;
            // 
            // lblToDate
            // 
            this.lblToDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.lblToDate.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.ForeColor = System.Drawing.Color.White;
            this.lblToDate.Location = new System.Drawing.Point(280, 8);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(88, 31);
            this.lblToDate.TabIndex = 7;
            this.lblToDate.Text = "To Date";
            this.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFromDate
            // 
            this.lblFromDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.lblFromDate.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.ForeColor = System.Drawing.Color.White;
            this.lblFromDate.Location = new System.Drawing.Point(1, 8);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(88, 31);
            this.lblFromDate.TabIndex = 5;
            this.lblFromDate.Text = "From Date";
            this.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShowAll
            // 
            this.btnShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(101)))), ((int)(((byte)(63)))));
            this.btnShowAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowAll.ForeColor = System.Drawing.Color.White;
            this.btnShowAll.Location = new System.Drawing.Point(558, 8);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(88, 31);
            this.btnShowAll.TabIndex = 4;
            this.btnShowAll.Text = "SHOW ALL";
            this.btnShowAll.UseVisualStyleBackColor = false;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // InventoryGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.InventoryGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.InventoryGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InventoryGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.InventoryGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.InventoryGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.InventoryGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InventoryGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.InventoryGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.InventoryGridView.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InventoryGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.InventoryGridView.Location = new System.Drawing.Point(12, 142);
            this.InventoryGridView.Margin = new System.Windows.Forms.Padding(5);
            this.InventoryGridView.Name = "InventoryGridView";
            this.InventoryGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InventoryGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.InventoryGridView.RowHeadersVisible = false;
            this.InventoryGridView.RowHeadersWidth = 10;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(3);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.InventoryGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.InventoryGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.InventoryGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.InventoryGridView.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(3);
            this.InventoryGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            this.InventoryGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.DarkGreen;
            this.InventoryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InventoryGridView.Size = new System.Drawing.Size(860, 555);
            this.InventoryGridView.TabIndex = 11;
            // 
            // FrmInventoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 733);
            this.Controls.Add(this.InventoryGridView);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.panel1);
            this.Name = "FrmInventoryReport";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Inventory Report";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InventoryGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSubHeading;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Button btnShowAll;
        public System.Windows.Forms.DataGridView InventoryGridView;
        private System.Windows.Forms.DateTimePicker datePickerStartDate;
        private System.Windows.Forms.DateTimePicker datePickerEndDate;
    }
}