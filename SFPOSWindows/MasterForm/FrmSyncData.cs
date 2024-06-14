using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmSyncData : Form
    {
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmSyncData()
        {
            InitializeComponent();
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    PictureWatermark.Image = SFPOSWindows.Properties.Resources.intPOSDemo_light;
            //    PictureWatermark.Refresh();
            //}
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
            var UpdatedTableName = _db.tbl_UpdateLog.Where(x => x.IsChange == true).ToList();
            foreach (tbl_UpdateLog tbl in UpdatedTableName)
            {
                tbl.IsChange = false;
                tbl.IsSync = false;
                tbl.UpdatedDate = DateTime.Now;
                tbl.UpdatedBy = LoginInfo.UserId;
            }
            _db.SaveChanges();
        }

        private void btnLastSyncStatus_Click(object sender, EventArgs e)
        {
            FrmLastSyncStatus obj = new FrmLastSyncStatus();
            obj.ShowDialog();
        }

        private void btnScaleSync_Click(object sender, EventArgs e)
        {
            FrmScaleSync obj = new FrmScaleSync();
            obj.ShowDialog();
        }

        public void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                btnCheckUpdate.Text = "Please wait...";
                btnCheckUpdate.Enabled = false;
                //ReleaseResponse _ReleaseResponse = new ReleaseResponse();
                APIUtility _APIUtility = new APIUtility();

                DataTable dt = _APIUtility.CheckRelease("intPOS01");
                if (dt != null && dt.Rows.Count > 0)
                {
                    string StoreId = "0";
                    SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                    int j = 0;
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        int i = int.Parse(dtRow["ReleaseID"].ToString());
                        var ReleaseData = _db.tbl_ReleaseMaster.Where(x => x.ReleaseID == i).FirstOrDefault();
                        if (ReleaseData == null)
                        {
                            ReleaseData = new tbl_ReleaseMaster();
                            StoreId= dtRow["StoreId"].ToString();
                            ReleaseData.ReleaseID = Convert.ToInt32(dtRow["ReleaseID"].ToString());
                            ReleaseData.ReleaseType = dtRow["ReleaseType"].ToString();
                            ReleaseData.Priority = dtRow["Priority"].ToString();
                            ReleaseData.Version = dtRow["Version"].ToString();
                            ReleaseData.Description = dtRow["Description"].ToString();
                            ReleaseData.FilesURL = dtRow["FilesURL"].ToString();
                            ReleaseData.ReleaseDate = Convert.ToDateTime(dtRow["ReleaseDate"]);
                            ReleaseData.CreatedDate = DateTime.Now;
                            ReleaseData.UpdatedDate = null;
                            _db.tbl_ReleaseMaster.Add(ReleaseData);
                            _db.SaveChanges();
                            j++;
                        }
                    }
                    _APIUtility.UpdateStoreReleaseStatus(StoreId);
                    if (j > 0)
                        ClsCommon.MsgBox("Information", "Release information updated, Please update all system.", false);
                    else
                        ClsCommon.MsgBox("Information", "Current version is up to date for this store, Please contact system Administrator if any issues.", false);
                }
                else
                {
                    ClsCommon.MsgBox("Information", "Current version is up to date for this store, Please contact system Administrator if any issues.", false);
                }
                btnCheckUpdate.Text = "Check Update";
                btnCheckUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, "Check Release - btnCheckUpdate_Click" + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
