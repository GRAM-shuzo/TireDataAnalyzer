using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireDataAnalyzer
{
    namespace Export
    {
        public enum ExportMode
        {
            excel,
            tsv,
            ini
        }


        public static class MagicFormulaExpoter
        {
            static public bool Export(string filename, ExportMode mode)
            {
                return false;
            }
        }
    }

    
}
