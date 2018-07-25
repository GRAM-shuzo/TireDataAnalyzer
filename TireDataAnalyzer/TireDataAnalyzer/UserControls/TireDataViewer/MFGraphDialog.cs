using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TTCDataUtils;
using System.Windows.Forms.DataVisualization.Charting;

namespace TireDataAnalyzer.UserControls
{
    public partial class MFGraphDialog : Form
    {
        public MFGraphDialog()
        {

            InitializeComponent();
        }
        SeriesEditor Editor;
        List<ProjectTree.Node_TireDataSet> DataList;
        bool init = true;
        public MFGraphDialog(MultiTireDataViewer viewer, string ID)
        {
            Viewer = viewer;
            InitializeComponent();
            Editor = new SeriesEditor(Viewer, ID);
            Editor.Location = new Point(12, 38);
            this.Controls.Add(Editor);

            var args = Viewer.GetArguments(ID)[0];

            SATB.Text = args.SA.ToString();
            SRTB.Text = args.SR.ToString();
            FZTB.Text = args.FZ.ToString();
            IATB.Text = args.IA.ToString();
            PTB.Text = args.P.ToString();
            TTB.Text = args.T.ToString();
            foreach (Table t in Enum.GetValues(typeof(Table)))
            {
                if (t == Table.None) continue;
                if (t == Table.StaticTable) continue;
                TableCB.Items.Add(t);
            }
            TableCB.SelectedItem = Table.CorneringTable;
            InitSourceList();
            init = false;
        }
        MultiTireDataViewer Viewer;
        bool hasError = false;
        int SeriesData = 0;
        int SeriesSelected = 1;

        private void InitSourceList()
        {
            SourceCB.Items.Clear();
            DataList = ProjectManager.ProjectNode.GetTireDataSet();
            SourceCB.Items.Add("Parent");
            foreach (var data in DataList)
            {
                SourceCB.Items.Add(data);
            }
            SourceCB.SelectedIndex = 0;
        }

        private void IsReal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);

            string s = tb.Text;
            if (!(StaticFunctions.IsNumeric(s) || s == ""))
            {
                this.EP_NumericalInput.SetError(tb, "実数のみ入力");

                hasError = true;
                e.Cancel = true;
                return;
            }
        }

        private void IsReal_Validated(object sender, EventArgs e)
        {
            hasError = false;
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cea = new CancelEventArgs(false);
                IsReal_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsReal_Validated(sender, e);
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (hasError) return;
            ApplyButton_Click(sender, e);
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (hasError) return;
            Editor.Args = new TTCDataUtils.MagicFormulaArguments
                (
                    double.Parse(SATB.Text),
                    double.Parse(SRTB.Text),
                    double.Parse(FZTB.Text),
                    double.Parse(IATB.Text),
                    double.Parse(PTB.Text),
                    double.Parse(TTB.Text)
                );
            Editor.Replot();
            
        }


        private void MFGraphDialog_Load(object sender, EventArgs e)
        {
            selected[TireDataColumn.SA] = 0;
            selected[TireDataColumn.SR] = 0;
            selected[TireDataColumn.FZ] = 0;
            selected[TireDataColumn.IA] = 0;
            selected[TireDataColumn.P] = 0;
            selected[TireDataColumn.TSTC] = 0;

            selectedTB[TireDataColumn.SA] = SATB;
            selectedTB[TireDataColumn.SR] = SRTB;
            selectedTB[TireDataColumn.FZ] = FZTB;
            selectedTB[TireDataColumn.IA] = IATB;
            selectedTB[TireDataColumn.P] = PTB;
            selectedTB[TireDataColumn.TSTC] = TTB;
            ResetChart(TireDataColumn.SA);
        }

        public void ResetChart(TireDataColumn column)
        {
            MainChart.ChartAreas[0].AxisX.Title = column.ToString();
            MainChart.ChartAreas[0].AxisY.Title = "データ数（個）";
            Series Data = new Series();
            Series Selected = new Series();

            Data.LegendText = "Data";
            Selected.LegendText = "選択";



            Data.ChartType = SeriesChartType.Column;
            Selected.ChartType = SeriesChartType.RangeColumn;
            Selected["PointWidth"] = "1";
            Data["PointWidth"] = "0.6";

            Data.Color = Color.Green;
            Selected.Color = Color.Red;

            MainChart.Series.Clear();
            MainChart.Series.Add(Data);
            MainChart.Series.Add(Selected);
            CalcBin_ShowGraph(column);
        }

        void SelectBin(TireDataColumn column)
        {
            TireDataSet tds = Editor.SelectedDataSet;
            var table = (TableCB.SelectedItem as Table?).Value;
            if (SourceCB.SelectedItem as ProjectTree.Node_TireDataSet !=null)
            {
                var ntds = SourceCB.SelectedItem as ProjectTree.Node_TireDataSet;
                tds = ntds.GetIDataSet().GetDataSet();
            }
            if (tds.GetDataList(table).Count == 0) return;

            ValidateCount();
            int i = SortedBin[selected[column]].Item1;
            

            var max = tds.MaxminSet.Limit(table).Max[column];
            var min = tds.MaxminSet.Limit(table).Min[column];
            double dif = (max - min) / Bin.Count;
            var data = new DataPoint(i * dif + dif / 2 + min, Bin[i]);
            MainChart.Series[SeriesSelected].Points.Clear();
            MainChart.Series[SeriesSelected].Points.Add(data);
            for (int j = 0; j < Bin.Count; ++j)
            {
                if (j == i) continue;
                data = new DataPoint(j * dif + dif / 2 + min, 0);
                MainChart.Series[SeriesSelected].Points.Add(data);
            }
        }

        private void CalcBin_ShowGraph(TireDataColumn column)
        {
            TireDataSet tds = Editor.SelectedDataSet;
            if (SourceCB.SelectedItem as ProjectTree.Node_TireDataSet !=null)
            {
                var ntds = SourceCB.SelectedItem as ProjectTree.Node_TireDataSet;
                tds = ntds.GetIDataSet().GetDataSet();
            }

            var table = (TableCB.SelectedItem as Table?).Value;

            Target = new List<double>();
            foreach (var data in tds.GetDataList(table))
            {
                Target.Add(data[column]);
            }
            Target.Sort();
            MainChart.Series[SeriesData].Points.Clear();
            if (Target.Count == 0) return;
            

            var max = tds.MaxminSet.Limit(table).Max[column];
            var min = tds.MaxminSet.Limit(table).Min[column];
            var bin = CalculateMinimumEntropyBin(Target, TireData.Resolution()[column], max, min);
            double dif = (max - min) / bin.Count;

            

            for (int i = 0; i < bin.Count; ++i)
            {
                var data = new DataPoint(i * dif + dif / 2 + min, bin[i]);
                MainChart.Series[SeriesData].Points.Add(data);
            }

            //選択

            SortedBin = new List<Tuple<int, double>>(bin.Count);
            for (int i = 0; i < bin.Count; ++i)
            {
                SortedBin.Add(new Tuple<int, double>(i, bin[i]));
            }
            SortedBin.Sort(delegate (Tuple<int, double> ls, Tuple<int, double> rh)
            {
                if (ls.Item2 == rh.Item2) return 0;
                return ls.Item2 - rh.Item2 > 0 ? -1 : 1;
            }
            );

            Bin = bin;
            SelectBin(column);
        }

        List<int> CalculateMinimumEntropyBin(List<double> list, double resolustion, double max, double min)
        {
            int i = fistBins;
            int count = 0;
            double minentropy = double.MaxValue;
            double entropy;
            var Bin = new List<int>(fistBins);
            int NumBinsMax = (int)((max - min) / resolustion);
            int NumBins = i;
            while (count < 100 && i < NumBinsMax)
            {
                Bin.Clear();
                entropy = CalculateEntropy(i, list, max, min, Bin);

                if (entropy > minentropy)
                {
                    ++count;
                }
                else
                {
                    NumBins = i;
                    count = 0;
                    minentropy = entropy;
                }
                i = (int)Math.Round(i * coe);
            }
            CalculateEntropy(NumBins, list, max, min, Bin);
            return Bin;
        }

        double CalculateEntropy(int numBins, List<double> list, double max, double min, List<int> Bin)
        {
            double entropy = 0;
            double dif = (max - min) / numBins;
            Bin.Clear();
            int count = 0;
            for (int i = 1; i < numBins; ++i)
            {
                Bin.Add(0);
                double binMax = min + dif * i;
                for (; count < list.Count && list[count] <= binMax; ++count)
                {
                    Bin[i - 1] = Bin[i - 1] + 1;
                }
                double p = (double)Bin[i - 1] / (double)list.Count;
                if (p != 0)
                {
                    entropy += -p * Math.Log(p);
                }

            }
            {
                Bin.Add(0);
                for (; count < list.Count; ++count)
                {
                    Bin[numBins - 1] = Bin[numBins - 1] + 1;
                }
                double p = (double)Bin[numBins - 1] / (double)list.Count;
                entropy += -p * Math.Log(p);
            }
            entropy /= Math.Log(numBins);
            return entropy;
        }

        Dictionary<TireDataColumn,int> selected  = new Dictionary<TireDataColumn, int>();
        Dictionary<TireDataColumn, TextBox> selectedTB = new Dictionary<TireDataColumn, TextBox>();
        TireDataColumn SelectedColumn = TireDataColumn.SA;
        static int fistBins = 10;
        static double coe = 1.1;
        
        List<double> Target;
        List<int> Bin;
        List<Tuple<int, double>> SortedBin;

        private void TB_Enter(object sender, EventArgs e)
        {
            SelectedColumn = TireDataColumn.TSTC;
            if (sender == SATB) SelectedColumn = TireDataColumn.SA;
            if (sender == SRTB) SelectedColumn = TireDataColumn.SR;
            if (sender == FZTB) SelectedColumn = TireDataColumn.FZ;
            if (sender == IATB) SelectedColumn = TireDataColumn.IA;
            if (sender == PTB) SelectedColumn = TireDataColumn.P;

            ValidateCount();

            ResetChart(SelectedColumn);
        }

        private void NextPreviouse_Click(object sender, EventArgs e)
        {
            if (sender == PreviousButton)
                --selected[SelectedColumn];
            if (sender == NextButton)
                ++selected[SelectedColumn];
            ValidateCount();
            SelectBin(SelectedColumn);
        }

        private void ValidateCount()
        {
            SelectedIndexTB.Text = (selected[SelectedColumn] + 1).ToString();
            if (selected[SelectedColumn] >= SortedBin.Count) selected[SelectedColumn] = SortedBin.Count - 1;
            if (selected[SelectedColumn] < 0) selected[SelectedColumn] = 0;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            TireDataSet tds = Editor.SelectedDataSet;
            var table = (TableCB.SelectedItem as Table?).Value;
            if (SourceCB.SelectedItem as ProjectTree.Node_TireDataSet != null)
            {
                var ntds = SourceCB.SelectedItem as ProjectTree.Node_TireDataSet;
                tds = ntds.GetIDataSet().GetDataSet();
            }
            if (tds.GetDataList(table).Count == 0) return;

            ValidateCount();
            int i = SortedBin[selected[SelectedColumn]].Item1;
            var max = tds.MaxminSet.Limit(table).Max[SelectedColumn];
            var min = tds.MaxminSet.Limit(table).Min[SelectedColumn];
            double dif = (max - min) / Bin.Count;
            var value = i * dif + dif / 2 + min;


            selectedTB[SelectedColumn].Text = value.ToString();
            TextBox_KeyDown(selectedTB[SelectedColumn], new KeyEventArgs(Keys.Enter));
        }

        private void IsNInt_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);

            string s = tb.Text;
            if (!StaticFunctions.IsNInt(s))
            {
                this.EP_NumericalInput.SetError(tb, "整数のみ入力");
                e.Cancel = true;
                return;
            }
        }

        private void IsNInt_Validated(object sender, EventArgs e)
        {
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
            selected[SelectedColumn] = int.Parse(SelectedIndexTB.Text) - 1;
            ValidateCount();
            SelectBin(SelectedColumn);
        }

        private void Nint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cea = new CancelEventArgs(false);
                IsNInt_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsNInt_Validated(sender, e);
                }
            }
        }

        private void SorceDataSIC(object sender, EventArgs e)
        {
            if (init) return;
            ResetChart(SelectedColumn);
        }
    }
}
