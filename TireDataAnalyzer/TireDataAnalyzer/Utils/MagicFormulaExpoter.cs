using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using TTCDataUtils;
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
            static public bool Export(string filename, ExportMode mode, TireMagicFormula mf)
            {
                if(mode == ExportMode.excel)
                {
                    try
                    {
                        return ExportExcel(filename, mf);
                    }
                    catch(Exception e)
                    {
                        Log.Output(e.Message);
                        return false;
                    }
                }
                return false;
            }

            static bool ExportExcel(string filename, TireMagicFormula mf)
            {


                //エクセルファイルのオープン
                var path = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                var workBook = new XLWorkbook(path + "\\MagicFormulaBase.xlsx");

                //1シート目の選択
                var sheet = workBook.Worksheet(1);
                for (int i = 2; i <= 7; i++)
                {
                    sheet.Cell(i, 2).Value = mf.FX.NormalizeOffsetParam[i - 2];
                    sheet.Cell(i, 3).Value = mf.FX.NormalizeScaleParam[i - 2];
                }

                var list = new List<ApproximatingCurve>(10);
                list.Add(mf.FX);
                list.Add(mf.FY);
                list.Add(mf.CFX);
                list.Add(mf.CFY);
                list.Add(mf.MZ.PT);
                list.Add(mf.MZ.CMZM);
                list.Add(mf.MZ.MZR);

                int[] row = { 9, 12, 15, 18, 21, 24, 27 };

                for (int i = 0; i < list.Count; i++)
                {
                    for(int j = 0; j< list[i].Parameters.Count; ++j)
                    {
                        sheet.Cell(row[i], 2 + j).Value = "a" + j.ToString();
                        sheet.Cell(row[i]+1, 2 + j).Value = list[i].Parameters[j];
                    }
                    
                }
                workBook.SaveAs(filename);
                return true;
            }
        }
    }

    
}
