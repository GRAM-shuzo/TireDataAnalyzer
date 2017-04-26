using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfMath;

namespace TireDataAnalyzer.UserControls
{
    public partial class WPFMathTest : Form
    {

        public WPFMathTest()
        {
            InitializeComponent();
            this.ResizeEnd += magicFormula_TexEquation1.Control_Resize;
            magicFormula_TexEquation1.Type = TexEquation.MagicFormula_TexEquation.MagicFormulaType.FX;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            magicFormula_TexEquation1.Highlight(int.Parse(textBox1.Text));
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                textBox1_TextChanged(sender, e);
            }
        }
    }
}
