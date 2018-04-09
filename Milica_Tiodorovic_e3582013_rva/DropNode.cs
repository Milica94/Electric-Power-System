using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class DropNode : Komand
    {
        
            private ConnectivityNode cn = new ConnectivityNode();
            private double x2 = 0;//nove vrednosti
            private double y2 = 0;
            private double x = 0;
            private double y = 0;

            public DropNode(ConnectivityNode draggedNode, double x, double y)
            {
                cn = draggedNode;
                this.x = x;
                this.y = y;
                this.x2 = cn.x;
                this.y2 = cn.y;
            }

            public override void Izvrsi()
            {
                cn.x = x2;
                cn.y = y2;
            }

            public override void NeIzvrsi()
            {
                cn.x = x;
                cn.y = y;

                foreach (Substation s in Singleton.Instance().Substations)
                {
                    foreach (ConnectivityNode conNode in s.connectivityNodes)
                    {
                        if (conNode.mRID.Equals(cn.mRID))
                        {
                            for (int i = 0; i < s.connectivityNodes.Count; i++)
                            {
                                if (!cn.mRID.Equals(s.connectivityNodes[i].mRID))
                                {
                                    if (Kanvas.Check(cn.x, cn.y, s.connectivityNodes[i].x, s.connectivityNodes[i].y, 55))
                                    {
                                        s.connectivityNodes[i].x = -1;
                                        s.connectivityNodes[i].y = -1;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }

            }
        }
    }

