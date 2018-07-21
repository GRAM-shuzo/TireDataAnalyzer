using System;
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace TireDataAnalyzer.UserControls
{
    [Serializable]
    public partial class MultiTireDataViewer : UserControl
    {
        Guid uuid = Guid.NewGuid();

        public Guid ID { get { return uuid; } }
        public string GraphName;
        private EnumScreenCount screenCount;
        public MultiTireDataViewer()
        {
            InitializeComponent();
            for (int i = 0; i < 9; ++i)
            {
                Viewers.Add(new TireDataViewer());
                Viewers[i].Dock = DockStyle.Fill;
                var viewer = Viewers[i];
                viewer.Chart.MouseDoubleClick += delegate (object sender, MouseEventArgs e)
                {
                    var htr = viewer.Chart.HitTest(e.X, e.Y);
                    if (htr.ChartElementType == ChartElementType.DataPoint || htr.ChartElementType == ChartElementType.PlottingArea)
                    {

                            Property(viewer,new EventArgs());
                    }
                };
            }
            AddContextMenuChildren();
            
            ResetScreen(EnumScreenCount.Four);
            this.Load += MultiTireDataViewer_Load;
        }
        [field: NonSerialized]
        public EventHandler SeriesChanged;
        public void AddContextMenuChildren()
        {
            ToolStripMenuItem[,] TSMIs = new ToolStripMenuItem[9, 6];
            for (int i = 0; i < 9; ++i)
            {
                
               
                TSMIs[i, 0] = new ToolStripMenuItem("1画面", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.One); }, null);
                TSMIs[i, 1] = new ToolStripMenuItem("2画面(縦)", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.Two_Ver); }, null);
                TSMIs[i, 2] = new ToolStripMenuItem("2画面(横)", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.Two_Hor); }, null);
                TSMIs[i, 3] = new ToolStripMenuItem("4画面(", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.Four); }, null);
                TSMIs[i, 4] = new ToolStripMenuItem("6画面", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.Six); ; }, null);
                TSMIs[i, 5] = new ToolStripMenuItem("9画面", null, delegate (object sender, EventArgs e) { ResetScreen(EnumScreenCount.Nine); }, null);
                Viewers[i].ContextMenuStrip.Items.Add(new ToolStripMenuItem("グラフの書式設定(&F)", null, Property, Keys.F | Keys.Control));
                Viewers[i].ContextMenuStrip.Items.Add(new ToolStripSeparator());
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 0]);
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 1]);
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 2]);
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 3]);
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 4]);
                Viewers[i].ContextMenuStrip.Items.Add(TSMIs[i, 5]);
            }
            OnResetScreen += delegate (EnumScreenCount sc)
            {
                for (int i = 0; i < 9; ++i)
                {
                    for (int j = 0; j < 6; ++j)
                    {
                        if (j != (int)sc)
                            TSMIs[i, j].Checked = false;
                        else
                            TSMIs[i, j].Checked = true;
                    }
                }
            };
        }

        public bool AutoScaleX
        {
            get
            {
                return Viewers[ViewerNumber].AutoScaleX;
            }
            set
            {
                Viewers[ViewerNumber].AutoScaleX = value;
            }
        }
        public bool AutoScaleY
        {
            get
            {
                return Viewers[ViewerNumber].AutoScaleY;
            }
            set
            {
                Viewers[ViewerNumber].AutoScaleY = value;
            }
        }

        public enum EnumScreenCount
        {
            One = 0,
            Two_Ver,
            Two_Hor,
            Four,
            Six,
            Nine
        }
        public TireDataColumn X1 { get { return Viewers[ViewerNumber].X1; } }
        public TireDataColumn Y1
        {
            get { return Viewers[ViewerNumber].Y1; }
        }
        public MagicFormulaInputVariables X2
        {
            get { return Viewers[ViewerNumber].X2; }
        }
        public MagicFormulaOutputVariables Y2
        {
            get { return Viewers[ViewerNumber].Y2; }
        }

        public TireDataViewer SelectedViewer
        {
            get
            {
                return Viewers[ViewerNumber];
            }
        }

        public TireDataViewer.EnumAxis Axis()
        {
            return Viewers[ViewerNumber].Axis;
        }
        public void SetAxis(TireDataViewer.EnumAxis axis)
        {
            if (axis == Axis()) return;
            foreach (var viewer in Viewers)
            {
                viewer.Axis = axis;
            }
            AddContextMenuChildren();
        }
        public TireDataViewer.EnumAxis Axis(int number)
        {
            return Viewers[number].Axis;
        }
        List<TireDataViewer> Viewers = new List<TireDataViewer>(9);
        public EnumScreenCount ScreenCount { get { return screenCount; } private set { screenCount = value; } }
        public bool PropertyEnable { get; set; }
        public bool ScreenCountEnable { get; set; }
        public MultiTireDataViewer(EnumScreenCount Num, bool propertyEnable, bool screenCountEnable)
            : this()
        {
            PropertyEnable = propertyEnable;
            ScreenCountEnable = screenCountEnable;
            ResetScreen(Num);

        }
        public void SetDrawPriority(int diff, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetDrawPriority(diff, legendText);
            }
            changed = true;
        }

        delegate void OnResetScreenDelegate(EnumScreenCount Num);
        OnResetScreenDelegate OnResetScreen;

        private void MultiTireDataViewer_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Property(new COnload());
            for (int i = 0; i < 9; ++i)
            {
                int k = 6;
                if (!ScreenCountEnable)
                {
                    for (int j = 0; j < 6; ++j)
                    {
                        Viewers[i].ContextMenuStrip.Items.RemoveAt(Viewers[i].ContextMenuStrip.Items.Count - 1);
                    }
                    k = 0;
                }

                if (!PropertyEnable)
                    (Viewers[i].ContextMenuStrip.Items[Viewers[i].ContextMenuStrip.Items.Count - 2-k] as ToolStripMenuItem).Enabled = false;
            }
        }

        public void ResetScreen(EnumScreenCount Num)
        {
            if (Num == ScreenCount) return;
            foreach (var viewer in Viewers)
            {
                viewer.Parent = null;
            }
            this.Controls.Clear();
            if (Num == EnumScreenCount.One)
            {
                this.Controls.Add(Viewers[0]);
            }
            else if (Num == EnumScreenCount.Two_Hor)
            {
                var spritter1 = new SplitContainer();

                spritter1.Dock = DockStyle.Fill;
                spritter1.Panel1.Controls.Add(Viewers[0]);
                spritter1.Panel2.Controls.Add(Viewers[1]);
                this.Controls.Add(spritter1);
                spritter1.SplitterDistance = spritter1.Size.Width / 2;
            }
            else if (Num == EnumScreenCount.Two_Ver)
            {
                var spritter1 = new SplitContainer();
                spritter1.Dock = DockStyle.Fill;

                spritter1.Dock = DockStyle.Fill;
                spritter1.Orientation = Orientation.Horizontal;
                spritter1.Panel1.Controls.Add(Viewers[0]);
                spritter1.Panel2.Controls.Add(Viewers[1]);
                this.Controls.Add(spritter1);
                spritter1.SplitterDistance = spritter1.Size.Height / 2;
            }
            else if (Num == EnumScreenCount.Four)
            {
                var spritter1 = new SplitContainer();
                var spritter2 = new SplitContainer();
                var spritter3 = new SplitContainer();
                spritter1.Dock = DockStyle.Fill;
                spritter2.Dock = DockStyle.Fill;
                spritter3.Dock = DockStyle.Fill;
                spritter2.Orientation = Orientation.Horizontal;
                spritter3.Orientation = Orientation.Horizontal;




                spritter1.Panel1.Controls.Add(spritter2);
                spritter1.Panel2.Controls.Add(spritter3);


                spritter2.Panel1.Controls.Add(Viewers[0]);
                spritter2.Panel2.Controls.Add(Viewers[1]);
                spritter3.Panel1.Controls.Add(Viewers[2]);
                spritter3.Panel2.Controls.Add(Viewers[3]);

                this.Controls.Add(spritter1);
                spritter1.SplitterDistance = spritter1.Size.Width / 2;
                spritter2.SplitterDistance = spritter1.Size.Height / 2;
                spritter3.SplitterDistance = spritter1.Size.Height / 2;
            }
            else if (Num == EnumScreenCount.Six)
            {
                var spritter1 = new SplitContainer();
                var spritter2 = new SplitContainer();
                var spritter3 = new SplitContainer();
                var spritter4 = new SplitContainer();
                var spritter5 = new SplitContainer();
                spritter1.Dock = DockStyle.Fill;
                spritter2.Dock = DockStyle.Fill;
                spritter3.Dock = DockStyle.Fill;
                spritter4.Dock = DockStyle.Fill;
                spritter5.Dock = DockStyle.Fill;

                spritter3.Orientation = Orientation.Horizontal;
                spritter4.Orientation = Orientation.Horizontal;
                spritter5.Orientation = Orientation.Horizontal;

                spritter1.Panel2.Controls.Add(spritter2);

                spritter1.Panel1.Controls.Add(spritter3);
                spritter2.Panel1.Controls.Add(spritter4);
                spritter2.Panel2.Controls.Add(spritter5);

                spritter3.Panel1.Controls.Add(Viewers[0]);
                spritter3.Panel2.Controls.Add(Viewers[1]);
                spritter4.Panel1.Controls.Add(Viewers[2]);
                spritter4.Panel2.Controls.Add(Viewers[3]);
                spritter5.Panel1.Controls.Add(Viewers[4]);
                spritter5.Panel2.Controls.Add(Viewers[5]);

                this.Controls.Add(spritter1);
                spritter1.SplitterDistance = spritter1.Size.Width / 3;
                spritter2.SplitterDistance = spritter1.Size.Width / 3;

                spritter3.SplitterDistance = spritter1.Size.Height / 2;
                spritter4.SplitterDistance = spritter1.Size.Height / 2;
                spritter5.SplitterDistance = spritter1.Size.Height / 2;
            }
            else if (Num == EnumScreenCount.Nine)
            {
                var spritter1 = new SplitContainer();
                var spritter2 = new SplitContainer();
                var spritter3 = new SplitContainer();
                var spritter4 = new SplitContainer();
                var spritter5 = new SplitContainer();
                var spritter6 = new SplitContainer();
                var spritter7 = new SplitContainer();
                var spritter8 = new SplitContainer();
                spritter1.Dock = DockStyle.Fill;
                spritter2.Dock = DockStyle.Fill;
                spritter3.Dock = DockStyle.Fill;
                spritter4.Dock = DockStyle.Fill;
                spritter5.Dock = DockStyle.Fill;
                spritter6.Dock = DockStyle.Fill;
                spritter7.Dock = DockStyle.Fill;
                spritter8.Dock = DockStyle.Fill;


                spritter3.Orientation = Orientation.Horizontal;
                spritter4.Orientation = Orientation.Horizontal;
                spritter5.Orientation = Orientation.Horizontal;
                spritter6.Orientation = Orientation.Horizontal;
                spritter7.Orientation = Orientation.Horizontal;
                spritter8.Orientation = Orientation.Horizontal;

                spritter1.Panel2.Controls.Add(spritter2);

                spritter1.Panel1.Controls.Add(spritter3);
                spritter2.Panel1.Controls.Add(spritter4);
                spritter2.Panel2.Controls.Add(spritter5);

                spritter3.Panel2.Controls.Add(spritter6);
                spritter4.Panel2.Controls.Add(spritter7);
                spritter5.Panel2.Controls.Add(spritter8);


                spritter3.Panel1.Controls.Add(Viewers[0]);
                spritter4.Panel1.Controls.Add(Viewers[1]);
                spritter5.Panel1.Controls.Add(Viewers[2]);

                spritter6.Panel1.Controls.Add(Viewers[3]);
                spritter6.Panel2.Controls.Add(Viewers[4]);
                spritter7.Panel1.Controls.Add(Viewers[5]);
                spritter7.Panel2.Controls.Add(Viewers[6]);
                spritter8.Panel1.Controls.Add(Viewers[7]);
                spritter8.Panel2.Controls.Add(Viewers[8]);

                this.Controls.Add(spritter1);
                spritter1.SplitterDistance = spritter1.Size.Width / 3;
                spritter2.SplitterDistance = spritter1.Size.Width / 3;

                spritter3.SplitterDistance = spritter1.Size.Height / 3;
                spritter4.SplitterDistance = spritter1.Size.Height / 3;
                spritter5.SplitterDistance = spritter1.Size.Height / 3;
                spritter6.SplitterDistance = spritter1.Size.Height / 3;
                spritter7.SplitterDistance = spritter1.Size.Height / 3;
                spritter8.SplitterDistance = spritter1.Size.Height / 3;
            }
            OnResetScreen(Num);
            ScreenCount = Num;
        }

        public void SetAxis(TireDataColumn x, TireDataColumn y, int number)
        {
            Viewers[number].SetAxis(x, y);
        }
        public void SetAxis(MagicFormulaInputVariables x, MagicFormulaOutputVariables y, int number)
        {
            Viewers[number].SetAxis(x, y);
        }
        public void SetAxis(TireDataColumn x, TireDataColumn y)
        {
            Viewers[ViewerNumber].SetAxis(x, y);
        }
        public void SetAxis(MagicFormulaInputVariables x, MagicFormulaOutputVariables y)
        {
            Viewers[ViewerNumber].SetAxis(x, y);
        }
        public Legend GetLegend()
        {
            return Viewers[ViewerNumber].GetLegend();
        }
        
        public ChartArea GetArea(int number)
        {
            return Viewers[number].GetArea();
        }
        public ChartArea GetArea()
        {
            return GetArea(ViewerNumber);
        }
        public Title GetTitle(int number)
        {
            return Viewers[number].GetTitle();
        }
        public Title GetTitle()
        {
            return GetTitle(ViewerNumber);
        }
        public SeriesChartType GetChartType(string legendText)
        {
            return Viewers[0].GetChartType(legendText);
        }
        public Color GetColor(string legendText)
        {
            return Viewers[0].GetColor(legendText);
        }
        public int GetLineWidth(string legendText)
        {
            return Viewers[0].GetLineWidth(legendText);
        }
        public List<string> GetLegents()
        {
            return Viewers[0].GetLegents();
        }
        public string GetDataListRefMF(string dataListLegend)
        {
            return Viewers[ViewerNumber].GetDataListRefMF(dataListLegend);
        }
        public MagicFormulaArguments GetArguments(string legendText)
        {
            return Viewers[0].GetArguments(legendText);
        }
        public string LegendTextOverride(string legendText)
        {

            return Viewers[0].LegendTextOverride(legendText);
        }
        public TireDataViewer.DataType GetDataType(string legendText)
        {
            return Viewers[0].GetDataType(legendText);
        }

        public void SetLegend(Legend l)
        {
            Viewers[ViewerNumber].SetLegend(l);
            changed = true;
        }
        public void SetDataList(List<TireData> dataList, Table t, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetDataList(dataList, t, legendText);
            }
            changed = true;
        }
        public Table? GetTableInfo(string legendText)
        {
            return Viewers[0].GetTableInfo(legendText);
        }
        public void SetDataListRefMF(string dataListLegend, string magicFormulaLegend)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetDataListRefMF(dataListLegend, magicFormulaLegend);
            }
            changed = true;
        }
        public void SetMagicFormula(TireMagicFormula formula, MagicFormulaArguments constantArgs, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetMagicFormula(formula, constantArgs, legendText);
            }
            changed = true;
        }
        public void SetNonManagedData(List<TireDataViewer.XY> xyList, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetNonManagedData(xyList, legendText);
            }
            changed = true;
        }
        public void LegendTextOverride(string legendText, string overrideText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.LegendTextOverride(legendText, overrideText);
            }
            changed = true;
        }
        public void SetTitle(string text, Docking? dock, Font font, Color? color, int number)
        {
            Viewers[number].SetTitle(text, dock, font, color);
            changed = true;
        }
        public void SetTitle(string text, Docking? dock, Font font, Color? color)
        {
            SetTitle(text, dock, font, color, ViewerNumber);
            changed = true;
        }
        public void SetColor(Color color, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetColor(color, legendText);
            }
            changed = true;
        }

        public void SetChartType(SeriesChartType ct, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetChartType(ct, legendText);
            }
            changed = true;

        }
        public void SetLineWidth(int width, string legendText)
        {
            Viewers[ViewerNumber].SetLineWidth(width, legendText);
            changed = true;
        }

        public void SetXAxisStyle(TireDataViewer.AxisStyle s)
        {
            Viewers[ViewerNumber].SetXAxisStyle(s);
        }
        public TireDataViewer.AxisStyle GetXAxisStyle()
        {
            return Viewers[ViewerNumber].GetXAxisStyle();
        }
        public void SetYAxisStyle(TireDataViewer.AxisStyle s)
        {
            Viewers[ViewerNumber].SetYAxisStyle(s);
        }
        public TireDataViewer.AxisStyle GetYAxisStyle()
        {
            return Viewers[ViewerNumber].GetXAxisStyle();
        }
        public void Remove(string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.Remove(legendText);
            }
            if (SeriesChanged != null)
            {
                SeriesChanged(this, new EventArgs());
                changed = false;
            }
        }
        public void DrawGraph(string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.DrawGraph(legendText);
            }
            if (changed && SeriesChanged != null)
            {
                SeriesChanged(this, new EventArgs());
                changed = false;
            }
        }

        bool firstTimeDraw = true;
        public void DrawAllGraph()
        {
            foreach (var viewer in Viewers)
            {
                viewer.DrawAllGraph();
            }
            if (SeriesChanged != null)
            {
                SeriesChanged(this, new EventArgs());
                changed = false;
            }
            if (firstTimeDraw)
            {
                Property(new COnload());
                firstTimeDraw = false;
            }
            
        }
        public void UpdateViewer()
        {
            foreach (var viewer in Viewers)
            {
                viewer.UpdateViewer();
            }
        }

        public void SetEnable(bool enable, string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetEnable(enable,legendText);
            }
        }
        public bool GetEnable(string legendText)
        {
            return Viewers[ViewerNumber].GetEnable(legendText);
        }
        public void SetGradation(TireDataColumn? column, double min, double max, string legendText, TireDataViewer.GradationCalcurator gradation)
        {
            foreach (var viewer in Viewers)
            {
                viewer.SetGradation(column, min,max, gradation, legendText);
            }
        }
        public Tuple<TireDataColumn?, double, double, TireDataViewer.GradationCalcurator> SetGradation(string legendText)
        {
            return Viewers[0].GetGradation(legendText);
        }

        public int numPoints
        {
            get
            {
                return Viewers[0].NumPoints;
            }
            set
            {
                foreach (var viewer in Viewers)
                {
                    viewer.NumPoints = value;
                }
            }
        }
        public void Clear(string legendText)
        {
            foreach (var viewer in Viewers)
            {
                viewer.Clear(legendText);
            }
        }

        bool changed = false;

        public void Property(EventArgs onload = null)
        {
            Property(this, onload);
        }

        private class COnload
            : EventArgs
        {

        }


        public int ViewerNumber = 0;
        void Property(object sender, EventArgs e)
        {
            var s = sender as ToolStripMenuItem;
            var s2 = sender as TireDataViewer;
            if(s != null)
            {
                var o = s.Owner as ContextMenuStrip;
                var viewer = o.SourceControl;
                for (int i = 0; i < Viewers.Count; ++i)
                {
                    if (viewer == Viewers[i])
                    {
                        ViewerNumber = i;
                    }

                }
                
            }
            else if(s2 != null)
            {
                ViewerNumber = Viewers.IndexOf(s2);
            }
            else
            {
                ViewerNumber = 0;
            }
            var dialog = new TireDataViewerProperty(this);
            if (!(e is COnload) &&  PropertyEnable)
            {
                DialogResult result = dialog.ShowDialog();
            }
            else
            {
                
            }
                
        }

        [Serializable]
        public class SaveData
        {
            public EnumScreenCount screenCount;
            public List<TireDataViewer.SaveData> listSaveData = new List<TireDataViewer.SaveData>(10);
            public Guid uuid;
            public string GraphName;
        }

        static public MultiTireDataViewer LoadViewers(Stream reader)
        {
            var viewer  = new MultiTireDataViewer();
            viewer.Dock = DockStyle.Fill;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as MultiTireDataViewer.SaveData;
            for(int i = 0;i<viewer.Viewers.Count; ++i)
            {
                viewer.Viewers[i].SetSaveData(data.listSaveData[i]);
            }
            viewer.ResetScreen(data.screenCount);
            viewer.uuid = data.uuid;
            viewer.GraphName = data.GraphName;
            viewer.AddContextMenuChildren();
            return viewer;
        }


        public void SaveViewers(Stream writer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = new SaveData();
            data.uuid = uuid;
            data.GraphName = GraphName;
            data.screenCount = ScreenCount;
            for (int i = 0; i < Viewers.Count; ++i)
            {
                data.listSaveData.Add(Viewers[i].GetSaveData());
            }

            binaryFormatter.Serialize(writer, data);
        }
    }
}
