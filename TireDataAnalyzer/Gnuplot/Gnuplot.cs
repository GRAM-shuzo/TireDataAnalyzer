using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GnuplotLib
{
    using XY = Tuple<double, double>;
    using MaxMin = Tuple<double, double>;
    using XYZ = Tuple<double, double, double>;

    public class Gnuplot
    {
        #region enum
        /// <summary>
        /// Key position of graph
        /// </summary>
        public enum Position
        {
            left,
            right,
            top,
            bottom,
            outside,
            below,
            unset,
            off
        }
        public enum PlotMode
        {
            Plot,
            Replot
        }
        #endregion

        #region Internal Constant
        private static string gnuplotExeFilePath = "gnuplot";
        private System.Diagnostics.Process GnuConsole
        {
            get;
            set;
        }
        private bool Started = false;
        #endregion

        #region Property
        public bool SetXaxisLogScale = false;
        public bool SetYaxisLogScale = false;
        public string GraphTitle = "";
        public string XaxisLabel = "";
        public string YaxisLabel = "";
        public string ZaxisLabel = "";
        public double LineWidth = 1.0;
        public Tuple<double, double> xrange = null;
        public Tuple<double, double> yrange = null;
        public Tuple<double, double> zrange = null;
        public Position KeyPosition = Position.unset;
        #endregion

        #region Public Method
        public Gnuplot(
            bool redirectStandardInput = true,
            bool createNoWindow = true,
            bool useShellExecute = false
            )
        {
            GnuConsole = new System.Diagnostics.Process();
            GnuConsole.StartInfo.Arguments = "-p";
            GnuConsole.StartInfo.FileName = gnuplotExeFilePath;
            GnuConsole.StartInfo.CreateNoWindow = createNoWindow;
            GnuConsole.StartInfo.UseShellExecute = useShellExecute;
            GnuConsole.StartInfo.RedirectStandardInput = redirectStandardInput;
            try
            {
                GnuConsole.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error:Gnuplotが見つかりません");
                throw e;
            }
        }

        

        public void PlotXY(
            string title,
            List<XY> list,
            string style,
            bool yaxis2 = false,
            string color = ""
        )
        {
            string command = "replot";
            if (!Started)
            {
                Start();
                command = "plot";
            }
            WritePreCommandsForGnuConsole();
            string name = Math.Abs(title.GetHashCode()).ToString();
            name = ReplaceNumber(name);

            GnuConsole.StandardInput.WriteLine("$" + name + " << EOD");
            foreach (var data in list)
            {
                GnuConsole.StandardInput.WriteLine("{0}\t{1}", data.Item1, data.Item2);
            }
            GnuConsole.StandardInput.WriteLine("EOD");

            command += " $" + name + " with " + style + " title" + " \"" + title + "\"";
            if (color != "") command += " lc rgb \"" + color + "\"";
            command += string.Format(" linewidth {0}",LineWidth);
            if (yaxis2) command += " axis x1y2";
            GnuConsole.StandardInput.WriteLine(command);
        }

        public void PlotXYZ(
            string title,
            List<XYZ> list,
            string style,
            string color = ""
        )
        {
            string command = "replot";
            if (!Started)
            {
                Start();
                command = "plot";
            }
            WritePreCommandsForGnuConsole();
            string name = Math.Abs(title.GetHashCode()).ToString();
            name = ReplaceNumber(name);

            GnuConsole.StandardInput.WriteLine("$" + name + " << EOD");
            foreach (var data in list)
            {
                GnuConsole.StandardInput.WriteLine("{0}\t{1}\t{2}", data.Item1, data.Item2, data.Item3);
            }
            GnuConsole.StandardInput.WriteLine("EOD");

            command += " $" + name + " with " + style + " title" + " \"" + title + "\"";
            if (color != "") command += " lc rgb \"" + color + "\"";
            GnuConsole.StandardInput.WriteLine(command);
        }

        public void PlotXYwithErrors(
            string title,
            List<XY> list,
            List<MaxMin> errorList,
            string color = ""
        )
        {
            string command = "replot";
            if (!Started)
            {
                Start();
                command = "plot";
            }
            WritePreCommandsForGnuConsole();
            string name = Math.Abs(title.GetHashCode()).ToString();
            name = ReplaceNumber(name);

            GnuConsole.StandardInput.WriteLine("$" + name + " << EOD");
            for(int i = 0; i<list.Count; ++i)
            {
                var data = list[i];
                var error = errorList[i];
                GnuConsole.StandardInput.WriteLine("{0}\t{1}\t{2}\t{3}", data.Item1, data.Item2, error.Item2, error.Item1);
            }
            GnuConsole.StandardInput.WriteLine("EOD");

            command += " $" + name +  " using 1:2:3:4 with yerrorbars " + "title" + " \"" + title + "\"";
            if (color != "") command += " lc rgb \"" + color + "\"";
            GnuConsole.StandardInput.WriteLine(command);
        }
        public void WriteCommand(string command)
        {
            GnuConsole.StandardInput.WriteLine(command);
        }


        #endregion

        #region Private Method
        private void Start()
        {
                Started = true;
        }


        private string ReplaceNumber(string s)
        {
            s = s.Replace('0', 'h');
            s = s.Replace('1', 'i');
            s = s.Replace('2', 'j');
            s = s.Replace('3', 'k');
            s = s.Replace('4', 'l');
            s = s.Replace('5', 'm');
            s = s.Replace('6', 'n');
            s = s.Replace('7', 'o');
            s = s.Replace('8', 'p');
            s = s.Replace('9', 'q');
            return s;
        }

        private void WritePreCommandsForGnuConsole()
        {

            if (SetXaxisLogScale)
            {
                GnuConsole.StandardInput.WriteLine("set logscale x");

            }

            if (SetYaxisLogScale)
                GnuConsole.StandardInput.WriteLine("set logscale y");

            if (!XaxisLabel.Equals(""))
                GnuConsole.StandardInput.WriteLine(@"set xlabel '{0}'", XaxisLabel);
            if (!YaxisLabel.Equals(""))
                GnuConsole.StandardInput.WriteLine(@"set ylabel '{0}'", YaxisLabel);
            if (!ZaxisLabel.Equals(""))
                GnuConsole.StandardInput.WriteLine(@"set zlabel '{0}'", ZaxisLabel);
            if (!GraphTitle.Equals(""))
                GnuConsole.StandardInput.WriteLine(@"set title '{0}'", GraphTitle);

            if (!KeyPosition.Equals(Position.unset))
                GnuConsole.StandardInput.WriteLine("set key {0}", KeyPosition.ToString());

            if (xrange != null)
            {
                GnuConsole.StandardInput.WriteLine("set xrange [{0}:{1}]", xrange.Item1, xrange.Item2);
            }

            if (yrange != null)
            {
                GnuConsole.StandardInput.WriteLine("set yrange [{0}:{1}]", yrange.Item1, yrange.Item2);
            }
        }
        
        #endregion
    }

}