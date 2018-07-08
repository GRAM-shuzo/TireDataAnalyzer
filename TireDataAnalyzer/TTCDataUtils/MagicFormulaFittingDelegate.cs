using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MagicFormulaFittingSolver;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.IO;

namespace TTCDataUtils
{
    [Serializable]
    public class MagicFormulaFittingDelegate
    {
        public MagicFormulaFittingDelegate(string TireName, IDataSet iDataset)
        {
            MagicFormula = new TireMagicFormula(TireName);
            IDataset = iDataset;
            FittingResolved = false;
            Initialized = false;
        }
        [NonSerialized]
        public Guid ID;

        private MagicFormulaFittingDelegate(TireMagicFormula formula, IDataSet iDataset)
        {
            MagicFormula = formula;
            IDataset = iDataset;
            FittingResolved = false;
            Initialized = false;
        }

        public MagicFormulaFittingDelegate Copy()
        {
            var mffd = new MagicFormulaFittingDelegate(MagicFormula.Copy(), IDataset);
            mffd.Initialized = this.Initialized;
            mffd.FittingResolved = this.FittingResolved;
            mffd.FittingPFX = FittingPFX;
            mffd.FittingPFY = FittingPFY;
            mffd.FittingCFX = FittingCFX;
            mffd.FittingCFY = FittingCFY;
            mffd.FittingSAT = FittingSAT;
            mffd.ID = ID;
            return mffd;
        }

        public void CopyFrom(MagicFormulaFittingDelegate mffd, bool sameParent)
        {
            MagicFormula = mffd.MagicFormula;
            idataset = mffd.IDataset;
            this.Initialized = mffd.Initialized;
            this.FittingResolved = mffd.FittingResolved && sameParent;
            FittingPFX = mffd.FittingPFX;
            FittingPFY = mffd.FittingPFY;
            FittingCFX = mffd.FittingCFX;
            FittingCFY = mffd.FittingCFY;
            FittingSAT = mffd.FittingSAT;
            ID = mffd.ID;
            if (ValueChanged != null) ValueChanged();
        }

        public delegate void OnValueChangedDelegate();
        [field: NonSerialized]
        public OnValueChangedDelegate ValueChanged;

        public TireMagicFormula MagicFormula { get; private set; }
        [NonSerialized]
        IDataSet idataset;
        public IDataSet IDataset
        {
            get { return idataset; }
            private set
            {
                idataset = value;
            }
        }
        public void SetIDataSetOnLoad(MagicFormulaFittingDelegate other)
        {
            IDataset = other.IDataset;
        }

        bool initialized;
        [field: NonSerialized]
        public EventHandler OnInitialized;
        [field: NonSerialized]
        public EventHandler OnFittingResolved;
        public bool Initialized
        {
            get
            {
                return initialized;
            }
            private set
            {
                initialized = value;
                if (initialized == true && OnInitialized != null)
                {
                    OnInitialized(this, new EventArgs());
                }
            }
        }
        bool fittingResolved;
        public bool FittingResolved
        {
            get
            {
                return fittingResolved;
            }
            private set
            {
                fittingResolved = value;
                if (fittingResolved == true && OnFittingResolved != null)
                {
                    OnFittingResolved(this, new EventArgs());
                }
            }
        }

        [NonSerialized]
        FittingSolver solver;
        public FittingSolver Solver { get { return solver; } set { solver = value; } }

        public void SetInitialValue(TireMagicFormula mf)
        {
            MagicFormula = mf;
            if (ValueChanged != null) ValueChanged();
            Initialized = true;
        }

        public void Run(CancellationTokenSource cancel, IProgress<ProgressNotification> prg)
        {
            var pn = new ProgressNotification();
            if (!(Solver is NoFitting))
            {
                if (!Initialized) throw new Exception("初期化されていません");
                var dataset = IDataset.GetDataSet();

                //PureFY

                pn.Stage = 1;
                prg.Report(pn);
                if (FittingPFY) solver.Run(MagicFormula.FY, dataset.CorneringTable, cancel, prg);


                //PureFX
                pn = new ProgressNotification();
                pn.Stage = 2;
                prg.Report(pn);
                if (FittingPFX)
                {
                    TireDataSelector fxSelector = new TireDataSelector();
                    fxSelector.AddConstrain(new TireDataConstrain("fx", TireDataColumn.SA, 5, -5));
                    fxSelector.Update(dataset.DriveBrakeTable, dataset.MaxminSet.DriveBrakeTableLimit);
                    solver.Run(MagicFormula.FX, fxSelector.ExtractedData, cancel, prg);
                }

                var list = dataset.GetDataList(Table.StaticTable);

                //CombinedFY
                pn = new ProgressNotification();
                pn.Stage = 3;
                prg.Report(pn);
                if (FittingCFY) solver.Run(MagicFormula.CFY, list, cancel, prg);
                //CombinedFX
                pn = new ProgressNotification();
                pn.Stage = 4;
                prg.Report(pn);
                if (FittingCFX) solver.Run(MagicFormula.CFX, list, cancel, prg);
                if (FittingSAT)
                {
                    //PnumaticTrail
                    pn = new ProgressNotification();
                    pn.Stage = 5;
                    prg.Report(pn);
                    solver.Run(MagicFormula.MZ.PT, dataset.CorneringTable, cancel, prg);
                    //CombinedMzMember
                    pn = new ProgressNotification();
                    pn.Stage = 6;
                    prg.Report(pn);
                    solver.Run(MagicFormula.MZ.CMZM, dataset.DriveBrakeTable, cancel, prg);

                    //MzR
                    pn = new ProgressNotification();
                    pn.Stage = 7;
                    prg.Report(pn);
                    solver.Run(MagicFormula.MZ.MZR, list, cancel, prg);
                }
            }



            FittingResolved = true;

            pn = new ProgressNotification();
            pn.finished = true;
            prg.Report(pn);
        }

        public bool FittingPFX = true;
        public bool FittingPFY = true;
        public bool FittingCFX = true;
        public bool FittingCFY = true;
        public bool FittingSAT = false;

        public static MagicFormulaFittingDelegate Load(Stream reader)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as MagicFormulaFittingDelegate;
            data.MagicFormula.ResetDiff();
            data.ID = Guid.NewGuid();
            return data;
        }

        public void Save(Stream writer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(writer, this);
        }
    }
}