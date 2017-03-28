using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCDataUtils;

namespace TireDataAnalyzer.ProjectTree
{
    public class Node_DataSelector : Node_TireDataSet
    {
        string saveDataName = "DataSelector.ds";
        public Node_DataSelector(string name, Node_TireDataSet parent, bool extract)
            : base(name,parent)
        {
            TDSS = new TireDataSetSelector(parent.GetIDataSet());
            if (extract) TDSS.ExtractData();
            TDSS.TireDataSetSelectorStateChanged += OnTDSSStateChanged;
            base.IDataSet = TDSS;
        }
        private void OnTDSSStateChanged(TireDataSetSelector.TireDataSetSelectorState state)
        {
            if (state == TireDataSetSelector.TireDataSetSelectorState.Changed)
            {
                this.State = ProjectTreeState.Changed;
                NoticeHaveToUpdateToChildren();
            }
        }

        public TireDataSetSelector TDSS { get; private set; }      
        protected override bool OnLoad(ZipArchive archive, string directoryPath)
        {
            string filename = directoryPath + saveDataName;
            ZipArchiveEntry entry = archive.GetEntry(filename);
            if (entry != null)
            {
                using (var stream = entry.Open())
                {
                    TDSS.Load(stream);
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
                TDSS.Save(stream);
                return true;
            }
        }

        protected override void OnUpdate()
        {
            TDSS.Update();
        }

        public override IDataSet GetIDataSet()
        {
            return TDSS;
        }
    }
}
