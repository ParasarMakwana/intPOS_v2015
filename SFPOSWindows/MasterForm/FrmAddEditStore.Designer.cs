namespace SFPOSWindows.MasterForm
{
    partial class FrmAddEditStore
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.txtZipcode = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblZipCode = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblFax = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblStoreAddress2 = new System.Windows.Forms.Label();
            this.lblStoreAddress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.lblStoreName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 30);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cmbCity);
            this.groupBox1.Controls.Add(this.cmbState);
            this.groupBox1.Controls.Add(this.cmbCountry);
            this.groupBox1.Controls.Add(this.txtZipcode);
            this.groupBox1.Controls.Add(this.txtFax);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtAddress2);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.lblZipCode);
            this.groupBox1.Controls.Add(this.lblState);
            this.groupBox1.Controls.Add(this.lblCountry);
            this.groupBox1.Controls.Add(this.lblCity);
            this.groupBox1.Controls.Add(this.lblFax);
            this.groupBox1.Controls.Add(this.lblPhone);
            this.groupBox1.Controls.Add(this.lblStoreAddress2);
            this.groupBox1.Controls.Add(this.lblStoreAddress);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtStoreName);
            this.groupBox1.Controls.Add(this.lblStoreName);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 358);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor";
            // 
            // cmbCity
            // 
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(135, 268);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(213, 21);
            this.cmbCity.TabIndex = 7;
            // 
            // cmbState
            // 
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(135, 242);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(213, 21);
            this.cmbState.TabIndex = 6;
            this.cmbState.SelectedIndexChanged += new System.EventHandler(this.cmbState_SelectedIndexChanged);
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "---select one---"});
            this.cmbCountry.Location = new System.Drawing.Point(135, 217);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(213, 21);
            this.cmbCountry.TabIndex = 5;
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            // 
            // txtZipcode
            // 
            this.txtZipcode.Location = new System.Drawing.Point(135, 295);
            this.txtZipcode.Name = "txtZipcode";
            this.txtZipcode.Size = new System.Drawing.Size(213, 20);
            this.txtZipcode.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(135, 192);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(213, 20);
            this.txtFax.TabIndex = 4;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(135, 166);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(213, 20);
            this.txtPhone.TabIndex = 3;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(135, 112);
            this.txtAddress2.Multiline = true;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(213, 48);
            this.txtAddress2.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(135, 58);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(213, 48);
            this.txtAddress.TabIndex = 1;
            // 
            // lblZipCode
            // 
            this.lblZipCode.AutoSize = true;
            this.lblZipCode.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZipCode.ForeColor = System.Drawing.Color.Black;
            this.lblZipCode.Location = new System.Drawing.Point(6, 295);
            this.lblZipCode.Name = "lblZipCode";
            this.lblZipCode.Size = new System.Drawing.Size(78, 17);
            this.lblZipCode.TabIndex = 15;
            this.lblZipCode.Text = "ZipCode : ";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.Black;
            this.lblState.Location = new System.Drawing.Point(6, 243);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(56, 17);
            this.lblState.TabIndex = 14;
            this.lblState.Text = "State : ";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.ForeColor = System.Drawing.Color.Black;
            this.lblCountry.Location = new System.Drawing.Point(6, 218);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(74, 17);
            this.lblCountry.TabIndex = 13;
            this.lblCountry.Text = "Country : ";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.ForeColor = System.Drawing.Color.Black;
            this.lblCity.Location = new System.Drawing.Point(6, 268);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(47, 17);
            this.lblCity.TabIndex = 12;
            this.lblCity.Text = "City : ";
            // 
            // lblFax
            // 
            this.lblFax.AutoSize = true;
            this.lblFax.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFax.ForeColor = System.Drawing.Color.Black;
            this.lblFax.Location = new System.Drawing.Point(6, 189);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(44, 17);
            this.lblFax.TabIndex = 11;
            this.lblFax.Text = "Fax : ";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.ForeColor = System.Drawing.Color.Black;
            this.lblPhone.Location = new System.Drawing.Point(6, 163);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(92, 17);
            this.lblPhone.TabIndex = 10;
            this.lblPhone.Text = "Phone No. : ";
            // 
            // lblStoreAddress2
            // 
            this.lblStoreAddress2.AutoSize = true;
            this.lblStoreAddress2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreAddress2.ForeColor = System.Drawing.Color.Black;
            this.lblStoreAddress2.Location = new System.Drawing.Point(6, 109);
            this.lblStoreAddress2.Name = "lblStoreAddress2";
            this.lblStoreAddress2.Size = new System.Drawing.Size(131, 17);
            this.lblStoreAddress2.TabIndex = 9;
            this.lblStoreAddress2.Text = "Store Address 2 : ";
            // 
            // lblStoreAddress
            // 
            this.lblStoreAddress.AutoSize = true;
            this.lblStoreAddress.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreAddress.ForeColor = System.Drawing.Color.Black;
            this.lblStoreAddress.Location = new System.Drawing.Point(6, 57);
            this.lblStoreAddress.Name = "lblStoreAddress";
            this.lblStoreAddress.Size = new System.Drawing.Size(118, 17);
            this.lblStoreAddress.TabIndex = 8;
            this.lblStoreAddress.Text = "Store Address : ";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(273, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Location = new System.Drawing.Point(192, 323);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 27);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(192, 323);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 27);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(135, 31);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(213, 20);
            this.txtStoreName.TabIndex = 0;
            // 
            // lblStoreName
            // 
            this.lblStoreName.AutoSize = true;
            this.lblStoreName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreName.ForeColor = System.Drawing.Color.Black;
            this.lblStoreName.Location = new System.Drawing.Point(6, 30);
            this.lblStoreName.Name = "lblStoreName";
            this.lblStoreName.Size = new System.Drawing.Size(102, 17);
            this.lblStoreName.TabIndex = 1;
            this.lblStoreName.Text = "Store Name : ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblMessage);
            this.panel2.Location = new System.Drawing.Point(0, 412);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(391, 27);
            this.panel2.TabIndex = 5;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Green;
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 21);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAddEditStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 439);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmAddEditStore";
            this.Text = "FrmAddEditStore";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.Label lblStoreName;
        private System.Windows.Forms.Label lblStoreAddress;
        private System.Windows.Forms.Label lblStoreAddress2;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblFax;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblZipCode;
        public System.Windows.Forms.TextBox txtZipcode;
        public System.Windows.Forms.TextBox txtFax;
        public System.Windows.Forms.TextBox txtPhone;
        public System.Windows.Forms.TextBox txtAddress2;
        public System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.ComboBox cmbCountry;
        public System.Windows.Forms.ComboBox cmbCity;
        public System.Windows.Forms.ComboBox cmbState;
    }
}