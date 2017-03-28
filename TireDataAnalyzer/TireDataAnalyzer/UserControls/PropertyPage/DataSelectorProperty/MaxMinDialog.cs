using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls
{
    public partial class MaxMinDialog : Form
    {
        public MaxMinDialog(TireDataColumn column, Table table, TireDataSetSelector selector, TireDataConstrain constrain = null)
        {
            InitializeComponent();
            Origin = constrain;

            Column = column;
            Table = table;
            Selector = selector;

            Target = new List<double>();
            foreach( var data in selector.Target(table))
            {
                Target.Add(data[column]);
            }
            Target.Sort();

            MainChart.ChartAreas[0].AxisX.Title = Column.ToString();
            MainChart.ChartAreas[0].AxisY.Title = "データ数（個）";
            Series exSeries = new Series();
            Series nexSeries = new Series();
            Series SelectedAreaSeries = new Series();

            exSeries.LegendText = "抽出データ";
            nexSeries.LegendText = "削除データ";
            SelectedAreaSeries.LegendText = "選択範囲";

            

            exSeries.ChartType = SeriesChartType.StackedColumn;
            nexSeries.ChartType = SeriesChartType.StackedColumn;
            SelectedAreaSeries.ChartType = SeriesChartType.Area;

            exSeries.Color = Color.Green;
            nexSeries.Color = Color.Red;
            SelectedAreaSeries.Color = Color.LightBlue;

            MainChart.Series.Clear();
            MainChart.Series.Add(SelectedAreaSeries);
            MainChart.Series.Add(exSeries);
            MainChart.Series.Add(nexSeries);
            
        }

        private void MaxMinDialog_Load(object sender, EventArgs e)
        {
            

            Max = Selector.Maxmin(Table).Max[Column];
            Min = Selector.Maxmin(Table).Min[Column];
            MaxLabel.Text = Max.ToString();
            MinLabel.Text = Min.ToString();
            var constrains = Selector.Constrains(Table)[Column];
            var names = new List<string>();
            foreach(var constrain in constrains)
            {
                names.Add(constrain.Name);
            }

            if (Origin == null)
            {
                Constrain = new TireDataConstrain(StaticFunctions.GetUniqueName(names,"新規セレクタ"), Column, Max, Min, false);
            }
            else
            {
                Constrain = Origin.Copy();
            }

            NameTB.Text = Constrain.Name;
            NotCheckBox.Checked = Constrain.Not;
            MaxTB.Text = Constrain.Max.ToString();
            MinTB.Text = Constrain.Min.ToString();

            CalculateMinimumEntropy(Target, Max, Min);
            Extract();

        }

        void Extract()
        {
            ExtractedBin = new List<int>(NumBins);
            RemovedBin = new List<int>(NumBins);
            double dif = (Max - Min) / NumBins;
            var dicconstrains = Selector.Constrains(Table);
            var constrains = dicconstrains[Column];

            int count = 0;
            for (int i = 1; i <= NumBins; ++i)
            {
                ExtractedBin.Add(0);
                RemovedBin.Add(0);
                double binMax = Min + dif * i;
                if(i == NumBins) binMax = Max;
                for (; count < Target.Count && Target[count] <= binMax; ++count)
                {

                    bool remove = false;
                    foreach (var constrain in constrains)
                    {
                        if (constrain == Origin || !constrain.Not) continue;
                        if (!constrain.Evaluate(Target[count]))
                        {
                            remove = true;
                            break;
                        }
                    }
                    
                    if (Constrain.Not && !Constrain.Evaluate(Target[count]))
                    {
                        remove = true;
                    }
                    if (!remove)
                    {
                        remove = true;
                        int counttemp = 0;
                        foreach (var constrain in constrains)
                        {
                            if (constrain == Origin || constrain.Not) continue;
                            ++counttemp;
                            if (constrain.Evaluate(Target[count]))
                            {
                                remove = false;
                                break;
                            }
                        }
                        if (counttemp == 0) remove = false;
                        if (!Constrain.Not)
                        {
                            remove = true;
                            if (Constrain.Evaluate(Target[count]))

                            {
                                remove = false;
                            }
                        }
                        
                    }
                    
                    if (remove)
                    {
                        RemovedBin[i - 1] = RemovedBin[i - 1] + 1;
                    }
                    else
                    {
                        ExtractedBin[i - 1] = ExtractedBin[i - 1] + 1;
                    }

                }
                
            }


            MainChart.Series[0].Points.Clear();
            MainChart.Series[1].Points.Clear();
            MainChart.Series[2].Points.Clear();
            for (int i = 0; i < NumBins; ++i)
            {
                var expoint = new DataPoint(i * dif + dif / 2+Min, ExtractedBin[i]);
                var nexpoint = new DataPoint(i * dif + dif / 2+Min, RemovedBin[i]);
                MainChart.Series[1].Points.Add(expoint);
                MainChart.Series[2].Points.Add(nexpoint);
            }
            if(Constrain.Not)
            {
                MainChart.Series[0].Points.Add(new DataPoint(Min,Highest));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Min, Highest));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Min, 0));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Max, 0));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Max, Highest));
                MainChart.Series[0].Points.Add(new DataPoint(Max, Highest));
            }
            else
            {
                MainChart.Series[0].Points.Add(new DataPoint(Min, 0));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Min, 0));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Min, Highest));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Max, Highest));
                MainChart.Series[0].Points.Add(new DataPoint(Constrain.Max, 0));
                MainChart.Series[0].Points.Add(new DataPoint(Max, 0));
            }
        }

        void CalculateMinimumEntropy(List<double> list, double max, double min)
        {
            int i = fistBins;
            NumBins = i;
            int count = 0;
            double minentropy = double.MaxValue;
            double entropy;
            var Bin = new List<int>(NumBins);
            int NumBinsMax = (int)((max - min) / TireData.Resolution()[Column]);
            Highest = 0;
            int HighestTemp = 0;
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
                    HighestTemp = Highest;
                    NumBins = i;
                    count = 0;
                    minentropy = entropy;
                }
                i = (int)Math.Round(i * coe);
            }
            Highest = (int)(HighestTemp*1.1);
            if (HighestTemp == 0)
            {
                entropy = CalculateEntropy(i, list, max, min, Bin);
                Highest = (int)(Highest * 1.1);
            }
           
        }

        double CalculateEntropy(int numBins, List<double> list, double max, double min, List<int> Bin)
        {
            double entropy = 0;
            double dif = (max - min) / numBins;

            int count = 0;
            for(int i = 1; i< numBins; ++i)
            {
                Bin.Add(0);
                double binMax = min + dif * i;
                for(;count < list.Count && list[count] <= binMax; ++count)
                {
                   Bin[i - 1] = Bin[i - 1]+1;
                }
                double p = (double)Bin[i - 1] / (double)list.Count;
                if(p != 0)
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
            Highest = 0;
            foreach (var num in Bin)
            {
                if (Highest < num)
                {
                    Highest = num;
                }
            }
            entropy /= Math.Log(numBins);
            return entropy;
        }

        double Max;
        double Min;

        static int fistBins = 10;
        static double coe = 1.1;

        Table Table;
        TireDataColumn Column;
        public TireDataConstrain Constrain;
        public TireDataConstrain Origin;
        TireDataSetSelector Selector;
        List<double> Target;
        List<int> ExtractedBin;
        List<int> RemovedBin;
        int NumBins;
        int Highest;

        private void OKButton_Click(object sender, EventArgs e)
        {
            if(NameTB.Text != "" )
                Constrain.Name = NameTB.Text;
            this.DialogResult = DialogResult.OK;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
        }

        private void NotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Constrain.Not = NotCheckBox.Checked;
            Extract();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TB_Leave(sender, e);
                e.Handled = true;
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                if (this.Text.IndexOf(".") >= 0 || this.Text.Length == 0)
                {
                    e.Handled = true;
                }
            }
            else if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void TB_Leave(object sender, EventArgs e)
        {
            double value = 0;
            TextBox tb = sender as TextBox;
            if (double.TryParse(tb.Text, out value))
            {
                if (tb == MinTB)
                {

                    if (value < Min) value = Min;
                    if (value > Constrain.Max) value = Constrain.Max;
                    Constrain.Min = value;

                }
                if (tb == MaxTB)
                {
                    if (value > Max) value = Max;
                    if (value < Constrain.Min) value = Constrain.Min;
                    Constrain.Max = value;

                }
            }
            MaxTB.Text = Constrain.Max.ToString();
            MinTB.Text = Constrain.Min.ToString();
            Extract();
        }
    }
}
