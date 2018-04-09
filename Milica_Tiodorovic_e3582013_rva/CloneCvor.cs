using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class CloneCvor : Komand
    {

         
        private ConnectivityNode cnode;
        private string ID = string.Empty;



        public CloneCvor(String id)
        {
            foreach (ConnectivityNode conNode in Singleton.Instance().Nodes)
            {
                if (conNode.mRID.Equals(id))
                {
                    cnode = conNode;
                    break;
                }
            }



            ID = "a" + Guid.NewGuid().ToString().Substring(0, 8);

        }

        public override void Izvrsi()
        {
            cnode = (ConnectivityNode)cnode.Clone();
            cnode.mRID = ID;
            Singleton.Instance().Nodes.Add(cnode);
        }



        public override void NeIzvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(cnode.mRID))
                {

                    for (int j = 0; j < Singleton.Instance().Substations.Count; j++)
                    {

                        for (int k = 0; k < Singleton.Instance().Substations[j].connectivityNodes.Count; k++)
                        {

                            if (Singleton.Instance().Substations[j].connectivityNodes[k].mRID.Equals(Singleton.Instance().Nodes[i].mRID))
                            {
                                Singleton.Instance().Substations[j].connectivityNodes.Remove(cnode);
                                break;
                            }
                        }
                    }


                    Singleton.Instance().Nodes.Remove(Singleton.Instance().Nodes[i]);

                    break;
                }
            }
        }
    }
}

   