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
    public partial class SelfAligningTorquePage : FittingWizardPage
    {
        List<TextBox> ParameterTB = new List<TextBox>();
        List<CheckBox> FittingParametersCB = new List<CheckBox>();
        List<SimpleTireDataSelector> TDSs = new List<SimpleTireDataSelector>();
        List<TireDataViewer> Viewers = new List<TireDataViewer>();
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

        public SelfAligningTorquePage(FittingWizardPage previous)
            : base(previous, "セルフアライニングトルクパラメータ")
        {
            InitializeComponent();

            Advises.Add(AdviseText0);
            Advises.Add(AdviseText1);
            Advises.Add(AdviseText2);
            Advises.Add(AdviseText3);

            ParameterTB.Add(a0TB);
            ParameterTB.Add(a1TB);
            ParameterTB.Add(a2TB);
            ParameterTB.Add(a3TB);
            ParameterTB.Add(a4TB);
            ParameterTB.Add(a5TB);
            ParameterTB.Add(a6TB);
            ParameterTB.Add(a7TB);
            ParameterTB.Add(a8TB);
            ParameterTB.Add(a9TB);
            ParameterTB.Add(a10TB);
            ParameterTB.Add(a11TB);
            ParameterTB.Add(a12TB);
            ParameterTB.Add(a13TB);
            ParameterTB.Add(a14TB);
            ParameterTB.Add(a15TB);
            ParameterTB.Add(a16TB);
            ParameterTB.Add(a17TB);
            ParameterTB.Add(a18TB);
            ParameterTB.Add(s1TB);
            ParameterTB.Add(s2TB);
            ParameterTB.Add(s3TB);
            ParameterTB.Add(s4TB);

            FittingParametersCB.Add(checkBox0);
            FittingParametersCB.Add(checkBox1);
            FittingParametersCB.Add(checkBox2);
            FittingParametersCB.Add(checkBox3);
            FittingParametersCB.Add(checkBox4);
            FittingParametersCB.Add(checkBox5);
            FittingParametersCB.Add(checkBox6);
            FittingParametersCB.Add(checkBox7);
            FittingParametersCB.Add(checkBox8);
            FittingParametersCB.Add(checkBox9);
            FittingParametersCB.Add(checkBox10);
            FittingParametersCB.Add(checkBox11);
            FittingParametersCB.Add(checkBox12);
            FittingParametersCB.Add(checkBox13);
            FittingParametersCB.Add(checkBox14);
            FittingParametersCB.Add(checkBox15);
            FittingParametersCB.Add(checkBox16);
            FittingParametersCB.Add(checkBox17);
            FittingParametersCB.Add(checkBox18);

            FittingParametersCB.Add(checkBoxs1);
            FittingParametersCB.Add(checkBoxs2);
            FittingParametersCB.Add(checkBoxs3);
            FittingParametersCB.Add(checkBoxs4);

            TDSs.Add(simpleTireDataSelector1);
            TDSs.Add(simpleTireDataSelector2);
            TDSs.Add(simpleTireDataSelector3);
            TDSs.Add(simpleTireDataSelector4);

            Viewers.Add(Theta_FyViewer);
            Viewers.Add(Fz_FyViewer);
            Viewers.Add(Fz_CpViewer);
            Viewers.Add(P_FyViewer);

            Viewers[0].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[1].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[2].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[3].Axis = TireDataViewer.EnumAxis.MagicFormula;

            FirstPlot.Add(false);
            FirstPlot.Add(false);
            FirstPlot.Add(false);
            FirstPlot.Add(false);

            NumPointList.Add(-1);
            NumPointList.Add(100000);
            NumPointList.Add(50000);
            NumPointList.Add(10000);
            NumPointList.Add(5000);
            NumPointList.Add(1000);
            foreach (var tb in ParameterTB)
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
                TDSs[i].SASREnable = true;
                TDSs[i].AttachedTable = Table.StaticTable;
                TDSs[i].Size = new Size(TDSs[i].Size.Width, 200);
                TDSs[i].MFFD = MFFD;
                TDSs[i].ValueChanged += SelectorValueChanged;

                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint, dataLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendU);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendL);
                Viewers[i].SetDataListRefMF(dataLegend, formulaLegend);
                Viewers[i].SetLineWidth(5, formulaLegend);
            }
            /*
            Viewers[5].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, EUpperLegend);
            Viewers[5].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, ELowerLegend);
            Viewers[5].SetLineWidth(5, EUpperLegend);
            Viewers[5].SetLineWidth(5, ELowerLegend);
            */
            Equations.Add(magicFormula_TexEquation0);
            Equations.Add(magicFormula_TexEquation1);
            Equations.Add(magicFormula_TexEquation2);
            Equations.Add(magicFormula_TexEquation3);
            SetClickAllControls(this);
        }

        public void SetClickAllControls(Control top)
        {
            var buf = new List<Control>();
            foreach (Control c in top.Controls)
            {
                if (c is TextBox || c is CheckBox) continue;
                c.Click += PureCorneringPage_Click;
                SetClickAllControls(c);
            }
            return;
        }

        private void PureCorneringPage_Click(object sender, EventArgs e)
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
                AdviseText0.Text = @"SelfAligningTorqueモデルへのフィッティングを行います。
入力値：スリップアングル(SA)、スリップ率(SR)、輪荷重(FZ)、キャンバ角(IA)、空気圧(P)
出力値：横力(Mz)
決める必要のあるパラメータはa0～a18, s1～s4の23変数です。(のちに最適化されます)
ニューマチックトレールPTと前後力影響度sを決めます";

                AdviseText1.Text = @"ニューマチックトレールのフィッティングを行います。
入力値：スリップアングル(SA)、スリップ率(SR)、輪荷重(FZ)、キャンバ角(IA)、空気圧(P)
出力値：ニューマチックトレール(PT)
ここではニューマチックトレールのカーブ形状を決めます";

                AdviseText2.Text = @"ニューマチックトレールのフィッティングを行います。
入力値：スリップアングル(SA)、輪荷重(FZ)、キャンバ角(IA)、空気圧(P)
出力値：ニューマチックトレール(PT)
ここではニューマチックトレールの最大値を決めます";

                AdviseText3.Text = @"セルフアライニングトルクのコンバインドスリップ要素です。
入力値：スリップアングル(SA)、輪荷重(FZ)、キャンバ角(IA)、空気圧(P)
出力値：コンバインドスリップ要素(s)
この値はコンバインドスリップ時のセルフアライニングトルクのカーブのずれ（Fxに比例と仮定）を埋めます";
                return;
            }

            string[] text = new string[24];
            text[0] = "Cはカーブ形状を決めます。通常は1<C<1.65の値をとります。";

            text[1] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a1は摩擦係数μの定数成分を決定します。";
            text[2] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a2は摩擦係数μの輪荷重依存成分を決定します。
輪荷重依存度a2は通常負の値をとります。(輪荷重が多いほど摩擦係数が減る)";
            text[3] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a3は摩擦係数μのキャンバ依存成分を決定します。
キャンバ依存度a3は通常負の値をとります。(キャンバスラストによる増分はSv、Shで決定されます)";
            text[4] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a4は摩擦係数μの圧力依存成分を決定します。
圧力依存性はa5と合わせて2次で近似します";
            text[5] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a5は摩擦係数μの圧力依存成分を決定します。
圧力依存性はa4と合わせて2次で近似します";
            text[6] = @"Dはキャンバ角0°でのカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a6は摩擦係数μの温度依存成分を決定します。
温度依存性は線形近似です。
温度依存性を加味しない場合はこの係数は0に固定してください";

            text[7] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
キャンバ0ではコーナリングパワーと一致します。
a7はコーナリングパワーのゲイン(FZの何倍か)を決定します。";
            text[8] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
キャンバ0ではコーナリングパワーと一致します。
a8はコーナリングパワーに対する圧力の線形的な依存性を決定します。";
            text[9] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
キャンバ0ではコーナリングパワーと一致します。
a9～a12はコーナリングパワーに対するキャンバ、輪荷重、圧力の非線形的依存性を表します。";
            text[10] = text[9];
            text[11] = text[9];
            text[12] = text[9];
            text[13] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
キャンバ0ではコーナリングパワーと一致します。
a13はコーナリングパワーに対するキャンバー角の線形的な依存性を決定します。";
            text[14] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
キャンバ0ではコーナリングパワーと一致します。
a13はコーナリングパワーに対するタイヤ温度の線形的な依存性を決定します。";

            text[15] = @"Eはカーブの形状を決定します。
Eは-(1+0.5C^2) < E < 1を満たす必要があり、
表示されるすべての点がアッパーとロアーの間に収まっていなければなりません。";
            text[16] = text[15];
            text[17] = text[15];
            text[18] = text[15];
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
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.MZ;
            }

            Viewers[0].SetAxis(MagicFormulaInputVariables.SA, MagicFormulaOutputVariables.MZ);
            Viewers[1].SetAxis(MagicFormulaInputVariables.SA, MagicFormulaOutputVariables.PT);
            Viewers[2].SetAxis(MagicFormulaInputVariables.FZ, MagicFormulaOutputVariables.PT_D);
            Viewers[3].SetAxis(MagicFormulaInputVariables.SA, MagicFormulaOutputVariables.MZ);
        }

        protected override void Reload(bool back)
        {
            stopReplot = true;
            var PTParams = MFFD.MagicFormula.MZ.PT.Parameters;
            var SParams = MFFD.MagicFormula.MZ.CMZM.Parameters;
            for (int i = 0; i < PTParams.Count(); ++i)
            {
                ParameterTB[i].Text = PTParams[i].ToString();
                FittingParametersCB[i].Checked = MFFD.MagicFormula.MZ.PT.FittingParameters[i];
            }
            for (int i = 0; i < SParams.Count(); ++i)
            {
                ParameterTB[PTParams.Count()+i].Text = SParams[i].ToString();
                FittingParametersCB[PTParams.Count()+i].Checked = MFFD.MagicFormula.MZ.CMZM.FittingParameters[i];
            }
            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].MFFD = MFFD;
                FirstPlot[i] = false;
            }
            comboBox0.SelectedIndex = 4;
            comboBox1.SelectedIndex = 4;
            comboBox2.SelectedIndex = 4;
            comboBox3.SelectedIndex = 4;
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

            var PTParams = MFFD.MagicFormula.MZ.PT.Parameters;
            var SParams = MFFD.MagicFormula.MZ.CMZM.Parameters;
            int i = ParameterTB.IndexOf((TextBox)sender);
            if (i < PTParams.Count)
                PTParams[i] = double.Parse(((TextBox)sender).Text);
            else
            {
                SParams[i- PTParams.Count] = double.Parse(((TextBox)sender).Text);
            }
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
                if (cea.Cancel != true)
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
                var driveBrakeTable = TDSs[tabIndex].SelectedData().GetDataSet().DriveBrakeTable;
                var combinedTable = new List<TireData>(corneringTable.Count + driveBrakeTable.Count);
                combinedTable.AddRange(corneringTable);
                corneringTable.AddRange(driveBrakeTable);
                Viewers[tabIndex].SetDataList(combinedTable, Table.CorneringTable, dataLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].CenterValue), formulaLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].UpperValue), formulaLegendU);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].LowerValue), formulaLegendL);
                Viewers[tabIndex].DrawGraph(dataLegend);
                ReplotData = false;
            }
            if ((ReplotFormula || !FirstPlot[tabIndex]))
            {

                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].CenterValue), formulaLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].UpperValue), formulaLegendU);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, StaticFunctions.ConstArgToViewer(TDSs[tabIndex].LowerValue), formulaLegendL);
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
            Viewers[i].NumPoints = points;
            ReplotData = true;
            RePlot();
        }

        private void FittingCheckedChanged(object sender, EventArgs e)
        {
            var i = FittingParametersCB.IndexOf(sender as CheckBox);
            if (i < MFFD.MagicFormula.MZ.PT.FittingParameters.Count)
            {
                MFFD.MagicFormula.MZ.PT.FittingParameters[i] = FittingParametersCB[i].Checked;
            }
            else
            {
                MFFD.MagicFormula.FY.FittingParameters[i- MFFD.MagicFormula.MZ.PT.FittingParameters.Count] = FittingParametersCB[i].Checked;
            }
        }
    }
}
