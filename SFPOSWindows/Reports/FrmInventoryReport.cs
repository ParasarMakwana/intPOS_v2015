using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
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

namespace SFPOSWindows.Reports
{
    public partial class FrmInventoryReport : Form
    {
        List<InventoryReport_Result> lstInventoryReport_Result = new List<InventoryReport_Result>();
        InventoryService _InventoryService = new InventoryService();

        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public FrmInventoryReport()
        {
            InitializeComponent();
        }
        public void dataLoad(string fromDate, string ToDate)
        {
            try
            {
                lstInventoryReport_Result = _InventoryService.GetAllInventory(fromDate, ToDate);
                InventoryGridView.DataSource = lstInventoryReport_Result;
                InventoryGridView.Columns[ProductMasterModelCont.ProductID].Visible = false;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmInventoryReport + ex.StackTrace, ex.LineNumber());
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = CommonModelCont.EmptyString;
                string ToDate = CommonModelCont.EmptyString;
                fromDate = datePickerStartDate.Value.ToShortDateString();
                ToDate = datePickerEndDate.Value.ToShortDateString();
                dataLoad(fromDate, ToDate);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.FrmInventoryReport + ex.StackTrace, ex.LineNumber());
            }
}
    }
}
