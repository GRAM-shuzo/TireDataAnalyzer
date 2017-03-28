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
    public partial class FinishedPage : FittingWizardPage
    {
        public FinishedPage(FittingWizardPage page)
            :base(page, "完了")
        {
            InitializeComponent();
        }
    }
}
