using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Milica_Tiodorovic_e3582013_rva
{
    public class Observer : IObserver
    {
        private De_Serialize_ serializer = new De_Serialize_();
        private MainWindow mainWindow;
        private Canvas viewCanvas;
        private Substation trafo = null;
        private double x = -1;
        private double y = -1;
        private double a; //sadasnje
        private double b;
        private bool drag;

        private Point point;
        private bool t = true;
        private Canvas d = null;


        public Observer(Canvas viewCanvas, MainWindow mainWindow)
        {
            this.viewCanvas = viewCanvas;
            this.mainWindow = mainWindow;
        }




        public void NotifyObservers()
        {
            PrintLines();
            AddSubstations();
        }

        private void PrintLines()
        {
            foreach (ACLineSegment l in Singleton.Instance().AClines)
            {
                System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Green;
                line.StrokeThickness = 1;
                line.Stroke = brush;

                ConnectivityNode od = null;//cvor pocetni
                ConnectivityNode na = null;//dest

                foreach (Terminal terminal in l.terminali)
                {
                    if (od == null)
                    {
                        foreach (CIM.IEC61970.Base.Core.Substation s in Singleton.Instance().Substations)
                        {
                            foreach (ConnectivityNode cvor in s.connectivityNodes)
                            {
                                if (cvor.mRID.Equals(terminal.ConnectivityNode.mRID))
                                {

                                    od = cvor;
                                    od.X = cvor.x + s.x + (s.connectivityNodes.IndexOf(cvor) * 50) + 10;
                                    od.Y = cvor.y + s.y + (s.connectivityNodes.IndexOf(cvor) * 50) + 10;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CIM.IEC61970.Base.Core.Substation s in Singleton.Instance().Substations)
                        {
                            foreach (ConnectivityNode cvor1 in s.connectivityNodes)
                            {
                                if (cvor1.mRID.Equals(terminal.ConnectivityNode.mRID))
                                {
                                    na = cvor1;
                                    na.X = cvor1.x + s.x + (s.connectivityNodes.IndexOf(cvor1) * 50) + 10;
                                    na.Y = cvor1.y + s.y + (s.connectivityNodes.IndexOf(cvor1) * 50) + 10;

                                    break;
                                }
                            }
                        }
                    }
                }


                BindingOperations.SetBinding(line, System.Windows.Shapes.Line.X1Property, new Binding { Source = od, Path = new PropertyPath("X") });
                BindingOperations.SetBinding(line, System.Windows.Shapes.Line.Y1Property, new Binding { Source = od, Path = new PropertyPath("Y") });
                BindingOperations.SetBinding(line, System.Windows.Shapes.Line.X2Property, new Binding { Source = na, Path = new PropertyPath("X") });
                BindingOperations.SetBinding(line, System.Windows.Shapes.Line.Y2Property, new Binding { Source = na, Path = new PropertyPath("Y") });

                viewCanvas.Children.Add(line);

            }
        }

        private void AddSubstations()
        {
            viewCanvas.Children.Clear();


            viewCanvas.AddHandler(Canvas.DragOverEvent, new DragEventHandler(DragOverSubCanvas));
            viewCanvas.AddHandler(Canvas.DropEvent, new DragEventHandler(DropSubCanvas));

            foreach (Substation s in Singleton.Instance().Substations)
            {
                Canvas c = new Canvas();
                c.Name = s.mRID;
                c.Width = 180;
                c.Height = 180;
                c.AllowDrop = true;

                c.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("White"));
                c.AddHandler(Canvas.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDownSubCanvas));

                TextBox text = new TextBox();
                text.Text = "Trafostanica ID: " + s.mRID;
                text.IsEnabled = false;
                text.Height = 20;
                text.Width = 180;
                text.Background = new SolidColorBrush(Colors.Black);
                text.Foreground = new SolidColorBrush(Colors.White);
                Canvas.SetLeft(text, 0);
                Canvas.SetBottom(text, 0);
                c.Children.Add(text);

                if (s.x != -1 && s.y != -1)
                {
                    Canvas.SetLeft(c, s.x);
                    Canvas.SetTop(c, s.y);
                    DodajNode(s, c);
                    viewCanvas.Children.Add(c);
                }
                else
                {

                    Canvas.SetLeft(c, 5);
                    Canvas.SetTop(c, 5);
                    s.x = 5;
                    s.y = 5;
                    DodajNode(s, c);
                    viewCanvas.Children.Add(c);

                }
            }
        }

        private void DodajNode(Substation s, Canvas c)
        {
            for (int i = 0; i < s.connectivityNodes.Count; i++)
            {
                Canvas n = new Canvas();
                n.Name = s.connectivityNodes[i].mRID;

                n.Width = 50;
                n.Height = 50;
                n.Background = new SolidColorBrush(Colors.LightGreen);

                TextBox text2 = new TextBox();
                text2.Text = "Node\n" + s.connectivityNodes[i].mRID + "\nVoltage\n" + s.connectivityNodes[i].m_BaseVoltage.nominalVoltage;
                text2.IsEnabled = true;
                text2.Height = 50;
                text2.Width = 50;
                text2.Background = new SolidColorBrush(Colors.Black);
                text2.Foreground = new SolidColorBrush(Colors.White);
                text2.FontSize = 7;
                Canvas.SetLeft(text2, 0);
                Canvas.SetBottom(text2, 0);
                n.Children.Add(text2);

                if (s.connectivityNodes[i].x != -1 && s.connectivityNodes[i].y != -1)
                {
                    Canvas.SetLeft(n, s.connectivityNodes[i].x + (i * 50));
                    Canvas.SetTop(n, s.connectivityNodes[i].y + (i * 50));
                    c.Children.Add(n);

                }
                else
                {
                    bool taken = false;



                    if (!taken)
                    {
                        Canvas.SetLeft(n, 5);
                        Canvas.SetTop(n, 5);
                        s.connectivityNodes[i].x = 5;
                        s.connectivityNodes[i].y = 5;
                        c.AllowDrop = true;
                        c.Children.Add(n);



                    }
                }
            }
        }


        private void MouseLeftButtonDownSubCanvas(object sender, MouseButtonEventArgs e)
        {
            d = new Canvas();

            trafo = new Substation();
            drag = true;
            d = (Canvas)sender;

            foreach (Substation s in Singleton.Instance().Substations)
            {
                if (s.mRID.Equals(d.Name))
                {
                    trafo = s;
                    x = s.x;
                    y = s.y;
                    break;
                }
            }

            DragDrop.DoDragDrop(mainWindow, d, DragDropEffects.All);

            drag = false;

        }

        private void DropSubCanvas(object sender, DragEventArgs e)
        {

            DropSub ds = new DropSub(trafo, x, y);
            Singleton.Instance().inv.DodajIzvrsi(ds);

            if (d != null)
            {
                if (((Canvas)sender).Resources["taken"] == null)
                {
                    ((Canvas)sender).Resources.Add("taken", true);
                    drag = false;
                }
            }

            t = true;
            e.Handled = true;


            trafo = null;

            d = null;

        }


        private void DragOverSubCanvas(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            Point p = e.GetPosition(viewCanvas);
            if (trafo != null)
            {
                if (t)
                {
                    t = false;
                    point = new Point();
                    point = e.GetPosition(d);
                }

                Canvas.SetLeft(d, p.X - point.X);
                Canvas.SetTop(d, p.Y - point.Y);
                trafo.x = p.X - point.X;
                trafo.y = p.Y - point.Y;

                if (p.Y - point.Y >= 220)
                {
                    Canvas.SetTop(d, 220);
                    trafo.y = 220;
                }

                if (p.Y - point.Y <= 0)
                {
                    Canvas.SetTop(d, 0);
                    trafo.y = 0;
                }

                if (p.X - point.X >= 415)
                {
                    Canvas.SetLeft(d, 415);
                    trafo.x = 415;
                }

                if (p.X - point.X <= 0)
                {
                    Canvas.SetLeft(d, 0);
                    trafo.x = 0;
                }

                foreach (ConnectivityNode cn in trafo.connectivityNodes)
                {
                    cn.X = trafo.x + cn.x + 30;
                    cn.Y = trafo.y + cn.y + 30;
                }

                e.Handled = true;
            }
        }
    }


}