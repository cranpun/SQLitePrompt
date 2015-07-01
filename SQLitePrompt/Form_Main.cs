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
    public partial class Form_Main : Form
    {
        private DataTable _table;

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            this._table = new DataTable();
            this.dataGridView_prompt.DataSource = this._table;
        }

        private void Form_Main_DragDrop(object sender, DragEventArgs e)
        {
            this.dropAction(sender, e);
        }

        private void dropAction(object sender, DragEventArgs e) {
            string[] fs = e.Data.GetFormats();
            this.textBox_log.Text = string.Join(",", fs);
        }

        private void Form_Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

    }
}
