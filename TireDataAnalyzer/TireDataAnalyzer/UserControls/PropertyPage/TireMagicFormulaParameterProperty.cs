using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TireDataAnalyzer.UserControls.PropertyPage
{
    public partial class TireMagicFormulaParameterProperty : PropertyPage
    {
        public TireMagicFormulaParameterProperty(ProjectTree.Node_MagicFormula nmf)
            :base(nmf)
        {
            InitializeComponent();
        }
    }
}
