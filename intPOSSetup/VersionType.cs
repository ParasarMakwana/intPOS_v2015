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
    public partial class VersionType : Form
    {
        public delegate void onMyEventHandler(object sender, EventArgs e);
        public event onMyEventHandler OnMyEvent;

        public int VersionNo = 0;

        public Boolean IsPrev = false;

        public VersionType()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(rdbFull.Checked==true)
            {
                VersionNo = 1;
            }
            IsPrev = false;
            this.Hide();
            this.Close();
            OnMyEvent(this, new EventArgs());
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
