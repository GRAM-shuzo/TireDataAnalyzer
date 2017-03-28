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
    public partial class RawTireDataProperty : PropertyPage
    {


        public RawTireDataProperty(ProjectTree.Node_RawTireData impl_)
            :base(impl_)
        {
            InitializeComponent();
            impl = impl_;
            RTDM = impl.RTDM;
            impl.OnRename += OnRename;


            TempRTDM = new RawTireDataManager();
            GraphTabControl.SelectedIndex = 0;
        }

        private void RawTireDataProperty_Load(object sender, EventArgs e)
        {
            OnRename(impl.Name);
            InitializeGraph();
            NumPoint.SelectedIndex = 4;          
        }

        private void InitializeGraph()
        {

            CorneringDataViewer.SetAxis(TireDataColumn.SA, TireDataColumn.FY);
            DriveBrakeDataViewer.SetAxis(TireDataColumn.SL, TireDataColumn.FX);
            TransientDataViewer.SetAxis(TireDataColumn.ET, TireDataColumn.SA);

            string[] legends = { "追加済みデータ", "新規データ" };
            foreach (Table table in Enum.GetValues(typeof(Table)))
            {
                if(table != Table.None && table != Table.StaticTable)
                {
                    GetViewer(table).SetColor(Color.ForestGreen, legends[0]);
                    GetViewer(table).SetChartType(SeriesChartType.FastPoint,legends[0]);
                    GetViewer(table).SetColor(Color.Red, legends[1]);
                    GetViewer(table).SetChartType(SeriesChartType.FastPoint, legends[1]);
                }
                
            }

        }

        void DrawGraph(Table table, bool newDataOnly)
        {
            GetTBDataAlreadyAdded(table).Text = RTDM.Count(table).ToString();
            GetTBNewData(table).Text = TempRTDM.Count(table).ToString();
            var dataLists = new List<TireData>[2];
            dataLists[0] = RTDM.GetDataList(table);
            dataLists[1] = TempRTDM.GetDataList(table);
            string[] legends = { "追加済みデータ", "新規データ" };
            GetViewer(table).NumPoints = PointToDraw(NumPoint.SelectedIndex);
            for (int i = newDataOnly ? 1 : 0; i <2; ++i)
            {
                
                
                GetViewer(table).SetDataList(dataLists[i], legends[i]);
                
                GetViewer(table).DrawGraph(legends[i]);
            }
        }

        private int PointToDraw(int i)
        {
            switch (i)
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

        private Label GetTBDataAlreadyAdded(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return CorneringTableDataCount;
                case Table.DriveBrakeTable:
                    return DriveBrakeTableDataCount;
                case Table.TransientTable:
                    return TransientTableDataCount;
            }
            return null;
        }

        private Label GetTBNewData(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return CorneringTableNewDataCount;
                case Table.DriveBrakeTable:
                    return DriveBrakeTableNewDataCount;
                case Table.TransientTable:
                    return TransientTableNewDataCount;
            }
            return null;
        }

        private TireDataViewer GetViewer(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return CorneringDataViewer;
                case Table.DriveBrakeTable:
                    return DriveBrakeDataViewer;
                case Table.TransientTable:
                    return TransientDataViewer;
            }
            return null;
        }

        ProjectTree.Node_RawTireData impl;
        RawTireDataManager RTDM;
        RawTireDataManager TempRTDM;
        public delegate void RenameDelegate(string name);
        public RenameDelegate Rename;

        void OnRename(string name)
        {
            this.TireNameTextBox.Text = name;
            SetTabText(name);
        }

        public override bool OnOKClick()
        {
            
            OnApplyClick();
            impl.OnRename -= OnRename;
            return true;
        }

        public override bool OnCancelClick()
        {
            impl.OnRename -= OnRename;
            return true;
        }

        public override void OnApplyClick()
        {
            if(Rename != null)
            {
                Rename(TireNameTextBox.Text);
            }
            RTDM.InsertData(TempRTDM);
            TempRTDM = new RawTireDataManager();
            ShowSelectedGraph();
            this.State = PropertyPageState.NotChanged;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ofd.Filter = "datファイル(*.dat)|*.dat|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "ファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                GetTB(sender as Button).Text = ofd.FileName;
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                DatFileReader reader = new DatFileReader(GetTB(sender as Button).Text);
                InsertData(reader, sender as Button);
                GetTB(sender as Button).Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show("ファイルを読み込めません");
                Log.Output(ex.Message);
            }
        }

        TextBox GetTB(Button b)
        {
            if (b == CorneringTableBrowseButton || b == CorneringTableAddButton) return CorneringTableTextBox;
            if (b == DriveBrakeTableBrowseButton || b == DriveBrakeTableAddButton) return DriveBrakeTableTextBox;
            if (b == TransientTableBrowseButton || b == TransientTableAddButton) return TransientTableTextBox;
            return null;
        }
        void InsertData(DatFileReader reader, Button b)
        {
            int i = GraphTabControl.SelectedIndex;
            if (b == CorneringTableAddButton)
            {
                TempRTDM.InsertData(reader.DataList, Table.CorneringTable);
                GraphTabControl.SelectedIndex = 0;
            }
            if ( b == DriveBrakeTableAddButton)
            {
                TempRTDM.InsertData(reader.DataList, Table.DriveBrakeTable);
                GraphTabControl.SelectedIndex = 1;
            }
            if ( b == TransientTableAddButton)
            {
                TempRTDM.InsertData(reader.DataList, Table.TransientTable);
                GraphTabControl.SelectedIndex = 2;
            }
            if(i == GraphTabControl.SelectedIndex)
                ShowSelectedGraph(true);
            this.State = PropertyPageState.Changed;
        }

        private void GraphTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedGraph();
        }

        void ShowSelectedGraph(bool newDataOnly = false)
        {
            Table table;
            switch (GraphTabControl.SelectedIndex)
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
            DrawGraph(table, newDataOnly);
        }

        private void NumPoint_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowSelectedGraph();
        }

        private RawTireDataProperty()
            : base(null)
        {
            InitializeComponent();
            throw new NotImplementedException();
        }
    }
}
