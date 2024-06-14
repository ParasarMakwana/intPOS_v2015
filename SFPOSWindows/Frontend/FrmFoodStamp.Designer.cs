namespace SFPOSWindows.Frontend
{
    partial class FrmFoodStamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFoodStamp));
            this.hidelbl = new MetroFramework.Controls.MetroLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFSAmount = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFSEligible = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hidelbl
            // 
            this.hidelbl.AutoSize = true;
            this.hidelbl.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.hidelbl.Location = new System.Drawing.Point(227, 265);
            this.hidelbl.Name = "hidelbl";
            this.hidelbl.Size = new System.Drawing.Size(17, 19);
            this.hidelbl.TabIndex = 30;
            this.hidelbl.Text = "0";
            this.hidelbl.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(202, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 41);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 27);
            this.label1.TabIndex = 36;
            this.label1.Text = "FS Eligible:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 27);
            this.label3.TabIndex = 40;
            this.label3.Text = "Received FS Amount:";
            // 
            // txtFSAmount
            // 
            this.txtFSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFSAmount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtFSAmount.Location = new System.Drawing.Point(23, 162);
            this.txtFSAmount.MaximumSize = new System.Drawing.Size(430, 60);
            this.txtFSAmount.MinimumSize = new System.Drawing.Size(430, 60);
            this.txtFSAmount.Name = "txtFSAmount";
            this.txtFSAmount.Size = new System.Drawing.Size(430, 60);
            this.txtFSAmount.TabIndex = 0;
            this.txtFSAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFSAmount_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.txtFSEligible);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(25, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 50);
            this.panel1.TabIndex = 51;
            // 
            // txtFSEligible
            // 
            this.txtFSEligible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFSEligible.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtFSEligible.ForeColor = System.Drawing.Color.White;
            this.txtFSEligible.Location = new System.Drawing.Point(225, 4);
            this.txtFSEligible.MaximumSize = new System.Drawing.Size(200, 41);
            this.txtFSEligible.MinimumSize = new System.Drawing.Size(200, 41);
            this.txtFSEligible.Name = "txtFSEligible";
            this.txtFSEligible.Size = new System.Drawing.Size(200, 41);
            this.txtFSEligible.TabIndex = 48;
            this.txtFSEligible.Text = "$0.00";
            this.txtFSEligible.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmFoodStamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 323);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtFSAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.hidelbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFoodStamp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFoodStamp_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel hidelbl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtFSAmount;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label txtFSEligible;
    }
}