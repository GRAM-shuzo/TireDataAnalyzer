using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls.FittingWizard
{
    public partial class ProgressPage : FittingWizardPage
    {
        public ProgressPage(FittingWizardPage page)
            : base(page,"フィッティング中")
        {
            InitializeComponent();
        }

        private CancellationTokenSource CancellationTokenSource;
        Progress<ProgressNotification> Progress;

        bool notConfirm = false;
        TireMagicFormula TMF_Save;


        protected async override void Reload(bool back)
        {
            NextButton.Enabled = false;
            CountBar.Value = 0;
            if (back)
            {
                notConfirm = true;
                Previous();
            }
                
            else
            {
                notConfirm = false;
                CountBar.Maximum = MFFD.Solver.Maxeval;
                Progress = new Progress<ProgressNotification>();
                Progress.ProgressChanged += Progress_ProgressChanged;

                TMF_Save = MFFD.MagicFormula.Copy();

                using (CancellationTokenSource = new CancellationTokenSource())
                {
                    try
                    {
                        await Task.Run(() => MFFD.Run(CancellationTokenSource, Progress), CancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        // キャンセルされた場合
                        MessageBox.Show("キャンセル");
                        MFFD.SetInitialValue(TMF_Save);
                        return;
                    }
                    catch (Exception e)
                    {
                        // キャンセルされた場合
                        MessageBox.Show("失敗\n" + e.Message);
                        MFFD.SetInitialValue(TMF_Save);
                        notConfirm = true;
                        Previous();
                        return;
                    }
                }
            }
        }

        public override bool Cancel()
        {
            DialogResult result = MessageBox.Show("フィッティングをキャンセルしますか？",
    "質問",
    MessageBoxButtons.OKCancel,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                FittingCancel();
                return true;
            }
            return false;
        }

        protected override bool OnPrevious()
        {
            if (notConfirm) return true;
            DialogResult result = MessageBox.Show("フィッティングをキャンセルしますか？",
    "質問",
    MessageBoxButtons.OKCancel,
    MessageBoxIcon.Exclamation,
    MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.OK)
            {
                FittingCancel();
                return true;
            }
            return false;
        }

        private void Progress_ProgressChanged(object sender, ProgressNotification e)
        {
            if(e.Count >= 0)
            {
                CountBar.Value = e.Count;
                CountTB.Text = string.Format("{0} / {1}", e.Count, MFFD.Solver.Maxeval);
            }
            if (e.Stage > 0)
            {
                StageTB.Text = e.Stage.ToString();
                StageBar.Value = e.Stage;
            }
            if(e.Error >0)
            {
                var value = e.Error / MFFD.Solver.Xtol;
                value = 101-10*Math.Log10(value);
                value = value > 100 ? 100 : value;
                value = value < 0 ? 0 : value;
                ErrorBar.Value = (int)value;
                ErrorTB.Text = e.Error.ToString();
            }
            if (e.finished)
                Next();
        }

        public void FittingCancel()
        {
            CancellationTokenSource.Cancel();
            Task.WaitAll();
        }

    }
}
