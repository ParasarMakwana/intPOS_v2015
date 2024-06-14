using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.Metro_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class frmFunctionalPermission : Form
    {
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        List<RoleMasterModel> lstRoleMasterModel = new List<RoleMasterModel>();
        RoleService _RoleService = new RoleService();
        EmployeeService _objEmployeeService = new EmployeeService();
        string PrimaryId;
        public frmFunctionalPermission()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            lstRoleMasterModel = _RoleService.GetAllRole();
            List<string> fun = new List<string>();
            fun.Add("Cash Payout");
            fun.Add("Lotto Fuction");
            
            RoleGrdView.DataSource = fun.Select(o => new
            {
                FunType = o.ToString()
            }).ToList();

            RoleGrdView.Columns["FunType"].HeaderText = "Function";
            
        }
        private void RoleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //chkBoxListBackend.Enabled = true;
                //chkBoxListFrontend.Enabled = true;
                PrimaryId = (RoleGrdView.Rows[e.RowIndex].Cells["FunType"].Value).ToString();
                chkBoxListLoad(PrimaryId);
                //ClsCommon.MsgBox("name",PrimaryId.ToString(),false);
                //FillCheckBoxList(PrimaryId);
            }
        }
        public void chkBoxListLoad(string FunType = "")
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(EmployeeMasterModelCont.EmployeeID, typeof(int));
            dt.Columns.Add(EmployeeMasterModelCont.FirstName, typeof(string));
            dt.Columns.Add(EmployeeMasterModelCont.IsCashPayout, typeof(bool));
            dt.Columns.Add(EmployeeMasterModelCont.IsLottoFunction, typeof(bool));

            var result = from table1 in _db.tbl_EmployeeMaster
                         select new
                         { EmployeeID = table1.EmployeeID, FirstName = table1.FirstName, LastName = table1.LastName, IsCashPayout = table1.IsCashPayout, IsLottoFunction = table1.IsLottoFunction };

            foreach (var item in result)
            {
                DataRow dr = dt.NewRow();
                dr[EmployeeMasterModelCont.EmployeeID] = item.EmployeeID;
                dr[EmployeeMasterModelCont.FirstName] = item.FirstName + " " + item.LastName ;
                dr[EmployeeMasterModelCont.IsCashPayout] = (item.IsCashPayout.ToString() == "" ? false : item.IsCashPayout);
                dr[EmployeeMasterModelCont.IsLottoFunction] = (item.IsLottoFunction.ToString() == "" ? false : item.IsLottoFunction);
                dt.Rows.Add(dr);
            }

            chkBoxList.DataSource = dt;
            chkBoxList.DisplayMember = EmployeeMasterModelCont.FirstName;
            chkBoxList.ValueMember = EmployeeMasterModelCont.EmployeeID;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(FunType.ToString() != "")
                {
                    if(FunType.ToString() == "Cash Payout")
                    {
                        if (dt.Rows[i]["IsCashPayout"].ToString().ToUpper() == "TRUE")
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        else
                        {
                            chkBoxList.SetItemChecked(i, false);
                        }
                    }
                    else if(FunType.ToString() == "Lotto Fuction")
                    {
                        if (dt.Rows[i]["IsLottoFunction"].ToString().ToUpper() == "TRUE")
                        {
                            chkBoxList.SetItemChecked(i, true);
                        }
                        else
                        {
                            chkBoxList.SetItemChecked(i, false);
                        }
                    }
                }
                
                
            }
        }

        private void lblSubHeading_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeMasterModel objEmployeeMasterModel = new EmployeeMasterModel();

            foreach(object itemSelected in chkBoxList.Items)
            {
                DataRowView castedItem = itemSelected as DataRowView;
                int id = Convert.ToInt32(castedItem[EmployeeMasterModelCont.EmployeeID]);
                var selected = false;
                foreach (object selectedItems in chkBoxList.CheckedItems)
                {
                    DataRowView rowItem = selectedItems as DataRowView;
                    int rowId = Convert.ToInt32(rowItem[EmployeeMasterModelCont.EmployeeID]);
                    if(id == rowId)
                    {
                        selected = true;
                        break;
                    }
                }
                
                objEmployeeMasterModel.EmployeeID = Convert.ToInt32(id);
                if(PrimaryId == "Cash Payout")
                {
                    objEmployeeMasterModel.IsCashPayout = Convert.ToBoolean(selected);
                    _objEmployeeService.AddEmployee(objEmployeeMasterModel, 4);
                }
                else if(PrimaryId == "Lotto Fuction")
                {
                    objEmployeeMasterModel.IsLottoFunction = Convert.ToBoolean(selected);
                    _objEmployeeService.AddEmployee(objEmployeeMasterModel, 5);
                }

                

            }

            UpdateLog();
            ClsCommon.MsgBox(AlertMessages.SuccessAlert, "Permission Updated Successfully!!!", false);
            //dataLoad();
            chkBoxListLoad(PrimaryId);
        }
        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_EmployeeMaster");
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataLoad();
            chkBoxListLoad();
        }
    }
}
