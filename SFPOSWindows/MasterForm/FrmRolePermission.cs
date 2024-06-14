using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmRolePermission : Form
    {
        #region Properties

        RolePermissionService _RolePermissionService = new RolePermissionService();
        RoleService _RoleService = new RoleService();
        MenuService _MenuService = new MenuService();

        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(CommonModelCont.EmptyString);
        RolePermissionMasterModel objRolePermissionMasterModel = new RolePermissionMasterModel();
        bool delete = false, insert = false , edit = false, view = false;
        int PrimaryId = 0;
        string MenuName = CommonModelCont.EmptyString, RoleType = CommonModelCont.EmptyString;
        List<RolePermissionMasterModel> lstRolePermissionMasterModel = new List<RolePermissionMasterModel>();
        List<RoleMasterModel> lstRoleMasterModel = new List<RoleMasterModel>();
        List<MenuMasterModel> lstMenuMasterModel = new List<MenuMasterModel>();

        #endregion

        #region Events
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchRoleType.Text = AlertMessages.RoleSearch;
            dataLoad();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //List<RolePermissionMasterModel> objRolePermissionMasterModel1 = new List<RolePermissionMasterModel>();

            //int menuId = Convert.ToInt32(cmbMenu.SelectedValue);
            //int RoleId = Convert.ToInt32(cmbRoleName.SelectedValue);
            //bool IsExist = _RolePermissionService.CheckRoleName(RoleId,menuId); 

            //if (IsExist == false)
            //{
            //    objRolePermissionMasterModel.PermissionID = PrimaryId;
            //    objRolePermissionMasterModel.RoleID = Convert.ToInt32(cmbRoleName.SelectedValue);
            //    objRolePermissionMasterModel.MenuID = Convert.ToInt32(cmbMenu.SelectedValue);
            //    var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 1);
            //}
            //else
            //{
            //    DialogResult result = MessageBox.Show(AlertMessages.RolePermissionValid, AlertMessages.RolePermissionInfoAlert, MessageBoxButtons.OK);
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PrimaryId = 0;
        }

        private void RoleGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                objRolePermissionMasterModel.InsertRecords = insert;
                objRolePermissionMasterModel.DeleteRecords = delete;
                objRolePermissionMasterModel.EditRecords = edit;
                objRolePermissionMasterModel.PermissionID = PrimaryId;
                if (view == false)
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmAllow + RoleType + AlertMessages.RolePermissionView + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.ViewRecords = true;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                else
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmRemove + RoleType + AlertMessages.RolePermissionView + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.ViewRecords = false;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                dataLoad();
            }
            if (e.ColumnIndex == 4)
            {
                objRolePermissionMasterModel.EditRecords = edit;
                objRolePermissionMasterModel.DeleteRecords = delete;
                objRolePermissionMasterModel.ViewRecords = view;
                objRolePermissionMasterModel.PermissionID = PrimaryId;
                if (insert == false)
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmAllow + RoleType + AlertMessages.RolePermissionInsert + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.InsertRecords = true;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                else
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmRemove + RoleType + AlertMessages.RolePermissionInsert + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.InsertRecords = false;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                dataLoad();
            }
            if (e.ColumnIndex == 5)
            {
                objRolePermissionMasterModel.InsertRecords = insert;
                objRolePermissionMasterModel.DeleteRecords = delete;
                objRolePermissionMasterModel.ViewRecords = view;
                objRolePermissionMasterModel.PermissionID = PrimaryId;
                if (edit == false)
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmAllow + RoleType + AlertMessages.RolePermissionEdit + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.EditRecords = true;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                else
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmRemove + RoleType + AlertMessages.RolePermissionEdit + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.EditRecords = false;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                dataLoad();
            }
            if (e.ColumnIndex == 6)
            {
                objRolePermissionMasterModel.EditRecords = edit;
                objRolePermissionMasterModel.InsertRecords = insert;
                objRolePermissionMasterModel.ViewRecords = view;
                objRolePermissionMasterModel.PermissionID = PrimaryId;
                if (delete == false)
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmAllow + RoleType + AlertMessages.RolePermissionDelete + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.DeleteRecords = true;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                else
                {
                    DialogResult Res = MessageBox.Show(AlertMessages.RolePermissionConfirmRemove + RoleType + AlertMessages.RolePermissionDelete + MenuName + AlertMessages.QuestionMark, AlertMessages.ReloPermissionConfirm, MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        objRolePermissionMasterModel.DeleteRecords = false;
                        var add = _RolePermissionService.AddRolePermission(objRolePermissionMasterModel, 2);
                    }
                }
                dataLoad();
            }
        }

        private void RoleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PrimaryId = Convert.ToInt32(RoleGrdView.Rows[e.RowIndex].Cells[RoleMasterModelCont.RoleID].Value);
            CmbMenu();
        }

        private void txtSearchRoleType_TextChanged(object sender, EventArgs e)
        {
            string SearchStr = txtSearchRoleType.Text;
            if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.RoleSearch)
            {
                List<RolePermissionMasterModel> objRolePermissionMasterModel = new List<RolePermissionMasterModel>();
                objRolePermissionMasterModel = _RolePermissionService.GetAllRolePermission();
                List<RoleMasterModel> objRoleMasterModel = new List<RoleMasterModel>();
                objRoleMasterModel = _RoleService.GetSearchRole(SearchStr);
                //_db.tbl_RoleMaster.Where(x => x.IsDelete == false && x.RoleType.ToLower().StartsWith(SearchStr.ToLower())).ToList()
                if (objRoleMasterModel.Count > 0)
                {
                    RoleGrdView.DataSource = objRolePermissionMasterModel
                        .Where(o => o.RoleID == (objRoleMasterModel.Select(x => new { RoleID = x.RoleID })).FirstOrDefault().RoleID)
                        .Select(o => new
                        {
                            PermissionID = o.PermissionID,
                            Role = (from rp in _db.tbl_RolePermission
                                    join rm in _db.tbl_RoleMaster on rp.RoleID equals rm.RoleID
                                    where (rm.RoleID == o.RoleID)
                                    select new
                                    { Name = rm.RoleType }).FirstOrDefault().Name,
                            Menu = (from rp in _db.tbl_RolePermission
                                    join mm in _db.tbl_MenuMaster on rp.MenuID equals mm.MenuID
                                    where (mm.MenuID == o.MenuID)
                                    select new
                                    { MenuName = mm.MenuName }).FirstOrDefault().MenuName,
                            ViewRecord = o.ViewRecords,
                            InsertRecord = o.InsertRecords,
                            EditRecord = o.EditRecords,
                            DeleteRecord = o.DeleteRecords,
                            RoleID = o.RoleID,
                            MenuID = o.MenuID
                        }).ToList();
                    RoleGrdView.Columns[RolePermissionMasterModelCont.PermissionID].Visible = false;
                    RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
                    RoleGrdView.Columns[RolePermissionMasterModelCont.MenuID].Visible = false;
                }
                else { dataLoad(); }
            }
        }

        private void txtSearchRoleType_Enter(object sender, EventArgs e)
        {
            if (txtSearchRoleType.Text == AlertMessages.RoleSearch)
            {
                txtSearchRoleType.Text = CommonModelCont.EmptyString;
                txtSearchRoleType.ForeColor = Color.Black;
            }
        }

        private void btnToOne_Click(object sender, EventArgs e)
        {
            //lstTo.Items.AddRange(lstFrom.Items);           
            //SetButtonsEditable();

            MoveSelectedItems(lstFrom, lstTo);

        }

        private void MoveSelectedItems(ListBox lstFrom, ListBox lstTo)
        {
            //foreach (object itemChecked in lstFrom.SelectedItems)
            //{
            //    DataRowView castedItem = itemChecked as DataRowView;
            //    string comapnyName = castedItem["MenuName"].ToString();
            //    lstTo.Items.Add(castedItem.Row["MenuName"]);
            //    int id = Convert.ToInt32(castedItem["MenuID"]);
            //    int idx = (lstFrom.SelectedIndices[id]);
            //    lstFrom.Items.RemoveAt(idx);


            //}
            for (int i = 0; i < lstFrom.Items.Count; i++)
            {
                if (lstFrom.GetSelected(i))
                {
                    string id = lstFrom.SelectedIndex.ToString();
                    string item = (string)lstFrom.Items[i];
                    lstTo.Items.Add(item);
                    lstFrom.Items.Remove(item);
                }
            }
            //for (int x = lstFrom.SelectedIndices.Count - 1; x >= 0; x--)
            //{
            //    int idx = lstFrom.SelectedIndices[x];
            //    lstFrom.Items.RemoveAt(idx);
            //}


            //for (int i = 0; i < lstFrom.Items.Count; i++)
            //{
            //    if (lstFrom.GetSelected(i))
            //    {
            //        string str = (string)chkBoxList.Items[i];
            //        DataRowView castedItem = lstFrom.Items[i] as DataRowView;
            //        string comapnyName = castedItem["MenuName"].ToString();
            //        int id = Convert.ToInt32(castedItem["MenuID"]);
            //        lstFrom.Items.RemoveAt(id);
            //    }
            //}
            SetButtonsEditable();
        }
    


        private void btnToAll_Click(object sender, EventArgs e)
        {

        }

        private void btnFromOne_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lstTo, lstFrom);
        }

        private void btnFromAll_Click(object sender, EventArgs e)
        {

        }
        // Enable and disable buttons.
        private void SetButtonsEditable()
        {
            //btnSelect.Enabled = (lstUnselected.SelectedItems.Count > 0);
            //btnSelectAll.Enabled = (lstUnselected.Items.Count > 0);
            //btnDeselect.Enabled = (lstSelected.SelectedItems.Count > 0);
            //btnDeselectAll.Enabled = (lstSelected.Items.Count > 0);
        }

        private void txtSearchRoleType_Leave(object sender, EventArgs e)
        {
            if (txtSearchRoleType.Text == CommonModelCont.EmptyString)
            {
                txtSearchRoleType.Text = AlertMessages.RoleSearch;
                txtSearchRoleType.ForeColor = Color.Silver;
            }

        }
        #endregion

        #region Functions
        public FrmRolePermission()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            lstRoleMasterModel = _RoleService.GetAllRole();
            //lstRolePermissionMasterModel = _RolePermissionService.GetAllRole();
            RoleGrdView.DataSource = lstRoleMasterModel.Select(o => new
            {
                RoleID = o.RoleID,
                RoleType = o.RoleType
            }).ToList();

            RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
        }

        //public void CmbRole()
        //{
        //    lstRoleMasterModel = _RoleService.GetAllRole();
        //    cmbRoleName.DisplayMember = RoleMasterModelCont.RoleType;
        //    cmbRoleName.ValueMember = RoleMasterModelCont.RoleID;
        //    cmbRoleName.DataSource = lstRoleMasterModel;
        //}

        public void CmbMenu()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MenuID", typeof(int));
            dt.Columns.Add("MenuName", typeof(string));
            var results = from table1 in _db.tbl_MenuMaster.AsEnumerable()
                          select new
                          { MenuID = table1.MenuID, MenuName = table1.MenuName };
            foreach (var item in results)
            {
                DataRow dr = dt.NewRow();
                dr["MenuID"] = item.MenuID;
                dr["MenuName"] = item.MenuName;
                dt.Rows.Add(dr);
            }

            //lstMenuMasterModel = _MenuService.GetAllMenu();
            //lstFrom.DataSource = dt;
            //lstFrom.DisplayMember = "MenuName";
            //lstFrom.ValueMember = "MenuID";
            lstFrom.Items.Insert(0, "initialItem");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //men obj = new men();
                //obj.MenuID = Convert.ToInt32(dt.Rows[i]["MenuID"]);
                //obj.MenuName = dt.Rows[i]["MenuName"].ToString();
                
                lstFrom.Items.Insert(Convert.ToInt32(dt.Rows[i]["MenuID"]), dt.Rows[i]["MenuName"].ToString()); 
                //lstFrom.DisplayMember = (dt.Rows[i]["MenuName"].ToString());
                //lstFrom.ValueMember = (dt.Rows[i]["MenuID"].ToString());
            }


        }

        public class men
        {
           
            public int MenuID { get; set; }
            public string MenuName { get; set; }
        }

        #endregion
    }
}
