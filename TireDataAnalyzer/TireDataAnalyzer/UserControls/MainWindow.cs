using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TTCDataUtils;
using TireDataAnalyzer.UserControls.TreeViewNodes;
using TireDataAnalyzer.UserControls.GraphViewer;
using TireDataAnalyzer.UserControls;
using System.Runtime.Serialization;
namespace TireDataAnalyzer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowName = Text;

            OnMWStateChanging += BeforeClosed;
            OnMWStateChanged += ChangeEnablesOnStateChanged;
            OnMWStateChanged += OnClosed;

            ProjectManager.DataStateChanged += AddAsterisk_toWindowName;
        }

        public static CustomTabControl MainTabControl;
        public static CustomTabControl GraphTabControl;
        public static TreeView GraphTreeView;
        private void MainWindow_Load(object sender, EventArgs e)
        {
            

            //var p = new UserControls.TireDataViewerProperty();
            //p.ShowDialog();
            MainTabControl = mainTabControl;
            GraphTabControl = graphTabControl;
            GraphTreeView = GraphTree;
            State = MWState.Closed;

            LoadWindowSettings();
            ProjectTreeView.AfterExpand += ProjectTreeView_AfterExpandChange;
            ProjectTreeView.AfterCollapse += ProjectTreeView_AfterExpandChange;
        }

        

        private void ProjectTreeView_AfterExpandChange(object sender, TreeViewEventArgs e)
        {
            (e.Node as MyTreeNode).ExpandChanged();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            State = MWState.Closed;
            if (State != MWState.Closed)
            {
                e.Cancel = true;
            }
            SaveWindowSettings();
        }

        void LoadWindowSettings()
        {
            // ウィンドウの位置・サイズを復元
            Location = Properties.Settings.Default.MyWindowLocation;
            Bounds = Properties.Settings.Default.MyBounds;
            WindowState = Properties.Settings.Default.MyWindowState;
            splitContainer1.SplitterDistance = Properties.Settings.Default.MySplitterDistance1;
            splitContainer2.SplitterDistance = Properties.Settings.Default.MySplitterDistance2;
        }

        void SaveWindowSettings()
        {
            // ウィンドウの位置・サイズを保存
            Properties.Settings.Default.MyWindowLocation = Location;
            if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MyBounds = Bounds;
                
            }
            else
            {
                Properties.Settings.Default.MyBounds = RestoreBounds;
            }
                

            if (WindowState != FormWindowState.Minimized)
                Properties.Settings.Default.MyWindowState = WindowState;

            Properties.Settings.Default.MySplitterDistance1 = splitContainer1.SplitterDistance;
            Properties.Settings.Default.MySplitterDistance2 = splitContainer2.SplitterDistance;
            Properties.Settings.Default.Save();
        }

        #region StateMachine

        public string WindowName;

        public enum MWState
        {
            Loaded,
            Closed
        }

        public delegate bool OnChangingMWStateDelegate(MWState newState);
        public delegate void OnMWStateChangedDelegate(MWState newState);
        public OnChangingMWStateDelegate OnMWStateChanging;
        public OnMWStateChangedDelegate OnMWStateChanged;

        MWState state = MWState.Closed;
        public MWState State
        {
            get
            {
                return state;
            }
            set
            {
                Delegate[] delegateArray = OnMWStateChanging.GetInvocationList();
                foreach (OnChangingMWStateDelegate del in delegateArray)
                {
                    if(!del(value)) return; 
                }
                state = value;
                OnMWStateChanged(value);
            }
        }

        bool BeforeClosed(MWState newState)
        {
            //変更されている場合
            if (newState == MWState.Closed && State == MWState.Loaded && ProjectManager.DataState == ProjectTree.ProjectTreeNode.ProjectTreeState.Changed)
            {
                while(true)
                {
                    //メッセージボックスを表示する
                    DialogResult result = MessageBox.Show("プロジェクトは変更されています．保存しますか？",
                        "確認",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        return Save(ProjectManager.FilePath);//保存に失敗すればキャンセル扱いする
                    }
                    else if (result == DialogResult.No)
                    {
                        break;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        void ChangeEnablesOnStateChanged(MWState newState)
        {
            if (newState == MWState.Closed)
            {
                保存SToolStripMenuItem.Enabled = false;
                名前を付けて保存AToolStripMenuItem.Enabled = false;
                閉じるToolStripMenuItem.Enabled = false;
                //グラフToolStripMenuItem.Enabled = false;

            }
            else if (newState == MWState.Loaded)
            {
                保存SToolStripMenuItem.Enabled = true;
                名前を付けて保存AToolStripMenuItem.Enabled = true;
                閉じるToolStripMenuItem.Enabled = true;
                //グラフToolStripMenuItem.Enabled = true;
            }
        }
        void OnClosed(MWState newState)
        {
            if(newState == MWState.Closed)
            {
                GraphTreeView.Nodes.Clear();
                GraphTreeView.Nodes.Add(new TopNode(GraphTree));
                GraphTabControl.TabPages.Clear();

                ProjectTreeView.Nodes.Clear();
                MainTabControl.TabPages.Clear();
                GraphWindow = new TabPage("GraphWindow");
                GraphWindow.Controls.Add(GraphTabControl);
                MainTabControl.TabPages.Add(GraphWindow);
                ProjectManager.Close();
            }
        }

        void AddAsterisk_toWindowName(ProjectTree.ProjectTreeNode.ProjectTreeState newState)
        {
            if (newState == ProjectTree.ProjectTreeNode.ProjectTreeState.Changed)
            {
                Text = WindowName + "  *";
            }
            else
            {
                Text = WindowName;
            }
        }


        #endregion

        #region メニューバー
        #region  ファイルメニュー
        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            State = MWState.Closed;
            if (State == MWState.Closed)
            {
                New();
            }
        }

        private void 読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.InitialDirectory = Properties.Settings.Default.OpenInitialDirectory;
            ofd.Filter = "プロジェクトファイル(*.fstda)|*.fstda|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != "")
            {
                State = MWState.Closed;
                if (State == MWState.Closed)
                {
                    Properties.Settings.Default.OpenInitialDirectory = System.IO.Path.GetDirectoryName(ofd.FileName);
                    if (ProjectManager.Load(ofd.FileName))
                    {
                        State = MWState.Loaded;
                        ProjectTreeView.Nodes.Add(GetTreeView(ProjectManager.ProjectNode));
                    }
                    else
                    {
                        MessageBox.Show("ファイルの読み込みに失敗しました");
                    }
                }
            }
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(ProjectManager.FilePath);
        }

        private void 名前を付けて保存AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(null);
        }

        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            State = MWState.Closed;
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region グラフ
        private void 新規グラフ作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region バージョン情報

        private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        void New()
        {
            ProjectManager.New("新規プロジェクト");

            //左側ツリービューの更新
            ProjectTreeView.Nodes.Add(
                new UserControls.TreeViewNodes.TreeViewNode_Project(
                    ProjectTreeView,
                    ProjectManager.ProjectNode
                    )
                );
            State = MWState.Loaded;
            
        }

        bool Save(string filename)
        {
            bool done;
            if (filename == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = ProjectManager.ProjectNode.Name + ".fstda";
                //はじめに表示されるフォルダを指定する
                if (ProjectManager.FilePath != null)
                {
                    sfd.InitialDirectory = System.IO.Path.GetDirectoryName(ProjectManager.FilePath);
                }
                else
                {
                    sfd.InitialDirectory = @"C:\";
                }


                sfd.Filter = "プロジェクトファイル(*.fstda)|*.fstda|すべてのファイル(*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "保存先のファイルを選択してください";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                }
                else
                {
                    return false;
                }
                    
            }

            if (!(done = ProjectManager.Save(filename)))
            {
                MessageBox.Show("セーブデータの保存に失敗しました");
            }
            return done;
        }

        MyTreeNode GetTreeView(ProjectTree.ProjectTreeNode node)
        {
            MyTreeNode mtn;
            if(node is ProjectTree.Node_Project )
            {
                mtn = new TreeViewNode_Project(ProjectTreeView, node as ProjectTree.Node_Project);
            }
            else if(node is ProjectTree.Node_RawTireData)
            {
                mtn = new TreeViewNode_RawTireData(ProjectTreeView, node as ProjectTree.Node_RawTireData);
            }
            else if(node is ProjectTree.Node_DataSelector)
            {
                mtn = new TreeViewNode_Selector(ProjectTreeView, node as ProjectTree.Node_DataSelector);
            }
            else// if (node is ProjectTree.Node_MagicFormula)
            {
                mtn = new TreeViewNode_MagicFormula(ProjectTreeView, node as ProjectTree.Node_MagicFormula);
            }
            foreach (var child in node.Children)
            {
                mtn.Nodes.Add(GetTreeView(child));
            }
            return mtn;
        }

        private void MainTabControl_TabClosing(object sender, TabControlCancelEventArgs e)
        {
            
            if (e.TabPage == GraphWindow) e.Cancel = true;

        }

        private void ProjectTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            (e.Node as MyTreeNode).OnProperty();
        }
        bool ProjectTreeViewDoubleClicked;
        private void ProjectTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                ProjectTreeViewDoubleClicked = true;
            }
            else
            {
                ProjectTreeViewDoubleClicked = false;
            }

        }
        private void ProjectTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (ProjectTreeViewDoubleClicked == true)
            {
                e.Cancel = true;
                ProjectTreeViewDoubleClicked = false;
            }
        }
        private void ProjectTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (ProjectTreeViewDoubleClicked == true)
            {
                e.Cancel = true;
                ProjectTreeViewDoubleClicked = false;
            }
        }

        private void GraphTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node as GraphViewTreeNode;
            if(node != null)
            {
                node.OnDoubleClick();
            }
            var node2 = e.Node as GraphNode;
        }
        bool GraphTreeDoubleClicked;
        private void GraphTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                GraphTreeDoubleClicked = true;
            }
            else
            {
                GraphTreeDoubleClicked = false;
            }

        }
        private void GraphTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (GraphTreeDoubleClicked == true)
            {
                e.Cancel = true;
                GraphTreeDoubleClicked = false;
            }
        }

        private void GraphTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (GraphTreeDoubleClicked == true)
            {
                e.Cancel = true;
                GraphTreeDoubleClicked = false;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
