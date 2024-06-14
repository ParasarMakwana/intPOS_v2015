using MetroFramework.Forms;
using SFPOS.BAL.MasterDataServices;
using SFPOS.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace SFPOSWindows.MasterForm
{
    public partial class FrmLastSyncStatus : MetroForm
    {
        static public int time = 0;
        static public int TableCount = 0;
        static public int temptime = 0;
        static public int TotalTable = 0;
        static public int DoneTable = 0;
        static public int PendingTable = 0;

        public FrmLastSyncStatus()
        {
            InitializeComponent();
            LoadData();
            timer1.Interval = 1;
            timer1.Start();
        }

        public void LoadData()
        {
            try
            {
                LastSyncStatusService _LastSyncStatusService = new LastSyncStatusService();
                List<LastSyncStatusMasterModel> lstLastSyncStatusMasterModel = new List<LastSyncStatusMasterModel>();
                lstLastSyncStatusMasterModel = _LastSyncStatusService.GetAllLastSyncStatus();
                DataTable dt = new DataTable();

                //dt.Columns.Add("CounterNo", typeof(long));
                dt.Columns.Add("CounterIP", typeof(string));
                dt.Columns.Add("TblName", typeof(string));
                dt.Columns.Add("UpdatedDate", typeof(DateTime));
                dt.Columns.Add("IsSync", typeof(bool));
                dt.Columns.Add("SyncDate", typeof(DateTime));
                dt.Columns.Add("SyncStatus", typeof(string));


                foreach (var item in lstLastSyncStatusMasterModel)
                {
                    DataRow dr = dt.NewRow();
                    //dr["CounterNo"] = item.CounterNo;
                    dr["CounterIP"] = item.CounterIP;
                    dr["TblName"] = item.TblName;
                    dr["UpdatedDate"] = item.UpdatedDate == null ? DateTime.Now : item.UpdatedDate;
                    dr["IsSync"] = item.IsSync;
                    dr["SyncDate"] = item.SyncDate == null ? DateTime.Now : item.SyncDate;
                    dr["SyncStatus"] = item.SyncStatus;
                    TotalTable++;
                    if (item.IsSync == true)
                        DoneTable++;
                    if (item.IsSync == false)
                        PendingTable++;
                    dt.Rows.Add(dr);

                }
                LastSyncGrdView.DataSource = dt;
            }
            catch (Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TableCount = DoneTable * 100 / TotalTable;
                if (TableCount == 0)
                    TableCount = 1;
                time = TableCount;
                if (temptime <= time)
                {
                    temptime = temptime + 1;
                }
                lblpre.Text = temptime.ToString() + " % ";
                progressBar.Value = temptime;
                if (progressBar.Value >= 100)
                {
                    time = 0;
                    temptime = 0;
                    TotalTable = 0;
                    DoneTable = 0;
                    PendingTable = 0;
                    timer1.Stop();
                }
            }
            catch (Exception ex)
            {
                time = 0;
                temptime = 0;
                TotalTable = 0;
                DoneTable = 0;
                PendingTable = 0;
                timer1.Stop();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            time = 0;
            temptime = 0;
            TotalTable = 0;
            DoneTable = 0;
            PendingTable = 0;
            LoadData();
            timer1.Interval = 1;
            timer1.Start();
        }
    }
}
