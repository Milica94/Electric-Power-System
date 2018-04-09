using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private De_Serialize_ serializer = new De_Serialize_();
        public MainWindow()
        {
            InitializeComponent();


            Visibilityy();

            Observer observer = new Observer(ViewCanvas, this);
            Singleton.Instance().Register(observer);

            DataContext = Singleton.Instance();

            Singleton.Instance().Register(observer);


         

            Registracija reg = new Registracija();
            this.Show();
            reg.ShowDialog();

         

        }

      
        private void Table(object sender, RoutedEventArgs e)
        {
            Tabele t = new Tabele();
            this.Show();
            t.ShowDialog();
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

        private void Line(object sender, RoutedEventArgs e)
        {
            AddACLine addl = new AddACLine();
            this.Show();
            addl.ShowDialog();
            Singleton.Instance().NotifyObservers();
        }
        private void Visibilityy()
        {
           
            CanvasBorder6.Visibility = Visibility.Hidden;
        }
        private void OpenCanvas(object sender, RoutedEventArgs e)
        {
            Visibilityy();
            CanvasBorder6.Visibility = Visibility.Visible;
            Singleton.Instance().NotifyObservers();
        }

        private void Save(object sender, EventArgs e)
        {
            serializer.SerializeObject<BindingList<CIM.IEC61970.Base.Core.Substation>>(Singleton.Instance().Substations, "../../doc/trafostanice.xml");
            serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../doc/cvorovi.xml");
            serializer.SerializeObject<BindingList<ACLineSegment>>(Singleton.Instance().AClines, "../../doc/konekcije.xml");
        }
    }
}
