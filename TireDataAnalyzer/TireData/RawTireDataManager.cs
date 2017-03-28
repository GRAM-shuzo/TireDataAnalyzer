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

        public void InsertData(List<TireData> dataList,  Table table)
        {
            if(dataList.Count != 0)
            {
                var tgt = GetDataList(table);
                GetDataList(table).AddRange(dataList);
                GetDataList(table).Sort((j,k) => Guid.NewGuid().CompareTo(Guid.NewGuid()));

                State = ManagerState.Changed;
                ChangeState(ManagerState.Changed, table);
                saveData.GetLimitData(table);
            }
            
        }
        public void InsertData(RawTireDataManager manager)
        {
            InsertData(manager.CorneringTable, Table.CorneringTable);
            InsertData(manager.DriveBrakeTable, Table.DriveBrakeTable);
            InsertData(manager.TransientTable, Table.TransientTable);
        }

        public TireDataSet GetDataSet()
        {
            return saveData;
        }
        

        #region Private

        public List<TireData> GetDataList(Table table)
        {
            switch(table)
            {
                case Table.CorneringTable:
                    return CorneringTable;
                case Table.DriveBrakeTable:
                    return DriveBrakeTable;
                case Table.TransientTable:
                    return TransientTable;
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

        #endregion
    }
}
