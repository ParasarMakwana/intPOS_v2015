using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intPOSSetup
{
    public partial class SelectComponent : Form
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public Boolean InsFrontend = true;
        public Boolean InsBackend = true;
        public Boolean InsLabelApp = true;

        public Boolean IsPrev = false;

        public SelectComponent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chkBackend.Checked == false && chkFrontend.Checked == false)
            {
                MessageBox.Show("Please select atleast one component Frontend or Backend !", "Please select atleast one component");
            }
            else
            {
                if (chkFrontend.Checked == false)
                    InsFrontend = false;

                if (chkBackend.Checked == false)
                    InsBackend = false;

                if (chkLabelApp.Checked == false)
                    InsLabelApp = false;

                IsPrev = false;

                this.Hide();
                this.Close();
                OnMyEvent(this, new EventArgs());
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            IsPrev = true;

            this.Hide();
            this.Close();
            OnMyEvent(this, new EventArgs());
        }
    }
}
