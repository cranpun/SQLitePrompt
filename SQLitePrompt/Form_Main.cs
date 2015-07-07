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

        private string _lastSelect;

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

        private void dropAction(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            SQLiteUtils.path = paths[0];
            SQLiteUtils.pass = null;
            object[,] ret;
            try
            {
                ret = SQLiteUtils.select(SQLiteUtils.CMD_ALLTABLES);
                this.textBox_log.Text = string.Join(",", this.makeTablesStr(ret));
            }
            catch (Exception)
            {
                try
                {
                    Form_inputText form = new Form_inputText();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SQLiteUtils.pass = form.textBox_input.Text;
                        ret = SQLiteUtils.select(SQLiteUtils.CMD_ALLTABLES);
                        this.textBox_log.Text = string.Join(",", this.makeTablesStr(ret));
                    }
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(string.Format("Error : {0}", ex2.Message));
                }
            }
        }

        private string[] makeTablesStr(object[,] data)
        {
            int cnt = data.GetUpperBound(0);
            string[] ret = new string[cnt];
            for (int i = 0; i < cnt; i++)
            {
                ret[i] = data[i, 0].ToString();
            }
            return ret;
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

        private async void button_exec_Click(object sender, EventArgs e)
        {
            try
            {
                string q = this.textBox_prompt.Text.TrimStart(new char[] { ' ', '　', '\n', '\r' });
                if (q.Substring(0, 6).ToLower() == "select")
                {
                    this._lastSelect = q;
                }
                else
                {
                    await SQLiteUtils.queryAsync(q);
                }
                await SQLiteUtils.fillAsync(this._lastSelect, this._table, this.dataGridView_prompt);
            }
            catch (Exception ex)
            {
                this.textBox_log.Text = "ERR:" + ex.Message;
            }
        }

        private void setPasswordPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SQLiteUtils.path != null)
            {
                Form_inputText form = new Form_inputText();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SQLiteUtils.setPassword(form.textBox_input.Text);
                    SQLiteUtils.pass = form.textBox_input.Text;
                    MessageBox.Show("changed password");
                }
                else
                {
                    MessageBox.Show("no opened file.");
                }

            }
        }

    }
}
