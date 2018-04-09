using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddCvor.xaml
    /// </summary>
    public partial class AddCvor : Window
    {
        private De_Serialize_ serializer = new De_Serialize_();
        public AddCvor()
        {
            InitializeComponent();


            if (File.Exists("../../doc/trafostanice.xml"))
            {
                foreach (Substation s in Singleton.Instance().Substations)
                {
                    comboBox.Items.Add(s);
                }
            }


        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            bool isOk = true;

            if (textBox.Text.Trim().Equals(""))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(1);
                isOk = false;//name
            }

            if (textBox_Copy1.Text.Trim().Equals(""))
            {
                textBox_Copy1.BorderBrush = Brushes.Red;
                textBox_Copy1.BorderThickness = new Thickness(1);
                isOk = false;//alias
            }

            if (textBox_Copy2.Text.Trim().Equals(""))
            {
                textBox_Copy2.BorderBrush = Brushes.Red;
                textBox_Copy2.BorderThickness = new Thickness(1);
                isOk = false;//desc
            }

            if (comboBox.SelectedItem == null)
            {
                comboBox.BorderBrush = Brushes.Red;
                comboBox.BorderThickness = new Thickness(1);
                isOk = false; //substation
            }

            if (textBox1.Text.Trim().Equals(""))
            {
                textBox1.BorderBrush = Brushes.Red;
                textBox1.BorderThickness = new Thickness(1);
                isOk = false;//voltage
            }
            else
            {
                try
                {
                    float.Parse(textBox1.Text.Trim());

                    if (float.Parse(textBox1.Text.Trim()) < 10)
                    {
                        textBox1.BorderBrush = Brushes.Red;
                        textBox1.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    textBox1.BorderBrush = Brushes.Red;
                    textBox1.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

           

            if (isOk)
            {



                Substation s = comboBox.SelectedItem as Substation;


                DodajCvor asc = new DodajCvor(textBox.Text.Trim(), textBox_Copy1.Text.Trim(), textBox_Copy2.Text.Trim(), s, float.Parse(textBox1.Text.Trim()));
          
                Singleton.Instance().inv.DodajIzvrsi(asc);
                this.Close();
            }
        }
    }
}
