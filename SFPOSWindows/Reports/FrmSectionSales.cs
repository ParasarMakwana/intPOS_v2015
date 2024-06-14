using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using SFPOSWindows.CrystalReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.Frontend
{
    public partial class FrmSectionSales : MetroForm
    {
        SectionService _SectionService = new SectionService();
        List<SectionMasterModel> lstSectionMasterModel = new List<SectionMasterModel>();
        long PrimaryID = 0;
        public FrmSectionSales()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            lstSectionMasterModel = _SectionService.GetAllSection();
            SectionSaleGrdView.DataSource = lstSectionMasterModel
                                        .Select(o => new { SectionID = o.SectionID, SectionName = o.SectionName }).ToList();
            SectionSaleGrdView.Columns[SectionMasterModelCont.SectionID].Visible = false;
            SectionSaleGrdView.Columns[SectionMasterModelCont.SectionName].HeaderText = "Name";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SectionRdlcReport objSectionRdlcReport = new SectionRdlcReport();
            objSectionRdlcReport.SectionID = PrimaryID;
            objSectionRdlcReport.startdate = startDate.Value.Date;
            objSectionRdlcReport.enddate = endDate.Value.Date;
            objSectionRdlcReport.ShowDialog();
        }

        private void SectionSaleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PrimaryID = Convert.ToInt64(SectionSaleGrdView.Rows[e.RowIndex].Cells[SectionMasterModelCont.SectionID].Value.ToString());
                btnOK.Enabled = true;
            }
        }
    }
}
