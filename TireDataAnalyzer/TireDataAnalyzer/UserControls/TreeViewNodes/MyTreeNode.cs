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
    abstract public class MyTreeNode : TreeNode
    {
        public ProjectTreeNode Impl{ get; private set; }
        public string ImplName { get { return Impl.Name; } }
        public MyTreeNode(TreeView tv, ProjectTreeNode impl)
        {
            Impl = impl;
            Text = impl.Name;

            if(impl.Expand) Expand();
            ContextMenuStrip = Menu();
            tv.AfterLabelEdit += TreeView_AfterLabelEdit;
            Impl.OnStateChanged += OnImplStateChanged;
            Impl.OnUpdateStateChanged += OnUpdateStateChanged;
            UpdateMenu = new ToolStripMenuItem("アップデート(&U)", null, delegate
            {
                Impl.Update();
            }, Keys.U | Keys.Control);

            OnImplStateChanged(Impl.State);
            OnUpdateStateChanged(Impl.Updated);
        }

        ToolStripMenuItem UpdateMenu;
        bool updateMenuShow = true;
        protected bool UpdateMenuShow
        {
            get
            {
                return updateMenuShow;
            }
            set
            {
                updateMenuShow = value;
                if (ContextMenuStrip.Items.Contains(UpdateMenu))
                {
                    ContextMenuStrip.Items.Remove(UpdateMenu);
                }
            }
        }



        void OnImplStateChanged(ProjectTree.ProjectTreeNode.ProjectTreeState newState)
        {
            AddTextMark(newState, Impl.Updated);
        }
        void OnUpdateStateChanged(ProjectTree.ProjectTreeNode.UpdateState newState)
        {
            AddTextMark(Impl.State, newState);
            if(newState == ProjectTreeNode.UpdateState.Normal && UpdateMenuShow)
            {
                if(ContextMenuStrip.Items.Contains(UpdateMenu))
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
        void AddTextMark(ProjectTree.ProjectTreeNode.ProjectTreeState newState1, ProjectTree.ProjectTreeNode.UpdateState newState2)
        {
            Text = Impl.Name;
            if (newState1 == ProjectTreeNode.ProjectTreeState.Changed)
            {
                Text = Text + "  *";
            }
            if(newState2 == ProjectTreeNode.UpdateState.NotUpdated)
            {
                Text = "! " + Text;
            }
            if(OnMyTreeNodeTextChanged != null)
            {
                OnMyTreeNodeTextChanged(Text);
            }
        }

        public delegate void OnMyTreeNodeTextChangedDelegate(string text);
        public OnMyTreeNodeTextChangedDelegate OnMyTreeNodeTextChanged; 

        public void ConfirmIfNotUpdated()
        {
            if (Impl.Updated == ProjectTree.ProjectTreeNode.UpdateState.NotUpdated)
            {
                DialogResult result = MessageBox.Show("親ノードの変更が反映されていません。更新しますか？）",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    Impl.Update();
                }
            }
        }
        public void ExpandChanged()
        {
            Impl.Expand = this.IsExpanded;
        }

        abstract protected void OnRename(string name);

        abstract protected bool OnRemove();

        abstract protected void OnCopy();

        abstract protected void OnPaste();

        abstract public void OnProperty();

        public void CheckPastable()
        {
            if(Pastable())
            {
                PasteTSMI.Visible = true;

            }
            else
            {
                PasteTSMI.Visible = false;
            }
        }

        virtual protected bool Pastable()
        {
            return false;
        }


        #region ContextMenu
        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            if (e.Node == this && e.Label != null && e.Label != "" && e.Label != Impl.Name)
            {
                Impl.Name = e.Label;
                OnRename(e.Label);
            }
            
        }

        void Copy(object sender, EventArgs e)
        {
            OnCopy();
            /*foreach(MyTreeNode child in Nodes)
            {
                child.Copy(sender,e);
            }*/
        }

        void Delete(object sender, EventArgs e)
        {
            if(OnRemove())
            {
                Impl.DeleteSelf();
                this.Remove();
            }
        }

        void Rename(object sender, EventArgs e)
        {
            Text = Impl.Name;
            this.BeginEdit();
        }

        void Property(object sender, EventArgs e)
        {
            OnProperty();
        }

        void Paste(object sender, EventArgs e)
        {
            OnPaste();
        }

        protected ToolStripMenuItem CopyTSMI;
        protected ToolStripMenuItem DeleteTSMI;
        protected ToolStripMenuItem RenameTSMI;
        protected ToolStripMenuItem PropertyTSMI;
        ToolStripMenuItem PasteTSMI;
        ContextMenuStrip Menu()
        {
            var CopyTSMI = new ToolStripMenuItem("コピー(&C)", null, Copy, Keys.C | Keys.Control);

            PasteTSMI = new ToolStripMenuItem("貼り付け(&V)", null, Copy, Keys.V | Keys.Control);
            PasteTSMI.Visible = false;
            var DeleteTSMI = new ToolStripMenuItem("削除(&D)", null, Delete, Keys.D | Keys.Control);

            var RenameTSMI = new ToolStripMenuItem("名前の変更(&M)", null, Rename, Keys.M | Keys.Control);

            var PropertyTSMI = new ToolStripMenuItem("プロパティ(&P)", null, Property, Keys.P | Keys.Control);

            var menu = new ContextMenuStrip();
            menu.Items.Add(CopyTSMI);
            menu.Items.Add(PasteTSMI);
            menu.Items.Add(DeleteTSMI);
            menu.Items.Add(RenameTSMI);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(PropertyTSMI);


            return menu;
        }
        #endregion
    }
}
