using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLitePrompt
{
    public partial class Form_inputText : Form
    {
        public Form_inputText()
        {
            InitializeComponent();
        }

        private void button_action_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            MyUtils.textBoxSelectAll(sender, e);
        }
    }
}
