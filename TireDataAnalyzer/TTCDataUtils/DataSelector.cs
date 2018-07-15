using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace TTCDataUtils
{



    public interface IDataSet
    {
        TireDataSet GetDataSet();
    }

    public class TireDataSetSelector : IDataSet
    {
        
        public TireDataSetSelector(IDataSet dataset)
        {
            idataset = dataset;
            CorneringDataSelector = new TireDataSelector();
            DriveBrakeDataSelector = new TireDataSelector();
            TransientDataSelector = new TransientTireDataSelector();
            Reset();
        }
        IDataSet idataset;
        public void Reset()
        {
            var set = idataset.GetDataSet();
            CorneringDataSelector.Reset(set.CorneringTable, set.MaxminSet.CorneringTableLimit);
            DriveBrakeDataSelector.Reset(set.DriveBrakeTable, set.MaxminSet.DriveBrakeTableLimit);
            TransientDataSelector.Reset(set.TransientTable, set.MaxminSet.TransientTableLimit, set.TransientTableIndexHolder);
            State = TireDataSetSelectorState.Changed;
        }
        public void Update()
        {
            var set = idataset.GetDataSet();
            CorneringDataSelector.Update(set.CorneringTable, set.MaxminSet.CorneringTableLimit);
            DriveBrakeDataSelector.Update(set.DriveBrakeTable, set.MaxminSet.DriveBrakeTableLimit);
            TransientDataSelector.Update(set.TransientTable, set.MaxminSet.TransientTableLimit, set.TransientTableIndexHolder);
            State = TireDataSetSelectorState.Changed;
        }
        public List<TireData> Target(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return idataset.GetDataSet().CorneringTable;
                case Table.DriveBrakeTable:
                    return idataset.GetDataSet().DriveBrakeTable;
                case Table.TransientTable:
                    return idataset.GetDataSet().TransientTable;
            }
            return null;
        }

        private TireDataSelector CorneringDataSelector;
        private TireDataSelector DriveBrakeDataSelector;
        private TransientTireDataSelector TransientDataSelector;

        public enum TireDataSetSelectorState
        {
            Changed,
            NotChanged
        }
        TireDataSetSelectorState state;
        public TireDataSetSelectorState State
        {
            get
            {
                return state;
            }
            private set
            {
                if (state != value)
                {
                    state = value;
                    if (TireDataSetSelectorStateChanged != null)
                    {
                        TireDataSetSelectorStateChanged(state);
                    }

                }
            }
        }
        public void ConfirmStateChanged(Table table)
        {
            this.State = TireDataSetSelectorState.Changed;
            GetSelector(table).ConfirmStateChanged();
        }
        public delegate void TireDataSetSelectorStateChangedDelegate(TireDataSetSelectorState newState);
        public TireDataSetSelectorStateChangedDelegate TireDataSetSelectorStateChanged;
        public delegate void ExtractedDataChangedDelegate(TireDataSetSelector selector, Table table);
        public ExtractedDataChangedDelegate ExtractedDataChanged;

        public TireDataMaxmin Maxmin(Table table)
        {
            return GetSelector(table).Maxmin.Copy();
        }
        public void ExtractData(Table table, int NumSearch = 0)
        {
            GetSelector(table).ExtractData(NumSearch);
        }
        public void ExtractData(int NumSearch = 0)
        {
            CorneringDataSelector.ExtractData(NumSearch);
            DriveBrakeDataSelector.ExtractData(NumSearch);
            TransientDataSelector.ExtractData(NumSearch);
        }
        public List<TireData> ExtractedData(Table table)
        {
            return GetSelector(table).ExtractedData;
        }
        public List<TireData> NotExtractedData(Table table)
        {
            return GetSelector(table).NotExtractedData;
        }
        public void AddConstrain(TireDataConstrain constrain, Table table)
        {
            GetSelector(table).AddConstrain(constrain);
            State = TireDataSetSelectorState.Changed;
        }
        public void RemoveConstrain(TireDataConstrain constrain, Table table)
        {
            GetSelector(table).RemoveConstrain(constrain);
            State = TireDataSetSelectorState.Changed;
        }
        public Dictionary<TireDataColumn, List<TireDataConstrain>> Constrains(Table table)
        {
            return GetSelector(table).Constrains;
        }
        public TireDataSet GetDataSet()
        {
            var result = new TireDataSet();
            result.CorneringTable = CorneringDataSelector.ExtractedData;
            result.DriveBrakeTable = DriveBrakeDataSelector.ExtractedData;
            result.TransientTable = TransientDataSelector.ExtractedData;
            result.TransientTableIndexHolder = TransientDataSelector.IndexHolder;

            result.MaxminSet.CorneringTableLimit = CorneringDataSelector.ExtractedDataMaxmin;
            result.MaxminSet.DriveBrakeTableLimit = DriveBrakeDataSelector.ExtractedDataMaxmin;
            result.MaxminSet.TransientTableLimit = TransientDataSelector.ExtractedDataMaxmin;
            return result;
        }
        public void Load(string fileName)
        {
            FileStream serializeFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Load(serializeFile);
        }

        public void Load(Stream reader)
        {
            using (ZipArchive archive = new ZipArchive(reader, ZipArchiveMode.Read, false))
            {
                var set = idataset.GetDataSet();
                var corneringEntry = archive.GetEntry("CorneringSelector");
                var driveBrakeEntry = archive.GetEntry("DriveBrakeSelector");
                var transientEntry = archive.GetEntry("TransientSelector");
                var tstate = TireDataSetSelectorState.NotChanged;
                using (Stream s = corneringEntry.Open())
                {
                    var corneringDataSelector = TireDataSelector.Load(s,set.CorneringTable,set.MaxminSet.CorneringTableLimit);
                    if (corneringDataSelector == null)
                    {
                        tstate = TireDataSetSelectorState.Changed;
                    }
                    else
                    {
                        CorneringDataSelector = corneringDataSelector;
                    }
                }
                using (Stream s = driveBrakeEntry.Open())
                {
                    var driveBrakeDataSelector = TireDataSelector.Load(s,set.DriveBrakeTable,set.MaxminSet.DriveBrakeTableLimit);
                    if (driveBrakeDataSelector == null)
                    {
                        tstate = TireDataSetSelectorState.Changed;
                    }
                    else
                    {
                        DriveBrakeDataSelector = driveBrakeDataSelector;
                    }
                }
                using (Stream s = transientEntry.Open())
                {
                    var transientDataSelector = TransientTireDataSelector.Load(s,set.TransientTable,set.MaxminSet.TransientTableLimit, set.TransientTableIndexHolder);
                    if (transientDataSelector == null)
                    {
                        tstate = TireDataSetSelectorState.Changed;
                    }
                    else
                    {
                        TransientDataSelector = transientDataSelector;
                    }
                }
                State = tstate;

            }
        }

        public void Save(string fileName)
        {
            FileStream serializeFile = new FileStream(fileName, FileMode.OpenOrCreate);
            Save(serializeFile);
        }

        public void Save(Stream writer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (ZipArchive archive = new ZipArchive(writer, ZipArchiveMode.Update, false))
            {
                var CorneringSelectorEntry = archive.GetEntry("CorneringSelector");
                if (CorneringSelectorEntry == null)
                {
                    CorneringSelectorEntry = archive.CreateEntry("CorneringSelector");
                }
                var DriveBrakeSelectorEntry = archive.GetEntry("DriveBrakeSelector");
                if (DriveBrakeSelectorEntry == null)
                {
                    DriveBrakeSelectorEntry = archive.CreateEntry("DriveBrakeSelector");
                }

                var TransientSelectorEntry = archive.GetEntry("TransientSelector");
                if (TransientSelectorEntry == null)
                {
                    TransientSelectorEntry = archive.CreateEntry("TransientSelector");
                }

                using (Stream s = CorneringSelectorEntry.Open())
                {
                    CorneringDataSelector.Save(s);
                }
                using (Stream s = DriveBrakeSelectorEntry.Open())
                {
                    DriveBrakeDataSelector.Save(s);
                }
                using (Stream s = TransientSelectorEntry.Open())
                {
                    TransientDataSelector.Save(s);
                }
            }
            State = TireDataSetSelectorState.NotChanged;

        }

        public TireDataSetSelector Copy()
        {
            var temp = new TireDataSetSelector(idataset);
            temp.CorneringDataSelector = CorneringDataSelector.Copy();
            temp.DriveBrakeDataSelector = DriveBrakeDataSelector.Copy();
            temp.TransientDataSelector = TransientDataSelector.Copy();
            temp.State = State;
            return temp;
        }
        public void CopyFrom(TireDataSetSelector other)
        {
            CorneringDataSelector = other.CorneringDataSelector;
            DriveBrakeDataSelector = other.DriveBrakeDataSelector;
            TransientDataSelector = other.TransientDataSelector;
            State = TireDataSetSelectorState.Changed;
        }


        private TireDataSelector GetSelector(Table table)
        {
            switch(table)
            {
                case Table.CorneringTable:
                    return CorneringDataSelector;
                case Table.DriveBrakeTable:
                    return DriveBrakeDataSelector;
                case Table.TransientTable:
                    return TransientDataSelector;
            }
            return null;
        }
    }

    [Serializable]
    public class TireDataSelector
    {
        public TireDataSelector()
        {
            foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
            {
                if (column == TireDataColumn.NT) continue;
                Constrains[column] = new List<TireDataConstrain>();
            }
            ExtractedData = new List<TireData>();
            NotExtractedData = new List<TireData>();
        }
        public TireDataSelector(TireDataSelector other)
        {
            Target = other.Target;
            //if(Maxmin != null )
            Maxmin = other.Maxmin;
            Constrains = StaticFunctions.DeepCopy(other.Constrains);
            ExtractedData = new List<TireData>();
            NotExtractedData = new List<TireData>();
            ExtractData(0);
        }
        public void Reset(List<TireData> target, TireDataMaxmin maxmin)
        {
            Target = target;
            Maxmin = maxmin;

            foreach (var kv in Constrains)
            {
                kv.Value.Clear();
            }
            State = TireDataSelectorState.Changed;
        }

        public void Update(List<TireData> target, TireDataMaxmin maxmin, int NumSearch = 0)
        {
            Target = target;
            Maxmin = maxmin;
            ExtractData(NumSearch);
        }

        [NonSerialized()]
        protected List<TireData> Target;
        [NonSerialized()]
        public TireDataMaxmin Maxmin;

        public Dictionary<TireDataColumn, List<TireDataConstrain>> Constrains = new Dictionary<TireDataColumn, List<TireDataConstrain>>();



        public List<TireData> ExtractedData;
        public TireDataMaxmin ExtractedDataMaxmin;
        [NonSerialized()]
        public List<TireData> NotExtractedData;

        public enum TireDataSelectorState
        {
            Changed,
            NotChanged
        }
        public TireDataSelectorState State
        {
            get;
            set;
        }
        public void ConfirmStateChanged()
        {
            State = TireDataSelectorState.Changed;
        }

        public void AddConstrain(TireDataConstrain constrain)
        {
            Constrains[constrain.Column].Add(constrain);
        }
        public void RemoveConstrain(TireDataConstrain constrain)
        {
            Constrains[constrain.Column].Remove(constrain);
        }

        static public TireDataSelector Load(Stream reader, List<TireData> target, TireDataMaxmin maxmin)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as TireDataSelector;
            if (data != null)
            {
                data.NotExtractedData = new List<TireData>();
                data.Maxmin = maxmin;
                data.Target = target;
                //data.Update(target, maxmin);
                data.State = TireDataSelectorState.NotChanged;
            }
            return data;
        }
        virtual public void Save(Stream writer)
        {
            if(State == TireDataSelectorState.Changed)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(writer, this);
                State = TireDataSelectorState.NotChanged;
            }
            
        }
        virtual public void ExtractData( int NumSearch )
        {
            var maxmin = new TireDataMaxmin();
            ExtractedData.Clear();
            NotExtractedData.Clear();

            int i = 0;
            foreach (var data in Target)
            {
                /*
                bool add = true;
                foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
                {
                    if (column == TireDataColumn.NT) continue;



                    bool add_EvalNot = true;
                    bool add_EvalOrd = false;
                    if (Constrains[column].Count != 0)
                    {
                        foreach (var constrain in Constrains[column])
                        {
                            if(!constrain.Not)
                            {
                                add_EvalOrd = add_EvalOrd || constrain.Evaluate(data);
                            }
                            else
                            {
                                add_EvalNot = constrain.Evaluate(data);
                            }   
                            if (!add_EvalNot) break;
                        }
                    }
                    else
                    {
                        add_EvalNot = true;
                        add_EvalOrd = true;
                    }
                    add = add && (add_EvalNot && add_EvalOrd);
                    if (!add) break;
                }




                if (add)
                {
                    ExtractedData.Add(data);
                }
                else
                {
                    NotExtractedData.Add(data);
                }
                */
                if (NumSearch > 0 && i >= NumSearch) break;
                ++i;
                bool add = true;
                foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
                {
                    if (column == TireDataColumn.NT) continue;
                    bool remove = false;
                    foreach (var constrain in Constrains[column])
                    {
                        if (!constrain.Not) continue;
                        if (!constrain.Evaluate(data))
                        {
                            remove = true;
                            break;
                        }
                    }
                    if (remove == true)
                    {
                        add = false;
                        break;
                    }
                    if (!remove)
                    {
                        remove = true;
                        int counttemp = 0;

                        if (column == TireDataColumn.NT) continue;
                        foreach (var constrain in Constrains[column])
                        {
                            if (constrain.Not) continue;
                            ++counttemp;
                            if (constrain.Evaluate(data))
                            {
                                remove = false;
                                break;
                            }
                        }

                        if (counttemp == 0) remove = false;
                    }
                    add = add && !remove;
                }
                

                if (add)
                {
                    ExtractedData.Add(data);
                }
                else
                {
                    NotExtractedData.Add(data);
                }
            }
            State = TireDataSelectorState.Changed;
            ExtractedDataMaxmin = StaticFunctions.GetLimitData(ExtractedData);
        }

        virtual public TireDataSelector Copy()
        {
            return new TireDataSelector(this);
        }
    }

    [Serializable]
    public class TransientTireDataSelector
        : TireDataSelector
    {
        List<int> IndexHolderParent;
        List<int> indexHolder_ = new List<int>();
        public List<int> IndexHolder
        {
            get { return indexHolder_; } private set { indexHolder_ = value; }
        }
        public List<TireData> SplitedTransientTable(int i)
        {
            if (i >= IndexHolderParent.Count)
                i = IndexHolderParent.Count - 1;
            int lhs = IndexHolderParent[i];
            int rhs = IndexHolderParent[i + 1];
            return Target.GetRange(lhs, rhs - lhs );
        }

        public TransientTireDataSelector()
           :base()
        {
        }
        public TransientTireDataSelector(TransientTireDataSelector other)
            :base()
        {
            Target = other.Target;
            //if(Maxmin != null )
            Maxmin = other.Maxmin;
            Constrains = StaticFunctions.DeepCopy(other.Constrains);
            ExtractedData = new List<TireData>();
            NotExtractedData = new List<TireData>();
            IndexHolderParent = StaticFunctions.DeepCopy(other.IndexHolderParent);
            indexHolder_ = StaticFunctions.DeepCopy(other.indexHolder_);
            ExtractData(0);
        }
        public void Reset(List<TireData> target, TireDataMaxmin maxmin, List<int> indexHolder)
        {
            base.Reset(target, maxmin);
            IndexHolderParent = indexHolder;
        }

        public void Update(List<TireData> target, TireDataMaxmin maxmin, List<int> indexHolder, int NumSearch = 0)
        {
            Target = target;
            Maxmin = maxmin;
            IndexHolderParent = indexHolder;
            ExtractData(NumSearch);
        }
        
        static public TransientTireDataSelector Load(Stream reader, List<TireData> target, TireDataMaxmin maxmin, List<int> indexHolder)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as TransientTireDataSelector;
            if (data != null)
            {
                data.NotExtractedData = new List<TireData>();
                data.Maxmin = maxmin;
                data.Target = target;
                data.IndexHolderParent = indexHolder;
                //data.Update(target, maxmin);
                data.State = TireDataSelectorState.NotChanged;
            }
            return data;
        }
        public override void Save(Stream writer)
        {
            if (State == TireDataSelectorState.Changed)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(writer, this);
                State = TireDataSelectorState.NotChanged;
            }

        }
        public override void ExtractData(int NumSearch)
        {
            var maxmin = new TireDataMaxmin();
            ExtractedData.Clear();
            NotExtractedData.Clear();
            IndexHolder.Clear();
            IndexHolder.Add(0);
            for( int i=0; i<IndexHolderParent.Count; ++i)
            {
                if (NumSearch > 0 && i >= NumSearch) break;
                var list = SplitedTransientTable(i);
                bool add = true;
                foreach (var data in list)
                {

                    foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
                    {
                        if (column == TireDataColumn.NT) continue;
                        bool remove = false;
                        foreach (var constrain in Constrains[column])
                        {
                            if (!constrain.Not) continue;
                            if (!constrain.Evaluate(data))
                            {
                                remove = true;
                                break;
                            }
                        }
                        if (remove == true)
                        {
                            add = false;
                            break;
                        }
                        if (!remove)
                        {
                            remove = true;
                            int counttemp = 0;

                            if (column == TireDataColumn.NT) continue;
                            foreach (var constrain in Constrains[column])
                            {
                                if (constrain.Not) continue;
                                ++counttemp;
                                if (constrain.Evaluate(data))
                                {
                                    remove = false;
                                    break;
                                }
                            }

                            if (counttemp == 0) remove = false;
                        }
                        add = add && !remove;
                    }
                }


                if (add)
                {
                    ExtractedData.AddRange(list);
                    IndexHolder.Add(ExtractedData.Count);
                }
                else
                {
                    NotExtractedData.AddRange(list);
                }
            }


            State = TireDataSelectorState.Changed;
            ExtractedDataMaxmin = StaticFunctions.GetLimitData(ExtractedData);
        }

        new public TransientTireDataSelector Copy()
        {
            return new TransientTireDataSelector(this);
        }
    }

    [Serializable]
    public  class TireDataConstrain
    {
        public TireDataConstrain(string name,TireDataColumn column,  double max, double min, bool not = false)
        {
            Name = name;
            Max = max;
            Min = min;
            Not = not;
            Column = column;
        }
        public double Max;
        public double Min;
        public bool Not;
        public TireDataColumn Column { get; private set; }
        public string Name { get; set; }
        public bool Evaluate(TireData data)
        {
            double value = data[Column];
            return Evaluate(value);
        }
        public bool Evaluate(double value)
        {
            if (!Not)
            {
                return Min <= value && value <= Max;
            }
            else
            {
                return value <= Min || Max <= value;
            }
        }

        public TireDataConstrain Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }
        public void CopyFrom(TireDataConstrain other)
        {
            this.Max = other.Max;
            this.Min = other.Min;
            this.Not = other.Not;
            this.Column = other.Column;
            this.Name = other.Name;
        }
    }
}
