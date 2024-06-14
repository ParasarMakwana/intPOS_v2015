using MetroFramework.Forms;
using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;

namespace SFPOSWindows.RDLCReports
{
    public partial class ReportUPCCodeFilter : MetroForm
    {
        public int FormNo = 0;
        public DateTime StartDate;
        public DateTime EndDate;
        public string FilterVal = "";
        public int FilterMonth = 0;
        string UPCCode = String.Empty;
        private PosExplorer myPosExplorer;
        private Scanner myScanner;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();

        public ReportUPCCodeFilter()
        {
            InitializeComponent();
            myPosExplorer = new PosExplorer(this);
            DeviceAdd();
        }
        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            AutoProductMovement();
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
            txtUPC.Text = UPCCode;
            SearchProduct(txtUPC.Text.Trim());
            if (myScanner.DataEventEnabled == false)
            {
                myScanner.DataEventEnabled = true;
            }
        }

        private void txtUPC_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    SearchProduct(txtUPC.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmManagerPassWord + ex.StackTrace, ex.LineNumber());
            }

        }

        #endregion

        #region Functions

        public void formOpen(int FormNo)
        {
            switch (FormNo)
            {
                case 13:
                    FrmProductMovement_Print objFrmProductMovement_Print = new FrmProductMovement_Print();
                    objFrmProductMovement_Print.FromDate = StartDate;
                    objFrmProductMovement_Print.ToDate = EndDate;
                    objFrmProductMovement_Print.FilterVal = FilterVal;
                    objFrmProductMovement_Print.monthNumber = FilterMonth;
                    objFrmProductMovement_Print.Show();
                    objFrmProductMovement_Print.TopMost = true;
                    break;
            }
        }
        public void AutoProductMovement()
        {
            FilterVal = txtUPC.Text.Trim();
            this.Close();
            formOpen(FormNo);
        }

        public void DeviceRemove()
        {
            try
            {
                if (XMLData.ScannerInUsed)
                {
                    if (myScanner.DataEventEnabled == true)
                    {
                        myScanner.DataEventEnabled = false;
                        myScanner.DeviceEnabled = false;
                        myScanner.Release();
                        myScanner.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DeviceAdd()
        {
            try
            {
                if (XMLData.ScannerInUsed)
                {
                    var deviceCollection = myPosExplorer.GetDevices(DeviceType.Scanner);
                    foreach (DeviceInfo deviceInfo in deviceCollection)
                    {
                        if (deviceInfo.ServiceObjectName == XMLData.Scanner)
                        {
                            myScanner = (Scanner)myPosExplorer.CreateInstance(deviceInfo);
                            if (myScanner.DataEventEnabled == false)
                            {
                                myScanner.Open();
                                myScanner.Claim(1000);
                                myScanner.DataEvent += myScanner_DataEvent;
                                myScanner.DeviceEnabled = true;
                                myScanner.DataEventEnabled = true;
                                myScanner.DecodeData = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        public void SearchProduct(string searchStr)
        {
            try
            {
                UPCCode = searchStr;
                if (searchStr != "" && searchStr != null)
                {
                    if (searchStr.All(char.IsDigit))
                    {
                        int Count = searchStr.Length;
                        if (Count < 13)
                        {
                            Count = 13 - Count;
                            for (int i = 0; i < Count; i++)
                            {
                                searchStr = "0" + searchStr;
                            }
                        }
                    }
                    txtUPC.Text = searchStr;
                    AutoProductMovement();
                }
                //MessageBox.Show(searchStr);
            }
            catch (Exception ex)
            {
                // _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "FrmLabelPrint" + ex.StackTrace, ex.LineNumber());
            }
        }
        #endregion

    }
}
