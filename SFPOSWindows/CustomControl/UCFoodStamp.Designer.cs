namespace SFPOSWindows.CustomControl
{
    partial class UCFoodStamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFoodStamp));
            this.txtFSAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFSEligible = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTotalCancel = new System.Windows.Forms.Button();
            this.btnFoodstamp = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFSAmount
            // 
            this.txtFSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFSAmount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtFSAmount.Location = new System.Drawing.Point(22, 147);
            this.txtFSAmount.Name = "txtFSAmount";
            this.txtFSAmount.Size = new System.Drawing.Size(379, 40);
            this.txtFSAmount.TabIndex = 52;
            this.txtFSAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFSAmount_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 27);
            this.label3.TabIndex = 54;
            this.label3.Text = "Received FS Amount:";
            // 
            // txtFSEligible
            // 
            this.txtFSEligible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFSEligible.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtFSEligible.ForeColor = System.Drawing.Color.White;
            this.txtFSEligible.Location = new System.Drawing.Point(185, 2);
            this.txtFSEligible.MaximumSize = new System.Drawing.Size(200, 41);
            this.txtFSEligible.MinimumSize = new System.Drawing.Size(200, 41);
            this.txtFSEligible.Name = "txtFSEligible";
            this.txtFSEligible.Size = new System.Drawing.Size(200, 41);
            this.txtFSEligible.TabIndex = 48;
            this.txtFSEligible.Text = "$0.00";
            this.txtFSEligible.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 27);
            this.label1.TabIndex = 36;
            this.label1.Text = "FS Eligible:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.txtFSEligible);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 50);
            this.panel1.TabIndex = 55;
            // 
            // btnTotalCancel
            // 
            this.btnTotalCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTotalCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnTotalCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTotalCancel.BackgroundImage")));
            this.btnTotalCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTotalCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTotalCancel.FlatAppearance.BorderSize = 0;
            this.btnTotalCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTotalCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotalCancel.Location = new System.Drawing.Point(22, 226);
            this.btnTotalCancel.Name = "btnTotalCancel";
            this.btnTotalCancel.Size = new System.Drawing.Size(161, 54);
            this.btnTotalCancel.TabIndex = 147;
            this.btnTotalCancel.Text = "    CANCEL";
            this.btnTotalCancel.UseVisualStyleBackColor = false;
            this.btnTotalCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFoodstamp
            // 
            this.btnFoodstamp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFoodstamp.BackColor = System.Drawing.Color.Transparent;
            this.btnFoodstamp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFoodstamp.BackgroundImage")));
            this.btnFoodstamp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFoodstamp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFoodstamp.FlatAppearance.BorderSize = 0;
            this.btnFoodstamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFoodstamp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFoodstamp.Location = new System.Drawing.Point(240, 226);
            this.btnFoodstamp.Name = "btnFoodstamp";
            this.btnFoodstamp.Size = new System.Drawing.Size(161, 54);
            this.btnFoodstamp.TabIndex = 148;
            this.btnFoodstamp.Text = "            FOODSTAMP";
            this.btnFoodstamp.UseVisualStyleBackColor = false;
            this.btnFoodstamp.Click += new System.EventHandler(this.btnFoodstamp_Click);
            // 
            // UCFoodStamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnFoodstamp);
            this.Controls.Add(this.btnTotalCancel);
            this.Controls.Add(this.txtFSAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "UCFoodStamp";
            this.Size = new System.Drawing.Size(430, 313);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtFSAmount;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label txtFSEligible;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTotalCancel;
        private System.Windows.Forms.Button btnFoodstamp;
    }
}
