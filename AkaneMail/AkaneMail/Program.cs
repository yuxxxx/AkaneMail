using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace AkaneMail
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            var test1 = "test1 test2 test3".Split(' ');
            var test2 = "test4 test5 test6".Split(' ');
            var result = from t in test1
                         join s in test2 on t equals s
                         select t;
            var u = result.Count();
        }
    }
}