using Microsoft.PointOfService;
using SFPOS.BAL.Frontend;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.FrontEnd;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Printer;
using SFPOS.Printer.Enums;
using SFPOSWindows.CustomControl;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SFPOSWindows
{
    public partial class Cutomer : Form
    {

        #region Properties
        decimal Balance = 0;
        decimal FSEligibleAmt = 0;
        decimal TaxableAmount = 0;
        decimal FSEligibleVoidAmt = 0;

        List<OrderScanner_ResultModel> Productdata_ = new List<OrderScanner_ResultModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public DataTable dt_ = new DataTable();
        #endregion

        public Cutomer()
        {
            InitializeComponent();

            //int iWidth = Screen.PrimaryScreen.Bounds.Width;
            if (Screen.AllScreens.Length != 1)
            {
                int iWidth = Screen.AllScreens[XMLData.CustomerScreen].WorkingArea.Width;
                int ourScreenWidth = metroPanel1.Width;
                int ourScreenHeight = metroPanel1.Height;
                if (iWidth > 1025)
                {
                    lblWeight.Font = new Font("Courier New", 44, FontStyle.Bold);
                    float scaleFactorWidth = (float)ourScreenWidth / 540;
                    float scaleFactorHeigth = (float)ourScreenHeight / ourScreenHeight;
                    SizeF scaleFactor = new SizeF(scaleFactorWidth, scaleFactorHeigth);
                    metroPanel1.Scale(scaleFactor);
                    metroPanel1.Width = ourScreenWidth;

                    lblTotalCount.Font = new Font("Courier New", 20, FontStyle.Bold);
                    lblSubTotal.Font = new Font("Courier New", 20, FontStyle.Bold);
                    lblTaxAmount.Font = new Font("Courier New", 20, FontStyle.Bold);
                    lblFSTotal.Font = new Font("Courier New", 20, FontStyle.Bold);
                    lblTotal.Font = new Font("Courier New", 20, FontStyle.Bold);

                    label4.Text = "       " + label4.Text;
                }
            }


            timer1.Start();
            lblStoreName.Text = LoginInfo.StoreName;
            metroPanel1.Focus();

        }

        private void Cutomer_Shown(object sender, EventArgs e)
        {
            ClsCommon.SetScreen(this, XMLData.CustomerScreen);
            //int ScreenIndex = Convert.ToInt32(XMLData.CustomerScreen);
            //this.Location = Screen.AllScreens[ScreenIndex].Bounds.Location;
            //this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.AllScreens[ScreenIndex].Bounds;
        }

        public void dataLoad()
        {
            try
            {
                string QtyLBSign = "QtyLBSign";
                string RateLBSign = "RateLBSign";

                dataGridProducts.DataSource = dt_;

                #region DESIGN
                dataGridProducts.Columns[ProductMasterModelCont.ProductName].Width = 300;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].Width = 60;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].Width = 100;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].Width = 100;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Abb].Width = 30;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Image].Width = 40;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FinalPrice].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.RowNumber].Visible = false;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.OverridePrice].Visible = false;
                dataGridProducts.Columns[ProductMasterModelCont.PrintProductName].Visible = false;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.FSEligibleAmount].Visible = false;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.IsGroupPrice].Visible = false;
                dataGridProducts.ColumnHeadersHeight = 50;
                dataGridProducts.RowTemplate.MinimumHeight = 50;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.Qty].Visible = false;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.SalePrice].Visible = false;
                dataGridProducts.Columns[QtyLBSign].Width = 100;
                dataGridProducts.Columns[RateLBSign].Width = 210;
                dataGridProducts.Columns[QtyLBSign].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.Columns[RateLBSign].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridProducts.Columns[OrderScanner_ResultModelCont.IsManWTRefund].Visible = false;
                #endregion

                //itemCount();

                dataGridProducts.ClearSelection();
                int nRowIndex = dataGridProducts.Rows.Count - 1;

                if (dataGridProducts.RowCount >= 1)
                {
                    //dataGridProducts.ScrollBars = ScrollBars.Vertical;
                    dataGridProducts.Rows[nRowIndex].Selected = true;
                    dataGridProducts.FirstDisplayedScrollingRowIndex = dataGridProducts.RowCount - 1;
                }
                else
                {
                    dataGridProducts.ScrollBars = ScrollBars.None;
                }
                ChangeColor();
                //txtSearchUPCCode.Text = CommonModelCont.EmptyString;
                //txtSearchUPCCode.Focus();


                //



                //
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void itemCount()
        {
            try
            {
                decimal count = 0;
                decimal SubTotal = 0;
                decimal TaxAmount = 0;
                decimal Total = 0;
                string te = "0";
                decimal ForceTaxAmount = 0;
                decimal ForceTaxableAmount = 0;
                FSEligibleAmt = 0;
                TaxableAmount = 0;
                FSEligibleVoidAmt = 0;


                for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                {
                    if (dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value != null)
                    {
                        te = (dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.FinalPrice].Value).ToString().Replace(CommonModelCont.AddDollorSign, string.Empty);
                    }
                    SubTotal += Functions.GetDecimal(te);
                    TaxAmount += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());

                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsScale].Value.ToString()) == false)
                    {
                        count += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString());
                    }
                    else
                    {
                        if (Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.Qty].Value.ToString()) > 0)
                        {
                            count += 1;
                        }
                        else
                        {
                            count -= 1;
                        }
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true)
                    {
                        FSEligibleAmt += Functions.GetDecimal(te);
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        TaxableAmount += Functions.GetDecimal(te);
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsFoodStamp].Value.ToString()) == true
                       && Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {

                        FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                    }
                    else if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        FSEligibleVoidAmt += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                    }
                    if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsForceTax].Value.ToString()) == true
                       && Functions.GetBoolean(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.IsTax].Value.ToString()) == true)
                    {
                        ForceTaxAmount += Functions.GetDecimal(dataGridProducts.Rows[i].Cells[OrderScanner_ResultModelCont.TaxAmount].Value.ToString());
                        ForceTaxableAmount += Functions.GetDecimal(te);
                    }
                }
                //TaxCarry
                if (LoginInfo.TaxCarry)
                {
                    //OrderInfo.TaxCarryAmount = TaxAmount;
                    //TaxAmount = 0;

                    OrderInfo.TaxCarryAmount = TaxAmount - ForceTaxAmount;
                    TaxAmount = ForceTaxAmount;

                    TaxableAmount = ForceTaxableAmount;
                }
                Total += SubTotal + TaxAmount;
                if (count > 0)
                {
                    lblTotalCount.Text = count.ToString();
                }
                else
                {
                    lblTotalCount.Text = "0";
                }
                lblSubTotal.Text = Functions.GetDisplayAmount(SubTotal.ToString());
                lblTaxAmount.Text = Functions.GetDisplayAmount(TaxAmount.ToString());
                lblFinalAmount.Text = Functions.GetDisplayAmount(Total.ToString());
                lblFSTotal.Text = Functions.GetDisplayAmount("0.00");
                //ClsCommon.MsgBox("Information",FSEligibleVoidAmt.ToString());

                //MessageBox.Show(TaxableAmount.ToString());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }
        public void ChangeColor()
        {
            try
            {
                int nRowIndex = dataGridProducts.Rows.Count - 1;

                string i1 = nRowIndex.ToString();
                if (nRowIndex >= 0)
                {
                    for (int i = 0; i < dataGridProducts.Rows.Count; i++)
                    {
                        bool termp_ = Functions.GetBoolean(dataGridProducts.Rows[i].Cells["IsVoid"].Value.ToString());
                        if (termp_)
                        {
                            dataGridProducts.ClearSelection();
                            dataGridProducts.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else if (Functions.GetBoolean(dataGridProducts.Rows[i].Cells["DiscountApplyed"].Value.ToString()))
                        {
                            //dataGridProducts.ClearSelection();
                            //dataGridProducts.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                        }
                        else
                        {
                            dataGridProducts.ScrollBars = ScrollBars.Vertical;
                            dataGridProducts.Rows[nRowIndex].Selected = true;
                            dataGridProducts.FirstDisplayedScrollingRowIndex = dataGridProducts.RowCount - 1;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = Functions.GetCurrentDateTime().ToLongTimeString();
            lblDate.Text = Functions.GetCurrentDateTime().ToShortDateString();
        }
    }
}
