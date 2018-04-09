using System;
using System.Collections.Generic;
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
    /// Interaction logic for Registracija.xaml
    /// </summary>
    public partial class Registracija : Window
    {
        private De_Serialize_ ser = new De_Serialize_();
        private List<User> history = new List<User>();
        public Registracija()
        {
            InitializeComponent();
        }


        private void SignUp(object sender, RoutedEventArgs e)
        {
            bool ok = true;

            if (tB1.Text.Trim().Equals(""))
            {
                tB1.BorderBrush = Brushes.Red;
                tB1.BorderThickness = new Thickness(1);
                MessageBox.Show("Unesite username!");
                ok = false;
            }
            if (tB1.Text.Trim().Equals(""))
            {
                tB2.BorderBrush = Brushes.Red;
                tB2.BorderThickness = new Thickness(1);
                MessageBox.Show("Unesite password!");
                ok = false;
            }

            bool postoji = false;
            if (ok)
            {
                history = ser.DeSerializeObject<List<User>>("../../doc/korisnici.xml");
                if (history != null)
                {
                    for (int i = 0; i < history.Count; i++)
                    {
                        if (history[i].Usnm.Equals(tB1.Text))
                        {
                            postoji = true;
                        }
                    }
                }
                else
                {
                    history = new List<User>();
                }

            }

            if (ok && !postoji)
            {

                User user = new User();
                user.Usnm = tB1.Text;
                user.Pass = tB2.Password;

                history.Add(user);
                ser.SerializeObject<List<User>>(history, "../../doc/korisnici.xml");



                this.Close();

            }

            if (postoji)
            {
                MessageBox.Show("Username: '"+ tB1.Text + "' , vec postoji!");
            }
        }
        private void LogIn(object sender, RoutedEventArgs e)
        {
            bool ok = true;

            if (tB3.Text.Trim().Equals(""))
            {
                tB3.BorderBrush = Brushes.Red;
                tB3.BorderThickness = new Thickness(1);
                ok = false;
            }

            if (tB4.Password.Trim().Equals(""))
            {
                tB4.BorderBrush = Brushes.Red;
                tB4.BorderThickness = new Thickness(1);
                ok = false;
            }
            bool postoji = false;
            if (ok)
            {
                history = ser.DeSerializeObject<List<User>>("../../doc/korisnici.xml");
                if (history != null)
                {
                    for (int i = 0; i < history.Count; i++)
                    {
                        if (history[i].Usnm.Equals(tB3.Text) && history[i].Pass.Equals(tB4.Password))
                        {
                            postoji = true;
                        }

                    }
                    if(!postoji)
                    {
                        ok = false;
                        MessageBox.Show("Korisnik ne postoji!");
                            
                    }
                }

                if (ok && postoji)
                {

                    this.Close();

                }

            }

        }
        


        private void Close(object sender, RoutedEventArgs e)
        {

            App.Current.Shutdown();

        }
    }
}

