using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using TTCDataUtils;

namespace TireDataAnalyzer.ProjectTree
{
    public class Node_RawTireData : Node_TireDataSet
    {
        string saveDataName = "RawTireData.rtd";
        RawTireDataManager rtdm;
        public RawTireDataManager RTDM { get { return rtdm; } }

        public Node_RawTireData(string tireName, Node_Project parent)
            : base(tireName,parent)
        {
            rtdm = new RawTireDataManager();
            rtdm.OnStateChanged += OnRTDMStateChanged;
            base.IDataSet = rtdm;
        }
        private void OnRTDMStateChanged(RawTireDataManager.ManagerState state)
        {
            if(state == RawTireDataManager.ManagerState.Changed)
            {
                this.State = ProjectTreeState.Changed;
                NoticeHaveToUpdateToChildren();
            }
        }

        public void AddDataSelector(string selectorName)
        {
            new Node_DataSelector(selectorName, this, true);
        }

        protected override bool OnSave(ZipArchive archive, string directoryPath)
        {
            ZipArchiveEntry entry = StaticFunctions.GetEntity(directoryPath + saveDataName, archive);
            using (var stream = entry.Open())
            {
                rtdm.Save(stream);
                return true;
            }
        }

        protected override bool OnLoad(ZipArchive archive, string directoryPath)
        {
            string filename = directoryPath + saveDataName;
            ZipArchiveEntry entry = archive.GetEntry(filename);
            if(entry != null)
            {
                using (var stream = entry.Open())
                {
                    rtdm.Load(stream);
                    return true;
                }
            }
            return false;
        }

        protected override void OnUpdate()
        {
            

        }

        public override IDataSet GetIDataSet()
        {
            return RTDM;
        }
    }
}
