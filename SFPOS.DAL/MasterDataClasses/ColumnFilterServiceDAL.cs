using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFPOS.DAL.MasterDataClasses
{
   public class ColumnFilterServiceDAL
    {

        private SFPOS.DAL.DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);

        public tbl_FilterColumnsModel AddProductColumnFilter(tbl_FilterColumnsModel objtbl_FilterColumnsModel)
        {
            try
            {
                tbl_FilterColumns objtbl_FilterColumns = new tbl_FilterColumns();
                var existtbl_FilterColumns = _db.tbl_FilterColumns.Where(p => p.FilterColumnsName == objtbl_FilterColumnsModel.FilterColumnsName && p.CreatedBy==objtbl_FilterColumnsModel.CreatedBy && p.FilterPageName == objtbl_FilterColumnsModel.FilterPageName).FirstOrDefault();
                if (existtbl_FilterColumns == null)//ADD
                {
                    objtbl_FilterColumns.FilterColumnsName = objtbl_FilterColumnsModel.FilterColumnsName;
                    objtbl_FilterColumns.FilterPageName = objtbl_FilterColumnsModel.FilterPageName;
                    objtbl_FilterColumns.IsActive = objtbl_FilterColumnsModel.IsActive;
                    objtbl_FilterColumns.IsMaster = objtbl_FilterColumnsModel.IsMaster;
                    objtbl_FilterColumns.SeqNo = objtbl_FilterColumnsModel.SeqNo;
                    objtbl_FilterColumns.CreatedBy = objtbl_FilterColumnsModel.CreatedBy;
                    objtbl_FilterColumns.UpdatedBy = objtbl_FilterColumnsModel.UpdatedBy;
                    objtbl_FilterColumns.CreatedDate = objtbl_FilterColumnsModel.CreatedDate;
                    objtbl_FilterColumns.UpdatedDate = objtbl_FilterColumnsModel.UpdatedDate;

                    _db.tbl_FilterColumns.Add(objtbl_FilterColumns);
                    objtbl_FilterColumnsModel.FilterColumnsID = objtbl_FilterColumns.FilterColumnsID;
                }
                else //EDIT
                {
                    if (existtbl_FilterColumns != null)
                    {
                        existtbl_FilterColumns.IsActive = objtbl_FilterColumnsModel.IsActive;
                    }
                }
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                string ex = e.Message;
            }
            return objtbl_FilterColumnsModel;
        }


        public List<string> GetFilterColumnsList(string Frmname)
        {
            List<string> result = null;
            try
            {
                //Existing
                //result = _db.tbl_FilterColumns.Where(x => x.IsActive == true && x.FilterPageName == Frmname).Select(a => a.FilterColumnsName).ToList();

                //New
                result = _db.tbl_FilterColumns.Where(x => x.IsActive == true && x.FilterPageName == Frmname && x.CreatedBy == LoginInfo.UserId && x.IsMaster == false).Select(a => a.FilterColumnsName).ToList();

            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }

        public List<ClsFilterCheckBox> GetFilterMasterColumnsList(string Frmname)
        {
            List<ClsFilterCheckBox> result = null;
            try
            {
                result = _db.tbl_FilterColumns.Where(x => x.IsActive == true && x.FilterPageName == Frmname && x.IsMaster == true).Select(s => new ClsFilterCheckBox { FilterColumnsName = s.FilterColumnsName, KeySeq = (s.SeqNo == null ? 0 : s.SeqNo) }).ToList();

            }
            catch (Exception e)
            {
                string ex = e.Message;
            }
            return result;
        }
        
    }
}
