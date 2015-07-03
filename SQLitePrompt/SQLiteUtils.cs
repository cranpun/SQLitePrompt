using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLitePrompt
{
    class SQLiteUtils
    {
        static private SQLiteConnection CON;
        static private SQLiteCommand CMD;
        static private long _insert_id;

        static public string path = null;
        static public string pass = null;

        static public readonly string CMD_ALLTABLES = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";

        static public int query(string query)
        {
#if DEBUG
            System.Diagnostics.Debug.Write("======== query ==========" + System.Environment.NewLine);
            System.Diagnostics.Debug.Write(query + System.Environment.NewLine);
            System.Diagnostics.Debug.Write("=========================" + System.Environment.NewLine);
#endif
            SQLiteUtils.open();
            SQLiteUtils.CMD.CommandText = query;
            int ret = SQLiteUtils.CMD.ExecuteNonQuery();
            SQLiteUtils._insert_id = SQLiteUtils.CON.LastInsertRowId;
            SQLiteUtils.close();
            return ret;
        }

        static public long INSERT_ID
        {
            get
            {
                return SQLiteUtils._insert_id;
            }
        }

        static public void fill(string query, DataTable table)
        {
            try
            {
                SQLiteUtils.open();
#if DEBUG
                System.Diagnostics.Debug.Write("======== fill ===========" + System.Environment.NewLine);
                System.Diagnostics.Debug.Write(query + System.Environment.NewLine);
                System.Diagnostics.Debug.Write("=========================" + System.Environment.NewLine);
#endif
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, SQLiteUtils.CON);
                table.Columns.Clear();
                table.Rows.Clear();
                table.Clear();
                adapter.Fill(table);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                SQLiteUtils.close();
            }

        }

        /// <summary>
        /// 見つからなければnull
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        static public object[,] select(string query)
        {
#if DEBUG
            System.Diagnostics.Debug.Write("======== select =========" + System.Environment.NewLine);
            System.Diagnostics.Debug.Write(query + System.Environment.NewLine);
            System.Diagnostics.Debug.Write("=========================" + System.Environment.NewLine);
#endif
            SQLiteUtils.open();
            SQLiteUtils.CMD.CommandText = query;
            SQLiteDataReader rows = SQLiteUtils.CMD.ExecuteReader();
            int cntc = rows.FieldCount;

            if (rows.HasRows == false)
            {
                // 見つからなかったのでnull
                return null;
            }

            List<object[]> tmp = new List<object[]>();
            while (rows.Read())
            {
                object[] rowval = new object[cntc];

                int rank = rowval.Length;
                for (int c = 0; c < cntc; c++)
                {
                    rowval[c] = rows.GetValue(c);
                }
                tmp.Add(rowval);
            }

            // ジャグ配列を多次元配列に変換
            int cntr = tmp.Count;
            object[,] ret = new object[cntr, cntc];
            for (int r = 0; r < cntr; r++)
            {
                for (int c = 0; c < cntc; c++)
                {
                    ret[r, c] = tmp[r][c];
                }
            }
            SQLiteUtils.close();

            return ret;
        }

        static public async Task<object[,]> selectAsync(string q)
        {
            Cursor.Current = Cursors.WaitCursor;
            Func<object[,]> query = () =>
            {
                return SQLiteUtils.select(q);
            };
            object[,] ret = await Task.Run(query).ConfigureAwait(false);
            Cursor.Current = Cursors.Default;
            return ret;
        }

        #region delegate 
        delegate void fillDelegate(string q, DataTable table);
        internal static void fillInternal(string q, DataTable table)
        {
            SQLiteUtils.fill(q, table);
        }
        #endregion
        static public async Task<object> fillAsync(string q, DataTable table, DataGridView view)
        {
            Cursor.Current = Cursors.WaitCursor;
            table.Clear();
            Func<object> query = () =>
            {
                view.Invoke(new fillDelegate(SQLiteUtils.fillInternal), q, table);
                return null;
            };
            await Task.Run(query);
            Cursor.Current = Cursors.Default;
            return null;
        }

        static public async Task<object> queryAsync(string q)
        {
            Cursor.Current = Cursors.WaitCursor;
            Func<object> query = () =>
            {
                SQLiteUtils.query(q);
                return null;
            };
            await Task.Run(query).ConfigureAwait(false);
            Cursor.Current = Cursors.Default;
            return null;
        }

        /// <summary>
        /// 見つからなければnull
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="clm"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        static public object[] selectId(string tab, string clm, string id)
        {
            string q = "SELECT *  FROM " + tab;
            q += " WHERE " + clm + "=" + id;
#if DEBUG
            System.Diagnostics.Debug.Write("======== selectId =======" + System.Environment.NewLine);
            System.Diagnostics.Debug.Write(q + System.Environment.NewLine);
            System.Diagnostics.Debug.Write("=========================" + System.Environment.NewLine);
#endif
            object[,] rows = SQLiteUtils.select(q);
            if (rows != null)
            {
                int cnt = rows.GetLength(1);
                object[] ret = new object[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    ret[i] = rows[0, i];
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        static public string getValRC(object[,] data, int row, string[] columns, string column)
        {
            return data[row, Array.IndexOf(columns, column)].ToString();
        }

        static public string getValC(object[] data, string[] columns, string column)
        {
            return data[Array.IndexOf(columns, column)].ToString();
        }

        static public object[,] makeArrayFromDataTable(DataTable table)
        {
            int cntr = table.Rows.Count;
            int cntc = table.Columns.Count;
            object[,] ret = new object[cntr, cntc];

            for (int r = 0; r < cntr; r++)
            {
                for (int c = 0; c < cntc; c++)
                {
                    ret[r, c] = table.Rows[r][c];
                }
            }

            return ret;
        }

        /// <summary>
        /// 1行インサート
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="row"></param>
        /// <param name="esc"></param>
        /// <returns></returns>
        static public long insert(string tab, Dictionary<string, string> row, Boolean esc)
        {
            List<string> vals = new List<string>();
            List<string> cols = new List<string>();

            foreach (KeyValuePair<string, string> r in row)
            {
                cols.Add(r.Key);
                vals.Add((esc ? "'" + r.Value + "'" : r.Value));
            }
            string ret = "INSERT INTO " + tab;
            ret += " (" + string.Join(",", cols.ToArray()) + " ) ";
            ret += " VALUES ( " + string.Join(",", vals.ToArray()) + " ) ";
            ret += ";";

            SQLiteUtils.query(ret);

            return SQLiteUtils.INSERT_ID;
        }

        /// <summary>
        /// whereの条件は呼び出し元で作成makeWhere等を使うと楽。
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="row"></param>
        /// <param name="where"></param>
        /// <param name="esc"></param>
        static public void update(string tab, Dictionary<string, string> row, string where, Boolean esc)
        {
            String ret = "UPDATE " + tab + " SET ";
            List<string> sets = new List<string>();
            foreach (KeyValuePair<string, string> r in row)
            {
                sets.Add(r.Key + "=" + (esc ? "'" + r.Value + "'" : r.Value));
            }
            ret += string.Join(",", sets.ToArray());
            ret += " " + where;
            SQLiteUtils.query(ret);
        }

        static public int execTransaction(List<string> sql)
        {
            SQLiteUtils.open();

            SQLiteUtils.CMD.Transaction = SQLiteUtils.CON.BeginTransaction();
            int ret = 0;
            foreach (string q in sql)
            {
                try
                {
                    SQLiteUtils.CMD.CommandText = q;
                    ret += SQLiteUtils.CMD.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show(q);
                }
            }

            SQLiteUtils.CMD.Transaction.Commit();

            SQLiteUtils.close();
            return ret;
        }


        /// <summary>
        /// 見つからなければnull
        /// </summary>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        static public string getByFunc(string table, string col, string func, string cond)
        {
            string q = "SELECT " + func + "(" + col + ") FROM " + table + " " + cond;
            object[,] ret = SQLiteUtils.select(q);
            if (ret == null)
            {
                return null;
            }
            else
            {
                return ret[0, 0].ToString();
            }
        }


        #region makeWhere
        static public string makeLike(string connOp, string col, string val)
        {
            return (val == "") ? " " : connOp + " " + col + " LIKE '%" + val + "%'";
        }

        static public string makeCond(string connOp, string col, string op, string val)
        {
            return (val == "") ? " " : connOp + " " + col + " " + op + " '" + val + "'";
        }
        #endregion

        static private void open()
        {
            #region usepass
            //string password = "Y72KDTRS";
            //SQLiteUtils.CON = new SQLiteConnection("Data Source=" + SQLiteUtils.DBPATH + SQLiteUtils.DBFILE + "; password=" + password);
            // // これはいらない。接続文字列に入ってる SQLiteUtils.CON.SetPassword(password);
            #endregion
            try
            {
                if (SQLiteUtils.pass == null)
                {
                    SQLiteUtils.CON = new SQLiteConnection("Data Source=" + path);
                }
                else
                {
                    SQLiteUtils.CON = new SQLiteConnection(string.Format("Data Source={0}; password={1}", SQLiteUtils.path, SQLiteUtils.pass));
                }
                SQLiteUtils.CON.Open();
                //SQLiteUtils.CON.ChangePassword(password);
                SQLiteUtils.CMD = SQLiteUtils.CON.CreateCommand();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        static private void close()
        {
            SQLiteUtils.CON.Close();
            SQLiteUtils.CON = null;
            SQLiteUtils.CMD = null;
        }
    }
}
