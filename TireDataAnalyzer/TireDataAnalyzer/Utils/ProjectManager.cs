using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace TireDataAnalyzer
{
    public static class ProjectManager
    {
        public static ProjectTree.Node_Project ProjectNode{ get; private set; }
        public static string FilePath { get; private set; }
        public static string ProjectName
        {
            get
            {
                if(ProjectNode != null)
                {
                    return ProjectNode.Name;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ProjectNode.Name = value;
            }
        }
        

        public static ProjectTree.ProjectTreeNode.ProjectTreeState DataState
        {
            get
            {
                return ProjectNode.State;
            }
        }


        public delegate void DataStateChangedDelegate(ProjectTree.ProjectTreeNode.ProjectTreeState newState);
        public static DataStateChangedDelegate DataStateChanged;
        static void DataStateChangedImpl(ProjectTree.ProjectTreeNode.ProjectTreeState newState)
        {
            DataStateChanged(newState);
        }

        public static void New(string projectName)
        {
            ProjectNode = new ProjectTree.Node_Project(projectName);
            ProjectNode.OnStateChanged += DataStateChangedImpl;
            DataStateChangedImpl(ProjectNode.State);
        }
        public static void Close()
        {
            FilePath = null;
            ProjectNode = null;
            DataStateChangedImpl(ProjectTree.ProjectTreeNode.ProjectTreeState.Deleted);
        }
        public static bool Save(string path)
        {
            bool copy = false;
            try
            {
                if (path != FilePath && File.Exists(FilePath))
                {
                    System.IO.File.Copy(FilePath, path, true);
                    copy = true;
                }
            }
            catch(Exception ex)
            {
                Log.Output(ex.Message);
                return false;
            }
            try
            {
                using (FileStream zipToOpen = new FileStream(path, FileMode.OpenOrCreate))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update, false, Encoding.UTF8))
                    {
                        DeleteEntries(archive);
                        SaveAll(archive, ProjectNode, "");
                        SaveGraph(archive);
                    }
                }
                FilePath = path;
            }
            catch (Exception ex)
            {
                if(copy)
                {
                    Log.Output(ex.Message);
                    File.Delete(path);
                    return false;
                }
            }
            return true;
        }
        public static bool Load(string path)
        {
            try
            {
                using (FileStream zipToOpen = new FileStream(path, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read, false, Encoding.UTF8))
                    {
                        LoadAll(archive);
                        LoadGraph(archive);
                    }
                }
                FilePath = path;
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
                return false;

            }
            return true;
        }

        static void DeleteEntries(ZipArchive archive)
        {
            var entries = archive.Entries;
            var list = new List<string>();
            GetAllGUID(list, ProjectNode);
            foreach( var entry in entries.ToArray())
            {
                bool exist = false;
                foreach(var guid in list)
                {
                    string directory = Path.GetFileName(Path.GetDirectoryName(entry.FullName));
                    if (directory.Contains(guid)) exist = true;
                }
                if(!exist)
                {
                    entry.Delete();
                }
            }
        }
        static void GetAllGUID(List<string> list, ProjectTree.ProjectTreeNode node)
        {
            list.Add(node.ID.ToString());
            foreach( var child in node.Children)
            {
                GetAllGUID(list, child);
            }
        }
        static void SaveAll(ZipArchive archive, ProjectTree.ProjectTreeNode node, string path)
        {
            path += node.ID.ToString() + "\\";
            node.Save(archive, path);
            foreach (var child in node.Children)
            {
                SaveAll(archive, child, path);
            }
        }

       
        static void LoadAll(ZipArchive archive)
        {
            List<string> FolderTree = new List<string>();
            foreach( var entry in archive.Entries)
            {
                if(entry.FullName.Contains("node.xml"))
                {
                    FolderTree.Add(Path.GetDirectoryName(entry.FullName));
                }
            }
            FolderTree.Sort();
            FolderTree.Reverse();
            ProjectNode = LoadAll(null, archive, "", FolderTree) as ProjectTree.Node_Project;
            ProjectNode.OnStateChanged += DataStateChangedImpl;
            DataStateChangedImpl(ProjectNode.State);
        }

        static ProjectTree.ProjectTreeNode LoadAll(ProjectTree.ProjectTreeNode parent, ZipArchive archive, string path, List<string> folderTree)
        {
            var lastPath = folderTree.Last();
            while (lastPath != null && lastPath.Contains(path))
            {
                var node = ProjectTree.ProjectTreeNode.Load(archive, lastPath+"\\", parent);
                folderTree.RemoveAt(folderTree.Count - 1);
                if (folderTree.Count == 0)
                {
                    return node;
                }


                LoadAll(node, archive, lastPath, folderTree);
                if (folderTree.Count == 0)
                {
                    return node;
                }
                lastPath = folderTree.Last();
            }
            return null;
        }

        public static ProjectTree.ProjectTreeNode CopyProjectTree(ProjectTree.ProjectTreeNode original, ProjectTree.ProjectTreeNode parent)
        {
            ProjectTree.ProjectTreeNode newNode = null;
            if (original is ProjectTree.Node_DataSelector)
            {
                var nds = new ProjectTree.Node_DataSelector(original.Name, parent as ProjectTree.Node_TireDataSet, true);
                nds.TDSS.CopyFrom((original as ProjectTree.Node_DataSelector).TDSS.Copy());
                newNode = nds;
            }
            else if(original is ProjectTree.Node_MagicFormula)
            {
                var nmf = new ProjectTree.Node_MagicFormula(original.Name, parent as ProjectTree.Node_TireDataSet);
                nmf.MFFD.CopyFrom((original as ProjectTree.Node_MagicFormula).MFFD.Copy(), original.Parent == parent);
                newNode = nmf;
            }
            else if (original is ProjectTree.Node_RawTireData)
            {
                var nrt = new ProjectTree.Node_RawTireData(original.Name, parent as ProjectTree.Node_Project);
                nrt.RTDM.CopyFrom((original as ProjectTree.Node_RawTireData).RTDM);
                newNode = nrt;
            }
            foreach (var child in original.Children)
            {
                var node = CopyProjectTree(child, newNode);
                newNode.Children.Add(node);
                
            }
            return newNode;
        }

        static void LoadGraph(ZipArchive archive)
        {
            var node = UserControls.GraphViewer.TopNode.Load(archive, MainWindow.GraphTreeView);
            MainWindow.GraphTreeView.Nodes.Clear();
            MainWindow.GraphTreeView.Nodes.Add(node);
            node.Expand();
        }

        static void SaveGraph(ZipArchive archive)
        {
            (MainWindow.GraphTreeView.Nodes[0] as UserControls.GraphViewer.TopNode).Save(archive);
        }
    }
}
