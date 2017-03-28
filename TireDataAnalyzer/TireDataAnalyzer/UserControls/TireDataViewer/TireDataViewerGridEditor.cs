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
    public partial class TireDataViewerGridEditor : Form
    {
        public TireDataViewerGridEditor()
        {
            InitializeComponent();
        }


        Axis axis;
        bool init = true;
        TireDataViewer Viewer;
        public TireDataViewerGridEditor(Axis axis_, TireDataViewer viewer)
            :this()
        {

            axis = axis_;
            Viewer = viewer;
            this.Text = axis.AxisName.ToString();
            InitCombobox();
            double temp;
            MinTB.Text = double.IsNaN(temp = axis.Minimum) ? "" : temp.ToString();
            MaxTB.Text = double.IsNaN(temp = axis.Maximum) ? "" : temp.ToString();
            AutoScaleCB.Checked = axis.AxisName == AxisName.X ? viewer.AutoScaleX: viewer.AutoScaleY;
            FormatTB.Text = axis.LabelStyle.Format;
            LineWidthTB.Text = axis.LineWidth.ToString();

            MajorGridEnabledCB.Checked = axis.MajorGrid.Enabled;
            MinorGridEnabledCB.Checked = axis.MinorGrid.Enabled;
            MajorTickEnabledCB.Checked = axis.MajorTickMark.Enabled;
            MinorTickEnabledCB.Checked = axis.MinorTickMark.Enabled;

            MajorGridIntervalTB.Text = axis.MajorGrid.Interval.ToString();
            MinorGridIntervalTB.Text = axis.MinorGrid.Interval.ToString();
            MajorTickIntervalTB.Text = axis.MajorTickMark.Interval.ToString();
            MinorTickIntervalTB.Text = axis.MinorTickMark.Interval.ToString();

            MajorGridLineWidthTB.Text = axis.MajorGrid.LineWidth.ToString();
            MinorGridLineWidthTB.Text = axis.MinorGrid.LineWidth.ToString();
            MajorTickLineWidthTB.Text = axis.MajorTickMark.LineWidth.ToString();
            MinorTickLineWidthTB.Text = axis.MinorTickMark.LineWidth.ToString();

            MajorGridOffsetCB.Checked = axis.MajorGrid.IntervalOffset != 0;
            MinorGridOffsetCB.Checked = axis.MinorGrid.IntervalOffset != 0;
            MajorTickOffsetCB.Checked = axis.MajorTickMark.IntervalOffset != 0;
            MinorTickOffsetCB.Checked = axis.MinorTickMark.IntervalOffset != 0;

            MajorTickSizeTB.Text = axis.MajorTickMark.Size.ToString();
            MinorTickSizeTB.Text = axis.MinorTickMark.Size.ToString();
            init = false;
        }


        private void TireDataViewerGridEditor_Load(object sender, EventArgs e)
        {
            
        }

        private void InitCombobox()
        {
            MajorGridColorCB.Items.Clear();
            MinorGridColorCB.Items.Clear();
            MajorTickColorCB.Items.Clear();
            MinorTickColorCB.Items.Clear();
            ColorCB.Items.Clear();
            foreach (var color in Enum.GetValues(typeof(KnownColor)))
            {
                MajorGridColorCB.Items.Add(color);
                MinorGridColorCB.Items.Add(color);
                MajorTickColorCB.Items.Add(color);
                MinorTickColorCB.Items.Add(color);
                ColorCB.Items.Add(color);
            }
            MajorGridColorCB.DrawItem += ColorCB_DrawItem;
            MinorGridColorCB.DrawItem += ColorCB_DrawItem;
            MajorTickColorCB.DrawItem += ColorCB_DrawItem;
            MinorTickColorCB.DrawItem += ColorCB_DrawItem;
            ColorCB.DrawItem += ColorCB_DrawItem;

            Color c = axis.MajorGrid.LineColor;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    MajorGridColorCB.SelectedItem = color;
                }
            }
            c = axis.MinorGrid.LineColor;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    MinorGridColorCB.SelectedItem = color;
                }
            }
            c = axis.MajorTickMark.LineColor;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    MajorTickColorCB.SelectedItem = color;
                }
            }
            c = axis.MinorTickMark.LineColor;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    MinorTickColorCB.SelectedItem = color;
                }
            }
            c = axis.LineColor;
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor(color) == c)
                {
                    ColorCB.SelectedItem = color;
                }
            }

            MajorGridLineStyleCB.Items.Clear();
            MinorGridLineStyleCB.Items.Clear();
            MajorTickLineStyleCB.Items.Clear();
            MinorTickLineStyleCB.Items.Clear();
            AxisLineStyleCB.Items.Clear();
            foreach (var style in Enum.GetValues(typeof(ChartDashStyle)))
            {
                MajorGridLineStyleCB.Items.Add(style);
                MinorGridLineStyleCB.Items.Add(style);
                MajorTickLineStyleCB.Items.Add(style);
                MinorTickLineStyleCB.Items.Add(style);
                AxisLineStyleCB.Items.Add(style);
            }

            MajorGridLineStyleCB.SelectedItem = axis.MajorGrid.LineDashStyle;
            MinorGridLineStyleCB.SelectedItem = axis.MinorGrid.LineDashStyle;
            MajorTickLineStyleCB.SelectedItem = axis.MajorTickMark.LineDashStyle;
            MinorTickLineStyleCB.SelectedItem = axis.MinorTickMark.LineDashStyle;
            AxisLineStyleCB.SelectedItem = axis.LineDashStyle;

            MajorTickOrientationCB.Items.Clear();
            MinorTickOrientationCB.Items.Clear();
            foreach (var style in Enum.GetValues(typeof(TickMarkStyle)))
            {
                MajorTickOrientationCB.Items.Add(style);
                MinorTickOrientationCB.Items.Add(style);
            }

            MajorTickOrientationCB.SelectedItem = axis.MajorTickMark.TickMarkStyle;
            MinorTickOrientationCB.SelectedItem = axis.MinorTickMark.TickMarkStyle;

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

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void IsReal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);

            string s = tb.Text;
            if (!(StaticFunctions.IsNumeric(s) || s == ""))
            {
                this.EP_NumericalInput.SetError(tb, "実数のみ入力");
                e.Cancel = true;
                return;
            }
        }

        private void IsReal_Validated(object sender, EventArgs e)
        {
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
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
        }

        private void TextBox_RealKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cea = new CancelEventArgs(false);
                IsReal_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsReal_Validated(sender, e);
                    ValueChanged(sender, e);
                }
            }
        }

        private void TextBox_NIntKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cea = new CancelEventArgs(false);
                IsNInt_Validating(sender, cea);
                if (cea.Cancel != true)
                {
                    IsNInt_Validated(sender, e);
                    ValueChanged(sender, e);
                }
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (init) return;

            axis.LabelStyle.Format = FormatTB.Text;

            axis.LineColor = Color.FromKnownColor((ColorCB.SelectedItem as KnownColor?).Value);
            axis.LineDashStyle = (AxisLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            axis.LineWidth = int.Parse(LineWidthTB.Text);

            axis.MajorGrid.Enabled = MajorGridEnabledCB.Checked;
            axis.MinorGrid.Enabled = MinorGridEnabledCB.Checked;
            axis.MajorTickMark.Enabled = MajorTickEnabledCB.Checked;
            axis.MinorTickMark.Enabled = MinorTickEnabledCB.Checked;


            MajorGridIntervalTB.Text = MajorGridAutoIntervalCB.Checked? axis.MajorGrid.Interval.ToString(): MajorGridIntervalTB.Text;
            MinorGridIntervalTB.Text = MinorGridAutoIntervalCB.Checked? axis.MinorGrid.Interval.ToString(): MinorGridIntervalTB.Text;
            MajorTickIntervalTB.Text = MajorTickAutoIntervalCB.Checked? axis.MajorTickMark.Interval.ToString(): MajorTickIntervalTB.Text;
            MinorTickIntervalTB.Text = MinorTickAutoIntervalCB.Checked? axis.MinorTickMark.Interval.ToString(): MinorTickIntervalTB.Text;

            axis.MajorGrid.Interval = !(MajorGridIntervalTB.Enabled = !MajorGridAutoIntervalCB.Checked)?0: double.Parse(MajorGridIntervalTB.Text);
            axis.MinorGrid.Interval = !(MinorGridIntervalTB.Enabled = !MinorGridAutoIntervalCB.Checked) ? 0 : double.Parse(MinorGridIntervalTB.Text);
            axis.MajorTickMark.Interval =!( MajorTickIntervalTB.Enabled = !MajorTickAutoIntervalCB.Checked) ? 0 : double.Parse(MajorTickIntervalTB.Text);
            axis.MinorTickMark.Interval = !(MinorTickIntervalTB.Enabled = !MinorTickAutoIntervalCB.Checked) ? 0 : double.Parse(MinorTickIntervalTB.Text);

            axis.LabelStyle.Interval = axis.MajorTickMark.Interval;
            
            axis.MajorGrid.LineWidth = int.Parse(MajorGridLineWidthTB.Text);
            axis.MinorGrid.LineWidth = int.Parse(MinorGridLineWidthTB.Text);
            axis.MajorTickMark.LineWidth = int.Parse(MajorTickLineWidthTB.Text);
            axis.MinorTickMark.LineWidth = int.Parse(MinorTickLineWidthTB.Text);

            axis.MajorGrid.LineColor = Color.FromKnownColor((MajorGridColorCB.SelectedItem as KnownColor?).Value);
            axis.MinorGrid.LineColor = Color.FromKnownColor((MinorGridColorCB.SelectedItem as KnownColor?).Value);
            axis.MajorTickMark.LineColor = Color.FromKnownColor((MajorTickColorCB.SelectedItem as KnownColor?).Value);
            axis.MinorTickMark.LineColor = Color.FromKnownColor((MinorTickColorCB.SelectedItem as KnownColor?).Value);

            axis.MajorGrid.IntervalOffset = 0;
            axis.MinorGrid.IntervalOffset = 0;
            axis.MajorTickMark.IntervalOffset = 0;
            axis.MinorTickMark.IntervalOffset = 0;
            if (MajorGridOffsetCB.Checked)
            {
                double diff = Math.Floor(axis.Minimum / axis.MajorGrid.Interval);
                axis.MajorGrid.IntervalOffset= (diff + 1) * axis.MajorGrid.Interval - axis.Minimum;
            }
            if (MinorGridOffsetCB.Checked)
            {
                double diff = Math.Floor(axis.Minimum / axis.MinorGrid.Interval);
                axis.MinorGrid.IntervalOffset = (diff + 1) * axis.MinorGrid.Interval - axis.Minimum;
            }
            if (MajorTickOffsetCB.Checked)
            {
                double diff = Math.Floor(axis.Minimum / axis.MajorTickMark.Interval);
                axis.MajorTickMark.IntervalOffset = (diff + 1) * axis.MajorTickMark.Interval - axis.Minimum;
            }
            if (MinorTickOffsetCB.Checked)
            {
                double diff = Math.Floor(axis.Minimum / axis.MinorTickMark.Interval);
                axis.MinorTickMark.IntervalOffset = (diff + 1) * axis.MinorTickMark.Interval - axis.Minimum;
            }
            axis.LabelStyle.IntervalOffset = axis.MajorTickMark.IntervalOffset;

            axis.MajorGrid.LineDashStyle = (MajorGridLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            axis.MinorGrid.LineDashStyle = (MinorGridLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            axis.MajorTickMark.LineDashStyle = (MajorTickLineStyleCB.SelectedItem as ChartDashStyle?).Value;
            axis.MinorTickMark.LineDashStyle = (MinorTickLineStyleCB.SelectedItem as ChartDashStyle?).Value;

            axis.MajorTickMark.TickMarkStyle = (MajorTickOrientationCB.SelectedItem as TickMarkStyle?).Value;
            axis.MinorTickMark.TickMarkStyle = (MinorTickOrientationCB.SelectedItem as TickMarkStyle?).Value;

            axis.MajorTickMark.Size = float.Parse(MajorTickSizeTB.Text);
            axis.MinorTickMark.Size = float.Parse(MinorTickSizeTB.Text);

            axis.Maximum = MaxTB.Text == "" ? Double.NaN : Double.Parse(MaxTB.Text);
            axis.Minimum = MinTB.Text == "" ? Double.NaN : Double.Parse(MinTB.Text);

            if (axis.AxisName == AxisName.X)
            {
                Viewer.AutoScaleX = AutoScaleCB.Checked;

            }
            else
            {
                Viewer.AutoScaleY = AutoScaleCB.Checked;
            }
        }

        private void AutoScaleCB_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if(cb.Checked)
            {
                MaxTB.Enabled = false;
                MinTB.Enabled = false;
                MaxTB.Text = "";
                MinTB.Text = "";
            }
            else
            {
                MaxTB.Enabled = true;
                MinTB.Enabled = true;
                double temp;
                MinTB.Text = double.IsNaN(temp = axis.Minimum) ? "" : temp.ToString();
                MaxTB.Text = double.IsNaN(temp = axis.Maximum) ? "" : temp.ToString();
            }
            if (axis.AxisName == AxisName.X)
            {
                Viewer.AutoScaleX = AutoScaleCB.Checked;

            }
            else
            {
                Viewer.AutoScaleY = AutoScaleCB.Checked;
            }
            ValueChanged(sender, e);
        }
    }
}
