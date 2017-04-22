using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //FormクラスのdefaultIconフィールドを変更する
            typeof(Form).GetField("defaultIcon",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Static).SetValue(
                    null, Properties.Resources.Icon);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
