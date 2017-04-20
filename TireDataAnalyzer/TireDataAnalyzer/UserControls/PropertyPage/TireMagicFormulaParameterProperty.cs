using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TireDataAnalyzer.TexEquation;
using TTCDataUtils;
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

            

            DGVList.Add(dataGridView0);
            MTDVList.Add(multiTireDataViewer0);
            MFTEList.Add(magicFormula_TexEquation0);
            DGVList.Add(dataGridView1);
            MTDVList.Add(multiTireDataViewer1);
            MFTEList.Add(magicFormula_TexEquation1);
            DGVList.Add(dataGridView2);
            MTDVList.Add(multiTireDataViewer2);
            MFTEList.Add(magicFormula_TexEquation2);
            DGVList.Add(dataGridView3);
            MTDVList.Add(multiTireDataViewer3);
            MFTEList.Add(magicFormula_TexEquation3);
            foreach (var mtdv in MTDVList)
            {
                string id = "000_" + Impl.ID.ToString();
                mtdv.ResetScreen(MultiTireDataViewer.EnumScreenCount.One);
                mtdv.SetAxis(TireDataViewer.EnumAxis.MagicFormula);
                mtdv.SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, id);
                mtdv.SetLineWidth(5, id);
                mtdv.SetMagicFormula(Impl.MFFD.MagicFormula, null, id);
                mtdv.LegendTextOverride(id, Impl.Name);

            }
            multiTireDataViewer0.SetAxis(MagicFormulaInputVariables.SR, MagicFormulaOutputVariables.FX);
            multiTireDataViewer1.SetAxis(MagicFormulaInputVariables.SA, MagicFormulaOutputVariables.FY);
            multiTireDataViewer2.SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            multiTireDataViewer3.SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            foreach (var mtdv in MTDVList)
            {
                
                TireDataViewerProperty p = new TireDataViewerProperty(mtdv);
                p.ReplotAll();
            }
            foreach (var mfte in MFTEList)
            {
                MainWindow.Instance.ResizeEnd += mfte.Control_Resize;
                mfte.Control_Resize(mfte,new EventArgs());
            }
            magicFormula_TexEquation0.Type = MagicFormula_TexEquation.MagicFormulaType.FX;
            magicFormula_TexEquation1.Type = MagicFormula_TexEquation.MagicFormulaType.FY;
            magicFormula_TexEquation2.Type = MagicFormula_TexEquation.MagicFormulaType.CFX;
            magicFormula_TexEquation3.Type = MagicFormula_TexEquation.MagicFormulaType.CFY;
            foreach (var dgv in DGVList)
            {
                dgv.KeyDown += dataGridView_KeyDown;
                dgv.CellEnter += dataGridView_CellEnter;
                dgv.CellValidating += dataGridView_CellValidating;
            }

            MFList.Add(Impl.MFFD.MagicFormula.FX);
            MFList.Add(Impl.MFFD.MagicFormula.FY);
            MFList.Add(Impl.MFFD.MagicFormula.CFX);
            MFList.Add(Impl.MFFD.MagicFormula.CFY);

            splitContainer1.SplitterMoved += splitContainer_SplitterMoved;
            splitContainer3.SplitterMoved += splitContainer_SplitterMoved;
            splitContainer5.SplitterMoved += splitContainer_SplitterMoved;
            splitContainer7.SplitterMoved += splitContainer_SplitterMoved;
        }

        List<DataGridView> DGVList = new List<DataGridView>();
        List<MultiTireDataViewer> MTDVList = new List<MultiTireDataViewer>();
        List<MagicFormula_TexEquation> MFTEList = new List<MagicFormula_TexEquation>();
        List<ApproximatingCurve> MFList = new List<ApproximatingCurve>();

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
            for(int k = 0; k<MFList.Count; ++k)
            {
                for (int i = 0; i < MFList[k].Parameters.Count; ++i)
                {
                    DGVList[k].Rows.Add();
                    DGVList[k].Rows[i].Cells[0].Value = MFList[k].FittingParameters[i];
                    DGVList[k].Rows[i].Cells[1].Value = "a" + i.ToString();
                    DGVList[k].Rows[i].Cells[2].Value = MFList[k].Parameters[i];
                }
            }
            
        }

        public override bool OnRemove()
        {
            foreach (var mfte in MFTEList)
            {
                MainWindow.Instance.ResizeEnd -= mfte.Control_Resize;
            }
            //変更されている場合
            if (State == PropertyPageState.Changed)
            {
                DialogResult result = MessageBox.Show("内容の変更を反映しますか？",
                        "確認",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    OnApplyClick();
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
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
            int n = DGVList.IndexOf(dgv);
            for(int i=0; i< MFList[n].Parameters.Count; ++i)
            {
                MFList[n].FittingParameters[i] = bool.Parse(dgv.Rows[i].Cells[0].Value.ToString());
                MFList[n].Parameters[i] = double.Parse(dgv.Rows[i].Cells[2].Value.ToString());
            }
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            var dgv = (sender as SplitContainer).Panel1.Controls[0] as DataGridView;

            if (e.SplitX > DGVWidth(dgv))
            {
                var sp = sender as SplitContainer;
                sp.SplitterDistance = DGVWidth(dgv);
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

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int i = DGVList.IndexOf(dgv);
            MFTEList[i].Highlight(e.RowIndex);
            
        }
    }
}
