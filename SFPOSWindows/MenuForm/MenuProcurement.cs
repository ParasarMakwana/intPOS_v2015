using SFPOS.Common;
using SFPOSWindows.MasterForm;
using SFPOSWindows.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SFPOSWindows.MenuForm
{
    public partial class frmMenuProcurement : Form
    {
        public frmMenuProcurement()
        {
            InitializeComponent();
            //PictureWatermark = new PictureBox();
            //PictureWatermark.SizeMode = PictureBoxSizeMode.AutoSize;
            //PictureWatermark.Anchor = AnchorStyles.None;
            //CenterPictureBox(PictureWatermark, Resources.sf_);
            //if (XMLData.IsDemoVersion == 1)
            //{
            //    PictureWatermark.Image = SFPOSWindows.Properties.Resources.intPOSDemo_lightX;
            //    PictureWatermark.Refresh();
            //}
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            FrmVendor objFrmVendor = new FrmVendor();
            if (!CheckForm(objFrmVendor))
            {
                objFrmVendor.dataLoad();

                if (objFrmVendor.VendorGrdView.Rows.Count >= 0)
                {
                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmVendor.VendorGrdView.Columns.Add(imgEdit);
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmVendor.VendorGrdView.Columns.Add(imgDelete);
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                PanelGrid.Controls.Clear();
                objFrmVendor.TopLevel = false;
                PanelGrid.Controls.Add(objFrmVendor);
                objFrmVendor.FormBorderStyle = FormBorderStyle.None;
                objFrmVendor.Width = PanelGrid.Width;
                objFrmVendor.Height = PanelGrid.Height;
                objFrmVendor.Show();
            }
            else
            {
                Application.OpenForms[objFrmVendor.Name].Focus();
            }
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            PanelGrid.Controls.Clear();
            PictureWatermark.Visible = false;
            FrmPurchaseOrders objFrmPurchaseOrders = new FrmPurchaseOrders();
            if (!CheckForm(objFrmPurchaseOrders))
            {
                objFrmPurchaseOrders.dataLoad();

                if (objFrmPurchaseOrders.PurchaseOrderGrdView.Rows.Count >= 0)
                {
                    DataGridViewImageColumn imgHeaderLine = new DataGridViewImageColumn();
                    var add = new Bitmap(Resources.add);
                    imgHeaderLine.Image = add;
                    objFrmPurchaseOrders.PurchaseOrderGrdView.Columns.Add(imgHeaderLine);
                    imgHeaderLine.ToolTipText = AlertMessages.PurchaseHeaderLineToolTip;
                    imgHeaderLine.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgEdit = new DataGridViewImageColumn();
                    var bmpEdit = new Bitmap(Resources.edit);
                    imgEdit.Image = bmpEdit;
                    objFrmPurchaseOrders.PurchaseOrderGrdView.Columns.Add(imgEdit);
                    imgEdit.ToolTipText = AlertMessages.DeleteToolTip;
                    imgEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    DataGridViewImageColumn imgDelete = new DataGridViewImageColumn();
                    var bmp = new Bitmap(Resources.delete);
                    imgDelete.Image = bmp;
                    objFrmPurchaseOrders.PurchaseOrderGrdView.Columns.Add(imgDelete);
                    imgDelete.ToolTipText = AlertMessages.DeleteToolTip;
                    imgDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
               // objFrmPurchaseOrders.cmbVendor();
                objFrmPurchaseOrders.TopLevel = false;
                PanelGrid.Controls.Add(objFrmPurchaseOrders);
                objFrmPurchaseOrders.FormBorderStyle = FormBorderStyle.None;
                objFrmPurchaseOrders.Width = PanelGrid.Width;
                objFrmPurchaseOrders.Height = PanelGrid.Height;
                objFrmPurchaseOrders.Show();
            }
            else
            {
                Application.OpenForms[objFrmPurchaseOrders.Name].Focus();
            }
        }

        private void btnLabelPrint_Click(object sender, EventArgs e)
        {
            //PanelGrid.Controls.Clear();
            //FrmLabelPrint objFrmLabelPrint = new FrmLabelPrint();
            //if (!CheckForm(objFrmLabelPrint))
            //{
            //    objFrmLabelPrint.TopLevel = false;
            //    PanelGrid.Controls.Add(objFrmLabelPrint);
            //    objFrmLabelPrint.FormBorderStyle = FormBorderStyle.None;
            //    objFrmLabelPrint.Width = PanelGrid.Width;
            //    objFrmLabelPrint.Show();
            //}
            //else
            //{
            //    Application.OpenForms[objFrmLabelPrint.Name].Focus();
            //}
        }

        private void CenterPictureBox(PictureBox picBox, Bitmap picImage)
        {
            picBox.Image = picImage;
            picBox.Location = new Point((this.Width / 2) - (picImage.Width / 2),
                                        (this.Height / 2) - (picImage.Height / 2));
            picBox.Refresh();
        }
    }
}

