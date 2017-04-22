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
namespace TireDataAnalyzer.UserControls
{
    public partial class SeriesEditor : UserControl
    {
        public SeriesEditor()
        {
            InitializeComponent();
        }

        bool init = true;
        public SeriesEditor(MultiTireDataViewer viewer, int suf, Control owner = null)
        {

            InitializeComponent();
            ShowCB.Checked = true;
            Owner = owner;
            suffix = suf;
            if (Owner == null)
            {
                SourceCB.Enabled = false;
                ArgumentsButton.Enabled = false;
            }
            Viewer = viewer;
            foreach (Control c in this.Controls)
            {
                c.Click += C_Click;
            }
            InitColorList();
            InitSourceList();
            foreach(Table t in Enum.GetValues(typeof(Table)))
            {
                if (t == Table.None) continue;
                TableCB.Items.Add(t);
            }
            TableCB.SelectedItem = Table.StaticTable;
            Initialized = false;

            PlotTypeCB.Items.Add("Line");
            PlotTypeCB.Items.Add("Points");
            PlotTypeCB.SelectedIndex = Viewer.Axis() == TireDataViewer.EnumAxis.RawTireData?1:0;
            SizeTB.Text = "4";
            Changed = false;
            
            SourceCB.SelectedIndexChanged += SourceCB_SelectedIndexChanged;
            init = false;
        }    


        public ProjectTree.ProjectTreeNode SelectedTreeItem
        {
            get
            {
                return SourceCB.SelectedItem as ProjectTree.ProjectTreeNode;
            }
        }

        public SeriesEditor(MultiTireDataViewer viewer, string id, Control owner = null)
            :this(viewer, 0, owner)
        {
            init = true;
            string[] stArrayData = id.Split('_');

            ID = id;
            suffix = int.Parse(stArrayData[0]);
            InitSourceList();
            Args = Viewer.GetArguments(ID);
            if(Args == null) Args = new MagicFormulaArguments(0, 0, 1000, 0, 75, 50);
            if (ID != null)
            {
                Reload(ID);
            }
            ShowCB.Checked = Viewer.GetEnable(ID);
            if (Viewer.GetTableInfo(ID) != null) TableCB.SelectedItem = Viewer.GetTableInfo(ID);
            init = false;
        }

        public SeriesEditor(SeriesEditor other, int surf)
            : this(other.Viewer, surf, other.Owner)
        {
            init = true;
            string[] stArrayData = other.ID.Split('_');
            
            ID = suffix.ToString("000") + "_" + stArrayData[1];
            InitSourceList();
            Reload(other.ID);
            if (Args == null) Args = new MagicFormulaArguments(0, 0, 1000, 0, 75, 50);
            ShowCB.Checked = Viewer.GetEnable(other.ID);
            init = false;
        }

        private void Reload(string ID)
        {
            PlotTypeCB.SelectedIndex = Viewer.GetChartType(ID) == SeriesChartType.FastLine ? 0 : 1;
            Color c = Viewer.GetColor(ID);
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    ColorCB.SelectedIndex = (int)color - 1;
                }
            }
            Args = Viewer.GetArguments(ID) != null? Viewer.GetArguments(ID).Copy():null;
            SizeTB.Text = Viewer.GetLineWidth(ID).ToString();
            NameTB.Text = Viewer.LegendTextOverride(ID);
            CheckInitialized();
        }

        private void SourceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (NameTB.Text == "") NameTB.Text = SourceCB.SelectedItem.ToString();
            if (SourceCB.SelectedIndex < numDataList)
            {
                ArgumentsButton.Visible = false;
                MFSourceCB.Visible = true;
                PlotTypeCB.SelectedIndex = 1;
                PlotTypeCB.Enabled = false;
                IsMagicFormula = false;
                TableCB.Enabled = true;
            }
            else
            {
                PlotTypeCB.SelectedIndex = 0;
                PlotTypeCB.Enabled = true;
                ArgumentsButton.Visible = true;
                MFSourceCB.Visible = false;
                IsMagicFormula = true;
                TableCB.Enabled = false;
            }
        }

        public bool IsMagicFormula { get; private set; }

        public bool Initialized { get; private set; }
        MultiTireDataViewer Viewer;
        public string ID;
        int suffix = 0;
        public int Suffix {  get { return suffix; } }
        List<ProjectTree.Node_TireDataSet> DataList;
        List<ProjectTree.Node_MagicFormula> MFList;
        Control Owner;
        private void C_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        bool selected = false;
        public bool Changed { get; set; }
        public bool Selected
        {
            get{ return selected; }
            set{
                selected = value;
                if (selected == true)
                    this.BackColor = Color.Blue;
                else
                {
                    this.BackColor = System.Drawing.SystemColors.Control;
                }
            }
        }

        private  void InitColorList()
        {
            ColorCB.Items.Clear();
            ColorCB.Items.AddRange(Enum.GetNames(typeof(KnownColor)));
            ColorCB.DrawItem += ColorCB_DrawItem;
            ColorCB.SelectedIndex = (int)KnownColor.Red-1;
        }

        int numDataList;
        private void InitSourceList()
        {
            SourceCB.Items.Clear();
            DataList = ProjectManager.ProjectNode.GetTireDataSet();
            MFList = ProjectManager.ProjectNode.GetMagicFormula();
            var list = new List<SeriesEditor>();
            bool found = false;
            string uuid = null;
            if (ID != null)
                uuid = ID.Split('_')[1];
            if (Owner != null)
            {
                foreach (Control c in Owner.Controls)
                {
                    var editor = c as SeriesEditor;
                    if (editor != null && editor != this)
                    {
                        list.Add(editor);
                    }
                }
            }
            numDataList = DataList.Count;
            foreach (var data in DataList)
            {
                bool c = false;
                foreach(var s in list)
                {
                    if(s.SourceCB.SelectedItem == data)
                    {
                        numDataList -= 1;
                        c = true;
                        break;
                    }
                }
                if (c) continue;
                
                SourceCB.Items.Add(data);
                if (data.ID.ToString() == uuid)
                {
                    found = true;
                    SourceCB.SelectedIndex = SourceCB.Items.Count - 1;
                }
            }
            foreach (var mf in MFList)
            {
                /*bool c = false;
                foreach (var s in list)
                {
                    if (s.SourceCB.SelectedItem == mf)
                    {
                        c = true;
                        break;
                    }
                }
                if (c) continue;
                */
                SourceCB.Items.Add(mf);
                if (mf.ID.ToString() == uuid)
                {
                    found = true;
                    SourceCB.SelectedIndex = SourceCB.Items.Count - 1;
                }
                    
            }
            if (!found && ID != null)
            {
                Viewer.Remove(ID);
                ID = null;
            }
            MFSourceCB.Items.Clear();
            foreach (var mf in MFList)
            {
                MFSourceCB.Items.Add(mf);
                
                if (mf.ID.ToString() == Viewer.GetDataListRefMF(ID)) MFSourceCB.SelectedIndex = MFSourceCB.Items.Count - 1;
            }
        }

        public TireDataSet SelectedDataSet
        {
            get
            {
                var node= SourceCB.SelectedItem as ProjectTree.Node_MagicFormula;
                return node.MFFD.IDataset.GetDataSet();
            }
        }

        private void ColorCB_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }

            ComboBox cmbColor = (ComboBox)sender;

            string itemString = cmbColor.Items[e.Index].ToString();
            e.DrawBackground();

            e.Graphics.DrawString("■", e.Font, new SolidBrush(Color.FromName(itemString)),
                 new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.Graphics.DrawString("   " + itemString, e.Font, new SolidBrush(e.ForeColor),
                 new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        public void OnDelete()
        {
            if(ID != null)
            Viewer.Remove(ID);
        }

        void CheckInitialized()
        {
            Initialized = SourceCB.SelectedIndex >= 0 && PlotTypeCB.SelectedIndex >= 0 && ColorCB.SelectedIndex >= 0;
            
        }

        public void SetColor(KnownColor c)
        {
            ColorCB.SelectedIndex = ((int)c) - 1;
        }
        
        public MagicFormulaArguments Args = null;
        public void BeforeReplot()
        {
            CheckInitialized();
            Table t = (TableCB.SelectedItem as Table?).Value;
            if (Initialized)
            {
                
                if (SourceCB.SelectedIndex < numDataList)
                {
                    var node = SourceCB.SelectedItem as ProjectTree.Node_TireDataSet;
                    if (node != null && ID != null && ID != suffix.ToString("000") + "_" + node.ID.ToString())
                    {
                        OnDelete();
                    }
                    ID = suffix.ToString("000") + "_" + node.ID.ToString();
                    Viewer.SetDataList(node.IDataSet.GetDataSet().GetDataList(t), t, ID);
                    if (MFSourceCB.SelectedIndex >= 0)
                    {
                        
                        if (Args == null)
                        {
                            var mfnode = MFSourceCB.SelectedItem as ProjectTree.Node_MagicFormula;
                            var data = mfnode.MFFD.IDataset.GetDataSet().MaxminSet.Limit(t).Mean;
                            Args = new MagicFormulaArguments(data);
                        }
                        var nodeMF = MFSourceCB.SelectedItem as ProjectTree.Node_MagicFormula;
                        Viewer.SetDataListRefMF(ID, nodeMF.ID.ToString());

                        Viewer.SetMagicFormula(nodeMF.MFFD.MagicFormula, Args, nodeMF.ID.ToString());
                    }
                    else
                    {
                        Viewer.SetDataListRefMF(ID, null);
                    }
                }
                else
                {
                    var node = SourceCB.SelectedItem as ProjectTree.Node_MagicFormula;
                    if (node != null && ID != null && ID !=  suffix.ToString("000") + "_" + node.ID.ToString())
                    {
                        OnDelete();
                        DialogResult result = MessageBox.Show("引数をリセットしますか?", "確認",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)
                        {
                            var data = node.MFFD.IDataset.GetDataSet().MaxminSet.Limit(t).Mean;
                            Args = new MagicFormulaArguments(data);
                        }
                    }
                    if (Args == null)
                    {
                        var data = node.MFFD.IDataset.GetDataSet().MaxminSet.Limit(t).Mean;
                        Args = new MagicFormulaArguments(data);
                    }
                    ID = suffix.ToString("000") + "_" + node.ID.ToString();
                    Viewer.SetMagicFormula(node.MFFD.MagicFormula, Args, ID);

                }
                KnownColor color = (KnownColor)(ColorCB.SelectedIndex + 1);
                var ct = PlotTypeCB.SelectedIndex == 0 ? SeriesChartType.FastLine : SeriesChartType.FastPoint;
                Viewer.SetChartType(ct, ID);

                Viewer.SetColor(Color.FromKnownColor(color), ID);
                Viewer.SetLineWidth(int.Parse(SizeTB.Text), ID);
                Viewer.LegendTextOverride(ID, NameTB.Text);
                Viewer.SetEnable(ShowCB.Checked, ID);
            }
        }

        public void Replot()
        {
            BeforeReplot();
            if (Initialized)
            {
                Viewer.DrawGraph(ID);
                Changed = false;
            }
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
            Changed = true;
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

        private void ValueChanged(object sender, EventArgs e)
        {
            

            if (init) return;
            if(Initialized ) Viewer.SetEnable(ShowCB.Checked, ID);
            Changed = true;
        }

        

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValueChanged(sender, e);
            }
        }

        private void SourceCB_Click(object sender, EventArgs e)
        {
            InitSourceList();
        }

        private void OtherButton_Click(object sender, EventArgs e)
        {
            BeforeReplot();
            MFGraphDialog dialog = new MFGraphDialog(Viewer, ID);
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                
                Reload(ID);
            }
        }

        private void MFSourceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void TableCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Changed = true;
        }
    }
}
