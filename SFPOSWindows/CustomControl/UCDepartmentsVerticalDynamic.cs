using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using SFPOS.BAL;
using System.Data.SqlServerCe;
using System.Data.Common;
using System.Reflection;
using SFPOS.DAL;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;

namespace SFPOSWindows.CustomControl
{
    public partial class UCDepartmentsVerticalDynamic : UserControl
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public string DeptCode = "";
        SqlCeConnection conn = new SqlCeConnection(ClsCommon.SqlCeConn);

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDepartmentsVerticalDynamic));

        //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCDepartments));
        DepartmentService _DepartmentService = new DepartmentService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<DepartmentMasterModel> _objdepartmentlistFinal = new List<DepartmentMasterModel>();
        public UCDepartmentsVerticalDynamic()
        {
            InitializeComponent();
        }

        private void UCDepartmentsVerticalDynamic_Load(object sender, EventArgs e)
        {
            List<DepartmentMasterModel> objdepartmentlist = new List<DepartmentMasterModel>();

            if (LoginInfo.Connections)
            {
                objdepartmentlist = _DepartmentService.GetAllDepartment();
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DataTable dt = new DataTable();
                //string query = "SELECT EM.EmployeeID,EM.RoleID,EM.StoreID,EM.FirstName,EM.LastName,EM.MaxVoidAmount,EM.BirthDate,EM.IsActive,SM.IsStoreTax,SM.DefaultTax,SM.Disclaimer,SM.StoreName,RM.RoleType,SM.AgeVarificationAge FROM tbl_EmployeeMaster EM join tbl_StoreMaster SM on sm.StoreID = em.StoreID join tbl_RoleMaster RM ON RM.RoleID = EM.RoleID WHERE EmailID = @EmailID AND Password = @Password AND EM.IsDelete = 0";
                string queryGetDepartment = "SELECT CM.DepartmentID,CM.DepartmentNo,CM.SubNo,CM.DepartmentName,CM.AgeVarificationAge,CM.UnitMeasureID AS UnitMeasureID,CM.TaxGroupID AS TaxGroupID,CM.IsFoodStamp AS IsFoodStamp,CM.IsActive,CM.IsDelete,CM.Remark,CM.CreatedBy,CM.CreatedDate,CM.UpdatedBy,CM.UpdatedDate,cm.DepartmentBtn,cm.BtnCode,cm.DepartmentBtnIndex FROM tbl_DepartmentMaster CM WHERE CM.IsDelete = 0";
                SqlCeDataAdapter DataAdapter = new SqlCeDataAdapter(queryGetDepartment, conn);
                DataAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    objdepartmentlist = DataTableToList<DepartmentMasterModel>(dt);
                }
            }
            _objdepartmentlistFinal = objdepartmentlist.Where(person => person.IsActive == true && person.DepartmentBtn == true).ToList();
            try
            {

                var len = _objdepartmentlistFinal.Count;
                var _nextbtncount = 0;
                var locX_Dim = 0;
                var locY_Dim = 0;
                var countbtn = 0;
                for (int i = 0; i < 10; i++)
                {
                    locX_Dim = 0;
                    if (i == 0)
                    {
                        locY_Dim = 0;
                    }
                    for (int j = 0; j < 1; j++)
                    {
                        Button obj = new Button();

                        if (_nextbtncount < len)
                        {
                            obj.Name = "btn" + _objdepartmentlistFinal[_nextbtncount].DepartmentName.ToString();
                            obj.Text = _objdepartmentlistFinal[_nextbtncount].DepartmentName.ToString();
                            obj.Location = new System.Drawing.Point(locX_Dim, locY_Dim);
                            obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                            obj.Click += delegate (object cs, EventArgs ev) { button_Click(cs, ev, ""); };
                            _nextbtncount = _nextbtncount + 1;
                        }
                        else
                        {
                            obj.Name = "btn" + countbtn;
                            obj.Text = "-";
                            obj.Location = new System.Drawing.Point(locX_Dim, locY_Dim);
                            obj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                            countbtn++;
                        }

                        obj.Anchor = System.Windows.Forms.AnchorStyles.None;
                        obj.BackColor = System.Drawing.Color.White;
                        obj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        obj.Cursor = System.Windows.Forms.Cursors.Hand;
                        obj.FlatAppearance.BorderSize = 0;
                        obj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        obj.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        obj.Size = new System.Drawing.Size(162, 53);
                        this.Controls.Add(obj);
                    }
                    locY_Dim = locY_Dim + 63;

                }
            }
            catch (Exception ex)
            {
                ClsCommon.MsgBox("Information", "Something went wrong.!", false);
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, ex.StackTrace, ex.LineNumber());
            }
        }
        void button_Click(object sender, EventArgs e, string message)
        {
            string _DeptCode = string.Empty;
            var objPictureBox = (sender as Button);
            _objdepartmentlistFinal = _objdepartmentlistFinal.Where(person => person.DepartmentName == objPictureBox.Text).ToList();
            _DeptCode = _objdepartmentlistFinal[0].BtnCode.ToString();
            DeptCode = _DeptCode;
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        
        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            List<T> lstItems = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                    lstItems.Add(ConvertDataRowToGenericType<T>(row));
            else
                lstItems = null;
            return lstItems;
        }

        private static T ConvertDataRowToGenericType<T>(DataRow row) where T : class, new()
        {
            Type entityType = typeof(T);
            T objEntity = new T();
            foreach (DataColumn column in row.Table.Columns)
            {
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                PropertyInfo property = entityType.GetProperty(column.ColumnName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
                try
                {
                    if (property != null && property.CanWrite)
                        property.SetValue(objEntity, value, null);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objEntity;
        }
    }
}
