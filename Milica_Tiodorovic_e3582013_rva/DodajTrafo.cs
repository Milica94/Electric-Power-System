using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{//treba dodati connectivity nodes
    public class DodajTrafo : Komand
    { 
        private Substation s = new Substation();
        public DodajTrafo(String name, String alias, String desc,float voltage)
        {
            s.name = name;
            s.description = desc;
            Random r = new Random();
            s.mRID = 'a'+ r.Next(1000).ToString();
            s.aliasName = alias;
            s.connectivityNodes = new List<ConnectivityNode>();
           
            s.x = -1;
            s.y = -1;
            s.Bays = new List<Bay>();
            s.Equipments = new List<Equipment>();
            s.VoltageLevels = new List<VoltageLevel>();



            BaseVoltage bv = new BaseVoltage()
            {
                aliasName = "sub_-" + s.mRID + "_bv_" + s.aliasName,
                mRID = "sub_-" + s.mRID + "-_bv_-" + s.mRID,
                name = "sub_-" + s.mRID + "-_bv_-" + s.name,
                description = name = "sub_-" + s.mRID + "-_bv_-" + s.description,
                nominalVoltage = voltage,
                ConductingEquipment = new List<ConductingEquipment>()
            };

            VoltageLevel vl = new VoltageLevel()
            {
                aliasName = "sub_" + s.mRID + "_vl_" + s.aliasName,
                mRID = "sub_-" + s.mRID + "-_vl_-" + s.mRID,
                name = "sub_-" +s.mRID + "-_vl_-" + s.name,
                description = name = "sub_-" + s.mRID + "-_vl_-" + s.description,
                BaseVoltage = bv,
                Bays = new List<Bay>(),
                Equipments = new List<Equipment>()
            };

            s.VoltageLevels.Add(vl);

        }

        public override void Izvrsi()
        {
            Singleton.Instance().Substations.Add(s);
        }

        public override void NeIzvrsi()
        {
            for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
            {
                if (Singleton.Instance().Substations[i].mRID.Equals(s.mRID))
                {
                    Singleton.Instance().Substations.Remove(Singleton.Instance().Substations[i]);
                    break;
                }
            }
        }

        }
    }

