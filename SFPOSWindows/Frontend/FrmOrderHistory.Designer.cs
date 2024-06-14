namespace SFPOSWindows.Frontend
{
    partial class FrmOrderHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderHistory));
            this.txtSearchProductName = new MetroFramework.Controls.MetroTextBox();
            this.GridViewProductDetails = new MetroFramework.Controls.MetroGrid();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProductDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearchProductName
            // 
            // 
            // 
            // 
            this.txtSearchProductName.CustomButton.Image = null;
            this.txtSearchProductName.CustomButton.Location = new System.Drawing.Point(654, 2);
            this.txtSearchProductName.CustomButton.Name = "";
            this.txtSearchProductName.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtSearchProductName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchProductName.CustomButton.TabIndex = 1;
            this.txtSearchProductName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchProductName.CustomButton.UseSelectable = true;
            this.txtSearchProductName.CustomButton.Visible = false;
            this.txtSearchProductName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtSearchProductName.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtSearchProductName.Lines = new string[0];
            this.txtSearchProductName.Location = new System.Drawing.Point(12, 63);
            this.txtSearchProductName.MaxLength = 32767;
            this.txtSearchProductName.Name = "txtSearchProductName";
            this.txtSearchProductName.PasswordChar = '\0';
            this.txtSearchProductName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchProductName.SelectedText = "";
            this.txtSearchProductName.SelectionLength = 0;
            this.txtSearchProductName.SelectionStart = 0;
            this.txtSearchProductName.ShortcutsEnabled = true;
            this.txtSearchProductName.Size = new System.Drawing.Size(686, 34);
            this.txtSearchProductName.TabIndex = 29;
            this.txtSearchProductName.UseSelectable = true;
            this.txtSearchProductName.WaterMark = "UPC Code/ProductName/Price";
            this.txtSearchProductName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchProductName.WaterMarkFont = new System.Drawing.Font("Courier New", 15.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchProductName.TextChanged += new System.EventHandler(this.txtSearchProductName_TextChanged);
            // 
            // GridViewProductDetails
            // 
            this.GridViewProductDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.GridViewProductDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GridViewProductDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridViewProductDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridViewProductDetails.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GridViewProductDetails.BackgroundColor = System.Drawing.Color.White;
            this.GridViewProductDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridViewProductDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridViewProductDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewProductDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GridViewProductDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridViewProductDetails.DefaultCellStyle = dataGridViewCellStyle3;
            this.GridViewProductDetails.EnableHeadersVisualStyles = false;
            this.GridViewProductDetails.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GridViewProductDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.GridViewProductDetails.Location = new System.Drawing.Point(12, 103);
            this.GridViewProductDetails.Name = "GridViewProductDetails";
            this.GridViewProductDetails.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridViewProductDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GridViewProductDetails.RowHeadersVisible = false;
            this.GridViewProductDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridViewProductDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridViewProductDetails.Size = new System.Drawing.Size(686, 381);
            this.GridViewProductDetails.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 27);
            this.label2.TabIndex = 40;
            this.label2.Text = "Order History";
            // 
            // FrmOrderHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 495);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchProductName);
            this.Controls.Add(this.GridViewProductDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmOrderHistory";
            ((System.ComponentModel.ISupportInitialize)(this.GridViewProductDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox txtSearchProductName;
        public MetroFramework.Controls.MetroGrid GridViewProductDetails;
        private System.Windows.Forms.Label label2;
    }
}