using SFPOS.BAL.MasterDataServices;
using SFPOS.Common;
using SFPOS.DAL;
using SFPOS.Entities.MasterDataClasses;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SFPOSWindows.MasterForm
{

    public partial class FrmLabelPrint : Form
    {
        
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        public FrmLabelPrint()
        {
            InitializeComponent();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext _db = SFPOSDataContext.Create(XMLData.DbConnectionString);
                if (rBtnUpcCode.Checked)
                {
                    var objProductDetails = (from pm in _db.tbl_ProductMaster.Where(X => X.UPCCode == txtUPCCode.Text && X.IsDelete == false)
                                             select new
                                             {
                                                 UPCCode = pm.UPCCode,
                                                 ProductName = pm.ProductName,
                                                 Price = pm.Price
                                             }).ToList();

                    if (objProductDetails != null)
                    {
                        PoNumberGrdView.DataSource = objProductDetails;
                        btnPrint.Visible = true;
                        #region BarCode

                        string barCode = objProductDetails.FirstOrDefault().UPCCode;
                        lblName.Text = objProductDetails.FirstOrDefault().ProductName;
                        lblPrice.Text = "$ " + objProductDetails.FirstOrDefault().Price;


                        Bitmap bitMap = new Bitmap(barCode.Length * 40, 80);

                        using (Graphics graphics = Graphics.FromImage(bitMap))
                        {
                            Font oFont = new Font("IDAutomationHC39M", 16);
                            PointF point = new PointF(2f, 2f);
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                            graphics.DrawString(barCode, oFont, blackBrush, point);
                        }
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bitMap.Save(ms, ImageFormat.Png);
                            pictureBox1.Image = bitMap;
                            //pictureBox2.Height = bitMap.Height;
                            //pictureBox2.Width = bitMap.Width;
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("UPCCode is not valid!!");
                    }
                }
                else if (rBtnPoNumber.Checked)
                {
                    var objProductDetails = (from PH in _db.tbl_PurchaseHeader
                                             join PL in _db.tbl_PurchaseLine on PH.PurchaseHeaderID equals PL.PurchaseHeaderID
                                             join PM in _db.tbl_ProductMaster on PL.ProductID equals PM.ProductID
                                             where PH.PONumber == txtUPCCode.Text
                                             select new
                                             {
                                                 UPCCode = PM.UPCCode,
                                                 ProductName = PM.ProductName,
                                                 Price = PM.Price
                                             }).ToList();

                    if (objProductDetails != null)
                    {
                        btnPrint.Visible = true;
                        PoNumberGrdView.DataSource = objProductDetails;
                        //int Counter = 1;
                        //var pnlLocX = 10;
                        //var pnlLocY = 165;
                        //var pnlSizeX = 298;
                        //var pnlSizeY = 129;

                        //var pixboxLocX = 3; 
                        //var pixboxLocY = 49;
                        //var pixboxSizeX = 292; 
                        //var pixboxSizeY = 80;

                        //var lblLocX = 3;
                        //var lblLocY = 4;
                        //var lblSizeX = 182; 
                        //var lblSizeY = 17;

                        //var lblPriceLocX = 2;
                        //var lblPriceLocY = 29;
                        //var lblPriceSizeX = 182; 
                        //var lblPriceSizeY = 17;


                        //#region BarCode
                        //for (int row = 0; row < objProductDetails.Count; row++)
                        //{
                        //    var dynamicPanel = new Panel();
                        //    dynamicPanel.Name = "pnl" + row.ToString();
                        //    dynamicPanel.Size = new Size(pnlSizeX, pnlSizeY);
                        //    dynamicPanel.Location = new Point(pnlLocX, pnlLocY);
                        //    dynamicPanel.Cursor = Cursors.No;
                        //    dynamicPanel.BorderStyle = BorderStyle.FixedSingle;

                        //    string barCode = objProductDetails[row].UPCCode;

                        //    var dynamicLabel = new Label();
                        //    dynamicLabel.Name = "lblName" + row.ToString();
                        //    dynamicLabel.Size = new Size(lblPriceSizeX, lblPriceSizeY);
                        //    dynamicLabel.Location = new Point(lblLocX, lblLocY);
                        //    dynamicLabel.Text = objProductDetails[row].ProductName;
                        //    dynamicPanel.Controls.Add(dynamicLabel);

                        //    var dynamicLabelPrice = new Label();
                        //    dynamicLabelPrice.Name = "lblPrice" + row.ToString();
                        //    dynamicLabelPrice.Size = new Size(lblSizeX, lblSizeY);
                        //    dynamicLabelPrice.Location = new Point(lblPriceLocX, lblPriceLocY);
                        //    dynamicLabelPrice.Text = objProductDetails[row].ProductName;
                        //    dynamicLabelPrice.Text = "$ " + objProductDetails[row].Price;
                        //    dynamicPanel.Controls.Add(dynamicLabelPrice);

                        //    Bitmap bitMap = new Bitmap(barCode.Length * 40, 80);

                        //    using (Graphics graphics = Graphics.FromImage(bitMap))
                        //    {
                        //        Font oFont = new Font("IDAutomationHC39M", 16);
                        //        PointF point = new PointF(2f, 2f);
                        //        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        //        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        //        graphics.DrawString(barCode, oFont, blackBrush, point);
                        //    }
                        //    using (MemoryStream ms = new MemoryStream())
                        //    {
                        //        bitMap.Save(ms, ImageFormat.Png);
                        //        var picturebox = new PictureBox();
                        //        picturebox.Location = new Point(pixboxLocX, pixboxLocY);
                        //        picturebox.Name = "pic" + row.ToString();
                        //        picturebox.Size = new Size(pixboxSizeX, pixboxSizeY);
                        //        dynamicPanel.Controls.Add(picturebox);
                        //    }
                        //    pnlLocX += pnlSizeX + 50;
                        //    if (Counter % 5 == 0)
                        //    {
                        //        pnlLocY += pnlSizeY + 50;
                        //        pnlLocX = 50;
                        //    }
                        //    Controls.Add(dynamicPanel);
                        //    Counter++;
                        //}
                        //#endregion
                    }
                    else
                    {
                        MessageBox.Show("PO Number is not valid!!");
                    }
                }
                else
                {
                    MessageBox.Show("Please make any selection.!!");
                }
            }
            catch(Exception ex)
            {
                _ExceptionLogService.AddExceptionLog(ex.GetType().Name, ex.Message, CommonTextBoxs.frmOrderScanner + ex.StackTrace, ex.LineNumber());
            }
        }

        private void rBtnUpcCode_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnUpcCode.Checked)
            {
                txtUPCCode.Text = "";
                txtUPCCode.WaterMark = "UPC Number";
                btnPrint.Visible = false;
                PoNumberGrdView.DataSource = null;
            }
            else if(rBtnPoNumber.Checked)
            {
                txtUPCCode.Text = "";
                txtUPCCode.WaterMark = "PO Number";
                btnPrint.Visible = false;
                PoNumberGrdView.DataSource = null;                
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Barcode printer is not connected.!");
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add();
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }
    }
}
