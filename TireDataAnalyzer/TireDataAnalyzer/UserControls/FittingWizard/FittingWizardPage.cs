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
    public partial class FittingWizardPage : UserControl
    {
        public FittingWizardPage(MagicFormulaFittingDelegate formula, string name)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            MFFD = formula;
            PageName = name;
        }

        public MagicFormulaFittingDelegate MFFD { get; private set; }

        public FittingWizardPage(FittingWizardPage previous, string name)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            PreviousPage = previous;
            previous.NextPage = this;
            previous.NextButton.Text = "次へ";
            MFFD = previous.MFFD;
            PageName = name;
        }

        string PageName;

        private void FittingWizardPage_Load(object sender, EventArgs e)
        {
            if(PreviousPage == null)
            {
                PreviousButton.Hide();
            }
            var parent = Parent as FittingWizard;
            if (parent != null) parent.Text = "ウィザード - " + PageName;
        }

        FittingWizardPage PreviousPage = null;
        FittingWizardPage NextPage = null;

        public FittingWizardPage()
        {
            InitializeComponent();
        }

        public virtual bool Finish()
        {
            return true;
        }

        public virtual bool Cancel()
        {
            return true;
        }

        protected virtual bool OnNext()
        {
            return true;
        }

        protected virtual bool OnPrevious()
        {
            return true;
        }

        protected void Previous()
        {
            PreviousButton_Click(null, null);
        }

        protected void Next()
        {
            NextButton_Click(null, null);
        }

        


        protected virtual void Reload(bool back)
        {

        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            PreviousButton.Enabled = false;
            if (!OnPrevious()) return;
            
            var parent = this.Parent;
            parent.Controls.Remove(this);
            parent.Controls.Add(PreviousPage);
            PreviousPage.Reload(true);
            PreviousPage.NextButton.Enabled = true;
            PreviousPage.PreviousButton.Enabled = true;
            parent.Text = "ウィザード - " + PreviousPage.PageName;
            PreviousPage.Update();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextButton.Enabled = false;
            if(NextPage != null)
            {
                if (!OnNext()) return;

                var parent = this.Parent as FittingWizard;
                parent.Controls.Remove(this);
                parent.Controls.Add(NextPage);
                NextPage.Reload(false);
                NextPage.NextButton.Enabled = true;
                NextPage.PreviousButton.Enabled = true;
                parent.Text = "ウィザード - " + NextPage.PageName;
                NextPage.Update();
            }
            else
            {
                if(Finish())
                {
                    (Parent as FittingWizard).DialogResult = DialogResult.OK;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            if(Cancel())
            {
                (Parent as FittingWizard).DialogResult = DialogResult.Cancel;
            }
        }

    }
}
