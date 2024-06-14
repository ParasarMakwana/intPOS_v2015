using ClosedXML.Excel;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms;
using SFPOSWindows.Metro_Forms.Metro_Sub_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmCustomer : Form
    {
        #region Properties
        public CustomerService _CustomerService = new CustomerService();
        public static long PrimaryId = 0;
        List<CustomerMasterModel> LstCustomerMasterModel = new List<CustomerMasterModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        CustomerMasterModel objCustomerMasterModel = new CustomerMasterModel();

        #endregion

        #region Events

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void EmployeeGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    UpdateData(e.RowIndex);
                }
                if (e.ColumnIndex == 1)
                {
                    if (PrimaryId > 0)
                    {
                        DialogResult result = MessageBox.Show(AlertMessages.Delete, AlertMessages.ConfirmDeletionAlert, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {

                            objCustomerMasterModel.CustomerID = PrimaryId;
                            //if (objCustomerMasterModel.CustomerID == LoginInfo.UserId)
                            //{
                            //    ClsCommon.MsgBox(AlertMessages.SuccessAlert, "You can't delete the currently logged User.!", false);
                            //}
                            //else
                            //{
                                var add = _CustomerService.AddCustomer(objCustomerMasterModel, 3);
                                UpdateLog();
                                if (add != null)
                                {
                                    ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.DeleteSuccess,false);
                                }
                                Clear();
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void EmployeeGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    PrimaryId = Convert.ToInt64(CustomerGrdView.Rows[e.RowIndex].Cells[CustomerMasterModelCont.CustomerID].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMetro_AddCustomer objFrmMetro_AddCustomer = new FrmMetro_AddCustomer();
                objFrmMetro_AddCustomer.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        private void txtSearchEmployeeName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchStr = txtSearchEmployeeName.Text;
                if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.EmployeeSearch)
                {
                    
                    CustomerGrdView.DataSource = LstCustomerMasterModel
                                                               .Where(o => o.FirstName.ToLower().StartsWith(SearchStr.ToLower())
                                                               || o.LastName.ToLower().StartsWith(SearchStr.ToLower())
                                                               || o.Email.ToLower().StartsWith(SearchStr.ToLower())
                                                               || o.MobileNo.ToLower().Contains(SearchStr.ToLower()))
                                                               .Select(o => new
                                                               {
                                                                   CustomerID = o.CustomerID,
                                                                   FirstName = o.FirstName,
                                                                   LastName = o.LastName,
                                                                   Email = o.Email,
                                                                   MobileNo = o.MobileNo,
                                                                   StoreID = o.StoreID,
                                                                   //StoreName = o.StoreName,
                                                                   ZipCode = o.ZipCode,
                                                                   Message = o.Message,
                                                                   ResellerID = o.ResellerID,
                                                                   TaxExempted = o.TaxExempted,
                                                                   BusinessName = o.BusinessName,
                                                                   NewsLetter = o.NewsLetter,
                                                                   IsActive = o.IsActive
                                                               }).ToList();

                    CustomerGrdView.Columns[CustomerMasterModelCont.CustomerID].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.StoreID].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.ResellerID].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.TaxExempted].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.BusinessName].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.NewsLetter].Visible = false;
                    CustomerGrdView.Columns[CustomerMasterModelCont.Message].Visible = false;

                    CustomerGrdView.Columns["FirstName"].HeaderText = "First Name";
                    CustomerGrdView.Columns["LastName"].HeaderText = "Last Name";
                    CustomerGrdView.Columns["Email"].HeaderText = "Email";
                    CustomerGrdView.Columns["MobileNo"].HeaderText = "Phone No";
                    CustomerGrdView.Columns["ZipCode"].HeaderText = "ZipCode";
                    //CustomerGrdView.Columns["StoreName"].HeaderText = "Store Name";
                    CustomerGrdView.Columns["IsActive"].HeaderText = "Active";

                }
                else
                { dataLoad(); }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        #endregion

        #region Functions
        public FrmCustomer()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            try
            {
                LstCustomerMasterModel = _CustomerService.GetAllCustomers();

                CustomerGrdView.DataSource = LstCustomerMasterModel.Select(o => new
                {
                    CustomerID = o.CustomerID,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Email = o.Email,
                    MobileNo = o.MobileNo,
                    StoreID = o.StoreID,
                    //StoreName = o.StoreName,
                    ZipCode = o.ZipCode,
                    Message = o.Message,
                    ResellerID = o.ResellerID,
                    TaxExempted = o.TaxExempted,
                    BusinessName = o.BusinessName,
                    NewsLetter = o.NewsLetter,
                    IsActive = o.IsActive,
                    City = o.City
                }).ToList();

                CustomerGrdView.Columns[CustomerMasterModelCont.CustomerID].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.StoreID].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.ResellerID].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.TaxExempted].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.BusinessName].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.NewsLetter].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.Message].Visible = false;
                CustomerGrdView.Columns[CustomerMasterModelCont.City].Visible = false;

                CustomerGrdView.Columns["FirstName"].HeaderText = "First Name";
                CustomerGrdView.Columns["LastName"].HeaderText = "Last Name";
                CustomerGrdView.Columns["Email"].HeaderText = "Email";
                CustomerGrdView.Columns["MobileNo"].HeaderText = "Phone No";
                CustomerGrdView.Columns["ZipCode"].HeaderText = "ZipCode";
                //CustomerGrdView.Columns["StoreName"].HeaderText = "Store Name";
                CustomerGrdView.Columns["IsActive"].HeaderText = "Active";
                
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }

        public void Clear()
        {
            dataLoad();
            PrimaryId = 0;
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_CustomerMaster");
        }
        #endregion

        #region IMPORT/EXPORT
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HRD=Yes'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {/* Kaushik
            try
            {
                string filePath = openFileDialog1.FileName;
                string extension = Path.GetExtension(filePath);
                string conStr, sheetName;
                int count = 0;
                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath);
                        break;
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();

                            StoreService _StoreService = new StoreService();
                            DataTable uniqueCols = dt.DefaultView.ToTable(true, "StoreName");
                            string SName = "";
                            for (int row = 0; row < uniqueCols.Rows.Count; row++)
                            {
                                #region Check Store
                                bool IsDepartment = _StoreService.CheckName(uniqueCols.Rows[row]["StoreName"].ToString());
                                if (!IsDepartment)
                                {
                                    if (SName == "")
                                    {
                                        SName = uniqueCols.Rows[row]["StoreName"].ToString();
                                    }
                                    else
                                    {
                                        SName = ", " + uniqueCols.Rows[row]["StoreName"].ToString();
                                    }
                                }
                                #endregion
                            }
                            RoleService _RoleService = new RoleService();
                            DataTable uniqueRoles = dt.DefaultView.ToTable(true, "RoleType");
                            string RName = "";
                            for (int row = 0; row < uniqueRoles.Rows.Count; row++)
                            {
                                #region Check Role
                                bool IsRole = _RoleService.CheckName(uniqueRoles.Rows[row]["RoleType"].ToString());
                                if (!IsRole)
                                {
                                    if (RName == "")
                                    {
                                        RName = uniqueRoles.Rows[row]["RoleType"].ToString();
                                    }
                                    else
                                    {
                                        RName = ", " + uniqueRoles.Rows[row]["RoleType"].ToString();
                                    }
                                }
                                #endregion
                            }

                            if (SName == "" && RName == "")
                            {
                                #region Add Employee
                                for (int row = 0; row < dt.Rows.Count; row++)
                                {
                                    long StoreID = 0;
                                    long RoleID = 0;
                                    StoreID = _StoreService.GetStoreID(dt.Rows[row]["StoreName"].ToString());
                                    RoleID = _RoleService.GetRoleID(dt.Rows[row]["RoleType"].ToString());
                                    if (StoreID != 0 && RoleID != 0)
                                    {
                                        bool IsEmployee = _CustomerService.CheckName(dt.Rows[row]["EmailID"].ToString());
                                        if (!IsEmployee)
                                        {
                                            count++;
                                            objEmployeeMasterModel.EmailID = dt.Rows[row]["EmailID"].ToString().Trim();
                                            objEmployeeMasterModel.PhoneNo = dt.Rows[row]["PhoneNo"].ToString().Trim();
                                            objEmployeeMasterModel.FirstName = dt.Rows[row]["FirstName"].ToString().Trim();
                                            objEmployeeMasterModel.LastName = dt.Rows[row]["LastName"].ToString().Trim();
                                            objEmployeeMasterModel.StoreID = StoreID;
                                            objEmployeeMasterModel.RoleID = RoleID;
                                            var add = _CustomerService.AddEmployee(objEmployeeMasterModel, 1);
                                        }
                                        //else
                                        //{
                                        //    objEmployeeMasterModel.EmailID = dt.Rows[row]["EmailID"].ToString().Trim();
                                        //    objEmployeeMasterModel.PhoneNo = dt.Rows[row]["PhoneNo"].ToString().Trim();
                                        //    objEmployeeMasterModel.LastName = dt.Rows[row]["FirstName"].ToString().Trim();
                                        //    objEmployeeMasterModel.LastName = dt.Rows[row]["LastName"].ToString().Trim();
                                        //    objEmployeeMasterModel.StoreID = StoreID;
                                        //    objEmployeeMasterModel.RoleID = RoleID;
                                        //}
                                    }
                                    else
                                    {
                                        ClsCommon.MsgBox("Information",dt.Rows[row]["EmailID"].ToString() + " Employee is not available!",false);
                                    }
                                }
                                ClsCommon.MsgBox("Information","Total " + count + " Employee imported successfully.!", false);
                                #endregion
                                dataLoad();
                            }
                            else
                            {
                                if (SName != "" && RName != "")
                                    ClsCommon.MsgBox("Information",SName + " Store & " + RName + "Role are not available!", false);
                                else if (RName != "")
                                    ClsCommon.MsgBox("Information",RName + " Role is not available!", false);
                                else if (SName != "")
                                    ClsCommon.MsgBox("Information",SName + " Store is not available!", false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Please select valid format.!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }*/
        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {/* Kaushik
            try
            {
                DataTable dt = new DataTable();
                dt = tableLoad();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx (*.xlsx)|*.xlsx";
                sfd.FileName = "Employee";
                sfd.Title = "Save an Excel File";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ClsCommon.MsgBox("Information","Data will be exported and you will be notified when it is ready.", false);
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.MsgBox("Information","It wasn't possible to write the data to the disk." + ex.Message, false);
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Employee");
                        wb.Worksheet(1).Columns().AdjustToContents();

                        if (!String.IsNullOrWhiteSpace(sfd.FileName))
                            wb.SaveAs(sfd.FileName, new SaveOptions { EvaluateFormulasBeforeSaving = false, GenerateCalculationChain = false, ValidatePackage = false });
                        ClsCommon.MsgBox("Information","Data will be exported successfully !!!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information","Something went wrong while exporting Employee !!!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }*/
        }
        /* Kaushik
        public DataTable tableLoad()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add(CustomerMasterModelCont.FirstName, typeof(string));
                dt.Columns.Add(CustomerMasterModelCont.LastName, typeof(string));
                dt.Columns.Add(CustomerMasterModelCont.EmailID, typeof(string));
                dt.Columns.Add(CustomerMasterModelCont.PhoneNo, typeof(string));
                dt.Columns.Add(StoreMasterModelCont.StoreName, typeof(string));
                dt.Columns.Add(RoleMasterModelCont.RoleType, typeof(string));

                foreach (var item in LstEmployeeMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    dr[CustomerMasterModelCont.FirstName] = item.FirstName;
                    dr[CustomerMasterModelCont.LastName] = item.LastName;
                    dr[CustomerMasterModelCont.EmailID] = item.EmailID;
                    dr[CustomerMasterModelCont.PhoneNo] = item.PhoneNo;
                    dr[StoreMasterModelCont.StoreName] = item.StoreName;
                    dr[RoleMasterModelCont.RoleType] = item.RoleType;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
            return dt;
        }
        */
        #endregion

        private void EmployeeGrdView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateData(e.RowIndex);
        }

        public void UpdateData(int RowIndex)
        {
            try
            {
                FrmMetro_AddCustomer objFrmMetro_AddCustomer = new FrmMetro_AddCustomer();
                objFrmMetro_AddCustomer.CustomerID = PrimaryId;
                objFrmMetro_AddCustomer.txtFirstName.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.FirstName].Value.ToString().Trim();
                objFrmMetro_AddCustomer.txtLastName.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.LastName].Value.ToString().Trim();
                objFrmMetro_AddCustomer.cmbStoreName.SelectedValue = CustomerGrdView.Rows[RowIndex].Cells[StoreMasterModelCont.StoreID].Value;
                objFrmMetro_AddCustomer.txtEmail.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.Email].Value.ToString().Trim();
                objFrmMetro_AddCustomer.txtMobile.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.MobileNo].Value.ToString().Trim();
                objFrmMetro_AddCustomer.txtZipcode.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.ZipCode].Value.ToString().Trim();
                if (CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.City].Value != null)
                {
                    objFrmMetro_AddCustomer.txtCity.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.City].Value.ToString().Trim();

                }
                if (CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.Message].Value != null)
                {
                    objFrmMetro_AddCustomer.txtMessage.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.Message].Value.ToString().Trim();
                }
                if (CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.ResellerID].Value != null)
                {
                    objFrmMetro_AddCustomer.txtResllerID.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.ResellerID].Value.ToString().Trim();
                }
                if (CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.BusinessName].Value != null)
                {
                    objFrmMetro_AddCustomer.txtBusinessName.Text = CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.BusinessName].Value.ToString().Trim();
                }
                objFrmMetro_AddCustomer.toggleActive.Checked = Convert.ToBoolean(CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.IsActive].Value);
                objFrmMetro_AddCustomer.toggleNewsLetter.Checked = Convert.ToBoolean(CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.NewsLetter].Value);
                objFrmMetro_AddCustomer.toggletaxExe.Checked = Convert.ToBoolean(CustomerGrdView.Rows[RowIndex].Cells[CustomerMasterModelCont.TaxExempted].Value);

                objFrmMetro_AddCustomer.ShowDialog();
                dataLoad();
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmEmployee + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
