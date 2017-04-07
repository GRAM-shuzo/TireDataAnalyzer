using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace TireDataAnalyzer.ProjectTree
{
    [Serializable]
    public abstract class ProjectTreeNode
    {
        [Serializable]
        public class SaveData
        {
            public string Name;
            public Guid ID;
            public string Class;
            public bool Expand;
            public UpdateState Updated = UpdateState.Normal;

        }
        SaveData savedata = new SaveData();

        public Guid ID {
            get
            {
                return savedata.ID;
            }
            private set
            {
                savedata.ID = value;
            }
        }
        public string Name
        {
            get
            {
                return savedata.Name;
            }
            set
            {
                var temp = savedata.Name;
                savedata.Name = value;

                if (temp != value)
                {
                    State = ProjectTreeState.Changed;
                    if (OnRename != null) OnRename(savedata.Name);
                }
            }
        }


        public ProjectTreeNode(ProjectTreeNode parent)
        {
            Parent = parent;
            Children = new List<ProjectTreeNode>();
            if(parent != null) parent.Children.Add(this);
            OnStateChanged += ChangeParentAndChildrenState;
            OnUpdateStateChanged += ChangeParentAndChildrenState;
            ID = Guid.NewGuid();
            if (parent != null)
                Updated = parent.Updated;
            else
                Updated = UpdateState.Normal;
            Expand = true;
            savedata.Class = GetClass();
        }
        public void DeleteSelf()
        {
            Parent.Children.Remove(this);
            this.State = ProjectTreeState.Deleted;
        }

        public void Update()
        {
            if(Updated == UpdateState.NotUpdated)
            {
                Parent.Update();
                OnUpdate();
                Updated = UpdateState.Normal;
                if(OnUpdated != null) OnUpdated();
            }
        }

        public void Save( ZipArchive archive, string directoryPath )
        {
            ZipArchiveEntry entry = StaticFunctions.GetEntity(directoryPath + "node.xml", archive, true);
            
            using (var stream = entry.Open())
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {

                    System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
                    //シリアル化し、XMLファイルに保存する
                    serializer.Serialize(writer, savedata);
                    
                }
            }
            if(OnSave(archive, directoryPath))
            {
                State = ProjectTreeState.NotChanged;
            }
           
        }
        protected abstract bool OnSave(ZipArchive archive, string directoryPath);

        public static ProjectTreeNode Load( ZipArchive archive,  string directoryPath, ProjectTreeNode parent )
        {
            ZipArchiveEntry entry = archive.GetEntry(directoryPath + "node.xml");
            ProjectTreeNode node;
            using (StreamReader reader = new StreamReader(entry.Open(),Encoding.UTF8))
            {
                System.Xml.Serialization.XmlSerializer serializer =
                   new System.Xml.Serialization.XmlSerializer(typeof(SaveData));
                var savedata = (SaveData)serializer.Deserialize(reader);
                if (savedata.Class == "Node_Project")
                {
                    node = new Node_Project(null);
                }
                else if (savedata.Class =="Node_RawTireData")
                {
                    node = new Node_RawTireData(null, parent as Node_Project);
                }
                else if (savedata.Class == "Node_DataSelector")
                {
                    node = new Node_DataSelector(null, parent as Node_TireDataSet, false);
                }
                else// if (savedata.Class == typeof(Node_MagicFormula))
                {
                    node = new Node_MagicFormula(null, parent as Node_TireDataSet);
                }
                node.savedata = savedata;
            } 
            if(node.OnLoad(archive, directoryPath))
            {
                node.State = ProjectTreeState.NotChanged;
            }
            return node;
        }
        protected abstract bool OnLoad(ZipArchive archive, string directoryPath);

        protected abstract void OnUpdate();
        public delegate void OnUpdatedDelegate();
        public OnUpdatedDelegate OnUpdated;
        public delegate void OnRenameDelegate(string name);
        public OnRenameDelegate OnRename;

        public ProjectTreeState State
        {
            get
            {
                return state;
            }
            protected set
            {
                state = value;
                if(OnStateChanged != null)
                {
                    OnStateChanged(value);
                }
            }
        }
        public UpdateState Updated
        {
            get
            {
                return savedata.Updated;
            }
            protected set
            {
                savedata.Updated = value;
                if (OnUpdateStateChanged != null)
                {
                    OnUpdateStateChanged(value);
                }
            }
        }
        [NonSerialized]
        ProjectTreeNode parent;
        public ProjectTreeNode Parent
        {
            get { return parent; }
            private set { parent = value; }
        }
        public List<ProjectTreeNode> Children
        {
            get;
            private set;
        }
        public bool Expand
        {
            get
            {
                return savedata.Expand;
            }
            set
            {
                savedata.Expand = value;
            }
        }


        public enum ProjectTreeState
        {
            NotChanged,
            Changed,
            Deleted
        }
        public enum UpdateState
        {
            Normal,
            NotUpdated
        }

        public delegate void OnStateChangedDelegate(ProjectTreeState newState);
        [field: NonSerialized]
        public OnStateChangedDelegate OnStateChanged;
        public delegate void OnUpdateStateChangedDelegate(UpdateState newState);
        [field: NonSerialized]
        public OnUpdateStateChangedDelegate OnUpdateStateChanged;
        public ProjectTreeState state = ProjectTreeState.Changed;

        void ChangeParentAndChildrenState(ProjectTreeState newState)
        {
            if (newState == ProjectTreeState.Deleted)
            {
                if(Parent != null)
                    Parent.State = ProjectTreeState.Changed;
            }
            if (newState == ProjectTreeState.Changed)
            {
                if (Parent != null)
                    Parent.State = ProjectTreeState.Changed;
            }
            
        }
        void ChangeParentAndChildrenState(UpdateState newState)
        {
            if (newState == UpdateState.NotUpdated)
            {
                foreach (var child in Children)
                {
                    child.Updated = UpdateState.NotUpdated;
                }
            }
        }
        protected void NoticeHaveToUpdateToChildren()
        {
            foreach (var child in Children)
            {
                child.Updated = UpdateState.NotUpdated;
            }
        }
        string GetClass()
        {
            if (this is Node_Project) return "Node_Project";
            else if (this is Node_RawTireData) return "Node_RawTireData";
            else if (this is Node_DataSelector) return "Node_DataSelector";
            else if (this is Node_MagicFormula) return "Node_MagicFormula";
            return null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
