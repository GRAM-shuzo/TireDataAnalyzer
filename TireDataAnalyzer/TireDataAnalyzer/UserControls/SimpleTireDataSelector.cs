using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTCDataUtils;

namespace TireDataAnalyzer.UserControls
{
    public partial class SimpleTireDataSelector : UserControl
    {
        public Table AttachedTable
        {
            get;
            set;
        }
        public bool SASREnable
        {
            get
            {
                return SABar.Enabled;
            }
            set
            {
                SABar.Enabled = value;
                SRBar.Enabled = value;
            }
        }


        public bool PureSlipX { get; set; }
        public SimpleTireDataSelector()
        {
            InitializeComponent();
            Changed = true;
            PureSlipX = false;
            AttachedTable = Table.CorneringTable;
            NumSearch = 5000;
        }
        int numSearch;
        public int NumSearch { get { return numSearch; } set { numSearch = value; Changed = true; } }
        public bool Changed { get; private set; }
        public MagicFormulaArguments CenterValue
        {
            get
            {
                return new MagicFormulaArguments(SASREnable?SABar.valueMean:0 , SASREnable ? SRBar.valueMean : 0, FZBar.valueMean, IABar.valueMean, PBar.valueMean, TBar.valueMean);
            }
        }
        public MagicFormulaArguments UpperValue
        {
            get
            {
                return new MagicFormulaArguments(SASREnable ? SABar.valueR : 0, SASREnable ? SRBar.valueR : 0, FZBar.valueR, IABar.valueR, PBar.valueR, TBar.valueR);
            }
        }
        public MagicFormulaArguments LowerValue
        {
            get
            {
                return new MagicFormulaArguments(SASREnable ? SABar.valueL : 0, SASREnable ? SRBar.valueL : 0, FZBar.valueL, IABar.valueL, PBar.valueL, TBar.valueL);
            }
        }

        public MagicFormulaFittingDelegate MFFD
        {
            get
            {
                return mffd;
            }
            set
            {
                mffd = value;
                if(mffd != null)UpdateValues();
            }
        }
        TireDataSetSelector Selector;

        private MagicFormulaFittingDelegate mffd;
        private void UpdateValues()
        {

            var limit = MFFD.IDataset.GetDataSet().MaxminSet.Limit(AttachedTable);
            if (PureSlipX)
            {
                if (Selector != null)
                {
                    Selector.RemoveConstrain(FZ, AttachedTable);
                    Selector.RemoveConstrain(P, AttachedTable);
                    Selector.RemoveConstrain(IA, AttachedTable);
                    Selector.RemoveConstrain(T, AttachedTable);
                    Selector.RemoveConstrain(SA, AttachedTable);
                }
                FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, limit.Max.FZ, limit.Min.FZ);
                P = new TireDataConstrain("P", TireDataColumn.P, limit.Max.P, limit.Min.P);
                IA = new TireDataConstrain("IA", TireDataColumn.IA, limit.Max.IA, limit.Min.IA);
                T = new TireDataConstrain("T", TireDataColumn.TSTC, limit.Max.TSTC, limit.Min.TSTC);
                SA = new TireDataConstrain("SA", TireDataColumn.SA, limit.Max.SA, limit.Min.SA);
                Selector = new TireDataSetSelector(mffd.IDataset);
                Selector.AddConstrain(FZ, AttachedTable);
                Selector.AddConstrain(P, AttachedTable);
                Selector.AddConstrain(IA, AttachedTable);
                Selector.AddConstrain(T, AttachedTable);
                Selector.AddConstrain(SA, AttachedTable);
            }
            else
            {
                if(Selector != null)
                {
                    Selector.RemoveConstrain(FZ, Table.CorneringTable);
                    Selector.RemoveConstrain(P, Table.CorneringTable);
                    Selector.RemoveConstrain(IA, Table.CorneringTable);
                    Selector.RemoveConstrain(T, Table.CorneringTable);
                    Selector.RemoveConstrain(SA, Table.CorneringTable);
                    Selector.RemoveConstrain(SR, Table.CorneringTable);

                    Selector.RemoveConstrain(FZ, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(P, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(IA, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(T, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(SA, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(SR, Table.DriveBrakeTable);
                }
                limit = StaticFunctions.TireDataMaxminMerge(
                    MFFD.IDataset.GetDataSet().MaxminSet.Limit(Table.CorneringTable),
                    MFFD.IDataset.GetDataSet().MaxminSet.Limit(Table.DriveBrakeTable)
                    );
                limit = StaticFunctions.TireDataMaxminMerge(
                    limit,
                    MFFD.IDataset.GetDataSet().MaxminSet.Limit(Table.TransientTable)
                    );
                FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, limit.Max.FZ, limit.Min.FZ);
                P = new TireDataConstrain("P", TireDataColumn.P, limit.Max.P, limit.Min.P);
                IA = new TireDataConstrain("IA", TireDataColumn.IA, limit.Max.IA, limit.Min.IA);
                T = new TireDataConstrain("T", TireDataColumn.TSTC, limit.Max.TSTC, limit.Min.TSTC);
                SA = new TireDataConstrain("SA", TireDataColumn.SA, limit.Max.SA, limit.Min.SA);
                SR = new TireDataConstrain("SR", TireDataColumn.SR, limit.Max.SR, limit.Min.SR);
                Selector = new TireDataSetSelector(mffd.IDataset);
                Selector.AddConstrain(FZ, Table.CorneringTable);
                Selector.AddConstrain(P, Table.CorneringTable);
                Selector.AddConstrain(IA, Table.CorneringTable);
                Selector.AddConstrain(T, Table.CorneringTable);
                Selector.AddConstrain(SA, Table.CorneringTable);
                Selector.AddConstrain(SR, Table.CorneringTable);

                Selector.AddConstrain(FZ, Table.DriveBrakeTable);
                Selector.AddConstrain(P, Table.DriveBrakeTable);
                Selector.AddConstrain(IA, Table.DriveBrakeTable);
                Selector.AddConstrain(T, Table.DriveBrakeTable);
                Selector.AddConstrain(SA, Table.DriveBrakeTable);
                Selector.AddConstrain(SR, Table.DriveBrakeTable);
            }
            FZBar.Max = limit.Max.FZ;
            FZBar.Min = limit.Min.FZ;

            PBar.Max = limit.Max.P;
            PBar.Min = limit.Min.P;

            IABar.Max = limit.Max.IA;
            IABar.Min = limit.Min.IA;

            TBar.Max = limit.Max.TSTC;
            TBar.Min = limit.Min.TSTC;

            SABar.Max = limit.Max.SA;
            SABar.Min = limit.Min.SA;

            SRBar.Max = limit.Max.SR;
            SRBar.Min = limit.Min.SR;

            FZBar.valueR = limit.Max.FZ;
            FZBar.valueL = limit.Min.FZ;

            PBar.valueR = limit.Max.P;
            PBar.valueL = limit.Min.P;

            IABar.valueR = limit.Max.IA;
            IABar.valueL = limit.Min.IA;

            TBar.valueR = limit.Max.TSTC;
            TBar.valueL = limit.Min.TSTC;

            SABar.valueR = limit.Max.SA;
            SABar.valueL = limit.Min.SA;

            SRBar.valueR = limit.Max.SR;
            SRBar.valueL = limit.Min.SR;
        }
    
        TireDataConstrain FZ;
        TireDataConstrain P;
        TireDataConstrain IA;
        TireDataConstrain T;
        TireDataConstrain SA;
        TireDataConstrain SR;
        public IDataSet SelectedData()
        {

            if (Changed)
            {
                if (PureSlipX)
                {
                    Selector.RemoveConstrain(FZ, AttachedTable);
                    Selector.RemoveConstrain(P, AttachedTable);
                    Selector.RemoveConstrain(IA, AttachedTable);
                    Selector.RemoveConstrain(T, AttachedTable);
                    Selector.RemoveConstrain(SA, AttachedTable);
                    FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, FZBar.valueR, FZBar.valueL);
                    P = new TireDataConstrain("P", TireDataColumn.P, PBar.valueR, PBar.valueL);
                    IA = new TireDataConstrain("IA", TireDataColumn.IA, IABar.valueR, IABar.valueL);
                    T = new TireDataConstrain("T", TireDataColumn.TSTC, TBar.valueR, TBar.valueL);
                    SA = new TireDataConstrain("SA", TireDataColumn.SA, 0.5, -0.5);

                    Selector.AddConstrain(FZ, AttachedTable);
                    Selector.AddConstrain(P, AttachedTable);
                    Selector.AddConstrain(IA, AttachedTable);
                    Selector.AddConstrain(T, AttachedTable);
                    Selector.AddConstrain(SA, AttachedTable);
                    Selector.ExtractData(AttachedTable, NumSearch);
                }
                else
                {

                    Selector.RemoveConstrain(FZ, Table.CorneringTable);
                    Selector.RemoveConstrain(P, Table.CorneringTable);
                    Selector.RemoveConstrain(IA, Table.CorneringTable);
                    Selector.RemoveConstrain(T, Table.CorneringTable);
                    Selector.RemoveConstrain(SA, Table.CorneringTable);
                    Selector.RemoveConstrain(SR, Table.CorneringTable);

                    Selector.RemoveConstrain(FZ, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(P, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(IA, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(T, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(SA, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(SR, Table.DriveBrakeTable);

                    Selector.RemoveConstrain(FZ, Table.TransientTable);
                    Selector.RemoveConstrain(P, Table.TransientTable);
                    Selector.RemoveConstrain(IA, Table.TransientTable);
                    Selector.RemoveConstrain(T, Table.TransientTable);
                    Selector.RemoveConstrain(SA, Table.TransientTable);
                    Selector.RemoveConstrain(SR, Table.TransientTable);

                    FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, FZBar.valueR, FZBar.valueL);
                    P = new TireDataConstrain("P", TireDataColumn.P, PBar.valueR, PBar.valueL);
                    IA = new TireDataConstrain("IA", TireDataColumn.IA, IABar.valueR, IABar.valueL);
                    T = new TireDataConstrain("T", TireDataColumn.TSTC, TBar.valueR, TBar.valueL);
                    SA = new TireDataConstrain("SA", TireDataColumn.SA, SABar.valueR, SABar.valueL);
                    SR = new TireDataConstrain("SR", TireDataColumn.SR, SRBar.valueR, SRBar.valueL);

                    Selector.AddConstrain(FZ, Table.CorneringTable);
                    Selector.AddConstrain(P, Table.CorneringTable);
                    Selector.AddConstrain(IA, Table.CorneringTable);
                    Selector.AddConstrain(T, Table.CorneringTable);
                    Selector.AddConstrain(SA, Table.CorneringTable);
                    Selector.AddConstrain(SR, Table.CorneringTable);

                    Selector.AddConstrain(FZ, Table.DriveBrakeTable);
                    Selector.AddConstrain(P, Table.DriveBrakeTable);
                    Selector.AddConstrain(IA, Table.DriveBrakeTable);
                    Selector.AddConstrain(T, Table.DriveBrakeTable);
                    Selector.AddConstrain(SA, Table.DriveBrakeTable);
                    Selector.AddConstrain(SR, Table.DriveBrakeTable);

                    Selector.AddConstrain(FZ, Table.TransientTable);
                    Selector.AddConstrain(P, Table.TransientTable);
                    Selector.AddConstrain(IA, Table.TransientTable);
                    Selector.AddConstrain(T, Table.TransientTable);
                    Selector.AddConstrain(SA, Table.TransientTable);
                    Selector.AddConstrain(SR, Table.TransientTable);
                    Selector.ExtractData(Table.CorneringTable, NumSearch/4);
                    Selector.ExtractData(Table.DriveBrakeTable, NumSearch*3/4);
                    Selector.ExtractData(Table.TransientTable, NumSearch);
                }
            }
            

            Changed = false;
            return Selector;
        }

        public event EventHandler ValueChanged;

        private void BarValueChanged(object sender, EventArgs e)
        {
            MagicFormulaArguments max = new MagicFormulaArguments(SABar.valueR, SRBar.valueR, FZBar.valueR, IABar.valueR, PBar.valueR, TBar.valueR);
            MagicFormulaArguments min = new MagicFormulaArguments(SABar.valueL, SRBar.valueL, FZBar.valueL, IABar.valueL, PBar.valueL, TBar.valueL);
            var c = CenterValue;
            if (NormalizedCB.Checked == true)
            {
                max = MFFD.MagicFormula.GetNormalizedValue(max);
                min = MFFD.MagicFormula.GetNormalizedValue(min);
                c = MFFD.MagicFormula.GetNormalizedValue(c);
            }
            FzMax.Text = max.FZ.ToString("f2");
            FzMin.Text = min.FZ.ToString("f2");
            FzC.Text = c.FZ.ToString("f2");
            IAMax.Text = max.IA.ToString("f2");
            IAMin.Text = min.IA.ToString("f2");
            IAC.Text = c.IA.ToString("f2");
            PMax.Text = max.P.ToString("f2");
            PMin.Text = min.P.ToString("f2");
            PC.Text = c.P.ToString("f2");
            TMax.Text = max.T.ToString("f2");
            TMin.Text = min.T.ToString("f2");
            TC.Text = c.T.ToString("f2");
            SAMax.Text = max.SA.ToString("f2");
            SAMin.Text = min.SA.ToString("f2");
            SAC.Text = c.SA.ToString("f2");
            SRMax.Text = max.SR.ToString("f2");
            SRMin.Text = min.SR.ToString("f2");
            SRC.Text = c.SR.ToString("f2");
            var bar = sender as CustomTrackBar.DoubleTrackBar;
            Changed = true;
            if (ValueChanged != null && bar !=null && !bar.ThumbClicked ) ValueChanged(this, new EventArgs());
        }
    }
}
