using MetroFramework.Forms;
using SFPOS.BAL;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using SFPOS.Entities.MasterDataClasses;
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
    public partial class FrmVendorSales : MetroForm
    {
        private VendorService _VendorService = new VendorService();
        
        List<VendorMasterModel> LstVendorMasterModel = new List<VendorMasterModel>();
        long PrimaryID = 0;
        public FrmVendorSales()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            LstVendorMasterModel = _VendorService.GetAllVendor();
            DepartmentSaleGrdView.DataSource = LstVendorMasterModel
                                        .Select(o => new { VendorID = o.VendorID, VendorName = o.VendorName }).ToList();
            DepartmentSaleGrdView.Columns[VendorMasterModelCont.VendorID].Visible = false;
            DepartmentSaleGrdView.Columns[VendorMasterModelCont.VendorName].HeaderText = "Name";
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            VendorRdlcReport objVendorRdlcReport = new VendorRdlcReport();
            objVendorRdlcReport.VendorID = PrimaryID;
            objVendorRdlcReport.startdate = startDate.Value.Date;
            objVendorRdlcReport.enddate = endDate.Value.Date;
            objVendorRdlcReport.ShowDialog();
        }

        private void DepartmentSaleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PrimaryID = Convert.ToInt64(DepartmentSaleGrdView.Rows[e.RowIndex].Cells[VendorMasterModelCont.VendorID].Value.ToString());
                btnOK.Enabled = true;
            }
        }
    }
}
