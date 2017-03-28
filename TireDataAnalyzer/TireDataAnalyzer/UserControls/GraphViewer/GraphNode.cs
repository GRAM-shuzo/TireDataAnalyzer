using System;
using System.IO;
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
    public class GraphNode
        : GraphViewTreeNode
    {
        public GraphNode(TreeView tv,string label)
            :base(tv, label)
        {
            /*
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("データを追加(&A)", null, delegate
                {
                    AddData();
                }, Keys.A | Keys.Control)
            );
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("回帰曲線を追加(&M)", null, delegate
                {
                    AddMagicFormula();
                }, Keys.M | Keys.Control)
            );
            
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("ユーザーデータを追加(&M)", null, delegate
                {
                    AddUserData();
                }, Keys.M | Keys.Control)
            );*/
            TV = tv;
            
            Viewer = new MultiTireDataViewer(MultiTireDataViewer.EnumScreenCount.One);
            Viewer.GraphName = label;
            Viewer.Dock = DockStyle.Fill;
            Viewer.SeriesChanged += ViewerChanged;
            MainWindow.GraphTabControl.TabClosing += GraphTabControl_TabClosing;
        }

        public GraphNode(TreeView tv, string label, MultiTireDataViewer viewer)
            : base(tv, label)
        {
            /*
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("データを追加(&A)", null, delegate
                {
                    AddData();
                }, Keys.A | Keys.Control)
            );
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("回帰曲線を追加(&M)", null, delegate
                {
                    AddMagicFormula();
                }, Keys.M | Keys.Control)
            );
            
            this.ContextMenuStrip.Items.Insert(3,
                new ToolStripMenuItem("ユーザーデータを追加(&M)", null, delegate
                {
                    AddUserData();
                }, Keys.M | Keys.Control)
            );*/
            TV = tv;
            Viewer = viewer;
            Viewer.SeriesChanged += ViewerChanged;
            MainWindow.GraphTabControl.TabClosing += GraphTabControl_TabClosing;
        }
        TreeView TV;
        void ViewerChanged (object sender, EventArgs e)
        {
            var legends = Viewer.GetLegents();
            this.Nodes.Clear();
            foreach(var s in legends)
            {
                var node = new DataNode(TV, Viewer, s);
                this.Nodes.Add(node);
            }
            this.Expand();

        }

        public MultiTireDataViewer Viewer { get; private set; }
        TabPage GraphPage;

        override protected void OnRename(string name)
        {
            this.Text = name;
            if(GraphPage != null)
            {
                GraphPage.Text = name;
            }
            Viewer.GraphName = name;
        }

        override protected bool OnRemove()
        {
            {
                MainWindow.GraphTabControl.TabPages.Remove(GraphPage);
            }
            return true;
        }

        override protected void OnCopy()
        {
            MemoryStream stream = new MemoryStream();
            Viewer.SaveViewers(stream);
            stream.Position = 0;
            MultiTireDataViewer newViewer = MultiTireDataViewer.LoadViewers(stream);
            var node = new GraphNode(TV, this.Text + "-コピー", newViewer);
            node.ViewerChanged(null, new EventArgs());
            node.Expand();
            TireDataViewerProperty p = new TireDataViewerProperty(newViewer);
            p.ReplotAll();
            this.Parent.Nodes.Add(node);
        }

        override public bool OnProperty()
        {
            var dialog = new TireDataViewerProperty(Viewer);
            DialogResult result =  dialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        override public void OnDoubleClick()
        {
            if (GraphPage == null || !MainWindow.GraphTabControl.TabPages.Contains(GraphPage))
            {
                GraphPage = new TabPage(this.Text);
                GraphPage.Controls.Add(Viewer);
                
                MainWindow.GraphTabControl.TabPages.Add(GraphPage);
                MainWindow.GraphTabControl.SelectedTab = GraphPage;
            }
            else
            {
                MainWindow.GraphTabControl.SelectedTab = GraphPage;
            }
        }

        private void GraphTabControl_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == GraphPage)

            {
                Viewer.Parent = null;
            }
        }

        public static GraphNode Load(ZipArchiveEntry entry, TreeView tv)
        {
            using (var stream = entry.Open())
            {
                MultiTireDataViewer savedata = MultiTireDataViewer.LoadViewers(stream);

                GraphNode node = new GraphNode(tv, savedata.GraphName, savedata);
                node.ViewerChanged(null, new EventArgs());
                node.Expand();
                TireDataViewerProperty p = new TireDataViewerProperty(savedata);
                p.ReplotAll();

                return node;
            }
            return null;
        }

        public void Save(ZipArchive archive)
        {
            ZipArchiveEntry entry = StaticFunctions.GetEntity("graph\\" + Viewer.ID, archive, true);

            using (var stream = entry.Open())
            {
                Viewer.SaveViewers(stream);
            }
        }
    }
}
