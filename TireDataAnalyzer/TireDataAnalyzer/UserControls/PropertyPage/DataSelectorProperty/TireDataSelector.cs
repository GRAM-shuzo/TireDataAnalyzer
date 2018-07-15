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

namespace TireDataAnalyzer.UserControls
{
    [Docking(DockingBehavior.Ask)]
    public partial class TireDataSelector : UserControl
    {
        public static int MaxWidth = 360;

        public Table Table
        {
            get;
            private set;
        }
        TireDataSetSelector Selector;
        PropertyPage.DataSelectorProperty property;

        public delegate void SelectedAreaChangedDelegate(TireDataSetSelector selector);
        public SelectedAreaChangedDelegate SelectedAreaChanged;

        public TireDataSelector()
        {
            InitializeComponent();
        }
        public void Initialize(TireDataSetSelector selector, Table table, PropertyPage.DataSelectorProperty p)
        {
            Table = table;
            Selector = selector;
            property = p;
            var maxmin = selector.Maxmin(Table);
            foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
            {
                if (column == TireDataColumn.NT) continue;
                var node = new TreeNode(TireData.ColumnFormal[column]);
                SelectorTreeView.Nodes.Add(node);
                node.ContextMenuStrip = new ContextMenuStrip();
                node.ContextMenuStrip.Items.Add(
                    new ToolStripMenuItem(
                        "制約の追加",
                        null,
                        delegate (object sender, EventArgs e)
                        {
                            AddNewConstrain(column, node);
                        }
                    )
                );
                
                var list = Selector.Constrains(Table)[column];
                foreach( var constrain in list)
                {
                    AddConstrainToNode(column, node, constrain);
                }

            }
        }

        void AddConstrainToNode(TireDataColumn column, TreeNode ParentNode, TireDataConstrain constrain)
        {
            
            var node = new TreeNode(constrain.Name);
            ParentNode.Nodes.Add(node);
            node.ContextMenuStrip = new ContextMenuStrip();
            EventHandler handler = delegate (object sender, EventArgs e)
            {
                var d = new MaxMinDialog(column, Table, Selector, constrain);
                d.ShowDialog();
                if (d.DialogResult == DialogResult.OK)
                {
                    constrain.CopyFrom(d.Constrain);
                    Selector.ExtractData();
                    Selector.ConfirmStateChanged(Table);
                    node.Text = constrain.Name;
                    if (SelectedAreaChanged != null) SelectedAreaChanged(Selector);
                    property.Changed();
                }
            };
            node.TreeView.NodeMouseDoubleClick += delegate (object sender, TreeNodeMouseClickEventArgs e)
            {
                if (e.Node == node) handler(sender, e);
            };
            node.TreeView.AfterLabelEdit += delegate (object sender, NodeLabelEditEventArgs e)
            {
                e.CancelEdit = true;
                if (e.Node == node && e.Label != null && e.Label != "")
                {
                    constrain.Name = e.Label;
                    node.Text = e.Label;
                    Selector.ConfirmStateChanged(Table);

                    
                    property.Changed();
                }
            };
            

            node.ContextMenuStrip.Items.Add(
                    new ToolStripMenuItem(
                        "名前の変更",
                        null,
                        delegate (object sender, EventArgs e)
                        {
                            node.BeginEdit();
                            
                        }
                    )
                );
            node.ContextMenuStrip.Items.Add(
                    new ToolStripMenuItem(
                        "編集",
                        null,
                        handler
                    )
                );
            node.ContextMenuStrip.Items.Add(
                    new ToolStripMenuItem(
                        "削除",
                        null,
                        delegate (object sender, EventArgs e)
                        {
                            Selector.RemoveConstrain(constrain, Table);
                            Selector.ExtractData();
                            ParentNode.Nodes.Remove(node);
                            if (SelectedAreaChanged != null) SelectedAreaChanged(Selector);
                            property.Changed();
                        }
                    )
                );
            
            
        }

        public void AddNewConstrain(TireDataColumn column, TreeNode ParentNode)
        {
            var dialog = new MaxMinDialog(column, Table, Selector);
            dialog.ShowDialog();
            if (dialog.DialogResult == DialogResult.OK)
            {
                AddConstrainToNode(column, ParentNode, dialog.Constrain);
                Selector.AddConstrain(dialog.Constrain, Table);
                Selector.ExtractData();
                if (SelectedAreaChanged != null) SelectedAreaChanged(Selector);
                property.Changed();
            }
            

            
        }
    }
}
