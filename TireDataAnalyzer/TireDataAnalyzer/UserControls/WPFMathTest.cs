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
        private TexFormulaParser formulaParser = new TexFormulaParser();
        private System.Windows.Forms.Integration.ElementHost elementHost1;

        private TexFormula ParseFormula(string input)
        {
            // Create formula object from input text.
            TexFormula formula = null;
            try
            {
                formula = this.formulaParser.Parse(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while parsing the given input:" + Environment.NewLine +
                    Environment.NewLine + ex.Message, "WPF-Math Example");
            }

            return formula;
        }

        public WPFMathTest()
        {
            InitializeComponent();
            var testFormula1 = "\\int_0^{\\infty}{x^{2n} e^{-a x^2} dx} = \\frac{2n-1}{2a} \\int_0^{\\infty}{x^{2(n-1)} e^{-a x^2} dx} = \\frac{(2n-1)!!}{2^{n+1}} \\sqrt{\\frac{\\pi}{a^{2n+1}}}";
            var testFormula2 = "\\int_a^b{f(x) dx} = (b - a) \\sum_{n = 1}^{\\infty}  {\\sum_{m = 1}^{2^n  - 1} { ( { - 1} )^{m + 1} } } 2^{ - n} f(a + m ( {b - a}  )2^{-n} )";
            var testFormula3 = @"L = \int_a^b \sqrt[4]{ \left| \sum_{i,j=1}^ng_{ij}\left(\gamma(t)\right) \left[\frac{d}{dt}x^i\circ\gamma(t) \right] \left{\frac{d}{dt}x^j\circ\gamma(t) \right} \right|}dt";
            var formula = new WpfMath.Controls.FormulaControl();
            formula.Formula = testFormula1;


            elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            //コントロールの位置と大きさを設定する
            elementHost1.SetBounds(0,0,500, 100);
            elementHost1.Child = formula;

            //ElementHostをフォームに配置する
            this.Controls.Add(elementHost1);
        }
    }
}
