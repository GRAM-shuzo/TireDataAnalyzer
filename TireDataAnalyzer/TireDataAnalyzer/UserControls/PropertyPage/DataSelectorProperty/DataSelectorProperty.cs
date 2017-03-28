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

namespace TireDataAnalyzer.UserControls.PropertyPage
{
    public partial class DataSelectorProperty : PropertyPage
    {
        public DataSelectorProperty(ProjectTree.Node_DataSelector selector)
            :base(selector)
        {
            InitializeComponent();
            DataSelector = selector;
            tempSelector = selector.TDSS.Copy();
            DataSelector.OnUpdated += OnUpdated;

        }
        private void OnUpdated()
        {
            tempSelector.Reset();
        }

        ProjectTree.Node_DataSelector DataSelector;
        TireDataSetSelector tempSelector;
        bool loaded = false;
        private void DataSelectorProperty_Load(object sender, EventArgs e)
        {
            NumPoint.SelectedIndex = 4;
            OnRename(DataSelector.Name);
            InitializeGraph();
            CorneringDataSelector.Initialize(tempSelector, Table.CorneringTable, this);
            DriveBrakeDataSelector.Initialize(tempSelector, Table.DriveBrakeTable, this);
            TransientDataSelector.Initialize(tempSelector, Table.TransientTable, this);

           

            DataSelector.OnRename += OnRename;
            loaded = true;
            ReplotSelectedGraph();    
        }

        void InitializeGraph()
        {
            MultiTireDataViewer[] Viewers = { CorneringDataSelector.Viewer, DriveBrakeDataSelector.Viewer, TransientDataSelector.Viewer };
            CorneringDataSelector.Viewer.ResetScreen(MultiTireDataViewer.EnumScreenCount.Four);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.SA, TireDataColumn.FY, 0);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.IA, TireDataColumn.FY, 1);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.FZ, TireDataColumn.FY, 2);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.P, TireDataColumn.FY, 3);

            foreach ( var viewer in Viewers)
            {
                viewer.SetColor(Color.Green, TireDataSelectorWithViewer.LegendTexts[0]);
                viewer.SetColor(Color.Red, TireDataSelectorWithViewer.LegendTexts[1]);
                viewer.SetChartType(SeriesChartType.FastPoint, TireDataSelectorWithViewer.LegendTexts[0]);
                viewer.SetChartType(SeriesChartType.FastPoint, TireDataSelectorWithViewer.LegendTexts[1]);
            }
        }

        void OnRename(string name)
        {
            this.NameTextBox.Text = name;
            SetTabText(name);
        }

        public int PointToDraw()
        {
            switch (NumPoint.SelectedIndex)
            {
                case 0:
                    return -1;
                case 1:
                    return 100000;
                case 2:
                    return 50000;
                case 3:
                    return 10000;
                case 4:
                    return 5000;
                case 5:
                    return 1000;
            }
            return 0;
        }

        public void Changed()
        {
            State = PropertyPageState.Changed;
        }
        
        public override bool OnOKClick()
        {
            DataSelector.OnUpdated -= OnUpdated;
            DataSelector.OnRename -= OnRename;
            OnApplyClick();
            return true;
        }

        public override bool OnCancelClick()
        {
            DataSelector.OnUpdated -= OnUpdated;
            DataSelector.OnRename -= OnRename;
            return true;

        }

        public override void OnApplyClick()
        {
            if (DataSelector.Name != NameTextBox.Text) DataSelector.Name = NameTextBox.Text;
            DataSelector.TDSS.CopyFrom(tempSelector);
            this.State = PropertyPageState.NotChanged;
        }

        void ReplotSelectedGraph()
        {
                switch (MainTabControl.SelectedIndex)
                {
                    case 0:
                        CorneringDataSelector.ShowGraph(tempSelector);
                        break;
                    case 1:
                        DriveBrakeDataSelector.ShowGraph(tempSelector);
                        break;
                    default:
                        TransientDataSelector.ShowGraph(tempSelector);
                        break;
                }                
        }

        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(loaded)
                ReplotSelectedGraph();
        }

        private void NumPoint_SelectedValueChanged(object sender, EventArgs e)
        {
            if (loaded)
                ReplotSelectedGraph();
        }

        private DataSelectorProperty()
            :base(null)
        {
            InitializeComponent();
            throw new NotImplementedException();
        }
    }
}
