using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class BrisiTrafo : Komand
    {
        private Substation s;
        private List<ACLineSegment> lines = new List<ACLineSegment>();
        private List<int> terminalsID = new List<int>();
        public BrisiTrafo(String id)
        {
            s = new Substation();
            s.mRID = id;
        }
        public override void Izvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
            {
                if (Singleton.Instance().Substations[i].mRID.Equals(s.mRID))
                {
                    s = Singleton.Instance().Substations[i];

                    foreach (ConnectivityNode cn2 in s.connectivityNodes)
                    {
                        foreach (ConnectivityNode cn in Singleton.Instance().Nodes)
                        {
                            if (cn.mRID.Equals(cn2.mRID))
                            {
                                foreach (ACLineSegment line in Singleton.Instance().AClines)
                                {
                                    foreach (Terminal t in line.terminali)
                                    {
                                        if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                        {
                                            lines.Add(line);
                                            Singleton.Instance().AClines.Remove(line);
                                            break;
                                        }
                                    }
                                }
                             
                            }
                        }
                    }
                    Singleton.Instance().Substations.Remove(Singleton.Instance().Substations[i]);
                    break;
                }
            }
        }
 
        public override void NeIzvrsi()
        {
            Singleton.Instance().Substations.Add(s);

            foreach (ACLineSegment line in lines)
            {
                Singleton.Instance().AClines.Add(line);
            }

            lines.Clear();

            foreach (ConnectivityNode cn in s.connectivityNodes)
            {
                Singleton.Instance().Nodes.Add(cn);
            }
        }
    }
}
