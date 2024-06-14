using LiveCharts;
using LiveCharts.Wpf;
using SFPOS.BAL.ReportServices;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;


namespace SFPOSWindows.Reports
{
    public partial class FrmSalesChart : Form
    {
        CategoryWiseSaleService _CategoryWiseSaleService = new CategoryWiseSaleService();
        List<CategoryWiseSale_ResultModel> lstDayMonthYearSaleReport_Result = new List<CategoryWiseSale_ResultModel>();

        LastDaySaleService _LastDaySaleService = new LastDaySaleService();
        List<LastDaysSalesReport_ResultModel> lstLastDaysSalesReport_ResultModel = new List<LastDaysSalesReport_ResultModel>();

        public FrmSalesChart()
        {
            InitializeComponent();
            LoadPieChart();
        }

        public void LoadPieChart()
        {
            Func<LiveCharts.ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);


            //pieChart1.Series = new LiveCharts.SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Maria",
            //        Values = new ChartValues<double> {3},
            //        PushOut = 15,
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Charles",
            //        Values = new ChartValues<double> {4},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Frida",
            //        Values = new ChartValues<double> {6},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    },
            //    new PieSeries
            //    {
            //        Title = "Frederic",
            //        Values = new ChartValues<double> {2},
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    }
            //};

            for (int i = 0; i < 5; i++)
            {
                var PieSeries = new PieSeries();
                PieSeries.Title = "Frederic";
                PieSeries.Values = new ChartValues<double> { i + 5 };
                PieSeries.DataLabels = true;
                PieSeries.LabelPoint = labelPoint;
                pieChart1.Series.Add(PieSeries);
            }

            pieChart1.LegendLocation = LegendLocation.Bottom;

        }
    }
}
