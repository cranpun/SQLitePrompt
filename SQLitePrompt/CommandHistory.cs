using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLitePrompt
{
    class CommandHistory
    {
        List<string> _history;
        int _idx;
        private static CommandHistory _ins = null;

        public static CommandHistory ins
        {
            get
            {
                if (CommandHistory._ins == null)
                {
                    CommandHistory._ins = new CommandHistory();
                }
                return CommandHistory._ins;
            }
        }

        private CommandHistory()
        {
            this._history = new List<string>();
            this._idx = -1;
        }

        public string back()
        {
            // 0なら一番古いところまでいってしまったのでストップ
            if (this._idx > 0)
            {
                this._idx--;
            }

            return this._history[this._idx];
        }

        public string forward()
        {
            if (this._idx < this._history.Count - 1)
            {
                // 添字ベースなので-1で判断。
                this._idx++;
            }
            return this._history[this._idx];
        }

        public void push(string com)
        {
            this._history.Add(com);
            // 添字ベースなので-1
            this._idx = this._history.Count - 1;
        }
    }
}
