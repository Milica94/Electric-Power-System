using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class BrisiLiniju : Komand
    {

        private ACLineSegment line;

        public BrisiLiniju(String id)
        {
            line = new ACLineSegment();
            line.mRID = id;
        }
        public override void Izvrsi()
        {
            for (int i = 0; i < Singleton.Instance().AClines.Count; i++)
            {
                if (Singleton.Instance().AClines[i].mRID.Equals(line.mRID))
                {
                    Singleton.Instance().AClines.Remove(Singleton.Instance().AClines[i]);
                    break;
                }
            }
        }

        public override void NeIzvrsi()
        {
            Singleton.Instance().AClines.Add(line);
        }
    }
}
