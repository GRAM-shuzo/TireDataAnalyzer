using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TireDataAnalyzer
{
    public static class Log
    {
        public delegate void OutputHandler(string s);
        public static OutputHandler OnOutput { get; set; }
        public static StreamWriter logWriter =
              new StreamWriter(@"Log.txt", false, Encoding.GetEncoding("Shift_JIS"));

        static Log()
        {
            OnOutput += new OutputHandler(StackLog);
            OnOutput += new OutputHandler(WriteToLogFile);
            OnOutput += new OutputHandler(VSOutput);
        }

        public static void Output(string s)
        {
            OnOutput(s);
        }

        private static List<string> log_ = new List<string>();
        private static void StackLog(string s)
        {
            log_.Add(s);
        }

        public static void WriteToLogFile(string s)
        {     
            logWriter.WriteLine(s);
            logWriter.Flush();
        }

        public static void VSOutput(string s)
        {
            System.Diagnostics.Trace.WriteLine(s);
        }
    }
}
