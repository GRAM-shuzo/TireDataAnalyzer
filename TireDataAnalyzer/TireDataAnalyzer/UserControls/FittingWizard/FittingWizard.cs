using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class FittingWizard : Form
    {
        private FittingWizard()
        {
            InitializeComponent();
        }

        public FittingWizard(MagicFormulaFittingDelegate magicFormula, string name)
        {
            InitializeComponent();
            MagicFormulaFD = magicFormula;
            TireName = name;
        }
        string TireName;
        public MagicFormulaFittingDelegate MagicFormulaFD
        {
            get; private set;
        }

        private void FittingWizard_Load(object sender, EventArgs e)
        {
            MFInitialValuePage one = new MFInitialValuePage(MagicFormulaFD);
            NormalizeCoeficientPage two = new NormalizeCoeficientPage(one);
            PureCorneringPage three = new PureCorneringPage(two);
            PureDriveBrakePage four = new PureDriveBrakePage(three);
            CombinedSlipPage five = new CombinedSlipPage(four);
            SelfAligningTorquePage six = new SelfAligningTorquePage(five);
            //TransientPage sixp5 = new TransientPage(six);
            SolverSettingPage seven = new SolverSettingPage(six);
            ProgressPage eight = new ProgressPage(seven);
            FinishedPage nine = new FinishedPage(eight);
            this.Controls.Add(one);
        }
    }
}
