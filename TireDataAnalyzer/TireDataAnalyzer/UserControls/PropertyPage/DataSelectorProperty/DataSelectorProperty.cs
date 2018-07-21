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

            
            
            MinTB.Validating += IsReal_Validating;
            MinTB.Validated += IsReal_Validated;
            MinTB.KeyDown += TextBox_KeyDown;
            MaxTB.Validating += IsReal_Validating;
            MaxTB.Validated += IsReal_Validated;
            MaxTB.KeyDown += TextBox_KeyDown;
            foreach (var label in TireData.Label)
            {
                TableColumnCB.Items.Add(label.Value);
            }
            TableColumnCB.SelectedIndex = 0;
            GradationCB.SelectedIndexChanged += GradationCB_SelectedIndexChanged;
            TableColumnCB.SelectedIndexChanged += TableColumnCB_SelectedIndexChanged;
            GradationCB.Items.Add(new TireDataViewer.GradationNone());
            GradationCB.Items.Add(new TireDataViewer.GradationJet());
            GradationCB.SelectedIndex = 0;



            DataSelector.OnRename += OnRename;
            loaded = true;
            ReplotSelectedGraph();    
        }

        private void TableColumnCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table table = Table.None;
            switch (MainTabControl.SelectedIndex)
            {
                case 0:
                    table = Table.CorneringTable;
                    break;
                case 1:
                    table = Table.DriveBrakeTable;
                    break;
                default:
                    table = Table.TransientTable;
                    break;
            }
            var maxmin = tempSelector.Maxmin(table);
            MinTB.Text = maxmin.MinValue((TireDataColumn)(TableColumnCB.SelectedIndex)).ToString();
            MaxTB.Text = maxmin.MaxValue((TireDataColumn)(TableColumnCB.SelectedIndex)).ToString();
            ReplotSelectedGraph();
        }

        private void GradationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(GradationCB.SelectedIndex == 0)
            {
                TableColumnCB.Enabled = false;
                MinTB.Enabled = false;
                MaxTB.Enabled = false;
                
            }
            else
            {
                if (!TableColumnCB.Enabled)
                {
                    TableColumnCB.SelectedIndex = (int)TireDataColumn.FZ;
                }
                TableColumnCB.Enabled = true;
                MinTB.Enabled = true;
                MaxTB.Enabled = true;

                
            }
            TableColumnCB_SelectedIndexChanged(sender, e);

            ReplotSelectedGraph();
        }

        bool hasError = false;
        private void IsReal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);

            string s = tb.Text;
            if (!StaticFunctions.IsNumeric(s))
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
            ReplotSelectedGraph();
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

        void InitializeGraph()
        {
            MultiTireDataViewer[] Viewers = { CorneringDataSelector.Viewer, DriveBrakeDataSelector.Viewer, TransientDataSelector.Viewer };
            CorneringDataSelector.Viewer.ResetScreen(MultiTireDataViewer.EnumScreenCount.Four);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.SA, TireDataColumn.FY, 0);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.IA, TireDataColumn.FY, 1);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.FZ, TireDataColumn.FY, 2);
            CorneringDataSelector.Viewer.SetAxis(TireDataColumn.P, TireDataColumn.FY, 3);
            DriveBrakeDataSelector.Viewer.ResetScreen(MultiTireDataViewer.EnumScreenCount.Four);
            DriveBrakeDataSelector.Viewer.SetAxis(TireDataColumn.SA, TireDataColumn.FX, 0);
            DriveBrakeDataSelector.Viewer.SetAxis(TireDataColumn.IA, TireDataColumn.FX, 1);
            DriveBrakeDataSelector.Viewer.SetAxis(TireDataColumn.FZ, TireDataColumn.FX, 2);
            DriveBrakeDataSelector.Viewer.SetAxis(TireDataColumn.P, TireDataColumn.FX, 3);
            TransientDataSelector.Viewer.ResetScreen(MultiTireDataViewer.EnumScreenCount.Four);
            TransientDataSelector.Viewer.SetAxis(TireDataColumn.ET, TireDataColumn.V, 0);
            TransientDataSelector.Viewer.SetAxis(TireDataColumn.ET, TireDataColumn.FY, 1);
            TransientDataSelector.Viewer.SetAxis(TireDataColumn.IA, TireDataColumn.FY, 2);
            TransientDataSelector.Viewer.SetAxis(TireDataColumn.P, TireDataColumn.FY, 3);

            var maxmin = tempSelector.Maxmin(Table.TransientTable);
            foreach ( var viewer in Viewers)
            {
                viewer.SetColor(Color.Green, TireDataSelectorWithViewer.LegendTexts[0]);
                //viewer.SetGradation(TireDataColumn.FZ, maxmin.MinValue(TireDataColumn.FZ), maxmin.MaxValue(TireDataColumn.FZ), TireDataSelectorWithViewer.LegendTexts[0], new TireDataViewer.GradationJet());
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
            MultiTireDataViewer[] Viewers = { CorneringDataSelector.Viewer, DriveBrakeDataSelector.Viewer, TransientDataSelector.Viewer };
            double min = 0;
            double max = 0;
            double.TryParse(MinTB.Text, out min);
            double.TryParse(MaxTB.Text, out max);
            foreach (var viewer in Viewers)
            {
                viewer.SetGradation((TireDataColumn)(TableColumnCB.SelectedIndex), min, max, TireDataSelectorWithViewer.LegendTexts[0], GradationCB.SelectedItem as TireDataViewer.GradationCalcurator);
            }
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
