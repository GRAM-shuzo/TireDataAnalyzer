using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicFormulaFittingSolver;

namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class SolverSettingPage : FittingWizardPage
    {
        public SolverSettingPage(FittingWizardPage page)
            :base(page,"ソルバー設定")
        {
            InitializeComponent();

            


        }
        bool init = true;
        bool hasError = false;
        protected override void Reload(bool back)
        {
            init = true;
            PureFXCB.Checked = MFFD.FittingPFX;
            CombinedFXCB.Checked = MFFD.FittingCFX;
            PureFYCB.Checked = MFFD.FittingPFY;
            CombinedFYCB.Checked = MFFD.FittingCFY;
            SelfAligningTorqueCB.Checked = MFFD.FittingSAT;
            MaxEvalTB.Text = "100";
            XtolTB.Text = "0.1";
            MaxDataTB.Text = "50000";
            NloptAlgorithmCB.Items.Clear();
            foreach(NLOptFittingSolver.algorithm item in Enum.GetValues(typeof(NLOptFittingSolver.algorithm)))
            {
                NloptAlgorithmCB.Items.Add(item);
            }
            NloptAlgorithmCB.SelectedItem = NLOptFittingSolver.algorithm.LN_COBYLA;
            NoFittingRB.Checked = false;
            NLoptRB.Checked = false;
            LMMethodRB.Checked = false;
            init = false;
            if (MFFD.Solver == null)
            {
                NoFittingRB.Checked = true;
                MFFD.Solver = new NoFitting();
            }
            else if (MFFD.Solver is NoFitting)
                NoFittingRB.Checked = true;
            else if (MFFD.Solver is NLOptFittingSolver)
            {
                NLoptRB.Checked = true;
                NloptAlgorithmCB.SelectedItem = (MFFD.Solver as NLOptFittingSolver).Algorithm;
                MaxEvalTB.Text = (MFFD.Solver as NLOptFittingSolver).Maxeval.ToString();
                XtolTB.Text = (MFFD.Solver as NLOptFittingSolver).Xtol.ToString();
            }
            else if (MFFD.Solver is LMFittingSolver)
            {
                LMMethodRB.Checked = true;
                MaxEvalTB.Text = (MFFD.Solver as LMFittingSolver).Maxeval.ToString();
                XtolTB.Text = (MFFD.Solver as LMFittingSolver).Xtol.ToString();
                MaxDataTB.Text = (MFFD.Solver as LMFittingSolver).MaxDataUsage.ToString();
            }
                
            

        }
        private void FormulaCheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            MFFD.FittingPFX = PureFXCB.Checked;
            MFFD.FittingCFX = CombinedFXCB.Checked;
            MFFD.FittingPFY = PureFYCB.Checked;
            MFFD.FittingCFY = CombinedFYCB.Checked;
            MFFD.FittingSAT = SelfAligningTorqueCB.Checked;
        }

        private void FittingSolver_CheckedChanged(object sender, EventArgs e)
        {
            if (init) return;
            if (NoFittingRB.Checked)
            {
                MFFD.Solver = new NoFitting();
                MethodDescription.Text = "最適化は行いません";
                NloptAlgorithmCB.Enabled = false;
            }   
            if (NLoptRB.Checked)
            {
                var solver = new MagicFormulaFittingSolver.NLOptFittingSolver();
                solver.Maxeval = int.Parse(MaxEvalTB.Text);
                solver.Xtol = double.Parse(XtolTB.Text);
                solver.Algorithm = (NloptAlgorithmCB.SelectedItem as NLOptFittingSolver.algorithm?).Value;
                MFFD.Solver = solver;
                NloptAlgorithmCB.Enabled = true;
                MethodDescription.Text = "最適化ライブラリのNLOptを使用します。最適化ソルバは下部のコンボボックスから選択してください";
            }
            if(LMMethodRB.Checked)
            {
                var solver = new MagicFormulaFittingSolver.LMFittingSolver();
                solver.Maxeval = int.Parse(MaxEvalTB.Text);
                solver.Xtol = double.Parse(XtolTB.Text);
                solver.MaxDataUsage = int.Parse(MaxDataTB.Text);
                MFFD.Solver = solver;
                NloptAlgorithmCB.Enabled = false;
                MethodDescription.Text = "非線形最小二乗法を使用します。制約条件にはペナルティ法を使用します";
            }
               
        }

        protected override bool OnNext()
        {
            if (hasError)
            {
                NextButton.Enabled = true;
                return false;
            }
            FittingSolver_CheckedChanged(this, new EventArgs());
            if (!NoFittingRB.Checked)
            {
                DialogResult result = MessageBox.Show("フィッティングを実行しますか？",
                    "質問",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Cancel)
                {
                    NextButton.Enabled = true;
                    return false;
                }
                result = MessageBox.Show("現在の値のコピーを作成しますか？",
                   "質問",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    var list = ProjectManager.ProjectNode.GetMagicFormula();
                    bool found = false;
                    foreach(var node in list)
                    {
                        if(node.MFFD.ID == MFFD.ID)
                        {
                            var newNmf = ProjectManager.CopyProjectTree(node, node.Parent);
                            ProjectManager.ResetCopyFlag(newNmf);
                            newNmf.Name = newNmf.Name + "(コピー)";
                            (newNmf as ProjectTree.Node_MagicFormula).MFFD.ID = Guid.NewGuid();
                            found = true;
                        }
                    }
                    if(found)
                    {
                        MainWindow.Instance.ResetTreeView();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("コピーできませんでした");
                        NextButton.Enabled = true;
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
