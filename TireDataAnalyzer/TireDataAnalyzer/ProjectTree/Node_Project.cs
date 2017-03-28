using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace TireDataAnalyzer.ProjectTree
{
    public class Node_Project : ProjectTreeNode
    {
        public Node_Project(string name) : base(null)
        {
            if(name != null && name != "")
            {
                Name = name;
            }

        }
        public void AddRawTireData(string tireName)
        {
            new Node_RawTireData(tireName, this);
        }

        protected override bool OnLoad(ZipArchive archive, string directoryPath)
        {
            return true;
        }

        protected override bool OnSave(ZipArchive archive, string directoryPath)
        {
            return true;
        }

        protected override void OnUpdate()
        {


        }

        public List<Node_TireDataSet> GetTireDataSet()
        {
            var list = new List<Node_TireDataSet>();
            GetTireDataSet(this, list);
            return list;
        }

        private void GetTireDataSet(ProjectTreeNode node, List<Node_TireDataSet> list)
        {
            if (node is Node_TireDataSet)
                list.Add(node as Node_TireDataSet);
            foreach(var child in node.Children)
            {
                GetTireDataSet(child, list);
            }
        }

        public List<Node_MagicFormula> GetMagicFormula()
        {
            var list = new List<Node_MagicFormula>();
            GetMagicFormula(this, list);
            return list;
        }

        private void GetMagicFormula(ProjectTreeNode node, List<Node_MagicFormula> list)
        {
            if (node is Node_MagicFormula)
                list.Add(node as Node_MagicFormula);
            foreach (var child in node.Children)
            {
                GetMagicFormula(child, list);
            }
        }
    }
}
