using System;
using System.IO;
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
using System.IO.Compression;

namespace TireDataAnalyzer.UserControls.GraphViewer
{
    public class TopNode
        :GraphViewTreeNode
    {
        public TopNode(TreeView tv)
            :base(tv,"GraphView")
        {
            this.ContextMenuStrip.Items.Clear();
            this.ContextMenuStrip.Items.Add( 
                new ToolStripMenuItem("グラフを追加(&A)", null, delegate
            {
                Add();
            }, Keys.A | Keys.Control)
            );
        }

        public void Add()
        {
            var node = new GraphNode(this.TreeView, "新規グラフ");
            if (node.OnProperty())
            {
                this.Nodes.Add(node);
                this.Expand();
                node.OnDoubleClick();
            }
                

            return;
        }

        override public void OnDoubleClick()
        {

        }

        override protected void OnRename(string name)
        {
        }

        override protected bool OnRemove()
        {
            return true;
        }

        override protected void OnCopy()
        {
        }

        override public bool OnProperty()
        {
            return true;
        }

        public static TopNode Load(ZipArchive archive, TreeView tv, UserControls.ProgressDialog pd = null)
        {
            var entries = archive.Entries;
            TopNode node = new TopNode(tv);
            int max = 0;
            foreach (var entry in entries.ToArray())
            {
                if (!entry.FullName.Contains("graph")) continue;
                ++max;
            }
            if(pd !=null)
            {
                pd.Maximum = max;
                pd.Value = 0;
            }
            foreach (var entry in entries.ToArray())
            {
                if (!entry.FullName.Contains("graph")) continue;
                node.Nodes.Add(GraphNode.Load(entry, tv));
                ++pd.Value;
            }

            return node;
        }

        public void Save(ZipArchive archive)
        {
           var entries = archive.Entries;
           foreach (var entry in entries.ToArray())
           {
                bool exist = false;
                string directory = Path.GetFileName(Path.GetDirectoryName(entry.FullName));
                if (!directory.Contains("graph")) exist = true;
                foreach (GraphNode child in this.Nodes)
                {
                    
                    if (entry.FullName == "graph\\" + child.Viewer.ID +".graph") exist = true;
                }
                if (!exist)
                {
                    entry.Delete();
                }
            }
            
            foreach (GraphNode child in this.Nodes )
            {
                child.Save(archive);
            }
        }
    }
}
