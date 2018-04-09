using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class DodajCvor : Komand
    {
        private ConnectivityNode cvor = new ConnectivityNode();

        private Substation sub = new Substation();

        public DodajCvor(String name, String alias, String desc, Substation s, float nominal)
        {
            Random r = new Random();
            cvor.aliasName = alias;
            cvor.description = desc;
            cvor.mRID ='a' +  r.Next(324).ToString();
            cvor.name = name;

            //baseVoltage
            cvor.m_BaseVoltage = new BaseVoltage();
            cvor.m_BaseVoltage.nominalVoltage = nominal;
            cvor.m_BaseVoltage.aliasName = cvor.aliasName;
            cvor.m_BaseVoltage.description =  cvor.description;
            cvor.m_BaseVoltage.ConductingEquipment = new List<ConductingEquipment>();
            cvor.m_BaseVoltage.mRID = r.Next(1000).ToString();
            cvor.m_BaseVoltage.name =cvor.name;
            cvor.x = -1;
            cvor.y = -1;

        }
           
        public override void Izvrsi()
        {
            Singleton.Instance().Nodes.Add(cvor);
            sub.connectivityNodes.Add(cvor);

        }

        public override void NeIzvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Nodes.Count; i++)
            {
                if (Singleton.Instance().Nodes[i].mRID.Equals(cvor.mRID))
                {
                    sub.connectivityNodes.Remove(Singleton.Instance().Nodes[i]);
                    Singleton.Instance().Nodes.Remove(Singleton.Instance().Nodes[i]);
                    break;
                }
            }
        }
    }
}