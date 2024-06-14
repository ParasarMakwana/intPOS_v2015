namespace SFPOSWindows.CustomControl
{
    partial class UCCheckCoupon
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
            this.txtCouponCode = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 27);
            this.label2.TabIndex = 45;
            this.label2.Text = "Apply Coupon Code";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(228, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Visible = false;
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
            this.btnOK.Location = new System.Drawing.Point(147, 172);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 43;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "Scan Coupon";
            // 
            // txtCouponCode
            // 
            // 
            // 
            // 
            this.txtCouponCode.CustomButton.Image = null;
            this.txtCouponCode.CustomButton.Location = new System.Drawing.Point(251, 1);
            this.txtCouponCode.CustomButton.Name = "";
            this.txtCouponCode.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtCouponCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCouponCode.CustomButton.TabIndex = 1;
            this.txtCouponCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCouponCode.CustomButton.UseSelectable = true;
            this.txtCouponCode.CustomButton.Visible = false;
            this.txtCouponCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtCouponCode.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txtCouponCode.Icon = global::SFPOSWindows.Properties.Resources.dollar;
            this.txtCouponCode.IconRight = true;
            this.txtCouponCode.Lines = new string[0];
            this.txtCouponCode.Location = new System.Drawing.Point(26, 124);
            this.txtCouponCode.MaxLength = 20;
            this.txtCouponCode.Name = "txtCouponCode";
            this.txtCouponCode.PasswordChar = '\0';
            this.txtCouponCode.PromptText = "Code";
            this.txtCouponCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCouponCode.SelectedText = "";
            this.txtCouponCode.SelectionLength = 0;
            this.txtCouponCode.SelectionStart = 0;
            this.txtCouponCode.ShortcutsEnabled = true;
            this.txtCouponCode.Size = new System.Drawing.Size(277, 27);
            this.txtCouponCode.TabIndex = 41;
            this.txtCouponCode.UseSelectable = true;
            this.txtCouponCode.WaterMark = "Code";
            this.txtCouponCode.WaterMarkColor = System.Drawing.Color.Silver;
            this.txtCouponCode.WaterMarkFont = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCouponCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCouponCode_KeyPress);
            // 
            // UCCheckCoupon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCouponCode);
            this.Name = "UCCheckCoupon";
            this.Size = new System.Drawing.Size(333, 242);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        public MetroFramework.Controls.MetroTextBox txtCouponCode;
    }
}
