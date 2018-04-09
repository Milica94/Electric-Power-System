using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Milica_Tiodorovic_e3582013_rva
{
    /// <summary>
    /// Interaction logic for AddACLine.xaml
    /// </summary>
    public partial class AddACLine : Window
    {

        private De_Serialize_ serializer = new De_Serialize_();
        public AddACLine()
        {
            InitializeComponent();

          



            Singleton.Instance().AClines = serializer.DeSerializeObject<BindingList<ACLineSegment>>("../../doc/konekcije.xml");
            if (Singleton.Instance().AClines == null)
            {
                Singleton.Instance().AClines = new BindingList<ACLineSegment>();
            }

            dataGrid.ItemsSource = Singleton.Instance().AClines;




        }
        private void KlonirajLiniju(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                ACLineSegment ac = dataGrid.SelectedItem as ACLineSegment;
                CloneLine cl = new CloneLine(ac.mRID);
                Singleton.Instance().inv.DodajIzvrsi(cl);
                Singleton.Instance().NotifyObservers();
            }
        }
        

        private void DodajLiniju(object sender, RoutedEventArgs e)
        {
            AddLine addl = new AddLine();
            this.Show();
            addl.ShowDialog();
            serializer.SerializeObject<BindingList<ACLineSegment>>(Singleton.Instance().AClines, "../../doc/konekcije.xml");
            Singleton.Instance().NotifyObservers();
        }

        private void Brisi(object sender, RoutedEventArgs e)
        {

            ACLineSegment acs = dataGrid.SelectedItem as ACLineSegment;
            BrisiLiniju bl = new BrisiLiniju(acs.mRID);
            Singleton.Instance().inv.DodajIzvrsi(bl);
            serializer.SerializeObject<BindingList<ACLineSegment>>(Singleton.Instance().AClines, "../../doc/konekcije.xml");
            Singleton.Instance().NotifyObservers();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().inv.Undo();
            Singleton.Instance().NotifyObservers();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().inv.Redo();
            Singleton.Instance().NotifyObservers();
        }

      
    }
}
