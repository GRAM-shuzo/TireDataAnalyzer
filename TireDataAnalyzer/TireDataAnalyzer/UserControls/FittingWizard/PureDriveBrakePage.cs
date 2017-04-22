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
        bool ReplotData = false;
        bool ReplotFormula = false;
        bool stopReplot = false;
        
        string dataLegend = "data";
        string formulaLegend = "MagicFormula";
        string EUpperLegend = "EUpperLine";
        string ELowerLegend = "ELowerLine";

        public PureDriveBrakePage(FittingWizardPage previous)
            : base(previous, "PureSlip前後力パラメータ")
        {
            InitializeComponent();
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

            }
            foreach( var cb in FittingParametersCB)
            {
                cb.CheckedChanged += FittingCheckedChanged;
                cb.Enter += mf_Enter;
            }

            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].MFFD = MFFD;
                TDSs[i].ValueChanged += SelectorValueChanged;

                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint, dataLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegend);
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
            }
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
                    Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].CenterValue, formulaLegend);

                    Viewers[tabIndex].DrawGraph(dataLegend);
                    ReplotData = false;
                }
                if ((ReplotFormula || !FirstPlot[tabIndex]))
                {

                    Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].CenterValue, formulaLegend);
                    Viewers[tabIndex].DrawGraph(formulaLegend);
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
