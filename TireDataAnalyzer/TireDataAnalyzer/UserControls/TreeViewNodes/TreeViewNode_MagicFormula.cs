using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TireDataAnalyzer.ProjectTree;
using TireDataAnalyzer.Export;

namespace TireDataAnalyzer.UserControls.TreeViewNodes
{
    public class TreeViewNode_MagicFormula :  MyTreeNode
    {
        public TreeViewNode_MagicFormula(TreeView tv, Node_MagicFormula impl)
            :base(tv,impl)
        {
            Impl = impl;
            FittinWizardMenu = new ToolStripMenuItem("フィッティングウィザードの開始(&W)", null, Wizard, Keys.W & Keys.Control);
            ContextMenuStrip.Items.Insert(0, FittinWizardMenu);
            base.UpdateMenuShow = false;
            UpdateMenu = new ToolStripMenuItem("親ノードを更新してウィザードを開始(&U)", null, delegate
            {
                Impl.Update();
                if (!Wizard())
                {
                    Impl.ConfirmNotUpdated();
                }


            }, Keys.U | Keys.Control);
            ContextMenuStrip.Items.Insert(0, UpdateMenu);

            var tsmi = new ToolStripMenuItem("マジックフォーミュラを保存(&S)", null, delegate
            {
                SaveAs();
            }, Keys.S & Keys.Control);
            tsmi.Visible = Impl.MFFD.Initialized;
            ContextMenuStrip.Items.Add(tsmi);
            tsmi = new ToolStripMenuItem("エクスポート", null, null, Keys.None);

            tsmi.DropDownItems.Add(new ToolStripMenuItem("エクセル形式(.xlsx)", null, delegate
            {
                ExportMagicFormula(ExportMode.excel);
            }, Keys.None));
            tsmi.Visible = Impl.MFFD.Initialized;
            ContextMenuStrip.Items.Add(tsmi);

            UpdateMenu.Visible = false;
            OnUpdateStateChanged(Impl.Updated);
            impl.OnUpdateStateChanged += OnUpdateStateChanged;
            impl.MFFD.OnInitialized += OnMFInitialized;

        }

        new Node_MagicFormula Impl { get; set; }
        ToolStripMenuItem UpdateMenu;
        ToolStripMenuItem FittinWizardMenu;
        void OnUpdateStateChanged(ProjectTree.ProjectTreeNode.UpdateState newState)
        {
            
            if (newState == ProjectTreeNode.UpdateState.Normal)
            {
                UpdateMenu.Visible = false;
                FittinWizardMenu.Visible = true;
            }
            else if (newState == ProjectTreeNode.UpdateState.NotUpdated)
            {
                UpdateMenu.Visible = true;
                FittinWizardMenu.Visible = false;
            }
        }

        void Wizard(object sender, EventArgs e)
        {
            Wizard();
        }

        bool Wizard()
        {
            var wizard = new TireDataAnalyzer.UserControls.FittingWizard.FittingWizard(Impl.MFFD.Copy(), Impl.Name);
            wizard.ShowDialog();
            if(wizard.DialogResult== DialogResult.OK)
            {
                Impl.MFFD.CopyFrom(wizard.MagicFormulaFD, Impl.MFFD.FittingResolved);
                return true;
            }
            return false;
        }

        bool SaveAs()
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = Impl.Name + ".mf";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "MagicFormulaファイル(*.mf)|*.mf|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.Stream stream = sfd.OpenFile())
                {
                    if (stream != null)
                    {

                        Impl.MFFD.MagicFormula.Save(stream);
                    }
                }
                MessageBox.Show("保存しました");
                return true;
                    

                 
            }
            return false;
        }

        bool ExportMagicFormula(ExportMode mode)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";

            switch (mode)
            {
                case ExportMode.excel:
                    sfd.FileName = Impl.Name + ".xlsx";
                    sfd.Filter = "Excelファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
                    break;
                case ExportMode.tsv:
                    sfd.FileName = Impl.Name + ".tsv";
                    sfd.Filter = "タブ区切りテキスト(*.tsv)|*.tsv|すべてのファイル(*.*)|*.*";
                    break;
                case ExportMode.ini:
                    sfd.FileName = Impl.Name + ".ini";
                    sfd.Filter = "iniファイル(*.ini)|*.ini|すべてのファイル(*.*)|*.*";
                    break;
            }
            sfd.FilterIndex = 1;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (MagicFormulaExpoter.Export(sfd.FileName, mode))
                {
                    MessageBox.Show("保存しました");
                    return true;
                }
                else
                {
                    MessageBox.Show("保存に失敗しました");
                }
            }
            return false;
        }

        void OnMFInitialized(object sender, EventArgs e)
        {
            ContextMenuStrip.Items[ContextMenuStrip.Items.Count - 1].Visible = true;
            ContextMenuStrip.Items[ContextMenuStrip.Items.Count - 2].Visible = true;
        }

        override protected void OnRename(string name)
        {

        }

        override protected bool OnRemove()
        {
            if (Property != null)
            {
                return Property.Remove();
            }
            else
                return true;
        }

        override protected void OnCopy()
        {
           StaticFunctions.ClipBoad = Impl;
        }
        protected override void OnPaste()
        {

        }

        override protected bool Pastable()
        {
            return false;
        }

        UserControls.PropertyPage.TireMagicFormulaParameterProperty Property;
        override public void OnProperty()
        {
            /*
            if (!StaticFunctions.IsAlreadyAdded(Property))
            {
                Property = new UserControls.PropertyPage.TireMagicFormulaParameterProperty(Impl);
                StaticFunctions.AddPropertyPage(Property);
            }
            */
        }
    }
}
