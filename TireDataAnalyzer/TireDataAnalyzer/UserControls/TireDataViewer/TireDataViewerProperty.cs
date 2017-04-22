using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;
using System.Windows.Forms.DataVisualization.Charting;
namespace TireDataAnalyzer.UserControls
{
    public partial class TireDataViewerProperty : Form
    {
        public TireDataViewerProperty()
            :this(null)
        {
        }

        public List<KnownColor> ColorList = new List<KnownColor>
        {
            KnownColor.Red,
            KnownColor.Blue,
            KnownColor.Green,
            KnownColor.Magenta,
            KnownColor.Cyan,
            KnownColor.LightGreen,
            KnownColor.Pink,
            KnownColor.Aqua,
            KnownColor.DarkGreen
        };
        bool init = true;
        bool axisChanged = false;
        public TireDataViewerProperty(MultiTireDataViewer viewer)
        {
            InitializeComponent();
            Viewer = viewer;
            Reload();
            foreach (var s in Viewer.GetLegents())
            {
                try
                {
                    var raw = new SeriesEditor(Viewer, s, DataSourceList);
                    if (raw.Initialized)
                    {
                        raw.Dock = DockStyle.Top;

                        DataSourceList.Controls.Add(raw);
                        raw.Click += SeriesEditor_Click;
                        DataSourceList.Controls.SetChildIndex(raw, 0);
                    }
                }
                catch(Exception e )
                {
                    Log.Output(e.Message);
                }
            }
        }

        private void Reload()
        {
            init = true ;
            MagicFormulaRB.Checked = true;

            AutoScaleX.Checked = Viewer.AutoScaleX;
            AutoAxisStateChanged(AutoScaleX, new EventArgs());
            AutoScaleY.Checked = Viewer.AutoScaleY;
            AutoAxisStateChanged(AutoScaleY, new EventArgs());

            foreach (var name in Enum.GetValues(typeof(StringAlignment)))
            {
                LegendAlignCB.Items.Add(name);
            }
            foreach (var name in Enum.GetValues(typeof(Docking)))
            {
                LegendDockCB.Items.Add(name);
                TitleDockCB.Items.Add(name);
            }
            foreach (var name in Enum.GetValues(typeof(LegendStyle)))
            {
                LegendStyleCB.Items.Add(name);
            }
            var legend = Viewer.GetLegend();
            LegendAlignCB.SelectedItem = legend.Alignment;
            LegendDockCB.SelectedItem = legend.Docking;
            LegendStyleCB.SelectedItem = legend.LegendStyle;
            DockToAreaCB.Checked = legend.IsDockedInsideChartArea;
            TitleDockCB.SelectedItem = Viewer.GetTitle().Docking;
            if (Viewer.Axis() == TireDataViewer.EnumAxis.RawTireData)
            {
                RawDataRB.Checked = true;
                XAxisCB.SelectedIndex = (int)Viewer.X1;
                YAxisCB.SelectedIndex = (int)Viewer.Y1;
            }
            else
            {
                MagicFormulaRB.Checked = true;
                XAxisCB.SelectedIndex = (int)Viewer.X2;
                YAxisCB.SelectedIndex = (int)Viewer.Y2;
            }
            if (Viewer.GetTitle().Text == null)
            {
                ShowTitleCB.Checked = false;

            }
            else
            {
                TitleTB.Text = Viewer.GetTitle().Text;
                ShowTitleCB.Checked = true;

            }

            foreach (var e in Enum.GetValues(typeof(TickMarkStyle)))
            {
                XTickStyleCB.Items.Add(e);
                YTickStyleCB.Items.Add(e);
            }
            foreach (var e in Enum.GetValues(typeof(ChartDashStyle)))
            {
                XGridLineStyleCB.Items.Add(e);
                YGridLineStyleCB.Items.Add(e);
            }

            var xas = Viewer.GetXAxisStyle();
            var yas = Viewer.GetYAxisStyle();
            XTickStyleCB.SelectedItem = xas.tms;
            YTickStyleCB.SelectedItem = yas.tms;
            XGridLineStyleCB.SelectedItem = xas.ds;
            YGridLineStyleCB.SelectedItem = yas.ds;
            XGridLabelFormat.Text = xas.format;
            YGridLabelFormat.Text = yas.format;
            //XGridLineOffsetCB.Checked = xas.offset;
            //YGridLineOffsetCB.Checked = xas.offset;
            PointsToRenderTB.Text = Viewer.numPoints.ToString();
            init = false;
        }

        private void AxisTypeChanged(object sender, EventArgs e)
        {
            XAxisCB.Items.Clear();
            YAxisCB.Items.Clear();
            if (sender == MagicFormulaRB)
            {
                XAxisCB.Items.AddRange(Enum.GetNames(typeof(MagicFormulaInputVariables)));
                YAxisCB.Items.AddRange(Enum.GetNames(typeof(MagicFormulaOutputVariables)));
                XAxisCB.SelectedIndex = 0;
                YAxisCB.SelectedIndex = 1;
            }
            else
            {
                foreach (var label in TireData.Label)
                {
                    XAxisCB.Items.Add(label.Value);
                    YAxisCB.Items.Add(label.Value);
                }
                XAxisCB.SelectedIndex = 0;
                YAxisCB.SelectedIndex = 1;
            }
            if (init) return;
            axisChanged = true;
        }

        MultiTireDataViewer Viewer;
        bool hasError = false;
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
                if (sender == TitleTB)
                {
                    ChartItemChanged(sender, e);
                    return;
                }
                var cea = new CancelEventArgs(false);
                IsReal_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsReal_Validated(sender, e);
                }
            }
        }

        private void IsNatural_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);

            string s = tb.Text;
            if (!(StaticFunctions.IsNInt(s) || s == ""))
            {
                this.EP_NumericalInput.SetError(tb, "自然数のみ入力");

                hasError = true;
                e.Cancel = true;
                return;
            }
        }

        private void IsNatural_Validated(object sender, EventArgs e)
        {
            hasError = false;
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
            Viewer.numPoints = StaticFunctions.IsNInt(PointsToRenderTB.Text) ? int.Parse(PointsToRenderTB.Text) : 3000;
            axisChanged = true;
        }

        private void TextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender == TitleTB)
                {
                    ChartItemChanged(sender, e);
                    return;
                }
                var cea = new CancelEventArgs(false);
                IsReal_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsNatural_Validated(sender, e);
                }
            }
        }

        private int getNewSurfix()
        {
            int i = 0;
            while (true)
            {
                bool found = false;
                foreach (Control c in DataSourceList.Controls)
                {

                    var editor = c as SeriesEditor;
                    if (editor != null)
                    {
                        if (editor.Suffix == i)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found) ++i;
                else break;
            }
            return i;
        }

        private void AddRaw(SeriesEditor raw)
        {
            raw.Dock = DockStyle.Top;
            DataSourceList.Controls.Add(raw);
            if (DataSourceList.Controls.Count - 2 < ColorList.Count)
            {
                raw.SetColor(ColorList[DataSourceList.Controls.Count - 2]);
            }
            raw.Click += SeriesEditor_Click;

            DataSourceList.Controls.SetChildIndex(raw, 0);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(ProjectManager.ProjectNode == null)
            {
                MessageBox.Show("プロジェクトが読み込まれていません");
                return;
            }

            int i = getNewSurfix();
            


            var raw = new SeriesEditor(Viewer,i, DataSourceList);
            AddRaw(raw);
        }

        int lastIndex = -1;
        private void SeriesEditor_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                foreach (var raw in DataSourceList.Controls)
                {
                    var se = raw as SeriesEditor;
                    if (se != null)
                    {
                        se.Selected = false;
                    }
                }
            }
            var series = sender as SeriesEditor;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift && lastIndex != -1)
            {
                int nowIndex = DataSourceList.Controls.IndexOf(series);
                int start = Math.Min(nowIndex, lastIndex);
                int range = Math.Abs(nowIndex - lastIndex);
                foreach(Control c in DataSourceList.Controls)
                {
                    var editor = c as SeriesEditor;
                    if(editor != null)
                    {
                        int i = DataSourceList.Controls.IndexOf(editor);
                        if( i >= start && i <= start + range)
                        {
                            editor.Selected = true;
                        }
                    }
                }
            }
            else
            {
                series.Selected = true;
                lastIndex = DataSourceList.Controls.IndexOf(series);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var list = new List<SeriesEditor>();
            foreach (Control c in DataSourceList.Controls)
            {
                var editor = c as SeriesEditor;
                if (editor != null && editor.Selected == true)
                {
                    list.Add(editor);
                }
            }
            if(list.Count != 0)
            {
                DialogResult result = MessageBox.Show(string.Format("{0}件を削除しますか?", list.Count()), "確認",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    foreach (var editor in list)
                    {

                        DataSourceList.Controls.Remove(editor);
                        editor.OnDelete();
                    }
                    lastIndex = -1;
                }
            }
            
              
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if(MagicFormulaRB.Checked)
            {
                Viewer.SetAxis(TireDataViewer.EnumAxis.MagicFormula);
                Viewer.SetAxis((MagicFormulaInputVariables)XAxisCB.SelectedIndex, (MagicFormulaOutputVariables)YAxisCB.SelectedIndex);
            }
            else
            {
                Viewer.SetAxis(TireDataViewer.EnumAxis.RawTireData);
                Viewer.SetAxis((TireDataColumn)XAxisCB.SelectedIndex, (TireDataColumn)YAxisCB.SelectedIndex);
            }
            //var x = Viewer.GetArea().AxisX;
            Viewer.GetArea().AxisX.Minimum = XMinTB.Text == "" ? Double.NaN: Double.Parse(XMinTB.Text);
            Viewer.GetArea().AxisX.Maximum = XMaxTB.Text == "" ? Double.NaN : Double.Parse(XMaxTB.Text);
            Viewer.GetArea().AxisY.Minimum = YMinTB.Text == "" ? Double.NaN : Double.Parse(YMinTB.Text);
            Viewer.GetArea().AxisY.Maximum= YMaxTB.Text == "" ? Double.NaN : Double.Parse(YMaxTB.Text);
            
            Viewer.UpdateViewer();

            

            if (ShowTitleCB.Checked)
            {
                Viewer.SetTitle(TitleTB.Text, null, null, null);
            }
            else
            {
                Viewer.SetTitle(null, null, null, null);
            }
            

            if (axisChanged)
            {
                foreach (Control c in DataSourceList.Controls)
                {
                    var editor = c as SeriesEditor;
                    if (editor != null)
                    {
                        editor.Replot();
                    }
                }
                axisChanged = false;
            }
            else
            {
                
                foreach (Control c in DataSourceList.Controls)
                {
                    var editor = c as SeriesEditor;
                    if (editor != null && editor.Changed == true)
                    {
                        editor.Replot();
                    }
                }
            }
            
            
        }

        public void ReplotAll()
        {
            axisChanged = true;
            ApplyButton_Click(this, new EventArgs());
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

            ApplyButton_Click(sender, e);
            this.DialogResult = DialogResult.OK;
        }

        private void ShowTitleCB_CheckedChanged(object sender, EventArgs e)
        {
            if(ShowTitleCB.Checked)
            {
                TitleTB.Enabled = true;
            }
            else
            {
                TitleTB.Enabled = false;
            }
        }

        private void AutoAxisStateChanged(object sender, EventArgs e)
        {
            double temp;
            if (sender == AutoScaleX)
            {
                Viewer.AutoScaleX = AutoScaleX.Checked;
                XMinTB.Enabled = !Viewer.AutoScaleX;
                XMaxTB.Enabled = !Viewer.AutoScaleX;
                if (Viewer.AutoScaleX)
                {
                    Viewer.GetArea().AxisX.Minimum = double.NaN;
                    Viewer.GetArea().AxisX.Maximum = double.NaN;
                }

                
                XMinTB.Text = double.IsNaN(temp = Viewer.GetArea().AxisX.Minimum) ? "" : temp.ToString();
                XMaxTB.Text = double.IsNaN(temp = Viewer.GetArea().AxisX.Maximum) ? "" : temp.ToString();
            }
            if (sender == AutoScaleY)
            {
                Viewer.AutoScaleY = AutoScaleY.Checked;
                YMinTB.Enabled = !Viewer.AutoScaleY;
                YMaxTB.Enabled = !Viewer.AutoScaleY;
                if (Viewer.AutoScaleY)
                {
                    Viewer.GetArea().AxisY.Minimum = double.NaN;
                    Viewer.GetArea().AxisY.Maximum = double.NaN;
                }


                YMinTB.Text = double.IsNaN(temp = Viewer.GetArea().AxisY.Minimum) ? "" : temp.ToString();
                YMaxTB.Text = double.IsNaN(temp = Viewer.GetArea().AxisY.Maximum) ? "" : temp.ToString();
            }

            UpdateViewer();
        }

        void UpdateViewer()
        {
            Viewer.UpdateViewer();
            
        }

        private void AxisCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init) return;
            axisChanged = true;
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            int index = -1;
            string legend = "";
            foreach (var raw in DataSourceList.Controls)
            {
                var se = raw as SeriesEditor;
                if (se != null && se.Selected && se.Initialized)
                {
                    index = DataSourceList.Controls.IndexOf(se);
                    index = index + 1;
                    if (index <= DataSourceList.Controls.Count-2)
                    {
                        legend = se.ID;
                        Viewer.SetDrawPriority(-1, legend);
                        DataSourceList.Controls.SetChildIndex(se,index);
                        break;
                    }
                    
                }
                
            }
            
            
            
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            int index = -1;
            string legend = "";
            foreach (var raw in DataSourceList.Controls)
            {
                var se = raw as SeriesEditor;
                if (se != null && se.Selected && se.Initialized)
                {
                    index = DataSourceList.Controls.IndexOf(se);
                    index = index - 1;
                    if (index >= 0)
                    {
                        legend = se.ID;
                        Viewer.SetDrawPriority(1, legend);
                        DataSourceList.Controls.SetChildIndex(se, index);
                        break;
                    }

                }

            }
        }

        private void ChartItemChanged(object sender, EventArgs e)
        {
            if (init) return;
            var xas = Viewer.GetXAxisStyle();
            var yas = Viewer.GetYAxisStyle();
            xas.tms = (XTickStyleCB.SelectedItem as TickMarkStyle?).Value;
            yas.tms = (YTickStyleCB.SelectedItem as TickMarkStyle?).Value;
            xas.ds = (XGridLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            yas.ds = (YGridLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            //xas.format = XGridLabelFormat.Text;
            //yas.format = YGridLabelFormat.Text;
            xas.offset = false;//XGridLineOffsetCB.Checked;
            yas.offset = false;//YGridLineOffsetCB.Checked;
            Viewer.SetXAxisStyle(xas);
            Viewer.SetYAxisStyle(yas);

            var legend = Viewer.GetLegend();
            var legend2 = new Legend(legend.Name);
            legend2.DockedToChartArea = legend.DockedToChartArea;
            legend2.Font = legend.Font;
            legend2.ForeColor = legend.ForeColor;
            legend2.Alignment = (StringAlignment)LegendAlignCB.SelectedItem;
            legend2.LegendStyle = (LegendStyle)LegendStyleCB.SelectedItem;
            legend2.Docking = (Docking)LegendDockCB.SelectedItem;
            legend2.IsDockedInsideChartArea = DockToAreaCB.Checked;
            Viewer.SetLegend(legend2);
            Viewer.UpdateViewer();

            if (ShowTitleCB.Checked)
            {
                Viewer.SetTitle(TitleTB.Text, TitleDockCB.SelectedItem as Docking?, null, null);
            }
            else
            {
                Viewer.SetTitle(null, TitleDockCB.SelectedItem as Docking?, null, null);
            }
        }

        private void XGridLabelFormat_Validating(object sender, CancelEventArgs e)
        {

        }

        private void XGridLabelFormat_Validated(object sender, EventArgs e)
        {
            var xas = Viewer.GetXAxisStyle();
            var yas = Viewer.GetYAxisStyle();
            xas.format = XGridLabelFormat.Text;
            yas.format = YGridLabelFormat.Text;
            Viewer.SetXAxisStyle(xas);
            Viewer.SetYAxisStyle(yas);
        }

        private void XAdvanced_Click(object sender, EventArgs e)
        {
            TireDataViewerGridEditor editor = new TireDataViewerGridEditor(Viewer.GetArea().AxisX, Viewer.SelectedViewer);
            editor.ShowDialog();
            Reload();
        }

        private void YAdvanced_Click(object sender, EventArgs e)
        {
            TireDataViewerGridEditor editor = new TireDataViewerGridEditor(Viewer.GetArea().AxisY, Viewer.SelectedViewer);
            editor.ShowDialog();
            Reload();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            var controls = new Control[DataSourceList.Controls.Count];
            DataSourceList.Controls.CopyTo(controls, 0);
            foreach (var raw in controls)
            {
                var se = raw as SeriesEditor;
                if (se != null && se.Selected && se.Initialized && se.IsMagicFormula)
                {
                    var newraw = new SeriesEditor( se, getNewSurfix());
                    AddRaw(newraw);
                    newraw.Replot();
                }

            }
        }
    }
}
