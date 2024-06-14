namespace intPOSSetup
{
    partial class VersionType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionType));
            this.rdbDemo = new System.Windows.Forms.RadioButton();
            this.rdbFull = new System.Windows.Forms.RadioButton();
            this.btnnext = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbDemo
            // 
            this.rdbDemo.AutoSize = true;
            this.rdbDemo.Checked = true;
            this.rdbDemo.Location = new System.Drawing.Point(46, 37);
            this.rdbDemo.Name = "rdbDemo";
            this.rdbDemo.Size = new System.Drawing.Size(91, 17);
            this.rdbDemo.TabIndex = 0;
            this.rdbDemo.TabStop = true;
            this.rdbDemo.Text = "Demo Version";
            this.rdbDemo.UseVisualStyleBackColor = true;
            // 
            // rdbFull
            // 
            this.rdbFull.AutoSize = true;
            this.rdbFull.Location = new System.Drawing.Point(46, 72);
            this.rdbFull.Name = "rdbFull";
            this.rdbFull.Size = new System.Drawing.Size(79, 17);
            this.rdbFull.TabIndex = 1;
            this.rdbFull.Text = "Full Version";
            this.rdbFull.UseVisualStyleBackColor = true;
            // 
            // btnnext
            // 
            this.btnnext.Location = new System.Drawing.Point(425, 13);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(75, 23);
            this.btnnext.TabIndex = 2;
            this.btnnext.Text = "Next >";
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rdbFull);
            this.panel3.Controls.Add(this.rdbDemo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(554, 206);
            this.panel3.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.btnnext);
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 54);
            this.panel2.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(344, 13);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 71);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Installation Version for intPOS";
            // 
            // VersionType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 331);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Installation Version";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdbDemo;
        private System.Windows.Forms.RadioButton rdbFull;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}