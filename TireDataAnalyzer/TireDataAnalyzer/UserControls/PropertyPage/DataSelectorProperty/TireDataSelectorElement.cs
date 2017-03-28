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

namespace TireDataAnalyzer.UserControls
{
    
    public partial class TireDataSelectorElement : UserControl
    {
        public TireDataSelectorElement()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return groupBox.Text;
            }

            set
            {
                groupBox.Text = value;
            }
        }

        public void Initialize(double ma, double mi, TireDataColumn column)
        {
            Column = column;
            max = ma;
            min = mi;
            ConstrainedMax = max;
            ConstrainedMin = min;

            labelMax.Text = max.ToString();
            labelMin.Text = min.ToString();
            MaxTrackBar.Maximum = (int)max * 1000;
            MaxTrackBar.Minimum = (int)min * 1000;
            MinTrackBar.Maximum = (int)max * 1000;
            MinTrackBar.Minimum = (int)min * 1000;
        }
        TireDataColumn Column;
        double max;
        double min;
        double constrainedMax;
        double constrainedMin;

        double ConstrainedMax
        {
            get
            {
                return constrainedMax;
            }
            set
            {
                if (value > max) value = max;
                if (value < ConstrainedMin) value = ConstrainedMin;
                constrainedMax = value;
                maxTB.Text = constrainedMax.ToString();
                if (ConstrainedValueChanged != null)
                {
                    ConstrainedValueChanged(ConstrainedMax, ConstrainedMin, Column);
                }
            }
        }
        double ConstrainedMin
        {
            get
            {
                return constrainedMin;
            }
            set
            {
                if (value < min) value = min;
                if (value > ConstrainedMax) value = ConstrainedMax;
                constrainedMin = value;
                minTB.Text = ConstrainedMin.ToString();
                
                if (ConstrainedValueChanged !=null)
                {
                    ConstrainedValueChanged(ConstrainedMax, ConstrainedMin, Column);
                }
            }
        }
        public delegate void ConstrainedValueChangedDelegate(double max, double min, TireDataColumn column);
        public ConstrainedValueChangedDelegate ConstrainedValueChanged;

        private void MaxTrackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        private void MinTrackBar_ValueChanged(object sender, EventArgs e)
        {

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
                if (tb == minTB)
                {
                    
                    ConstrainedMin = value;
                    
                }
                if (tb == maxTB)
                {
                    
                    ConstrainedMax = value;
                    
                }
            }
            else
            {
                maxTB.Text = constrainedMax.ToString();
                minTB.Text = ConstrainedMin.ToString();
            }
        }


    }
}
