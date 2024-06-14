using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SFPOSWindows.Reports
{
    public partial class FrmDepositVerification : Form
    {

        public static decimal S_Cash = 0;
        public static decimal S_CreditCard = 0;
        public static decimal S_Check = 0;
        public static decimal S_CashPayout = 0;
        public static decimal R_Cash = 0;
        public static decimal R_CreditCard = 0;
        public static decimal R_Check = 0;
        public static decimal R_CashPayout = 0;
        DepositeVerificationservice objDepositeVerification = new DepositeVerificationservice();


        ErrorProvider ep = new ErrorProvider();
        public FrmDepositVerification()
        {
            InitializeComponent();
            cleardata();
        }

        public void cleardata()
        {
            S_Cash = 0;
            S_CreditCard = 0;
            S_Check = 0;
            S_CashPayout = 0;
            R_Cash = 0;
            R_CreditCard = 0;
            R_Check = 0;
            R_CashPayout = 0;
        }

        private void FrmAddReport_Load(object sender, EventArgs e)
        {
            GetDate();
        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
            GetDate();
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GetSystemReportData(long EmployeeID, DateTime date)
        {
            try
            {
                ReportStatusModel _ReportStatusModel = new ReportStatusModel();
                ReportReq _ReportReq = new ReportReq();
                _ReportReq.EmployeeID = 0;
                _ReportReq.Date = date;
                //List<ReportStatusModel> lst = ClsCommon.getdata(_ReportStatusModel, "SP_TillStatusEmployeeReport_System", _ReportReq);
                List<ReportStatusModel> lst = objDepositeVerification.GetSystemReportData(EmployeeID, date);
                if (lst != null && lst.Count > 0)
                {
                    _ReportStatusModel = lst.FirstOrDefault();
                    if (_ReportStatusModel != null)
                    {
                        S_Cash = !String.IsNullOrEmpty(_ReportStatusModel.CashAmount.ToString()) ? _ReportStatusModel.CashAmount : 0;
                        S_CreditCard = _ReportStatusModel.CreditCardAmount + _ReportStatusModel.FoodStampAmount;
                        S_Check = !String.IsNullOrEmpty(_ReportStatusModel.CheckAmount.ToString()) ? _ReportStatusModel.CheckAmount : 0;
                    }
                }
                else
                {
                    // Clear();
                    cleardata();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void GetRegisterReportData(DateTime date)
        {
            try
            {
                RegisterReportDataModel _RegisterReportDataModel = new RegisterReportDataModel();
                ReportReq _ReportReq = new ReportReq();
                _ReportReq.EmployeeID = 0;
                _ReportReq.Date = date;
                //List<RegisterReportDataModel> lst = ClsCommon.getdata(_RegisterReportDataModel, "SP_TillStatusEmployeeReportsTotal", _ReportReq);
                List<RegisterReportDataModel> lst = objDepositeVerification.GetRegisterReportData(date);
                if (lst != null && lst.Count > 0)
                {
                    _RegisterReportDataModel = new RegisterReportDataModel();
                    _RegisterReportDataModel = lst.FirstOrDefault();

                    if (_RegisterReportDataModel != null)
                    {
                        R_Cash = !String.IsNullOrEmpty(_RegisterReportDataModel.CashTotalAmount.ToString()) ? _RegisterReportDataModel.CashTotalAmount : 0;
                        R_CreditCard = !String.IsNullOrEmpty(_RegisterReportDataModel.CreditCardTotalAmount.ToString()) ? _RegisterReportDataModel.CreditCardTotalAmount : 0;
                        R_Check = !String.IsNullOrEmpty(_RegisterReportDataModel.CheckTotalAmount.ToString()) ? _RegisterReportDataModel.CheckTotalAmount : 0;
                        R_CashPayout = !String.IsNullOrEmpty(_RegisterReportDataModel.CashPayoutTotalAmount.ToString()) ? _RegisterReportDataModel.CashPayoutTotalAmount : 0;
                    }
                }
                else
                {
                    // Clear();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void Clear()
        {
            ep.Clear();
        }

        public void GetDate()
        {
            GetSystemReportData(0, Convert.ToDateTime(txtDate.Text));
            GetRegisterReportData(Convert.ToDateTime(txtDate.Text));
            Cal();
        }

        public void Cal()
        {
            try
            {
                lblDate.Text = txtDate.Text;
                txtS_Cash.Text = "$" + S_Cash.ToString();
                txtS_Check.Text = "$" + S_Check.ToString();
                txtS_CreaditCard.Text = "$" + S_CreditCard.ToString();
                txtDTCashPayout.Text = "$" + S_CashPayout.ToString();
                txtSystemTotal.Text = "$" + (S_Cash + S_Check + S_CreditCard).ToString();

                txtB_Cash.Text = "$" + R_Cash.ToString();
                txtB_Check.Text = "$" + R_Check.ToString();
                txtB_CreaditCard.Text = "$" + R_CreditCard.ToString();
                txtACashPayout.Text = "$" + R_CashPayout.ToString();
                txtBalanceTotal.Text = "$" + (R_Cash + R_Check + R_CreditCard + R_CashPayout).ToString();

                txtD_Cash.Text = "$" + (S_Cash - R_Cash).ToString();
                txtD_Check.Text = "$" + (S_Check - R_Check).ToString();
                txtD_Creditcard.Text = "$" + (S_CreditCard - R_CreditCard).ToString();
                txtDCashPayout.Text = "$" + (S_CashPayout - R_CashPayout).ToString();
                txtdifftotal.Text = "$" + ((S_Cash - R_Cash) + (S_Check - R_Check) + (S_CreditCard - R_CreditCard) + (S_CashPayout - R_CashPayout)).ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void metroPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
