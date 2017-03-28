﻿using System;
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
        private MagicFormulaFittingDelegate(TireMagicFormula formula, IDataSet iDataset)
        {
            MagicFormula = formula;
            IDataset = iDataset;
            FittingResolved = false;
            Initialized = false;
        }

        public MagicFormulaFittingDelegate Copy()
        {
            var mffd =  new MagicFormulaFittingDelegate(MagicFormula.Copy(), IDataset);
            mffd.Initialized = this.Initialized;
            mffd.FittingResolved = this.FittingResolved;
            mffd.FittingPFX = FittingPFX;
            mffd.FittingPFY = FittingPFY;
            mffd.FittingCFX = FittingCFX;
            mffd.FittingCFY = FittingCFY;
            mffd.FittingSAT = FittingSAT;
            return mffd;
        }

        public void  CopyFrom(MagicFormulaFittingDelegate mffd)
        {
            MagicFormula = mffd.MagicFormula;
            idataset = mffd.IDataset;
            this.Initialized = mffd.Initialized;
            this.FittingResolved = mffd.FittingResolved;
            FittingPFX = mffd.FittingPFX;
            FittingPFY = mffd.FittingPFY;
            FittingCFX = mffd.FittingCFX;
            FittingCFY = mffd.FittingCFY;
            FittingSAT = mffd.FittingSAT;
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

        public bool Initialized{ get; private set; }
        public bool FittingResolved { get; private set; }
        [NonSerialized]
        FittingSolver solver;
        public FittingSolver Solver { get { return solver; } set { solver = value; } }

        public void SetInitialValue(TireMagicFormula mf)
        {
            MagicFormula = mf;
            if (ValueChanged != null) ValueChanged();
            Initialized = true;
        }

        public void Run( CancellationTokenSource cancel, IProgress<ProgressNotification> prg)
        {
            if (!Initialized) throw new Exception("初期化されていません");
            var dataset = IDataset.GetDataSet();

            //PureFY
            var pn = new ProgressNotification();
            pn.Stage = 1;
            prg.Report(pn);
            if (FittingPFY) solver.Run(MagicFormula.FY, dataset.CorneringTable, cancel, prg);


            TireDataSelector fxSelector = new TireDataSelector();
            fxSelector.AddConstrain(new TireDataConstrain("fx", TireDataColumn.SA, 5, -5));
            fxSelector.Update(dataset.DriveBrakeTable, dataset.MaxminSet.DriveBrakeTableLimit);


            //PureFX
            pn = new ProgressNotification();
            pn.Stage = 2;
            prg.Report(pn);
            if (FittingPFX) solver.Run(MagicFormula.FX, fxSelector.ExtractedData, cancel, prg);


            //CombinedFY
            pn = new ProgressNotification();
            pn.Stage = 3;
            prg.Report(pn);
            if (FittingCFY) solver.Run(MagicFormula.CFY, dataset.DriveBrakeTable, cancel, prg);
            //CombinedFX
            pn = new ProgressNotification();
            pn.Stage = 4;
            prg.Report(pn);
            if (FittingCFX) solver.Run(MagicFormula.CFX, dataset.DriveBrakeTable, cancel, prg);

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
                var list = new List<TireData>();
                list.AddRange(dataset.CorneringTable);
                list.AddRange(dataset.DriveBrakeTable);
                solver.Run(MagicFormula.MZ.MZR, list, cancel, prg);
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
        public bool FittingSAT = true;

        public static MagicFormulaFittingDelegate Load(Stream reader)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as MagicFormulaFittingDelegate;
            data.MagicFormula.ResetDiff();
            return data;
        }

        public void Save(Stream writer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(writer, this);
        }
    }
}