using CIM.IEC61970.Base.Core;
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
    /// Interaction logic for Tabele.xaml
    /// </summary>
    public partial class Tabele : Window
    {
        private De_Serialize_ serializer = new De_Serialize_();
        public Tabele()
        {
            InitializeComponent();
            DataContext = Singleton.Instance();


            Singleton.Instance().Nodes = serializer.DeSerializeObject<BindingList<ConnectivityNode>>("../../doc/cvorovi.xml");
           if (Singleton.Instance().Nodes == null)
            {
                Singleton.Instance().Nodes = new BindingList<ConnectivityNode>();
            }

            dataGrid_Copy.ItemsSource = Singleton.Instance().Nodes;

            Singleton.Instance().Substations = serializer.DeSerializeObject<BindingList<Substation>>("../../doc/trafostanice.xml");
            if (Singleton.Instance().Substations == null)
            {
                Singleton.Instance().Substations = new BindingList<Substation>();
            }
            dataGrid.ItemsSource = Singleton.Instance().Substations;



        }

        private void DodajTrafostanicu(object sender, RoutedEventArgs e)
        {
            AddTrafo tr = new AddTrafo();
            this.Show();
            tr.ShowDialog();
            serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../doc/trafostanice.xml");
            Singleton.Instance().NotifyObservers();

        }

        private void DodajCvor(object sender, RoutedEventArgs e)
        {
            AddCvor cv = new AddCvor();
            this.Show();
            cv.ShowDialog();
            serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../doc/cvorovi.xml");
            Singleton.Instance().NotifyObservers();
        }
        private void BrisiTrafostanicu(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Substation s = dataGrid.SelectedItem as Substation;
                BrisiTrafo bt = new BrisiTrafo(s.mRID);
                Singleton.Instance().inv.DodajIzvrsi(bt);
                serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../doc/trafostanice.xml");
              Singleton.Instance().NotifyObservers();
            }
        }

        private void ObrisiCvor(object sender, RoutedEventArgs e)
        {
            ConnectivityNode s = dataGrid_Copy.SelectedItem as ConnectivityNode;
            BrisiCvor bc = new BrisiCvor(s.mRID);
            Singleton.Instance().inv.DodajIzvrsi(bc);
            serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../doc/cvorovi.xml");
            Singleton.Instance().NotifyObservers();
        }
        private void Undo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().inv.Undo();
            Singleton.Instance().NotifyObservers();
        }
        private void KlonirajTrafo(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Substation s = dataGrid.SelectedItem as CIM.IEC61970.Base.Core.Substation;
                CloneTrafo ct = new CloneTrafo(s.mRID);
                Singleton.Instance().inv.DodajIzvrsi(ct);
                serializer.SerializeObject<BindingList<CIM.IEC61970.Base.Core.Substation>>(Singleton.Instance().Substations, "../../doc/trafostanice.xml");
                Singleton.Instance().NotifyObservers();
            }
        }
        private void Redo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().inv.Redo();
            Singleton.Instance().NotifyObservers();
        }

        private void KlonirajCvor(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Copy.SelectedItem != null)
            {
                ConnectivityNode s = dataGrid_Copy.SelectedItem as ConnectivityNode;
                CloneCvor cc = new CloneCvor(s.mRID);
                Singleton.Instance().inv.DodajIzvrsi(cc);
                serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../doc/cvorovi.xml");
                Singleton.Instance().NotifyObservers();
            }
        }
    }
}
