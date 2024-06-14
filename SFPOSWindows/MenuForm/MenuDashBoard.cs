using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SFPOS.BAL.MasterDataServices;
using SFPOS.BAL.ReportServices;
using SFPOS.Common;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;

namespace SFPOSWindows.MenuForm
{
    public partial class MenuDashBoard : Form
    {
        TotalSalesService _TotalSalesService = new TotalSalesService();
        DepartmentWiseSaleService _DepartmentWiseSaleService = new DepartmentWiseSaleService();
        List<DepartmentWiseSale_ResultModel> lstDayMonthYearSaleReport_Result = new List<DepartmentWiseSale_ResultModel>();
        DayMonthYearSaleService _DayMonthYearSaleService = new DayMonthYearSaleService();
        LastDaySaleService _LastDaySaleService = new LastDaySaleService();
        List<LastDaysSalesReport_ResultModel> lstLastDaysSalesReport_ResultModel = new List<LastDaysSalesReport_ResultModel>();
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();

        public MenuDashBoard()
        {
            InitializeComponent();
            loadGauges();
            LoadPieChart();
            cmbFilter();
            DayChart();
            MonthChart();
            QuarterChart();
            FillLastDaySaleGross();
            FillLastDaySaleDiscount();
        }

        public void loadGauges()
        {
            try
            {
                List<ReportTotalSales_ResultModel> lstReportTotalSales_ResultModel = new List<ReportTotalSales_ResultModel>();
                lstReportTotalSales_ResultModel = _TotalSalesService.GetTotalSales(LoginInfo.StoreID);
                loadTodayGauge(lstReportTotalSales_ResultModel);
                loadMonthGauge(lstReportTotalSales_ResultModel);
                loadYearGauge(lstReportTotalSales_ResultModel);
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }

        }

        public void loadTodayGauge(List<ReportTotalSales_ResultModel> lstReportTotalSales_ResultModel)
        {
            try
            {
                float sales = float.Parse(lstReportTotalSales_ResultModel[0].Totalsales_TODAY.ToString(), CultureInfo.InvariantCulture.NumberFormat);

                angularGaugeToday.Value = sales;
                angularGaugeToday.TicksForeground = Brushes.White;
                angularGaugeToday.Base.Foreground = Brushes.White;
                angularGaugeToday.Base.FontWeight = System.Windows.FontWeights.Bold;
                angularGaugeToday.Base.FontSize = 16;
                angularGaugeToday.SectionsInnerRadius = 0.5;
                if (sales != 0)
                {
                    float FromVal = 0;
                    float Toval = sales * 5;
                    float val = Toval / 3;

                    angularGaugeToday.FromValue = FromVal;
                    angularGaugeToday.ToValue = Toval;
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = val * 1,
                        Fill = new SolidColorBrush(Color.FromRgb(88, 173, 219))
                    });
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = val * 1,
                        ToValue = val * 2,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 121, 164))
                    });
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = val * 2,
                        ToValue = val * 3,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 73, 112))
                    });
                }
                else
                {
                    angularGaugeToday.FromValue = 0;
                    angularGaugeToday.ToValue = 1000;
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = 333,
                        Fill = new SolidColorBrush(Color.FromRgb(88, 173, 219))
                    });
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = 333,
                        ToValue = 666,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 121, 164))
                    });
                    angularGaugeToday.Sections.Add(new AngularSection
                    {
                        FromValue = 666,
                        ToValue = 1000,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 73, 112))
                    });

                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void loadMonthGauge(List<ReportTotalSales_ResultModel> lstReportTotalSales_ResultModel)
        {
            try
            {
                float sales = float.Parse(lstReportTotalSales_ResultModel[0].Totalsales_MONTH.ToString(), CultureInfo.InvariantCulture.NumberFormat);

                angularGaugeMonth.Value = sales;
                angularGaugeMonth.TicksForeground = System.Windows.Media.Brushes.White;
                angularGaugeMonth.Base.Foreground = Brushes.White;
                angularGaugeMonth.Base.FontWeight = System.Windows.FontWeights.Bold;
                angularGaugeMonth.Base.FontSize = 16;
                angularGaugeMonth.SectionsInnerRadius = 0.5;
                if (sales != 0)
                {
                    float FromVal = 0;
                    float Toval = sales * 5;
                    float val = Toval / 3;
                    angularGaugeMonth.FromValue = FromVal;
                    angularGaugeMonth.ToValue = Toval;
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = val * 1,
                        Fill = new SolidColorBrush(Color.FromRgb(221, 112, 94))
                    });
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = val * 1,
                        ToValue = val * 2,
                        Fill = new SolidColorBrush(Color.FromRgb(161, 60, 47))
                    });
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = val * 2,
                        ToValue = val * 3,
                        Fill = new SolidColorBrush(Color.FromRgb(103, 0, 1))
                    });
                }
                else
                {
                    angularGaugeMonth.FromValue = 0;
                    angularGaugeMonth.ToValue = 1000;
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = 333,
                        Fill = new SolidColorBrush(Color.FromRgb(221, 112, 94))
                    });
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = 333,
                        ToValue = 666,
                        Fill = new SolidColorBrush(Color.FromRgb(161, 60, 47))
                    });
                    angularGaugeMonth.Sections.Add(new AngularSection
                    {
                        FromValue = 666,
                        ToValue = 1000,
                        Fill = new SolidColorBrush(Color.FromRgb(103, 0, 1))
                    });
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void loadYearGauge(List<ReportTotalSales_ResultModel> lstReportTotalSales_ResultModel)
        {
            try
            {
                float sales = float.Parse(lstReportTotalSales_ResultModel[0].Totalsales_YEAR.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                angularGaugeYear.Value = sales;
                angularGaugeYear.TicksForeground = System.Windows.Media.Brushes.White;
                angularGaugeYear.Base.Foreground = System.Windows.Media.Brushes.White;
                angularGaugeYear.Base.FontWeight = System.Windows.FontWeights.Bold;
                angularGaugeYear.Base.FontSize = 16;
                angularGaugeYear.SectionsInnerRadius = 0.5;

                if (sales != 0)
                {
                    float FromVal = 0;
                    float Toval = sales * 5;
                    float val = Toval / 3;

                    angularGaugeYear.FromValue = FromVal;
                    angularGaugeYear.ToValue = Toval;
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = val * 1,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243, 67, 54))
                    });
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = val * 1,
                        ToValue = val * 2,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(254, 192, 7))
                    });
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = val * 2,
                        ToValue = val * 3,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153, 204, 0))
                    });
                }
                else
                {
                    angularGaugeYear.FromValue = 0;
                    angularGaugeYear.ToValue = 1000;
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = 0,
                        ToValue = 333,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(243, 67, 54))
                    });
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = 333,
                        ToValue = 666,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(254, 192, 7))
                    });
                    angularGaugeYear.Sections.Add(new AngularSection
                    {
                        FromValue = 666,
                        ToValue = 1000,
                        Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(153, 204, 0))
                    });
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void LoadPieChart()
        {
            try
            {
                lstDayMonthYearSaleReport_Result = _DepartmentWiseSaleService.GetAllDepartmentWiseSales(LoginInfo.StoreID);
                Func<ChartPoint, string> labelPoint = chartPoint =>
                   string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                for (int i = 0; i < lstDayMonthYearSaleReport_Result.Count; i++)
                {
                    var PieSeries = new PieSeries();
                    PieSeries.Title = lstDayMonthYearSaleReport_Result[i].DepartmentName;
                    PieSeries.Values = new ChartValues<decimal> { Convert.ToDecimal(lstDayMonthYearSaleReport_Result[i].TotalSales) };
                    PieSeries.DataLabels = true;
                    PieSeries.LabelPoint = labelPoint;
                    pieChart1.Series.Add(PieSeries);
                }

                pieChart1.LegendLocation = LegendLocation.Bottom;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void cmbFilter()
        {
            try
            {
                cmbDropdown.Items.Insert(0, "Month");
                cmbDropdown.Items.Insert(1, "Year");
                cmbDropdown.Items.Insert(2, "Quarter");
                cmbDropdown.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void DayChart()
        {
            try
            {
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                List<DayMonthYearSaleReport_ResultModel> lstDayMonthYearSaleReport_Result = new List<DayMonthYearSaleReport_ResultModel>();

                int currentMonth = DateTime.Now.Month;
                int Currentyear = DateTime.Now.Year;
                //currentMonth = 12;
                //Currentyear = 2018;
                int days = DateTime.DaysInMonth(Currentyear, currentMonth);

                #region DayChart

                lstDayMonthYearSaleReport_Result = _DayMonthYearSaleService.GetAllDayMontYearSale(LoginInfo.StoreID, currentMonth, Currentyear);
                int x = 1;
                var _Values = new ChartValues<ObservableValue>();

                List<string> listDays = new List<string>();

                for (int i = 0; i < days; i++)
                {
                    decimal TotalSales = 0;
                    var data = lstDayMonthYearSaleReport_Result.Where(o => o.Day == x.ToString()).ToList();
                    if (data.Count > 0)
                    {
                        if (data[0].TotalSales != null)
                        {
                            TotalSales = Convert.ToDecimal(data[0].TotalSales);
                        }
                    }
                    var ov = new ObservableValue();
                    ov.Value = Convert.ToDouble(TotalSales);
                    _Values.Add(ov);
                    listDays.Add(x.ToString());
                    x++;
                }
                cartesianChartDay.Series.Add(new ColumnSeries
                {
                    Values = _Values,
                    DataLabels = true,
                    LabelPoint = point => CommonModelCont.AddDollorSign + point.Y
                });


                cartesianChartDay.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Labels = listDays,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = false
                    },
                    //LabelsRotation = 15
                });

                cartesianChartDay.AxisY.Add(new LiveCharts.Wpf.Axis
                {
                    LabelFormatter = value => CommonModelCont.AddDollorSign + value,
                    Separator = new Separator()
                });
                #endregion
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }

        }

        public void MonthChart()
        {
            try
            {
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                List<DayMonthYearSaleReport_ResultModel> lstDayMonthYearSaleReport_Result = new List<DayMonthYearSaleReport_ResultModel>();

                // int currentMonth = DateTime.Now.Month;
                int Currentyear = DateTime.Now.Year;
                //currentMonth = 12;
                //Currentyear = 2018;
                // int days = DateTime.DaysInMonth(Currentyear, currentMonth);
                var _Values = new ChartValues<ObservableValue>();

                #region MonthChart

                lstDayMonthYearSaleReport_Result = _DayMonthYearSaleService.GetAllDayMontYearSale(LoginInfo.StoreID, 0, Currentyear);
                List<string> listMonths = new List<string>();
                int x = 1;
                for (int i = 0; i < 12; i++)
                {
                    decimal TotalSales = 0;
                    string MonthName = mfi.GetMonthName(x).ToString();
                    var data = lstDayMonthYearSaleReport_Result.Where(o => o.Day == MonthName).ToList();
                    if (data.Count > 0)
                    {
                        if (data[0].TotalSales != null)
                        {
                            TotalSales = Convert.ToDecimal(data[0].TotalSales);
                        }
                    }
                    var ov = new ObservableValue();
                    ov.Value = Convert.ToDouble(TotalSales);
                    _Values.Add(ov);
                    listMonths.Add(MonthName);
                    x++;
                }
                cartesianChartMonth.Series.Add(new ColumnSeries
                {
                    Values = _Values,
                    DataLabels = true,
                    LabelPoint = point => CommonModelCont.AddDollorSign + point.Y
                });


                cartesianChartMonth.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Labels = listMonths,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = false
                    },
                    //LabelsRotation = 15
                });

                cartesianChartMonth.AxisY.Add(new LiveCharts.Wpf.Axis
                {
                    LabelFormatter = value => CommonModelCont.AddDollorSign + value,
                    Separator = new Separator()
                });

                #endregion

            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void QuarterChart()
        {
            try
            {
                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                List<DayMonthYearSaleReport_ResultModel> lstDayMonthYearSaleReport_Result = new List<DayMonthYearSaleReport_ResultModel>();

                //int currentMonth = DateTime.Now.Month;
                int Currentyear = DateTime.Now.Year;
                //currentMonth = 12;
                //Currentyear = 2018;
                //int days = DateTime.DaysInMonth(Currentyear, currentMonth);
                var _Values = new ChartValues<ObservableValue>();

                #region QuarterChart
                lstDayMonthYearSaleReport_Result = _DayMonthYearSaleService.GetAllDayMontYearSale(LoginInfo.StoreID, 0, Currentyear);
                int x = 1;
                List<string> listYear = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    string MonthPrint = CommonModelCont.EmptyString;
                    decimal TotalSales = 0;
                    string MonthName = mfi.GetMonthName(x).ToString();
                    for (int j = 0; j < 3; j++)
                    {
                        MonthName = mfi.GetMonthName(x).ToString();
                        if (j == 0)
                        { MonthPrint = MonthName; }
                        if (j == 2)
                        { MonthPrint = MonthPrint + " - " + MonthName; }

                        var data = lstDayMonthYearSaleReport_Result.Where(o => o.Day == MonthName).ToList();
                        if (data.Count > 0)
                        {
                            if (data[0].TotalSales != null)
                            {
                                TotalSales += Convert.ToDecimal(data[0].TotalSales);
                            }
                        }
                        x++;
                    }
                    var ov = new ObservableValue();
                    ov.Value = Convert.ToDouble(TotalSales);
                    _Values.Add(ov);
                    listYear.Add(MonthPrint);
                }
                cartesianChartYear.Series.Add(new ColumnSeries
                {
                    Values = _Values,
                    DataLabels = true,
                    LabelPoint = point => CommonModelCont.AddDollorSign + point.Y
                });


                cartesianChartYear.AxisX.Add(new Axis
                {
                    Labels = listYear,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = false
                    },
                });

                cartesianChartYear.AxisY.Add(new Axis
                {
                    LabelFormatter = value => CommonModelCont.AddDollorSign + value,
                    Separator = new Separator()
                });

                #endregion
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void FillLastDaySaleGross()
        {
            try
            {
                lstLastDaysSalesReport_ResultModel = _LastDaySaleService.GetLastDaySale(LoginInfo.StoreID, 1);

                var _Values = new ChartValues<ObservableValue>();
                List<string> listDay = new List<string>();
                for (int i = 5; i > 0; i--)
                {
                    decimal TotalSales = 0;
                    string Days = DateTime.Now.Date.AddDays((-i) + 1).Day.ToString();
                    var data = lstLastDaysSalesReport_ResultModel.Where(o => o.Day == Days).ToList();
                    if (data.Count > 0)
                    {
                        if (data[0].TotalSales != null)
                        {
                            TotalSales = Convert.ToDecimal(data[0].TotalSales);
                        }
                    }
                    var ov = new ObservableValue();
                    ov.Value = Convert.ToDouble(TotalSales);
                    listDay.Add(Days);
                    _Values.Add(ov);
                }

                cartesianChartGross.Series.Add(new LineSeries
                {
                    Values = _Values,
                    DataLabels = true,
                    StrokeThickness = 4,
                    PointGeometrySize = 0,
                    LabelPoint = point => CommonModelCont.AddDollorSign + point.Y
                });

                cartesianChartGross.AxisX.Add(new Axis
                {
                    Labels = listDay,
                    Separator = new Separator
                    {
                        Step = 1,
                        IsEnabled = true
                    }
                });

                cartesianChartGross.AxisY.Add(new Axis
                {
                    LabelFormatter = value => CommonModelCont.AddDollorSign + value,
                });

                cartesianChartGross.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        public void FillLastDaySaleDiscount()
        {
            try
            {
                lstLastDaysSalesReport_ResultModel = _LastDaySaleService.GetLastDaySale(LoginInfo.StoreID, 2);
                List<string> listDay = new List<string>();

                var _Values = new ChartValues<ObservableValue>();
                for (int i = 5; i > 0; i--)
                {
                    decimal TotalSales = 0;
                    string Days = DateTime.Now.Date.AddDays((-i) + 1).Day.ToString();
                    var data = lstLastDaysSalesReport_ResultModel.Where(o => o.Day == Days).ToList();
                    if (data.Count > 0)
                    {
                        if (data[0].TotalSales != null)
                        {
                            TotalSales = Convert.ToDecimal(data[0].TotalSales);
                        }
                    }
                    var ov = new ObservableValue();
                    ov.Value = Convert.ToDouble(TotalSales);
                    _Values.Add(ov);
                    listDay.Add(Days);
                }
                //cartesianChartDiscount.Series.Add(new LineSeries
                //{
                //    Values = _Values,
                //    StrokeThickness = 4,
                //    PointGeometrySize = 0,
                //    DataLabels = true,
                //    LabelPoint = point => CommonModelCont.AddDollorSign + point.Y
                //});

                //cartesianChartDiscount.AxisX.Add(new Axis
                //{
                //    Labels = listDay,
                //    Separator = new Separator
                //    {
                //        Step = 1,
                //        IsEnabled = true
                //    }
                //});

                //cartesianChartDiscount.AxisY.Add(new Axis
                //{
                //    LabelFormatter = value => CommonModelCont.AddDollorSign + value,
                //    Separator = new Separator()
                //});


                //cartesianChartDiscount.LegendLocation = LegendLocation.Right;
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }

        private void cmbDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDropdown.SelectedIndex.ToString() == 0.ToString() && cmbDropdown.SelectedIndex.ToString() != null)
                {
                    cartesianChartYear.Visible = false;
                    cartesianChartMonth.Visible = false;
                    cartesianChartDay.Visible = true;
                }
                if (cmbDropdown.SelectedIndex.ToString() == 1.ToString() && cmbDropdown.SelectedIndex.ToString() != null)
                {
                    cartesianChartYear.Visible = false;
                    cartesianChartMonth.Visible = true;
                    cartesianChartDay.Visible = false;
                }
                if (cmbDropdown.SelectedIndex.ToString() == 2.ToString() && cmbDropdown.SelectedIndex.ToString() != null)
                {
                    cartesianChartYear.Visible = true;
                    cartesianChartMonth.Visible = false;
                    cartesianChartDay.Visible = false;
                }
            }
            catch (Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.MenuDashBoard + ex.StackTrace, ex.LineNumber());
            }
        }
    }
}
