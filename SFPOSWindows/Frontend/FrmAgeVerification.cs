using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmAgeVerification : MetroForm
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public bool IsAgeDone = false;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public FrmAgeVerification()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AgeVerifidInfo.AgeChecked = true;
            try
            {

                int age = CalculateAge(Convert.ToDateTime(Convert.ToDateTime(dtBirthdate.Text).ToString("MM/dd/yyyy")));

                if (AgeVerifidInfo.SectionAge > 0)
                {
                    if (age < AgeVerifidInfo.SectionAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        dtBirthdate.Text = "";
                        dtBirthdate.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        this.Close();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else if (AgeVerifidInfo.DepartmentAge > 0)
                {
                    if (age < AgeVerifidInfo.DepartmentAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        dtBirthdate.Text = "";
                        dtBirthdate.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        this.Close();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else if (AgeVerifidInfo.StoreAge > 0)
                {
                    if (age < AgeVerifidInfo.StoreAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        dtBirthdate.Text = "";
                        dtBirthdate.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        this.Close();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                    dtBirthdate.Text = "";
                    dtBirthdate.Focus();
                    AgeVerifidInfo.AgeVerified = false;
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                AgeVerifidInfo.AgeVerified = false;
                dtBirthdate.Text = "";
                dtBirthdate.Focus();
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            try
            {
                age = DateTime.Now.Year - dateOfBirth.Year;
                if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                    age = age - 1;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return age;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                AgeVerifidInfo.AgeVerified = false;
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        private void FrmAgeVerification_Load(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void dtBirthdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtBirthdate.Text.Length == 2)
                {
                    if (Regex.IsMatch(this.dtBirthdate.Text, CommonModelCont.NumericOnetoNine_Validation))
                    {
                        dtBirthdate.Text = dtBirthdate.Text + "/";
                    }
                }
                else if (dtBirthdate.Text.Length == 5)
                {
                    if (Regex.IsMatch(this.dtBirthdate.Text, @"^\d+\/\d+$"))
                    {
                        dtBirthdate.Text = dtBirthdate.Text + "/";
                    }
                }
                dtBirthdate.SelectionStart = dtBirthdate.Text.Length;
                dtBirthdate.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        protected bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dtBirthdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 'C' && e.KeyChar != 'L' && e.KeyChar != 'c' && e.KeyChar != 'l')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == (char)13)
                {
                    AgeVerifidInfo.AgeChecked = true;
                    try
                    {
                        if (dtBirthdate.Text.Length == 10)
                        {
                            if (dtBirthdate.Text.Trim().ToLower().Contains("cl"))
                            {
                                dtBirthdate.Text = "";
                                dtBirthdate.Focus();
                            }
                            else
                            {
                                int age = 0;
                                if (CheckDate(dtBirthdate.Text))
                                    age = CalculateAge(Convert.ToDateTime(Convert.ToDateTime(dtBirthdate.Text).ToString("MM/dd/yyyy")));
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                                    AgeVerifidInfo.AgeVerified = false;
                                    dtBirthdate.Text = "";
                                    dtBirthdate.Focus();
                                    return;
                                }
                                if (AgeVerifidInfo.SectionAge > 0)
                                {
                                    if (age < AgeVerifidInfo.SectionAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        dtBirthdate.Text = "";
                                        dtBirthdate.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        this.Close();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else if (AgeVerifidInfo.DepartmentAge > 0)
                                {
                                    if (age < AgeVerifidInfo.DepartmentAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        dtBirthdate.Text = "";
                                        dtBirthdate.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        this.Close();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else if (AgeVerifidInfo.StoreAge > 0)
                                {
                                    if (age < AgeVerifidInfo.StoreAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        dtBirthdate.Text = "";
                                        dtBirthdate.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        this.Close();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                    dtBirthdate.Text = "";
                                    dtBirthdate.Focus();
                                    AgeVerifidInfo.AgeVerified = false;
                                }
                            }
                        }

                        if (dtBirthdate.Text.Trim().ToLower().Contains("cl"))
                        {
                            dtBirthdate.Text = "";
                            dtBirthdate.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                        AgeVerifidInfo.AgeVerified = false;

                        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", "***Date format(" + dtBirthdate.Text + ")***" + CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
                        dtBirthdate.Text = "";
                        dtBirthdate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message + "(" + ex.InnerException + ")", CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
