using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
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
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        private De_Serialize_ serializer = new De_Serialize_();
        public AddLine()
        {
            InitializeComponent();
            if (File.Exists("../../doc/cvorovi.xml"))
            {
                foreach (ConnectivityNode cvor in Singleton.Instance().Nodes)
                {
                    comboBox.Items.Add(cvor);
                    comboBox_Copy.Items.Add(cvor);
                }
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            bool ok = true;



            if (textBox1.Text.Trim().Equals(""))
            {
                textBox1.BorderBrush = Brushes.Red;
                textBox1.BorderThickness = new Thickness(1);
                ok = false;//length
            }
            else
            {

                try
                {
                    float.Parse(textBox1.Text.Trim());

                    if (float.Parse(textBox1.Text.Trim()) < 5)
                    {
                        textBox1.BorderBrush = Brushes.Red;
                        textBox1.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    textBox1.BorderBrush = Brushes.Red;
                    textBox1.BorderThickness = new Thickness(1);
                    ok = false;
                }
            }

            if (textBox_Copy.Text.Trim().Equals(""))//gch
            {
                textBox_Copy.BorderBrush = Brushes.Red;
                textBox_Copy.BorderThickness = new Thickness(1);
                ok = false;//length
            }
            else
            {

                try
                {
                    float.Parse(textBox_Copy.Text.Trim());

                    if (float.Parse(textBox_Copy.Text.Trim()) < 1)
                    {
                        textBox_Copy.BorderBrush = Brushes.Red;
                        textBox_Copy.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    textBox_Copy.BorderBrush = Brushes.Red;
                    textBox_Copy.BorderThickness = new Thickness(1);
                    ok = false;
                }
            }

            if (textBox_Copy1.Text.Trim().Equals(""))//bch
            {
                textBox_Copy1.BorderBrush = Brushes.Red;
                textBox_Copy1.BorderThickness = new Thickness(1);
                ok = false;//length
            }
            else
            {

                try
                {
                    float.Parse(textBox_Copy1.Text.Trim());

                    if (float.Parse(textBox_Copy1.Text.Trim()) < 1)
                    {
                        textBox_Copy1.BorderBrush = Brushes.Red;
                        textBox_Copy1.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    textBox_Copy1.BorderBrush = Brushes.Red;
                    textBox_Copy1.BorderThickness = new Thickness(1);
                    ok = false;
                }
            }

            if (textBox_Copy2.Text.Trim().Equals(""))//r
            {
                textBox_Copy2.BorderBrush = Brushes.Red;
                textBox_Copy2.BorderThickness = new Thickness(1);
                ok = false;//length
            }
            else
            {

                try
                {
                    float.Parse(textBox_Copy2.Text.Trim());

                    if (float.Parse(textBox_Copy2.Text.Trim()) < 1)
                    {
                        textBox_Copy2.BorderBrush = Brushes.Red;
                        textBox_Copy2.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    textBox_Copy2.BorderBrush = Brushes.Red;
                    textBox_Copy2.BorderThickness = new Thickness(1);
                    ok = false;
                }
            }

            if (textBox_Copy3.Text.Trim().Equals(""))//x
            {
                textBox_Copy3.BorderBrush = Brushes.Red;
                textBox_Copy3.BorderThickness = new Thickness(1);
                ok = false;//length
            }
            else
            {

                try
                {
                    float.Parse(textBox_Copy3.Text.Trim());

                    if (float.Parse(textBox_Copy3.Text.Trim()) < 1)
                    {
                        textBox_Copy3.BorderBrush = Brushes.Red;
                        textBox_Copy3.BorderThickness = new Thickness(1);
                        ok = false;
                    }
                }
                catch
                {
                    textBox_Copy3.BorderBrush = Brushes.Red;
                    textBox_Copy3.BorderThickness = new Thickness(1);
                    ok = false;
                }
            }


            if (textBox.Text.Trim().Equals(""))
            {
                textBox.BorderBrush = Brushes.Red;
                textBox.BorderThickness = new Thickness(1);
                ok = false;//name
            }

            if (comboBox.SelectedItem == null)
            {
               
                ok = false; //node1
            }

            if (comboBox_Copy.SelectedItem == null)
            {
               
                ok = false; //node2
            }
            ConnectivityNode n1 = comboBox.SelectedItem as ConnectivityNode;
            ConnectivityNode n2 = comboBox_Copy.SelectedItem as ConnectivityNode;
            bool sameSubstation = false;
            bool sameSubstation2 = false;

            foreach (Substation s in Singleton.Instance().Substations)
            {
                foreach (ConnectivityNode conNode in s.connectivityNodes)
                {
                    if (conNode.mRID.Equals(n1.mRID))
                    {
                        sameSubstation = true;
                    }
                    else if (conNode.mRID.Equals(n2.mRID))
                    {
                        sameSubstation2 = true;
                    }
                }

                if (sameSubstation && sameSubstation2)
                {
                    break;
                }
                sameSubstation = false;
                sameSubstation2 = false;
            }

            if (n1.mRID.Equals(n2.mRID))
            {
                MessageBox.Show("Cvorovi moraju biti razliciti.");
                ok = false;
            }
            else if (n1.m_BaseVoltage.nominalVoltage != n2.m_BaseVoltage.nominalVoltage)
            {
                MessageBox.Show("Cvorovi moraju imati isti nominaln napon.");
                ok= false;
            }
            else if (sameSubstation && sameSubstation2)
            {
                MessageBox.Show("Cvorovi ne smeju pripadati istoj trafostanici.");
                ok= false;
            }
           
            
            if (ok)
            {


                DodajACLine dl = new DodajACLine(n1,n2,textBox.Text.Trim(), float.Parse(textBox1.Text.Trim()),float.Parse(textBox_Copy.Text.Trim()), float.Parse(textBox_Copy1.Text.Trim()), float.Parse(textBox_Copy2.Text.Trim()), float.Parse(textBox_Copy3.Text.Trim()));
                Singleton.Instance().inv.DodajIzvrsi(dl);
                this.Close();

            }
            
        }
    }
}
