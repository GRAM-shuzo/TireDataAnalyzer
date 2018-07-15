using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;
using System.Windows.Forms.DataVisualization.Charting;
using External;

namespace TireDataAnalyzer.UserControls
{

    using XY2 = Tuple<double, double>;
    public partial class TireDataViewer : UserControl
    {
        [Serializable]
        public class CMaxMinInterval
        {
            public double XMin = Double.NaN;
            public double XMax = Double.NaN;
            public double XInterval = 0;

            public double YMin = Double.NaN;
            public double YMax = Double.NaN;
            public double YInterval = 0;
        }
        public TireDataViewer()
        {
            saveData = new SaveData();
            InitializeComponent();
            Axis = EnumAxis.RawTireData;
            chart.Titles.Add(new Title());
            SetMenu();
            X1 = TireDataColumn.SA;
            Y1 = TireDataColumn.FY;
            X2 = MagicFormulaInputVariables.SA;
            Y2 = MagicFormulaOutputVariables.FY;


            this.KeyDown += Chart_KeyDown;
            
            //chart.Serializer.Save()
        }

        private SaveData saveData;
        public void SetSaveData(SaveData data)
        {
            saveData = data;
            saveData.DicMagicFormula = new Dictionary<string, TireMagicFormula>();
            saveData.DicTireData = new Dictionary<string, List<TireData>>();
            saveData.DicNotManagedData = new Dictionary<string, List<XY>>();
            saveData.DicSeries = new Dictionary<string, Series>();
            OnSaveDataLoad();
        }

        public SaveData GetSaveData()
        {
            foreach(var kv in saveData.DicSeries)
            {
                kv.Value.Name = kv.Key;
            }
            saveData.mem = new MemoryStream();
            chart.Serializer.Save(saveData.mem);
            
            return saveData;
        }
        public void OnSaveDataLoad()
        {
            this.chart.Serializer.Load(saveData.mem);
            foreach ( var series in chart.Series)
            {
                saveData.DicSeries.Add(series.Name, series);
            }
            if (saveData.TableInfo == null) saveData.TableInfo = new Dictionary<string, Table>();
            SetMenu();
        }

        public TireDataViewer(EnumAxis axisType)
            :this()
        {

            Axis = axisType;
        }
        public enum EnumAxis
        {
            RawTireData,
            MagicFormula
        }

        public class XY
        {
            public XY(MagicFormulaArguments xx, double yy)
            {
                x = xx;
                y = yy;
            }
            public MagicFormulaArguments x;
            public double y;
        }
        

        public enum DataType
        {
            RawTireData,
            MagicFormula,
            NonManagedData,
            NotDefined
        }
              
        public int GraphSample
        {
            get
            {
                return saveData.graphSample;
            }
            set
            {
                saveData.graphSample = value;
                DrawAllGraph();
            }
        }

        public CMaxMinInterval MaxMinInterval
        {
            get
            {
                return saveData.maxMinInterval;
            }
            set
            {
                if(value != null)
                {
                    chart.ChartAreas[0].AxisX.Minimum = value.XMin;
                    chart.ChartAreas[0].AxisX.Maximum = value.XMax;
                    chart.ChartAreas[0].AxisX.Interval = value.XInterval;
                    chart.ChartAreas[0].AxisY.Minimum = value.YMin;
                    chart.ChartAreas[0].AxisY.Maximum = value.YMax;
                    chart.ChartAreas[0].AxisY.Interval = value.YInterval;

                    saveData.maxMinInterval = value;
                }
                else
                {
                    chart.ChartAreas[0].AxisX.Minimum = double.NaN;
                    chart.ChartAreas[0].AxisX.Maximum = double.NaN;
                    chart.ChartAreas[0].AxisX.Interval = 0;
                    chart.ChartAreas[0].AxisY.Minimum = double.NaN;
                    chart.ChartAreas[0].AxisY.Maximum = double.NaN;
                    chart.ChartAreas[0].AxisY.Interval = 0;
                }
                
            }
        }
        public bool MenuEnabled
        {
            get
            {
                return saveData.menuEnabled;
            }
            set
            {
                saveData.menuEnabled = value;
                SetMenu();
            }
        }
        public EnumAxis Axis
        {
            get
            {
                return saveData.axis;
            }
            set
            {
                if(saveData.axis != value)
                {
                    saveData.axis = value;
                    SetMenu();
                }       
            }
        }
        
        public int NumPoints { get { return saveData.numPoints; } set { saveData.numPoints = value; } }

        public bool AutoScaleX { get { return saveData.AutoScaleX; }  set { saveData.AutoScaleX = value; } }
        public bool AutoScaleY { get { return saveData.AutoScaleY; }  set { saveData.AutoScaleY = value; } }
        public TireDataColumn X1 { get { return saveData.X1; } private set { saveData.X1 = value; } }
        public TireDataColumn Y1 { get { return saveData.Y1; } private set { saveData.Y1 = value; } }
        public MagicFormulaInputVariables X2 { get { return saveData.X2; } private set { saveData.X2 = value; } }
        public MagicFormulaOutputVariables Y2 { get { return saveData.Y2; } private set { saveData.Y2 = value; } }

        public ChartArea GetArea()
        {
            return chart.ChartAreas[0];
        }
        public SeriesChartType GetChartType(string legendText)
        {
            return GetSeries(legendText).ChartType;
        }
        public Color GetColor(string legendText)
        {
            return GetSeries(legendText).Color;
        }
        public int GetLineWidth(string legendText)
        {
            return GetSeries(legendText).BorderWidth;
        }
        public DataType GetDataType(string legendText)
        {
            DataType dataType = DataType.NotDefined;
            if(!saveData.DicDataType.TryGetValue(legendText, out dataType)) dataType = DataType.NotDefined;
            return dataType;
        }


        public Legend GetLegend()
        {
            return chart.Legends[0];
        }
        public void SetLegend(Legend l)
        {
            chart.Legends[0] = l;
        }

        public MagicFormulaArguments GetArguments(string legendText)
        {
            MagicFormulaArguments args = null;
            if(legendText != null)
                saveData.DicMFArgs.TryGetValue(legendText, out args);
            return args;
        }
        public List<string> GetLegents()
        {
            var s = new List<string>();
            foreach(var kv in saveData.DicSeries)
            {
                s.Add(kv.Key);
            }
            s.Sort(
                delegate(string lhs, string rhs)
                {
                    if (lhs == rhs) return 0;
                    Series slhs = GetSeries(lhs);
                    Series srhs = GetSeries(rhs);
                    return chart.Series.IndexOf(slhs) > chart.Series.IndexOf(srhs) ? 1 : -1;
                }
                );

            return s;
        }
        public void SetTitle(string text, Docking? dock, Font font, Color? color)
        {
                chart.Titles[0].Text = text;
            if (dock != null)
                chart.Titles[0].Docking = dock.Value;
            if (font != null)
                chart.Titles[0].Font = font;
            if (color != null)
                chart.Titles[0].ForeColor = color.Value;
        }
        public Title GetTitle()
        {
            return chart.Titles[0];
        }
        public void SetColor(Color color, string legendText)
        {
            GetSeries(legendText).Color = color;
        }
        public void SetChartType(SeriesChartType ct, string legendText)
        {
            if (ct == SeriesChartType.FastPoint) ct = SeriesChartType.Point;
            GetSeries(legendText).ChartType = ct;
        }
        public void SetLineWidth(int width, string legendText)
        {
            GetSeries(legendText).BorderWidth = width;
            GetSeries(legendText).MarkerSize = width;
        }

        public void SetAxis(TireDataColumn x, TireDataColumn y)
        {
            if (Axis != EnumAxis.RawTireData) throw new Exception("軸タイプ違反");
            ClearMenuCheck();
            X1 = x;
            Y1 = y;
            (xaxisMenu.DropDownItems[(int)x] as ToolStripMenuItem).Checked = true;
            (yaxisMenu.DropDownItems[(int)y] as ToolStripMenuItem).Checked = true;
            //軸ラベルの設定
            chart.ChartAreas[0].AxisX.Title = TireData.Label[X1];
            chart.ChartAreas[0].AxisY.Title = TireData.Label[Y1];
        }
        public void SetAxis(MagicFormulaInputVariables x, MagicFormulaOutputVariables y)
        {
            if (Axis == EnumAxis.RawTireData) throw new Exception("軸タイプ違反");
            ClearMenuCheck();
            X2 = x;
            if(X2 == MagicFormulaInputVariables.FY)
            {
                Y2 = MagicFormulaOutputVariables.FX;
            }
            else
            {
                Y2 = y;
            }
            X1 = ConvertEnum(X2);


            (xaxisMenu.DropDownItems[(int)x] as ToolStripMenuItem).Checked = true;
            (yaxisMenu.DropDownItems[(int)y] as ToolStripMenuItem).Checked = true;
            //軸ラベルの設定
            chart.ChartAreas[0].AxisX.Title = X2.ToString();
            chart.ChartAreas[0].AxisY.Title = Y2.ToString();
        }
        public void SetDrawPriority(int diff, string legendText)
        {
            var s = GetSeries(legendText);
            int index = chart.Series.IndexOf(s);
            index = index + diff;
            index = index < 0 ? 0 : index;
            index = index > chart.Series.Count-1 ? chart.Series.Count - 1 : index;
            chart.Series.Remove(s);
            chart.Series.Insert(index, s);
        }

        public void SetDataListRefMF(string dataListLegend, string magicFormulaLegend)
        {
            saveData.DicTireDataRef[dataListLegend] = magicFormulaLegend;
        }
        public string GetDataListRefMF(string dataListLegend)
        {
            string mfLegend = null;
            if(dataListLegend != null)
            {
                saveData.DicTireDataRef.TryGetValue(dataListLegend, out mfLegend);
                
            }
            return mfLegend;
        }
        public void SetDataList(List<TireData> dataList, Table table, string legendText)
        {
            saveData.DicTireData[legendText] = dataList;
            saveData.DicDataType[legendText] = DataType.RawTireData;
            saveData.TableInfo[legendText] = table;
        }
        public Table? GetTableInfo(string legendText)
        {
            Table t;
            if(saveData.TableInfo.TryGetValue(legendText, out t))
            {
                return t;
            }
            return null;
        }

        public void SetMagicFormula(TireMagicFormula formula, MagicFormulaArguments constantArgs, string legendText)
        {
            //if (Axis == EnumAxis.RawTireData) throw new Exception("軸タイプ違反");
            saveData.DicMagicFormula[legendText] = formula;
            saveData.DicDataType[legendText] = DataType.MagicFormula;
            saveData.DicMFArgs[legendText] = constantArgs;
        }
        public void SetNonManagedData(List<XY> xyList, string legendText)
        {
            if (Axis == EnumAxis.RawTireData) throw new Exception("軸タイプ違反");
            saveData.DicNotManagedData[legendText] = xyList;
            saveData.DicDataType[legendText] = DataType.NonManagedData;
        }
        public void SetEnable(bool enable, string legendText)
        {
            GetSeries(legendText).Enabled = enable;
        }
        public bool GetEnable(string legendText)
        {
            return GetSeries(legendText).Enabled;
        }

        
        public void LegendTextOverride(string legendText, string overrideText)
        {
            saveData.legendTextOverride[legendText] = overrideText;
        }
        public string LegendTextOverride(string legendText)
        {
            string overrideText = null;
            if (legendText != null)
            {
                saveData.legendTextOverride.TryGetValue(legendText, out overrideText);

            }
            return overrideText;
        }
        public void Remove(string legendText)
        {
            if (legendText == null) return;
            Series series;
            if (saveData.DicSeries.TryGetValue(legendText, out series))
            {
                saveData.DicSeries.Remove(legendText);
                chart.Series.Remove(series);
            }
        }

        private Series GetSeries(string legendText)
        {
            Series series;
            if (!saveData.DicSeries.TryGetValue(legendText, out series))
            {
                saveData.DicSeries[legendText] = new Series();
                chart.Series.Add(saveData.DicSeries[legendText]);
            }
            series = saveData.DicSeries[legendText];
            series.Legend = "legend";

            string text;

            try
            {
                if (saveData.legendTextOverride.TryGetValue(legendText, out text) && text != null)
                {
                    series.LegendText = text;
                }
                else
                {
                    series.LegendText = legendText;
                }
            }
            catch(Exception)
            {
                series.LegendText = legendText;
            }
            return series;
        }
        private Color GetGradationColorFromRangedValue(double min, double max, double value)
        {
            if(max < min)
            {
                double temp = max;
                max = min;
                min = temp;
            }
            if (max - min == 0) return Color.Red;
            if (value > max) value = max;
            if (value < min) value = min;
            value = value - min;
            value = value / (max - min);
            value = value * 8;
            double r = 0;
            double g = 0;
            double b = 0;
            if( value >= 3 && value <= 7)
            {
                r = Math.Min((value - 3) * 0.5, 1);
            }
            if(value >7 )
            {
                r = 1-(value - 7) * 0.5;
            }

            if (value >= 1 && value <= 5)
            {
                g = Math.Min((value - 1) * 0.5, 1);
            }
            if (value > 5)
            {
                g = Math.Max( 1-(value - 5) * 0.5, 0);
            }

            if (value <= 3)
            {
                b = Math.Min((value + 1) * 0.5, 1);
            }
            if (value > 5)
            {
                b = Math.Max(1 - (value - 3) * 0.5, 0);
            }
            int rr = (int)(255 * r);
            int gg = (int)(255 * g);
            int bb = (int)(255 * b);
            return Color.FromArgb(rr, gg, bb);
        }

        TireDataColumn? GradationColumn = null;
        double GradationMax = 0;
        double GradationMin = 0;
        public void SetGradation(TireDataColumn? column, double min, double max, string legendText)
        {
            GradationColumn = column;
            GradationMax = max;
            GradationMin = min;
        }
        public Tuple<TireDataColumn?, double, double> GetGradation(string legendText)
        {
            return new Tuple<TireDataColumn?, double, double>(GradationColumn, GradationMax, GradationMin);
        }

        public void DrawGraph(string legendText)
        {

            var series = GetSeries(legendText);
            var dataType = new DataType();
            if( !saveData.DicDataType.TryGetValue(legendText, out dataType) ) return;
            double xmax = double.MinValue;
            double xmin = double.MaxValue;
            if(Axis != EnumAxis.RawTireData)
            {
                if (MaxMinInterval == null)
                {
                    
                    switch (X2)
                    {
                        case MagicFormulaInputVariables.SA:
                            xmax = 13;
                            xmin = -13;
                            break;
                        case MagicFormulaInputVariables.SR:
                            xmax = 0.4;
                            xmin = -0.4;
                            break;
                        case MagicFormulaInputVariables.FZ:
                            xmax = 2000;
                            xmin = 0;
                            break;
                        case MagicFormulaInputVariables.IA:
                            xmax = 2;
                            xmin = -2;
                            break;
                        case MagicFormulaInputVariables.P:
                            xmax = 100;
                            xmin = 0;
                            break;
                        case MagicFormulaInputVariables.T:
                            xmax = 60;
                            xmin = 10;
                            break;
                        case MagicFormulaInputVariables.FY:
                            xmax = 2000;
                            xmin = -2000;
                            break;
                    }
                }
                else
                {
                    xmax = saveData.maxMinInterval.XMax;
                    xmin = saveData.maxMinInterval.XMin;
                }
                
            }



            series.Points.Clear();
            
            if (dataType == DataType.RawTireData && Axis != EnumAxis.MagicFormula)
            {
                var dataList = saveData.DicTireData[legendText];
                //var maxMinInterval = new CMaxMinInterval();
                int points = saveData.numPoints < 0 ? dataList.Count : saveData.numPoints;
                for (int i = 0; i < Math.Min(dataList.Count, points); ++i)
                {
                    var data = dataList[i];
                    if (xmax < data[X1]) xmax = data[X1];
                    if (xmin > data[X1]) xmin = data[X1];


                    if (!StaticFunctions.IsNotValidValue(data[X1]) && !StaticFunctions.IsNotValidValue(data[Y1]))
                    {
                        var point = new DataPoint(data[X1], data[Y1]);
                        //point.Color = Color.Red;
                        //point.MarkerColor = Color.Red;
                        series.Points.Add(point);

                    }
                }
                //maxMinInterval.XMax = xmax;
                //maxMinInterval.XMin = xmin;
                //MaxMinInterval = maxMinInterval;
            }
            else if (dataType == DataType.RawTireData)
            {
                var dataList = saveData.DicTireData[legendText];
                //var maxMinInterval = new CMaxMinInterval();
                bool Y1Convert = false;
                Y1 = TireDataColumn.FY;
                try
                {
                    Y1 = ConvertEnum(Y2);
                    Y1Convert = true;
                    var mf = saveData.DicMagicFormula[saveData.DicTireDataRef[legendText]];
                    var points = saveData.numPoints > 0 ? saveData.numPoints : dataList.Count;
                    for (int i = 0; i < Math.Min(dataList.Count, points); ++i)
                    {
                        var data = dataList[i];
                        var sa = data.SA;
                        var sr = data.SR;
                        if (
                            (Y2 == MagicFormulaOutputVariables.FY_BCD) &&
                            (Math.Abs(sa) < 0.5 || 1 < Math.Abs(sa))
                           ) continue;
                        if (
                            (Y2 == MagicFormulaOutputVariables.FX_BCD) &&
                            (Math.Abs(sr) < 0.05 || 1 < Math.Abs(sr))
                           ) continue;

                        if (xmax < data[X1]) xmax = data[X1];
                        if (xmin > data[X1]) xmin = data[X1];


                        if (!StaticFunctions.IsNotValidValue(data[X1]) && !StaticFunctions.IsNotValidValue(data[Y1]))
                        {
                            var mfa = new MagicFormulaArguments(data);

                            double y = 0;
                            if (Y1Convert)
                            {
                                y = data[Y1];
                                if (Y2 == MagicFormulaOutputVariables.FY_BCD)
                                {
                                    sa = mf.GetNormalizedValue(new MagicFormulaArguments(sa, 0, 0, 0, 0, 0)).SA;
                                    y /= sa;
                                }
                                if (Y2 == MagicFormulaOutputVariables.FX_BCD)
                                {
                                    sr = mf.GetNormalizedValue(new MagicFormulaArguments(0, sr, 0, 0, 0, 0)).SR;
                                    y /= sr;
                                }
                            }
                            else
                            {
                                y = mf.GetVariables(Y2, mfa);
                            }
                            if (!StaticFunctions.IsNotValidValue(data[X1]) && !StaticFunctions.IsNotValidValue(y))
                            {
                                var point = new DataPoint(data[X1], y);
                                //point.Color = Color.Red;
                                //point.MarkerColor = Color.Red;
                                Color c = GetGradationColorFromRangedValue(0, 1200, data.FZ);
                                point.MarkerColor = c;
                                point.Color = c;
                                series.Points.Add(point);
                            }
                                
                        }
                    }
                }
                catch (Exception)
                {

                }
                
            }
            else if (Axis == EnumAxis.MagicFormula && dataType == DataType.MagicFormula && X2 != MagicFormulaInputVariables.FY)
            {
                var magicFormula = saveData.DicMagicFormula[legendText];
                var dif = (xmax - xmin) / (double)(GraphSample);
                var arg = saveData.DicMFArgs[legendText].Copy();
                if(X2 == MagicFormulaInputVariables.SR && Y2 == MagicFormulaOutputVariables.FY)
                {
                    if(arg.SA == 0)
                    {
                        var Ylim = magicFormula.FY.GetPeak(arg);
                        arg.SA = Ylim.Item1;
                    }
                    
                }
                if (X2 == MagicFormulaInputVariables.SA && Y2 == MagicFormulaOutputVariables.FX)
                {
                    if(arg.SR == 0)
                    {
                        var Xlim = magicFormula.FX.GetPeak(arg);
                        arg.SR = Xlim.Item1;
                    }
                    
                }
                for (int i = 0; i <= GraphSample; ++i)
                {
                    var x = xmin + i * dif;
                    arg.setValue(X2, x);
                    double y = magicFormula.GetVariables(Y2, arg);
                    if (!StaticFunctions.IsNotValidValue(x) && !StaticFunctions.IsNotValidValue(y))
                    {
                        var point = new DataPoint(x, y);
                        series.Points.Add(point);
                    }

                }
            }
            else if (Axis == EnumAxis.MagicFormula && dataType == DataType.MagicFormula && X2 == MagicFormulaInputVariables.FY)
            {
                var magicFormula = saveData.DicMagicFormula[legendText];
                var arg = saveData.DicMFArgs[legendText].Copy();
                var Xlim = magicFormula.FX.GetPeak(arg);
                var Ylim = magicFormula.FY.GetPeak(arg);
                var random = new RandomBoxMuller();
                int sample = 1600;
                var Xpoints = new List<double>((int)(sample + 2));
                var Ypoints = new List<double>((int)(sample + 2));
                int count = (int)(Math.Sqrt(sample));
                double diffX = (Xlim.Item1 - Xlim.Item2) / count;
                double diffY = (Ylim.Item1 - Ylim.Item2)/count;
                for (int i = 0; i < count; ++i)
                {
                    Xpoints.Add(Xlim.Item2 + diffX*i);
                    
                    Ypoints.Add(Ylim.Item2 + diffY * i);
                }
                List<XY2> points = new List<XY2>(Xpoints.Count * Ypoints.Count);
                for (int i = 0; i < Xpoints.Count; ++i)
                {
                    for (int j = 0; j < Ypoints.Count; ++j)
                    {
                        arg.SA = Ypoints[i];
                        arg.SR = Xpoints[j];
                        var output = magicFormula.CombinedFunction(arg);
                        points.Add(new XY2(output.FY, output.FX));
                    }
                }
                points.Sort(
                delegate(XY2 ls, XY2 rh)
                {
                    if (ls.Item1 == rh.Item1) return 0;
                    return ls.Item1 - rh.Item1 > 0 ? 1 : -1;
                }
                );
                //upperchan作成
                List<XY2> chan = new List<XY2>(points.Count);
                chan.Add(points[0]);
                chan.Add(points[1]);
                for(int i = 2; i<points.Count; ++i)
                {
                    while (chan.Count >= 2 && isLeft(chan[chan.Count - 2], chan[chan.Count - 1], points[i]))
                        chan.RemoveAt(chan.Count - 1);


                    chan.Add(points[i]);
                }
                //lowerchan作成
                chan.Add(points[points.Count-2]);
                int n = chan.Count;

                for (int i = points.Count - 3; i >= 0; --i)
                {
                    while (chan.Count >= n && isLeft(chan[chan.Count - 2], chan[chan.Count - 1], points[i]))
                        chan.RemoveAt(chan.Count - 1);


                    chan.Add(points[i]);
                }
                for(int i = 0; i< chan.Count; ++i)
                {
                    if (!StaticFunctions.IsNotValidValue(chan[i].Item1) && !StaticFunctions.IsNotValidValue(chan[i].Item2))
                    {
                        series.Points.Add(new DataPoint(chan[i].Item1, chan[i].Item2));
                    }
                    
                }
                
            }
            else if(dataType == DataType.NonManagedData)
            {
                var dataList = saveData.DicNotManagedData[legendText];
                int points = saveData.numPoints < 0 ? dataList.Count : saveData.numPoints;
                for (int i = 0; i < Math.Min(dataList.Count, points); ++i)
                {
                    if (!StaticFunctions.IsNotValidValue(dataList[i].x.getValue(X2)) && !StaticFunctions.IsNotValidValue(dataList[i].y))
                    {
                        var point = new DataPoint(dataList[i].x.getValue(X2), dataList[i].y);
                        series.Points.Add(point);
                    }
                }
            }
            if (saveData.GridLineOffsetX)
            {
                double diff = Math.Floor(chart.ChartAreas[0].AxisX.Minimum / chart.ChartAreas[0].AxisX.Interval);
                double offset = (diff + 1)* chart.ChartAreas[0].AxisX.Interval - chart.ChartAreas[0].AxisX.Minimum;

                chart.ChartAreas[0].AxisX.IntervalOffset = offset;
            }
            else
            {
                chart.ChartAreas[0].AxisX.IntervalOffset = 0;
            }
            if (saveData.GridLineOffsetY)
            {
                chart.ChartAreas[0].AxisY.IntervalOffset = -chart.ChartAreas[0].AxisY.Minimum;
            }
            else
            {
                chart.ChartAreas[0].AxisY.IntervalOffset = 0;
            }
        }
        private bool isLeft(XY2 lhs, XY2 rhs, XY2 point)
        {
            XY2 vec = new Tuple<double, double>(-rhs.Item2 + lhs.Item2, rhs.Item1 - lhs.Item1);
            XY2 vec2 = new Tuple<double, double>(point.Item1 - lhs.Item1, point.Item2 - lhs.Item2);
            return vec.Item1 * vec2.Item1 + vec.Item2 * vec2.Item2 >= 0;
        }
        public void DrawAllGraph()
        {
            foreach(var kv in saveData.DicSeries)
            {
                DrawGraph(kv.Key);
            }
            
        }
        public void Clear(string legendText)
        {
            var series = saveData.DicSeries[legendText];
            series.Points.Clear();
        }

        public class AxisStyle
        {
            public bool offset;
            public string format;
            public TickMarkStyle tms;
            public ChartDashStyle ds;
        }

        public void SetXAxisStyle(AxisStyle s)
        {
            saveData.GridLineOffsetX = s.offset;
            chart.ChartAreas[0].AxisX.LabelStyle.Format = s.format;
            chart.ChartAreas[0].AxisX.MajorTickMark.TickMarkStyle = s.tms;
            chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = s.ds;
        }
        public AxisStyle GetXAxisStyle()
        {
            var ret = new AxisStyle();
            ret.offset = saveData.GridLineOffsetX;
            ret.format = chart.ChartAreas[0].AxisX.LabelStyle.Format;
            ret.tms = chart.ChartAreas[0].AxisX.MajorTickMark.TickMarkStyle;
            ret.ds = chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle;
            return ret;
        }
        public void SetYAxisStyle(AxisStyle s)
        {
            saveData.GridLineOffsetY = s.offset;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = s.format;
            chart.ChartAreas[0].AxisY.MajorTickMark.TickMarkStyle = s.tms;
            chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = s.ds;
        }
        public AxisStyle GetYAxisStyle()
        {
            var ret = new AxisStyle();
            ret.offset = saveData.GridLineOffsetY;
            ret.format = chart.ChartAreas[0].AxisY.LabelStyle.Format;
            ret.tms = chart.ChartAreas[0].AxisY.MajorTickMark.TickMarkStyle;
            ret.ds = chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle;
            return ret;
        }
        private void ClearMenuCheck()
        {
            ToolStripMenuItem[] axises = { xaxisMenu, yaxisMenu };
            foreach (var axis in axises)
            {
                foreach(ToolStripMenuItem item in axis.DropDownItems)
                {
                    item.Checked = false;
                }
            }
        }

        ToolStripMenuItem xaxisMenu;
        ToolStripMenuItem yaxisMenu;
        private void SetMenu()
        {
            
            var menu = new ContextMenuStrip();

            if(saveData.menuEnabled)
            {
                xaxisMenu = new ToolStripMenuItem("X軸(&X)", null, null, Keys.X | Keys.Control);
                yaxisMenu = new ToolStripMenuItem("Y軸(&Y)", null, null, Keys.Y | Keys.Control);


                ToolStripMenuItem[] axises = { xaxisMenu, yaxisMenu };
                if (Axis == EnumAxis.RawTireData)
                {
                    foreach (var axis in axises)
                    {
                        axis.DropDownItems.Clear();
                        foreach (var label in TireData.Label)
                        {
                            axis.DropDownItems.Add(new ToolStripMenuItem(label.Value, null, SelectAxis, null));
                        }
                    }
                    SetAxis(X1, Y1);
                }
                else
                {
                    axises[0].DropDownItems.Clear();
                    foreach (var label in Enum.GetValues(typeof(MagicFormulaInputVariables)))
                    {
                        axises[0].DropDownItems.Add(new ToolStripMenuItem(label.ToString(), null, SelectAxis, null));
                    }


                    var values = Enum.GetValues(typeof(MagicFormulaOutputVariables));
                    axises[1].DropDownItems.Clear();
                    for (int i = 0; i < values.Length; ++i)
                    {
                        axises[1].DropDownItems.Add(new ToolStripMenuItem(values.GetValue(i).ToString(), null, SelectAxis, null));
                    }

                    SetAxis(X2, Y2);
                }
                menu.Items.Add(xaxisMenu);
                menu.Items.Add(yaxisMenu);
                menu.Items.Add(new ToolStripMenuItem("画像としてコピー(&C)", null, delegate (object sender, EventArgs e) { Chart_KeyDown(sender, new KeyEventArgs(Keys.C | Keys.Control)); }, Keys.C | Keys.Control));
                menu.Items.Add(new ToolStripSeparator());
            }

            ContextMenuStrip = menu;
        }

        void SelectAxis(object sender, EventArgs e)
        {
            ToolStripMenuItem[] axises = { xaxisMenu, yaxisMenu };
            int i = 0;
            int j = 0;
            for (; i < 2; ++i)
            {
                for (j=0; j < axises[i].DropDownItems.Count; ++j)
                {
                    if(axises[i].DropDownItems[j] == sender)
                    {
                        goto LOOPEND;
                    }
                }
            }
            LOOPEND:
            if (Axis == EnumAxis.RawTireData)
            {
                if(i == 0)
                {
                    SetAxis((TireDataColumn)j, Y1);
                }
                else
                {
                    SetAxis(X1, (TireDataColumn)j);
                }
            }
            else
            {
                if (i == 0)
                {
                    SetAxis((MagicFormulaInputVariables)j, Y2);
                }
                else
                {
                    SetAxis(X2, (MagicFormulaOutputVariables)j);
                }
            }
            DrawAllGraph();
        }

        public void UpdateViewer()
        {
            this.Update();

            var axis = GetArea().AxisX;
            for (int i = 0; i < 2; ++i)
            {
                if (axis.MajorGrid.IntervalOffset != 0)
                {
                    double diff = Math.Floor(axis.Minimum / axis.MajorGrid.Interval);
                    axis.MajorGrid.IntervalOffset = (diff + 1) * axis.MajorGrid.Interval - axis.Minimum;
                }
                if (axis.MinorGrid.IntervalOffset != 0)
                {
                    double diff = Math.Floor(axis.Minimum / axis.MinorGrid.Interval);
                    axis.MinorGrid.IntervalOffset = (diff + 1) * axis.MinorGrid.Interval - axis.Minimum;
                }
                if (axis.MajorTickMark.IntervalOffset != 0)
                {
                    double diff = Math.Floor(axis.Minimum / axis.MajorTickMark.Interval);
                    axis.MajorTickMark.IntervalOffset = (diff + 1) * axis.MajorTickMark.Interval - axis.Minimum;
                    axis.LabelStyle.IntervalOffset = axis.MajorTickMark.IntervalOffset;
                }
                if (axis.MinorTickMark.IntervalOffset != 0)
                {
                    double diff = Math.Floor(axis.Minimum / axis.MinorTickMark.Interval);
                    axis.MinorTickMark.IntervalOffset = (diff + 1) * axis.MinorTickMark.Interval - axis.Minimum;
                }
                axis = GetArea().AxisY;
            }
        }

        private TireDataColumn ConvertEnum(MagicFormulaInputVariables input)
        {
            switch (input)
            {
                case MagicFormulaInputVariables.SA:
                    return TireDataColumn.SA;
                case MagicFormulaInputVariables.SR:
                    return TireDataColumn.SR;
                case MagicFormulaInputVariables.FZ:
                    return TireDataColumn.FZ;
                case MagicFormulaInputVariables.IA:
                    return TireDataColumn.IA;
                case MagicFormulaInputVariables.P:
                    return TireDataColumn.P;
                case MagicFormulaInputVariables.FY:
                    return TireDataColumn.FY;
                default:
                    return TireDataColumn.TSTC;
            }
        }

        private TireDataColumn ConvertEnum(MagicFormulaOutputVariables output)
        {
            switch (output)
            {
                case MagicFormulaOutputVariables.FX:
                    return TireDataColumn.FX;
                case MagicFormulaOutputVariables.FY:
                    return TireDataColumn.FY;
                case MagicFormulaOutputVariables.MZ:
                    return TireDataColumn.MZ;
                case MagicFormulaOutputVariables.FX_D:
                    return TireDataColumn.FX;
                case MagicFormulaOutputVariables.FY_D:
                    return TireDataColumn.FY;
                case MagicFormulaOutputVariables.FX_BCD:
                    return TireDataColumn.FX;
                case MagicFormulaOutputVariables.FY_BCD:
                    return TireDataColumn.FY;
                default:
                    throw new Exception("軸指定違反");
            }
        }

        private void Chart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                // Ctrl + C
                using (var memStream = new System.IO.MemoryStream())
                {
                    chart.SaveImage(memStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    var bmp = new Bitmap(memStream);
                    Clipboard.SetDataObject(bmp);
                }
            }

        }

        [Serializable]
        public class SaveData
        {
            //[NonSerialized]

            public MemoryStream mem = new MemoryStream();
            public bool GridLineOffsetX = true;
            public bool GridLineOffsetY = true;
            public bool AutoScaleX = true;
            public bool AutoScaleY = true;
            public CMaxMinInterval maxMinInterval = null;
            public int graphSample = 225;
            public int numPoints = 2000;
            public bool menuEnabled = true;
            public EnumAxis axis = EnumAxis.RawTireData;
            public TireDataColumn X1;
            public TireDataColumn Y1;
            public MagicFormulaInputVariables X2;
            public MagicFormulaOutputVariables Y2;
            [NonSerialized]
            public Dictionary<string, Series> DicSeries = new Dictionary<string, Series>();
            [NonSerialized]
            public Dictionary<string, List<TireData>> DicTireData = new Dictionary<string, List<TireData>>();
            public Dictionary<string, string> DicTireDataRef = new Dictionary<string, string>();
            [NonSerialized]
            public Dictionary<string, TireMagicFormula> DicMagicFormula = new Dictionary<string, TireMagicFormula>();
            public Dictionary<string, DataType> DicDataType = new Dictionary<string, DataType>();
            public Dictionary<string, MagicFormulaArguments> DicMFArgs = new Dictionary<string, MagicFormulaArguments>();
            [NonSerialized]
            public Dictionary<string, List<XY>> DicNotManagedData = new Dictionary<string, List<XY>>();
            public Dictionary<string, string> legendTextOverride = new Dictionary<string, string>();
            public Dictionary<string, Table> TableInfo = new Dictionary<string, Table>();
        }

        private void chart_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        public Chart Chart { get { return chart; } }

        private void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var htr = chart.HitTest(e.X, e.Y);
            //FontDialogクラスのインスタンスを作成
            FontDialog fd = new FontDialog();
            //存在しないフォントやスタイルをユーザーが選択すると
            //エラーメッセージを表示する
            fd.FontMustExist = true;
            //横書きフォントだけを表示する
            fd.AllowVerticalFonts = false;
            //色を選択できるようにする
            fd.ShowColor = true;

            if (htr.ChartElementType == ChartElementType.Title)
            {
                //初期のフォントを設定
                fd.Font = chart.Titles[0].Font;
                //初期の色を設定
                fd.Color = chart.Titles[0].ForeColor;
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    //TextBox1のフォントと色を変える
                    chart.Titles[0].Font = fd.Font;
                    chart.Titles[0].ForeColor = fd.Color;
                }
            }
            if (htr.ChartElementType == ChartElementType.LegendItem)
            {
                //初期のフォントを設定
                fd.Font = chart.Legends[0].Font;
                //初期の色を設定
                fd.Color = chart.Legends[0].ForeColor;
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    //TextBox1のフォントと色を変える
                    chart.Legends[0].Font = fd.Font;
                    chart.Legends[0].ForeColor = fd.Color;
                }
            }
            if (htr.ChartElementType == ChartElementType.AxisTitle)
            {
                //初期のフォントを設定
                fd.Font = htr.Axis.TitleFont;
                //初期の色を設定
                fd.Color = htr.Axis.TitleForeColor;
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    //TextBox1のフォントと色を変える
                    htr.Axis.TitleFont = fd.Font;
                    htr.Axis.TitleForeColor = fd.Color;
                }
            }
            if (htr.ChartElementType == ChartElementType.AxisLabels)
            {
                //初期のフォントを設定
                fd.Font = htr.Axis.LabelStyle.Font;
                //初期の色を設定
                fd.Color = htr.Axis.LabelStyle.ForeColor;
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    //TextBox1のフォントと色を変える
                    htr.Axis.LabelStyle.Font = fd.Font;
                    htr.Axis.LabelStyle.ForeColor = fd.Color;
                }
            }

            if (htr.ChartElementType == ChartElementType.Gridlines || htr.ChartElementType == ChartElementType.TickMarks || htr.ChartElementType == ChartElementType.StripLines)
            {
                bool auto = htr.Axis.AxisName == AxisName.X ? saveData.AutoScaleX : saveData.AutoScaleY;
                TireDataViewerGridEditor editor = new TireDataViewerGridEditor(htr.Axis, this);
                editor.ShowDialog();
            }
        }

        private void chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            var htr = chart.HitTest(e.X, e.Y);
            if(e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                var i = e.HitTestResult.PointIndex;
                var dp = e.HitTestResult.Series.Points[i];
                e.Text = String.Format("{0}( {1} = {2} : {3} = {4}", e.HitTestResult.Series.Name, htr.ChartArea.AxisX.Title, dp.XValue, htr.ChartArea.AxisY.Title, dp.YValues[0]);

            }

        }

        private void TireDataViewer_Load(object sender, EventArgs e)
        {
            UpdateViewer();
        }
    }
}
