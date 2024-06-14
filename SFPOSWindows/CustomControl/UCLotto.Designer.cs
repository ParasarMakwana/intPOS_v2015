namespace SFPOSWindows.CustomControl
{
    partial class UCLotto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLotto));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLottoAmount = new System.Windows.Forms.TextBox();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnPayout = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(22, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 27);
            this.label1.TabIndex = 43;
            this.label1.Text = "Enter Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 27);
            this.label2.TabIndex = 36;
            this.label2.Text = "Lotto:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(24, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 50);
            this.panel1.TabIndex = 56;
            // 
            // txtLottoAmount
            // 
            this.txtLottoAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLottoAmount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtLottoAmount.Location = new System.Drawing.Point(22, 147);
            this.txtLottoAmount.Name = "txtLottoAmount";
            this.txtLottoAmount.Size = new System.Drawing.Size(379, 40);
            this.txtLottoAmount.TabIndex = 57;
            // 
            // btnSales
            // 
            this.btnSales.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSales.BackColor = System.Drawing.Color.Transparent;
            this.btnSales.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSales.BackgroundImage")));
            this.btnSales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSales.FlatAppearance.BorderSize = 0;
            this.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSales.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSales.Location = new System.Drawing.Point(22, 226);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(161, 54);
            this.btnSales.TabIndex = 148;
            this.btnSales.Text = "SALES";
            this.btnSales.UseVisualStyleBackColor = false;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnPayout
            // 
            this.btnPayout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPayout.BackColor = System.Drawing.Color.Transparent;
            this.btnPayout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPayout.BackgroundImage")));
            this.btnPayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPayout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayout.FlatAppearance.BorderSize = 0;
            this.btnPayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayout.Location = new System.Drawing.Point(240, 226);
            this.btnPayout.Name = "btnPayout";
            this.btnPayout.Size = new System.Drawing.Size(161, 54);
            this.btnPayout.TabIndex = 149;
            this.btnPayout.Text = "PAYOUT";
            this.btnPayout.UseVisualStyleBackColor = false;
            this.btnPayout.Click += new System.EventHandler(this.btnPayout_Click);
            // 
            // UCLotto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnPayout);
            this.Controls.Add(this.btnSales);
            this.Controls.Add(this.txtLottoAmount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "UCLotto";
            this.Size = new System.Drawing.Size(430, 313);
            this.Load += new System.EventHandler(this.UCLotto_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtLottoAmount;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnPayout;
    }
}
