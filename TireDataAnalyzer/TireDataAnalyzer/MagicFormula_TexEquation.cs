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
using TTCDataUtils;

namespace TireDataAnalyzer.TexEquation
{

    public partial class MagicFormula_TexEquation : UserControl
    {
        private System.Windows.Forms.Integration.ElementHost[] elementHost = new System.Windows.Forms.Integration.ElementHost[2];
        private TexFormulaParser formulaParser = new TexFormulaParser();
        private WpfMath.Controls.FormulaControl[] formulaControl = new WpfMath.Controls.FormulaControl[2];

        public MagicFormula_TexEquation()
        {
            InitializeComponent();
            

            for(int i = elementHost.Count()-1; i>=0; --i)
            {
                formulaControl[i] = new WpfMath.Controls.FormulaControl();
                formulaControl[i].Formula = FY[i];
                elementHost[i] = new System.Windows.Forms.Integration.ElementHost();
                //コントロールの位置と大きさを設定する
                elementHost[i].Dock = DockStyle.Top;
                elementHost[i].Child = formulaControl[i];

                //ElementHostをフォームに配置する
                this.Controls.Add(elementHost[i]);
            }
        }

        public enum MagicFormulaType
        {
            FY
        }

        MagicFormulaType type;
        public MagicFormulaType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public void Highlight(int k)
        {
            var formula = GetFormula();
            var param = GetParameterString(k);
            string Coef = "nothing__";
            for(int i=1; i<formula.Count(); ++i)
            {
                var r = new System.Text.RegularExpressions.Regex(param);
                System.Text.RegularExpressions.Match m = r.Match(formula[i]);
                if (m.Success)
                {
                    formulaControl[1].Formula = System.Text.RegularExpressions.Regex.Replace(
                        formula[i],
                        param,
                        "\\color{red}$&");
                    r = new System.Text.RegularExpressions.Regex(@"{?[\s\t]*(?<name>.+?)[\s\t]*}?[\s\t]*=",System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
                    m = r.Match(formula[i]);
                    if (m.Success)
                    {
                        var name = m.Groups["name"].Value;
                        formulaControl[0].Formula = System.Text.RegularExpressions.Regex.Replace(
                        formula[0],
                        name,
                        "{\\color{red}$&}");
                    }
                }
            }
            Control_Resize(this, new EventArgs());
        }

        string[] GetFormula()
        {
            switch (Type)
            {
                case MagicFormulaType.FY:
                    return FY;
            }
            return null;
        }

        string GetParameterString(int i)
        {
            if (i < 0) return "";
            switch (Type)
            {
                case MagicFormulaType.FY:
                    return @"a_{?[\s\t]*"+i.ToString()+ @"\D[\s\t]*}?";
            }
            return "nothing_____";
        }

        string[] FY = {
            @"{F_y} = D\, \sin(C \arctan(B(x+ S_h) - E(B(x + S_h)  - \arctan B(x+ S_h) ))) + S_v",
            @"{D} = { FZ}\,\left({ a_2}\,{ FZ}+{ a_1}\right)\,\left(1-{ a_3}\,{ IA}^2\right)\,\left({ a_5}\,P^2+{ a_4}\,P+1\right)\,\left({ a_6}\,T+1\right)",
            @"{BCD}={ a_7}\,{ FZ}\,\left({ a_8}\,P + 1\right)\,\sin \left({ a_9}\,\arctan \left({\frac{ { FZ} } {\left({ a_{10} } +{ a_{ 11} }\, { IA}^ 2\right)\,\left(1 +{ a_{ 12} }\,P\right)} }\right)\right)\,\left(1 -{ a_{ 13} }\,\left | { IA}\right | \right)\,\left({ a_{ 14} }\,T + 1\right)",
            @"{E}=\left({ a_{15}}+{ a_{16}}\,{ FZ}\right)\,\left({ a_{17}}\,{ IA}^2-{ a_{18}}\,{ IA}\,{ sgn}\left({ x}+{ Sh}\right)+1\right)",
            @"{S_h}=\left({ a_{19}}\,{ FZ}+{ a_{20}}\,{ FZ}^2\right)\,\left({ a_{21}}\,P+1\right){ IA}",
            @"{S_v}= \left({ a_{23}}\,{ FZ}+{ a_{24}}\,{ FZ}^2 \right)\, { IA}"
        };

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

        public void Control_Resize(object sender, EventArgs e)
        {
            double minsize = 30;
            for(int i=0; i<elementHost.Count(); ++i)
            {
                elementHost[i].Update();
                formulaControl[i].Scale = minsize;
                DoEvents();

                while (formulaControl[i].DesiredSize.Width >= elementHost[i].Width)
                {
                    --formulaControl[i].Scale;
                    DoEvents();
                }
                minsize = Math.Min(minsize,formulaControl[i].Scale);
            }
            for (int i = 0; i < elementHost.Count(); ++i)
            {
                formulaControl[i].Scale = minsize;
                
            }
            DoEvents();
            for (int i = 0; i < elementHost.Count(); ++i)
            {
                int before = 0;
                while(before != (int)(formulaControl[i].DesiredSize.Height) + 1)
                {
                    elementHost[i].Height = (int)(formulaControl[i].DesiredSize.Height) + 1;
                    before = elementHost[i].Height;
                    DoEvents();
                }
                
            }
        }

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
    }
}
