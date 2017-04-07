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
    public class Node_MagicFormula : ProjectTreeNode
    {
        string saveDataName = "MagicFormulaFittingDelegate.mffd";
        public Node_MagicFormula(string mfName, Node_TireDataSet parent)
            :base(parent)
        {
            if (mfName != null && mfName != "")
            {
                Name = mfName;
            }
            MFFD = new MagicFormulaFittingDelegate(Name,parent.IDataSet);
            MFFD.ValueChanged += MFFDValueChanged;
        }
        public MagicFormulaFittingDelegate MFFD
        {
            get; private set;
        }

        void MFFDValueChanged()
        {
            this.State = ProjectTreeState.Changed;
            NoticeHaveToUpdateToChildren();
        }
       

        

        protected override bool OnLoad(ZipArchive archive, string directoryPath)
        {
            string filename = directoryPath + saveDataName;
            ZipArchiveEntry entry = archive.GetEntry(filename);
            if (entry != null)
            {
                using (var stream = entry.Open())
                {
                    var mffd  = MagicFormulaFittingDelegate.Load(stream);
                    mffd.ValueChanged += MFFDValueChanged;
                    mffd.SetIDataSetOnLoad(MFFD);
                    MFFD = mffd;
                    return true;
                }
            }
            return false;
        }

        protected override bool OnSave(ZipArchive archive, string directoryPath)
        {
            ZipArchiveEntry entry = StaticFunctions.GetEntity(directoryPath + saveDataName, archive);
            using (var stream = entry.Open())
            {
                MFFD.Save(stream);
                return true;
            }
        }

        protected override void OnUpdate()
        {


        }
    }
}
