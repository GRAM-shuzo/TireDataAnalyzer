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
        private WpfMath.Controls.FormulaControl formula;

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
            var testFormula1 = @"{F_y} = D\, \sin(C \arctan(B(x+ S_h) - E(B(x + S_h)  - \arctan B(x+ S_h) ))) + S_v";
            var testFormula2 = @"{D} = { FZ}\,\left({ a_2}\,{ FZ}+{ a_1}\right)\,\left(1-{ a_3}\,{ IA}^2\right)\,\left({ a_5}\,P^2+{ a_4}\,P+1\right)\,\left({ a_6}\,T+1\right)";
            var testFormula3 = @"{BCD}={ a_7}\,{ FZ}\,\left({ a_8}\,P + 1\right)\,\sin \left({ a_9}\,\arctan \left({\frac{ { FZ} } {\left({ a_{ 10} } +{ a_{ 11} }\, { IA}^ 2\right)\,\left(1 +{ a_{ 12} }\,P\right)} }\right)\right)\,\left(1 -{ a_{ 13} }\,\left | { IA}\right | \right)\,\left({ a_{ 14} }\,T + 1\right)";
            var testFormula4 = @"{E}=\left({ a_{15}}+{ a_{16}}\,{ FZ}\right)\,\left({ a_{17}}\,{ IA}^2-{ a_{18}}\,{ IA}\,{ sgn}\left({ x}+{ Sh}\right)+1\right)";
            var testFormula5 = @"{S_h}=\left({ a_{19}}\,{ FZ}+{ a_{20}}\,{ FZ}^2\right)\,\left({ a_{21}}\,P+1\right){ IA}";
            var testFormula6 = @"{S_v}= \left({ a_{23}}\,{ FZ}+{ a_{24}}\,{ FZ}^2 \right)\, { IA}";
            formula = new WpfMath.Controls.FormulaControl();
            formula.Formula = testFormula1;


            elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            //コントロールの位置と大きさを設定する
            elementHost1.Dock = DockStyle.Fill;
            //elementHost1.SetBounds(0,0,760, 100);
            elementHost1.Child = formula;

            //ElementHostをフォームに配置する
            this.Controls.Add(elementHost1);
            
        }
        private void DoEvents()
        {
            var frame = new System.Windows.Threading.DispatcherFrame();
            var callback = new System.Windows.Threading.DispatcherOperationCallback(obj =>
            {
                ((System.Windows.Threading.DispatcherFrame)obj).Continue = false;
                return null;
            });
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, callback, frame);
            System.Windows.Threading.Dispatcher.PushFrame(frame);
        }

        private void WPFMathTest_Resize(object sender, EventArgs e)
        {
            elementHost1.Update();
            formula.Scale = 30;
            DoEvents();
            
            while (formula.DesiredSize.Width >= elementHost1.Width)
            {
                --formula.Scale;
                DoEvents();
            }
        }
    }
}
