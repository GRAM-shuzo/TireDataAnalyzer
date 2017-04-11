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
    public partial class MFInitialValuePage : FittingWizardPage
    {
        public MFInitialValuePage(MagicFormulaFittingDelegate magicFormula)
            :base(magicFormula,"初期パラメータ設定")
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "default.mf";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "MagicFormulaファイル(*.mf)|*.mf|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
               FileNameTB.Text = ofd.FileName;
            }
        }

        override protected bool OnNext()
        {
            if (MFFD.Initialized && !NowValueRB.Checked)
            {
                DialogResult result = MessageBox.Show("現在の値は変更されます。よろしいですか？","確認",
                    MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //何もしない
                }
                else if (result == DialogResult.Cancel)
                {
                    return false;
                }

            }
            if (LoadDefaultRB.Checked)
            {
                var mf = new TireMagicFormula(MFFD.MagicFormula.TireName);

                /*
                mf.FX.NormalizeOffsetParam = new MagicFormulaArguments(0, 0, 0, 0, 0, 0);
                mf.FX.NormalizeScaleParam = new MagicFormulaArguments(1, 1, 1, 1, 1, 1);
                */

                mf.FX.NormalizeOffsetParam = new MagicFormulaArguments(0, 0, 0, 0, 40, 53.1);
                mf.FX.NormalizeScaleParam = new MagicFormulaArguments(13, 1, 1600, 4, 50, 16);

                var param = mf.FY.Parameters;
                param[0] = 1.4;

                param[1] = 2200;
                param[2] = -800;
                param[3] = 0.05;
                param[4] = 3;
                param[5] = -1.8;
                param[6] = 0;

                param[7] = -6000;
                param[8] = 1;
                param[9] = 2;
                param[10] = 1;
                param[11] = 0.01;
                param[12] = 0.5;
                param[13] = 0.01;
                param[14] = 0;

                param[15] = -1;
                param[16] = -0.8;
                param[17] = 0.01;
                param[18] = 0.01;

                param[19] = 0.01;
                param[20] = 0;
                param[21] = 0;

                param[22] = 0;
                param[23] = 0;
                MFFD.SetInitialValue(mf);
                return true;
            }
            else if(LoadFromFileRB.Checked)
            {
                if(System.IO.File.Exists(FileNameTB.Text))
                {
                    try
                    {
                        using (System.IO.FileStream stream = new System.IO.FileStream(FileNameTB.Text, System.IO.FileMode.Open))
                        {
                            var mf = TireMagicFormula.Load(stream);
                            MFFD.SetInitialValue(mf);
                        }
                        return true;
                    }
                    catch(Exception　e)
                    {
                        Log.Output(e.Message);
                        MessageBox.Show("読み込みに失敗しました");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("ファイルがありません");
                    return false;
                }
            }
            else if(NowValueRB.Checked)
            {
                MFFD.SetInitialValue(MFFD.MagicFormula);
                return true;
            }
            else if(LoadFromOtherRB.Checked)
            {
                var node = MFLoadedCB.SelectedItem as ProjectTree.Node_MagicFormula;
                if(node != null)
                {
                    MFFD.SetInitialValue(node.MFFD.MagicFormula.Copy());
                    return true;
                }
                
            }
            
            return false;
        }

        protected override void Reload(bool back)
        {
            
            if(MFFD.Initialized)
            {
                NowValueRB.Enabled = true;
                NowValueRB.Checked = true;
            }
            else
            {
                LoadDefaultRB.Checked = true;
                NowValueRB.Enabled = false;
            }

            var list = ProjectManager.ProjectNode.GetMagicFormula();
            MFLoadedCB.Items.Clear();
            LoadFromOtherRB.Enabled = true;
            MFLoadedCB.Enabled = false;
            BrowseButton.Enabled = false;
            FileNameTB.Enabled = false;
            foreach (var node in list)
            {
                if (node.MFFD.Initialized)
                    MFLoadedCB.Items.Add(node);
            }
            if(MFLoadedCB.Items.Count >0)
            {
                MFLoadedCB.SelectedIndex = 0;
            }
            else
            {
                LoadFromOtherRB.Enabled = false;
            }
        }
        
        private void MFInitialValuePage_Load(object sender, EventArgs e)
        {
            Reload(false);
        }

        private void LoadFromOtherRB_CheckedChanged(object sender, EventArgs e)
        {
            MFLoadedCB.Enabled = LoadFromOtherRB.Checked;
            BrowseButton.Enabled = LoadFromFileRB.Checked;
            FileNameTB.Enabled = LoadFromFileRB.Checked;
        }
    }
}
