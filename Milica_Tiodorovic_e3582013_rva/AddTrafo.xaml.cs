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
    /// Interaction logic for AddTrafo.xaml
    /// </summary>
    public partial class AddTrafo : Window
    {
        private De_Serialize_ serializer = new De_Serialize_();
        public AddTrafo()
        {
            InitializeComponent();
        }

       

       
        private void Create(object sender, RoutedEventArgs e)
        {
            bool ok = true;

            if (textBox.Text.Trim().Equals(""))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(1);
                ok = false;//name
            }

            if (textBox_Copy1.Text.Trim().Equals(""))
            {
                textBox_Copy1.BorderBrush = Brushes.Red;
                textBox_Copy1.BorderThickness = new Thickness(1);
                ok = false;//alias
            }

            if (textBox_Copy2.Text.Trim().Equals(""))
            {
                textBox_Copy2.BorderBrush = Brushes.Red;
                textBox_Copy2.BorderThickness = new Thickness(1);
                ok = false;//desc
            }

           if (text_box5.Text.Trim().Equals(""))
            {
                text_box5.BorderBrush = Brushes.Red;
                text_box5.BorderThickness = new Thickness(1);
                ok = false;//voltage
            }
            else
            {
                try
                {
                    float.Parse(text_box5.Text.Trim());

                    if (float.Parse(text_box5.Text.Trim()) < 1)
                    {
                        text_box5.BorderBrush = Brushes.Red;
                        text_box5.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    text_box5.BorderBrush = Brushes.Red;
                    text_box5.BorderThickness = new Thickness(1);
                    ok = false;
                }
                
           }

            if (ok)
            {
                DodajTrafo asc = new DodajTrafo(textBox.Text.Trim(), textBox_Copy1.Text.Trim(), textBox_Copy2.Text.Trim(), float.Parse(text_box5.Text.Trim()));
                Singleton.Instance().inv.DodajIzvrsi(asc);
                this.Close();
            }

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
    }
}
