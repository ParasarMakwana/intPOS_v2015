namespace SFPOSWindows.CustomControl
{
    partial class UCOrderSuccess
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
            this.components = new System.ComponentModel.Container();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblReceive = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTotal.Location = new System.Drawing.Point(250, 75);
            this.lblTotal.MaximumSize = new System.Drawing.Size(100, 23);
            this.lblTotal.MinimumSize = new System.Drawing.Size(120, 23);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 23);
            this.lblTotal.TabIndex = 59;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(73, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(362, 27);
            this.label4.TabIndex = 57;
            this.label4.Text = "Order Successfully Placed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 23);
            this.label3.TabIndex = 56;
            this.label3.Text = "Total Amount:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 23);
            this.label1.TabIndex = 55;
            this.label1.Text = "Receive Amount:";
            // 
            // lblChange
            // 
            this.lblChange.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblChange.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblChange.Location = new System.Drawing.Point(263, 5);
            this.lblChange.MaximumSize = new System.Drawing.Size(160, 50);
            this.lblChange.MinimumSize = new System.Drawing.Size(160, 50);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(160, 50);
            this.lblChange.TabIndex = 51;
            this.lblChange.Text = "0";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(215, 245);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 34);
            this.btnClear.TabIndex = 60;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblReceive
            // 
            this.lblReceive.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceive.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReceive.Location = new System.Drawing.Point(250, 118);
            this.lblReceive.MaximumSize = new System.Drawing.Size(100, 23);
            this.lblReceive.MinimumSize = new System.Drawing.Size(120, 23);
            this.lblReceive.Name = "lblReceive";
            this.lblReceive.Size = new System.Drawing.Size(120, 23);
            this.lblReceive.TabIndex = 58;
            this.lblReceive.Text = "0";
            this.lblReceive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Green;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblChange);
            this.panel2.Location = new System.Drawing.Point(31, 158);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(455, 65);
            this.panel2.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 31);
            this.label2.TabIndex = 48;
            this.label2.Text = "Change Amount:";
            // 
            // UCOrderSuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblReceive);
            this.Controls.Add(this.panel2);
            this.Name = "UCOrderSuccess";
            this.Size = new System.Drawing.Size(517, 308);
            this.Load += new System.EventHandler(this.FrnOrderSuccess_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblChange;
        public System.Windows.Forms.Label lblReceive;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnClear;
    }
}
