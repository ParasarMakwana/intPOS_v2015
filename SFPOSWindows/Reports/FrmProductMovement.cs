using SFPOS.BAL.ReportServices;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmProductMovement : Form
    {
        ProductMovementService _ProductMovementService = new ProductMovementService();
        List<ProductMovement_ResultModel> lstProductMovement_ResultModel = new List<ProductMovement_ResultModel>();
        bool check12Days = false;
        bool check12Week = false;
        bool check12Month = false;
        bool check12Year = false;
        string tempUPCCode;

        public FrmProductMovement()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            groupBox2.Controls.Clear();
            groupBox3.Controls.Clear();
            groupBox4.Controls.Clear();
            check12Days = false;
            check12Week = false;
            check12Month = false;
            check12Year = false;
            PreviousDaysBox.Checked = false;
            previousWeekBox.Checked = false;
            previousMonthBox.Checked = false;
            previousYearBox.Checked = false;
            GetData();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    groupBox1.Controls.Clear();
                    groupBox2.Controls.Clear();
                    groupBox3.Controls.Clear();
                    groupBox4.Controls.Clear();
                    check12Days = false;
                    check12Week = false;
                    check12Month = false;
                    check12Year = false;
                    PreviousDaysBox.Checked = false;
                    previousWeekBox.Checked = false;
                    previousMonthBox.Checked = false;
                    previousYearBox.Checked = false;
                    GetData();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void GetData()
        {
            try
            {
                string upccode;
                if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    upccode = tempUPCCode;
                }
                else
                {
                    upccode = ClsCommon.GetFullUPCCode(txtSearch.Text.Trim());
                    tempUPCCode = upccode;
                }
                var productDetail = _ProductMovementService.GetProductDetails(upccode, startDate.Value, endDate.Value);
                if (productDetail.Count > 0)
                {
                    lblProductName.Text = !String.IsNullOrEmpty(productDetail[0].ProductName) ? productDetail[0].ProductName.ToString() : "";
                    lblUpcCode.Text = !String.IsNullOrEmpty(productDetail[0].UPCCode) ? productDetail[0].UPCCode.ToString() : "";
                    //lblUnitCost.Text = productDetail[0].Discount.ToString();
                    //lblTotalCost.Text = productDetail[0].Total_Cost.ToString();
                    //lblTotalRevenue.Text = productDetail[0].Total_Revenue.ToString();
                    //lblSellPrice.Text = productDetail[0].Margin.ToString();
                    //lblGrossProfit.Text = productDetail[0].Gross_Profit.ToString();
                    lblVendor.Text = !String.IsNullOrEmpty(productDetail[0].VendorName) ? productDetail[0].VendorName.ToString() : "";
                    lblItem.Text = !String.IsNullOrEmpty(productDetail[0].ItemCode) ? productDetail[0].ItemCode.ToString() : "";
                    lblUnitCost.Text = !String.IsNullOrEmpty(productDetail[0].UnitCost.ToString()) ? "$" + productDetail[0].UnitCost.ToString() : "";
                    lblSellPrice.Text = !String.IsNullOrEmpty(productDetail[0].Price.ToString()) ? "$" + productDetail[0].Price.ToString() : "";
                }
                else
                {
                    lblProductName.Text = "";
                    lblUpcCode.Text = "";
                    //lblUnitCost.Text = productDetail[0].Discount.ToString();
                    //lblTotalCost.Text = productDetail[0].Total_Cost.ToString();
                    //lblTotalRevenue.Text = productDetail[0].Total_Revenue.ToString();
                    //lblSellPrice.Text = productDetail[0].Margin.ToString();
                    //lblGrossProfit.Text = productDetail[0].Gross_Profit.ToString();
                    lblVendor.Text = "";
                    lblItem.Text = "";
                    lblUnitCost.Text = "";
                    lblSellPrice.Text = "";
                }


                txtSearch.Text = "";
                txtSearch.Focus();
                //groupBox1.Controls.Clear();
                //groupBox2.Controls.Clear();
                //groupBox3.Controls.Clear();
                //groupBox4.Controls.Clear();

                #region Days
                if (check12Days == true)
                {
                    var DaypnlLocX = 20;
                    var DaypnlLocY = 19;
                    var DaypnlSizeX = 75;
                    var DaypnlSizeY = 80;

                    var DaypixboxLocX = 0;
                    var DaypixboxLocY = 0;
                    var DaypixboxSizeX = 74;
                    var DaypixboxSizeY = 23;

                    var DaylblLocX = 0;
                    var DaylblLocY = 39;
                    var DaylblSizeX = 76;
                    var DaylblSizeY = 25;
                    DateTimeFormatInfo Dfi = new DateTimeFormatInfo();
                    lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(upccode, 4);
                    if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                    {
                        for (int i = 0; i < lstProductMovement_ResultModel.Count; i++)
                        {
                            string DayName = lstProductMovement_ResultModel[i].datevalue.Value.ToString("dd");
                            string yearName = lstProductMovement_ResultModel[i].datevalue.Value.ToString("MMM");

                            var dynamicPanel = new MetroFramework.Controls.MetroPanel();
                            dynamicPanel.Name = "pnlDay" + lstProductMovement_ResultModel[i].ToString();
                            dynamicPanel.Size = new Size(DaypnlSizeX, DaypnlSizeY);
                            dynamicPanel.Location = new Point(DaypnlLocX, DaypnlLocY);
                            dynamicPanel.Cursor = Cursors.No;
                            dynamicPanel.BorderStyle = BorderStyle.FixedSingle;
                            dynamicPanel.Anchor = AnchorStyles.Top;

                            var TextBox = new MetroFramework.Controls.MetroTextBox();
                            TextBox.Location = new Point(DaypixboxLocX, DaypixboxLocY);
                            TextBox.Name = "txtDay" + lstProductMovement_ResultModel[i].ToString();
                            TextBox.Text = DayName + "-" + yearName;
                            TextBox.Size = new Size(DaypixboxSizeX, DaypixboxSizeY);
                            TextBox.BackColor = Color.FromArgb(0, 174, 219);
                            TextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
                            TextBox.TextAlign = HorizontalAlignment.Center;
                            TextBox.ForeColor = Color.White;
                            TextBox.Style = MetroFramework.MetroColorStyle.Blue;
                            TextBox.UseCustomBackColor = true;
                            TextBox.UseCustomForeColor = true;

                            dynamicPanel.Controls.Add(TextBox);

                            var lbl = new MetroFramework.Controls.MetroLabel
                            {
                                Location = new Point(DaylblLocX, DaylblLocY),
                                Name = "lblDay" + lstProductMovement_ResultModel[i].ToString(),
                                Size = new Size(DaylblSizeX, DaylblSizeY),
                                Text = lstProductMovement_ResultModel[i].Qty.ToString(),
                                FontSize = MetroFramework.MetroLabelSize.Tall,
                                FontWeight = MetroFramework.MetroLabelWeight.Regular,
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            dynamicPanel.Controls.Add(lbl);
                            DaypnlLocX += DaypnlSizeX + 20;

                            groupBox1.Controls.Add(dynamicPanel);
                        }
                    }
                }
                #endregion

                #region Weeks
                if (check12Week == true)
                {
                    var pnlLocX = 20;
                    var pnlLocY = 19;
                    var pnlSizeX = 75;
                    var pnlSizeY = 80;

                    var pixboxLocX = 0;
                    var pixboxLocY = 0;
                    var pixboxSizeX = 74;
                    var pixboxSizeY = 23;

                    var lblLocX = 0;
                    var lblLocY = 39;
                    var lblSizeX = 76;
                    var lblSizeY = 25;
                    lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(upccode, 2);
                    if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                    {
                        for (int i = 0; i < lstProductMovement_ResultModel.Count; i++)
                        {
                            int Month = lstProductMovement_ResultModel[i].datevalue.Value.Month;
                            int Day = lstProductMovement_ResultModel[i].datevalue.Value.Day;

                            var dynamicPanel = new MetroFramework.Controls.MetroPanel();
                            dynamicPanel.Name = "pnlWeek" + lstProductMovement_ResultModel[i].ToString();
                            dynamicPanel.Size = new Size(pnlSizeX, pnlSizeY);
                            dynamicPanel.Location = new Point(pnlLocX, pnlLocY);
                            dynamicPanel.Cursor = Cursors.No;
                            dynamicPanel.BorderStyle = BorderStyle.FixedSingle;
                            dynamicPanel.Anchor = AnchorStyles.Top;

                            var TextBox = new MetroFramework.Controls.MetroTextBox();
                            TextBox.Location = new Point(pixboxLocX, pixboxLocY);
                            TextBox.Name = "txtWeek" + lstProductMovement_ResultModel[i].ToString();
                            TextBox.Text = Month + "/" + Day;
                            TextBox.Size = new Size(pixboxSizeX, pixboxSizeY);
                            TextBox.BackColor = Color.FromArgb(0, 174, 219);
                            TextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
                            TextBox.TextAlign = HorizontalAlignment.Center;
                            TextBox.ForeColor = Color.White;
                            TextBox.Style = MetroFramework.MetroColorStyle.Blue;
                            TextBox.UseCustomBackColor = true;
                            TextBox.UseCustomForeColor = true;
                            dynamicPanel.Controls.Add(TextBox);

                            var lbl = new MetroFramework.Controls.MetroLabel
                            {
                                Location = new Point(lblLocX, lblLocY),
                                Name = "lblWeek" + lstProductMovement_ResultModel[i].ToString(),
                                Size = new Size(lblSizeX, lblSizeY),
                                Text = lstProductMovement_ResultModel[i].Qty.ToString(),
                                FontSize = MetroFramework.MetroLabelSize.Tall,
                                FontWeight = MetroFramework.MetroLabelWeight.Regular,
                                TextAlign = ContentAlignment.MiddleCenter

                            };
                            dynamicPanel.Controls.Add(lbl);
                            pnlLocX += pnlSizeX + 20;

                            groupBox2.Controls.Add(dynamicPanel);
                        }
                    }
                }
                #endregion

                #region Months
                if (check12Month == true)
                {
                    var MonthpnlLocX = 20;
                    var MonthpnlLocY = 19;
                    var MonthpnlSizeX = 75;
                    var MonthpnlSizeY = 80;

                    var MonthpixboxLocX = 0;
                    var MonthpixboxLocY = 0;
                    var MonthpixboxSizeX = 74;
                    var MonthpixboxSizeY = 23;

                    var MonthlblLocX = 0;
                    var MonthlblLocY = 39;
                    var MonthlblSizeX = 76;
                    var MonthlblSizeY = 25;
                    lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(upccode, 1);
                    DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                    if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                    {
                        for (int i = 0; i < lstProductMovement_ResultModel.Count; i++)
                        {
                            string MonthName = lstProductMovement_ResultModel[i].datevalue.Value.ToString("MMM");
                            int yearName = lstProductMovement_ResultModel[i].datevalue.Value.Year;

                            var dynamicPanel = new MetroFramework.Controls.MetroPanel();
                            dynamicPanel.Name = "pnlMonth" + lstProductMovement_ResultModel[i].ToString();
                            dynamicPanel.Size = new Size(MonthpnlSizeX, MonthpnlSizeY);
                            dynamicPanel.Location = new Point(MonthpnlLocX, MonthpnlLocY);
                            dynamicPanel.Cursor = Cursors.No;
                            dynamicPanel.BorderStyle = BorderStyle.FixedSingle;
                            dynamicPanel.Anchor = AnchorStyles.Top;

                            var TextBox = new MetroFramework.Controls.MetroTextBox();
                            TextBox.Location = new Point(MonthpixboxLocX, MonthpixboxLocY);
                            TextBox.Name = "txtMonth" + lstProductMovement_ResultModel[i].ToString();
                            TextBox.Text = MonthName + "-" + yearName;
                            TextBox.Size = new Size(MonthpixboxSizeX, MonthpixboxSizeY);
                            TextBox.BackColor = Color.FromArgb(0, 174, 219);
                            TextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
                            TextBox.TextAlign = HorizontalAlignment.Center;
                            TextBox.ForeColor = Color.White;
                            TextBox.Style = MetroFramework.MetroColorStyle.Blue;
                            TextBox.UseCustomBackColor = true;
                            TextBox.UseCustomForeColor = true;

                            dynamicPanel.Controls.Add(TextBox);

                            var lbl = new MetroFramework.Controls.MetroLabel
                            {
                                Location = new Point(MonthlblLocX, MonthlblLocY),
                                Name = "lblMonth" + lstProductMovement_ResultModel[i].ToString(),
                                Size = new Size(MonthlblSizeX, MonthlblSizeY),
                                Text = lstProductMovement_ResultModel[i].Qty.ToString(),
                                FontSize = MetroFramework.MetroLabelSize.Tall,
                                FontWeight = MetroFramework.MetroLabelWeight.Regular,
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            dynamicPanel.Controls.Add(lbl);
                            MonthpnlLocX += MonthpnlSizeX + 20;

                            groupBox3.Controls.Add(dynamicPanel);
                        }
                    }

                }

                #endregion

                #region Years
                if (check12Year == true)
                {
                    var YearpnlLocX = 20;
                    var YearpnlLocY = 19;
                    var YearpnlSizeX = 75;
                    var YearpnlSizeY = 80;

                    var YearpixboxLocX = 0;
                    var YearpixboxLocY = 0;
                    var YearpixboxSizeX = 74;
                    var YearpixboxSizeY = 23;

                    var YearlblLocX = 0;
                    var YearlblLocY = 39;
                    var YearlblSizeX = 76;
                    var YearlblSizeY = 25;
                    lstProductMovement_ResultModel = _ProductMovementService.GetProductDetails(upccode, 3);
                    if (lstProductMovement_ResultModel != null && lstProductMovement_ResultModel.Count > 0)
                    {
                        for (int i = 0; i < lstProductMovement_ResultModel.Count; i++)
                        {
                            var dynamicPanel = new MetroFramework.Controls.MetroPanel();
                            dynamicPanel.Name = "pnlYear" + lstProductMovement_ResultModel[i].ToString();
                            dynamicPanel.Size = new Size(YearpnlSizeX, YearpnlSizeY);
                            dynamicPanel.Location = new Point(YearpnlLocX, YearpnlLocY);
                            dynamicPanel.Cursor = Cursors.No;
                            dynamicPanel.BorderStyle = BorderStyle.FixedSingle;
                            dynamicPanel.Anchor = AnchorStyles.Top;

                            var TextBox = new MetroFramework.Controls.MetroTextBox();
                            TextBox.Location = new Point(YearpixboxLocX, YearpixboxLocY);
                            TextBox.Name = "txtYear" + lstProductMovement_ResultModel[i].ToString();
                            TextBox.Text = lstProductMovement_ResultModel[i].datevalue.Value.Year.ToString();
                            TextBox.Size = new Size(YearpixboxSizeX, YearpixboxSizeY);
                            TextBox.BackColor = Color.FromArgb(0, 174, 219);
                            TextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
                            //TextBox.FontWeight = MetroFramework.MetroTextBoxWeight.;
                            TextBox.ForeColor = Color.White;
                            TextBox.TextAlign = HorizontalAlignment.Center;
                            TextBox.Style = MetroFramework.MetroColorStyle.Blue;
                            TextBox.UseCustomBackColor = true;
                            TextBox.UseCustomForeColor = true;
                            dynamicPanel.Controls.Add(TextBox);

                            var lbl = new MetroFramework.Controls.MetroLabel
                            {
                                Location = new Point(YearlblLocX, YearlblLocY),
                                Name = "lblYear" + lstProductMovement_ResultModel[i].ToString(),
                                Size = new Size(YearlblSizeX, YearlblSizeY),
                                Text = lstProductMovement_ResultModel[i].Qty.ToString(),
                                FontSize = MetroFramework.MetroLabelSize.Tall,
                                FontWeight = MetroFramework.MetroLabelWeight.Regular,
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            dynamicPanel.Controls.Add(lbl);
                            YearpnlLocX += YearpnlSizeX + 20;

                            groupBox4.Controls.Add(dynamicPanel);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void PreviousDaysBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PreviousDaysBox.Checked == true)
            {
                check12Days = true;
                check12Week = false;
                check12Month = false;
                check12Year = false;
                GetData();
            }
            else
            {
                check12Days = false;
                groupBox1.Controls.Clear();
            }
        }

        private void previousWeekBox_CheckedChanged(object sender, EventArgs e)
        {
            if (previousWeekBox.Checked == true)
            {
                check12Days = false;
                check12Week = true;
                check12Month = false;
                check12Year = false;
                GetData();
            }
            else
            {
                check12Week = false;
                groupBox2.Controls.Clear();
            }
        }

        private void previousMonthBox_CheckedChanged(object sender, EventArgs e)
        {
            if (previousMonthBox.Checked == true)
            {
                check12Days = false;
                check12Week = false;
                check12Month = true;
                check12Year = false;
                GetData();
            }
            else
            {
                check12Month = false;
                groupBox3.Controls.Clear();
            }
        }

        private void previousYearBox_CheckedChanged(object sender, EventArgs e)
        {
            if (previousYearBox.Checked == true)
            {
                check12Days = false;
                check12Week = false;
                check12Month = false;
                check12Year = true;
                GetData();
            }
            else
            {
                check12Year = false;
                groupBox4.Controls.Clear();
            }
        }
    }
}
