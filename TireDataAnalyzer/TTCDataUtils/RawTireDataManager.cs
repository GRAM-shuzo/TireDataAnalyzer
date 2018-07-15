using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TTCDataUtils;
using System.IO.Compression;

namespace TTCDataUtils
{
    

    public class RawTireDataManager : IDataSet
    {

        public RawTireDataManager()
        {
            New();
        }

        public double TransientVelocityThresholdOnRaise = 0.2;
        public double TransientVelocityThresholdOnDrop = 1;
        public int Count(Table table)
        {
            return GetDataList(table).Count;
        }

        private List<TireData> CorneringTable
        {
            get
            {
                return saveData.CorneringTable;
            }
        }

        private List<TireData> DriveBrakeTable
        {
            get
            {
                return saveData.DriveBrakeTable;
            }
        }
        
        private List<TireData> TransientTable
        {
            get
            {
                return saveData.TransientTable;
            }
        }

        private List<TireData> SplitedTransientTable(int i)
        {
            
            return saveData.SplitedTransientTable(i);
        }

        public int SplitedTransientTableCount
        {
            get
            {
                return saveData.TransientTableIndexHolder.Count-1;
            }
        }


        public enum ManagerState
        {
            NotChanged,
            Changed
        }
        public ManagerState State
        {
            get;
            private set;
        }

        public delegate void OnStateChangedDelegate(ManagerState State);
        public OnStateChangedDelegate OnStateChanged;

        public void InsertData(List<TireData> dataList,  Table table, bool copying = false)
        {
            if(dataList.Count != 0)
            {
                var tgt = GetDataList(table);
                if(table == Table.TransientTable)
                {
                    if(!copying)dataList = ExtractTransientTest(dataList);
                    GetDataList(table).AddRange(dataList);
                }
                else
                {
                    GetDataList(table).AddRange(dataList);
                    GetDataList(table).Sort((j, k) => Guid.NewGuid().CompareTo(Guid.NewGuid()));
                }
                

                State = ManagerState.Changed;
                ChangeState(ManagerState.Changed, table);
                saveData.GetLimitData(table);
            }
            
        }
        public void InsertData(RawTireDataManager manager)
        {
            InsertData(manager.CorneringTable, Table.CorneringTable, true);
            InsertData(manager.DriveBrakeTable, Table.DriveBrakeTable, true);
            InsertData(manager.TransientTable, Table.TransientTable, true);
            int offset = saveData.TransientTable.Count;
            for (int i = 0; i< manager.saveData.TransientTableIndexHolder.Count; ++i)
            {
                saveData.TransientTableIndexHolder.Add(offset + manager.saveData.TransientTableIndexHolder[i]);
            }
        }

        public TireDataSet GetDataSet()
        {
            return saveData;
        }

        List<TireData> ExtractTransientTest(List<TireData> data)
        {
            List<TireData> rvalue = new List<TireData>() ;
            List<int> indexes = new List<int>();
            int offset = 0;
            if (saveData.TransientTableIndexHolder.Count == 0)
                indexes.Add(0);
            else
            {
                indexes.Add(saveData.TransientTable.Count);
                offset = saveData.TransientTable.Count;
            }
            bool raised = false;
            int end = 0;
            int start = 0;
            int temp = 0;
            for(int i=0; i<data.Count; ++i)
            {
                if (raised == false && data[i].V >= TransientVelocityThresholdOnDrop)
                {
                    raised = true;
                    temp = i;
                }
                if (raised == true && data[i].V <= TransientVelocityThresholdOnRaise)
                {
                    bool getend = false;
                    raised = false;
                    int j = i;
                    for(j = i; j > 0; --j)
                    {
                        if (data[j].V > TransientVelocityThresholdOnDrop && !getend)
                        {
                            end = j;
                            getend = true;
                        }
                        if(data[j].V < TransientVelocityThresholdOnRaise && j <= temp)
                        {
                            start = j;
                            break;
                        }

                    }
                    if (j == 0) continue;
                    var list = data.GetRange(start, end - start+1);
                    double t0 = list[0].ET;
                    for (int k = 0; k < list.Count; ++k)
                    {
                        list[k].ET -= t0;
                    }
                    rvalue.AddRange(list);
                    indexes.Add(offset+rvalue.Count);
                }
                
            }
            saveData.TransientTableIndexHolder.AddRange(indexes);
            return rvalue;

        }

        #region Private

        public List<TireData> GetDataList(Table table, int transientTestNum = -1)
        {
            switch(table)
            {
                case Table.CorneringTable:
                    return CorneringTable;
                case Table.DriveBrakeTable:
                    return DriveBrakeTable;
                case Table.TransientTable:
                    if(transientTestNum >= 0)
                    {
                        return SplitedTransientTable(transientTestNum);
                    }
                    else
                    {
                        return TransientTable;
                    }
                    
                default:
                    return null;
            }
        }

        private void ChangeState(ManagerState state, Table table)
        {
            State = state;
            var saveDataState = state == ManagerState.Changed ? TireDataSet.TireDataSetState.Changed : TireDataSet.TireDataSetState.NotChanged;
            switch (table)
            {
                case Table.CorneringTable:
                    saveData.CorneringTableState = saveDataState;
                    break;
                case Table.DriveBrakeTable:
                    saveData.DriveBrakeTableState = saveDataState;
                    break;
                case Table.TransientTable:
                    saveData.TransientTableState = saveDataState;
                    break;
                case Table.None:
                    saveData.CorneringTableState = saveDataState;
                    saveData.DriveBrakeTableState = saveDataState;
                    saveData.TransientTableState = saveDataState;
                    break;
            }

            if (OnStateChanged != null)
            {
                OnStateChanged(state);
            }
            
        }

        #endregion

        #region SaveAndLoad
        
        TireDataSet saveData;

        public void New()
        {
            saveData = new TireDataSet();
            ChangeState(ManagerState.Changed, Table.None);
        }

        public void Load(string fileName)
        {
            TireDataSet data = TireDataSet.Load(fileName);
            ChangeState(ManagerState.NotChanged, Table.None);
        }

        public void Load(Stream reader)
        {
            saveData= TireDataSet.Load(reader);
            ChangeState(ManagerState.NotChanged, Table.None);
        }

        public void Save(string fileName)
        {
            saveData.Save(fileName);
            ChangeState(ManagerState.NotChanged, Table.None);
        }

        public void Save(Stream writer)
        {
            saveData.Save(writer);
            ChangeState(ManagerState.NotChanged, Table.None);
        }

        public void CopyFrom(RawTireDataManager other)
        {
            saveData = other.saveData.Copy();
        }
        #endregion
    }
}
