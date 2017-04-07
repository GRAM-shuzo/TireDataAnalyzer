using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using TTCDataUtils;

namespace TireDataAnalyzer.ProjectTree
{
    [Serializable]
    public abstract class Node_TireDataSet:ProjectTreeNode
    {
        public Node_TireDataSet(string name, ProjectTreeNode parent)
            :base(parent)
        {
            if (name != null && name != "")
            {
                Name = name;
            }
        }

        public IDataSet IDataSet { get; protected set; }

        public abstract IDataSet GetIDataSet();
    }
}
