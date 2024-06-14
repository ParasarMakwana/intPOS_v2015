using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class frmPriceCheck : MetroForm
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
        private void frmPriceCheck_Load(object sender, EventArgs e)
        {
            try
            {
                var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                foreach (DeviceInfo deviceInfo in deviceCollection)
                {
                    if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                    {
                        myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                        myScanner.Open();
                        myScanner.Claim(1000);
                        myScanner.DataEvent += myScanner_DataEvent;
                        myScanner.DeviceEnabled = true;
                        myScanner.DataEventEnabled = true;
                        myScanner.DecodeData = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        void myScanner_DataEvent(object sender, DataEventArgs e)
        {
            ASCIIEncoding myEncoding = new ASCIIEncoding();
            string UPCCode = (myEncoding.GetString(myScanner.ScanDataLabel));
            if (UPCCode.Length > 1)
            {
                if (myScanner.ScanDataType != BarCodeSymbology.Code39)
                    UPCCode = UPCCode.Substring(0, UPCCode.Length - 1);
            }
            txtUPCCode.Text = UPCCode;
            myScanner.DataEventEnabled = true;
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        private void frmProductDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DeviceRemove();
                //PortOpen_Close(false);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DeviceRemove();
                //PortOpen_Close(false);
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

        #region Functions
        public frmPriceCheck()
        {
            InitializeComponent();
            //ComPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived_1);
            //OpenPort();
            myPosExplorer = new PosExplorer(this);
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
                        txtUPCCode.Focus();
                    }
                    #endregion

                    #region cn
                    else if (txtUPCCode.Text.ToLower().Contains("cn"))
                    {
                        this.Close();
                        OnMyEvent(this, new EventArgs());
                    }
                    #endregion

                    #region 4 Digit Search
                    else
                    {
                        string OrignalUPCCode = txtUPCCode.Text.Trim();
                        int Count = txtUPCCode.Text.Length;

                        Count = 13 - Count;
                        for (int i = 0; i < Count; i++)
                        {
                            txtUPCCode.Text = "0" + txtUPCCode.Text;
                        }

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
        }

        public DataTable GetProduct(string UPCCode)
        {
            DataTable dt = new DataTable();
            try
            {
                SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                if (LoginInfo.Connections)
                {
                    var query = from PM in _db.tbl_ProductMaster
                                where PM.UPCCode == UPCCode
                                select PM;

                    dt = ClsCommon.LinqToDataTable(query);
                }
                else
                {
                    string query = "SELECT ProductID,Price,ProductName,CaseQty,CasePrice,GroupQty,GroupPrice FROM tbl_ProductMaster AS PM WHERE UPCCode=@UPCCode";
                    DataAdapter = new SqlCeDataAdapter(query, conn);
                    DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", UPCCode);
                    DataAdapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
                return dt;
            }
        }

        public decimal UPCScanner(string UPCCode)
        {
            try
            {
                #region Local Connetion

                decimal _SellPrice = 0;
                long _ProductID;
                DataTable dt = new DataTable();
                dt = GetProduct(UPCCode);
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

                    #region Search
                    dt = new DataTable();
                    if (LoginInfo.Connections)
                    {
                        SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                        var query = from PS in _db.tbl_ProductSalePriceMaster
                                    where PS.ProductID == _ProductID && PS.StartDate == DateTime.Now && PS.EndDate == DateTime.Now
                                    select PS.SellPrice;
                        dt = ClsCommon.LinqToDataTable(query);
                    }
                    else
                    {
                        string query = "SELECT SellPrice FROM tbl_ProductSalePriceMaster WHERE ProductID = @ProductID_ AND StartDate <= @Date AND EndDate >= @Date ORDER BY StartDate,EndDate";
                        DataAdapter = new SqlCeDataAdapter(query, conn);
                        DataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID_", _ProductID);
                        DataAdapter.SelectCommand.Parameters.AddWithValue("@Date", DateTime.Now);
                        DataAdapter.Fill(dt);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        _SellPrice = Functions.GetDecimal(dt.Rows[0]["SellPrice"].ToString());
                    }

                    lblPriceVal.Visible = true;
                    lblPrice.Visible = true;

                    lblPrice.Text = _SellPrice.ToString();
                    dt.Dispose();
                    #endregion
                }

                dt.Dispose();
                conn.Close();
                #endregion
                return _SellPrice;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
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
                //string query = "SELECT ProductID,LabeledPrice,ProductName FROM tbl_ProductMaster AS PM " +
                //               "WHERE UPCCode=@UPCCode";
                //DataAdapter = new SqlCeDataAdapter(query, conn);
                //DataAdapter.SelectCommand.Parameters.AddWithValue("@UPCCode", _UPCCode);
                //DataAdapter.Fill(dt);

                dt = GetProduct(_UPCCode);
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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
                return 0;
            }
        }

        public void DeviceRemove()
        {
            try
            {
                myScanner.DataEventEnabled = false;
                myScanner.DeviceEnabled = false;
                myScanner.Release();
                myScanner.Close();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    this.Close();
                    OnMyEvent(this, new EventArgs());
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.frmProductDetail + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
