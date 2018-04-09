using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class CloneTrafo : Komand
    {
        private string ID = string.Empty;
        private CIM.IEC61970.Base.Core.Substation substation;

        public CloneTrafo(String id)
        {



            foreach (Substation subs in Singleton.Instance().Substations)
            {
                if (subs.mRID.Equals(id))
                {
                    substation = subs;
                    break;
                }
            }



            ID = "a" + Guid.NewGuid().ToString().Substring(0, 8);

        }

        public override void Izvrsi()
        {

            substation = (CIM.IEC61970.Base.Core.Substation)substation.Clone();
            substation.mRID = ID;
            Singleton.Instance().Substations.Add(substation);
        }


        public override void NeIzvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
            {

                if (Singleton.Instance().Substations[i].mRID.Equals(substation.mRID))
                {
                    Singleton.Instance().Substations.Remove(Singleton.Instance().Substations[i]);
                    break;
                }

            }
        }
    }
}
    

