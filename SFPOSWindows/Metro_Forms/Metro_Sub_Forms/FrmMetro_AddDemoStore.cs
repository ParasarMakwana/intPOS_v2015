using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Linq;
using SFPOS.DAL;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddDemoStore : MetroForm
    {
        #region Properties
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        private string connectionString = "Data Source = 71.95.23.110; Initial Catalog = intPOS_Key;User ID=POSAdmin; Pwd=POS@123; Integrated Security=False";
        //private string connectionString = "Data Source =tbs24; Initial Catalog = intPOSKeyTest1;User ID=sa; Pwd=sa@123; Integrated Security=False";
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public long PrimaryId = 0;
        public bool IsSuccess = false;
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        #endregion

        #region Events

        private long UpdateCityName(string CityName, long StateID)
        {
            long CityID = 1;
            try
            {
                var city = _db.tbl_City.Where(x => x.CityName == CityName).FirstOrDefault();
                if (city != null && city.CityID != 0)
                {
                    CityID = city.CityID;
                }
                else
                {
                    tbl_City citymodel = new tbl_City();
                    citymodel.CityName = CityName;
                    citymodel.StateID = StateID;
                    _db.tbl_City.Add(citymodel);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }

            return CityID;
        }

        private long UpdateStateName(string StateName, long CountryID)
        {
            long StateID = 1;
            try
            {
                var state = _db.tbl_State.Where(x => x.StateName == StateName).FirstOrDefault();
                if (state != null && state.StateID != 0)
                {
                    StateID = state.StateID;
                }
                else
                {
                    tbl_State statemodel = new tbl_State();
                    statemodel.StateName = StateName;
                    statemodel.CountryID = CountryID;
                    _db.tbl_State.Add(statemodel);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }

            return StateID;
        }
        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    Boolean IsExistEmail = false;
                    IsExistEmail = CheckEmailIDLicensekey(txtEmail.Text.Trim());
                    if (IsExistEmail)
                    {
                        ClsCommon.MsgBox("Information", "This email id is already registered, Please enter different email id!", false);
                    }
                    else
                    {
                        StoreService _StoreService = new StoreService();
                        bool IsStore = _StoreService.CheckStoreName(txtStoreName.Text.Trim(), PrimaryId);
                        if (!IsStore)
                        {
                            GenerateNewKey();

                            long _CountryID = Convert.ToInt64(cmbCountry.SelectedValue);
                            long _StateID = UpdateStateName(txtState.Text, _CountryID);
                            long _CityID = UpdateCityName(txtCity.Text, _StateID);

                            StoreMasterModel objStoreMasterModel = new StoreMasterModel();

                            objStoreMasterModel.StoreName = txtStoreName.Text;
                            objStoreMasterModel.Address = txtAddress.Text;
                            objStoreMasterModel.Address2 = txtAddress2.Text;
                            objStoreMasterModel.City = _CityID;
                            objStoreMasterModel.State = _StateID;
                            objStoreMasterModel.ZipCode = txtZipcode.Text.Trim();
                            objStoreMasterModel.Country = _CountryID;
                            objStoreMasterModel.Phone = txtPhone.Text.Trim();
                            objStoreMasterModel.Fax = txtEmail.Text.Trim();
                            objStoreMasterModel.DefaultTax = 9;
                            objStoreMasterModel.Disclaimer = "Thank you for shopping at " + txtStoreName.Text + "\n  Refund/Exchange within 24 hrs. w/Receipt\n  No Refund Or Exchange On Any Liquor\n  All Electronics refundable/exchange\n  within 2 weeks only. No exceptions.\n  Have a Nice Day !!!!!!!!";
                            objStoreMasterModel.IsStoreTax = true;
                            objStoreMasterModel.AgeVarificationAge = 21;

                            var add = _StoreService.AddStore(objStoreMasterModel, 1);
                            if (add != null)
                            {
                                //ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }

                            UpdateLog();
                        }
                        else
                        {
                            GenerateNewKey();

                            StoreMasterModel objStoreMasterModel = new StoreMasterModel();
                            tbl_StoreMaster objtbl_StoreMaster = new tbl_StoreMaster();
                            objtbl_StoreMaster = _db.tbl_StoreMaster.Where(p => p.StoreID == objStoreMasterModel.StoreID).FirstOrDefault();

                            objStoreMasterModel.StoreID = objtbl_StoreMaster.StoreID;
                            objStoreMasterModel.StoreName = txtStoreName.Text;
                            objStoreMasterModel.Address = txtAddress.Text;
                            objStoreMasterModel.Address2 = txtAddress2.Text;
                            //objStoreMasterModel.City = Convert.ToInt64(txtCity.SelectedValue);
                            //objStoreMasterModel.State = Convert.ToInt64(txtState.SelectedValue);
                            objStoreMasterModel.ZipCode = txtZipcode.Text.Trim();
                            objStoreMasterModel.Country = Convert.ToInt64(cmbCountry.SelectedValue);
                            objStoreMasterModel.Phone = txtPhone.Text.Trim();
                            objStoreMasterModel.Fax = txtEmail.Text.Trim();
                            objStoreMasterModel.DefaultTax = 9;
                            objStoreMasterModel.Disclaimer = "Thank you for shopping at " + txtStoreName.Text + "\n  Refund/Exchange within 24 hrs. w/Receipt\n  No Refund Or Exchange On Any Liquor\n  All Electronics refundable/exchange\n  within 2 weeks only. No exceptions.\n  Have a Nice Day !!!!!!!!";
                            objStoreMasterModel.IsStoreTax = true;
                            objStoreMasterModel.AgeVarificationAge = 21;

                            var add = _StoreService.AddStore(objStoreMasterModel, 2);
                            if (add != null)
                            {
                                // ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                            }
                            else
                            {
                                ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                            }

                            UpdateLog();
                        }
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private Boolean CheckEmailIDLicensekey(string EmailID)
        {
            Boolean IsExistEmail = false;
            try
            {
                 using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query1 = "select Phone, Email from tbl_StoreDetails where Email = @Email";
                    SqlCommand sqlCmd = new SqlCommand(query1, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Email", EmailID);
                    SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    adp.Fill(dt);
                    sqlCon.Close();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        IsExistEmail = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
            return IsExistEmail;
        }

        private void GenerateNewKey()
        {
            try
            {
                StoreDetailModel _storeDetailModel = new StoreDetailModel();
                _storeDetailModel.StoreName = txtStoreName.Text;
                _storeDetailModel.PersonName = txtpersonName.Text;
                _storeDetailModel.Address = txtAddress.Text + " " + txtAddress2.Text;
                _storeDetailModel.Phone = txtPhone.Text;
                _storeDetailModel.Email = txtEmail.Text;
                _storeDetailModel.City = txtCity.Text;
                _storeDetailModel.State = txtState.Text;
                _storeDetailModel.Country = cmbCountry.Text;
                _storeDetailModel.ZipCode = txtZipcode.Text;

                string NewKey = GenerateKey();


                int result = 0;
                if (NewKey != string.Empty)
                {
                     using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string query1 = "GenerateNewKey";
                        SqlCommand sqlCmd = new SqlCommand(query1, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@StoreName", _storeDetailModel.StoreName);
                        sqlCmd.Parameters.AddWithValue("@PersonName", _storeDetailModel.PersonName);
                        sqlCmd.Parameters.AddWithValue("@Address", _storeDetailModel.Address);
                        sqlCmd.Parameters.AddWithValue("@Phone", _storeDetailModel.Phone);
                        sqlCmd.Parameters.AddWithValue("@Email", _storeDetailModel.Email);
                        sqlCmd.Parameters.AddWithValue("@City", _storeDetailModel.City);
                        sqlCmd.Parameters.AddWithValue("@State", _storeDetailModel.State);
                        sqlCmd.Parameters.AddWithValue("@Country", _storeDetailModel.Country);
                        sqlCmd.Parameters.AddWithValue("@ZipCode", _storeDetailModel.ZipCode);
                        sqlCmd.Parameters.AddWithValue("@GeneratedKey", NewKey);
                        sqlCmd.Parameters.Add("@success", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.ExecuteNonQuery();
                        result = Convert.ToInt32(sqlCmd.Parameters["@success"].Value);
                        sqlCon.Close();
                    }
                }
                if (result > 0)
                {
                    SendLicenceKeybyEmail(NewKey, "info@intpos.net", "Qal12348", "smtp.office365.com", 587, _storeDetailModel);

                    ClsCommon.MsgBox("Information", "Your evaluation version license key is sent to given email, Please check your email.",false);

                    OpenFrmSettingForm();
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Key is not generated , Try after some time", false);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void SendLicenceKeybyEmail(string NewKey, string EmailFromID, string FromPassword, string SMTPDetails, int PortNo, StoreDetailModel storeDetail)
        {
            try
            {
                //string mailMessage = "Hi " + storeDetail.PersonName + ",\n\nThank you for your interest in intPOS"
                //    + "\n\n Instructions : Please launch intPOS Backend and activate with provided key first"
                //    + "\n\nYour 30 day evaluation key is : " + NewKey
                //    + "\n\nLogin detail\nUsername : 1212\nPassword : 1212"
                //+ "\n\nThanks,\n\nintPOS\nwww.intPOS.net";
                string mailMessage = $"Dear {storeDetail.PersonName},\n\n" +
                     "We hope this message finds you well. Thank you for expressing interest in intPOS.\n\n" +
                     "We are delighted to provide you with a 30-day evaluation key that will grant you access to the full suite of intPOS features. Your evaluation key is:\n\n" +
                      NewKey +
                     "\n\nBelow are your login details for intPOS:\n\n" +
                     "Username: 1212\n" +
                     "Password: 1212\n\n" +
                     "Instructions for a smooth start:\n" +
                     "Activation Process:\n\n" +
                     "1. First launch the intPOS Backend application.\n" +
                     "2. Navigate to the activation section.\n" +
                     "3. Enter the provided evaluation key("+NewKey+").\n\n" +
                     "Please make sure to activate intPOS with the provided key before exploring the extensive features it has to offer.\n\n" +
                     "For any assistance or inquiries during your evaluation period, our support team is ready to help. Reach out to us at support@intPOS.net.\n\n" +
                     "Thank you for choosing intPOS. We look forward to your experience with our solution.\n\n" +
                     "Best regards,\n\n" +
                     "intPOS Team\n" +
                     "www.intPOS.net";


                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFromID);
                message.To.Add(new MailAddress(storeDetail.Email));
                message.Subject = "intPOS evaluation key";
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = mailMessage;
                smtp.Port = PortNo;
                smtp.Host = SMTPDetails;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(EmailFromID, FromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }

            try
            {
                string Tomail = "info@intpos.net"; 

                string mailMessage = "Customer Details" 
                    + "\n\nCompany Name : " + storeDetail.StoreName
                    + "\nPerson Name  : " + storeDetail.PersonName
                    + "\nPhone Number : " + storeDetail.Phone
                    + "\nEmail        : " + storeDetail.Email
                    + "\nAddress      : " + storeDetail.Address
                    + "\nCity         : " + storeDetail.City
                    + "\nState        : " + storeDetail.State
                    + "\nCountry      : " + storeDetail.Country
                    + "\nZip Code     : " + storeDetail.ZipCode;

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(EmailFromID);
                message.To.Add(new MailAddress(Tomail));
                message.Subject = "intPOS evaluation customer registration detail";
                message.IsBodyHtml = false; //to make message body as html  
                message.Body = mailMessage;
                smtp.Port = PortNo;
                smtp.Host = SMTPDetails;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(EmailFromID, FromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private string GenerateKey()
        {
            string NewGeneratedKey = string.Empty;
            string NoofNode = "1", ExpYear = "15";
            try
            {
                Random rnd = new Random();
                string Key = "";

                string p1 = GenerateCoupon(4, rnd);
                string p2 = GenerateCoupon(4, rnd);
                string p3 = GetPOSNodes(NoofNode);
                string p4 = GetExpYear(ExpYear);

                Key = p1 + "-" + p2 + "-" + p3 + "-" + p4;
                Key = Decrypt(Key);
                GetPOSNodes_revers(Key);

                NewGeneratedKey = Key;
            }
            catch (Exception ex)
            {

            }
            return NewGeneratedKey;
        }

        private string GenerateCoupon(int length, Random random)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                string ch = characters[random.Next(characters.Length)].ToString();
                //result.Append(ch);
                if (i > 0 && i % 4 == 0)
                {
                    result.Append("-");
                    result.Append(ch);
                }
                else
                {
                    result.Append(ch);
                }
            }
            return result.ToString();
        }

        private string GetPOSNodes(string POSNodes)
        {
            Random rnd = new Random();
            string p3 = "";
            int len = POSNodes.Length;
            p3 = len.ToString();
            len = 3 - len;
            for (int i = 0; i < len; i++)
            {
                p3 = p3 + GetOneAlphabet(1, rnd);
            }
            p3 = p3 + POSNodes;

            return p3;
        }

        private string GetExpYear(string ExpYear)
        {
            Random rnd = new Random();
            string p4 = "";
            int len = ExpYear.Length;
            p4 = len.ToString();
            len = 3 - len;
            for (int i = 0; i < len; i++)
            {
                string temp = GetOneAlphabet(1, rnd);
                temp = GetOneAlphabet(1, rnd);
                p4 = p4 + GetOneAlphabet(1, rnd);
            }
            p4 = p4 + ExpYear;
            return p4;
        }

        private string GetOneAlphabet(int length, Random random)
        {
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                string ch = characters[random.Next(characters.Length)].ToString();
                if (i > 0 && i % 4 == 0)
                {
                    result.Append("-");
                    result.Append(ch);
                }
                else
                {
                    result.Append(ch);
                }
            }
            return result.ToString();
        }

        private string GetPOSNodes_revers(string Key)
        {
            //T5O5-07HG-GDIN-JYGH
            Random rnd = new Random();
            string Key_ = Key.Substring(10, 4);
            Key_ = Encryp(Key_);
            int lenth = Convert.ToInt32(Key_.Substring(0, 1));
            string node = Key_.Substring(Key_.Length - lenth, lenth);
            Key = node;
            return Key;
        }

        private string Encryp(string CodeKey)
        {
            string _CodeKey = "";
            string characters = CodeKey;
            char[] array = characters.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                string Char = array[i].ToString();
                switch (Char)
                {
                    #region Check 
                    case "H":
                        Char = "0";
                        break;
                    case "G":
                        Char = "1";
                        break;
                    case "J":
                        Char = "2";
                        break;
                    case "B":
                        Char = "3";
                        break;
                    case "C":
                        Char = "4";
                        break;
                    case "N":
                        Char = "5";
                        break;
                    case "F":
                        Char = "6";
                        break;
                    case "K":
                        Char = "7";
                        break;
                    case "O":
                        Char = "8";
                        break;
                    case "E":
                        Char = "9";
                        break;
                    case "P":
                        Char = "A";
                        break;
                    case "Y":
                        Char = "B";
                        break;
                    case "8":
                        Char = "C";
                        break;
                    case "4":
                        Char = "D";
                        break;
                    case "W":
                        Char = "E";
                        break;
                    case "U":
                        Char = "F";
                        break;
                    case "3":
                        Char = "G";
                        break;
                    case "I":
                        Char = "H";
                        break;
                    case "A":
                        Char = "I";
                        break;
                    case "D":
                        Char = "J";
                        break;
                    case "7":
                        Char = "K";
                        break;
                    case "0":
                        Char = "L";
                        break;
                    case "T":
                        Char = "M";
                        break;
                    case "9":
                        Char = "N";
                        break;
                    case "S":
                        Char = "O";
                        break;
                    case "2":
                        Char = "P";
                        break;
                    case "Z":
                        Char = "Q";
                        break;
                    case "X":
                        Char = "R";
                        break;
                    case "5":
                        Char = "S";
                        break;
                    case "1":
                        Char = "T";
                        break;
                    case "Q":
                        Char = "U";
                        break;
                    case "R":
                        Char = "V";
                        break;
                    case "6":
                        Char = "W";
                        break;
                    case "V":
                        Char = "X";
                        break;
                    case "M":
                        Char = "Y";
                        break;
                    case "L":
                        Char = "Z";
                        break;
                    case "-":
                        Char = "-";
                        break;
                    default:
                        Char = "*";
                        break;
                        #endregion
                }
                _CodeKey = _CodeKey + Char;
            }
            return _CodeKey;
        }

        private string Decrypt(string CodeKey)
        {
            string _CodeKey = "";
            string characters = CodeKey;
            char[] array = characters.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                string Char = array[i].ToString();
                switch (Char)
                {
                    #region Check 
                    case "0":
                        Char = "H";
                        break;
                    case "1":
                        Char = "G";
                        break;
                    case "2":
                        Char = "J";
                        break;
                    case "3":
                        Char = "B";
                        break;
                    case "4":
                        Char = "C";
                        break;
                    case "5":
                        Char = "N";
                        break;
                    case "6":
                        Char = "F";
                        break;
                    case "7":
                        Char = "K";
                        break;
                    case "8":
                        Char = "O";
                        break;
                    case "9":
                        Char = "E";
                        break;
                    case "A":
                        Char = "P";
                        break;
                    case "B":
                        Char = "Y";
                        break;
                    case "C":
                        Char = "8";
                        break;
                    case "D":
                        Char = "4";
                        break;
                    case "E":
                        Char = "W";
                        break;
                    case "F":
                        Char = "U";
                        break;
                    case "G":
                        Char = "3";
                        break;
                    case "H":
                        Char = "I";
                        break;
                    case "I":
                        Char = "A";
                        break;
                    case "J":
                        Char = "D";
                        break;
                    case "K":
                        Char = "7";
                        break;
                    case "L":
                        Char = "0";
                        break;
                    case "M":
                        Char = "T";
                        break;
                    case "N":
                        Char = "9";
                        break;
                    case "O":
                        Char = "S";
                        break;
                    case "P":
                        Char = "2";
                        break;
                    case "Q":
                        Char = "Z";
                        break;
                    case "R":
                        Char = "X";
                        break;
                    case "S":
                        Char = "5";
                        break;
                    case "T":
                        Char = "1";
                        break;
                    case "U":
                        Char = "Q";
                        break;
                    case "V":
                        Char = "R";
                        break;
                    case "W":
                        Char = "6";
                        break;
                    case "X":
                        Char = "V";
                        break;
                    case "Y":
                        Char = "M";
                        break;
                    case "Z":
                        Char = "L";
                        break;
                    case "-":
                        Char = "-";
                        break;
                    default:
                        Char = "*";
                        break;
                        #endregion
                }
                _CodeKey = _CodeKey + Char;
            }
            return _CodeKey;
        }

       private void metroBtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }
        
        private void txtStoreName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtStoreName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtPhone);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtFax_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtEmail);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }

        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtZipcode);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.cmbCountry);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtState);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtCity);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Functions

        public FrmMetro_AddDemoStore()
        {
            InitializeComponent();
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    Functions.SetIcon(this);
            //}
            RefreshCountry();
            Clear();

        }

        public void Clear()
        {
            txtStoreName.Text = null;
            txtAddress.Text = null;
            txtAddress2.Text = null;
            txtZipcode.Text = null;
            txtPhone.Text = null;
            txtEmail.Text = null;
            txtState.Text = null;
            txtCity.Text = null;
            ep.SetError(txtStoreName, CommonModelCont.EmptyString);
            ep.SetError(txtAddress, CommonModelCont.EmptyString);
            ep.SetError(txtAddress2, CommonModelCont.EmptyString);
            ep.SetError(txtZipcode, CommonModelCont.EmptyString);
            ep.SetError(txtPhone, CommonModelCont.EmptyString);
            ep.SetError(txtEmail, CommonModelCont.EmptyString);
            //cmbCountry.SelectedIndex = 0;
            ep.SetError(cmbCountry, CommonModelCont.EmptyString);
            ep.SetError(txtState, CommonModelCont.EmptyString);
           ep.SetError(txtCity, CommonModelCont.EmptyString);
            ep.SetError(txtpersonName, CommonModelCont.EmptyString);
            PrimaryId = 0;
        }

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case CommonTextBoxs.txtStoreName:
                    //txtStoreName
                    if ((string.IsNullOrWhiteSpace(txtStoreName.Text)))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtStoreName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtStoreName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtpersonName:
                    //txtpersonName
                    if ((string.IsNullOrWhiteSpace(txtpersonName.Text)))
                    {
                        txtpersonName.Focus();
                        ep.SetError(txtpersonName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtpersonName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtpersonName.Focus();
                        ep.SetError(txtpersonName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtpersonName, CommonModelCont.EmptyString);
                    }
                    break;

                case CommonTextBoxs.txtPhone:
                    //txtPhone
                    if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtEmail:
                    //txtEmail
                    if ((string.IsNullOrWhiteSpace(txtEmail.Text)))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid3);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid1);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.cmbCountry:
                    //cmbCountry
                    if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCountry.SelectedIndex < 0)
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtState:
                    //txtState
                    if ((string.IsNullOrWhiteSpace(txtState.Text)))
                    {
                        txtState.Focus();
                        ep.SetError(txtState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (txtState.Text == string.Empty)
                    {
                        txtState.Focus();
                        ep.SetError(txtState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtState, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtCity:
                    //txtCity
                    if ((string.IsNullOrWhiteSpace(txtCity.Text)))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (txtCity.Text == string.Empty)
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCity, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonTextBoxs.txtZipcode:
                    //txtZipcode
                    if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                        status = false;

                    }
                    else
                    {
                        ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtStoreName
                    if ((string.IsNullOrWhiteSpace(txtStoreName.Text)))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtStoreName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtStoreName.Focus();
                        ep.SetError(txtStoreName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtStoreName, CommonModelCont.EmptyString);
                    }
                    //txtpersonName
                    if ((string.IsNullOrWhiteSpace(txtpersonName.Text)))
                    {
                        txtpersonName.Focus();
                        ep.SetError(txtpersonName, AlertMessages.NameValidation1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtpersonName.Text, CommonModelCont.Name_Validation)).Success))
                    {
                        txtpersonName.Focus();
                        ep.SetError(txtpersonName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtpersonName, CommonModelCont.EmptyString);
                    }
                    //txtPhone
                    if ((string.IsNullOrWhiteSpace(txtPhone.Text)))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid1);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtPhone.Text, CommonModelCont.phone_Validation)).Success))
                    {
                        txtPhone.Focus();
                        ep.SetError(txtPhone, AlertMessages.PhoneNoValid2);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtPhone, CommonModelCont.EmptyString);
                    }
                    //txtEmail
                    if (((string.IsNullOrWhiteSpace(txtEmail.Text))))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid2);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtEmail.Text, CommonModelCont.Email_Validation)).Success))
                    {
                        txtEmail.Focus();
                        ep.SetError(txtEmail, AlertMessages.EmailValid1);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtEmail, CommonModelCont.EmptyString);
                    }
                    //cmbCountry
                    if ((string.IsNullOrWhiteSpace(cmbCountry.Text)))
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (cmbCountry.SelectedIndex < 0)
                    {
                        cmbCountry.Focus();
                        ep.SetError(cmbCountry, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(cmbCountry, CommonModelCont.EmptyString);
                    }
                    //txtState
                    if ((string.IsNullOrWhiteSpace(txtState.Text)))
                    {
                        txtState.Focus();
                        ep.SetError(txtState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (txtState.Text == string.Empty)
                    {
                        txtState.Focus();
                        ep.SetError(txtState, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtState, CommonModelCont.EmptyString);
                    }
                    //txtCity
                    if ((string.IsNullOrWhiteSpace(txtCity.Text)))
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else if (txtCity.Text == string.Empty)
                    {
                        txtCity.Focus();
                        ep.SetError(txtCity, AlertMessages.DropdownValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCity, CommonModelCont.EmptyString);
                    }
                    //txtZipcode
                    if ((string.IsNullOrWhiteSpace(txtZipcode.Text)))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.ZipCodeValid);
                        status = false;
                    }
                    else if ((!(Regex.Match(txtZipcode.Text, CommonModelCont.ZipCode_Validation)).Success))
                    {
                        txtZipcode.Focus();
                        ep.SetError(txtZipcode, AlertMessages.StoreZipCodeValid);
                        status = false;

                    }
                    else
                    {
                        ep.SetError(txtZipcode, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        public void RefreshCountry()
        {
            try
            {
                CountryService _CountryService = new CountryService();
                List<CountryMasterModel> lstCountryMasterModel = new List<CountryMasterModel>();
                lstCountryMasterModel = _CountryService.GetAllCountry();
                cmbCountry.DisplayMember = CountryMasterModelCont.CountryName;
                cmbCountry.ValueMember = CountryMasterModelCont.CountryID;
                cmbCountry.DataSource = lstCountryMasterModel;
            }
            catch (Exception ex)
            {
               _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_StoreMaster");
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFrmSettingForm();
        }

        void OpenFrmSettingForm()
        {
            this.Hide();
            LoginInfo.SettingScreen = "tabLicenceKey";
            frmSettings _frmSettings = new frmSettings();
            _frmSettings.ShowDialog();
            Application.Exit();
        }

        private void FrmMetro_AddDemoStore_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(IsSuccess == false)
                Application.Exit();
        }

        private void txtpersonName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckValidation(CommonTextBoxs.txtpersonName);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmdemoStore + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
