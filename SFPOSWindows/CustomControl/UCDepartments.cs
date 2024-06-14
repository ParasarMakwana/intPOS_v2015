using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SFPOS.Entities.MasterDataClasses;
using SFPOS.BAL.MasterDataServices;

namespace SFPOSWindows.CustomControl
{
    public partial class UCDepartments : UserControl
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;
        public string DeptCode = "";
        ExceptionLogService _ExceptionLogService = new ExceptionLogService();
        ExceptionLogMasterModel objExceptionLogMasterModel = new ExceptionLogMasterModel();
        public UCDepartments()
        {
            InitializeComponent();
        }

        #region KeyBoard(Departments)
        private void btnCigarrette_Click(object sender, EventArgs e)
        {
            DeptCode = "DP120";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnScaleProduce_Click(object sender, EventArgs e)
        {
            DeptCode = "PK1100050";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnScaleSeafood_Click(object sender, EventArgs e)
        {
            DeptCode = "PK1100040";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnScalemeat_Click(object sender, EventArgs e)
        {
            DeptCode = "PK1100030";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnBakery_Click(object sender, EventArgs e)
        {
            DeptCode = "DP30";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnSeaFood_Click(object sender, EventArgs e)
        {
            DeptCode = "DP90";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnFrozenFood_Click(object sender, EventArgs e)
        {
            DeptCode = "DP100";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnLiquor_Click(object sender, EventArgs e)
        {
            DeptCode = "DP180";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnGrocery_Click(object sender, EventArgs e)
        {
            DeptCode = "DP10";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnTaxGrocery_Click(object sender, EventArgs e)
        {
            DeptCode = "DP70";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnShrimp_Click(object sender, EventArgs e)
        {
            DeptCode = "DP170";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnDeli_Click(object sender, EventArgs e)
        {
            DeptCode = "DP160";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnBear_Click(object sender, EventArgs e)
        {
            DeptCode = "DP60";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnProduce_Click(object sender, EventArgs e)
        {
            DeptCode = "DP40";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnMeat_Click(object sender, EventArgs e)
        {
            DeptCode = "DP50";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnSoda_Click(object sender, EventArgs e)
        {
            DeptCode = "DP110";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }

        private void btnRice_Click(object sender, EventArgs e)
        {
            DeptCode = "DP80";
            this.Hide();
            OnMyEvent(this, new EventArgs());
        }
        #endregion
    }
}
