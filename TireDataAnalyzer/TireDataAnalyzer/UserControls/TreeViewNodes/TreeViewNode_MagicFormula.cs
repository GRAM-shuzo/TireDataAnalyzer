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
            ContextMenuStrip.Items.Insert(0,
                new ToolStripMenuItem("フィッティングウィザードの開始(&S)", null, Wizard, Keys.A & Keys.Control)
                );
            base.UpdateMenuShow = false;
            UpdateMenu = new ToolStripMenuItem("親ノードを更新してウィザードを開始(&U)", null, delegate
            {
                Impl.Update();
                if (!Wizard())
                {
                    Impl.ConfirmNotUpdated();
                }
                
                
            }, Keys.U | Keys.Control);
            OnUpdateStateChanged(Impl.Updated);
            impl.OnUpdateStateChanged += OnUpdateStateChanged;
        }

        Node_MagicFormula Impl { get; set; }
        ToolStripMenuItem UpdateMenu;

        void OnUpdateStateChanged(ProjectTree.ProjectTreeNode.UpdateState newState)
        {
            
            if (newState == ProjectTreeNode.UpdateState.Normal && UpdateMenuShow)
            {
                if (ContextMenuStrip.Items.Contains(UpdateMenu))
                {
                    ContextMenuStrip.Items.Remove(UpdateMenu);
                }
            }
            else if (newState == ProjectTreeNode.UpdateState.NotUpdated && UpdateMenuShow)
            {
                if (!ContextMenuStrip.Items.Contains(UpdateMenu))
                {
                    ContextMenuStrip.Items.Add(UpdateMenu);
                }
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
                Impl.MFFD.CopyFrom(wizard.MagicFormulaFD);
                return true;
            }
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
