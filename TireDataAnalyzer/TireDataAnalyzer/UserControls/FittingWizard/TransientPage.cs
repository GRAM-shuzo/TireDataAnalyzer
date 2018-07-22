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
using TireDataAnalyzer.TexEquation;
namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class TransientPage : FittingWizardPage
    {
        List<TextBox> ParameterTB = new List<TextBox>();
        List<CheckBox> FittingParametersCB = new List<CheckBox>();
        List<SimpleTireDataSelector> TDSs = new List<SimpleTireDataSelector>();
        List<MultiTireDataViewer> Viewers = new List<MultiTireDataViewer>();
        List<MagicFormula_TexEquation> Equations = new List<MagicFormula_TexEquation>();
        List<bool> FirstPlot = new List<bool>();
        List<TireDataViewer.XY> EList = new List<TireDataViewer.XY>();
        List<int> NumPointList = new List<int>();
        List<Label> Advises = new List<Label>();
        bool ReplotData = false;
        bool ReplotFormula = false;
        bool stopReplot = false;
        string dataLegend = "data";
        string formulaLegend = "CenterLine";
        string formulaLegendU = "UpperLine";
        string formulaLegendL = "LowerLine";

        public TransientPage(FittingWizardPage previous)
            : base(previous, "過渡特性パラメータ")
        {
            InitializeComponent();

            Advises.Add(AdviseText0);
            Advises.Add(AdviseText1);

            ParameterTB.Add(s1TB);
            ParameterTB.Add(s2TB);
            ParameterTB.Add(s3TB);
            ParameterTB.Add(s4TB);
            ParameterTB.Add(s1TBX);
            ParameterTB.Add(s2TBX);
            ParameterTB.Add(s3TBX);
            ParameterTB.Add(s4TBX);



            FittingParametersCB.Add(checkBox1);
            FittingParametersCB.Add(checkBox2);
            FittingParametersCB.Add(checkBox3);
            FittingParametersCB.Add(checkBox4);
            FittingParametersCB.Add(checkBox11);
            FittingParametersCB.Add(checkBox12);
            FittingParametersCB.Add(checkBox13);
            FittingParametersCB.Add(checkBox14);

            TDSs.Add(simpleTireDataSelector1);
            TDSs.Add(simpleTireDataSelector2);
            Viewers.Add(Theta_FyViewer);
            Viewers.Add(sr_FxViewer);

            Viewers[0].SetAxis(TireDataViewer.EnumAxis.MagicFormula);
            Viewers[1].SetAxis(TireDataViewer.EnumAxis.MagicFormula);

            FirstPlot.Add(false);
            FirstPlot.Add(false);

            NumPointList.Add(-1);
            NumPointList.Add(100000);
            NumPointList.Add(50000);
            NumPointList.Add(10000);
            NumPointList.Add(5000);
            NumPointList.Add(1000);

            foreach( var tb in ParameterTB)
            {
                tb.KeyDown += TextBox_KeyDown;
                tb.Enter += Tb_Enter;
                tb.Leave += Tb_Leave;
                tb.Validating += IsReal_Validating;
                tb.Validated += IsReal_Validated;
            }
            foreach (var cb in FittingParametersCB)
            {
                cb.Enter += Tb_Enter;
                cb.CheckedChanged += FittingCheckedChanged;
            }
            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].SASREnable = false;
                TDSs[i].Size = new Size(TDSs[i].Size.Width, 200);
                TDSs[i].MFFD = MFFD;
                TDSs[i].ValueChanged += SelectorValueChanged;
            }
            for (int i = 0; i < Viewers.Count; ++i)
            {   
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint, dataLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegend);
                Viewers[i].SetDataListRefMF(dataLegend, formulaLegend);
                Viewers[i].SetLineWidth(5, formulaLegend);
                Viewers[i].ResetScreen(MultiTireDataViewer.EnumScreenCount.One);
            }
            

            Equations.Add(magicFormula_TexEquation0);
            Equations.Add(magicFormula_TexEquation1);
            SetClickAllControls(this);
        }

        public void  SetClickAllControls(Control top)
        {
            var buf = new List<Control>();
            foreach (Control c in top.Controls)
            {
                if (c is TextBox || c is CheckBox) continue;
                c.Click += TransientPage_Click;
                SetClickAllControls(c);
            }
            return;
        }

        private void TransientPage_Click(object sender, EventArgs e)
        {
            SetAdviceText(-1);
        }

        private void Tb_Leave(object sender, EventArgs e)
        {
            //SetAdviceText(-1);
        }

        private void SetAdviceText(int i)
        {

            if (i < 0 || i >= ParameterTB.Count)
            {
                foreach (var l in Advises)
                {
                    l.Text = @"Transientモデルへのフィッティングを行います。
入力値：輪荷重(FZ)、空気圧(P)
出力値：スリップアングル(スリップ率)に対する時定数（σ）
決める必要のあるパラメータはs1～s4の4変数です。
時定数が0(すべてのパラメータが0)のとき、遅れモデルは使用されません";
                }
                return;
            }

            string[] text = new string[8];
            text[0] = "横力に関する時定数の定数値を決定します";
            text[1] = @"横力に関する時定数の荷重依存性を決めます";
            text[2] = @"横力に関する時定数の空気圧依存性を決めます";
            text[3] = @"横力に関する時定数の荷重と空気圧の積への依存性を決めます。";
            text[4] = "前後力に関する時定数の定数値を決定します";
            text[5] = @"前後力に関する時定数の荷重依存性を決めます";
            text[6] = @"前後力に関する時定数の空気圧依存性を決めます";
            text[7] = @"前後力に関する時定数の荷重と空気圧の積への依存性を決めます。";
            foreach (var l in Advises)
            {
                l.Text = text[i];
            }
        }

        private void Tb_Enter(object sender, EventArgs e)
        {
            int i = -1;
            if (sender is TextBox)
            {
                i = ParameterTB.IndexOf(sender as TextBox);
            }
            else
            {
                i = FittingParametersCB.IndexOf(sender as CheckBox);
            }
            foreach (var mf in Equations)
            {
                mf.Highlight(i);
            }
            SetAdviceText(i);
        }

        private void PureCorneringPage_Load(object sender, EventArgs e)
        {

            foreach (var mf in Equations)
            {
                (Parent as Form).ResizeEnd += mf.Control_Resize;
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.Transient;
            }

            Viewers[0].SetAxis(MagicFormulaInputVariables.ET, MagicFormulaOutputVariables.FY);
            Viewers[1].SetAxis(MagicFormulaInputVariables.ET, MagicFormulaOutputVariables.FX);
        }

        protected override void Reload(bool back)
        {
            stopReplot = true;
            var Params = MFFD.MagicFormula.TFY.Parameters;
            for (int i = 0; i < MFFD.MagicFormula.TFY.FittingParameters.Count; ++i)
            {
                ParameterTB[i].Text = Params[i].ToString();
                FittingParametersCB[i].Checked = MFFD.MagicFormula.TFY.FittingParameters[i];
            }
            Params = MFFD.MagicFormula.TFX.Parameters;
            for (int i = 0; i < MFFD.MagicFormula.TFY.FittingParameters.Count; ++i)
            {
                ParameterTB[i+ MFFD.MagicFormula.TFY.FittingParameters.Count].Text = Params[i].ToString();
                FittingParametersCB[i+ MFFD.MagicFormula.TFY.FittingParameters.Count].Checked = MFFD.MagicFormula.TFX.FittingParameters[i];
            }
            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].MFFD = MFFD;
                FirstPlot[i] = false;
            }
            comboBox0.SelectedIndex = 4;
            comboBox1.SelectedIndex = 4;
            stopReplot = false;
            ReplotFormula = true;
            ReplotData = true;

            foreach (var mf in Equations)
            {
                mf.Control_Resize(mf, new EventArgs());
                mf.Size = new Size(mf.Size.Width, 100);
            }
            SetAdviceText(-1);
            RePlot();
        }

        bool hasError = false;
        private void IsReal_Validating(object sender, CancelEventArgs e)
        {
            var tb = (sender as TextBox);
            
            string s = tb.Text;
            if (!StaticFunctions.IsNumeric(s))
            {
                this.EP_NumericalInput.SetError(tb, "実数のみ入力");

                hasError = true;
                e.Cancel = true;
                NextButton.Enabled = false;
                PreviousButton.Enabled = false;
                return;
            }

            var ParamsY = MFFD.MagicFormula.TFY.Parameters;
            var ParamsX = MFFD.MagicFormula.TFX.Parameters;
            int i = ParameterTB.IndexOf((TextBox)sender);
            if(i< MFFD.MagicFormula.TFY.FittingParameters.Count)
                ParamsY[i] = double.Parse(((TextBox)sender).Text);
            else
                ParamsX[i- MFFD.MagicFormula.TFY.FittingParameters.Count] = double.Parse(((TextBox)sender).Text);


        }

        private void IsReal_Validated(object sender, EventArgs e)
        {
            hasError = false;
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
            ReplotFormula = true;
            NextButton.Enabled = true;
            PreviousButton.Enabled = true;
            RePlot();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cea = new CancelEventArgs(false);
                IsReal_Validating(sender, cea);
                if(cea.Cancel != true)
                {
                    IsReal_Validated(sender, e);
                }
            }
        }

        void RePlot()
        {
            if (stopReplot) return;
            int tabIndex = TabControl.SelectedIndex;
            if (ReplotData || !FirstPlot[tabIndex])
            {
                var corneringTable = TDSs[tabIndex].SelectedData().GetDataSet().CorneringTable;
                Viewers[tabIndex].SetDataList(corneringTable, Table.CorneringTable, dataLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].CenterValue, formulaLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].UpperValue, formulaLegendU);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].LowerValue, formulaLegendL);
                Viewers[tabIndex].DrawGraph(dataLegend);
                ReplotData = false;
            }
            if ((ReplotFormula || !FirstPlot[tabIndex]))
            {

                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].CenterValue, formulaLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].UpperValue, formulaLegendU);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].LowerValue, formulaLegendL);
                Viewers[tabIndex].DrawGraph(formulaLegend);
                Viewers[tabIndex].DrawGraph(formulaLegendU);
                Viewers[tabIndex].DrawGraph(formulaLegendL);
                ReplotFormula = false;
                FirstPlot[tabIndex] = true;
            }
        }

        protected override bool OnNext()
        {
            return !hasError;
        }

        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (hasError)
            {
                e.Cancel = true;
                return;
            }
        }

        private void TabControl_TabIndexChanged(object sender, EventArgs e)
        {
            ReplotFormula = true;
            RePlot();
        }

        private void SelectorValueChanged(object sender, EventArgs e)
        {
            ReplotData = true;
            ReplotFormula = true;
            RePlot();
        }

        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            ReplotFormula = true;
            RePlot();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = TabControl.SelectedIndex;
            int points = NumPointList[(sender as ComboBox).SelectedIndex];
            TDSs[i].NumSearch = points;
            Viewers[i].numPoints = points;
            ReplotData = true;
            RePlot();
        }

        private void FittingCheckedChanged(object sender, EventArgs e)
        {
            var i = FittingParametersCB.IndexOf(sender as CheckBox);
            if(i< MFFD.MagicFormula.TFY.FittingParameters.Count)
                MFFD.MagicFormula.TFY.FittingParameters[i] = FittingParametersCB[i].Checked;
            else
                MFFD.MagicFormula.TFX.FittingParameters[i- MFFD.MagicFormula.TFY.FittingParameters.Count] = FittingParametersCB[i].Checked;
        }
    }
}
