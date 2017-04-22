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

namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class SelfAligningTorquePage : FittingWizardPage
    {
        List<TextBox> ParameterTB = new List<TextBox>();
        List<SimpleTireDataSelector> TDSs = new List<SimpleTireDataSelector>();
        List<TireDataViewer> Viewers = new List<TireDataViewer>();
        List<bool> FirstPlot = new List<bool>();
        List<TireDataViewer.XY> EList = new List<TireDataViewer.XY>();
        List<int> NumPointList = new List<int>();
        bool ReplotData = false;
        bool ReplotFormula = false;
        bool stopReplot = false;
        string dataLegend = "data";
        string formulaLegend = "MagicFormula";
        string EUpperLegend = "EUpperLine";
        string ELowerLegend = "ELowerLine";

        public SelfAligningTorquePage(FittingWizardPage page)
            : base(page, "Pureセルフアライニングトルクパラメータ")
        {
            InitializeComponent();
            this.Load += SelfAligningTorquePage_Load;
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

            TDSs.Add(simpleTireDataSelector1);
            TDSs.Add(simpleTireDataSelector2);
            TDSs.Add(simpleTireDataSelector3);
            TDSs.Add(simpleTireDataSelector5);

            Viewers.Add(TireDataViewer0);
            Viewers.Add(TireDataViewer1);
            Viewers.Add(TireDataViewer2);
            Viewers.Add(TireDataViewer4);

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
                tb.Validated += IsReal_Validated;
                tb.Validating += IsReal_Validating;
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

        }

        private void SelfAligningTorquePage_Load(object sender, EventArgs e)
        {
            Viewers[0].SetAxis(MagicFormulaInputVariables.SA, MagicFormulaOutputVariables.MZ);
            Viewers[1].SetAxis(MagicFormulaInputVariables.FZ, MagicFormulaOutputVariables.MZ_D);
            Viewers[2].SetAxis(MagicFormulaInputVariables.FZ, MagicFormulaOutputVariables.FY_BCD);
            Viewers[3].SetAxis(MagicFormulaInputVariables.P, MagicFormulaOutputVariables.FY_D);
        }

        protected override void Reload(bool back)
        {
            stopReplot = true;
            var Params = MFFD.MagicFormula.FY.Parameters;
            for (int i = 0; i < ParameterTB.Count(); ++i)
            {
                ParameterTB[i].Text = Params[i].ToString();
            }
            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].MFFD = MFFD;
                FirstPlot[i] = false;
            }
            comboBox0.SelectedIndex = 4;
            comboBox1.SelectedIndex = 4;
            comboBox2.SelectedIndex = 4;
            comboBox4.SelectedIndex = 4;
            stopReplot = false;
            ReplotFormula = true;
            ReplotData = true;

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

            var Params = MFFD.MagicFormula.FY.Parameters;
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
                    var corneringTable = TDSs[tabIndex].SelectedData().GetDataSet().CorneringTable;
                    Viewers[tabIndex].SetDataList(corneringTable, Table.CorneringTable, dataLegend);
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
                var corneringTable = TDSs[tabIndex].SelectedData().GetDataSet().CorneringTable;
                var maxmin = TDSs[tabIndex].SelectedData().GetDataSet().MaxminSet.CorneringTableLimit;
                EList.Clear();
                var Eupper = new List<TireDataViewer.XY>(corneringTable.Count);
                var Elower = new List<TireDataViewer.XY>(corneringTable.Count);
                Eupper.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Max), 1));
                Eupper.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Min), 1));

                var C = MFFD.MagicFormula.GetVariables(MagicFormulaOutputVariables.FY_C, new MagicFormulaArguments(maxmin.Max));
                Elower.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Max), -(1 + 0.5 * C * C)));
                Elower.Add(new TireDataViewer.XY(new MagicFormulaArguments(maxmin.Min), -(1 + 0.5 * C * C)));

                foreach (var data in corneringTable)
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
    }
}
