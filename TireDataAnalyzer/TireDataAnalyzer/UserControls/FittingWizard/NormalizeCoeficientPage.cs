using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class NormalizeCoeficientPage : FittingWizardPage
    {
        public NormalizeCoeficientPage(FittingWizardPage page)
            :base(page, "規格化パラメータ設定")
        {
            InitializeComponent();
        }

        protected override void Reload(bool back)
        {
            
            var mffx = MFFD.MagicFormula.FX;
            SA_C0.Text = mffx.NormalizeOffsetParam.SA.ToString();
            SA_C1.Text = mffx.NormalizeScaleParam.SA.ToString();
            SR_C0.Text = mffx.NormalizeOffsetParam.SR.ToString();
            SR_C1.Text = mffx.NormalizeScaleParam.SR.ToString();
            IA_C0.Text = mffx.NormalizeOffsetParam.IA.ToString();
            IA_C1.Text = mffx.NormalizeScaleParam.IA.ToString();

            FZ_C0.Text = mffx.NormalizeOffsetParam.FZ.ToString();
            FZ_C1.Text = mffx.NormalizeScaleParam.FZ.ToString();
            P_C0.Text = mffx.NormalizeOffsetParam.P.ToString();
            P_C1.Text = mffx.NormalizeScaleParam.P.ToString();
            T_C0.Text = mffx.NormalizeOffsetParam.T.ToString();
            T_C1.Text = mffx.NormalizeScaleParam.T.ToString();
            calculateMaxMinMean();
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

            var mffx = MFFD.MagicFormula.FX;
            if ( tb == SA_C0)
            {
                mffx.NormalizeOffsetParam.SA = double.Parse(SA_C0.Text);
            }
            else if (tb == SA_C1)
            {
                mffx.NormalizeScaleParam.SA = double.Parse(SA_C1.Text);
            }
            else if (tb == SR_C0)
            {
                mffx.NormalizeOffsetParam.SR = double.Parse(SR_C0.Text);
            }
            else if (tb == SR_C1)
            {
                mffx.NormalizeScaleParam.SR = double.Parse(SR_C1.Text);
            }
            else if (tb == IA_C0)
            {
                mffx.NormalizeOffsetParam.IA = double.Parse(IA_C0.Text);
            }
            else if (tb == IA_C1)
            {
                mffx.NormalizeScaleParam.IA = double.Parse(IA_C1.Text);
            }
            else if (tb == FZ_C0)
            {
                mffx.NormalizeOffsetParam.FZ = double.Parse(FZ_C0.Text);
            }
            else if (tb == FZ_C1)
            {
                mffx.NormalizeScaleParam.FZ = double.Parse(FZ_C1.Text);
            }
            else if (tb == P_C0)
            {
                mffx.NormalizeOffsetParam.P = double.Parse(P_C0.Text);
            }
            else if (tb == P_C1)
            {
                mffx.NormalizeScaleParam.P = double.Parse(P_C1.Text);
            }
            else if (tb == T_C0)
            {
                mffx.NormalizeOffsetParam.T = double.Parse(T_C0.Text);
            }
            else if (tb == T_C1)
            {
                mffx.NormalizeScaleParam.T = double.Parse(T_C1.Text);
            }
            
        }

        private void IsReal_Validated(object sender, EventArgs e)
        {
            
            hasError = false;
            var tb = (sender as TextBox);
            this.EP_NumericalInput.SetError(tb, null);
            calculateMaxMinMean();
        }

        protected override bool OnNext()
        {
            return !hasError;
        }

        private void calculateMaxMinMean()
        {
            var dataset = MFFD.IDataset.GetDataSet();
            var maxmin = dataset.MaxminSet;
            var mffx = MFFD.MagicFormula.FX;

            SA_Min.Text = maxmin.CorneringTableLimit.Min.SA.ToString("G4");
            SA_Max.Text = maxmin.CorneringTableLimit.Max.SA.ToString("G4");
            SA_Mean.Text = maxmin.CorneringTableLimit.Mean.SA.ToString("G4");
            SA_MinN.Text = ((maxmin.CorneringTableLimit.Min.SA- mffx.NormalizeOffsetParam.SA)/ mffx.NormalizeScaleParam.SA).ToString("G4");
            SA_MaxN.Text = ((maxmin.CorneringTableLimit.Max.SA - mffx.NormalizeOffsetParam.SA) / mffx.NormalizeScaleParam.SA).ToString("G4");
            SA_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.SA - mffx.NormalizeOffsetParam.SA) / mffx.NormalizeScaleParam.SA).ToString("G4");

            SR_Min.Text = maxmin.CorneringTableLimit.Min.SR.ToString("G4");
            SR_Max.Text = maxmin.CorneringTableLimit.Max.SR.ToString("G4");
            SR_Mean.Text = maxmin.CorneringTableLimit.Mean.SR.ToString("G4");
            SR_MinN.Text = ((maxmin.CorneringTableLimit.Min.SR - mffx.NormalizeOffsetParam.SR) / mffx.NormalizeScaleParam.SR).ToString("G4");
            SR_MaxN.Text = ((maxmin.CorneringTableLimit.Max.SR - mffx.NormalizeOffsetParam.SR) / mffx.NormalizeScaleParam.SR).ToString("G4");
            SR_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.SR - mffx.NormalizeOffsetParam.SR) / mffx.NormalizeScaleParam.SR).ToString("G4");

            IA_Min.Text = maxmin.CorneringTableLimit.Min.IA.ToString("G4");
            IA_Max.Text = maxmin.CorneringTableLimit.Max.IA.ToString("G4");
            IA_Mean.Text = maxmin.CorneringTableLimit.Mean.IA.ToString("G4");
            IA_MinN.Text = ((maxmin.CorneringTableLimit.Min.IA - mffx.NormalizeOffsetParam.IA) / mffx.NormalizeScaleParam.IA).ToString("G4");
            IA_MaxN.Text = ((maxmin.CorneringTableLimit.Max.IA - mffx.NormalizeOffsetParam.IA) / mffx.NormalizeScaleParam.IA).ToString("G4");
            IA_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.IA - mffx.NormalizeOffsetParam.IA) / mffx.NormalizeScaleParam.IA).ToString("G4");

            FZ_Min.Text = maxmin.CorneringTableLimit.Min.FZ.ToString("G4");
            FZ_Max.Text = maxmin.CorneringTableLimit.Max.FZ.ToString("G4");
            FZ_Mean.Text = maxmin.CorneringTableLimit.Mean.FZ.ToString("G4");
            FZ_MinN.Text = ((maxmin.CorneringTableLimit.Min.FZ - mffx.NormalizeOffsetParam.FZ) / mffx.NormalizeScaleParam.FZ).ToString("G4");
            FZ_MaxN.Text = ((maxmin.CorneringTableLimit.Max.FZ - mffx.NormalizeOffsetParam.FZ) / mffx.NormalizeScaleParam.FZ).ToString("G4");
            FZ_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.FZ - mffx.NormalizeOffsetParam.FZ) / mffx.NormalizeScaleParam.FZ).ToString("G4");

            P_Min.Text = maxmin.CorneringTableLimit.Min.P.ToString("G4");
            P_Max.Text = maxmin.CorneringTableLimit.Max.P.ToString("G4");
            P_Mean.Text = maxmin.CorneringTableLimit.Mean.P.ToString("G4");
            P_MinN.Text = ((maxmin.CorneringTableLimit.Min.P - mffx.NormalizeOffsetParam.P) / mffx.NormalizeScaleParam.P).ToString("G4");
            P_MaxN.Text = ((maxmin.CorneringTableLimit.Max.P - mffx.NormalizeOffsetParam.P) / mffx.NormalizeScaleParam.P).ToString("G4");
            P_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.P - mffx.NormalizeOffsetParam.P) / mffx.NormalizeScaleParam.P).ToString("G4");

            T_Min.Text = maxmin.CorneringTableLimit.Min.TST.ToString("G4");
            T_Max.Text = maxmin.CorneringTableLimit.Max.TST.ToString("G4");
            T_Mean.Text = maxmin.CorneringTableLimit.Mean.TST.ToString("G4");
            T_MinN.Text = ((maxmin.CorneringTableLimit.Min.TST - mffx.NormalizeOffsetParam.T) / mffx.NormalizeScaleParam.T).ToString("G4");
            T_MaxN.Text = ((maxmin.CorneringTableLimit.Max.TST - mffx.NormalizeOffsetParam.T) / mffx.NormalizeScaleParam.T).ToString("G4");
            T_MeanN.Text = ((maxmin.CorneringTableLimit.Mean.TST - mffx.NormalizeOffsetParam.T) / mffx.NormalizeScaleParam.T).ToString("G4");
        }

        private void NormalizeCoeficientPage_Load(object sender, EventArgs e)
        {
            
        }
    }
}
