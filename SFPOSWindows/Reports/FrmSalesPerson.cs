using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities.MasterDataClasses;
using SFPOSWindows.RDLCReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOSWindows.Reports
{
    public partial class FrmSalesPerson : MetroForm
    {
        private EmployeeService _EmployeeService = new EmployeeService();
        List<EmployeeMasterModel> lstEmployeeMasterModel = new List<EmployeeMasterModel>();
        long PrimaryID = 0;

        public FrmSalesPerson()
        {
            InitializeComponent();
            loaddata();
        }

        public void loaddata()
        {
            lstEmployeeMasterModel = _EmployeeService.GetAllEmployee();
            EmployeeSaleGrdView.DataSource = lstEmployeeMasterModel.
                                                Select(o => new { EmployeeID = o.EmployeeID, Name = o.FirstName }).ToList();
            EmployeeSaleGrdView.Columns[EmployeeMasterModelCont.EmployeeID].Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SalesPersonRdlcReport objSalesPersonRdlcReport = new SalesPersonRdlcReport();
            objSalesPersonRdlcReport.EmployeeID = PrimaryID;
            objSalesPersonRdlcReport.startdate = startDate.Value.Date;
            objSalesPersonRdlcReport.enddate = endDate.Value.Date;
            objSalesPersonRdlcReport.ShowDialog();
        }

        private void EmployeeSaleGrdView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                PrimaryID = Convert.ToInt64(EmployeeSaleGrdView.Rows[e.RowIndex].Cells[EmployeeMasterModelCont.EmployeeID].Value.ToString());
                btnOK.Enabled = true;
            }
        }
    }
}
