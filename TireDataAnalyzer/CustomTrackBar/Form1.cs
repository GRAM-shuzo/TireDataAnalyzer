using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CustomTrackBar
{
    partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                       this.Width = 500;
        }

        private void doubleTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = doubleTrackBar1.valueL.ToString();
            label2.Text = doubleTrackBar1.valueR.ToString();
        }
    }

    
}
