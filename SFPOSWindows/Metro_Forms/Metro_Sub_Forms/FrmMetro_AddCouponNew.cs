using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SFPOSWindows.Metro_Forms.Metro_Sub_Forms
{
    public partial class FrmMetro_AddCouponNew : MetroForm
    {
        #region Properties
        public long CouponID = 0;
        ErrorProvider ep = new ErrorProvider();
        bool flagSave = false;
        public long UpdateCouponID;
        public bool UpdateAllowAllDepartment;
        public bool UpdateSelectedDepartment;
        public bool LoadData = false;
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        
        #endregion

        public FrmMetro_AddCouponNew()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(460, 480);
            AlignmentSize();
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

        public bool CheckValidation(string ControlName)
        {
            bool status = true;
            switch (ControlName)
            {
                case "txtCouponName":
                    //txtCouponName
                    if ((string.IsNullOrWhiteSpace(txtCouponName.Text)))
                    {
                        txtCouponName.Focus();
                        ep.SetError(txtCouponName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCouponName, CommonModelCont.EmptyString);
                    }
                    break;
                case CommonModelCont.EmptyString:
                    //default-ALL
                    //txtCouponName
                    if ((string.IsNullOrWhiteSpace(txtCouponName.Text)))
                    {
                        txtCouponName.Focus();
                        ep.SetError(txtCouponName, AlertMessages.NameValidation);
                        status = false;
                    }
                    else
                    {
                        ep.SetError(txtCouponName, CommonModelCont.EmptyString);
                    }
                    break;
            }
            return status;
        }

        private void MetrobtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                flagSave = CheckValidation(CommonModelCont.EmptyString);
                if (flagSave)
                {
                    CouponService _CouponService = new CouponService();

                    CouponMasterModel objCouponMasterModel = new CouponMasterModel();

                    objCouponMasterModel.CouponName = txtCouponName.Text.Trim();
                    objCouponMasterModel.CoupenCode = (String.IsNullOrEmpty(txtCouponCode.Text) ? GetUniqueToken(8) : (txtCouponCode.Text));

                    DateTime selectedStartDate = datePickerStartDate.Value.Date; // Get the selected date
                    DateTime startTime = selectedStartDate.Add(new TimeSpan(1, 0, 0)); // Set the time to 01:00:00 (01:00 AM)
                    datePickerStartDate.Value = startTime;
                    objCouponMasterModel.StartDate = datePickerStartDate.Value;

                    DateTime selectedEndDate = datePickerEndDate.Value.Date; // Get the selected date
                    DateTime endTime = selectedEndDate.Add(new TimeSpan(23, 59, 0)); // Set the time to 23:59:00 (11:59 PM)
                    datePickerEndDate.Value = endTime;
                    objCouponMasterModel.EndDate = datePickerEndDate.Value;

                    objCouponMasterModel.MinPurchaseAmt = (String.IsNullOrEmpty(txtMinPurAmt.Text.ToString()) ? 0 : Convert.ToDecimal(txtMinPurAmt.Text.Trim()));
                    objCouponMasterModel.Discount = (String.IsNullOrEmpty(txtDiscount.Text.ToString()) ? 0 : Convert.ToDecimal(txtDiscount.Text.Trim()));
                    //objCouponMasterModel.AvailableCount = Convert.ToInt64(txtCouponFrequency.Text);
                    objCouponMasterModel.UsedCount = 0;

                    if (toggleIsMulUser.Checked)
                        objCouponMasterModel.IsAllowMultipleTime = true;
                    else
                        objCouponMasterModel.IsAllowMultipleTime = false;

                    if (metroCustRest.Checked)
                        objCouponMasterModel.IsRestricted = true;
                    else
                        objCouponMasterModel.IsRestricted = false;

                    if (metroAllDep.Checked)
                        objCouponMasterModel.AllowAllDepartment = true;
                    else
                        objCouponMasterModel.AllowAllDepartment = false;

                    if (metroSelectedDep.Checked)
                    {
                        objCouponMasterModel.SelectedDepartment = true;
                        objCouponMasterModel.AllowAllDepartment = false;
                    }
                    else
                        objCouponMasterModel.SelectedDepartment = false;

                    if (objCouponMasterModel.AllowAllDepartment == true)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(DepartmentMasterModelCont.DepartmentID, typeof(int));

                        var result = (from table1 in _db.tbl_DepartmentMaster.Where(c => c.IsActive == true)
                                      select new
                                      {
                                          DepartmentID = table1.DepartmentID,
                                      }).OrderBy(c => c.DepartmentID);

                        foreach (var item in result)
                        {
                            DataRow dr = dt.NewRow();
                            dr[DepartmentMasterModelCont.DepartmentID] = item.DepartmentID;
                            dt.Rows.Add(dr);
                        }
                        List<int> departmentIds = dt.AsEnumerable()
                        .Select(row => row.Field<int>("DepartmentID"))
                        .ToList();

                        objCouponMasterModel.DepartmentID = departmentIds;
                    }

                    if(objCouponMasterModel.SelectedDepartment == true)
                    {
                        List<int> selectedDepartmentIDs = chkAllowDepartment.CheckedItems
                                .Cast<DataRowView>()
                                .Select(rowView => (int)rowView.Row[DepartmentMasterModelCont.DepartmentID])
                                .ToList();


                        objCouponMasterModel.DepartmentID = selectedDepartmentIDs;
                    }

                    if (CouponID <= 0)
                    {
                        var add = _CouponService.AddCoupon(objCouponMasterModel, 1);
                        if (add != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Add, false);
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                    }
                    else if (CouponID > 0)
                    {
                        objCouponMasterModel.CouponID = CouponID;
                        var add = _CouponService.AddCoupon(objCouponMasterModel, 2);

                        if (add != null)
                        {
                            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.Update, false);
                        }
                        else
                        {
                            ClsCommon.MsgBox(AlertMessages.InformationAlert, AlertMessages.Error, false);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmCoupon + ex.StackTrace, ex.LineNumber());
            }

        }

        private void metroBtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtCouponCode.Text = "";
            txtCouponName.Text = "";
            txtMinPurAmt.Text = "";
            txtDiscount.Text = "";
            datePickerStartDate.Value = DateTime.Now;
            datePickerEndDate.Value = DateTime.Now;
            metroAllDep.Checked = false;
            metroSelectedDep.Checked = false;
            metroCustRest.Checked = false;
            toggleIsMulUser.Checked = false;
            //UpdateCouponID = 0;
            LoadData = false;
            LoadDepartmentData();
        }



        //public string GenerateRandomString(int length)
        //{
        //    var builder = new StringBuilder();
        //    while (builder.Length < length)
        //    {
        //        builder.Append(Guid.NewGuid().ToString());
        //    }
        //    return builder.ToString(0, length);
        //}

        //private string GetUniqueKey()
        //{
        //    int maxSize = 8;
        //    int minSize = 6;
        //    char[] chars = new char[62];
        //    string a;
        //    a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    chars = a.ToCharArray();
        //    int size = maxSize;
        //    byte[] data = new byte[1];
        //    RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        //    crypto.GetNonZeroBytes(data);
        //    size = maxSize;
        //    data = new byte[size];
        //    crypto.GetNonZeroBytes(data);
        //    StringBuilder result = new StringBuilder(size);
        //    foreach (byte b in data)
        //    {
        //        result.Append(chars[b % (chars.Length)]);
        //    }
        //    return result.ToString();
        //}

        public static string GetUniqueToken(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                byte[] buffer = null;

                int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

                crypto.GetBytes(data);
                char[] result = new char[length];

                for (int i = 0; i < length; i++)
                {
                    byte value = data[i];
                    while (value > maxRandom)
                    {
                        if (buffer == null)
                        {
                            buffer = new byte[1];
                        }

                        crypto.GetBytes(buffer);
                        value = buffer[0];
                    }
                    result[i] = chars[value % chars.Length];
                }
                return new string(result);
            }
        }

        private void FrmMetro_AddCouponNew_Load(object sender, EventArgs e)
        {

        }

        private void toggleIsMulUser_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroSelectedDep_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (metroSelectedDep.Checked == true)
                {
                    // Set the form size
                    
                    this.Size = new System.Drawing.Size(684, 480);
                    groupBox1.Visible = true;
                    metroAllDep.Checked = false;
                    chkAllowDepartment.Visible = true;
                    AlignmentSize();
                    if(LoadData == false)
                    {  
                        LoadDepartmentData();
                    }
                    else
                    {
                        CheckedDeparmentdata(UpdateCouponID);
                    }
                    chkAllowDepartment.CheckOnClick = true;
                }
                else
                {
                    // Set the form size
                    this.Size = new System.Drawing.Size(460, 480);
                    groupBox1.Visible = false;
                    metroAllDep.Checked = true;
                    chkAllowDepartment.Visible = false;
                    AlignmentSize();
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void metroAllDep_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDepartmentData();
            }
            catch(Exception ex)
            {

            }
        }

        private void AlignmentSize()
        {
            this.StartPosition = FormStartPosition.CenterScreen; 
            // Change the size of individual controls
            txtCouponCode.Size = new System.Drawing.Size(265, 29);
            txtCouponCode.Location = new System.Drawing.Point(163, 89);
            txtCouponName.Size = new System.Drawing.Size(265, 29);
            txtCouponName.Location = new System.Drawing.Point(163, 124);
            datePickerStartDate.Size = new System.Drawing.Size(265, 29);
            datePickerStartDate.Location = new System.Drawing.Point(163, 159);
            datePickerEndDate.Size = new System.Drawing.Size(265, 29);
            datePickerEndDate.Location = new System.Drawing.Point(163, 194);
            txtMinPurAmt.Size = new System.Drawing.Size(265, 29);
            txtMinPurAmt.Location = new System.Drawing.Point(163, 229);
            txtDiscount.Size = new System.Drawing.Size(265, 29);
            txtDiscount.Location = new System.Drawing.Point(163, 264);
            metroBtnClear.Location = new System.Drawing.Point(336, 379);
            MetrobtnSave.Location = new System.Drawing.Point(225, 379);
            groupBox1.Location = new System.Drawing.Point(441, 49);
        }

        private void LoadDepartmentData()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentID, typeof(int));
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentName, typeof(string));

                var result = (from table1 in _db.tbl_DepartmentMaster.Where(c => c.IsActive == true)
                              select new
                              {
                                  DepartmentID = table1.DepartmentID,
                                  DepartmentName = table1.DepartmentName
                              }).OrderBy(c => c.DepartmentID);

                foreach (var item in result)
                {
                    DataRow dr = dt.NewRow();
                    dr[DepartmentMasterModelCont.DepartmentID] = item.DepartmentID;
                    dr[DepartmentMasterModelCont.DepartmentName] = item.DepartmentName;
                    dt.Rows.Add(dr);
                }

                chkAllowDepartment.DataSource = dt;
                chkAllowDepartment.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                chkAllowDepartment.ValueMember = DepartmentMasterModelCont.DepartmentID;

                if (metroAllDep.Checked == false)
                {
                    if (LoadData == false)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            chkAllowDepartment.SetItemChecked(i, false);
                        }
                    }
                    else
                    {
                        CheckedDeparmentdata(UpdateCouponID);
                    }
                }
                else
                {
                    if(LoadData == false)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            chkAllowDepartment.SetItemChecked(i, true);
                        }
                    }
                    else
                    {
                        CheckedDeparmentdata(UpdateCouponID);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CheckedDeparmentdata(long CouponID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentID, typeof(int));
                dt.Columns.Add(DepartmentMasterModelCont.DepartmentName, typeof(string));

                var departmentNames = _db.tbl_DepartmentMaster
                    .Where(dm => dm.IsActive == true)
                    .OrderBy(dm => dm.DepartmentID)
                    .Select(dm => new
                    {
                        DepartmentID = dm.DepartmentID,
                        DepartmentName = dm.DepartmentName
                    })
                    .ToList();

                var couponID = CouponID;
                //var associatedDepartmentIDs = _db.tbl_CouponAppliedDepartment
                //    .Where(cad => cad.CouponID == couponID && cad.IsActive == true)
                //    .Select(cad => cad.DepartmentID)
                //    .ToList();



                foreach (var department in departmentNames)
                {
                    DataRow dr = dt.NewRow();
                    dr[DepartmentMasterModelCont.DepartmentID] = department.DepartmentID;
                    dr[DepartmentMasterModelCont.DepartmentName] = department.DepartmentName;
                    dt.Rows.Add(dr);

                    // Check if department.DepartmentID exists in chkAllowDepartment
                }

                chkAllowDepartment.DataSource = dt;
                chkAllowDepartment.DisplayMember = DepartmentMasterModelCont.DepartmentName;
                chkAllowDepartment.ValueMember = DepartmentMasterModelCont.DepartmentID;

                if (couponID != 0)
                {
                    if(metroAllDep.Checked == false)
                    {
                        List<int> associatedDepartmentIDs = _db.tbl_CouponAppliedDepartment.Where(cad => cad.CouponID == couponID && cad.IsActive == true).Select(cad => (int)cad.DepartmentID).ToList(); // Your list of department IDs
                        foreach (DataRowView item in chkAllowDepartment.Items.Cast<DataRowView>().ToList())
                        {
                            int departmentID = (int)item.Row[DepartmentMasterModelCont.DepartmentID];

                            // Check if departmentID exists in associatedDepartmentIDs list
                            bool isChecked = associatedDepartmentIDs.Contains(departmentID);

                            // Set the checkbox state based on isChecked
                            chkAllowDepartment.SetItemChecked(chkAllowDepartment.Items.IndexOf(item), isChecked);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            chkAllowDepartment.SetItemChecked(i, true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        chkAllowDepartment.SetItemChecked(i, true);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        private void chkAllowDepartment_Click(object sender, EventArgs e)
        {
        }

        //private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        //{
        //    // Check or uncheck the single item in the CheckedListBox
        //    e.NewValue = e.NewValue == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        //}



        //private void toggleIsMulUser_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (toggleIsMulUser.Checked)
        //    {
        //        if (!ToggleIsStaticCoupon.Checked)
        //        {
        //            txtCouponCode.Text = GetUniqueToken(8);
        //            txtCouponCode.Enabled = false;
        //        }
        //        else
        //        {
        //            txtCouponCode.Text = "";
        //            txtCouponCode.Enabled = true;
        //        }
        //        ToggleIsStaticCoupon.Checked = false;
        //    }
        //    else
        //    {
        //        if (!ToggleIsStaticCoupon.Checked)
        //        {
        //            txtCouponCode.Text = GetUniqueToken(8);
        //            txtCouponCode.Enabled = false;
        //        }
        //        else
        //        {
        //            txtCouponCode.Text = "";
        //            txtCouponCode.Enabled = true;
        //        }
        //        ToggleIsStaticCoupon.Checked = false;
        //    }
        //}
    }
}

