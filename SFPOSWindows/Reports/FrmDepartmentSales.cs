using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.Entities;
using SFPOSWindows.CrystalReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmDepartmentSales : MetroForm
    {
        private DepartmentService _DepartmentService = new DepartmentService();
        DepartmentMasterModel objDepartmentMasterModel = new DepartmentMasterModel();
        List<DepartmentMasterModel> LstDepartmentMasterModel = new List<DepartmentMasterModel>();
        long PrimaryID = 0;

        public FrmDepartmentSales()
        {
            InitializeComponent();
        }

        public void loaddata()
        {
            LstDepartmentMasterModel = _DepartmentService.GetAllDepartment();
            DepartmentSaleGrdView.DataSource = LstDepartmentMasterModel
                                        .Select(o => new { DepartmentID = o.DepartmentID, DepartmentName = o.DepartmentName }).ToList();
            DepartmentSaleGrdView.Columns[DepartmentMasterModelCont.DepartmentID].Visible = false;
            DepartmentSaleGrdView.Columns[DepartmentMasterModelCont.DepartmentName].HeaderText = "Name";
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            DepartmentRdlcReport objDepartmentRdlcReport = new DepartmentRdlcReport();
            objDepartmentRdlcReport.DepartmentID = PrimaryID;
            objDepartmentRdlcReport.startdate = startDate.Value.Date;
            objDepartmentRdlcReport.enddate = endDate.Value.Date;
            objDepartmentRdlcReport.ShowDialog();
        }

        private void DepartmentSaleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PrimaryID = Convert.ToInt64(DepartmentSaleGrdView.Rows[e.RowIndex].Cells[DepartmentMasterModelCont.DepartmentID].Value.ToString());
                btnOK.Enabled = true;
            }
        }
    }
}
