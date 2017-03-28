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
    public class DataNode
        :GraphViewTreeNode
    {
        public DataNode(TreeView tv, MultiTireDataViewer viewer, string id)
            :base(tv, viewer.LegendTextOverride(id) )
        {
            this.ContextMenuStrip.Items.RemoveAt(0);
            Viewer = viewer;
            ID = id;
        }
        string ID;
        MultiTireDataViewer Viewer;
        override protected void OnRename(string name)
        {
            this.Text = name;
            var node = this.Parent as GraphNode;
            node.Viewer.LegendTextOverride(ID, name);
            Viewer.DrawGraph(ID);
        }

        override protected bool OnRemove()
        {
            Viewer.Remove(ID);
            return true;
        }

        override protected void OnCopy()
        {
        }

        override public bool OnProperty()
        {
            if (Viewer.GetDataType(ID) == TireDataViewer.DataType.MagicFormula)
            {
                var dialog = new MFGraphDialog(Viewer,ID);
                dialog.ShowDialog();
            }
            else
            {
                Viewer.Property();
            }
            return true;
        }

        override public void OnDoubleClick()
        {
            OnProperty();
        }
    }
}
