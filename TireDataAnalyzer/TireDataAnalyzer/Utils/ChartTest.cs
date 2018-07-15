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

namespace TireDataAnalyzer
{
    public partial class ChartTest : Form
    {
        public ChartTest()
        {
            InitializeComponent();
            // フォームをロードするときの処理
            chart1.Series.Clear();  // ← 最初からSeriesが1つあるのでクリアします
            chart1.ChartAreas.Clear();

            // ChartにChartAreaを追加します
            string chart_area1 = "Area1";
            chart1.ChartAreas.Add(new ChartArea(chart_area1));
            // ChartにSeriesを追加します
            string legend1 = "Graph1";
            chart1.Series.Add(legend1);
            // グラフの種別を指定
            chart1.Series[legend1].ChartType = SeriesChartType.FastPoint; // ポイントグラフを指定
                                                                      //
            chart1.Series[legend1].MarkerColor = Color.Blue;
            // データを用意します
            double[] x_values = new double[5] { 1.0, 0.8, 1.8, 0.2, 1.2 };
            double[] y_values = new double[5] { 1.0, 1.2, 0.8, 1.8, 0.2 };

            // データをシリーズにセットします
            for (int i = 0; i < y_values.Length; i++)
            {
                DataPoint dp = new DataPoint((double)x_values[i], y_values[i]);
                
                chart1.Series[legend1].Points.Add(dp);
            }
            chart1.Series[legend1].Points[3].Color = Color.Red;
            chart1.Series[legend1].Points[3].MarkerColor = Color.Red;
            chart1.Series[legend1].MarkerColor = Color.Blue;
        }
    }
}
