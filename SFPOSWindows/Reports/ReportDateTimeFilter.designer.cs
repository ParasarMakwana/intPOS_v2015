namespace TillStatusReport.RDLCReports
{
    partial class ReportDateTimeFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDateTimeFilter));
            this.dtStartDate = new MetroFramework.Controls.MetroDateTime();
            this.lblStartDate = new MetroFramework.Controls.MetroLabel();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // dtStartDate
            // 
            this.dtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtStartDate.CalendarFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtStartDate.FontWeight = MetroFramework.MetroDateTimeWeight.Light;
            this.dtStartDate.Location = new System.Drawing.Point(23, 108);
            this.dtStartDate.MinDate = new System.DateTime(1809, 12, 26, 23, 59, 59, 0);
            this.dtStartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(214, 29);
            this.dtStartDate.TabIndex = 0;
            this.dtStartDate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.dtStartDate.Value = new System.DateTime(2020, 2, 13, 0, 0, 0, 0);
            this.dtStartDate.ValueChanged += new System.EventHandler(this.dtStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.lblStartDate.Location = new System.Drawing.Point(23, 74);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(80, 19);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Select Date:";
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Highlight = true;
            this.btnOK.Location = new System.Drawing.Point(75, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 29);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ReportDateTimeFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 236);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtStartDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportDateTimeFilter";
            this.Text = "Date Time";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel lblStartDate;
        private MetroFramework.Controls.MetroButton btnOK;
        public MetroFramework.Controls.MetroDateTime dtStartDate;
    }
}