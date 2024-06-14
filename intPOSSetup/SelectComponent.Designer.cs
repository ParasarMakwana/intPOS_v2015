namespace intPOSSetup
{
    partial class SelectComponent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectComponent));
            this.chkFrontend = new System.Windows.Forms.CheckBox();
            this.chkBackend = new System.Windows.Forms.CheckBox();
            this.chkLabelApp = new System.Windows.Forms.CheckBox();
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
            // chkFrontend
            // 
            this.chkFrontend.AutoSize = true;
            this.chkFrontend.Checked = true;
            this.chkFrontend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFrontend.Location = new System.Drawing.Point(30, 35);
            this.chkFrontend.Name = "chkFrontend";
            this.chkFrontend.Size = new System.Drawing.Size(68, 17);
            this.chkFrontend.TabIndex = 0;
            this.chkFrontend.Text = "Frontend";
            this.chkFrontend.UseVisualStyleBackColor = true;
            // 
            // chkBackend
            // 
            this.chkBackend.AutoSize = true;
            this.chkBackend.Checked = true;
            this.chkBackend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackend.Location = new System.Drawing.Point(30, 69);
            this.chkBackend.Name = "chkBackend";
            this.chkBackend.Size = new System.Drawing.Size(69, 17);
            this.chkBackend.TabIndex = 1;
            this.chkBackend.Text = "Backend";
            this.chkBackend.UseVisualStyleBackColor = true;
            // 
            // chkLabelApp
            // 
            this.chkLabelApp.AutoSize = true;
            this.chkLabelApp.Checked = true;
            this.chkLabelApp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLabelApp.Location = new System.Drawing.Point(30, 105);
            this.chkLabelApp.Name = "chkLabelApp";
            this.chkLabelApp.Size = new System.Drawing.Size(74, 17);
            this.chkLabelApp.TabIndex = 2;
            this.chkLabelApp.Text = "Label App";
            this.chkLabelApp.UseVisualStyleBackColor = true;
            // 
            // btnnext
            // 
            this.btnnext.Location = new System.Drawing.Point(429, 13);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(75, 23);
            this.btnnext.TabIndex = 3;
            this.btnnext.Text = "Next >";
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.chkLabelApp);
            this.panel3.Controls.Add(this.chkFrontend);
            this.panel3.Controls.Add(this.chkBackend);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(554, 206);
            this.panel3.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.btnBack);
            this.panel2.Controls.Add(this.btnnext);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 277);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 54);
            this.panel2.TabIndex = 7;
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
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Installation Components for intPOS";
            // 
            // SelectComponent
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
            this.Name = "SelectComponent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Installation Component";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFrontend;
        private System.Windows.Forms.CheckBox chkBackend;
        private System.Windows.Forms.CheckBox chkLabelApp;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}