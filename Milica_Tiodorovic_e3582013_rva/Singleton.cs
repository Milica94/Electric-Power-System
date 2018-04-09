using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class Singleton
    {
        private De_Serialize_ serializer = new De_Serialize_();
     
        private BindingList<Substation> substations;
        private BindingList<ConnectivityNode> nodes;
        private BindingList<VoltageLevel> voltage;
    
        public ComandInvoker inv;
        private string name;
        private List<IObserver> observ;

        private static Singleton instance;

        private BindingList<ACLineSegment> aclines;

        private Singleton()
        {
            voltage = new BindingList<VoltageLevel>();
            inv = new ComandInvoker();
            Nodes = new BindingList<ConnectivityNode>();
            Substations = new BindingList<Substation>();
            Name = string.Empty;
            observ = new List<IObserver>();
            AClines = new BindingList<ACLineSegment>();
      
        }

       public void Register(IObserver observer)
        {
            observ.Add(observer);
        }

        public void UnRegister(IObserver observer)
        {
            observ.Remove(observer);
        }
        
     public void NotifyObservers()
        {
            foreach (IObserver observer in observ)
            {
                observer.NotifyObservers();
            }
        }



        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public ComandInvoker Inv
        {
            get
            {
                return inv;
            }
            set
            {
                inv = value;
            }
        }

        public BindingList<Substation> Substations
        {
            get
            {
                return substations;
            }
            set
            {
                substations = value;
            }
        }

        public BindingList<ConnectivityNode> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
            }
        }

        public BindingList<ACLineSegment> AClines
        {
            get
            {
                return aclines;
            }
            set
            {
                aclines = value;
            }
        }


        public static Singleton Instance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }
    }
}

