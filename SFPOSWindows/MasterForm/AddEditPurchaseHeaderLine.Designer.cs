namespace SFPOSWindows.MasterForm
{
    partial class AddEditPurchaseHeaderLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditPurchaseHeaderLine));
            this.txtTax = new System.Windows.Forms.TextBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.txtTaxAmt = new System.Windows.Forms.TextBox();
            this.lbltaxAmt = new System.Windows.Forms.Label();
            this.txtUnitCost = new System.Windows.Forms.TextBox();
            this.lblUnitCost = new System.Windows.Forms.Label();
            this.txtInclTax = new System.Windows.Forms.TextBox();
            this.lblAmtInclTax = new System.Windows.Forms.Label();
            this.txtExclTax = new System.Windows.Forms.TextBox();
            this.lblAmtExclTax = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTaxGroupCode = new System.Windows.Forms.Label();
            this.cmbTaxGroup = new System.Windows.Forms.ComboBox();
            this.ltbPercentage = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblUpcItem = new System.Windows.Forms.Label();
            this.txtUpcItem = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rBtnItem = new System.Windows.Forms.RadioButton();
            this.rbtnUpcCode = new System.Windows.Forms.RadioButton();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbProdName = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTax
            // 
            this.txtTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTax.Location = new System.Drawing.Point(159, 225);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(213, 25);
            this.txtTax.TabIndex = 7;
            this.txtTax.TextChanged += new System.EventHandler(this.txtTax_TextChanged);
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.ForeColor = System.Drawing.Color.Black;
            this.lblTax.Location = new System.Drawing.Point(26, 225);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(35, 17);
            this.lblTax.TabIndex = 4;
            this.lblTax.Text = "Tax: ";
            // 
            // txtTaxAmt
            // 
            this.txtTaxAmt.Enabled = false;
            this.txtTaxAmt.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAmt.Location = new System.Drawing.Point(159, 256);
            this.txtTaxAmt.Name = "txtTaxAmt";
            this.txtTaxAmt.Size = new System.Drawing.Size(213, 25);
            this.txtTaxAmt.TabIndex = 8;
            // 
            // lbltaxAmt
            // 
            this.lbltaxAmt.AutoSize = true;
            this.lbltaxAmt.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltaxAmt.ForeColor = System.Drawing.Color.Black;
            this.lbltaxAmt.Location = new System.Drawing.Point(26, 256);
            this.lbltaxAmt.Name = "lbltaxAmt";
            this.lbltaxAmt.Size = new System.Drawing.Size(84, 17);
            this.lbltaxAmt.TabIndex = 6;
            this.lbltaxAmt.Text = "Tax Amount: ";
            // 
            // txtUnitCost
            // 
            this.txtUnitCost.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnitCost.Location = new System.Drawing.Point(159, 162);
            this.txtUnitCost.Name = "txtUnitCost";
            this.txtUnitCost.Size = new System.Drawing.Size(213, 25);
            this.txtUnitCost.TabIndex = 5;
            this.txtUnitCost.TextChanged += new System.EventHandler(this.txtUnitCost_TextChanged);
            // 
            // lblUnitCost
            // 
            this.lblUnitCost.AutoSize = true;
            this.lblUnitCost.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitCost.ForeColor = System.Drawing.Color.Black;
            this.lblUnitCost.Location = new System.Drawing.Point(26, 162);
            this.lblUnitCost.Name = "lblUnitCost";
            this.lblUnitCost.Size = new System.Drawing.Size(68, 17);
            this.lblUnitCost.TabIndex = 8;
            this.lblUnitCost.Text = "Unit Cost: ";
            // 
            // txtInclTax
            // 
            this.txtInclTax.Enabled = false;
            this.txtInclTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInclTax.Location = new System.Drawing.Point(159, 288);
            this.txtInclTax.Name = "txtInclTax";
            this.txtInclTax.Size = new System.Drawing.Size(213, 25);
            this.txtInclTax.TabIndex = 9;
            // 
            // lblAmtInclTax
            // 
            this.lblAmtInclTax.AutoSize = true;
            this.lblAmtInclTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtInclTax.ForeColor = System.Drawing.Color.Black;
            this.lblAmtInclTax.Location = new System.Drawing.Point(26, 288);
            this.lblAmtInclTax.Name = "lblAmtInclTax";
            this.lblAmtInclTax.Size = new System.Drawing.Size(112, 17);
            this.lblAmtInclTax.TabIndex = 10;
            this.lblAmtInclTax.Text = "Line Amt Incl Tax: ";
            // 
            // txtExclTax
            // 
            this.txtExclTax.Enabled = false;
            this.txtExclTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExclTax.Location = new System.Drawing.Point(159, 319);
            this.txtExclTax.Name = "txtExclTax";
            this.txtExclTax.Size = new System.Drawing.Size(213, 25);
            this.txtExclTax.TabIndex = 7;
            // 
            // lblAmtExclTax
            // 
            this.lblAmtExclTax.AutoSize = true;
            this.lblAmtExclTax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtExclTax.ForeColor = System.Drawing.Color.Black;
            this.lblAmtExclTax.Location = new System.Drawing.Point(26, 319);
            this.lblAmtExclTax.Name = "lblAmtExclTax";
            this.lblAmtExclTax.Size = new System.Drawing.Size(111, 17);
            this.lblAmtExclTax.TabIndex = 12;
            this.lblAmtExclTax.Text = "Line Amt Excl Tax:";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(26, 100);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(95, 17);
            this.lblProductName.TabIndex = 14;
            this.lblProductName.Text = "Product Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTaxGroupCode);
            this.groupBox1.Controls.Add(this.cmbTaxGroup);
            this.groupBox1.Controls.Add(this.ltbPercentage);
            this.groupBox1.Controls.Add(this.txtProductName);
            this.groupBox1.Controls.Add(this.lblUpcItem);
            this.groupBox1.Controls.Add(this.txtUpcItem);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cmbProdName);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.lblQty);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.txtTax);
            this.groupBox1.Controls.Add(this.lblProductName);
            this.groupBox1.Controls.Add(this.lblTax);
            this.groupBox1.Controls.Add(this.txtExclTax);
            this.groupBox1.Controls.Add(this.lbltaxAmt);
            this.groupBox1.Controls.Add(this.lblAmtExclTax);
            this.groupBox1.Controls.Add(this.txtTaxAmt);
            this.groupBox1.Controls.Add(this.txtInclTax);
            this.groupBox1.Controls.Add(this.lblUnitCost);
            this.groupBox1.Controls.Add(this.lblAmtInclTax);
            this.groupBox1.Controls.Add(this.txtUnitCost);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(410, 403);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Information";
            // 
            // lblTaxGroupCode
            // 
            this.lblTaxGroupCode.AutoSize = true;
            this.lblTaxGroupCode.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxGroupCode.ForeColor = System.Drawing.Color.Black;
            this.lblTaxGroupCode.Location = new System.Drawing.Point(26, 194);
            this.lblTaxGroupCode.Name = "lblTaxGroupCode";
            this.lblTaxGroupCode.Size = new System.Drawing.Size(107, 17);
            this.lblTaxGroupCode.TabIndex = 28;
            this.lblTaxGroupCode.Text = "Tax Group Code:";
            // 
            // cmbTaxGroup
            // 
            this.cmbTaxGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTaxGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTaxGroup.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTaxGroup.FormattingEnabled = true;
            this.cmbTaxGroup.ItemHeight = 17;
            this.cmbTaxGroup.Location = new System.Drawing.Point(159, 194);
            this.cmbTaxGroup.Name = "cmbTaxGroup";
            this.cmbTaxGroup.Size = new System.Drawing.Size(213, 25);
            this.cmbTaxGroup.TabIndex = 6;
            this.cmbTaxGroup.TextChanged += new System.EventHandler(this.cmbTaxGroup_TextChanged);
            // 
            // ltbPercentage
            // 
            this.ltbPercentage.AutoSize = true;
            this.ltbPercentage.BackColor = System.Drawing.Color.White;
            this.ltbPercentage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ltbPercentage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ltbPercentage.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltbPercentage.ForeColor = System.Drawing.Color.Silver;
            this.ltbPercentage.Location = new System.Drawing.Point(348, 229);
            this.ltbPercentage.Name = "ltbPercentage";
            this.ltbPercentage.Size = new System.Drawing.Size(19, 17);
            this.ltbPercentage.TabIndex = 26;
            this.ltbPercentage.Text = "%";
            this.ltbPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProductName
            // 
            this.txtProductName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductName.Location = new System.Drawing.Point(159, 100);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(213, 25);
            this.txtProductName.TabIndex = 3;
            // 
            // lblUpcItem
            // 
            this.lblUpcItem.AutoSize = true;
            this.lblUpcItem.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpcItem.ForeColor = System.Drawing.Color.Black;
            this.lblUpcItem.Location = new System.Drawing.Point(26, 69);
            this.lblUpcItem.Name = "lblUpcItem";
            this.lblUpcItem.Size = new System.Drawing.Size(74, 17);
            this.lblUpcItem.TabIndex = 25;
            this.lblUpcItem.Text = "UPC Code: ";
            // 
            // txtUpcItem
            // 
            this.txtUpcItem.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpcItem.Location = new System.Drawing.Point(159, 69);
            this.txtUpcItem.Name = "txtUpcItem";
            this.txtUpcItem.Size = new System.Drawing.Size(213, 25);
            this.txtUpcItem.TabIndex = 2;
            this.txtUpcItem.TextChanged += new System.EventHandler(this.txtUpcItem_TextChanged);
            this.txtUpcItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUpcItem_KeyDown);
            this.txtUpcItem.Leave += new System.EventHandler(this.txtUpcItem_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rBtnItem);
            this.groupBox2.Controls.Add(this.rbtnUpcCode);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(102, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 45);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // rBtnItem
            // 
            this.rBtnItem.AutoSize = true;
            this.rBtnItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rBtnItem.Location = new System.Drawing.Point(109, 16);
            this.rBtnItem.Name = "rBtnItem";
            this.rBtnItem.Size = new System.Drawing.Size(86, 21);
            this.rBtnItem.TabIndex = 1;
            this.rBtnItem.Text = "Item Code";
            this.rBtnItem.UseVisualStyleBackColor = true;
            this.rBtnItem.CheckedChanged += new System.EventHandler(this.rBtnItem_CheckedChanged);
            // 
            // rbtnUpcCode
            // 
            this.rbtnUpcCode.AutoSize = true;
            this.rbtnUpcCode.Checked = true;
            this.rbtnUpcCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnUpcCode.Location = new System.Drawing.Point(7, 16);
            this.rbtnUpcCode.Name = "rbtnUpcCode";
            this.rbtnUpcCode.Size = new System.Drawing.Size(85, 21);
            this.rbtnUpcCode.TabIndex = 1;
            this.rbtnUpcCode.TabStop = true;
            this.rbtnUpcCode.Text = "UPC Code";
            this.rbtnUpcCode.UseVisualStyleBackColor = true;
            this.rbtnUpcCode.CheckedChanged += new System.EventHandler(this.rbtnUpcCode_CheckedChanged);
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
            this.btnClear.Location = new System.Drawing.Point(159, 364);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 31);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbProdName
            // 
            this.cmbProdName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProdName.FormattingEnabled = true;
            this.cmbProdName.Location = new System.Drawing.Point(159, 100);
            this.cmbProdName.Name = "cmbProdName";
            this.cmbProdName.Size = new System.Drawing.Size(213, 25);
            this.cmbProdName.TabIndex = 1;
            this.cmbProdName.Visible = false;
            this.cmbProdName.TextChanged += new System.EventHandler(this.cmbProdName_TextChanged);
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
            this.btnAdd.Location = new System.Drawing.Point(284, 364);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 31);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.ForeColor = System.Drawing.Color.Black;
            this.lblQty.Location = new System.Drawing.Point(26, 131);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(63, 17);
            this.lblQty.TabIndex = 16;
            this.lblQty.Text = "Quantity: ";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(159, 131);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(213, 25);
            this.txtQty.TabIndex = 4;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // AddEditPurchaseHeaderLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 413);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditPurchaseHeaderLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Purchase Header Line";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label lblTax;
        public System.Windows.Forms.TextBox txtTaxAmt;
        private System.Windows.Forms.Label lbltaxAmt;
        public System.Windows.Forms.TextBox txtUnitCost;
        private System.Windows.Forms.Label lblUnitCost;
        public System.Windows.Forms.TextBox txtInclTax;
        private System.Windows.Forms.Label lblAmtInclTax;
        public System.Windows.Forms.TextBox txtExclTax;
        private System.Windows.Forms.Label lblAmtExclTax;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblQty;
        public System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbProdName;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnUpcCode;
        private System.Windows.Forms.RadioButton rBtnItem;
        private System.Windows.Forms.Label lblUpcItem;
        public System.Windows.Forms.TextBox txtUpcItem;
        public System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label ltbPercentage;
        private System.Windows.Forms.Label lblTaxGroupCode;
        private System.Windows.Forms.ComboBox cmbTaxGroup;
    }
}