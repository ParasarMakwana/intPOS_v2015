using Microsoft.PointOfService;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SFPOSWindows.CustomControl
{
    public partial class UCAgeVerification : UserControl
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public bool IsAgeDone = false;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        private PosExplorer myPosExplorer;

        public UCAgeVerification()
        {
            InitializeComponent();
            //myPosExplorer = new PosExplorer(this);

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
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return age;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Escape))
                {
                    IsAgeDone = false;
                    this.Hide();
                    OnMyEvent(this, new EventArgs());
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void VerifyAge()
        {
            AgeVerifidInfo.AgeChecked = true;
            try
            {
                int age = CalculateAge(Convert.ToDateTime(Convert.ToDateTime(txtPassword.Text).ToString("MM/dd/yyyy")));

                if (AgeVerifidInfo.SectionAge > 0)
                {
                    if (age < AgeVerifidInfo.SectionAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        IsAgeDone = true;
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else if (AgeVerifidInfo.DepartmentAge > 0)
                {
                    if (age < AgeVerifidInfo.DepartmentAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        IsAgeDone = true;
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else if (AgeVerifidInfo.StoreAge > 0)
                {
                    if (age < AgeVerifidInfo.StoreAge)
                    {
                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                        AgeVerifidInfo.AgeVerified = false;
                    }
                    else
                    {
                        AgeVerifidInfo.AgeVerified = true;
                        IsAgeDone = true;
                        this.Hide();
                        OnMyEvent(this, new EventArgs());
                    }
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    AgeVerifidInfo.AgeVerified = false;
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                AgeVerifidInfo.AgeVerified = false;
                txtPassword.Text = "";
                txtPassword.Focus();
                //_ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                VerifyAge();
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                AgeVerifidInfo.AgeVerified = false;
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                AgeVerifidInfo.AgeVerified = false;
                IsAgeDone = true;
                OnMyEvent(this, new EventArgs());
                this.Hide();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        private void UCAgeVerification_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            
        }

        private void dtBirthdate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Length == 2)
                {
                    if (Regex.IsMatch(this.txtPassword.Text, CommonModelCont.NumericOnetoNine_Validation))
                    {
                        txtPassword.Text = txtPassword.Text + "/";
                    }
                }
                else if (txtPassword.Text.Length == 5)
                {
                    if (Regex.IsMatch(this.txtPassword.Text, @"^\d+\/\d+$"))
                    {
                        txtPassword.Text = txtPassword.Text + "/";
                    }
                }
                txtPassword.SelectionStart = txtPassword.Text.Length;
                txtPassword.SelectionLength = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
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
                        if (txtPassword.Text.Length == 10)
                        {
                            if (txtPassword.Text.Trim().ToLower().Contains("cl"))
                            {
                                txtPassword.Text = "";
                                txtPassword.Focus();
                            }
                            else
                            {
                                int age = 0;
                                if (CheckDate(txtPassword.Text))
                                    age = CalculateAge(Convert.ToDateTime(Convert.ToDateTime(txtPassword.Text).ToString("MM/dd/yyyy")));
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                                    AgeVerifidInfo.AgeVerified = false;
                                    txtPassword.Text = "";
                                    txtPassword.Focus();
                                    return;
                                }
                                if (AgeVerifidInfo.SectionAge > 0)
                                {
                                    if (age < AgeVerifidInfo.SectionAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        txtPassword.Text = "";
                                        txtPassword.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        IsAgeDone = true;
                                        this.Hide();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else if (AgeVerifidInfo.DepartmentAge > 0)
                                {
                                    if (age < AgeVerifidInfo.DepartmentAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        txtPassword.Text = "";
                                        txtPassword.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        IsAgeDone = true;
                                        this.Hide();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else if (AgeVerifidInfo.StoreAge > 0)
                                {
                                    if (age < AgeVerifidInfo.StoreAge)
                                    {
                                        ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                        txtPassword.Text = "";
                                        txtPassword.Focus();
                                        AgeVerifidInfo.AgeVerified = false;
                                    }
                                    else
                                    {
                                        AgeVerifidInfo.AgeVerified = true;
                                        IsAgeDone = true;
                                        this.Hide();
                                        OnMyEvent(this, new EventArgs());
                                    }
                                }
                                else
                                {
                                    ClsCommon.MsgBox("Information", "Can't sell this product because age is not valid !!!", false);
                                    txtPassword.Text = "";
                                    txtPassword.Focus();
                                    AgeVerifidInfo.AgeVerified = false;
                                }
                            }
                        }

                        if (txtPassword.Text.Trim().ToLower().Contains("cl"))
                        {
                            txtPassword.Text = "";
                            txtPassword.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsCommon.MsgBox("Information", "Date format is not correct.", false);
                        AgeVerifidInfo.AgeVerified = false;

                        _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "***Date format(" + txtPassword.Text + ")***" + CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmAgeVerification + ex.StackTrace, ex.LineNumber());
            }
        }

        private void dtBirthdate_Click(object sender, EventArgs e)
        {

        }

        private void UCAgeVerification_MouseHover(object sender, EventArgs e)
        {

        }
    }
}
