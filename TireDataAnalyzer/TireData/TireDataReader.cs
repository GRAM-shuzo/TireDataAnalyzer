using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCDataUtils
{
    

    

    public class DatFileReader
    {
        public List<TireData> DataList
        {
            get;
            private set;
        }
        public DatFileReader(string filename, int skip = 3)
        {
            DataList = new List<TireData>();
            using (var sr = new System.IO.StreamReader(filename))
            {
                uint count = 0;
                while (!sr.EndOfStream)
                {
                    //三行読み飛ばす
                    ++count;
                    var line = sr.ReadLine();
                    if (count <= skip) continue;
                    var values = line.Split('\t');

                    if (values.Count() < 21)
                        throw new Exception("データがおかしい");

                    var data = new TireData();
                    data.ET = double.Parse(values[0]);
                    data.V = double.Parse(values[1]);
                    data.N = double.Parse(values[2]);
                    data.SA = double.Parse(values[3]);
                    data.IA = double.Parse(values[4]);
                    data.RL = double.Parse(values[5]);
                    data.RE = double.Parse(values[6]);
                    data.P = double.Parse(values[7]);
                    data.FX = double.Parse(values[8]);
                    data.FY = double.Parse(values[9]);
                    data.FZ = -double.Parse(values[10]);
                    data.MX = double.Parse(values[11]);
                    data.MZ = double.Parse(values[12]);
                    data.NFX = double.Parse(values[13]);
                    data.NFY = double.Parse(values[14]);
                    data.RST = double.Parse(values[15]);
                    data.TSTI = double.Parse(values[16]);
                    data.TSTC = double.Parse(values[17]);
                    data.TSTO = double.Parse(values[18]);
                    data.AMBTMP = double.Parse(values[19]);
                    data.SR = double.Parse(values[20]);
                    if (values.Count() > 21)
                        data.SL = double.Parse(values[21]);
                    else
                    {
                        var speed = data.V / 3.6 / 100;
                        var rps = data.N / 60;
                        var tireSpeed = Math.PI * data.RE * 2 * rps;
                        data.SL = (speed - tireSpeed) / speed;
                    }
                    DataList.Add(data);
                }
            }
        }
    }
}
