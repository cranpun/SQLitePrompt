using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLitePrompt
{
    class MyUtils
    {
        public static bool textBoxSelectAll(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) {
                TextBox textBox = (TextBox)sender;
                textBox.SelectAll();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
