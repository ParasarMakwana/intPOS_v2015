using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.PointOfService;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Common;
using System.Data.SqlServerCe;
using SFPOS.BAL.MasterDataServices;
using SFPOS.DAL;

namespace SFPOSWindows.CustomControl
{
    public partial class UCPriceCheck : UserControl
    {
        #region Properties
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        private PosExplorer myPosExplorer;
        private Scanner myScanner;

        public static SqlCeDataAdapter DataAdapter = null;
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);
        public bool IsPriceCheckClose = false;

        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        #endregion

        #region Events
        private void UCPriceCheck_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }


        private void txtUPCCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    btnClose.Focus();
                    ProductAdd();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtUPCCode.Text = "";
                // DeviceRemove();
                LoginInfo.frmName = "Master";
                IsPriceCheckClose = true;
                OnMyEvent(this, new EventArgs());
                this.Hide();
                txtUPCCode.Focus();

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public UCPriceCheck()
        {
            InitializeComponent();
            //myPosExplorer = new PosExplorer(this);
            txtUPCCode.Focus();
        }

        public void ProductAdd()
        {
            try
            {
                if (txtUPCCode.Text != CommonModelCont.EmptyString)
                {
                    #region cl
                    if (txtUPCCode.Text.ToLower().Contains("cl"))
                    {
                        txtUPCCode.Text = "";
                    }
                    #endregion

                    #region 4 Digit Search
                    else
                    {
                        string OrignalUPCCode = txtUPCCode.Text.Trim();
                        int Count = txtUPCCode.Text.Length;

                        txtUPCCode.Text = ClsCommon.GetFullUPCCode(txtUPCCode.Text.Trim());

                        decimal Productdata;

                        #region Product Scan
                        Productdata = UPCScanner(txtUPCCode.Text.Trim());
                        #endregion

                        #region Product UPC-E
                        if (Productdata == 0)
                        {
                            string[] UPC_E = new string[2];
                            UPC_E[0] = OrignalUPCCode;
                            UPC_E[1] = Functions.GetUPC_E(UPC_E[0].ToString());
                            Productdata = UPCScanner(UPC_E[1]);
                        }
                        #endregion

                        #region Product Label
                        if (Productdata == 0)
                        {
                            Productdata = AddLabeledPrice(txtUPCCode.Text);
                        }
                        #endregion

                        if (Productdata == 0)
                        {
                            Clear();
                            ClsCommon.MsgBox("Information", "Product not found.!", false);
                        }

                        txtUPCCode.Text = "";
                        txtUPCCode.Focus();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Clear();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        public decimal UPCScanner(string UPCCode)
        {
            try
            {
                decimal _SellPrice = 0;
                long _ProductID;
                DataTable dt = new DataTable();
                if (LoginInfo.Connections)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var query1 = (from PM in _db.tbl_ProductMaster.Where(o => o.UPCCode == UPCCode)
                                  select new
                                  {
                                      PM.ProductID,
                                      PM.Price,
                                      PM.ProductName,
                                      PM.CaseQty,
                                      PM.CasePrice,
                                      PM.GroupQty,
                                      PM.GroupPrice,
                                  }).ToList();

                    dt = ClsCommon.LinqToDataTable(query1);

                }
                #region Local Connection

                string query = "SELECT ProductID,Price,ProductName,CaseQty,CasePrice,GroupQty,GroupPrice FROM tbl_ProductMaster AS PM WHERE UPCCode=@UPCCode";
                DataAdapter = new SqlCeDataAdapter(query, conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPCCode);
                DataAdapter.Fill(dt);

                #endregion

                if (dt.Rows.Count > 0)
                {
                    _SellPrice = Functions.GetDecimal(dt.Rows[0]["Price"].ToString());
                    _ProductID = Functions.GetLong(dt.Rows[0]["ProductID"].ToString());
                    lblDescription.Visible = true;
                    lblDesc.Visible = true;
                    lblDesc.Text = dt.Rows[0]["ProductName"].ToString();
                    lblUPCCode.Visible = true;
                    lblUPC.Visible = true;
                    lblUPC.Text = UPCCode;
                    lblCaseQtyVal.Visible = true;
                    lblCaseQty.Visible = true;
                    lblCaseQty.Text = dt.Rows[0]["CaseQty"].ToString() == "" ? "-" : dt.Rows[0]["CaseQty"].ToString();
                    lblCasePriceVal.Visible = true;
                    lblCasePrice.Visible = true;
                    lblCasePrice.Text = dt.Rows[0]["CasePrice"].ToString() == "" ? "-" : "$ " + dt.Rows[0]["CasePrice"].ToString();
                    lblGrpQtyVal.Visible = true;
                    lblGrpQty.Visible = true;
                    lblGrpQty.Text = dt.Rows[0]["GroupQty"].ToString() == "" ? "-" : dt.Rows[0]["GroupQty"].ToString();
                    lblGrpPriceVal.Visible = true;
                    lblGrpPrice.Visible = true;
                    lblGrpPrice.Text = dt.Rows[0]["GroupPrice"].ToString() == "" ? "-" : "$ " + dt.Rows[0]["GroupPrice"].ToString();
                    lblPriceVal.Visible = true;
                    lblPrice.Visible = true;
                    lblPrice.Text = _SellPrice.ToString();

                    dt.Dispose();
                    #region Search
                    //dt = new DataTable();
                    //query = "SELECT SellPrice FROM tbl_ProductSalePriceMaster WHERE ProductID = @ProductID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                    //DataAdapter = new SqlCeDataAdapter(query, conn);
                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID_", _ProductID);
                    //DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                    //DataAdapter.Fill(dt);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                    //}
                    #endregion
                }

                return _SellPrice;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
                return 0;
            }
        }

        public void Clear()
        {
            lblDescription.Visible = true;
            lblDesc.Visible = true;
            lblDesc.Text = "";
            lblUPCCode.Visible = true;
            lblUPC.Visible = true;
            lblUPC.Text = "";
            lblCaseQtyVal.Visible = true;
            lblCaseQty.Visible = true;
            lblCaseQty.Text = "";
            lblCasePriceVal.Visible = true;
            lblCasePrice.Visible = true;
            lblCasePrice.Text = "";
            lblGrpQtyVal.Visible = true;
            lblGrpQty.Visible = true;
            lblGrpQty.Text = "";
            lblGrpPriceVal.Visible = true;
            lblGrpPrice.Visible = true;
            lblGrpPrice.Text = "";
            lblPriceVal.Visible = true;
            lblPrice.Visible = true;
            lblPrice.Text = "";
        }

        public decimal AddLabeledPrice(string UPCCode)
        {
            try
            {
                decimal _SellPrice = 0;
                string TempUPCCode = UPCCode;
                string LastPrice = UPCCode.Substring(UPCCode.Length - 5, 5);
                TempUPCCode = UPCCode.Replace(LastPrice, "00000");
                bool _LabeledPrice;

                string _UPCCode = TempUPCCode;
                long _ProductID = 0;

                DataTable dt = new DataTable();
                if (LoginInfo.Connections)
                {
                    DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    var query1 = (from PM in _db.tbl_ProductMaster.Where(o => o.UPCCode == UPCCode)
                                  select new
                                  {
                                      PM.ProductID,
                                      PM.LabeledPrice,
                                      PM.ProductName,
                                  }).ToList();

                    dt = ClsCommon.LinqToDataTable(query1);
                }
                else
                {
                    string query = "SELECT ProductID,LabeledPrice,ProductName FROM tbl_ProductMaster AS PM " +
                                   "WHERE UPCCode=@UPCCode";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", _UPCCode);
                    DataAdapter.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    _ProductID = Functions.GetLong(dt.Rows[0]["ProductID"].ToString());

                    _LabeledPrice = (!String.IsNullOrEmpty(dt.Rows[0]["LabeledPrice"].ToString()) ? Convert.ToBoolean(dt.Rows[0]["LabeledPrice"].ToString()) : false);

                    if (_LabeledPrice == true)
                    {
                        #region Search

                        int FirstChar = Convert.ToInt32(LastPrice.Substring(0, 1));
                        FirstChar = FirstChar - 1;
                        LastPrice = LastPrice.Remove(0, 1);
                        LastPrice = LastPrice.Insert(LastPrice.Length - FirstChar, ".");
                        _SellPrice = Functions.GetDecimal(LastPrice);

                        lblDescription.Visible = true;
                        lblDesc.Visible = true;
                        lblDesc.Text = dt.Rows[0]["ProductName"].ToString();
                        lblUPCCode.Visible = true;
                        lblUPC.Visible = true;
                        lblUPC.Text = UPCCode;
                        lblPriceVal.Visible = true;
                        lblPrice.Visible = true;

                        lblPrice.Text = _SellPrice.ToString();
                        #endregion
                    }
                }
                return _SellPrice;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
                return 0;
            }
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {

                    IsPriceCheckClose = true;
                    OnMyEvent(this, new EventArgs());
                    this.Hide();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
