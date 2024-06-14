namespace SFPOSWindows.CustomControl
{
    partial class UCTotal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTotal));
            this.txtTotal = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new System.Windows.Forms.Label();
            this.lblCoupon = new System.Windows.Forms.Label();
            this.txtTotalAmt = new System.Windows.Forms.Label();
            this.lblDiscAmt = new System.Windows.Forms.Label();
            this.txtReceiveAmt = new System.Windows.Forms.TextBox();
            this.txtCoupon = new System.Windows.Forms.Label();
            this.lblTotalReceivedAmt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalReceivedAmount = new System.Windows.Forms.Label();
            this.txtChange = new System.Windows.Forms.Label();
            this.pnlChange = new System.Windows.Forms.Panel();
            this.pnlCoupon = new System.Windows.Forms.Panel();
            this.lblReceiveAmt = new System.Windows.Forms.Label();
            this.txtRemainingAmount = new System.Windows.Forms.Label();
            this.lblReceive = new System.Windows.Forms.Label();
            this.hidelbl = new MetroFramework.Controls.MetroLabel();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.pnlBalance = new System.Windows.Forms.Panel();
            this.btnCredit = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnRemoveCoupon = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlChange.SuspendLayout();
            this.pnlCoupon.SuspendLayout();
            this.pnlBalance.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTotal
            // 
            this.txtTotal.AutoSize = true;
            this.txtTotal.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.Location = new System.Drawing.Point(220, 8);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(58, 18);
            this.txtTotal.TabIndex = 60;
            this.txtTotal.Text = "$0.00";
            this.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.AutoSize = true;
            this.txtDiscount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.ForeColor = System.Drawing.Color.White;
            this.txtDiscount.Location = new System.Drawing.Point(292, 26);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(18, 18);
            this.txtDiscount.TabIndex = 60;
            this.txtDiscount.Text = "%";
            this.txtDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(19, 8);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(138, 18);
            this.lblTotal.TabIndex = 59;
            this.lblTotal.Text = "Total Amount:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.AutoSize = true;
            this.txtDiscountAmount.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscountAmount.ForeColor = System.Drawing.Color.White;
            this.txtDiscountAmount.Location = new System.Drawing.Point(220, 26);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Size = new System.Drawing.Size(58, 18);
            this.txtDiscountAmount.TabIndex = 58;
            this.txtDiscountAmount.Text = "$0.00";
            this.txtDiscountAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCoupon
            // 
            this.lblCoupon.AutoSize = true;
            this.lblCoupon.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoupon.ForeColor = System.Drawing.Color.White;
            this.lblCoupon.Location = new System.Drawing.Point(18, 44);
            this.lblCoupon.Name = "lblCoupon";
            this.lblCoupon.Size = new System.Drawing.Size(128, 18);
            this.lblCoupon.TabIndex = 57;
            this.lblCoupon.Text = "Coupon Code:";
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalAmt.BackColor = System.Drawing.Color.Transparent;
            this.txtTotalAmt.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmt.ForeColor = System.Drawing.Color.Black;
            this.txtTotalAmt.Location = new System.Drawing.Point(301, 238);
            this.txtTotalAmt.MaximumSize = new System.Drawing.Size(100, 20);
            this.txtTotalAmt.MinimumSize = new System.Drawing.Size(100, 20);
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmt.TabIndex = 64;
            this.txtTotalAmt.Text = "$0.00";
            this.txtTotalAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtTotalAmt.Visible = false;
            // 
            // lblDiscAmt
            // 
            this.lblDiscAmt.AutoSize = true;
            this.lblDiscAmt.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscAmt.ForeColor = System.Drawing.Color.White;
            this.lblDiscAmt.Location = new System.Drawing.Point(19, 26);
            this.lblDiscAmt.Name = "lblDiscAmt";
            this.lblDiscAmt.Size = new System.Drawing.Size(178, 18);
            this.lblDiscAmt.TabIndex = 57;
            this.lblDiscAmt.Text = "Applied Discount:";
            // 
            // txtReceiveAmt
            // 
            this.txtReceiveAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReceiveAmt.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtReceiveAmt.Location = new System.Drawing.Point(19, 177);
            this.txtReceiveAmt.MaximumSize = new System.Drawing.Size(377, 45);
            this.txtReceiveAmt.MinimumSize = new System.Drawing.Size(377, 45);
            this.txtReceiveAmt.Name = "txtReceiveAmt";
            this.txtReceiveAmt.Size = new System.Drawing.Size(377, 40);
            this.txtReceiveAmt.TabIndex = 71;
            this.txtReceiveAmt.TextChanged += new System.EventHandler(this.txtReceiveAmt_TextChanged);
            this.txtReceiveAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReceiveAmt_KeyPress);
            // 
            // txtCoupon
            // 
            this.txtCoupon.AutoSize = true;
            this.txtCoupon.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCoupon.ForeColor = System.Drawing.Color.White;
            this.txtCoupon.Location = new System.Drawing.Point(220, 44);
            this.txtCoupon.Name = "txtCoupon";
            this.txtCoupon.Size = new System.Drawing.Size(68, 18);
            this.txtCoupon.TabIndex = 58;
            this.txtCoupon.Text = "Coupon";
            // 
            // lblTotalReceivedAmt
            // 
            this.lblTotalReceivedAmt.AutoSize = true;
            this.lblTotalReceivedAmt.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReceivedAmt.ForeColor = System.Drawing.Color.Red;
            this.lblTotalReceivedAmt.Location = new System.Drawing.Point(37, 350);
            this.lblTotalReceivedAmt.Name = "lblTotalReceivedAmt";
            this.lblTotalReceivedAmt.Size = new System.Drawing.Size(222, 27);
            this.lblTotalReceivedAmt.TabIndex = 69;
            this.lblTotalReceivedAmt.Text = "Total Received:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 33);
            this.label3.TabIndex = 11;
            this.label3.Text = "Change";
            // 
            // txtTotalReceivedAmount
            // 
            this.txtTotalReceivedAmount.AutoSize = true;
            this.txtTotalReceivedAmount.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalReceivedAmount.ForeColor = System.Drawing.Color.Red;
            this.txtTotalReceivedAmount.Location = new System.Drawing.Point(296, 350);
            this.txtTotalReceivedAmount.Name = "txtTotalReceivedAmount";
            this.txtTotalReceivedAmount.Size = new System.Drawing.Size(82, 27);
            this.txtTotalReceivedAmount.TabIndex = 70;
            this.txtTotalReceivedAmount.Text = "$0.00";
            this.txtTotalReceivedAmount.TextChanged += new System.EventHandler(this.txtTotalReceivedAmount_TextChanged);
            // 
            // txtChange
            // 
            this.txtChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChange.AutoSize = true;
            this.txtChange.BackColor = System.Drawing.Color.Green;
            this.txtChange.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChange.ForeColor = System.Drawing.Color.White;
            this.txtChange.Location = new System.Drawing.Point(173, 10);
            this.txtChange.MaximumSize = new System.Drawing.Size(200, 41);
            this.txtChange.MinimumSize = new System.Drawing.Size(200, 41);
            this.txtChange.Name = "txtChange";
            this.txtChange.Size = new System.Drawing.Size(200, 41);
            this.txtChange.TabIndex = 16;
            this.txtChange.Text = "$0.00";
            this.txtChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlChange
            // 
            this.pnlChange.BackColor = System.Drawing.Color.Green;
            this.pnlChange.Controls.Add(this.txtChange);
            this.pnlChange.Controls.Add(this.label3);
            this.pnlChange.Location = new System.Drawing.Point(19, 261);
            this.pnlChange.Name = "pnlChange";
            this.pnlChange.Size = new System.Drawing.Size(377, 65);
            this.pnlChange.TabIndex = 67;
            // 
            // pnlCoupon
            // 
            this.pnlCoupon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(89)))), ((int)(((byte)(247)))));
            this.pnlCoupon.Controls.Add(this.lblDiscAmt);
            this.pnlCoupon.Controls.Add(this.txtTotal);
            this.pnlCoupon.Controls.Add(this.txtDiscount);
            this.pnlCoupon.Controls.Add(this.lblTotal);
            this.pnlCoupon.Controls.Add(this.txtDiscountAmount);
            this.pnlCoupon.Controls.Add(this.txtCoupon);
            this.pnlCoupon.Controls.Add(this.lblCoupon);
            this.pnlCoupon.Location = new System.Drawing.Point(24, 37);
            this.pnlCoupon.Name = "pnlCoupon";
            this.pnlCoupon.Size = new System.Drawing.Size(377, 66);
            this.pnlCoupon.TabIndex = 72;
            this.pnlCoupon.Visible = false;
            // 
            // lblReceiveAmt
            // 
            this.lblReceiveAmt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReceiveAmt.AutoSize = true;
            this.lblReceiveAmt.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceiveAmt.ForeColor = System.Drawing.Color.White;
            this.lblReceiveAmt.Location = new System.Drawing.Point(12, 15);
            this.lblReceiveAmt.Name = "lblReceiveAmt";
            this.lblReceiveAmt.Size = new System.Drawing.Size(134, 33);
            this.lblReceiveAmt.TabIndex = 11;
            this.lblReceiveAmt.Text = "Balance";
            // 
            // txtRemainingAmount
            // 
            this.txtRemainingAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemainingAmount.AutoSize = true;
            this.txtRemainingAmount.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Bold);
            this.txtRemainingAmount.ForeColor = System.Drawing.Color.White;
            this.txtRemainingAmount.Location = new System.Drawing.Point(168, 11);
            this.txtRemainingAmount.MaximumSize = new System.Drawing.Size(200, 41);
            this.txtRemainingAmount.MinimumSize = new System.Drawing.Size(200, 41);
            this.txtRemainingAmount.Name = "txtRemainingAmount";
            this.txtRemainingAmount.Size = new System.Drawing.Size(200, 41);
            this.txtRemainingAmount.TabIndex = 48;
            this.txtRemainingAmount.Text = "$0.00";
            this.txtRemainingAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReceive
            // 
            this.lblReceive.AutoSize = true;
            this.lblReceive.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceive.Location = new System.Drawing.Point(19, 147);
            this.lblReceive.Name = "lblReceive";
            this.lblReceive.Size = new System.Drawing.Size(208, 27);
            this.lblReceive.TabIndex = 68;
            this.lblReceive.Text = "Receive Amount";
            // 
            // hidelbl
            // 
            this.hidelbl.AutoSize = true;
            this.hidelbl.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.hidelbl.Location = new System.Drawing.Point(24, 238);
            this.hidelbl.Name = "hidelbl";
            this.hidelbl.Size = new System.Drawing.Size(17, 19);
            this.hidelbl.TabIndex = 65;
            this.hidelbl.Text = "0";
            this.hidelbl.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnCancel.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Highlight = true;
            this.btnCancel.Location = new System.Drawing.Point(144, 343);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 34);
            this.btnCancel.Style = MetroFramework.MetroColorStyle.Blue;
            this.btnCancel.TabIndex = 63;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnCancel.UseCustomBackColor = true;
            this.btnCancel.UseCustomForeColor = true;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBalance
            // 
            this.pnlBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.pnlBalance.Controls.Add(this.lblReceiveAmt);
            this.pnlBalance.Controls.Add(this.txtRemainingAmount);
            this.pnlBalance.Location = new System.Drawing.Point(24, 37);
            this.pnlBalance.Name = "pnlBalance";
            this.pnlBalance.Size = new System.Drawing.Size(377, 66);
            this.pnlBalance.TabIndex = 66;
            // 
            // btnCredit
            // 
            this.btnCredit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCredit.BackColor = System.Drawing.Color.Transparent;
            this.btnCredit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCredit.BackgroundImage")));
            this.btnCredit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCredit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCredit.FlatAppearance.BorderSize = 0;
            this.btnCredit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCredit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCredit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCredit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredit.Location = new System.Drawing.Point(232, 390);
            this.btnCredit.Name = "btnCredit";
            this.btnCredit.Size = new System.Drawing.Size(160, 45);
            this.btnCredit.TabIndex = 151;
            this.btnCredit.Text = "            CREDIT CARD";
            this.btnCredit.UseVisualStyleBackColor = false;
            this.btnCredit.Click += new System.EventHandler(this.btnCredit_Click);
            // 
            // btnCash
            // 
            this.btnCash.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCash.BackColor = System.Drawing.Color.Transparent;
            this.btnCash.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCash.BackgroundImage")));
            this.btnCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash.FlatAppearance.BorderSize = 0;
            this.btnCash.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCash.Location = new System.Drawing.Point(24, 390);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(160, 45);
            this.btnCash.TabIndex = 152;
            this.btnCash.Text = "  CASH  ";
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCheck.BackColor = System.Drawing.Color.Transparent;
            this.btnCheck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCheck.BackgroundImage")));
            this.btnCheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(129, 492);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(160, 45);
            this.btnCheck.TabIndex = 150;
            this.btnCheck.Text = "    CHECK";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnRemoveCoupon
            // 
            this.btnRemoveCoupon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemoveCoupon.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCoupon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoveCoupon.BackgroundImage")));
            this.btnRemoveCoupon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveCoupon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveCoupon.FlatAppearance.BorderSize = 0;
            this.btnRemoveCoupon.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCoupon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCoupon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRemoveCoupon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveCoupon.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveCoupon.Location = new System.Drawing.Point(232, 441);
            this.btnRemoveCoupon.Name = "btnRemoveCoupon";
            this.btnRemoveCoupon.Size = new System.Drawing.Size(160, 45);
            this.btnRemoveCoupon.TabIndex = 153;
            this.btnRemoveCoupon.Text = "            REMOVE              COUPON";
            this.btnRemoveCoupon.UseVisualStyleBackColor = false;
            this.btnRemoveCoupon.Visible = false;
            this.btnRemoveCoupon.Click += new System.EventHandler(this.btnRemoveCoupon_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(24, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 45);
            this.button1.TabIndex = 154;
            this.button1.Text = "            DEBIT CARD";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UCTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCredit);
            this.Controls.Add(this.btnCash);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnRemoveCoupon);
            this.Controls.Add(this.txtTotalAmt);
            this.Controls.Add(this.txtReceiveAmt);
            this.Controls.Add(this.lblTotalReceivedAmt);
            this.Controls.Add(this.txtTotalReceivedAmount);
            this.Controls.Add(this.pnlChange);
            this.Controls.Add(this.lblReceive);
            this.Controls.Add(this.hidelbl);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlBalance);
            this.Controls.Add(this.pnlCoupon);
            this.Name = "UCTotal";
            this.Size = new System.Drawing.Size(419, 546);
            this.Load += new System.EventHandler(this.UCTotal_Load);
            this.pnlChange.ResumeLayout(false);
            this.pnlChange.PerformLayout();
            this.pnlCoupon.ResumeLayout(false);
            this.pnlCoupon.PerformLayout();
            this.pnlBalance.ResumeLayout(false);
            this.pnlBalance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtTotal;
        private System.Windows.Forms.Label txtDiscount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label txtDiscountAmount;
        private System.Windows.Forms.Label lblCoupon;
        public System.Windows.Forms.Label txtTotalAmt;
        private System.Windows.Forms.Label lblDiscAmt;
        private System.Windows.Forms.Label txtCoupon;
        public System.Windows.Forms.Label lblTotalReceivedAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtTotalReceivedAmount;
        private System.Windows.Forms.Label txtChange;
        private System.Windows.Forms.Panel pnlChange;
        private System.Windows.Forms.Panel pnlCoupon;
        public System.Windows.Forms.Label lblReceiveAmt;
        public System.Windows.Forms.Label txtRemainingAmount;
        private System.Windows.Forms.Label lblReceive;
        private MetroFramework.Controls.MetroLabel hidelbl;
        private MetroFramework.Controls.MetroButton btnCancel;
        private System.Windows.Forms.Panel pnlBalance;
        public System.Windows.Forms.TextBox txtReceiveAmt;
        private System.Windows.Forms.Button btnCredit;
        private System.Windows.Forms.Button btnCash;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnRemoveCoupon;
        public System.Windows.Forms.Button btnCheck;
    }
}
