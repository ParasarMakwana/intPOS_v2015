namespace SFPOSWindows.CustomControl
{
    partial class UCCheckCustomerStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 27);
            this.label2.TabIndex = 45;
            this.label2.Text = "Verify Customer";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(228, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnOK.FlatAppearance.BorderSize = 2;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(147, 183);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "Phone Number";
            // 
            // txtCustomerPhone
            // 
            // 
            // 
            // 
            this.txtCustomerPhone.CustomButton.Image = null;
            this.txtCustomerPhone.CustomButton.Location = new System.Drawing.Point(251, 1);
            this.txtCustomerPhone.CustomButton.Name = "";
            this.txtCustomerPhone.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtCustomerPhone.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCustomerPhone.CustomButton.TabIndex = 1;
            this.txtCustomerPhone.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCustomerPhone.CustomButton.UseSelectable = true;
            this.txtCustomerPhone.CustomButton.Visible = false;
            this.txtCustomerPhone.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCustomerPhone.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtCustomerPhone.Icon = global::SFPOSWindows.Properties.Resources.dollar;
            this.txtCustomerPhone.Lines = new string[0];
            this.txtCustomerPhone.Location = new System.Drawing.Point(26, 124);
            this.txtCustomerPhone.MaxLength = 20;
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.PasswordChar = '\0';
            this.txtCustomerPhone.PromptText = "Phone Number";
            this.txtCustomerPhone.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCustomerPhone.SelectedText = "";
            this.txtCustomerPhone.SelectionLength = 0;
            this.txtCustomerPhone.SelectionStart = 0;
            this.txtCustomerPhone.ShortcutsEnabled = true;
            this.txtCustomerPhone.Size = new System.Drawing.Size(277, 27);
            this.txtCustomerPhone.TabIndex = 41;
            this.txtCustomerPhone.UseSelectable = true;
            this.txtCustomerPhone.WaterMark = "Phone Number";
            this.txtCustomerPhone.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtCustomerPhone.WaterMarkFont = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerPhone_KeyPress);
            // 
            // UCCheckCustomerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCustomerPhone);
            this.Name = "UCCheckCustomerStatus";
            this.Size = new System.Drawing.Size(331, 267);
            this.Load += new System.EventHandler(this.UCCheckCustomerStatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        public MetroFramework.Controls.MetroTextBox txtCustomerPhone;
    }
}
