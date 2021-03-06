﻿using System;
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
    public partial class PureDriveBrakePage : FittingWizardPage
    {
        List<TextBox> ParameterTB = new List<TextBox>();
        List<CheckBox> FittingParametersCB = new List<CheckBox>();
        List<SimpleTireDataSelector> TDSs = new List<SimpleTireDataSelector>();
        List<TireDataViewer> Viewers = new List<TireDataViewer>();
        List<bool> FirstPlot = new List<bool>();
        List<TireDataViewer.XY> EList = new List<TireDataViewer.XY>();
        List<MagicFormula_TexEquation> Equations = new List<MagicFormula_TexEquation>();
        List<int> NumPointList = new List<int>();
        List<Label> Advises = new List<Label>();
        bool ReplotData = false;
        bool ReplotFormula = false;
        bool stopReplot = false;
        
        string dataLegend = "data";
        string formulaLegend = "CenterLine";
        string formulaLegendU = "UpperLine";
        string formulaLegendL = "LowerLine";
        string EUpperLegend = "EUpperLine";
        string ELowerLegend = "ELowerLine";

        public PureDriveBrakePage(FittingWizardPage previous)
            : base(previous, "PureSlip前後力パラメータ")
        {
            InitializeComponent();

            Advises.Add(AdviseText0);
            Advises.Add(AdviseText1);
            Advises.Add(AdviseText2);
            Advises.Add(AdviseText3);
            Advises.Add(AdviseText4);
            Advises.Add(AdviseText5);

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
            TDSs.Add(simpleTireDataSelector1);
            TDSs.Add(simpleTireDataSelector2);
            TDSs.Add(simpleTireDataSelector3);
            TDSs.Add(simpleTireDataSelector4);
            TDSs.Add(simpleTireDataSelector5);
            TDSs.Add(simpleTireDataSelector6);

            Viewers.Add(TireDataViewer0);
            Viewers.Add(TireDataViewer1);
            Viewers.Add(TireDataViewer2);
            Viewers.Add(TireDataViewer3);
            Viewers.Add(TireDataViewer4);
            Viewers.Add(TireDataViewer5);

            Viewers[0].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[1].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[2].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[3].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[4].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[5].Axis = TireDataViewer.EnumAxis.MagicFormula;

            FirstPlot.Add(false);
            FirstPlot.Add(false);
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
                tb.Enter += mf_Enter;
                tb.Enter += Tb_Enter;

            }
            foreach( var cb in FittingParametersCB)
            {
                cb.CheckedChanged += FittingCheckedChanged;
                cb.Enter += mf_Enter;
                cb.Enter += Tb_Enter;
            }

            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].SASREnable = false;
                TDSs[i].MFFD = MFFD;
                TDSs[i].ValueChanged += SelectorValueChanged;

                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint, dataLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendU);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendL);
                Viewers[i].SetDataListRefMF(dataLegend, formulaLegend);
                Viewers[i].SetLineWidth(5, formulaLegend);
            }
            Viewers[5].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, EUpperLegend);
            Viewers[5].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, ELowerLegend);
            Viewers[5].SetLineWidth(5, EUpperLegend);
            Viewers[5].SetLineWidth(5, ELowerLegend);

            Equations.Add(magicFormula_TexEquation0);
            Equations.Add(magicFormula_TexEquation1);
            Equations.Add(magicFormula_TexEquation2);
            Equations.Add(magicFormula_TexEquation3);
            Equations.Add(magicFormula_TexEquation4);
            Equations.Add(magicFormula_TexEquation5);

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


        private void SetAdviceText(int i)
        {

            if (i < 0 || i >= ParameterTB.Count)
            {
                foreach (var l in Advises)
                {
                    l.Text = @"PureFxモデルへのフィッティングを行います。
入力値：スリップ率(SR)、輪荷重(FZ)、キャンバ角(IA)、空気圧(P)、タイヤ温度(T)
出力値:前後力(Fx)
決める必要のあるパラメータはa0～a18の19変数です。(のちに最適化されます)
中間パラメータD、BCD、C、E、Shは曲線の特性を表し
Dは最大横力、BCDはSR + Sh = 0での傾き、C、Eはカーブの形状を決めます。";
                }
                return;
            }

            string[] text = new string[24];
            text[0] = "Cはカーブ形状を決めます。通常は1<C<1.65の値をとります。";
            text[17] = @"Shはカーブを水平方向にオフセットします。走行抵抗によりオフセットされます。";
            text[18] = @"Shはカーブを水平方向にオフセットします。走行抵抗によりオフセットされます。
a18は走行抵抗の荷重依存成分を決定します。（転がり抵抗係数相当）";

            text[1] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a1は摩擦係数μの定数成分を決定します。";
            text[2] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a2は摩擦係数μの輪荷重依存成分を決定します。
輪荷重依存度a2は通常負の値をとります。(輪荷重が多いほど摩擦係数が減る)";
            text[3] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a3は摩擦係数μのキャンバ依存成分を決定します。
キャンバ依存度a3は通常負の値をとります。";
            text[4] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a4は摩擦係数μの圧力依存成分を決定します。
圧力依存性はa5と合わせて2次で近似します";
            text[5] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a5は摩擦係数μの圧力依存成分を決定します。
圧力依存性はa4と合わせて2次で近似します";
            text[6] = @"Dはカーブの最大値を決定します。
Dは輪荷重FZと摩擦係数との積で表せます。
a6は摩擦係数μの温度依存成分を決定します。
温度依存性は線形近似です。
温度依存性を加味しない場合はこの係数は0に固定してください";

            text[7] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
a7は傾きのゲイン(FZの何倍か)を決定します。";
            text[8] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
a8は傾きに対する輪荷重依存性を決定します。";
            text[9] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
a9は傾きに対する輪荷重依存性を決定します。
通常a9は負の値をとります。";
            text[10] = @"B*C*Dは SA+Sh=0 でのカーブの傾きを表します。
a10は傾きに対する圧力依存成分を決定します。
圧力依存性はa11と合わせて2次で近似します";
            text[11] = @"B* C*Dは SA + Sh = 0 でのカーブの傾きを表します。
a11は傾きに対する圧力依存成分を決定します。
圧力依存性はa10と合わせて2次で近似します";
            text[12] = @"B* C*Dは SA + Sh = 0 でのカーブの傾きを表します。
a12は傾きに対する温度依存成分を決定します。
温度依存性は線形近似です。
温度依存性を加味しない場合はこの係数は0に固定してください";



            text[13] = @"Eはカーブの形状を決定します。
Eは-(1+0.5C^2) < E < 1を満たす必要があり、
表示されるすべての点がアッパーとロアーの間に収まっていなければなりません。";
            text[14] = text[13];
            text[15] = text[13];
            text[16] = text[13];

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

        private void PureDriveBrakePage_Load(object sender, EventArgs e)
        {
            foreach (var mf in Equations)
            {
                (Parent as Form).ResizeEnd += mf.Control_Resize;
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.FX;
            }

            Viewers[0].SetAxis(MagicFormulaInputVariables.SR, MagicFormulaOutputVariables.FX);
            Viewers[1].SetAxis(MagicFormulaInputVariables.FZ, MagicFormulaOutputVariables.FX_D);
            Viewers[2].SetAxis(MagicFormulaInputVariables.FZ, MagicFormulaOutputVariables.FX_BCD);
            Viewers[3].SetAxis(MagicFormulaInputVariables.P, MagicFormulaOutputVariables.FX_D);
            Viewers[4].SetAxis(MagicFormulaInputVariables.P, MagicFormulaOutputVariables.FX_BCD);
            Viewers[5].SetAxis(MagicFormulaInputVariables.SR, MagicFormulaOutputVariables.FX_E);
        }

        private void mf_Enter(object sender, EventArgs e)
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
        }

        protected override void Reload(bool back)
        {
            stopReplot = true;
            var Params = MFFD.MagicFormula.FX.Parameters;
            for (int i = 0; i < ParameterTB.Count(); ++i)
            {
                ParameterTB[i].Text = Params[i].ToString();
                FittingParametersCB[i].Checked = MFFD.MagicFormula.FX.FittingParameters[i];
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
            comboBox4.SelectedIndex = 4;
            comboBox5.SelectedIndex = 4;
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

            var Params = MFFD.MagicFormula.FX.Parameters;
            int i = ParameterTB.IndexOf((TextBox)sender);
            Params[i] = double.Parse(((TextBox)sender).Text);


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
            if (tabIndex != 5)
            {
                if (ReplotData || !FirstPlot[tabIndex])
                {
                    var driveBrakeTable = TDSs[tabIndex].SelectedData().GetDataSet().DriveBrakeTable;
                    Viewers[tabIndex].SetDataList(driveBrakeTable, Table.DriveBrakeTable, dataLegend);
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
            else
            {
                //-(1+0.5c^2) < E < 1
                var driveBrakeTable = TDSs[tabIndex].SelectedData().GetDataSet().DriveBrakeTable;
                var maxmin = TDSs[tabIndex].SelectedData().GetDataSet().MaxminSet.DriveBrakeTableLimit;
                EList.Clear();
                var Eupper = new List<TireDataViewer.XY>(driveBrakeTable.Count);
                var Elower = new List<TireDataViewer.XY>(driveBrakeTable.Count);
                Eupper.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Max), 1));
                Eupper.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Min), 1));

                var C = MFFD.MagicFormula.GetVariables(MagicFormulaOutputVariables.FY_C, new MagicFormulaArguments(maxmin.Max));
                Elower.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Max), -(1 + 0.5 * C * C)));
                Elower.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Min), -(1 + 0.5 * C * C)));

                foreach (var data in driveBrakeTable)
                {
                    var inputData = new MagicFormulaArguments(data);
                    var E = MFFD.MagicFormula.GetVariables(MagicFormulaOutputVariables.FY_E, inputData);

                    EList.Add(new TireDataViewer.XY(inputData, E));

                }
                Viewers[tabIndex].SetNonManagedData(EList, dataLegend);
                Viewers[tabIndex].DrawGraph(dataLegend);

                Viewers[tabIndex].SetNonManagedData(Eupper, EUpperLegend);
                Viewers[tabIndex].DrawGraph(EUpperLegend);
                Viewers[tabIndex].SetNonManagedData(Elower, ELowerLegend);
                Viewers[tabIndex].DrawGraph(ELowerLegend);
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
            MFFD.MagicFormula.FX.FittingParameters[i] = FittingParametersCB[i].Checked;
        }
    }
}
