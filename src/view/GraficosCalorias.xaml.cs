using Calorias.src.model;
using Calorias.src.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calorias.src.view
{
    /// <summary>
    /// Lógica de interacción para GraficosCalorias.xaml
    /// </summary>
    public partial class GraficosCalorias : Window
    {
        #region Declaraciones
        private ViewModel viewModel;
        private NoModalCalorias nmCal;
        //Botones Flotantes
        private Button botonCalorias, botonReset;
        private StackPanel stackPanelBotones;
        private Border botonesFlotantes;
        private Label yCalorias;
        //Flags
        private Boolean cambio;
        private Boolean ventanaCal;
        private int sino;
        //Variables
        private double coordx;
        private double coordy;
        #endregion

        #region Constructor
        public GraficosCalorias()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            viewModel.AnadirBarraEvent += ViewModel_AnadirBarraEvent;
            viewModel.CanvasMaxCaloriasEvent += ViewModel_CanvasMaxCaloriasEvent;
            //Variables para dibujar en canvas
            this.coordx = 0;
            this.coordy = 0;
            this.sino = 0;
            //Creamos Botones Flotantes
            CrearBotonesFlotante();
            noModal(viewModel);
            //Cerrar programa
            this.Closed += GraficosCalorias_Closed;
            this.cambio = false;
        }
      
        #endregion

        #region Eventos Canvas
        private void GraficosCalorias_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GraficosCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redibujar();
        }
        #endregion

        #region NoModal
        private void noModal(ViewModel viewModel)
        {
            nmCal = new NoModalCalorias(viewModel);
            //Ventana No Modal Evento
            nmCal.Closed += NmCal_Closed;
            nmCal.SeleccionCambioEvent += SeleccionCambioEvent;
            nmCal.ActualizarCanvasEvent += ActualizarCanvasEvent;
            nmCal.GuardarImagenEvent += GuardarImagenEvent;
            nmCal.Show();
            this.ventanaCal = true;
        }

        private void NmCal_Closed(object sender, EventArgs e)
        {
            this.ventanaCal = false;
        }
        private void SeleccionCambioEvent(object sender, EventArgs e)
        {
            this.cambio = true;
            redibujar();
        }

        private void ActualizarCanvasEvent(object sender, EventArgs e)
        {
            this.cambio = false;
            redibujar();
        }
        //http://tutorialgenius.blogspot.com/2014/12/saving-window-or-canvas-as-png-bitmap.html
        private void GuardarImagenEvent(object sender, EventArgs e)
        {
            Visual pantalla = GraficosCanvas;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Grafica"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                if (GraficosCanvas.Visibility == Visibility.Visible)
                {
                    Size size = new Size(GraficosCanvas.ActualWidth, GraficosCanvas.ActualHeight);
                    GraficosCanvas.Measure(size);
                }

                var rtb = new RenderTargetBitmap(
                    (int)GraficosCanvas.ActualWidth, //width
                    (int)GraficosCanvas.ActualHeight, //height
                    96, //dpi x
                    96, //dpi y
                    PixelFormats.Pbgra32 // pixelformat
                    );
                rtb.Render(pantalla);

                var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
                enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));

                using (var stm = System.IO.File.Create(filename))
                    enc.Save(stm);
            }
        }

        #endregion

        #region ViewModel Evento
        private void ViewModel_AnadirBarraEvent(object sender, EventArgs e)
        {
            redibujar();
        }
        private void ViewModel_CanvasMaxCaloriasEvent(object sender, EventArgs e)
        {
            redibujar();
        }
        #endregion

        #region BotonesFlotantes
        private void CrearBotonesFlotante()
        {
            this.botonCalorias = new Button()
            {
                Name = "BotonNoModal",
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/"
                                                     + Assembly.GetExecutingAssembly().GetName().Name
                                                     + ";component/iconos/calorias-quemadas.png", UriKind.RelativeOrAbsolute))
                },
                Width = 50,
                Height = 50,
                ToolTip = "Abrir Ventana DatosCalorias"
            };
            botonCalorias.Click += BotonCalorias_Click;
            this.botonReset = new Button()
            {
                Name = "BotonReset",
                Content = new Image()
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/"
                                                     + Assembly.GetExecutingAssembly().GetName().Name
                                                     + ";component/iconos/graficas_globales.PNG", UriKind.RelativeOrAbsolute))
                },
                Width = 50,
                Height = 50,
                Margin = new Thickness(2, 0, 0, 0),
                ToolTip = "Mostrar Grafica Global"
            };
            botonReset.Click += BotonReset_Click;
            this.stackPanelBotones = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Children = { botonCalorias, botonReset }
            };
            this.stackPanelBotones.Background = Brushes.Gray;
            this.botonesFlotantes = new Border()
            {
                BorderThickness = new Thickness(2),
                Visibility = Visibility.Hidden,
                Child = stackPanelBotones
            };
            this.botonesFlotantes.BorderBrush = Brushes.LightCyan;
            Canvas.SetLeft(botonesFlotantes, 5);
            Canvas.SetBottom(botonesFlotantes, 5);

            this.yCalorias = new Label()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DodgerBlue,
                Foreground = Brushes.DimGray,
                Background = Brushes.Azure,
                Visibility = Visibility.Hidden
            };
            Canvas.SetRight(yCalorias, 0);
            Canvas.SetTop(yCalorias, 0);
        }

        private void GraficosCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            botonesFlotantes.Visibility = Visibility.Visible;
            yCalorias.Visibility = Visibility.Visible;
        }

        private void GraficosCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            botonesFlotantes.Visibility = Visibility.Hidden;
            yCalorias.Visibility = Visibility.Hidden;
        }

        private void GraficosCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(GraficosCanvas);
            double realY = GraficarBarra.yPant_to_yReal(p.Y, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax);
            if (realY >= 0)
            {
                yCalorias.Content = "Cantidad Calorias: " + (int)realY;
            }
            else
            {
                yCalorias.Content = "Cantidad Calorias: ";
            }
        }

        private void BotonReset_Click(object sender, RoutedEventArgs e)
        {
            this.cambio = false;
            redibujar();
        }

        private void BotonCalorias_Click(object sender, RoutedEventArgs e)
        {
            if (ventanaCal == false)
            {
                noModal(viewModel);
                ventanaCal = true;
            }
        }
        #endregion

        #region Funciones dibujar
        public void redibujar()
        {
            if (cambio == false)
            {
                GraficosCanvas.Children.Clear();
                dibujarEjes();
                dibujarEtiquetas();
                dibujarPuntos();
                dibujarBarras();
                GraficosCanvas.Children.Add(botonesFlotantes);
                GraficosCanvas.Children.Add(yCalorias);
            }
            else
            {
                GraficosCanvas.Children.Clear();
                dibujarEjes();
                dibujarEtiquetas();
                dibujarPuntos();
                dibujarBarrasEspecifica();
                GraficosCanvas.Children.Add(botonesFlotantes);
                GraficosCanvas.Children.Add(yCalorias);
            }
        }

        private void dibujarEjes()
        {
            if(GraficosCanvas != null)
            {               
                Line ejeX = GraficarBarra.GetEjeX(GraficosCanvas.ActualWidth, GraficosCanvas.ActualHeight, viewModel._CanvasConfig.ParamReps);
                ejeX.Opacity = 0.8;
                GraficosCanvas.Children.Add(ejeX);                             
                Line ejeY = GraficarBarra.GetEjeY(GraficosCanvas.ActualWidth, GraficosCanvas.ActualHeight, viewModel._CanvasConfig.ParamReps);
                ejeY.Opacity = 0.8;
                GraficosCanvas.Children.Add(ejeY);               
            }
        }

        private void dibujarEtiquetas()
        {
            if (GraficosCanvas != null)
            {
                int cantidad = (int) viewModel._CanvasConfig.ParamReps.YMax;
                double colocar = viewModel._CanvasConfig.ParamReps.XMin;
                for (int i = 0; i <= cantidad; i += 100)
                {
                    Label l = new Label();
                    l.Content = i;
                    Point pc = new Point()
                    {
                        X = GraficarBarra.xReal_to_xPant(colocar/2, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax),
                        Y = GraficarBarra.yReal_to_yPant(i + 8, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
                    };
                    l.Margin = new Thickness(pc.X, pc.Y, 0, 0);
                    l.Foreground = Brushes.Black;
                    GraficosCanvas.Children.Add(l);
                }
            }
        }

        private void dibujarPuntos()
        {
            int cantidad = (int)viewModel._CanvasConfig.ParamReps.YMax;
            for (int i = 0; i <= cantidad; i += 100)
            {
                Point pc = new Point()
                {
                    X = GraficarBarra.xReal_to_xPant(0, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax),
                    Y = GraficarBarra.yReal_to_yPant(i, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
                };
                Line linea = new Line()
                {
                    X1 = pc.X,
                    Y1 = pc.Y,
                    X2 = pc.X,
                    Y2 = pc.Y + 2
                };
                linea.Stroke = Brushes.Black;
                linea.StrokeThickness = 5;
                GraficosCanvas.Children.Add(linea);
            }
        }

        private void dibujarBarras()
        {
            if(GraficosCanvas != null)
            {
                List<Barra> lista = viewModel.GetBarraCollection().ToList();
                double cantidadMax = viewModel._CanvasConfig.ParamReps.XMax;
                this.coordx = 0;
                this.sino = 0;
                foreach(Barra br in lista)
                {
                    this.coordy = 0;
                    this.coordx += 5;
                    this.sino += 1;
                    obtenerBarra(br);
                }
            }
        }

        private void obtenerBarra(Barra br)
        {
            double cal_acumuladas = br.Total;
            Tipo t = br.Otros;
            dibujarBarrita(t,cal_acumuladas);
            cal_acumuladas -= t.calorias;
            t = br.Cena;
            dibujarBarrita(t, cal_acumuladas);
            cal_acumuladas -= t.calorias;
            t = br.Merienda;   
            dibujarBarrita(t, cal_acumuladas);
            cal_acumuladas -= t.calorias;
            t = br.Comida;
            dibujarBarrita(t, cal_acumuladas);
            cal_acumuladas -= t.calorias;
            t = br.Aperitivo;
            dibujarBarrita(t, cal_acumuladas);
            cal_acumuladas -= t.calorias;
            t = br.Desayuno;
            dibujarBarrita(t, cal_acumuladas);
            dibujarFecha(br);
        }

        private void dibujarBarrita(Tipo t,double altura)
        {
            Point pc = new Point()
            {
                X = GraficarBarra.xReal_to_xPant(this.coordx, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax),
                Y = GraficarBarra.yReal_to_yPant(this.coordy, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            Line barra = new Line()
            {
                X1 = pc.X,
                Y1 = pc.Y,
                X2 = pc.X,
                Y2 = GraficarBarra.yReal_to_yPant(altura, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            barra.Stroke = new SolidColorBrush(t.color);
            if (viewModel._CanvasConfig.ParamReps.XMax == 160)
            {
                barra.StrokeThickness = 5;
            }
            else
            {
                barra.StrokeThickness = 20;
            }          
            GraficosCanvas.Children.Add(barra);
        }

        private void dibujarFecha(Barra br)
        {
            double y;
            double colocarY = viewModel._CanvasConfig.ParamReps.YMin * -1;
            double colocarX = (viewModel._CanvasConfig.ParamReps.XMin * -1) / 5;
            if(this.sino%2 != 0)
            {
                y = colocarY/1000;
            }
            else
            {
                y = colocarY/3;
            }
            Point pc = new Point()
            {
                X = GraficarBarra.xReal_to_xPant(this.coordx - colocarX, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax),
                Y = GraficarBarra.yReal_to_yPant(this.coordy - y, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            Label l = new Label();
            l.Content = br.Fecha.ToString("d");
            l.Foreground = Brushes.Black;
            if (viewModel._CanvasConfig.ParamReps.XMax == 160)
            {
                l.FontSize = 10;
            }
            else
            {
                l.FontSize = 12;
            }
            l.Margin = new Thickness(pc.X, pc.Y, 0,0);
            GraficosCanvas.Children.Add(l);
        }

        private void dibujarBarrasEspecifica()
        {
            if (GraficosCanvas != null)
            {
                List<Tipo> lista = viewModel.GetTipoCollection().ToList();
                this.coordx = 0;
                double x = viewModel._CanvasConfig.ParamReps.XMin * -1;
                foreach (Tipo t in lista)
                {
                    this.coordy = 0;
                    this.coordx += x;
                    dibujarTipoBarra(t);
                }
            }
        }

        private void dibujarTipoBarra(Tipo t)
        {
            Point pc = new Point()
            {
                X = GraficarBarra.xReal_to_xPant(this.coordx, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax),
                Y = GraficarBarra.yReal_to_yPant(this.coordy, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            Line barra = new Line()
            {
                X1 = pc.X,
                Y1 = pc.Y,
                X2 = pc.X,
                Y2 = GraficarBarra.yReal_to_yPant(t.calorias, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            barra.Stroke = new SolidColorBrush(t.color);
            barra.StrokeThickness = 50;
            GraficosCanvas.Children.Add(barra);
            Point pt = new Point()
            {
                X = GraficarBarra.xReal_to_xPant(this.coordx, GraficosCanvas.ActualWidth, viewModel.ParametrosRepresentacion.XMin, viewModel.ParametrosRepresentacion.XMax) - 30,
                Y = GraficarBarra.yReal_to_yPant(-5, GraficosCanvas.ActualHeight, viewModel.ParametrosRepresentacion.YMin, viewModel.ParametrosRepresentacion.YMax)
            };
            Label l = new Label();
            l.Content = t.comida;
            l.Foreground = Brushes.Black;
            l.Margin = new Thickness(pt.X, pt.Y, 0, 0);
            GraficosCanvas.Children.Add(l);
        }
        #endregion
    }
}
