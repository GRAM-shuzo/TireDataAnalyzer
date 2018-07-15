using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace TTCDataUtils
{
    static public class StaticFunctions
    {
        public static T DeepCopy<T>(T target)
        {

            T result;
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T)b.Deserialize(mem);
            }
            return result;

        }
        public static  TireDataMaxmin GetLimitData(List<TireData> list)
        {
            var maxmin = new TireDataMaxmin();
            maxmin.count = list.Count;
            foreach (var data in list)
            {
                for (int i = 0; i < TireData.Count(); ++i)
                {
                    if (maxmin.Max[(TireDataColumn)i] < data[(TireDataColumn)i]) maxmin.Max[(TireDataColumn)i] = data[(TireDataColumn)i];
                    if (maxmin.Min[(TireDataColumn)i] > data[(TireDataColumn)i]) maxmin.Min[(TireDataColumn)i] = data[(TireDataColumn)i];
                    maxmin.Mean[(TireDataColumn)i] += data[(TireDataColumn)i];
                }
            }
            for (int i = 0; i < TireData.Count(); ++i)
            {
                maxmin.Mean[(TireDataColumn)i] /= list.Count();
            }
            return maxmin;
        }
    }

    public enum TireDataColumn
    {
        ET = 0,
        V,
        N,
        SA,
        IA,
        RL,
        RE,
        P,
        FX,
        FY,
        FZ,
        MX,
        MZ,
        NFX,
        NFY,
        RST,
        TSTI,
        TSTC,
        TSTO,
        AMBTMP,
        SR,
        SL,
        NT,
    }



    [Serializable]
    public class TireData
    {
        /*
        AMBTMP  degC or degF    Ambient room temperature
        ET      sec             Elapsed time for the test
        FX      N or lb         Longitudinal Force
        FY      N or lb         Lateral Force
        FZ      N or lb         Normal Load
        IA      deg             Inclination Angle
        MX      N-m or lb-ft    Overturning Moment
        MZ      N-m or lb-ft    Aligning Torque
        N       rpm             Wheel rotational speed
        NFX     unitless        Normalized longitudinal force (FX/FZ)
        NFY     unitless        Normalized lateral force (FY/FZ)
        P       kPa or psi      Tire pressure
        RE      cm or in        Effective Radius
        RL      cm or in        Loaded Radius
        RST     degC or degF    Road surface temperature
        SA      deg             Slip Angle
        SL      unitless        Slip Ratio based on RE (such that SL=0 gives FX=0)
        SR      unitless        Slip Ratio based on RL (used for Calspan machine control, SR=0 does not give FX=0)
        TSTC    degC or degF    Tire Surface Temperature--Center
        TSTI    degC or degF    Tire Surface Temperature--Inboard
        TSTO    degC or degF    Tire Surface Temperature--Outboard
        V       kph or mph      Road Speed
        */
        public double ET
        {
            get { return this[TireDataColumn.ET]; }
            set { this[TireDataColumn.ET] = value; }
        }
        public double V
        {
            get { return this[TireDataColumn.V]; }
            set { this[TireDataColumn.V] = value; }
        }
        public double N
        {
            get { return this[TireDataColumn.N]; }
            set { this[TireDataColumn.N] = value; }
        }
        public double SA
        {
            get { return this[TireDataColumn.SA]; }
            set { this[TireDataColumn.SA] = value; }
        }
        public double IA
        {
            get { return this[TireDataColumn.IA]; }
            set { this[TireDataColumn.IA] = value; }
        }
        public double RL
        {
            get { return this[TireDataColumn.RL]; }
            set { this[TireDataColumn.RL] = value; }
        }
        public double RE
        {
            get { return this[TireDataColumn.RE]; }
            set { this[TireDataColumn.RE] = value; }
        }
        public double P
        {
            get { return this[TireDataColumn.P]; }
            set { this[TireDataColumn.P] = value; }
        }
        public double FX
        {
            get { return this[TireDataColumn.FX]; }
            set { this[TireDataColumn.FX] = value; }
        }
        public double FY
        {
            get { return this[TireDataColumn.FY]; }
            set { this[TireDataColumn.FY] = value; }
        }
        public double FZ
        {
            get { return this[TireDataColumn.FZ]; }
            set { this[TireDataColumn.FZ] = value; }
        }
        public double MX
        {
            get { return this[TireDataColumn.MX]; }
            set { this[TireDataColumn.MX] = value; }
        }
        public double MZ
        {
            get { return this[TireDataColumn.MZ]; }
            set { this[TireDataColumn.MZ] = value; }
        }
        public double NFX
        {
            get { return this[TireDataColumn.NFX]; }
            set { this[TireDataColumn.NFX] = value; }
        }
        public double NFY
        {
            get { return this[TireDataColumn.NFY]; }
            set { this[TireDataColumn.NFY] = value; }
        }
        public double RST
        {
            get { return this[TireDataColumn.RST]; }
            set { this[TireDataColumn.RST] = value; }
        }
        public double TSTI
        {
            get { return this[TireDataColumn.TSTI]; }
            set { this[TireDataColumn.TSTI] = value; }
        }
        public double TSTC
        {
            get { return this[TireDataColumn.TSTC]; }
            set { this[TireDataColumn.TSTC] = value; }
        }
        public double TSTO
        {
            get { return this[TireDataColumn.TSTO]; }
            set { this[TireDataColumn.TSTO] = value; }
        }
        public double TST
        {
            get
            {
                double tst = this[TireDataColumn.TSTO] + this[TireDataColumn.TSTC] + this[TireDataColumn.TSTI];
                return tst / 3.0;
            }
        }
        public double AMBTMP
        {
            get { return this[TireDataColumn.AMBTMP]; }
            set { this[TireDataColumn.AMBTMP] = value; }
        }
        public double SR
        {
            get { return this[TireDataColumn.SR]; }
            set { this[TireDataColumn.SR] = value; }
        }
        public double SL
        {
            get { return this[TireDataColumn.SL]; }
            set { this[TireDataColumn.SL] = value; }
        }
        public double NT
        {
            get { return this[TireDataColumn.NT]; }
        }
        public double this[TireDataColumn c]
        {
            set
            {
                int i = (int)c;
                if (i < 0 || i >= _data.Count())
                    throw new IndexOutOfRangeException();
                this._data[i] = value;
            }
            get
            {
                if (c == TireDataColumn.NT)
                {
                    return MZ / FY * 1000;
                }
                int i = (int)c;
                if (i < 0 || i > _data.Count())
                    throw new IndexOutOfRangeException();

                return this._data[i];
            }
        }

        const int count = 22; //項目数
        private double[] _data = new double[count];
        public static int Count()
        {
            return count;
        }
        public TireData Copy()
        {
            var value = new TireData();
            _data.CopyTo(value._data, 0);
            return value;
        }

        public static Dictionary<TireDataColumn, string> Label = new Dictionary<TireDataColumn, string>()
        {
            {TireDataColumn.ET, "経過時間(s)"},
            {TireDataColumn.V, "速度(km/h)"},
            {TireDataColumn.N, "回転速度(rpm)"},
            {TireDataColumn.SA, "スリップ角(°)"},
            {TireDataColumn.IA, "キャンバ角(°)"},
            {TireDataColumn.RL, "タイヤ径(mm)"},
            {TireDataColumn.RE, "有効タイヤ径(mm)"},
            {TireDataColumn.P, "空気圧(kPa)"},
            {TireDataColumn.FX, "前後力(N)"},
            {TireDataColumn.FY, "横力(N)"},
            {TireDataColumn.FZ, "垂直抗力(N)"},
            {TireDataColumn.MX, "キャンバ方向モーメント(N・m)"},
            {TireDataColumn.MZ, "セルフアライニングトルク(N・m)"},
            {TireDataColumn.NFX, "正規化前後力(FX/FZ)"},
            {TireDataColumn.NFY, "正規化横力(FY/FZ)"},
            {TireDataColumn.RST, "路面温度(℃)"},
            {TireDataColumn.TSTI, "タイヤ内側温度(℃)"},
            {TireDataColumn.TSTC, "タイヤ中央温度(℃)"},
            {TireDataColumn.TSTO, "タイヤ外側温度(℃)"},
            {TireDataColumn.AMBTMP, "雰囲気温度(℃)"},
            {TireDataColumn.SR, "スリップ率(タイヤ径基準)"},
            {TireDataColumn.SL, "スリップ率(有効半径基準)"},
            {TireDataColumn.NT, "ニューマチックトレール(mm)"},
        };

        public static Dictionary<TireDataColumn, string> ColumnFormal = new Dictionary<TireDataColumn, string>()
        {
            {TireDataColumn.ET, "Elapsed Time"},
            {TireDataColumn.V, "Velocity"},
            {TireDataColumn.N, "Wheel Rotational Speed"},
            {TireDataColumn.SA, "Slip Angle"},
            {TireDataColumn.IA, "Inclination Angle"},
            {TireDataColumn.RL, "Loaded Radius"},
            {TireDataColumn.RE, "Effective Radius"},
            {TireDataColumn.P, "Tire pressure"},
            {TireDataColumn.FX, "Longitudinal Force"},
            {TireDataColumn.FY, "Lateral Force"},
            {TireDataColumn.FZ, "Normal Load"},
            {TireDataColumn.MX, "Overturning Moment"},
            {TireDataColumn.MZ, "Aligning Torque"},
            {TireDataColumn.NFX, "Normalized longitudinal force (FX/FZ)"},
            {TireDataColumn.NFY, "Normalized lateral force (FY/FZ)"},
            {TireDataColumn.RST, "Road surface temperature"},
            {TireDataColumn.TSTI, "Tire Surface Temperature--Inboard"},
            {TireDataColumn.TSTC, "Tire Surface Temperature--Center"},
            {TireDataColumn.TSTO, "Tire Surface Temperature--Outboard"},
            {TireDataColumn.AMBTMP, "Ambient room temperature"},
            {TireDataColumn.SR, "Slip Ratio based on RL"},
            {TireDataColumn.SL, "Slip Ratio based on RE"},
            {TireDataColumn.NT, "Pneumatic trail"},
        };

        public static TireData Resolution()
        {
            TireData data = new TireData();
            data.ET = 0.02;
            data.V = 0.05;
            data.N = 1;
            data.SA = 0.01;
            data.IA = 0.01;
            data.RL = 0.01;
            data.RE = 0.1;
            data.P = 0.1;
            data.FX = 1;
            data.FY = 1;
            data.FZ = 1;
            data.MX = 0.1;
            data.MZ = 0.1;
            data.NFX = 0.01;
            data.NFY = 0.01;
            data.RST = 0.1;
            data.TSTC = 0.1;
            data.TSTI = 0.1;
            data.TSTO = 0.1;
            data.AMBTMP = 0.1;
            data.SR = 0.01;
            data.SL = 0.01;
            return data;
        }

    }

    [Serializable]
    public class TireDataMaxmin
    {
        public int count;
        public TireData Max = new TireData();
        public TireData Min = new TireData();
        public TireData Mean = new TireData();
        public double MaxValue(TireDataColumn column)
        {
            return Max[column];
        }
        public double MinValue(TireDataColumn column)
        {
            return Min[column];
        }
        public double MeanValue(TireDataColumn column)
        {
            return Mean[column];
        }
        public TireDataMaxmin()
        {
            Max = new TireData();
            Min = new TireData();
            Mean = new TireData();
            for(int i = 0; i< TireData.Count(); ++i)
            {
                Max[(TireDataColumn)i] = double.MinValue;
                Min[(TireDataColumn)i] = double.MaxValue;
                Mean[(TireDataColumn)i] = 0;
            }
        }

        public TireDataMaxmin Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }
    }


    [Serializable]
    public class TireDataMaxminSet
    {
        public TireDataMaxmin CorneringTableLimit = new TireDataMaxmin();
        public TireDataMaxmin DriveBrakeTableLimit = new TireDataMaxmin();
        public TireDataMaxmin TransientTableLimit = new TireDataMaxmin();
        public TireDataMaxmin Limit(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return CorneringTableLimit;
                case Table.DriveBrakeTable:
                    return DriveBrakeTableLimit;
                case Table.StaticTable:
                    return TireDataMaxminMerge(CorneringTableLimit, DriveBrakeTableLimit);
                case Table.TransientTable:
                    return TransientTableLimit;
                default:
                    return null;
            }
        }
        public TireDataMaxminSet Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }
        public static TireDataMaxmin TireDataMaxminMerge(TireDataMaxmin lh, TireDataMaxmin rh)
        {
            var value = new TireDataMaxmin();
            foreach (TireDataColumn column in Enum.GetValues(typeof(TireDataColumn)))
            {
                if (column == TireDataColumn.NT) continue;
                value.Max[column] = Math.Max(lh.Max[column], rh.Max[column]);
                value.Min[column] = Math.Min(lh.Min[column], rh.Min[column]);
                value.Mean[column] = (lh.Min[column] * lh.count + rh.Min[column] * rh.count) / (lh.count+rh.count);
            }
            return value;
        }
    }

    public enum Table
    {
        CorneringTable,
        DriveBrakeTable,
        StaticTable,
        TransientTable,
        None
    }

    [Serializable]
    public class TireDataSet
    {
        public enum TireDataSetState
        {
            Changed,
            NotChanged
        }


        public TireDataSetState CorneringTableState = TireDataSetState.Changed;
        public TireDataSetState DriveBrakeTableState = TireDataSetState.Changed;
        public TireDataSetState TransientTableState = TireDataSetState.Changed;

        public List<TireData> CorneringTable = new List<TireData>();
        public List<TireData> DriveBrakeTable = new List<TireData>();
        public List<TireData> TransientTable = new List<TireData>();
        public List<int> TransientTableIndexHolder = new List<int>();

        public List<TireData> SplitedTransientTable(int i)
        {
            if (i >= TransientTableIndexHolder.Count)
                i = TransientTableIndexHolder.Count - 1;
            int lhs = TransientTableIndexHolder[i];
            int rhs = TransientTableIndexHolder[i + 1];
            return TransientTable.GetRange(lhs, rhs - lhs);
        }


        public TireDataMaxminSet MaxminSet = new TireDataMaxminSet();

        static public TireDataSet Load(string fileName)
        {
            FileStream serializeFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return Load(serializeFile);
        }

        static public TireDataSet Load(Stream reader)
        {
            var data = new TireDataSet();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (ZipArchive archive = new ZipArchive(reader, ZipArchiveMode.Read, false))
            {
                var corneringTableEntry = archive.GetEntry("CorneringTable");
                var driveBrakeTableEntry = archive.GetEntry("DriveBrakeTable");
                var transientTableEntry = archive.GetEntry("TransientTable");
                var MaxminEntry = archive.GetEntry("Maxmin");

                using (Stream s = corneringTableEntry.Open())
                {
                    data.CorneringTable = binaryFormatter.Deserialize(s) as List<TireData>;
                    if (data.CorneringTable == null) data.CorneringTable = new List<TireData>();
                }
                using (Stream s = driveBrakeTableEntry.Open())
                {
                    data.DriveBrakeTable = binaryFormatter.Deserialize(s) as List<TireData>;
                    if (data.DriveBrakeTable == null) data.DriveBrakeTable = new List<TireData>();
                }
                using (Stream s = transientTableEntry.Open())
                {
                    data.TransientTable = binaryFormatter.Deserialize(s) as List<TireData>;
                    if (data.TransientTable == null) data.TransientTable = new List<TireData>();
                }
                if (MaxminEntry != null)
                {
                    using (Stream s = MaxminEntry.Open())
                    {
                        data.MaxminSet = binaryFormatter.Deserialize(s) as TireDataMaxminSet;
                        data.GetLimitDataAll();
                        if (data.MaxminSet == null) data.GetLimitDataAll();
                    }
                }
                else
                {
                    data.GetLimitDataAll();
                }
            }

            return data;
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
                var corneringTableEntry = archive.GetEntry("CorneringTable");
                if (corneringTableEntry == null)
                {
                    corneringTableEntry = archive.CreateEntry("CorneringTable");
                }
                var driveBrakeTableEntry = archive.GetEntry("DriveBrakeTable");
                if (driveBrakeTableEntry == null)
                {
                    driveBrakeTableEntry = archive.CreateEntry("DriveBrakeTable");
                }
                var transientTableEntry = archive.GetEntry("TransientTable");
                if (transientTableEntry == null)
                {
                    transientTableEntry = archive.CreateEntry("TransientTable");
                }
                var maxminEntry = archive.GetEntry("Maxmin");
                if (maxminEntry == null)
                {
                    maxminEntry = archive.CreateEntry("Maxmin");
                }

                if (CorneringTableState == TireDataSetState.Changed)
                    using (Stream s = corneringTableEntry.Open())
                    {
                        binaryFormatter.Serialize(s, CorneringTable);
                    }

                if (DriveBrakeTableState == TireDataSetState.Changed)
                    using (Stream s = driveBrakeTableEntry.Open())
                    {
                        binaryFormatter.Serialize(s, DriveBrakeTable);
                    }

                if (TransientTableState == TireDataSetState.Changed)
                    using (Stream s = transientTableEntry.Open())
                    {
                        binaryFormatter.Serialize(s, TransientTable);
                    }
                using (Stream s = maxminEntry.Open())
                {
                    binaryFormatter.Serialize(s, MaxminSet);
                }

            }
        }

        public TireDataSet Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }

        public List<TireData> GetDataList(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    return CorneringTable;
                case Table.DriveBrakeTable:
                    return DriveBrakeTable;
                case Table.StaticTable:
                    var list = new List<TireData>(CorneringTable.Count + DriveBrakeTable.Count);
                    list.AddRange(CorneringTable);
                    list.AddRange(DriveBrakeTable);
                    list.Sort((j, k) => Guid.NewGuid().CompareTo(Guid.NewGuid()));
                    return list;
                case Table.TransientTable:
                    return TransientTable;
                default:
                    return null;
            }
        }

        public void GetLimitDataAll()
        {
            var maxmin = new TireDataMaxminSet();
            maxmin.CorneringTableLimit = StaticFunctions.GetLimitData(CorneringTable);
            maxmin.DriveBrakeTableLimit = StaticFunctions.GetLimitData(DriveBrakeTable);
            maxmin.TransientTableLimit = StaticFunctions.GetLimitData(TransientTable);

            MaxminSet = maxmin;
        }
        public void GetLimitData(Table table)
        {
            switch (table)
            {
                case Table.CorneringTable:
                    MaxminSet.CorneringTableLimit = StaticFunctions.GetLimitData(CorneringTable);
                    break;
                case Table.DriveBrakeTable:
                    MaxminSet.DriveBrakeTableLimit = StaticFunctions.GetLimitData(DriveBrakeTable);
                    break;
                case Table.TransientTable:
                    MaxminSet.TransientTableLimit = StaticFunctions.GetLimitData(TransientTable);
                    break;
            }
        }


        
    }

    public struct FuncResult
    {
        public double value;
        public List<double> grads;
    }

    public interface ApproximatingCurve
    {
        List<double> Parameters { get; }
        FuncResult Error(TireData data);
        List<Func<FuncResult>> ConstraintsPure();
        List<bool> FittingParameters { get; }
        List<Func<TireData, FuncResult>> ConstraintsDependOnData();
        void ResetDiff();
    }

    public class ProgressNotification
    {
        public ProgressNotification()
        {
            finished = false;
            Stage = -1;
            Count = -1;
            Error = -1;
        }
        public bool finished;
        public int Stage{ get; set; }
        public int Count { get; set; }
        public double Error { get; set; }
    }
}
