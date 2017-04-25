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
    public partial class CombinedSlipPage : FittingWizardPage
    {
        List<TextBox> ParameterTBX = new List<TextBox>();
        List<TextBox> ParameterTBY = new List<TextBox>();
        List<CheckBox> FittingParametersCBX = new List<CheckBox>();
        List<CheckBox> FittingParametersCBY = new List<CheckBox>();
        List<SimpleTireDataSelector> TDSs = new List<SimpleTireDataSelector>();
        List<MagicFormula_TexEquation> Equations = new List<MagicFormula_TexEquation>();
        List<TireDataViewer> Viewers = new List<TireDataViewer>();
        List<bool> FirstPlot = new List<bool>();
        List<int> NumPointList = new List<int>();
        bool ReplotData = false;
        bool ReplotFormula = false;
        bool stopReplot = false;
        string dataLegend = "data";
        string formulaLegend = "CenterLine";
        string formulaLegendU = "UpperLine";
        string formulaLegendL = "LowerLine";

        public CombinedSlipPage(FittingWizardPage page)
            :base(page,"コンバインドスリップパラメータ")
        {
            InitializeComponent();
            ParameterTBX.Add(bx0TB);
            ParameterTBX.Add(bx1TB);
            ParameterTBX.Add(bx2TB);
            ParameterTBX.Add(bx3TB);
            ParameterTBX.Add(bx4TB);
            ParameterTBX.Add(bx5TB);
            ParameterTBX.Add(bx6TB);

            ParameterTBY.Add(by0TB);
            ParameterTBY.Add(by1TB);
            ParameterTBY.Add(by2TB);
            ParameterTBY.Add(by3TB);
            ParameterTBY.Add(by4TB);
            ParameterTBY.Add(by5TB);
            ParameterTBY.Add(by6TB);
            ParameterTBY.Add(by7TB);
            ParameterTBY.Add(by8TB);
            ParameterTBY.Add(by9TB);
            ParameterTBY.Add(by10TB);
            ParameterTBY.Add(by11TB);
            ParameterTBY.Add(by12TB);
            ParameterTBY.Add(by13TB);
            ParameterTBY.Add(by14TB);

            FittingParametersCBY.Add(checkBox0);
            FittingParametersCBY.Add(checkBox1);
            FittingParametersCBY.Add(checkBox2);
            FittingParametersCBY.Add(checkBox3);
            FittingParametersCBY.Add(checkBox4);
            FittingParametersCBY.Add(checkBox5);
            FittingParametersCBY.Add(checkBox6);
            FittingParametersCBY.Add(checkBox7);
            FittingParametersCBY.Add(checkBox8);
            FittingParametersCBY.Add(checkBox9);
            FittingParametersCBY.Add(checkBox10);
            FittingParametersCBY.Add(checkBox11);
            FittingParametersCBY.Add(checkBox12);
            FittingParametersCBY.Add(checkBox13);
            FittingParametersCBY.Add(checkBox14);
            FittingParametersCBX.Add(checkBox15);
            FittingParametersCBX.Add(checkBox16);
            FittingParametersCBX.Add(checkBox17);
            FittingParametersCBX.Add(checkBox18);
            FittingParametersCBX.Add(checkBox19);
            FittingParametersCBX.Add(checkBox20);
            FittingParametersCBX.Add(checkBox21);

            TDSs.Add(simpleTireDataSelector1);
            TDSs.Add(simpleTireDataSelector2);
            TDSs.Add(simpleTireDataSelector3);
            TDSs.Add(simpleTireDataSelector4);
            TDSs.Add(simpleTireDataSelector5);

            Viewers.Add(TireDataViewer0);
            Viewers.Add(TireDataViewer1);
            Viewers.Add(TireDataViewer2);
            Viewers.Add(TireDataViewer3);
            Viewers.Add(TireDataViewer4);

            Viewers[0].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[1].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[2].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[3].Axis = TireDataViewer.EnumAxis.MagicFormula;
            Viewers[4].Axis = TireDataViewer.EnumAxis.MagicFormula;

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
            foreach (var tb in ParameterTBX)
            {
                tb.KeyDown += TextBox_KeyDown;
                tb.Validated += IsReal_Validated;
                tb.Validating += IsReal_Validating;
                tb.Enter += CFX_Enter;
            }
            foreach (var tb in ParameterTBY)
            {
                tb.KeyDown += TextBox_KeyDown;
                tb.Validated += IsReal_Validated;
                tb.Validating += IsReal_Validating;
                tb.Enter += CFY_Enter;
            }
            foreach (var cb in FittingParametersCBX)
            {
                cb.CheckedChanged += FittingCheckedChanged;
                cb.Enter += CFX_Enter;
            }
            foreach (var cb in FittingParametersCBY)
            {
                cb.CheckedChanged += FittingCheckedChanged;
                cb.Enter += CFY_Enter;
            }
            for (int i = 0; i < TDSs.Count; ++i)
            {
                TDSs[i].MFFD = MFFD;
                TDSs[i].ValueChanged += SelectorValueChanged;

                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint, dataLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegend);
                Viewers[i].SetDataListRefMF(dataLegend, formulaLegend);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendU);
                Viewers[i].SetChartType(System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine, formulaLegendL);
                Viewers[i].SetLineWidth(5, formulaLegend);
                Viewers[i].GraphSample = 2500;
            }

            Equations.Add(magicFormula_TexEquation0);
            Equations.Add(magicFormula_TexEquation1);
            Equations.Add(magicFormula_TexEquation2);
            Equations.Add(magicFormula_TexEquation3);
            Equations.Add(magicFormula_TexEquation4);
        }

        private void CFY_Enter(object sender, EventArgs e)
        {
            int i = -1;
            if (sender is TextBox)
            {
                i = ParameterTBY.IndexOf(sender as TextBox);
            }
            else
            {
                i = FittingParametersCBY.IndexOf(sender as CheckBox);
            }
            foreach (var mf in Equations)
            {
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.CFY;
                mf.Highlight(i);
            }
        }
        private void CFX_Enter(object sender, EventArgs e)
        {
            int i = -1;
            if (sender is TextBox)
            {
                i = ParameterTBX.IndexOf(sender as TextBox);
            }
            else
            {
                i = FittingParametersCBX.IndexOf(sender as CheckBox);
            }
            foreach (var mf in Equations)
            {
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.CFX;
                mf.Highlight(i);
            }
        }
        private void CombinedSlipPage_Load(object sender, EventArgs e)
        {
            foreach (var mf in Equations)
            {
                (Parent as Form).ResizeEnd += mf.Control_Resize;
                mf.Type = MagicFormula_TexEquation.MagicFormulaType.CFY;
            }

            Viewers[0].SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            Viewers[1].SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            Viewers[2].SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            Viewers[3].SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
            Viewers[4].SetAxis(MagicFormulaInputVariables.FY, MagicFormulaOutputVariables.FX);
        }

        protected override void Reload(bool back)
        {
            
            stopReplot = true;
            var ParamsX = MFFD.MagicFormula.CFX.Parameters;
            for (int i = 0; i < ParameterTBX.Count(); ++i)
            {
                ParameterTBX[i].Text = ParamsX[i].ToString();
                FittingParametersCBX[i].Checked = MFFD.MagicFormula.CFX.FittingParameters[i];
            }
            var ParamsY = MFFD.MagicFormula.CFY.Parameters;
            for (int i = 0; i < ParameterTBY.Count(); ++i)
            {
                ParameterTBY[i].Text = ParamsY[i].ToString();
                FittingParametersCBY[i].Checked = MFFD.MagicFormula.CFY.FittingParameters[i];
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
            stopReplot = false;
            ReplotFormula = true;
            ReplotData = true;
            foreach (var mf in Equations)
            {
                mf.Control_Resize(mf, new EventArgs());
                mf.Size = new Size(mf.Size.Width, 100);
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
                NextButton.Enabled = false;
                PreviousButton.Enabled = false;
                return;
            }
            
            int i = ParameterTBX.IndexOf((TextBox)sender);
            if(i>=0)
            {
                var Params = MFFD.MagicFormula.CFX.Parameters;
                Params[i] = double.Parse(((TextBox)sender).Text);
            }
            else
            {
                i = ParameterTBY.IndexOf((TextBox)sender);
                if (i < 0)
                    return;

                var Params = MFFD.MagicFormula.CFY.Parameters;
                Params[i] = double.Parse(((TextBox)sender).Text);
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

                Viewers[tabIndex].SetDataList(corneringTable, Table.CorneringTable, dataLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].CenterValue, formulaLegend);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].UpperValue, formulaLegendU);
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, TDSs[tabIndex].LowerValue, formulaLegendL);

                Viewers[tabIndex].DrawGraph(dataLegend);
                ReplotData = false;
            }
            if ((ReplotFormula || !FirstPlot[tabIndex]))
            {
                var formula = TDSs[tabIndex].CenterValue;
                Viewers[tabIndex].SetMagicFormula(MFFD.MagicFormula, formula , formulaLegend);
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
            Viewers[i].NumPoints = points;
            ReplotData = true;
            RePlot();
        }

        private void FittingCheckedChanged(object sender, EventArgs e)
        {
            var i = FittingParametersCBX.IndexOf(sender as CheckBox);
            if(i != -1)
            {
                MFFD.MagicFormula.CFX.FittingParameters[i] = FittingParametersCBX[i].Checked;
                return;
            }
            i = FittingParametersCBY.IndexOf(sender as CheckBox);
            if (i != -1)
            {
                MFFD.MagicFormula.CFY.FittingParameters[i] = FittingParametersCBY[i].Checked;
                return;
            }

            

        }
    }
}
