﻿using System;
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
    public class TreeViewNode_Selector : MyTreeNode
    {
        public TreeViewNode_Selector(TreeView tv, Node_DataSelector impl)
            :base(tv,impl)
        {
            Impl = impl;
            ContextMenuStrip.Items.Insert(0,
                new ToolStripMenuItem("セレクタを追加(&A)", null, AddSelector, Keys.A & Keys.Control)
                );
            ContextMenuStrip.Items.Insert(0,
                new ToolStripMenuItem("回帰曲線を追加(&A)", null, AddMagicFormula, Keys.B & Keys.Control)
                );
        }

        Node_DataSelector Impl { get; set; }

        void AddSelector(object sender, EventArgs e)
        {
            ConfirmIfNotUpdated();
            var selectorImpl = new Node_DataSelector(
                StaticFunctions.GetUniqueName(this.TreeView, "新規セレクタ"),
                Impl,
                true
                );
            var selector = new TreeViewNode_Selector(
                this.TreeView,
                selectorImpl
                );
            this.Nodes.Add(selector);
            this.Expand();
        }

        void AddMagicFormula(object sender, EventArgs e)
        {
            ConfirmIfNotUpdated();
            var magicFormulaImpl = new Node_MagicFormula(
               StaticFunctions.GetUniqueName(this.TreeView, "回帰曲線"),
               Impl
               );
            var magicFormula = new TreeViewNode_MagicFormula(
                this.TreeView,
                magicFormulaImpl
                );
            this.Nodes.Add(magicFormula);
            this.Expand();
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
            var obj = StaticFunctions.ClipBoad;
            if (obj != null && obj is Node_MagicFormula)
            {
                var newNmf = ProjectManager.CopyProjectTree(obj, this.Impl);
                ProjectManager.ResetCopyFlag(newNmf);
                var myNode = StaticFunctions.GetTreeView(newNmf);
                
                this.Nodes.Add(myNode);
            }
            else if (obj != null && obj is Node_DataSelector)
            {
                var newNds = ProjectManager.CopyProjectTree(obj, this.Impl);
                ProjectManager.ResetCopyFlag(newNds);
                var myNode = StaticFunctions.GetTreeView(newNds);

                this.Nodes.Add(myNode);
            }
        }

        override protected bool Pastable()
        {
            var obj = StaticFunctions.ClipBoad;
            if(obj != null && obj is Node_MagicFormula)
            {
                return true;
            }
            else if (obj != null && obj is Node_DataSelector)
            {
                return true;
            }
            return false;
        }

        PropertyPage.DataSelectorProperty Property;
        override public void OnProperty()
        {
            if (!StaticFunctions.IsAlreadyAdded(Property))
            {
                Property = new UserControls.PropertyPage.DataSelectorProperty(Impl);
                StaticFunctions.AddPropertyPage(Property);

            }
        }
    }
}
