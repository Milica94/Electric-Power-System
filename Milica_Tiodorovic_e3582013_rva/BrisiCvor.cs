using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class BrisiCvor : Komand
    {
        private ConnectivityNode node;
        private List<ACLineSegment> lines = new List<ACLineSegment>();
        int index;

        public BrisiCvor(String id)
        {
            node = new ConnectivityNode();
            node.mRID = id;
        }
        public override void Izvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(node.mRID))
                {
                    node = Singleton.Instance().Nodes[i];

                    for (int j = 0; j < Singleton.Instance().Substations.Count; j++)
                    {
                        for (int k = 0; k < Singleton.Instance().Substations[j].connectivityNodes.Count; k++)
                        {
                            if (Singleton.Instance().Substations[j].connectivityNodes[k].mRID.Equals(Singleton.Instance().Nodes[i].mRID))
                            {
                                foreach (ACLineSegment line in Singleton.Instance().AClines)
                                {
                                    foreach (Terminal t in line.terminali)
                                    {
                                        if (t.ConnectivityNode.mRID.Equals(node.mRID))
                                        {
                                            lines.Add(line);
                                            Singleton.Instance().AClines.Remove(line);
                                            break;
                                        }
                                    }

                                }
                                Singleton.Instance().Substations[j].connectivityNodes.RemoveAt(k);
                                index = j;
                                break;
                            }
                        }
                    }
                                Singleton.Instance().Nodes.Remove(Singleton.Instance().Nodes[i]);
                                 break;
                }
            }
        }

        public override void NeIzvrsi()
        {
            Singleton.Instance().Nodes.Add(node);
            Singleton.Instance().Substations[index].connectivityNodes.Add(node);

            foreach (ACLineSegment line in lines)
            {
                Singleton.Instance().AClines.Add(line);
            }
        }
    }
}
