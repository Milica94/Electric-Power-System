using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class ComandInvoker
    {
        private List<Komand> staro = new List<Komand>();

        private List<Komand> novo = new List<Komand>();
        public ComandInvoker()
        {

        }
        public void Undo()
        {
            if(staro.Count != 0)
            {
                Komand kom = staro[staro.Count - 1];
                kom.NeIzvrsi();
                staro.Remove(kom);
                novo.Add(kom);

            }

        }

        public void Redo()
        {
            if (novo.Count != 0)
            {
                Komand komand = novo[novo.Count - 1];
                novo.Remove(komand);
                komand.Izvrsi();
                staro.Add(komand);
            }
        }
        public void DodajIzvrsi(Komand kom)
        {
            staro.Add(kom);
            kom.Izvrsi();
            novo.Clear();
        }
       
    }
}
