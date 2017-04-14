using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer.UserControls.PropertyPage
{
    public partial class TireMagicFormulaParameterProperty : PropertyPage
    {
        public TireMagicFormulaParameterProperty(ProjectTree.Node_MagicFormula impl)
            :base(impl)
        {
            InitializeComponent();
            Impl = impl;
            Impl.OnRename += OnRename;
            
        }

        void OnRename(string name)
        {
            SetTabText(name);
        }

        ProjectTree.Node_MagicFormula Impl;

        private void TireMagicFormulaParameterProperty_Load(object sender, EventArgs e)
        {
            OnRename(Impl.Name);
            InitializeGraph();
            //NumPoint.SelectedIndex = 4;
            for (int i = 0; i< Impl.MFFD.MagicFormula.FX.Parameters.Count; ++i)
            {
                dataGridView0.Rows.Add();
                dataGridView0.Rows[i].Cells[0].Value = Impl.MFFD.MagicFormula.FX.FittingParameters[i];
                dataGridView0.Rows[i].Cells[1].Value = "a" + i.ToString();
                dataGridView0.Rows[i].Cells[2].Value = Impl.MFFD.MagicFormula.FX.Parameters[i];
            }
        }

        private void InitializeGraph()
        {
            /*
            CorneringDataViewer.SetAxis(TireDataColumn.SA, TireDataColumn.FY);
            DriveBrakeDataViewer.SetAxis(TireDataColumn.SL, TireDataColumn.FX);
            TransientDataViewer.SetAxis(TireDataColumn.ET, TireDataColumn.SA);

            string[] legends = { "追加済みデータ", "新規データ" };
            foreach (Table table in Enum.GetValues(typeof(Table)))
            {
                if (table != Table.None && table != Table.StaticTable)
                {
                    GetViewer(table).SetColor(Color.ForestGreen, legends[0]);
                    GetViewer(table).SetChartType(SeriesChartType.FastPoint, legends[0]);
                    GetViewer(table).SetColor(Color.Red, legends[1]);
                    GetViewer(table).SetChartType(SeriesChartType.FastPoint, legends[1]);
                }

            }
            */
        }

        private void dataGridView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            DataGridView dgv = ((DataGridView)(sender));
            int x = dgv.CurrentCellAddress.X;
            int y = dgv.CurrentCellAddress.Y;
            if (x != 2) return;
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dgv[x, y].Value = "";
            }
            else if ( (e.Modifiers & Keys.Control) == Keys.Control && e.KeyCode == Keys.V)
            {
                string clipText = Clipboard.GetText();
                clipText = clipText.Replace("\r\n", "\n");
                clipText = clipText.Replace("\r", "\n");
                string[] lines = clipText.Split('\n');
                int r;
                bool nflag = true;
                for (r = 0; r <= (lines.GetLength(0) - 1); r++)
                {
                    if (r >= (lines.GetLength(0) - 1) && ("".Equals(lines[r]) && (nflag == false)))
                    {
                        break;
                    }

                    if (("".Equals(lines[r]) == false))
                    {
                        nflag = false;
                    }

                    string[] vals = lines[r].Split('\t');
                    int c = 0;
                    int c2 = 0;
                    for (c = 0; c <= (vals.GetLength(0) - 1); c++)
                    {
                        if (!(x + c2 >= 0
                            && x  + c2 < dgv.ColumnCount
                            && y + r >= 0
                            && y + r < dgv.RowCount
                            ))
                        {
                            continue;
                        }
                        if ((dgv[(x + c2), (y + r)].Visible == false))
                        {
                            c = (c - 1);
                            continue;
                        }
                        
                        if( y + r == dgv.RowCount - 1 && dgv.AllowUserToAddRows == true )
                        {
                            dgv.RowCount = dgv.RowCount + 1;
                        }
                        dgv[x + c2, y + r].Value = vals[c];
                        c2 = (c2 + 1);
                    }

                }

            }

        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            if (e.ColumnIndex == 2 && !StaticFunctions.IsNumeric(e.FormattedValue.ToString()))
            {
                //行にエラーテキストを設定
                dgv.Rows[e.RowIndex].ErrorText = "実数を入力してください";
                //入力した値をキャンセルして元に戻すには、次のようにする
                dgv.CancelEdit();
                //キャンセルする
                e.Cancel = true;
                return;
            }
        }

        private void dataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        { 
            DataGridView dgv = (DataGridView)sender;
            //エラーテキストを消す
            dgv.Rows[e.RowIndex].ErrorText = null;

            for(int i=0; i<Impl.MFFD.MagicFormula.FX.Parameters.Count; ++i)
            {
                Impl.MFFD.MagicFormula.FX.FittingParameters[i] = bool.Parse(dgv.Rows[i].Cells[0].Value.ToString());
                Impl.MFFD.MagicFormula.FX.Parameters[i] = double.Parse(dgv.Rows[i].Cells[2].Value.ToString());
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (e.SplitX > DGVWidth(dataGridView0))
            {
                var sp = sender as SplitContainer;
                sp.SplitterDistance = DGVWidth(dataGridView0);
            }
        }

        private int DGVWidth(DataGridView dgv)
        {
            int w = dgv.RowHeadersWidth;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                w += column.Width;
            }
            return w;
        }
    }
}
