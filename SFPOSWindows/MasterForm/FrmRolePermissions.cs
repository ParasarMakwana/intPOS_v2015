using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using SFPOSWindows.Metro_Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmRolePermissions : Form
    {

        #region Properties
        DataTable dt = new DataTable();

        RoleService _RoleService = new RoleService();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        List<RoleMasterModel> lstRoleMasterModel = new List<RoleMasterModel>();
        RolePermissionService _RolePermissionService = new RolePermissionService();
        RolePermissionMasterModel _RolePermissionMasterModel = new RolePermissionMasterModel();
        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
        List<RolePermissionMasterModel> objRolePermissionMasterModel = new List<RolePermissionMasterModel>();

        int PrimaryId = 0;
        //MenuService _MenuService = new MenuService();
        //List<MenuMasterModel> lstMenuMasterModel = new List<MenuMasterModel>();

        #endregion

        #region Events

        private void txtSearchRoleType_TextChanged(object sender, EventArgs e)
        {
            string SearchStr = txtSearchRoleType.Text;
            if (SearchStr != null && SearchStr != CommonModelCont.EmptyString && SearchStr != AlertMessages.RoleSearch)
            {
                RoleGrdView.DataSource = lstRoleMasterModel
                    .Where(o => o.RoleType.ToLower().StartsWith(SearchStr.ToLower().ToString()))
                    .Select(o => new
                    {
                        RoleID = o.RoleID,
                        RoleType = o.RoleType
                    }).ToList();

                RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
                //RoleGrdView_CellClick(RoleGrdView, new DataGridViewCellEventArgs(0, 0));
                RoleGrdView.Columns["RoleType"].HeaderText = "Role";
            }
            else { dataLoad(); }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearchRoleType.Text = AlertMessages.RoleSearch;
            txtSearchRoleType.ForeColor = Color.Silver;
            dataLoad();
        }

        private void RoleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                chkBoxListBackend.Enabled = true;
                chkBoxListFrontend.Enabled = true;
                PrimaryId = Convert.ToInt32(RoleGrdView.Rows[e.RowIndex].Cells[RoleMasterModelCont.RoleID].Value);
                FillCheckBoxList(PrimaryId);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _RolePermissionMasterModel = new RolePermissionMasterModel();
            _RolePermissionMasterModel.RoleID = PrimaryId;

            foreach (object itemChecked in chkBoxListBackend.CheckedItems)
            {
                DataRowView castedItem = itemChecked as DataRowView;
                string Name = castedItem[RolePermissionMasterModelCont.MenuName].ToString();
                int id = Convert.ToInt32(castedItem[RolePermissionMasterModelCont.MenuID]);
                _RolePermissionMasterModel.MenuID = Convert.ToInt64(id);
                _RolePermissionMasterModel.ViewRecords = true;
                _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 3);
                _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 1);
            }

            for (int i = 0; i < chkBoxListBackend.Items.Count; i++)
            {
                if (!chkBoxListBackend.GetItemChecked(i))
                {
                    DataRowView castedItem = chkBoxListBackend.Items[i] as DataRowView;
                    string Name = castedItem[RolePermissionMasterModelCont.MenuName].ToString();
                    int id = Convert.ToInt32(castedItem[RolePermissionMasterModelCont.MenuID]);
                    _RolePermissionMasterModel.MenuID = Convert.ToInt64(id);
                    _RolePermissionMasterModel.ViewRecords = false;
                    _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 3);
                    _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 1);
                }
            }

            foreach (object itemChecked in chkBoxListFrontend.CheckedItems)
            {
                DataRowView castedItem = itemChecked as DataRowView;
                string Name = castedItem[RolePermissionMasterModelCont.MenuName].ToString();
                int id = Convert.ToInt32(castedItem[RolePermissionMasterModelCont.MenuID]);
                _RolePermissionMasterModel.MenuID = Convert.ToInt64(id);
                _RolePermissionMasterModel.ViewRecords = true;
                _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 3);
                _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 1);
            }

            for (int i = 0; i < chkBoxListFrontend.Items.Count; i++)
            {
                if (!chkBoxListFrontend.GetItemChecked(i))
                {
                    DataRowView castedItem = chkBoxListFrontend.Items[i] as DataRowView;
                    string Name = castedItem[RolePermissionMasterModelCont.MenuName].ToString();
                    int id = Convert.ToInt32(castedItem[RolePermissionMasterModelCont.MenuID]);
                    _RolePermissionMasterModel.MenuID = Convert.ToInt64(id);
                    _RolePermissionMasterModel.ViewRecords = false;
                    _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 3);
                    _RolePermissionService.AddRolePermission(_RolePermissionMasterModel, 1);
                }
            }

            UpdateLog();
            ClsCommon.MsgBox(AlertMessages.SuccessAlert, AlertMessages.RolePermissionSuccess,  false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PrimaryId = 0;
            dataLoad();
        }
        #endregion

        #region Functions
        public FrmRolePermissions()
        {
            InitializeComponent();
        }

        public void dataLoad()
        {
            lstRoleMasterModel = _RoleService.GetAllRole();
            RoleGrdView.DataSource = lstRoleMasterModel.Select(o => new
            {
                RoleID = o.RoleID,
                RoleType = o.RoleType
            }).ToList();

            RoleGrdView.Columns[RoleMasterModelCont.RoleID].Visible = false;
            RoleGrdView.Columns["RoleType"].HeaderText = "Role";
        }

        public void chkBoxListLoad()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(RolePermissionMasterModelCont.MenuID, typeof(int));
            dt.Columns.Add(RolePermissionMasterModelCont.MenuName, typeof(string));

            var result = (from table1 in _db.tbl_MenuMaster.Where(c => c.MenuType == "BE")//.AsEnumerable()
                         select new
                         { MenuID = table1.MenuID, MenuName = table1.MenuName, SortNo = table1.SortNo }).OrderBy(c=>c.SortNo);
            foreach (var item in result)
            {
                DataRow dr = dt.NewRow();
                dr[RolePermissionMasterModelCont.MenuID] = item.MenuID;
                dr[RolePermissionMasterModelCont.MenuName] = item.MenuName;
                dt.Rows.Add(dr);
            }

            chkBoxListBackend.DataSource = dt;
            chkBoxListBackend.DisplayMember = RolePermissionMasterModelCont.MenuName;
            chkBoxListBackend.ValueMember = RolePermissionMasterModelCont.MenuID;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chkBoxListBackend.SetItemChecked(i, false);
            }

            DataTable dt_FE = new DataTable();
            dt_FE.Columns.Add(RolePermissionMasterModelCont.MenuID, typeof(int));
            dt_FE.Columns.Add(RolePermissionMasterModelCont.MenuName, typeof(string));

            //Front End Permissions

            var resultFE = (from table1 in _db.tbl_MenuMaster.Where(c => c.MenuType == "FE")//.AsEnumerable()
                         select new
                         { MenuID = table1.MenuID, MenuName = table1.MenuName, SortNo = table1.SortNo }).OrderBy(c => c.SortNo);
            foreach (var item in resultFE)
            {
                DataRow dr = dt_FE.NewRow();
                dr[RolePermissionMasterModelCont.MenuID] = item.MenuID;
                dr[RolePermissionMasterModelCont.MenuName] = item.MenuName;
                dt_FE.Rows.Add(dr);
            }

            chkBoxListFrontend.DataSource = dt_FE;
            chkBoxListFrontend.DisplayMember = RolePermissionMasterModelCont.MenuName;
            chkBoxListFrontend.ValueMember = RolePermissionMasterModelCont.MenuID;
            for (int i = 0; i < dt_FE.Rows.Count; i++)
            {
                chkBoxListFrontend.SetItemChecked(i, false);
            }
        }

        public void FillCheckBoxList(int RolePermissionID)
        {
            DataTable dt_All_Test = new DataTable();

            dt_All_Test.Columns.Add(RolePermissionMasterModelCont.MenuID, typeof(int));
            dt_All_Test.Columns.Add(RolePermissionMasterModelCont.MenuName, typeof(string));
            dt_All_Test.Columns.Add(RolePermissionMasterModelCont.ViewRecords, typeof(bool));
            dt_All_Test.Columns.Add(RolePermissionMasterModelCont.SortNo, typeof(int));

            var result_All = (from table1 in _db.tbl_MenuMaster.Where(c=>c.MenuType == "BE")//.AsEnumerable()
                             select new
                             {
                                 MenuID = table1.MenuID,
                                 MenuName = table1.MenuName,
                                 ViewRecords = false,
                                 SortNo = table1.SortNo
                             }).OrderBy(c => c.SortNo);
            foreach (var item in result_All)
            {
                DataRow dr = dt_All_Test.NewRow();

                dr[RolePermissionMasterModelCont.MenuID] = item.MenuID;
                dr[RolePermissionMasterModelCont.MenuName] = item.MenuName;
                dr[RolePermissionMasterModelCont.ViewRecords] = item.ViewRecords;
                dr[RolePermissionMasterModelCont.SortNo] = item.SortNo;
                dt_All_Test.Rows.Add(dr);
            }

            DataTable dt_Selectd_Test = new DataTable();

            dt_Selectd_Test.Columns.Add(RolePermissionMasterModelCont.MenuID, typeof(int));
            dt_Selectd_Test.Columns.Add(RolePermissionMasterModelCont.MenuName, typeof(string));
            dt_Selectd_Test.Columns.Add(RolePermissionMasterModelCont.ViewRecords, typeof(bool));


            var result_Selected = from table1 in _db.tbl_MenuMaster//.AsEnumerable()
                                  join table2 in _db.tbl_RolePermission//.AsEnumerable()
                                  on table1.MenuID equals table2.MenuID
                                  where table2.RoleID == RolePermissionID
                                  select new
                                  {
                                      MenuID = table1.MenuID,
                                      MenuName = table1.MenuName,
                                      ViewRecords = table2.ViewRecords
                                  };
            foreach (var item in result_Selected)
            {
                DataRow dr = dt_Selectd_Test.NewRow();

                dr[RolePermissionMasterModelCont.MenuID] = item.MenuID;
                dr[RolePermissionMasterModelCont.MenuName] = item.MenuName;
                dr[RolePermissionMasterModelCont.ViewRecords] = item.ViewRecords;
                dt_Selectd_Test.Rows.Add(dr);
            }

            if (dt_All_Test.Rows.Count > 0)
            {
                chkBoxListBackend.DataSource = dt_All_Test;
                chkBoxListBackend.DisplayMember = RolePermissionMasterModelCont.MenuName;
                chkBoxListBackend.ValueMember = RolePermissionMasterModelCont.MenuID;

                if (dt_All_Test.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_All_Test.Rows.Count; i++)
                    {
                        chkBoxListBackend.SetItemChecked(i, false);
                    }
                }

                if (dt_Selectd_Test.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_All_Test.Rows.Count; i++)
                    {
                        for (int z = 0; z < dt_Selectd_Test.Rows.Count; z++)
                        {
                            if (dt_All_Test.Rows[i][RolePermissionMasterModelCont.MenuID].ToString() == dt_Selectd_Test.Rows[z][RolePermissionMasterModelCont.MenuID].ToString())
                            {
                                if (dt_Selectd_Test.Rows[z][RolePermissionMasterModelCont.ViewRecords].ToString() == true.ToString())
                                    chkBoxListBackend.SetItemChecked(i, true);
                            }
                        }

                    }
                }
            }


            //Front End Permissions

            DataTable dt_All_TestFE = new DataTable();

            dt_All_TestFE.Columns.Add(RolePermissionMasterModelCont.MenuID, typeof(int));
            dt_All_TestFE.Columns.Add(RolePermissionMasterModelCont.MenuName, typeof(string));
            dt_All_TestFE.Columns.Add(RolePermissionMasterModelCont.ViewRecords, typeof(bool));
            dt_All_TestFE.Columns.Add(RolePermissionMasterModelCont.SortNo, typeof(int));

            var result_AllFE = (from table1 in _db.tbl_MenuMaster.Where(c => c.MenuType == "FE")//.AsEnumerable()
                             select new
                             {
                                 MenuID = table1.MenuID,
                                 MenuName = table1.MenuName,
                                 ViewRecords = false,
                                 SortNo = table1.SortNo
                             }).OrderBy(c => c.SortNo);
            foreach (var item in result_AllFE)
            {
                DataRow dr = dt_All_TestFE.NewRow();

                dr[RolePermissionMasterModelCont.MenuID] = item.MenuID;
                dr[RolePermissionMasterModelCont.MenuName] = item.MenuName;
                dr[RolePermissionMasterModelCont.ViewRecords] = item.ViewRecords;
                dt_All_TestFE.Rows.Add(dr);
            }

            if (dt_All_TestFE.Rows.Count > 0)
            {
                chkBoxListFrontend.DataSource = dt_All_TestFE;
                chkBoxListFrontend.DisplayMember = RolePermissionMasterModelCont.MenuName;
                chkBoxListFrontend.ValueMember = RolePermissionMasterModelCont.MenuID;

                if (dt_All_TestFE.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_All_TestFE.Rows.Count; i++)
                    {
                        chkBoxListFrontend.SetItemChecked(i, false);
                    }
                }

                if (dt_Selectd_Test.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_All_TestFE.Rows.Count; i++)
                    {
                        for (int z = 0; z < dt_Selectd_Test.Rows.Count; z++)
                        {
                            if (dt_All_TestFE.Rows[i][RolePermissionMasterModelCont.MenuID].ToString() == dt_Selectd_Test.Rows[z][RolePermissionMasterModelCont.MenuID].ToString())
                            {
                                if (dt_Selectd_Test.Rows[z][RolePermissionMasterModelCont.ViewRecords].ToString() == true.ToString())
                                    chkBoxListFrontend.SetItemChecked(i, true);
                            }
                        }

                    }
                }
            }
        }

        public void UpdateLog()
        {
            FrmMetroMaster objFrmMetroMaster = new FrmMetroMaster();
            objFrmMetroMaster.ChangeSyncStatus("tbl_RolePermission");
        }
        #endregion

        private void RoleGrdView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
