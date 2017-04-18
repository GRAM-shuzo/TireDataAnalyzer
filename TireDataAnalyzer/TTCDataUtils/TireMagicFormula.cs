using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.IO;

namespace TTCDataUtils
{




    [Serializable]
    public class MagicFormulaArguments
    {

        public MagicFormulaArguments(double sa, double sr, double fz, double ia, double p, double t)
        {
            SA = sa;
            SR = sr;
            FZ = fz;
            IA = ia;
            P = p;
            T = t;
        }
        public MagicFormulaArguments(TireData data)
        {
            setValue(data);
        }
        public double SA;  //スリップ角
        public double SR;  //スリップ率
        public double FZ;  //輪荷重
        public double IA;  //キャンバ角
        public double P;   //タイヤ空気圧
        public double T;   //タイヤ平均温度
        public MagicFormulaArguments Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }
        public void setValue(MagicFormulaInputVariables name, double value)
        {
            switch (name)
            {
                case MagicFormulaInputVariables.SA:
                    SA = value;
                    return;
                case MagicFormulaInputVariables.SR:
                    SR = value;
                    return;
                case MagicFormulaInputVariables.FZ:
                    FZ = value;
                    return;
                case MagicFormulaInputVariables.IA:
                    IA = value;
                    return;
                case MagicFormulaInputVariables.P:
                    P = value;
                    return;
                case MagicFormulaInputVariables.T:
                    T = value;
                    return;
                default:
                    return;
            }

        }
        public void setValue(TireData data)
        {
            SA = data.SA;
            SR = data.SR;
            FZ = data.FZ;
            IA = data.IA;
            P = data.P;
            T = data.TSTC;
        }
        public double getValue(MagicFormulaInputVariables name)
        {
            switch (name)
            {
                case MagicFormulaInputVariables.SA:
                    return SA;
                case MagicFormulaInputVariables.SR:
                    return SR;
                case MagicFormulaInputVariables.FZ:
                    return FZ;
                case MagicFormulaInputVariables.IA:
                    return IA;
                case MagicFormulaInputVariables.P:
                    return P;
                case MagicFormulaInputVariables.T:
                    return T;
                default:
                    return 0;
            }

        }
        public double this[int i]
        {
            set
            {
                switch (i)
                {
                    case 0:
                        SA = value;
                        break;
                    case 1:
                        SR = value;
                        break;
                    case 2:
                        IA = value;
                        break;
                    case 3:
                        FZ = value;
                        break;
                    case 4:
                        P = value;
                        break;
                    default:
                        T = value;
                        break;
                }
            }
            get
            {
                switch (i)
                {
                    case 0:
                        return SA;
                    case 1:
                        return SR;
                    case 2:
                        return IA;
                    case 3:
                        return FZ;
                    case 4:
                        return P;
                    default:
                        return T;
                }

            }
        }
    };

    [Serializable]
    public class MagicFormulaOutputs
    {
        public MagicFormulaOutputs(double fx, double fy, double mz)
        {
            FX = fx;
            FY = fy;
            MZ = mz;
        }
        public double FX { get; private set; }
        public double FY { get; private set; }
        public double MZ { get; private set; }
    }

    [Serializable]
    public class TireMagicFormula
    {
        public string TireName { get; set; }

        public PureFXMagicFormula FX { get; private set; }
        public PureFYMagicFormula FY { get; private set; }
        public CombinedFXMagicFormula CFX { get; private set; }
        public CombinedFYMagicFormula CFY { get; private set; }

        public MZMagicFormula MZ { get; private set; }

        public TireMagicFormula(string tireName)
        {
            TireName = tireName;
            FX = new PureFXMagicFormula();
            FY = new PureFYMagicFormula(FX);
            CFX = new CombinedFXMagicFormula(FX);
            CFY = new CombinedFYMagicFormula(FY);
            var PTM = new PTMagicFormula(CFX, CFY);
            var CMZM = new CombinedMzMember(PTM);
            var MZRM = new MzrMagicFormula(CMZM);
            MZ = new MZMagicFormula(MZRM);
        }
        public MagicFormulaArguments GetNormalizedValue(MagicFormulaArguments args)
        {
            args = FX.Normalize(args);
            foreach(MagicFormulaInputVariables iv in Enum.GetValues(typeof(MagicFormulaInputVariables)))
            {
                if(double.IsNaN(args.getValue(iv)) || double.IsInfinity(args.getValue(iv)))
                {
                    args.setValue(iv, 0);
                }
            }
            return args;
        }
        public MagicFormulaArguments GetDenormalizedValue(MagicFormulaArguments args)
        {
            return FX.Denormalize(args);
        }
        public MagicFormulaOutputs NonSlipFunction(MagicFormulaArguments args)
        {
            args = GetNormalizedValue(args);
            return new MagicFormulaOutputs(0,FY.PureFunction(args),MZ.PureFunction(args));
        }
        public MagicFormulaOutputs CombinedFunction(MagicFormulaArguments args)
        {
            args = GetNormalizedValue(args);
            return new MagicFormulaOutputs(CFX.CombinedFunction(args), CFY.CombinedFunction(args), MZ.CombinedFunction(args));
        }

        public double GetVariables(MagicFormulaOutputVariables name, MagicFormulaArguments args)
        {
            args = GetNormalizedValue(args);
            switch (name)
            {
                case MagicFormulaOutputVariables.FY:
                    return CFY.CombinedFunction(args);
                case MagicFormulaOutputVariables.FY_D:
                    return FY.D(args);
                case MagicFormulaOutputVariables.FY_BCD:
                    return FY.BCD(args);
                case MagicFormulaOutputVariables.FY_E:
                    return FY.E(args);
                case MagicFormulaOutputVariables.FY_C:
                    return FY.C(args);
                case MagicFormulaOutputVariables.FX:
                    return CFX.CombinedFunction(args);
                case MagicFormulaOutputVariables.FX_D:
                    return FX.D(args);
                case MagicFormulaOutputVariables.FX_BCD:
                    return FX.BCD(args);
                case MagicFormulaOutputVariables.FX_E:
                    return FX.E(args);
                case MagicFormulaOutputVariables.FX_C:
                    return FX.C(args);
            }

            return 0;
        }

        public TireMagicFormula Copy()
        {
            return StaticFunctions.DeepCopy(this);
        }

        public void ResetDiff()
        {
            FX.ResetDiff();
            FY.ResetDiff();
            CFX.ResetDiff();
            CFY.ResetDiff();
            MZ.ResetDiff();
        }

        public static TireMagicFormula Load(Stream reader)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var data = binaryFormatter.Deserialize(reader) as TireMagicFormula;
            data.ResetDiff();
            return data;
        }

        public void Save(Stream writer)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(writer, this);
        }
    }

    public enum MagicFormulaInputVariables
    {
        SA,
        SR,
        FZ,
        IA,
        P,
        T,
        FY,
    }

    public enum MagicFormulaOutputVariables
    {
        FX,
        FY,
        MZ,
        PT,

        FX_C,
        FX_D,
        FX_BCD,
        FX_E,
        FX_Sh,
        FX_Sv,
        
        
        FY_C,
        FY_D,
        FY_BCD,
        FY_E,
        FY_Sh,
        FY_Sv,

        MZ_D
    }

    #region F

    [Serializable]
    public abstract class SinTypePureMagicFormula
    {
        public string FunctionTex = @"{F_y} = D\, \sin(C \arctan(B(x+ S_h) - E(B(x + S_h)  - \arctan B(x+ S_h) ))) + S_v";

        public List<double> Parameters { get; protected set; }
        public List<bool> FittingParameters { get; protected set; }

        public double PureFunction(MagicFormulaArguments args)
        {
            double x = X(args) + Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);
            x = x * b;
            return d * Math.Sin(c * Math.Atan(x - e * (x - Math.Atan(x))));
        }

        public abstract Tuple<double, double> GetPeak(MagicFormulaArguments args);
        

        public abstract double X(MagicFormulaArguments args);
        public abstract double D(MagicFormulaArguments args);
        public abstract double C(MagicFormulaArguments args);
        public abstract double BCD(MagicFormulaArguments args);
        public abstract double E(MagicFormulaArguments args);
        public abstract double Sh(MagicFormulaArguments args);
        public abstract double Sv(MagicFormulaArguments args);


        abstract public MagicFormulaArguments Normalize(MagicFormulaArguments args);
        abstract public MagicFormulaArguments Denormalize(MagicFormulaArguments args);
        #region diff

        public List<Func<MagicFormulaArguments, double>> GradPureFunctions;
        public List<double> GradPure(MagicFormulaArguments args)
        {
            var result = new List<double>();
            foreach (var func in GradPureFunctions)
            {
                result.Add(func(args));
            }
            return result;
        }

        public double dF_dD(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);
            return -Math.Sin(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dC(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);

            return -d * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)) * Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dB(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);

            return -c * d * (e * (-(x + sh) * Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1) + x + sh) - x - sh) *
    Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) *
    Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dBCD(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);

            return c * (e * (-(x + sh) * Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1) + x + sh) - x - sh) *
    Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) *
    Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)) * Math.Sin(c * Math.Atan(e *
    (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh))) - (e * (-(x + sh) *
    Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1) + x + sh) - x - sh) *
    Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) *
    Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dE(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);


            return -c * d * Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) * (b * (x + sh) - Math.Atan(b * (x + sh))) * Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));

        }
        public double dF_dSh(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double bcd = BCD(args);
            double b = bcd / (c * d);
            double e = E(args);
            return -c * d * (e * (b - b * Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1)) - b) *
    Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) *
    Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dSv(MagicFormulaArguments args)
        {
            return 1;
        }

        #endregion
    }

    [Serializable]
    public abstract class SinTypeCombinedMagicFormula
    {
        public List<double> Parameters { get; protected set; }
        public List<bool> FittingParameters { get; protected set; }
        public SinTypePureMagicFormula PureMagicFormula { get; private set; }
        public SinTypeCombinedMagicFormula(SinTypePureMagicFormula pm)
        {
            PureMagicFormula = pm;
        }

        public double PureFunction(MagicFormulaArguments args)
        {
            return PureMagicFormula.PureFunction(args);
        }
        public MagicFormulaArguments Normalize(MagicFormulaArguments args)
        {
            return PureMagicFormula.Normalize(args);
        }
        public MagicFormulaArguments Denormalize(MagicFormulaArguments args)
        {
            return PureMagicFormula.Denormalize(args);
        }
        
        public double CombinedFunction(MagicFormulaArguments args)
        {
            return PureFunction(args) * G(args) + Svy(args);
        }

        public double G(MagicFormulaArguments args)
        {
            double y = Y(args);
            double shy = Shy(args);
            double by = By(args);
            double cy = Cy(args);
            double ey = Ey(args);
            var r = Math.Cos(cy * Math.Atan(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))) / Math.Cos(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy));
            return r;
        }

        public abstract double Y(MagicFormulaArguments args);
        public abstract double By(MagicFormulaArguments args);
        public abstract double Cy(MagicFormulaArguments args);
        public abstract double Ey(MagicFormulaArguments args);
        public abstract double Shy(MagicFormulaArguments args);
        public abstract double Svy(MagicFormulaArguments args);

        #region diff
        public List<Func<MagicFormulaArguments, double>> GradCombinedFunctions;
        public List<double> GradCombined(MagicFormulaArguments args)
        {
            var result = new List<double>();
            foreach (var func in GradCombinedFunctions)
            {
                result.Add(func(args));
            }
            return result;
        }

        public double dCF_dSvy(MagicFormulaArguments args)
        {
            return 1;
        }
        public double dCF_dShy(MagicFormulaArguments args)
        {
            double y = Y(args);
            double shy = Shy(args);
            double by = By(args);
            double cy = Cy(args);
            double ey = Ey(args);

            return PureFunction(args) * cy * (ey * (by - by * Math.Pow(Math.Pow(by, 2) * Math.Pow(shy, 2) + 1, -1)) - by) *
    Math.Pow(Math.Pow(ey * (by * shy - Math.Atan(by * shy)) - by * shy, 2) + 1, -1) *
    Math.Pow(Math.Cos(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -2) *
    Math.Sin(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)) * Math.Cos(cy *
    Math.Atan(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))) - cy *
    Math.Pow(Math.Cos(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -1) *
    (ey * (by - by * Math.Pow(Math.Pow(by, 2) * Math.Pow(y + shy, 2) + 1, -1)) - by) *
    Math.Pow(Math.Pow(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy), 2) + 1, -1) *
    Math.Sin(cy * Math.Atan(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy)));
        }
        public double dCF_dBy(MagicFormulaArguments args)
        {
            double y = Y(args);
            double shy = Shy(args);
            double by = By(args);
            double cy = Cy(args);
            double ey = Ey(args);

            return PureFunction(args) * cy * (ey * (shy - shy * Math.Pow(Math.Pow(by, 2) * Math.Pow(shy, 2) + 1, -1)) - shy) *
    Math.Pow(Math.Pow(ey * (by * shy - Math.Atan(by * shy)) - by * shy, 2) + 1, -1) *
    Math.Pow(Math.Cos(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -2) *
    Math.Sin(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)) * Math.Cos(cy *
    Math.Atan(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))) - cy *
    Math.Pow(Math.Cos(cy * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -1) *
    (ey * (-(y + shy) * Math.Pow(Math.Pow(by, 2) * Math.Pow(y + shy, 2) + 1, -1) + y + shy) - y - shy) *
    Math.Pow(Math.Pow(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy), 2) + 1, -1) *
    Math.Sin(cy * Math.Atan(ey * (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy)));
        }
        public double dCF_dCy(MagicFormulaArguments args)
        {
            double y = Y(args);
            double shy = Shy(args);
            double by = By(args);
            double cy = Cy(args);
            double ey = Ey(args);

            return PureFunction(args) * Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy) * Math.Pow(Math.Cos(cy *
    Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -2) * Math.Sin(cy *
    Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)) * Math.Cos(cy * Math.Atan(ey *
    (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))) - Math.Pow(Math.Cos(cy *
    Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -1) * Math.Atan(ey * (by *
    (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy)) * Math.Sin(cy * Math.Atan(ey * (by *
    (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))); ;
        }
        public double dCF_dEy(MagicFormulaArguments args)
        {
            double y = Y(args);
            double shy = Shy(args);
            double by = By(args);
            double cy = Cy(args);
            double ey = Ey(args);

            return PureFunction(args) * cy * Math.Pow(Math.Pow(ey * (by * shy - Math.Atan(by * shy)) - by * shy, 2) + 1, -1) * (by *
    shy - Math.Atan(by * shy)) * Math.Pow(Math.Cos(cy * Math.Atan(ey * (by *
    shy - Math.Atan(by * shy)) - by * shy)), -2) * Math.Sin(cy * Math.Atan(ey * (by *
    shy - Math.Atan(by * shy)) - by * shy)) * Math.Cos(cy * Math.Atan(ey * (by *
    (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))) - cy * Math.Pow(Math.Cos(cy *
    Math.Atan(ey * (by * shy - Math.Atan(by * shy)) - by * shy)), -1) * Math.Pow(Math.Pow(ey *
    (by * (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy), 2) + 1, -1) * (by *
    (y + shy) - Math.Atan(by * (y + shy))) * Math.Sin(cy * Math.Atan(ey * (by *
    (y + shy) - Math.Atan(by * (y + shy))) - by * (y + shy))); ;
        }
        #endregion
    }

    [Serializable]
    public class PureFXMagicFormula : SinTypePureMagicFormula, ApproximatingCurve
    {
        const int numParam = 19;
        public PureFXMagicFormula()
        {
            Parameters = new List<double>(numParam);
            GradPureFunctions = new List<Func<MagicFormulaArguments, double>>(numParam);
            for(int i = 0; i< numParam; ++i)
            {
                Parameters.Add(0);

            }
            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }

            ResetDiff();
        }
        public override Tuple<double, double> GetPeak(MagicFormulaArguments args)
        {
            var argsN = Normalize(args);
            
            double d = D(argsN);
            double c = C(argsN);
            double bcd = BCD(argsN);
            double b = bcd / (c * d);
            double e = E(argsN);

            double x = 0;
            for (int i = 0; i < 20; ++i)
            {
                x = (Math.Tan(Math.PI / (2 * c)) + e * Math.Atan(x)) / (e - 1);
            }
            if (x < 0) x = -x;
            x = x / b;
            var argNp = argsN.Copy();
            var argNm = argsN.Copy();
            double sh = Sh(argsN);
            argNp.SR = x - sh;
            argNm.SR = -x - sh;
            argNp = Denormalize(argNp);
            argNm = Denormalize(argNm);

            return new Tuple<double, double>(argNp.SR, argNm.SR);

        }
        public MagicFormulaArguments NormalizeOffsetParam = new MagicFormulaArguments(0, 0, 0, 0, 0, 0);
        public MagicFormulaArguments NormalizeScaleParam = new MagicFormulaArguments(1, 1, 1, 1, 1, 1);

        public override MagicFormulaArguments Normalize(MagicFormulaArguments args)
        {
            var v = new MagicFormulaArguments(0, 0, 0, 0, 0, 0);
            v.SA = (args.SA - NormalizeOffsetParam.SA) / NormalizeScaleParam.SA;
            v.SR = (args.SR - NormalizeOffsetParam.SR) / NormalizeScaleParam.SR;
            v.FZ = (args.FZ - NormalizeOffsetParam.FZ) / NormalizeScaleParam.FZ;
            v.IA = (args.IA - NormalizeOffsetParam.IA) / NormalizeScaleParam.IA;
            v.P = (args.P - NormalizeOffsetParam.P) / NormalizeScaleParam.P;
            v.T = (args.T - NormalizeOffsetParam.T) / NormalizeScaleParam.T;
            return v;
        }
        public override MagicFormulaArguments Denormalize(MagicFormulaArguments args)
        {
            var v = new MagicFormulaArguments(0, 0, 0, 0, 0, 0);
            v.SA = NormalizeScaleParam.SA*args.SA + NormalizeOffsetParam.SA;
            v.SR = NormalizeScaleParam.SR * args.SR + NormalizeOffsetParam.SR;
            v.FZ = NormalizeScaleParam.FZ * args.FZ + NormalizeOffsetParam.FZ;
            v.IA = NormalizeScaleParam.IA * args.IA + NormalizeOffsetParam.IA;
            v.P = NormalizeScaleParam.P * args.P + NormalizeOffsetParam.P;
            v.T = NormalizeScaleParam.T * args.T + NormalizeOffsetParam.T;
            return v;
        }
        override public double X(MagicFormulaArguments args)
        {
            return args.SR;
        }
        override public double C(MagicFormulaArguments args)
        {
            var a = Parameters;
            return a[0];
        }
        override public double D(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];

            return a1 * FZ * (a2 * FZ + 1) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        override public double BCD(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];

            return a7 * FZ * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * (a11 * Math.Pow(P, 2) + a10 * P + 1) * (a12 * T + 1);

        }
        override public double E(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];


            return a13 * (a15 * Math.Pow(FZ, 2) + a14 * FZ + 1) * (1 - a16 * Math.Sign(SR));
        }
        override public double Sh(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a17 = a[17];
            double a18 = a[18];


            return a18 * FZ + a17;
        }
        override public double Sv(MagicFormulaArguments args)
        {
            return 0;
        }
        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for(int i= 0; i<Math.Min(Parameters.Count,numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradPureFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradPureFunctions.Add(dF_da0);
            GradPureFunctions.Add(dF_da1);
            GradPureFunctions.Add(dF_da2);
            GradPureFunctions.Add(dF_da3);
            GradPureFunctions.Add(dF_da4);
            GradPureFunctions.Add(dF_da5);
            GradPureFunctions.Add(dF_da6);
            GradPureFunctions.Add(dF_da7);
            GradPureFunctions.Add(dF_da8);
            GradPureFunctions.Add(dF_da9);
            GradPureFunctions.Add(dF_da10);
            GradPureFunctions.Add(dF_da11);
            GradPureFunctions.Add(dF_da12);
            GradPureFunctions.Add(dF_da13);
            GradPureFunctions.Add(dF_da14);
            GradPureFunctions.Add(dF_da15);
            GradPureFunctions.Add(dF_da16);
            GradPureFunctions.Add(dF_da17);
            GradPureFunctions.Add(dF_da18);
        }
        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            arg = Normalize(arg);

            var result = new FuncResult();
            result.value = PureFunction(arg) - data.FX;
            result.grads = GradPure(arg);
            return result;

        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }
        #region diff

        private double dF_da0(MagicFormulaArguments args)
        {
            return dF_dC(args);
        }
        private double dF_da1(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * FZ * (a2 * FZ + 1) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da2(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * a1 * Math.Pow(FZ, 2) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da3(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * -a1 * FZ * (a2 * FZ + 1) * Math.Pow(IA, 2) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da4(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * a1 * FZ * (a2 * FZ + 1) * (1 - a3 * Math.Pow(IA, 2)) * P * (a6 * T + 1);
        }
        private double dF_da5(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * a1 * FZ * (a2 * FZ + 1) * (1 - a3 * Math.Pow(IA, 2)) * Math.Pow(P, 2) * (a6 * T + 1);
        }
        private double dF_da6(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * a1 * FZ * (a2 * FZ + 1) * (1 - a3 * Math.Pow(IA, 2)) * Math.Pow(P, 2) * (a6 * T + 1);
        }

        private double dF_da7(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];


            return dF_dBCD(args) * FZ * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * (a11 * Math.Pow(P, 2) + a10 * P + 1) * (a12 * T + 1);
        }
        private double dF_da8(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * a7 * Math.Pow(FZ, 2) * Math.Exp(a9 * FZ) * (a11 * Math.Pow(P, 2) + a10 * P + 1) * (a12 * T + 1);
        }
        private double dF_da9(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];

            return dF_dBCD(args) * a7 * Math.Pow(FZ, 2) * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * (a11 * Math.Pow(P, 2) + a10 * P + 1) * (a12 *
               T + 1);
        }
        private double dF_da10(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];

            return dF_dBCD(args) * a7 * FZ * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * P * (a12 * T + 1);
        }
        private double dF_da11(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];

            return dF_dBCD(args) * a7 * FZ * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * Math.Pow(P, 2) * (a12 * T + 1);
        }
        private double dF_da12(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];

            return dF_dBCD(args) * a7 * FZ * (a8 * FZ + 1) * Math.Exp(a9 * FZ) * (a11 * Math.Pow(P, 2) + a10 * P + 1) * T;
        }

        private double dF_da13(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];

            return dF_dE(args) * (a15 * Math.Pow(FZ, 2) + a14 * FZ + 1) * (1 - a16 * Math.Sign(SR));
        }
        private double dF_da14(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];

            return dF_dE(args) * a13 * FZ * (1 - a16 * Math.Sign(SR));
        }
        private double dF_da15(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];

            return dF_dE(args) * -a13 * (a15 * Math.Pow(FZ, 2) + a14 * FZ + 1) * Math.Sign(SR);
        }
        private double dF_da16(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];

            return dF_dE(args) * (-a13 * (a15 * Math.Pow(FZ, 2) + a14 * FZ + 1) * Math.Sign(SR));
        }

        private double dF_da17(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) *1;
        }
        private double dF_da18(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) * FZ;
        }
        #endregion
    }

    [Serializable]
    public class CombinedFXMagicFormula : SinTypeCombinedMagicFormula, ApproximatingCurve
    {
        const int numParam = 7;
        public CombinedFXMagicFormula(PureFXMagicFormula fx)
            :base(fx)
        {
            
            Parameters = new List<double>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
            }
            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }
            ResetDiff();
        }

        override public double Y(MagicFormulaArguments args)
        {
            return args.SA;
        }
        override public double Svy(MagicFormulaArguments args)
        {
            return 0;
        }
        override public double Shy(MagicFormulaArguments args)
        {
            var b = Parameters;

            double b6 = b[6];

            return b6;
        }
        override public double By(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];


            return b0 * (b1 * Math.Pow(IA, 2) + 1) / Math.Sqrt(Math.Pow(b2, 2) * Math.Pow(SR, 2) + 1);
        }
        override public double Cy(MagicFormulaArguments args)
        {
            var b = Parameters;
            double b3 = b[3];

            return b3;
        }
        override public double Ey(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b4 = b[4];
            double b5 = b[5];

            return b4 * (1 + b5 * FZ);
        }

        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            arg = Normalize(arg);

            var result = new FuncResult();
            result.value = CombinedFunction(arg) - data.FX;
            result.grads = GradCombined(arg);
            return result;

        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }
        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradCombinedFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradCombinedFunctions.Add(dCF_db0);
            GradCombinedFunctions.Add(dCF_db1);
            GradCombinedFunctions.Add(dCF_db2);
            GradCombinedFunctions.Add(dCF_db3);
            GradCombinedFunctions.Add(dCF_db4);
            GradCombinedFunctions.Add(dCF_db5);
            GradCombinedFunctions.Add(dCF_db6);
        }
        #region diff

        private double dCF_db0(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];


            return dCF_dBy(args) * (b1 * Math.Pow(IA, 2) + 1) / Math.Sqrt(Math.Pow(b2, 2) * Math.Pow(SR, 2) + 1);
        }
        private double dCF_db1(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];


            return dCF_dBy(args) * b0 * Math.Pow(IA, 2) / Math.Sqrt(Math.Pow(b2, 2) * Math.Pow(SR, 2) + 1);
        }
        private double dCF_db2(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];


            return dCF_dBy(args) * -b0 * b2 * (b1 * Math.Pow(IA, 2) + 1) * Math.Pow(SR, 2) * Math.Pow(Math.Pow(b2, 2) *
    Math.Pow(SR, 2) + 1, (-3.0) / 2.0);
        }

        private double dCF_db3(MagicFormulaArguments args)
        {
            return dCF_dCy(args);
        }

        private double dCF_db4(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b4 = b[4];
            double b5 = b[5];

            return dCF_dEy(args) * (b5 * FZ + 1);
        }
        private double dCF_db5(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b4 = b[4];
            double b5 = b[5];

            return dCF_dEy(args) * b4 * FZ;
        }
        private double dCF_db6(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            return dCF_dShy(args);
        }
        #endregion
    }

    [Serializable]
    public class PureFYMagicFormula : SinTypePureMagicFormula, ApproximatingCurve
    {
        public string DTex = @"{D} = { FZ}\,\left({ a_2}\,{ FZ}+{ a_1}\right)\,\left(1-{ a_3}\,{ IA}^2\right)\,\left({ a_5}\,P^2+{ a_4}\,P+1\right)\,\left({ a_6}\,T+1\right)";
        public string BCDTex = @"{BCD}={ a_7}\,{ FZ}\,\left({ a_8}\,P + 1\right)\,\sin \left({ a_9}\,\arctan \left({\frac{ { FZ} } {\left({ a_{ 10} } +{ a_{ 11} }\, { IA}^ 2\right)\,\left(1 +{ a_{ 12} }\,P\right)} }\right)\right)\,\left(1 -{ a_{ 13} }\,\left | { IA}\right | \right)\,\left({ a_{ 14} }\,T + 1\right)";
        public string ETex = @"{E}=\left({ a_{15}}+{ a_{16}}\,{ FZ}\right)\,\left({ a_{17}}\,{ IA}^2-{ a_{18}}\,{ IA}\,{ sgn}\left({ x}+{ Sh}\right)+1\right)";
        public string ShTex = @"{S_h}=\left({ a_{19}}\,{ FZ}+{ a_{20}}\,{ FZ}^2\right)\,\left({ a_{21}}\,P+1\right){ IA}";
        public string SvTex = @"{S_v}= \left({ a_{23}}\,{ FZ}+{ a_{24}}\,{ FZ}^2 \right)\, { IA}";

        const int numParam = 24;
        public PureFYMagicFormula(PureFXMagicFormula mfx)
        {
            MFX = mfx;
            
            Parameters = new List<double>(numParam);
            
            for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
            }
            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }
            ResetDiff();
        }
        public override Tuple<double, double> GetPeak(MagicFormulaArguments args)
        {
            var argsN = Normalize(args);
            double sh = Sh(argsN);
            double d = D(argsN);
            double c = C(argsN);
            double bcd = BCD(argsN);
            double b = bcd / (c * d);
            double e = E(argsN);

            double x = 0;
            for (int i = 0; i < 20; ++i)
            {
                x = (Math.Tan(Math.PI / (2 * c)) + e * Math.Atan(x)) / (e - 1);
            }
            
            x = x / b;
            if (x > 0) x = -x;
            var argNp = argsN.Copy();
            var argNm = argsN.Copy();
            argNp.SA = x - sh;
            argNm.SA = -x - sh;
            var argNp2 = Denormalize(argNp);
            var argNm2 = Denormalize(argNm);
            double fy = PureFunction(argNp);
            return new Tuple<double, double>(argNp2.SA, argNm2.SA);

        }
        PureFXMagicFormula MFX;
        public override MagicFormulaArguments Normalize(MagicFormulaArguments args)
        {
            return MFX.Normalize(args);
        }
        public override MagicFormulaArguments Denormalize(MagicFormulaArguments args)
        {
            return MFX.Denormalize(args);
        }
        override public double X(MagicFormulaArguments args)
        {
            return args.SA;
        }
        override public double C(MagicFormulaArguments args)
        {
            var a = Parameters;
            return a[0];
        }
        override public double D(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];

            return FZ * (a2 * FZ + a1) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        override public double BCD(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return a7 * FZ * (1 - a13 * Math.Abs(IA)) * (a8 * P + 1) * Math.Sin(a9 * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);

        }
        override public double E(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];


            return (a16 * FZ + a15) * (-a18 * IA * Math.Sign(SA + Sh(args)) + a17 * Math.Pow(IA, 2) + 1);
        }
        override public double Sh(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a19 = a[19];
            double a20 = a[20];
            double a21 = a[21];

            return (a20 * Math.Pow(FZ, 2) + a19 * FZ) * IA * (a21 * P + 1);
        }
        override public double Sv(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a22 = a[22];
            double a23 = a[23];

            return (a23 * Math.Pow(FZ, 2) + a22 * FZ) * IA;
        }

        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            arg = Normalize(arg);

            var result = new FuncResult();
            result.value = PureFunction(arg) - data.FY;
            result.grads = GradPure(arg);
            return result;

        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }
        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradPureFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradPureFunctions.Add(dF_da0);
            GradPureFunctions.Add(dF_da1);
            GradPureFunctions.Add(dF_da2);
            GradPureFunctions.Add(dF_da3);
            GradPureFunctions.Add(dF_da4);
            GradPureFunctions.Add(dF_da5);
            GradPureFunctions.Add(dF_da6);
            GradPureFunctions.Add(dF_da7);
            GradPureFunctions.Add(dF_da8);
            GradPureFunctions.Add(dF_da9);
            GradPureFunctions.Add(dF_da10);
            GradPureFunctions.Add(dF_da11);
            GradPureFunctions.Add(dF_da12);
            GradPureFunctions.Add(dF_da13);
            GradPureFunctions.Add(dF_da14);
            GradPureFunctions.Add(dF_da15);
            GradPureFunctions.Add(dF_da16);
            GradPureFunctions.Add(dF_da17);
            GradPureFunctions.Add(dF_da18);
            GradPureFunctions.Add(dF_da19);
            GradPureFunctions.Add(dF_da20);
            GradPureFunctions.Add(dF_da21);
            GradPureFunctions.Add(dF_da22);
            GradPureFunctions.Add(dF_da23);
        }
        #region diff

        private double dF_da0(MagicFormulaArguments args)
        {
            return dF_dC(args);
        }

        private double dF_da1(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * FZ * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da2(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * Math.Pow(FZ, 2) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da3(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * -FZ * (a2 * FZ + a1) * Math.Pow(IA, 2) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * (a6 * T + 1);
        }
        private double dF_da4(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * FZ * (a2 * FZ + a1) * (1 - a3 * Math.Pow(IA, 2)) * P * (a6 * T + 1);
        }
        private double dF_da5(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * FZ * (a2 * FZ + a1) * (1 - a3 * Math.Pow(IA, 2)) * Math.Pow(P, 2) * (a6 * T + 1);
        }
        private double dF_da6(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            return dF_dD(args) * FZ * (a2 * FZ + a1) * (1 - a3 * Math.Pow(IA, 2)) * (a5 * Math.Pow(P, 2) + a4 * P + 1) * T;
        }

        private double dF_da7(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args)* FZ * (1 - a13 * Math.Abs(IA)) * (a8 * P + 1) * Math.Sin(a9 * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da8(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * a7 * FZ * (1 - a13 * Math.Abs(IA)) * P * Math.Sin(a9 * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da9(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * a7 * FZ * (1 - a13 * Math.Abs(IA)) * (a8 * P + 1) * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1)) * Math.Cos(a9 * Math.Atan(FZ *
    Math.Pow(a11 * Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da10(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * -a7 * a9 * Math.Pow(FZ, 2) * Math.Pow(a11 * Math.Pow(IA, 2) + a10, -2) * (1 - a13 * Math.Abs(IA)) *
    Math.Pow(a12 * P + 1, -1) * (a8 * P + 1) * Math.Pow(Math.Pow(FZ, 2) * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -2) * Math.Pow(a12 * P + 1, -2) + 1, -1) * Math.Cos(a9 * Math.Atan(FZ *
    Math.Pow(a11 * Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da11(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * -a7 * a9 * Math.Pow(FZ, 2) * Math.Pow(IA, 2) * Math.Pow(a11 * Math.Pow(IA, 2) + a10, -2) * (1 - a13 *
    Math.Abs(IA)) * Math.Pow(a12 * P + 1, -1) * (a8 * P + 1) * Math.Pow(Math.Pow(FZ, 2) *
    Math.Pow(a11 * Math.Pow(IA, 2) + a10, -2) * Math.Pow(a12 * P + 1, -2) + 1, -1) * Math.Cos(a9 *
    Math.Atan(FZ * Math.Pow(a11 * Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) *
    (a14 * T + 1);
        }
        private double dF_da12(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * -a7 * a9 * Math.Pow(FZ, 2) * Math.Pow(a11 * Math.Pow(IA, 2) + a10, -1) * (1 - a13 * Math.Abs(IA)) *
    P * Math.Pow(a12 * P + 1, -2) * (a8 * P + 1) * Math.Pow(Math.Pow(FZ, 2) * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -2) * Math.Pow(a12 * P + 1, -2) + 1, -1) * Math.Cos(a9 * Math.Atan(FZ *
    Math.Pow(a11 * Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da13(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * -a7 * FZ * Math.Abs(IA) * (a8 * P + 1) * Math.Sin(a9 * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * (a14 * T + 1);
        }
        private double dF_da14(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];

            return dF_dBCD(args) * a7 * FZ * (1 - a13 * Math.Abs(IA)) * (a8 * P + 1) * Math.Sin(a9 * Math.Atan(FZ * Math.Pow(a11 *
    Math.Pow(IA, 2) + a10, -1) * Math.Pow(a12 * P + 1, -1))) * T;
        }

        private double dF_da15(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            double a19 = a[19];

            return dF_dE(args) * -a18 * IA * Math.Sign(SA + Sh(args)) + a17 * Math.Pow(IA, 2) + 1;
        }
        private double dF_da16(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            double a19 = a[19];

            return dF_dE(args) * FZ * (-a18 * IA * Math.Sign(SA + Sh(args)) + a17 * Math.Pow(IA, 2) + 1);
        }
        private double dF_da17(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            double a19 = a[19];

            return dF_dE(args) * (a16 * FZ + a15) * Math.Pow(IA, 2);
        }
        private double dF_da18(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            double a19 = a[19];

            return dF_dE(args) * -(a16 * FZ + a15) * IA * Math.Sign(SA + Sh(args));
        }
  
        private double dF_da19(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a19 = a[19];
            double a20 = a[20];
            double a21 = a[21];

            return dF_dSh(args) * FZ * IA * (a21 * P + 1);
        }
        private double dF_da20(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a19 = a[19];
            double a20 = a[20];
            double a21 = a[21];

            return dF_dSh(args) * Math.Pow(FZ, 2) * IA * (a21 * P + 1);
        }
        private double dF_da21(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a19 = a[19];
            double a20 = a[20];
            double a21 = a[21];

            return dF_dSh(args) * (a20 * Math.Pow(FZ, 2) + a19 * FZ) * IA * P;
        }

        private double dF_da22(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a22 = a[22];
            double a23 = a[23];

            return dF_dSv(args) * FZ * IA;
        }
        private double dF_da23(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a22 = a[22];
            double a23 = a[23];

            return dF_dSv(args) * Math.Pow(FZ, 2) * IA;
        }

        #endregion
    }

    [Serializable]
    public class CombinedFYMagicFormula : SinTypeCombinedMagicFormula, ApproximatingCurve
    {
        const int numParam = 15;
        public CombinedFYMagicFormula(PureFYMagicFormula fy)
            : base(fy)
        {
            
            Parameters = new List<double>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
            }

            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }
            ResetDiff();
        }

        override public double Y(MagicFormulaArguments args)
        {
            return args.SR;
        }
        override public double Svy(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return m * FZ * (b2 * IA + b1 * FZ + b0) * Math.Sin(b4 * Math.Atan(b5 * IA)) /
    Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }
        override public double Shy(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b6 = b[6];
            double b7 = b[7];

            return (b6 + b7 * FZ)*IA;
        }
        override public double By(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b8 = b[8];
            double b9 = b[9];
            double b10 = b[10];
            double b11 = b[11];


            return (b9 * Math.Pow(IA, 2) + b8) / Math.Sqrt(Math.Pow(b10, 2) * Math.Pow(SA - b11, 2) + 1);
        }
        override public double Cy(MagicFormulaArguments args)
        {
            var b = Parameters;
            double b12 = b[12];

            return b12;
        }
        override public double Ey(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b13 = b[13];
            double b14 = b[14];

            return (b13 + b14 * FZ);
        }

        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            arg = Normalize(arg);

            var result = new FuncResult();
            result.value = CombinedFunction(arg) - data.FY;
            result.grads = GradCombined(arg);
            return result;

        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }

        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradCombinedFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradCombinedFunctions.Add(dCF_db0);
            GradCombinedFunctions.Add(dCF_db1);
            GradCombinedFunctions.Add(dCF_db2);
            GradCombinedFunctions.Add(dCF_db3);
            GradCombinedFunctions.Add(dCF_db4);
            GradCombinedFunctions.Add(dCF_db5);
            GradCombinedFunctions.Add(dCF_db6);
            GradCombinedFunctions.Add(dCF_db7);
            GradCombinedFunctions.Add(dCF_db8);
            GradCombinedFunctions.Add(dCF_db9);
            GradCombinedFunctions.Add(dCF_db10);
            GradCombinedFunctions.Add(dCF_db11);
            GradCombinedFunctions.Add(dCF_db12);
            GradCombinedFunctions.Add(dCF_db13);
            GradCombinedFunctions.Add(dCF_db14);
        }
        #region diff

        private double dCF_db0(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * m * FZ * Math.Sin(b4 * Math.Atan(b5 * IA)) / Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }
        private double dCF_db1(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * m * Math.Pow(FZ, 2) * Math.Sin(b4 * Math.Atan(b5 * IA)) /
    Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }
        private double dCF_db2(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * m * FZ * IA * Math.Sin(b4 * Math.Atan(b5 * IA)) /
    Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }
        private double dCF_db3(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * -b3 * m * FZ * (b2 * IA + b1 * FZ + b0) * Math.Sin(b4 * Math.Atan(b5 * IA)) * Math.Pow(SA, 2) *
    Math.Pow(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1, (-3.0) / 2.0);
        }
        private double dCF_db4(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * m * FZ * (b2 * IA + b1 * FZ + b0) * Math.Atan(b5 * IA) * Math.Cos(b4 * Math.Atan(b5 * IA)) /
    Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }
        private double dCF_db5(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b0 = b[0];
            double b1 = b[1];
            double b2 = b[2];
            double b3 = b[3];
            double b4 = b[4];
            double b5 = b[5];

            double m = PureMagicFormula.D(args) / FZ;

            return dCF_dSvy(args) * b4 * m * FZ * IA * (b2 * IA + b1 * FZ + b0) * Math.Pow(Math.Pow(b5, 2) * Math.Pow(IA, 2) + 1, -1) *
    Math.Cos(b4 * Math.Atan(b5 * IA)) / Math.Sqrt(Math.Pow(b3, 2) * Math.Pow(SA, 2) + 1);
        }

        private double dCF_db6(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b6 = b[6];
            double b7 = b[7];

            return dCF_dShy(args) * IA;
        }
        private double dCF_db7(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b6 = b[6];
            double b7 = b[7];

            return dCF_dShy(args) * FZ * IA;
        }

        private double dCF_db8(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b8 = b[8];
            double b9 = b[9];
            double b10 = b[10];
            double b11 = b[11];

            return dCF_dBy(args) * 1 / Math.Sqrt(Math.Pow(b10, 2) * Math.Pow(SA - b11, 2) + 1);
        }
        private double dCF_db9(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b8 = b[8];
            double b9 = b[9];
            double b10 = b[10];
            double b11 = b[11];

            return dCF_dBy(args) * Math.Pow(IA, 2) / Math.Sqrt(Math.Pow(b10, 2) * Math.Pow(SA - b11, 2) + 1);
        }
        private double dCF_db10(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b8 = b[8];
            double b9 = b[9];
            double b10 = b[10];
            double b11 = b[11];

            return dCF_dBy(args) * -b10 * (b9 * Math.Pow(IA, 2) + b8) * Math.Pow(SA - b11, 2) * Math.Pow(Math.Pow(b10, 2) *
    Math.Pow(SA - b11, 2) + 1, (-3.0) / 2.0);
        }
        private double dCF_db11(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b8 = b[8];
            double b9 = b[9];
            double b10 = b[10];
            double b11 = b[11];

            return dCF_dBy(args) * Math.Pow(b10, 2) * (b9 * Math.Pow(IA, 2) + b8) * (SA - b11) * Math.Pow(Math.Pow(b10, 2) *
    Math.Pow(SA - b11, 2) + 1, (-3.0) / 2.0);
        }

        private double dCF_db12(MagicFormulaArguments args)
        {

            return dCF_dCy(args);
        }

        private double dCF_db13(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b13 = b[13];
            double b14 = b[14];

            return dCF_dEy(args);

        }
        private double dCF_db14(MagicFormulaArguments args)
        {
            var b = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double b13 = b[13];
            double b14 = b[14];

            return dCF_dEy(args) * FZ;
        }

        #endregion
    }

    #endregion

    #region M

    [Serializable]
    public abstract class CosTypeMagicFormula
    {
        public CosTypeMagicFormula(SinTypeCombinedMagicFormula x, SinTypeCombinedMagicFormula y)
        {
            MFX = x;
            MFY = y;
        }

        public SinTypeCombinedMagicFormula MFX;
        public SinTypeCombinedMagicFormula MFY;

        public List<double> Parameters { get; protected set; }
        public List<bool> FittingParameters { get; protected set; }
        public double PureFunction(MagicFormulaArguments args)
        {
            double x = X(args);
            double xc = x + Sh(args);

            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            xc = xc * b;
            return d * Math.Cos(c * Math.Atan(xc - e * (xc - Math.Atan(xc)))) * Math.Cos(x);
        }
        public double CombinedFunction(MagicFormulaArguments args)
        {
            double x = X(args);
            double xc = XC(x + Sh(args), args);

            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            xc = xc * b;
            return d * Math.Cos(c * Math.Atan(xc - e * (xc - Math.Atan(xc)))) * Math.Cos(x);
        }

        public abstract double X(MagicFormulaArguments args);
        public abstract double B(MagicFormulaArguments args);
        public abstract double C(MagicFormulaArguments args);
        public abstract double D(MagicFormulaArguments args);
        public abstract double E(MagicFormulaArguments args);
        public abstract double Sh(MagicFormulaArguments args);

        public double XC(double x, MagicFormulaArguments args)
        {
            double BCDx = MFX.PureMagicFormula.BCD(args);
            double BCDy = MFY.PureMagicFormula.BCD(args);
            return Math.Atan(Math.Sqrt(Math.Pow(Math.Tan(x), 2) + Math.Pow(BCDx / BCDy * args.SR, 2))) * Math.Sign(x);
        }

        abstract public MagicFormulaArguments Normalize(MagicFormulaArguments args);
        abstract public MagicFormulaArguments Denormalize(MagicFormulaArguments args);
        #region diff

        public List<Func<MagicFormulaArguments, double>> GradPureFunctions;
        public List<double> GradPure(MagicFormulaArguments args)
        {
            var result = new List<double>();
            foreach (var func in GradPureFunctions)
            {
                result.Add(func(args));
            }
            return result;
        }

        public double dF_dB(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            return -c * d * (e * (-(x + sh) * Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1) + x + sh) - x - sh) *
    Math.Cos(x) * Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b *
    (x + sh), 2) + 1, -1) * Math.Sin(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b *
    (x + sh)));
        }
        public double dF_dC(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            return -d * Math.Cos(x) * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)) * Math.Sin(c *
    Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dD(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            return Math.Cos(x) * Math.Cos(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dE(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            return -c * d * Math.Cos(x) * Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b *
    (x + sh), 2) + 1, -1) * (b * (x + sh) - Math.Atan(b * (x + sh))) * Math.Sin(c * Math.Atan(e * (b *
    (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }
        public double dF_dSh(MagicFormulaArguments args)
        {
            double x = X(args);
            double sh = Sh(args);
            double d = D(args);
            double c = C(args);
            double b = B(args);
            double e = E(args);
            return -c * d * (e * (b - b * Math.Pow(Math.Pow(b, 2) * Math.Pow(x + sh, 2) + 1, -1)) - b) * Math.Cos(x) *
                Math.Pow(Math.Pow(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh), 2) + 1, -1) *
    Math.Sin(c * Math.Atan(e * (b * (x + sh) - Math.Atan(b * (x + sh))) - b * (x + sh)));
        }

        #endregion
    }

    //diffだめ
    [Serializable]
    public class PTMagicFormula : CosTypeMagicFormula, ApproximatingCurve
    {
        const int numParam = 19;
        public PTMagicFormula(SinTypeCombinedMagicFormula x, SinTypeCombinedMagicFormula y)
            : base(x, y)
        {
            
            Parameters = new List<double>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
            }
            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }
            ResetDiff();
        }

        override public MagicFormulaArguments Normalize(MagicFormulaArguments args)
        {
            return MFX.Normalize(args);
        }
        override public MagicFormulaArguments Denormalize(MagicFormulaArguments args)
        {
            return MFX.Denormalize(args);
        }
        override public double X(MagicFormulaArguments args)
        {
            return args.SA;
        }
        override public double B(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];

            return  (a2 * Math.Pow(FZ, 2) + a1 * FZ + a0) * (a3 * Math.Abs(IA) + 1);
        }
        override public double C(MagicFormulaArguments args)
        {
            var a = Parameters;
            return a[4];
        }
        override public double D(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            return FZ * (a5 * FZ + a6) * (a8 * Math.Abs(IA) + a9 * Math.Pow(IA, 2) + 1) * (1 - a7 * P);
        }
        override public double E(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            return  (a12 * Math.Pow(FZ, 2) + a11 * FZ + a10 ) * (2 * Math.Pow(Math.PI, -1) * (a14 * IA + a13) * Math.Atan(a0 *
    a4 * (a2 * Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) *
    IA + a15 * (a16 * FZ + 1))) + 1); ;
        }
        override public double Sh(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];
            return a15  + a16 * FZ + a17 * (1 + a18 * FZ) * IA;
        }

        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            arg = Normalize(arg);

            double value = PureFunction(arg);
            double F = MFY.PureFunction(arg) - MFY.PureMagicFormula.Sv(arg);

            var result = new FuncResult();
            result.value = -value * F - data.MZ;
            result.grads = GradPure(arg);
            return result;
        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }
        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradPureFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradPureFunctions.Add(dF_da0);
            GradPureFunctions.Add(dF_da1);
            GradPureFunctions.Add(dF_da2);
            GradPureFunctions.Add(dF_da3);
            GradPureFunctions.Add(dF_da4);
            GradPureFunctions.Add(dF_da5);
            GradPureFunctions.Add(dF_da6);
            GradPureFunctions.Add(dF_da7);
            GradPureFunctions.Add(dF_da8);
            GradPureFunctions.Add(dF_da9);
            GradPureFunctions.Add(dF_da10);
            GradPureFunctions.Add(dF_da11);
            GradPureFunctions.Add(dF_da12);
            GradPureFunctions.Add(dF_da13);
            GradPureFunctions.Add(dF_da14);
            GradPureFunctions.Add(dF_da15);
            GradPureFunctions.Add(dF_da16);
            GradPureFunctions.Add(dF_da17);
            GradPureFunctions.Add(dF_da18);
        }
        #region diff

        private double dF_da0(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dB(args) * ((a2 * Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1)) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a10 * a4 * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1) * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 *
    FZ + 1)) * Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da1(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dB(args) * (a0 * FZ * (a3 * Math.Abs(IA) + 1)) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a4 * FZ * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a14 * IA + a13) * (a3 *
    Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 * FZ + 1)) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da2(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dB(args) * (a0 * Math.Pow(FZ, 2) * (a3 * Math.Abs(IA) + 1)) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a4 * Math.Pow(FZ, 2) * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a14 *
    IA + a13) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 * FZ + 1)) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da3(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dB(args) * (a0 * (a2 * Math.Pow(FZ, 2) + a1 * FZ + 1) * Math.Abs(IA)) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a4 * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a14 * IA + a13) * Math.Abs(IA) * (SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1)) * Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 *
    (a18 * FZ + 1) * IA + a15 * (a16 * FZ + 1), 2) + 1, -1));
        }

        private double dF_da4(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dC(args) * dF_dE(args) + (2 * Math.Pow(Math.PI, -1) * a0 * a10 * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1) * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 *
    FZ + 1)) * Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }

        private double dF_da5(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dD(args) * (FZ * (FZ + a6 + 1) * (a8 * Math.Abs(IA) + a9 * Math.Pow(IA, 2) + 1) * (1 - a7 * P));
        }
        private double dF_da6(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dD(args) * (a5 * FZ * (a8 * Math.Abs(IA) + a9 * Math.Pow(IA, 2) + 1) * (1 - a7 * P));
        }
        private double dF_da7(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dD(args) * (-a5 * FZ * (FZ + a6 + 1) * (a8 * Math.Abs(IA) + a9 * Math.Pow(IA, 2) + 1) * P);
        }
        private double dF_da8(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dD(args) * (a5 * FZ * (FZ + a6 + 1) * Math.Abs(IA) * (1 - a7 * P));
        }
        private double dF_da9(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dD(args) * (a5 * FZ * (FZ + a6 + 1) * Math.Pow(IA, 2) * (1 - a7 * P));
        }

        private double dF_da10(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dE(args) * ((a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (2 * Math.Pow(Math.PI, -1) * (a14 * IA + a13) * Math.Atan(a0 * a4 *
    (a2 * Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1))) + 1));
        }
        private double dF_da11(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dE(args) * (a10 * FZ * (2 * Math.Pow(Math.PI, -1) * (a14 * IA + a13) * Math.Atan(a0 * a4 * (a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 * FZ + 1))) + 1));
        }
        private double dF_da12(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dE(args) * (a10 * Math.Pow(FZ, 2) * (2 * Math.Pow(Math.PI, -1) * (a14 * IA + a13) * Math.Atan(a0 * a4 * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 *
    FZ + 1))) + 1));
        }
        private double dF_da13(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a10 * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * Math.Atan(a0 * a4 * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 *
    FZ + 1))));
        }
        private double dF_da14(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a10 * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * IA * Math.Atan(a0 * a4 * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a3 * Math.Abs(IA) + 1) * (SA + a17 * (a18 * FZ + 1) * IA + a15 * (a16 *
    FZ + 1))));
        }

        private double dF_da15(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) * (a16 * FZ + 1) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a4 * (a16 * FZ + 1) * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da16(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) * (a15 * FZ) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a15 * a4 * FZ * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da17(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) * ((a18 * FZ + 1) * IA) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a4 * (a18 * FZ + 1) * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * IA * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        private double dF_da18(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a0 = a[0];
            double a1 = a[1];
            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];
            double a9 = a[9];
            double a10 = a[10];
            double a11 = a[11];
            double a12 = a[12];
            double a13 = a[13];
            double a14 = a[14];
            double a15 = a[15];
            double a16 = a[16];
            double a17 = a[17];
            double a18 = a[18];

            return dF_dSh(args) * (a17 * FZ * IA) + dF_dE(args) * (2 * Math.Pow(Math.PI, -1) * a0 * a10 * a17 * a4 * FZ * (a12 * Math.Pow(FZ, 2) + a11 * FZ + 1) * (a2 *
    Math.Pow(FZ, 2) + a1 * FZ + 1) * IA * (a14 * IA + a13) * (a3 * Math.Abs(IA) + 1) *
    Math.Pow(Math.Pow(a0, 2) * Math.Pow(a4, 2) * Math.Pow(a2 * Math.Pow(FZ, 2) + a1 *
    FZ + 1, 2) * Math.Pow(a3 * Math.Abs(IA) + 1, 2) * Math.Pow(SA + a17 * (a18 * FZ + 1) * IA + a15 *
    (a16 * FZ + 1), 2) + 1, -1));
        }
        #endregion

    }

    [Serializable]
    public class CombinedMzMember : ApproximatingCurve
    {
        const int numParam = 4;
        public CombinedMzMember(PTMagicFormula ptm)
        {
            PTM = ptm;
            
            Parameters = new List<double>(numParam);
            FittingParameters = new List<bool>(numParam);
                        for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
                FittingParameters.Add(true);
            }
        }

        public PTMagicFormula PTM;

        public List<double> Parameters
        {
            get; private set;
        }
        public List<bool> FittingParameters { get; protected set; }
        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            var arg2 = new MagicFormulaArguments(data.SA, data.SR, data.FZ, 0, data.P, data.TSTC);
            arg = PTM.Normalize(arg);
            arg2 = PTM.Normalize(arg2);

            double Fy = PTM.MFY.PureFunction(arg2) - PTM.MFY.PureMagicFormula.Sv(arg2);
            double Fx = PTM.MFX.CombinedFunction(arg);
            Fy = Fy * PTM.MFY.G(arg2);

            var result = new FuncResult();
            result.value = -PTM.CombinedFunction(arg) * Fy + Function(arg) * Fx - data.MZ;
            result.grads = Grad(arg);

            for (int i = 0; i < result.grads.Count; ++i) result.grads[i] *= Fx;

            return result;
        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }


        public double Function(MagicFormulaArguments args)
        {

            var s = Parameters;

            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double s1 = s[0];
            double s2 = s[1];
            double s3 = s[2];
            double s4 = s[3];
            double Fy = PTM.MFY.CombinedFunction(args);

            return s1 + s2 * (Fy / FZ) + (s3 + s4 * FZ) * IA;
        }

        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
        }

        public List<double> Grad(MagicFormulaArguments args)
        {
            var result = new List<double>();
            result.Add(dF_ds1(args));
            result.Add(dF_ds2(args));
            result.Add(dF_ds3(args));
            result.Add(dF_ds4(args));
            return result;
        }

        public double dF_ds1(MagicFormulaArguments args)
        {
            var s = Parameters;

            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double s1 = s[1];
            double s2 = s[2];
            double s3 = s[3];
            double s4 = s[4];
            return 1;
        }
        public double dF_ds2(MagicFormulaArguments args)
        {
            var s = Parameters;

            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double s1 = s[1];
            double s2 = s[2];
            double s3 = s[3];
            double s4 = s[4];
            double Fy = PTM.MFY.CombinedFunction(args);
            return Fy / FZ;
        }
        public double dF_ds3(MagicFormulaArguments args)
        {
            var s = Parameters;

            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double s1 = s[1];
            double s2 = s[2];
            double s3 = s[3];
            double s4 = s[4];
            return IA;
        }
        public double dF_ds4(MagicFormulaArguments args)
        {
            var s = Parameters;

            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double s1 = s[1];
            double s2 = s[2];
            double s3 = s[3];
            double s4 = s[4];
            return IA * FZ;
        }

    }

    [Serializable]
    public class MzrMagicFormula : CosTypeMagicFormula, ApproximatingCurve
    {
        const int numParam = 9;
        public MzrMagicFormula(CombinedMzMember cmzm)
            : base(cmzm.PTM.MFX, cmzm.PTM.MFY)
        {
            CMZM = cmzm;

            
            Parameters = new List<double>(numParam);
            
            for (int i = 0; i < numParam; ++i)
            {
                Parameters.Add(0);
            }
            FittingParameters = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                FittingParameters.Add(true);
            }
            ResetDiff();
        }

        public CombinedMzMember CMZM;
        override public MagicFormulaArguments Normalize(MagicFormulaArguments args)
        {
            return CMZM.PTM.Normalize(args);
        }
        override public MagicFormulaArguments Denormalize(MagicFormulaArguments args)
        {
            return CMZM.PTM.Denormalize(args);
        }
        override public double X(MagicFormulaArguments args)
        {
            return args.SA;
        }
        override public double B(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a0 = a[0];

            return a0 * (1 + a1 * MFY.PureMagicFormula.BCD(args) / MFY.PureMagicFormula.D(args)); ;
        }
        override public double C(MagicFormulaArguments args)
        {
            return 1;
        }
        override public double D(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return FZ * (a4 * (a5 * FZ + 1) * IA * (a6 * P + 1) + a7 * (a8 * FZ + 1) * IA * Math.Abs(IA) + a2 * (a3 * FZ + 1));
        }
        override public double E(MagicFormulaArguments args)
        {
            return 0;
        }
        override public double Sh(MagicFormulaArguments args)
        {
            return MFY.PureMagicFormula.Sh(args) + MFY.PureMagicFormula.Sv(args) / MFY.PureMagicFormula.BCD(args);
        }

        public FuncResult Error(TireData data)
        {
            var arg = new MagicFormulaArguments(data.SA, data.SR, data.FZ, data.IA, data.P, data.TSTC);
            var arg2 = new MagicFormulaArguments(data.SA, data.SR, data.FZ, 0, data.P, data.TSTC);
            arg = CMZM.PTM.Normalize(arg);
            arg2 = CMZM.PTM.Normalize(arg2);

            double Fy = CMZM.PTM.MFY.PureFunction(arg2) - CMZM.PTM.MFY.PureMagicFormula.Sv(arg2);
            double Fx = CMZM.PTM.MFX.CombinedFunction(arg);
            Fy = Fy * CMZM.PTM.MFY.G(arg2);

            var result = new FuncResult();
            result.value = -CMZM.PTM.CombinedFunction(arg) * Fy + CMZM.Function(arg) * Fx + CombinedFunction(arg) - data.MZ;
            result.grads = GradPure(arg);
            return result;
        }
        public List<Func<FuncResult>> ConstraintsPure()
        {
            var list = new List<Func<FuncResult>>();
            return list;
        }
        public List<Func<TireData, FuncResult>> ConstraintsDependOnData()
        {
            var list = new List<Func<TireData, FuncResult>>();
            return list;
        }
        public void ResetDiff()
        {
            var param = new List<double>(numParam);
            var fitting = new List<bool>(numParam);
            for (int i = 0; i < numParam; ++i)
            {
                param.Add(0);
                fitting.Add(true);
            }
            if (Parameters == null) Parameters = new List<double>();
            if (FittingParameters == null) FittingParameters = new List<bool>();
            for (int i = 0; i < Math.Min(Parameters.Count, numParam); ++i)
            {
                param[i] = Parameters[i];
            }
            for (int i = 0; i < Math.Min(FittingParameters.Count, numParam); ++i)
            {
                fitting[i] = FittingParameters[i];
            }
            Parameters = param;
            FittingParameters = fitting;
            GradPureFunctions = new List<Func<MagicFormulaArguments, double>>();
            GradPureFunctions.Add(dF_da0);
            GradPureFunctions.Add(dF_da1);
            GradPureFunctions.Add(dF_da2);
            GradPureFunctions.Add(dF_da3);
            GradPureFunctions.Add(dF_da4);
            GradPureFunctions.Add(dF_da5);
            GradPureFunctions.Add(dF_da6);
            GradPureFunctions.Add(dF_da7);
            GradPureFunctions.Add(dF_da8);
        }

        #region diff

        private double dF_da0(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a0 = a[0];
            return dF_dB(args) * a1 * MFY.PureMagicFormula.BCD(args) / MFY.PureMagicFormula.D(args) + 1;
        }
        private double dF_da1(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a1 = a[1];
            double a0 = a[0];
            return dF_dB(args) * a0 * MFY.PureMagicFormula.BCD(args) / MFY.PureMagicFormula.D(args);
        }

        private double dF_da2(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * FZ * (a3 * FZ + 1);
        }
        private double dF_da3(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * a2 * Math.Pow(FZ, 2);
        }
        private double dF_da4(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * FZ * (a5 * FZ + 1) * IA * (a6 * P + 1);
        }
        private double dF_da5(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * a4 * Math.Pow(FZ, 2) * IA * (a6 * P + 1);
        }
        private double dF_da6(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * a4 * FZ * (a5 * FZ + 1) * IA * P;
        }
        private double dF_da7(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * FZ * (a8 * FZ + 1) * IA * Math.Abs(IA);
        }
        private double dF_da8(MagicFormulaArguments args)
        {
            var a = Parameters;
            double SA = args.SA;
            double SR = args.SR;
            double FZ = args.FZ;
            double IA = args.IA;
            double P = args.P;
            double T = args.T;

            double a2 = a[2];
            double a3 = a[3];
            double a4 = a[4];
            double a5 = a[5];
            double a6 = a[6];
            double a7 = a[7];
            double a8 = a[8];

            return dF_dD(args) * a7 * Math.Pow(FZ, 2) * IA * Math.Abs(IA);
        }

        #endregion
    }

    [Serializable]
    public class MZMagicFormula
    {
        public MZMagicFormula(MzrMagicFormula mzr)
        {
            MZR = mzr;
            PT = mzr.CMZM.PTM;
            CMZM = mzr.CMZM;
        }
        public void ResetDiff()
        {
            MZR.ResetDiff();
            PT.ResetDiff();
            CMZM.ResetDiff();
        }

        public MzrMagicFormula MZR { get; private set; }
        public PTMagicFormula PT { get; private set; }
        public CombinedMzMember CMZM { get; private set; }
        public double PureFunction(MagicFormulaArguments arg)
        {
            var arg2 = new MagicFormulaArguments(arg.SA, arg.SR, arg.FZ, 0, arg.P, arg.T);

            double Fy = MZR.CMZM.PTM.MFY.PureFunction(arg2) - MZR.CMZM.PTM.MFY.PureMagicFormula.Sv(arg2);
            return -MZR.CMZM.PTM.PureFunction(arg) * Fy + MZR.CombinedFunction(arg);
        }
        public double CombinedFunction(MagicFormulaArguments arg)
        {
            var arg2 = new MagicFormulaArguments(arg.SA, arg.SR, arg.FZ, 0, arg.P, arg.T);

            double Fy = MZR.CMZM.PTM.MFY.PureFunction(arg2) - MZR.CMZM.PTM.MFY.PureMagicFormula.Sv(arg2);
            double Fx = MZR.CMZM.PTM.MFX.CombinedFunction(arg);
            Fy = Fy * MZR.CMZM.PTM.MFY.G(arg2);


            return -MZR.CMZM.PTM.CombinedFunction(arg) * Fy + MZR.CMZM.Function(arg) * Fx + MZR.CombinedFunction(arg);
        }

    }

    #endregion
}