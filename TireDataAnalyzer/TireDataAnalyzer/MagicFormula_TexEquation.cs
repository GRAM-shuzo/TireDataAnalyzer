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
            FY,
            FX,
            CFY,
            CFX
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
                Highlight(-1);
            }
        }

        public void Highlight(int k)
        {
            var formula = GetFormula();
            var param = GetParameterString(k);
            formulaControl[1].Formula = formula[1];
            formulaControl[0].Formula = formula[0];
            for (int i=1; i<formula.Count(); ++i)
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
                case MagicFormulaType.FX:
                    return FX;
                case MagicFormulaType.CFY:
                    return CFY;
                case MagicFormulaType.CFX:
                    return CFX;
            }
            return null;
        }

        string GetParameterString(int i)
        {
            if (i < 0) return "nothing_____";
            switch (Type)
            {
                case MagicFormulaType.FY:
                case MagicFormulaType.FX:
                    return @"a_{?[\s\t]*"+i.ToString()+ @"\D[\s\t]*}?";
                case MagicFormulaType.CFY:
                case MagicFormulaType.CFX:
                    return @"b_{?[\s\t]*" + i.ToString() + @"\D[\s\t]*}?";
            }
            return "nothing_____";
        }

        string[] FX = {
            @"{F_x} = D\, \sin(C \arctan(B(x+ S_h) - E(B(x + S_h)  - \arctan B(x+ S_h) ))) + S_v",
            @"{C} = {a_0}",
            @"{D} = { a_1}\,{ FZ}\,\left({ a_2}\,{ FZ}+1\right)\,\left(1- { a_3}\,{ IA}^2\right)\,\left({ a_5}\,P^2+{ a_4}\,P+1\right)\,\left({ a_6}\,T+1\right)",
            @"{BCD}={BCD} = { a_7}\,{ FZ}\,\left({ a_8}\,{ FZ}+1\right)\,e^{ { a_9}\,{ FZ}}\,\left({ a_{11}}\,P^2+{ a_{10}}\,P+1\right)\,\left({ a_{12}}\,T+1\right)",
            @"{E} ={ a_{13}}\,\left({ a_{15}}\,{ FZ}^2+{ a_{14}}\,{ FZ}+1\right)\,\left(1-{ a_{16}}\,{ sgn}\left({ SR}\right)\right)",
            @"{S_h} = {a_{17}}+{a_{18}}\,{FZ}"
        };

        string[] FY = {
            @"{F_y} = D\, \sin(C \arctan(B(x+ S_h) - E(B(x + S_h)  - \arctan B(x+ S_h) ))) + S_v",
            @"{C} = {a_0}",
            @"{D} = { FZ}\,\left({ a_2}\,{ FZ}+{ a_1}\right)\,\left(1-{ a_3}\,{ IA}^2\right)\,\left({ a_5}\,P^2+{ a_4}\,P+1\right)\,\left({ a_6}\,T+1\right)",
            @"{BCD}={ a_7}\,{ FZ}\,\left({ a_8}\,P + 1\right)\,\sin \left({ a_9}\,\arctan \left({\frac{ { FZ} } {\left({ a_{10} } +{ a_{ 11} }\, { IA}^ 2\right)\,\left(1 +{ a_{ 12} }\,P\right)} }\right)\right)\,\left(1 -{ a_{ 13} }\,\left | { IA}\right | \right)\,\left({ a_{ 14} }\,T + 1\right)",
            @"{E}=\left({ a_{15}}+{ a_{16}}\,{ FZ}\right)\,\left({ a_{17}}\,{ IA}^2-{ a_{18}}\,{ IA}\,{ sgn}\left({ x}+{ Sh}\right)+1\right)",
            @"{S_h}=\left({ a_{19}}\,{+ FZ}+{ a_{20}}\,{ FZ}^2\right)\,\left({ a_{21}}\,P+1\right){ IA}",
            @"{S_v}= \left({ a_{22}}\,{ FZ}+{ a_{23}}\,{ FZ}^2 \right)\, { IA}"
        };

        string[] CFX = {
            @"{combF_x} = {F_x}{G_x}\,\,,{G_x} = \frac {\cos \left(C\,\arctan \left(B\,\left(x+{ S_h}\right) - E\,\left(B\,\left(x+{ S_h}\right)- \arctan \left(B\,\left(x+{ S_h}\right)\right)\right)\right)\right)} {\cos \left(C\,\arctan \left(B\,{ S_h} -E\, \left(B\,{ S_h}-\arctan \left(B\,{ S_h}\right)\right)\right)\right)}",
            @"{C} = {b_3}",
            @"{B} = \frac{{ b_0}\,\left({ b_1}\,{ IA}^2+1\right)}{\sqrt{ { b_2}^2\,{ SR}^2+1}}",
            @"{E}= {b_4}\,\left(1+{b_5}\,FZ\right)",
            @"{S_h}={b_6}",
        };

        string[] CFY = {
            @"{combF_y} = {F_y}{G_y}+{S_v}\,\,,{G_y} = \frac {\cos \left(C\,\arctan \left(B\,\left(x+{ S_h}\right) - E\,\left(B\,\left(x+{ S_h}\right)- \arctan \left(B\,\left(x+{ S_h}\right)\right)\right)\right)\right)} {\cos \left(C\,\arctan \left(B\,{ S_h} -E\, \left(B\,{ S_h}-\arctan \left(B\,{ S_h}\right)\right)\right)\right)}",
            @"{C} = {b_{12}}",
            @"{B} = \frac{{{ b_9}\,{ IA}^2+{ b_8}}}{ {\sqrt{{ b_{10}}^2\, \left({ SA}-{ b_{11}}\right)^2+1}}}",
            @"{E}=  {b_{13}}+{b_{14}}\,FZ",
            @"{S_h}= \left({ b_7}\,{ FZ}+{ b_6}\right)\,{ IA}",
            @"{S_v}= \frac{{m\,{ FZ}\,\left({ b_2}\,{ IA}+{ b_1}\,{ FZ}+ { b_0}\right)\,\sin \left({ b_4}\,\arctan \left({ b_5}\,{ IA}\right)\right)}}{{\sqrt{{ b_3}^2\,{ SA}^2+1}}}",
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

        private void MagicFormula_TexEquation_Load(object sender, EventArgs e)
        {
            Control_Resize(this, e);
        }
    }
}
