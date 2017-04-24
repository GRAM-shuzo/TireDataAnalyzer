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
                return new MagicFormulaArguments(0,0,FZBar.valueMean, IABar.valueMean, PBar.valueMean, TBar.valueMean);
            }
        }
        public MagicFormulaArguments UpperValue
        {
            get
            {
                return new MagicFormulaArguments(0, 0, FZBar.valueR, IABar.valueR, PBar.valueR, TBar.valueR);
            }
        }
        public MagicFormulaArguments LowerValue
        {
            get
            {
                return new MagicFormulaArguments(0, 0, FZBar.valueL, IABar.valueL, PBar.valueL, TBar.valueL);
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
                limit = StaticFunctions.TireDataMaxminMerge(
                    MFFD.IDataset.GetDataSet().MaxminSet.Limit(Table.CorneringTable),
                    MFFD.IDataset.GetDataSet().MaxminSet.Limit(Table.DriveBrakeTable)
                    );
                FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, limit.Max.FZ, limit.Min.FZ);
                P = new TireDataConstrain("P", TireDataColumn.P, limit.Max.P, limit.Min.P);
                IA = new TireDataConstrain("IA", TireDataColumn.IA, limit.Max.IA, limit.Min.IA);
                T = new TireDataConstrain("T", TireDataColumn.TSTC, limit.Max.TSTC, limit.Min.TSTC);
                SA = new TireDataConstrain("SA", TireDataColumn.SA, limit.Max.SA, limit.Min.SA);
                Selector = new TireDataSetSelector(mffd.IDataset);
                Selector.RemoveConstrain(FZ, Table.CorneringTable);
                Selector.RemoveConstrain(P, Table.CorneringTable);
                Selector.RemoveConstrain(IA, Table.CorneringTable);
                Selector.RemoveConstrain(T, Table.CorneringTable);
                Selector.RemoveConstrain(FZ, Table.DriveBrakeTable);
                Selector.RemoveConstrain(P, Table.DriveBrakeTable);
                Selector.RemoveConstrain(IA, Table.DriveBrakeTable);
                Selector.RemoveConstrain(T, Table.DriveBrakeTable);
            }
            FZBar.Max = limit.Max.FZ;
            FZBar.Min = limit.Min.FZ;

            PBar.Max = limit.Max.P;
            PBar.Min = limit.Min.P;

            IABar.Max = limit.Max.IA;
            IABar.Min = limit.Min.IA;

            TBar.Max = limit.Max.TSTC;
            TBar.Min = limit.Min.TSTC;

            FZBar.valueR = limit.Max.FZ;
            FZBar.valueL = limit.Min.FZ;

            PBar.valueR = limit.Max.P;
            PBar.valueL = limit.Min.P;

            IABar.valueR = limit.Max.IA;
            IABar.valueL = limit.Min.IA;

            TBar.valueR = limit.Max.TSTC;
            TBar.valueL = limit.Min.TSTC;
        }
    
        TireDataConstrain FZ;
        TireDataConstrain P;
        TireDataConstrain IA;
        TireDataConstrain T;
        TireDataConstrain SA;
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
                    Selector.RemoveConstrain(FZ, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(P, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(IA, Table.DriveBrakeTable);
                    Selector.RemoveConstrain(T, Table.DriveBrakeTable);
                    FZ = new TireDataConstrain("FZ", TireDataColumn.FZ, FZBar.valueR, FZBar.valueL);
                    P = new TireDataConstrain("P", TireDataColumn.P, PBar.valueR, PBar.valueL);
                    IA = new TireDataConstrain("IA", TireDataColumn.IA, IABar.valueR, IABar.valueL);
                    T = new TireDataConstrain("T", TireDataColumn.TSTC, TBar.valueR, TBar.valueL);
                    

                    Selector.AddConstrain(FZ, Table.CorneringTable);
                    Selector.AddConstrain(P, Table.CorneringTable);
                    Selector.AddConstrain(IA, Table.CorneringTable);
                    Selector.AddConstrain(T, Table.CorneringTable);
                    Selector.AddConstrain(FZ, Table.DriveBrakeTable);
                    Selector.AddConstrain(P, Table.DriveBrakeTable);
                    Selector.AddConstrain(IA, Table.DriveBrakeTable);
                    Selector.AddConstrain(T, Table.DriveBrakeTable);
                    Selector.ExtractData(Table.CorneringTable, NumSearch/4);
                    Selector.ExtractData(Table.DriveBrakeTable, NumSearch*3/4);
                }
            }
            

            Changed = false;
            return Selector;
        }

        public event EventHandler ValueChanged;

        private void BarValueChanged(object sender, EventArgs e)
        {
            MagicFormulaArguments max = new MagicFormulaArguments(0, 0, FZBar.valueR, IABar.valueR, PBar.valueR, TBar.valueR);
            MagicFormulaArguments min = new MagicFormulaArguments(0, 0, FZBar.valueL, IABar.valueL, PBar.valueL, TBar.valueL);
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

            var bar = sender as CustomTrackBar.DoubleTrackBar;
            Changed = true;
            if (ValueChanged != null && bar !=null && !bar.ThumbClicked ) ValueChanged(this, new EventArgs());
        }
    }
}
