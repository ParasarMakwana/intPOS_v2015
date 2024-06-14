using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFPOS.Common
{
    public class Common
    {
        public string validation(TextBox txtName)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Focus();
                //ep.SetError(txtStoreName, "Store name should not be left blank!");
            }
            return "";
        }
    }
}
