namespace SFPOSWindows.Frontend
{
    partial class frmPriceCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPriceCheck));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblUPCCode = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPriceVal = new System.Windows.Forms.Label();
            this.lblCaseQtyVal = new System.Windows.Forms.Label();
            this.lblCasePriceVal = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblUPC = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblCaseQty = new System.Windows.Forms.Label();
            this.lblCasePrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGrpPrice = new System.Windows.Forms.Label();
            this.lblGrpQty = new System.Windows.Forms.Label();
            this.lblGrpPriceVal = new System.Windows.Forms.Label();
            this.lblGrpQtyVal = new System.Windows.Forms.Label();
            this.txtUPCCode = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(505, 75);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 34);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "CANCEL";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblUPCCode
            // 
            this.lblUPCCode.AutoSize = true;
            this.lblUPCCode.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUPCCode.Location = new System.Drawing.Point(25, 129);
            this.lblUPCCode.Name = "lblUPCCode";
            this.lblUPCCode.Size = new System.Drawing.Size(140, 23);
            this.lblUPCCode.TabIndex = 43;
            this.lblUPCCode.Text = "UPC Code: ";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(25, 164);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(179, 23);
            this.lblDescription.TabIndex = 44;
            this.lblDescription.Text = "Description: ";
            // 
            // lblPriceVal
            // 
            this.lblPriceVal.AutoSize = true;
            this.lblPriceVal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceVal.Location = new System.Drawing.Point(25, 203);
            this.lblPriceVal.Name = "lblPriceVal";
            this.lblPriceVal.Size = new System.Drawing.Size(127, 23);
            this.lblPriceVal.TabIndex = 45;
            this.lblPriceVal.Text = "PRICE: $ ";
            // 
            // lblCaseQtyVal
            // 
            this.lblCaseQtyVal.AutoSize = true;
            this.lblCaseQtyVal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaseQtyVal.Location = new System.Drawing.Point(25, 239);
            this.lblCaseQtyVal.Name = "lblCaseQtyVal";
            this.lblCaseQtyVal.Size = new System.Drawing.Size(192, 23);
            this.lblCaseQtyVal.TabIndex = 46;
            this.lblCaseQtyVal.Text = "Case Quantity:";
            // 
            // lblCasePriceVal
            // 
            this.lblCasePriceVal.AutoSize = true;
            this.lblCasePriceVal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasePriceVal.Location = new System.Drawing.Point(25, 275);
            this.lblCasePriceVal.Name = "lblCasePriceVal";
            this.lblCasePriceVal.Size = new System.Drawing.Size(153, 23);
            this.lblCasePriceVal.TabIndex = 47;
            this.lblCasePriceVal.Text = "Case Price:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice.Location = new System.Drawing.Point(136, 202);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(127, 23);
            this.lblPrice.TabIndex = 48;
            this.lblPrice.Text = "<<PRICE>>";
            this.lblPrice.Visible = false;
            // 
            // lblUPC
            // 
            this.lblUPC.AutoSize = true;
            this.lblUPC.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUPC.Location = new System.Drawing.Point(155, 129);
            this.lblUPC.Name = "lblUPC";
            this.lblUPC.Size = new System.Drawing.Size(101, 23);
            this.lblUPC.TabIndex = 49;
            this.lblUPC.Text = "<<UPC>>";
            this.lblUPC.Visible = false;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(187, 164);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(205, 23);
            this.lblDesc.TabIndex = 50;
            this.lblDesc.Text = "<<Description>>";
            this.lblDesc.Visible = false;
            // 
            // lblCaseQty
            // 
            this.lblCaseQty.AutoSize = true;
            this.lblCaseQty.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaseQty.Location = new System.Drawing.Point(208, 239);
            this.lblCaseQty.Name = "lblCaseQty";
            this.lblCaseQty.Size = new System.Drawing.Size(166, 23);
            this.lblCaseQty.TabIndex = 51;
            this.lblCaseQty.Text = "<<Case Qty>>";
            this.lblCaseQty.Visible = false;
            // 
            // lblCasePrice
            // 
            this.lblCasePrice.AutoSize = true;
            this.lblCasePrice.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasePrice.Location = new System.Drawing.Point(169, 275);
            this.lblCasePrice.Name = "lblCasePrice";
            this.lblCasePrice.Size = new System.Drawing.Size(179, 23);
            this.lblCasePrice.TabIndex = 52;
            this.lblCasePrice.Text = "<<CasePrice>>";
            this.lblCasePrice.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 27);
            this.label2.TabIndex = 53;
            this.label2.Text = "Price Check";
            // 
            // lblGrpPrice
            // 
            this.lblGrpPrice.AutoSize = true;
            this.lblGrpPrice.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpPrice.Location = new System.Drawing.Point(182, 350);
            this.lblGrpPrice.Name = "lblGrpPrice";
            this.lblGrpPrice.Size = new System.Drawing.Size(192, 23);
            this.lblGrpPrice.TabIndex = 57;
            this.lblGrpPrice.Text = "<<GroupPrice>>";
            this.lblGrpPrice.Visible = false;
            // 
            // lblGrpQty
            // 
            this.lblGrpQty.AutoSize = true;
            this.lblGrpQty.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpQty.Location = new System.Drawing.Point(219, 312);
            this.lblGrpQty.Name = "lblGrpQty";
            this.lblGrpQty.Size = new System.Drawing.Size(244, 23);
            this.lblGrpQty.TabIndex = 56;
            this.lblGrpQty.Text = "<<Group Quantity>>";
            this.lblGrpQty.Visible = false;
            // 
            // lblGrpPriceVal
            // 
            this.lblGrpPriceVal.AutoSize = true;
            this.lblGrpPriceVal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpPriceVal.Location = new System.Drawing.Point(25, 350);
            this.lblGrpPriceVal.Name = "lblGrpPriceVal";
            this.lblGrpPriceVal.Size = new System.Drawing.Size(166, 23);
            this.lblGrpPriceVal.TabIndex = 55;
            this.lblGrpPriceVal.Text = "Group Price:";
            // 
            // lblGrpQtyVal
            // 
            this.lblGrpQtyVal.AutoSize = true;
            this.lblGrpQtyVal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrpQtyVal.Location = new System.Drawing.Point(25, 312);
            this.lblGrpQtyVal.Name = "lblGrpQtyVal";
            this.lblGrpQtyVal.Size = new System.Drawing.Size(205, 23);
            this.lblGrpQtyVal.TabIndex = 54;
            this.lblGrpQtyVal.Text = "Group Quantity:";
            // 
            // txtUPCCode
            // 
            // 
            // 
            // 
            this.txtUPCCode.CustomButton.Image = null;
            this.txtUPCCode.CustomButton.Location = new System.Drawing.Point(427, 2);
            this.txtUPCCode.CustomButton.Name = "";
            this.txtUPCCode.CustomButton.Size = new System.Drawing.Size(29, 29);
            this.txtUPCCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUPCCode.CustomButton.TabIndex = 1;
            this.txtUPCCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUPCCode.CustomButton.UseSelectable = true;
            this.txtUPCCode.CustomButton.Visible = false;
            this.txtUPCCode.DisplayIcon = true;
            this.txtUPCCode.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtUPCCode.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtUPCCode.Lines = new string[0];
            this.txtUPCCode.Location = new System.Drawing.Point(29, 75);
            this.txtUPCCode.MaxLength = 15;
            this.txtUPCCode.Name = "txtUPCCode";
            this.txtUPCCode.PasswordChar = '\0';
            this.txtUPCCode.PromptText = "UPC Code";
            this.txtUPCCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUPCCode.SelectedText = "";
            this.txtUPCCode.SelectionLength = 0;
            this.txtUPCCode.SelectionStart = 0;
            this.txtUPCCode.ShortcutsEnabled = true;
            this.txtUPCCode.Size = new System.Drawing.Size(459, 34);
            this.txtUPCCode.TabIndex = 25;
            this.txtUPCCode.UseSelectable = true;
            this.txtUPCCode.WaterMark = "UPC Code";
            this.txtUPCCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUPCCode.WaterMarkFont = new System.Drawing.Font("Courier New", 16.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPCCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPCCode_KeyPress);
            // 
            // frmPriceCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 410);
            this.Controls.Add(this.lblGrpPrice);
            this.Controls.Add(this.lblGrpQty);
            this.Controls.Add(this.lblGrpPriceVal);
            this.Controls.Add(this.lblGrpQtyVal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCasePrice);
            this.Controls.Add(this.lblCaseQty);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblUPC);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblCasePriceVal);
            this.Controls.Add(this.lblCaseQtyVal);
            this.Controls.Add(this.lblPriceVal);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblUPCCode);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtUPCCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPriceCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProductDetail_FormClosing);
            this.Load += new System.EventHandler(this.frmPriceCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblUPCCode;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPriceVal;
        private System.Windows.Forms.Label lblCaseQtyVal;
        private System.Windows.Forms.Label lblCasePriceVal;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblUPC;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblCaseQty;
        private System.Windows.Forms.Label lblCasePrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGrpPrice;
        private System.Windows.Forms.Label lblGrpQty;
        private System.Windows.Forms.Label lblGrpPriceVal;
        private System.Windows.Forms.Label lblGrpQtyVal;
        public MetroFramework.Controls.MetroTextBox txtUPCCode;
    }
}