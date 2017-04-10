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
            ContextMenuStrip.Items.Insert(1, tsmi);
            tsmi = new ToolStripMenuItem("エクスポート", null, null, Keys.None);
            tsmi.DropDownItems.Add(new ToolStripMenuItem("エクセル形式(.xlsx)", null, delegate
            {
                ExportMagicFormula();
            }, Keys.None));
            ContextMenuStrip.Items.Insert(2, tsmi);

            UpdateMenu.Visible = false;
            OnUpdateStateChanged(Impl.Updated);
            impl.OnUpdateStateChanged += OnUpdateStateChanged;
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
            return false;
        }

        bool ExportMagicFormula()
        {
            return false;
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
