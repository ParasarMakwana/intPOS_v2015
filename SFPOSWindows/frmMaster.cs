using SFPOS.Common;
using SFPOSWindows.MenuForm;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SFPOSWindows
{
    public partial class frmMaster : Form
    {
        bool x;
        public frmMaster()
        {
            InitializeComponent();
            menuStrip1.Renderer = new myRenderer();
            x = false;
            dashboardToolStripMenuItem.PerformClick();
            Role();
        }

        private class myRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
            {
                if (!myMenu.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(myMenu);
                    myMenu.Item.ForeColor = Color.Black;
                }
                else
                {
                    Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                    SolidBrush brush = new SolidBrush(Color.FromArgb(0, 174, 219));
                    //var converter = new System.Windows.Media.BrushConverter();
                    //var brush = (Brushes)converter.ConvertFromString("#00AEDB");
                    myMenu.Graphics.FillRectangle(brush, menuRectangle);
                    myMenu.Item.ForeColor = Color.White;
                }
            }
        }

        private bool CheckForm(Form form)
        {
            form = Application.OpenForms[form.Text];
            if (form != null)
                return true;
            else
                return false;
        }

        public void menuClick(int menuNumber)
        {            
            if (menuNumber == 1)
            {
                pic1.Visible = true;
                pic2.Visible = false;
                pic3.Visible = false;
                pic4.Visible = false;
                pic5.Visible = false;
                pic6.Visible = false;
                pic7.Visible = false;
            }
            else if (menuNumber == 2)
            {
                pic1.Visible = false;
                pic2.Visible = true;
                pic3.Visible = false;
                pic4.Visible = false;
                pic5.Visible = false;
                pic6.Visible = false;
                pic7.Visible = false;
            }
            else if (menuNumber == 3)
            {
                pic1.Visible = false;
                pic2.Visible = false;
                pic3.Visible = true;
                pic4.Visible = false;
                pic5.Visible = false;
                pic6.Visible = false;
                pic7.Visible = false;
            }
            else if (menuNumber == 4)
            {
                pic1.Visible = false;
                pic2.Visible = false;
                pic3.Visible = false;
                pic4.Visible = true;
                pic5.Visible = false;
                pic6.Visible = false;
                pic7.Visible = false;
            }
            else if (menuNumber == 5)
            {
                pic1.Visible = false;
                pic2.Visible = false;
                pic3.Visible = false;
                pic4.Visible = false;
                pic5.Visible = true;
                pic6.Visible = false;
                pic7.Visible = false;
            }
            else if (menuNumber == 6)
            {
                pic1.Visible = false;
                pic2.Visible = false;
                pic3.Visible = false;
                pic4.Visible = false;
                pic5.Visible = false;
                pic6.Visible = true;
                pic7.Visible = false;
            }
            else if (menuNumber == 7)
            {
                pic1.Visible = false;
                pic2.Visible = false;
                pic3.Visible = false;
                pic4.Visible = false;
                pic5.Visible = false;
                pic6.Visible = false;
                pic7.Visible = true;
            }
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuDashBoard objfrmDashboard = new MenuDashBoard();
            menuClick(1);
            ActiveMenu("dashboardToolStripMenuItem");
            if (!CheckForm(objfrmDashboard))
            {
                pnlGrid.Controls.Clear();
                objfrmDashboard.TopLevel = false;
                objfrmDashboard.Visible = true;
                pnlGrid.Controls.Add(objfrmDashboard);
          
                objfrmDashboard.FormBorderStyle = FormBorderStyle.None;
                if (x == false)
                {
                    objfrmDashboard.Width = Screen.PrimaryScreen.WorkingArea.Size.Width - (menuStrip1.Width - 11);
                    x = true;
                }
                else
                {
                    objfrmDashboard.Width = pnlGrid.Width;
                }
                objfrmDashboard.Show();
            }
            else
            {
                Application.OpenForms[objfrmDashboard.Name].Focus();
            }
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenuStore objMenuStore = new frmMenuStore();
            menuClick(RolePermission.A_Store);
            ActiveMenu("storeToolStripMenuItem");
            if (!CheckForm(objMenuStore))
            {
                pnlGrid.Controls.Clear();
                objMenuStore.TopLevel = false;
                objMenuStore.Visible = true;
                pnlGrid.Controls.Add(objMenuStore);
                objMenuStore.FormBorderStyle = FormBorderStyle.None;
                objMenuStore.Width = pnlGrid.Width;
                objMenuStore.Show();
            }
            else
            {
                Application.OpenForms[objMenuStore.Name].Focus();
            }
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenuProduct objMenuProduct = new frmMenuProduct();
            menuClick(RolePermission.A_Product);
            ActiveMenu("productToolStripMenuItem");
            if (!CheckForm(objMenuProduct))
            {
                pnlGrid.Controls.Clear();
                objMenuProduct.TopLevel = false;
                objMenuProduct.Visible = true;
                pnlGrid.Controls.Add(objMenuProduct);
                objMenuProduct.FormBorderStyle = FormBorderStyle.None;
                objMenuProduct.Width = pnlGrid.Width;
                objMenuProduct.Show();
            }
            else
            {
                Application.OpenForms[objMenuProduct.Name].Focus();
            }
        }

        private void procurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenuProcurement objMenuProcurement = new frmMenuProcurement();
            menuClick(RolePermission.A_Procurement);
            ActiveMenu("procurementToolStripMenuItem");
            if (!CheckForm(objMenuProcurement))
            {
                pnlGrid.Controls.Clear();
                objMenuProcurement.TopLevel = false;
                objMenuProcurement.Visible = true;
                pnlGrid.Controls.Add(objMenuProcurement);
                objMenuProcurement.FormBorderStyle = FormBorderStyle.None;
                objMenuProcurement.Width = pnlGrid.Width;
                objMenuProcurement.Show();
            }
            else
            {
                Application.OpenForms[objMenuProcurement.Name].Focus();
            }
        }

        private void taxSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMenuTax objMenuTax = new frmMenuTax();
            menuClick(RolePermission.A_Tax_Setup);
            ActiveMenu("taxSetupToolStripMenuItem");
            if (!CheckForm(objMenuTax))
            {
                pnlGrid.Controls.Clear();
                objMenuTax.TopLevel = false;
                objMenuTax.Visible = true;
                pnlGrid.Controls.Add(objMenuTax);
                objMenuTax.FormBorderStyle = FormBorderStyle.None;
                objMenuTax.Width = pnlGrid.Width;
                objMenuTax.Show();
            }
            else
            {
                Application.OpenForms[objMenuTax.Name].Focus();
            }
        }

        private void roleSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuRole objMenuRole = new MenuRole();
            menuClick(RolePermission.A_Role_Setup);
            ActiveMenu("roleSetupToolStripMenuItem");
            if (!CheckForm(objMenuRole))
            {
                pnlGrid.Controls.Clear();
                objMenuRole.TopLevel = false;
                objMenuRole.Visible = true;
                pnlGrid.Controls.Add(objMenuRole);
                objMenuRole.FormBorderStyle = FormBorderStyle.None;
                objMenuRole.Width = pnlGrid.Width;
                objMenuRole.Show();
            }
            else
            {
                Application.OpenForms[objMenuRole.Name].Focus();
            }
        }

        public void Role()
        {
            storeToolStripMenuItem.Visible = RolePermission.Store;
            productToolStripMenuItem.Visible = RolePermission.Product;
            procurementToolStripMenuItem.Visible = RolePermission.Procurement;
            taxSetupToolStripMenuItem.Visible = RolePermission.Tax_Setup;
            roleSetupToolStripMenuItem.Visible = RolePermission.Role_Setup;
        }

        public void ActiveMenu(string strControlVal)
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                if (strControlVal == item.Name)
                {
                    item.ForeColor = Color.FromArgb(0, 174, 219);
                    item.BackColor = Color.White;
                    item.Font = new Font(roleSetupToolStripMenuItem.Font, FontStyle.Bold);
                }
                else
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = SystemColors.ControlLightLight;
                    item.Font = new Font(dashboardToolStripMenuItem.Font, FontStyle.Regular);
                }
            }
        }

        private void liveCountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MenuLiveCounter objMenuReports = new MenuLiveCounter();
            menuClick(RolePermission.A_Live_Counters);
            ActiveMenu("liveCountersToolStripMenuItem");
            if (!CheckForm(objMenuReports))
            {
                pnlGrid.Controls.Clear();
                objMenuReports.TopLevel = false;
                objMenuReports.Visible = true;
                pnlGrid.Controls.Add(objMenuReports);
                objMenuReports.FormBorderStyle = FormBorderStyle.None;
                objMenuReports.Width = pnlGrid.Width;
                objMenuReports.Show();
            }
            else
            {
                Application.OpenForms[objMenuReports.Name].Focus();
            }

        }
    }
}
