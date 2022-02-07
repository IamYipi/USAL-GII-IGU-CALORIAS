using Calorias.src.model;
using Calorias.src.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Calorias.src.view
{
    /// <summary>
    /// Lógica de interacción para NoModalCalorias.xaml
    /// </summary>
    public partial class NoModalCalorias : Window
    {
        #region Declaraciones
        private ViewModel viewModel;
        public EventHandler SeleccionCambioEvent;
        public EventHandler ActualizarCanvasEvent;
        public EventHandler GuardarImagenEvent;
        #endregion

        #region Constructor
        public NoModalCalorias(ViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            mostrarDatos();
        }
        #endregion

        #region Añadir / Borrar
        private void Button_AnadirFecha_Click(object sender, RoutedEventArgs e)
        {
            List<Barra> barras = viewModel.GetBarraCollection().ToList();
            AnadirDatos ad = new AnadirDatos();
            bool anadir = true;
            ad.Owner = this;
            ad.ShowDialog();
            if(ad.DialogResult == true)
            {
                if(barras != null) {
                    foreach (Barra barrita in ad.BarrasCreadas)
                    {
                        foreach (Barra br in barras)
                        {
                            if(br.Fecha == barrita.Fecha)
                            {
                                MessageBoxResult messageBox = MessageBox.Show("La fecha introducida ya existe en la base de datos","ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
                                anadir = false;
                            }
                        }
                    }
                }
                if (anadir == true)
                {
                    foreach (Barra br in ad.BarrasCreadas)
                    {
                        viewModel.AnadirBarra(br);
                        anadirListView1(br);
                    }
                }
            }
        }

        private void Button_BorrarFecha_Click(object sender, RoutedEventArgs e)
        {
            TablaTotal.UnselectAll();
            TablaEspecifica.Items.Clear();
            OnActualizarCanvasEvent(new EventArgs());
            MessageBoxResult messageBox = MessageBox.Show("Desea borrar todas las fechas?", "ATENCION", MessageBoxButton.YesNo);
            if (messageBox == MessageBoxResult.Yes)
            {
                viewModel.ClearBarras();
                TablaTotal.Items.Clear();
                TablaEspecifica.Items.Clear();
                OnActualizarCanvasEvent(new EventArgs());
            }
        }
        #endregion

        #region TablaTotal
        private void mostrarDatos()
        {
            TablaTotal.Items.Clear();
            TablaEspecifica.Items.Clear();
            List<Barra> lista = viewModel.GetBarraCollection().ToList();
            if (lista != null)
            {
                foreach (Barra br in lista)
                {
                    anadirListView1(br);
                }
            }
        }

        private void anadirListView1(Barra br)
        {
            Fecha_TotalCal fcal = new Fecha_TotalCal(br.Fecha.ToString("d"), br.Total);
            TablaTotal.Items.Add(fcal);
        }

        private void TablaTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Limpiamos ListView 2 y buscamos el dia seleccionado   
            if (TablaTotal.SelectedItem != null)
            {
                TablaEspecifica.Items.Clear();
                string datos = TablaTotal.SelectedItem.ToString();
                string[] datos2 = datos.Split(' ');
                string fecha = datos2[0];
                listaTipo(fecha);
                OnSeleccionCambioEvent(new EventArgs());
            }
        }

        private void TablaTotal_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (TablaTotal.SelectedItem != null)
                {
                    TablaEspecifica.Items.Clear();
                    string datos = TablaTotal.SelectedItem.ToString();
                    string[] datos2 = datos.Split(' ');
                    string fecha = datos2[0];
                    MessageBoxResult messageBoxResult = MessageBox.Show("Estas seguro que desea borrar la fecha " + fecha, "ATENCION", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        List<Barra> list = viewModel.GetBarraCollection().ToList();
                        Barra barra = null;
                        foreach (Barra br in list)
                        {
                            if (br.Fecha.ToString("d") == fecha)
                            {
                                barra = br;
                            }
                        }
                        viewModel.BorrarBarraEspecifica(barra);                     
                        mostrarDatos();
                        OnActualizarCanvasEvent(new EventArgs());
                    }
                }
            }
        }
        #endregion

        #region TablaEspecifica
        private void listaTipo(string fecha)
        {
            List<Barra> list = viewModel.GetBarraCollection().ToList();
            Barra barra = null;
            foreach (Barra br in list)
            {
                if (br.Fecha.ToString("d") == fecha)
                {
                    barra = br;
                }
            }
            //Añadimos los datos obtenidos en un arrayList y al segundo List View
            viewModel.ClearListaTipo();
            Tipo tipo = new Tipo(barra.Desayuno.comida, barra.Desayuno.calorias, barra.Desayuno.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
            tipo = new Tipo(barra.Aperitivo.comida, barra.Aperitivo.calorias, barra.Aperitivo.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
            tipo = new Tipo(barra.Comida.comida, barra.Comida.calorias, barra.Comida.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
            tipo = new Tipo(barra.Merienda.comida, barra.Merienda.calorias, barra.Merienda.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
            tipo = new Tipo(barra.Cena.comida, barra.Cena.calorias, barra.Cena.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
            tipo = new Tipo(barra.Otros.comida, barra.Otros.calorias, barra.Otros.color);
            TablaEspecifica.Items.Add(tipo);
            viewModel.AnadirTipo(tipo);
        }

        //Para borrar la tabla si se pincha en un sitio fuera
        private void TablaTotal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //https://stackoverflow.com/questions/30737097/wpf-how-deselect-all-my-selected-items-when-click-on-empty-space-in-my-listview
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
            {
                TablaTotal.UnselectAll();
                TablaEspecifica.Items.Clear();
                OnActualizarCanvasEvent(new EventArgs());
            }
        }
        #endregion

        #region Botones Ordenar
        private void OrdenarAscendenteButton_Click(object sender, RoutedEventArgs e)
        {
            TablaTotal.UnselectAll();
            TablaTotal.Items.Clear();
            TablaEspecifica.Items.Clear();
            this.viewModel.SetBarraCollectionOrdenadaAscendente();
            List<Barra> barras = viewModel.GetBarraCollection().ToList();
            foreach (Barra br in barras)
            {
                anadirListView1(br);
            }
            OnActualizarCanvasEvent(new EventArgs());
        }

        private void OrdenarDescendenteButton_Click(object sender, RoutedEventArgs e)
        {
            TablaTotal.UnselectAll();
            TablaTotal.Items.Clear();
            TablaEspecifica.Items.Clear();
            this.viewModel.SetBarraCollectionOrdenadaDescendente();
            List<Barra> barras = viewModel.GetBarraCollection().ToList();
            foreach (Barra br in barras)
            {
                anadirListView1(br);
            }
            OnActualizarCanvasEvent(new EventArgs());
        }

        private void OrdenarPorCaloriasDescendente_Button_Click(object sender, RoutedEventArgs e)
        {
            TablaTotal.UnselectAll();
            TablaTotal.Items.Clear();
            TablaEspecifica.Items.Clear();
            this.viewModel.SetBarraCollectionOrdenadaAscendenteCalorias();
            List<Barra> barras = viewModel.GetBarraCollection().ToList();
            foreach (Barra br in barras)
            {
                anadirListView1(br);
            }
            OnActualizarCanvasEvent(new EventArgs());
        }

        private void OrdenarPorCaloriasAscendente_Button_Click(object sender, RoutedEventArgs e)
        {
            TablaTotal.UnselectAll();
            TablaTotal.Items.Clear();
            TablaEspecifica.Items.Clear();
            this.viewModel.SetBarraCollectionOrdenadaDescendenteCalorias();
            List<Barra> barras = viewModel.GetBarraCollection().ToList();
            foreach (Barra br in barras)
            {
                anadirListView1(br);
            }
            OnActualizarCanvasEvent(new EventArgs());
        }
        #endregion

        #region Eventos ActualizarCanvas
        protected virtual void OnSeleccionCambioEvent(EventArgs e)
        {
            SeleccionCambioEvent?.Invoke(this, e);
        }

        protected virtual void OnActualizarCanvasEvent(EventArgs e)
        {
            ActualizarCanvasEvent?.Invoke(this, e);
        }

        #endregion

        #region MenuItems Eventos y Funciones
        private void GuardarImagen_Click(object sender, RoutedEventArgs e)
        {
            OnGuardarImagenEvent(new EventArgs());
        }

        protected virtual void OnGuardarImagenEvent(EventArgs e)
        {
            GuardarImagenEvent?.Invoke(this, e);
        }

        private void Configuracion_Click(object sender, RoutedEventArgs e)
        {
            Configuracion config = new Configuracion();
            config.Owner = this;
            config.ShowDialog();
            if(config.DialogResult == true)
            {
                double x;
                if(config.ComboBoxSeguimiento.Text == "SEMANAL")
                {
                    x = 40;
                }
                else
                {
                    x = 160;
                }
                CanvasConfig canvascfg = viewModel._CanvasConfig;
                ParametrosRep rep = new ParametrosRep()
                {
                    XMax = x,
                    XMin = x / 10 * (-1),
                    YMin = config.calorias_max / 10 * (-1),
                    YMax = config.calorias_max
                };
                canvascfg.ParamReps = rep;
                viewModel._CanvasConfig = canvascfg;
            }
        }

        private void Creditos_Click(object sender, RoutedEventArgs e)
        {
            String creditos = "Creditos: Javier Garcia Pechero\n" +
                "Aplicacion: Contador Calorias 1.0\n" +
                "Asignatura: IGU\n" +
                "Email: javigp@usal.es";
            MessageBox.Show(creditos, "CREDITOS", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AyudaInstrucciones_Click(object sender, RoutedEventArgs e)
        {
            String ayuda = "ESTE ES UN MANUAL DE USO DE LA APLICACION\n" + "" +
                "DESEA LEERLO?";
            MessageBoxResult messageBox = MessageBox.Show(ayuda, "AYUDA", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if(messageBox == MessageBoxResult.Yes)
            {
                ayuda = "La ventana principal es la que muestra las graficas.\nAl pasar el raton sobre ella se puede observar la aparicion" +
                    " de dos botones flotantes.\n El primero (simbolo calorias) abre la ventana para consultar,borrar,introducir los datos de las calorias.\n" +
                    "El segundo resetea el canvas y muestra la grafica global de todos los datos\n" +
                    "Desea seguir leyendo sobre la seguna ventana?";
                messageBox = MessageBox.Show(ayuda, "AYUDA", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if(messageBox == MessageBoxResult.Yes)
                {
                    ayuda = "La segunda ventana permite introducir datos de calorias (boton +), borrar todos los datos (boton papelera), ordenar ascendentemente por fecha o " +
                        "por calorias, ordenar descendentemente por fecha o por calorias\n" + "Tambien se incluye opciones para guardar la imagen de la grafica que se esté representando en el canvas\n" +
                        "Al pinchar en cualquier dato de una fecha concreta aparecera en el segundo list view (Fecha Especifica) de manera desglosada, especificando cantidad de calorias de cada comida asi como cambiará la grafica en" + 
                        " el canvas principal. Ademas si seleccionas una fecha(Primer ListView) con el click izquierdo y despues le das a click derecho te dará la opcion de borrar esa fecha concreta\n" +
                        "Tambien cuenta con el boton configuracion que permite establecer el maximo de calorias a visualizar en la grafica, así como el máximo de graficas posibles para la representacion." +
                        "Es una aplicacion pensada para conteo de calorias semanal o mensual, guardando una imagen de la grafica. Y borrando las graficas ya que el canvas no permite mas de 31 gráficas(Mensual), 7 (semanal)\n" +
                        "Y bueno si has llegado hasta aqui es porque has descubierto el boton ayuda\n" +
                        "Desea seguir leyendo sobre el apartado Editar Fecha?";
                    messageBox = MessageBox.Show(ayuda, "AYUDA", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (messageBox == MessageBoxResult.Yes)
                    {
                        ayuda = "En este apartado se permite la edicion de una fecha concreta, seleccionando su comida concreta. Nos permite editar color y calorias\n" +
                            "Solamente se habilitará la opcion de edición si la fecha introducida existe en la base de datos. A su vez cuando actualicemos la edición podremos observar en el canvas de las " +
                            "graficas como se modifica las barras\n" + "Espero haberte ayudado, disfruta de la aplicacion :D";
                        MessageBox.Show(ayuda, "AYUDA", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
        #endregion

        #region TabControl Editar Fecha
        private void Calendario_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Calendario.IsEnabled)
            {
                List<Barra> list = viewModel.GetBarraCollection().ToList();
                if (list.Count == 0)
                {
                    MessageBox.Show("No existen fechas en la base de datos, se le va a dirigir a la interfaz de tablas (pulse + para añadir datos)", "ATENCION",MessageBoxButton.OK,MessageBoxImage.Error);
                    /*https://stackoverflow.com/questions/7929646/how-to-programmatically-select-a-tabitem-in-wpf-tabcontrol*/
                    Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                    WrapPanelComidas.Visibility = Visibility.Hidden;
                    WrapPanelCalorias.Visibility = Visibility.Hidden;
                    WrapPanelColor.Visibility = Visibility.Hidden;
                }
                else
                {
                    Barra barra = null;
                    foreach (Barra br in list)
                    {
                        if (br.Fecha == Calendario.SelectedDate)
                        {
                            barra = br;
                        }
                    }
                    if (barra == null)
                    {
                        MessageBox.Show("No existe la fecha seleccionada en la base de datos, se le va a dirigir a la interfaz de tablas para que se fije en una de ellas y la introduzca", "ATENCION",MessageBoxButton.OK, MessageBoxImage.Error);
                        /*https://stackoverflow.com/questions/7929646/how-to-programmatically-select-a-tabitem-in-wpf-tabcontrol*/
                        Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
                        WrapPanelComidas.Visibility = Visibility.Hidden;
                        WrapPanelCalorias.Visibility = Visibility.Hidden;
                        WrapPanelColor.Visibility = Visibility.Hidden;
                        GroupBoxConfirmacion.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        WrapPanelComidas.Visibility = Visibility.Visible;
                        WrapPanelCalorias.Visibility = Visibility.Visible;
                        WrapPanelColor.Visibility = Visibility.Visible;
                        GroupBoxConfirmacion.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void SliderCalorias_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SliderCalorias.Value = (int)SliderCalorias.Value;
        }

        private void MainTabControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0)
            {
                Calendario.IsEnabled = false;
                Calendario.SelectedDate = null;
                WrapPanelComidas.Visibility = Visibility.Hidden;
                WrapPanelCalorias.Visibility = Visibility.Hidden;
                WrapPanelColor.Visibility = Visibility.Hidden;
                GroupBoxConfirmacion.Visibility = Visibility.Hidden;
                Calendario.IsEnabled = true;
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            /*https://stackoverflow.com/questions/7929646/how-to-programmatically-select-a-tabitem-in-wpf-tabcontrol*/
            Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 0));
            WrapPanelComidas.Visibility = Visibility.Hidden;
            WrapPanelCalorias.Visibility = Visibility.Hidden;
            WrapPanelColor.Visibility = Visibility.Hidden;
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            Tipo comida = new Tipo(ComboBoxComidas.Text,SliderCalorias.Value,ColorSelector.SelectedColor.Value);
            DateTime fecha = (DateTime)Calendario.SelectedDate;
            viewModel.ActualizarComida(fecha, comida);
            listaTipo(fecha.ToString("d"));
            mostrarDatos();
            OnSeleccionCambioEvent(new EventArgs());
        }
    }
    #endregion
}
