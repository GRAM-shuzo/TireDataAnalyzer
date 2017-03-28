using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls.GraphViewer
{

    abstract public class GraphViewTreeNode: TreeNode
    {
        public GraphViewTreeNode(TreeView tv, string label)
            :base(label)
        {
            tv.AfterLabelEdit += TreeView_AfterLabelEdit;
            this.ContextMenuStrip = Menu();
        }
        abstract protected void OnRename(string name);

        abstract protected bool OnRemove();


        abstract protected void OnCopy();

        abstract public bool OnProperty();

        abstract public void OnDoubleClick();

        #region ContextMenu
        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;
            if (e.Node == this && e.Label != null && e.Label != "")
            {
                OnRename(e.Label);
            }
        }

        void Copy(object sender, EventArgs e)
        {
            OnCopy();
            foreach (GraphViewTreeNode child in Nodes)
            {
                child.Copy(sender, e);
            }
        }

        void Delete(object sender, EventArgs e)
        {
            if (OnRemove())
            {
                this.Remove();
            }
        }

        void Rename(object sender, EventArgs e)
        {
            this.BeginEdit();
        }

        void Property(object sender, EventArgs e)
        {
            OnProperty();
        }

        protected ToolStripMenuItem CopyTSMI;
        protected ToolStripMenuItem DeleteTSMI;
        protected ToolStripMenuItem RenameTSMI;
        protected ToolStripMenuItem PropertyTSMI;
        ContextMenuStrip Menu()
        {
            var CopyTSMI = new ToolStripMenuItem("コピー(&C)", null, Copy, Keys.C | Keys.Control);

            var DeleteTSMI = new ToolStripMenuItem("削除(&D)", null, Delete, Keys.D | Keys.Control);

            var RenameTSMI = new ToolStripMenuItem("名前の変更(&M)", null, Rename, Keys.M | Keys.Control);

            var PropertyTSMI = new ToolStripMenuItem("プロパティ(&P)", null, Property, Keys.P | Keys.Control);

            var menu = new ContextMenuStrip();
            menu.Items.Add(CopyTSMI);
            menu.Items.Add(DeleteTSMI);
            menu.Items.Add(RenameTSMI);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(PropertyTSMI);


            return menu;
        }

        #endregion
    }
}