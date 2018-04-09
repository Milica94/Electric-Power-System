using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{//dodati description i alias name
    public class DodajACLine : Komand
    {
        ACLineSegment als;
        
        Terminal term1 = new Terminal();
        Terminal term2 = new Terminal();
        public DodajACLine(ConnectivityNode node1, ConnectivityNode node2,String ime, float duzina, float gch, float bch, float r, float x)
        {
            als = new ACLineSegment();
            Random rand = new Random();
            als.name = ime;//linija ime
            als.length = duzina;
            als.mRID = rand.Next(1000).ToString();
            als.gch = gch;
            als.bch = bch;
            als.r = r;
            als.x = x;

            term1 = new Terminal()
            {
                name = "Terminal 1",
                ConnectivityNode = node1,
                ConductingEquipment = new ConductingEquipment(),
                phases = PhaseCode.AB,
                sequenceNumber = 1,
                connected = false,
                mRID = rand.Next(100).ToString(),
                aliasName = "A_Terminal 1",
                description = "opis"

            };

            term2 = new Terminal()
            {
                name = "Terminal 2",
                ConnectivityNode = node2,
                ConductingEquipment = new ConductingEquipment(),
                phases = PhaseCode.ABCN,
                sequenceNumber = 1,
                connected = false,
                mRID = rand.Next(100).ToString(),
                aliasName = "A_Terminal 2",
                description = "opis"

            };

            als.terminali.Add(term1);
            als.terminali.Add(term2);

        }


        public override void Izvrsi()
        {
            Singleton.Instance().AClines.Add(als);
        }

        public override void NeIzvrsi()
        {
            foreach (ACLineSegment acl in Singleton.Instance().AClines)
            {
                if (acl.mRID.Equals(acl.mRID))
                {
                    Singleton.Instance().AClines.Remove(acl);
                    break;
                }
            }
        }
    }
}
