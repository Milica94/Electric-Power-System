using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class CloneLine : Komand
    {
        Terminal terminal1 = null;
        Terminal terminal2 = null;



        private ACLineSegment line = new ACLineSegment();
        private string ID = string.Empty;





        public CloneLine(string id)
        {
            foreach (ACLineSegment ac in Singleton.Instance().AClines)
            {
                if (ac.mRID.Equals(id))
                {
                    line = ac;
                    break;
                }
            }

            ID = Guid.NewGuid().ToString().Substring(0, 8);

            foreach (ACLineSegment l in Singleton.Instance().AClines)
            {
                if (id.Equals(l.mRID))
                {
                    foreach (Terminal t in l.terminali)
                    {
                        foreach (CIM.IEC61970.Base.Core.Substation s in Singleton.Instance().Substations)
                        {
                            foreach (ConnectivityNode cn in s.connectivityNodes)
                            {
                                if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                                {
                                    if (terminal1 == null)
                                    {
                                        terminal1 = new Terminal()
                                        {
                                            aliasName = "Terminal",
                                            ConductingEquipment = new ConductingEquipment(),
                                            connected = false,
                                            ConnectivityNode = cn,
                                            description = "something",
                                            mRID = Guid.NewGuid().ToString().Substring(0, 8),
                                            name = "Terminal",
                                            phases = PhaseCode.A,
                                            sequenceNumber = 1
                                        };
                                    }
                                    else
                                    {
                                        terminal2 = new Terminal()
                                        {
                                            aliasName = "Terminal",
                                            ConductingEquipment = new ConductingEquipment(),
                                            connected = false,
                                            ConnectivityNode = cn,
                                            description = "something",
                                            mRID = Guid.NewGuid().ToString().Substring(0, 8),
                                            name = "Terminal",
                                            phases = PhaseCode.A,
                                            sequenceNumber = 1
                                        };

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }


        public override void Izvrsi()
        {
            line = (ACLineSegment)line.Clone();
            line.mRID = ID;
            line.terminali.Add(terminal1);
            line.terminali.Add(terminal2);
            Singleton.Instance().AClines.Add(line);
        }

        public override void NeIzvrsi()
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
    }
}

