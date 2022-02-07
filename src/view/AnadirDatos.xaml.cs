using Calorias.src.model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Calorias.src.view
{
    /// <summary>
    /// Lógica de interacción para anadirDatos.xaml
    /// </summary>
    public partial class AnadirDatos : Window
    {
        #region Constructor
        public AnadirDatos()
        {
            InitializeComponent();
            BarrasCreadas = new List<Barra>();
        }
        #endregion

        #region Propiedades
        public List<Barra> BarrasCreadas { get; }
        #endregion

        #region Añadir Y Cancelar
        private void AnadirButton_Click(object sender, RoutedEventArgs e)
        {
            if (Desayuno.Text.Length > 0 && Aperitivo.Text.Length > 0 && Comida.Text.Length > 0 && Merienda.Text.Length > 0 && Cena.Text.Length > 0 && Otros.Text.Length > 0)
            {
                BarrasCreadas.Clear();
                DateTime fecha = (DateTime)Fecha.SelectedDate;
                double desayuno_cantidad = Convert.ToDouble(Desayuno.Text);
                double aperitivo_cantidad = Convert.ToDouble(Aperitivo.Text);
                double comida_cantidad = Convert.ToDouble(Comida.Text);
                double merienda_cantidad = Convert.ToDouble(Merienda.Text);
                double cena_cantidad = Convert.ToDouble(Cena.Text);
                double otros_cantidad = Convert.ToDouble(Otros.Text);
                Color color = Color.FromRgb(255, 0, 0);
                Tipo desayuno = new Tipo("DESAYUNO", desayuno_cantidad, color);
                color = Color.FromRgb(0, 0, 255);
                Tipo aperitivo = new Tipo("APERITIVO", aperitivo_cantidad, color);
                color = Color.FromRgb(255, 142, 224);
                Tipo comida = new Tipo("COMIDA", comida_cantidad, color);
                color = Color.FromRgb(104, 104, 109);
                Tipo merienda = new Tipo("MERIENDA", merienda_cantidad, color);
                color = Color.FromRgb(0, 255, 0);
                Tipo cena = new Tipo("CENA", cena_cantidad, color);
                color = Color.FromRgb(255, 158, 78);
                Tipo otros = new Tipo("OTROS", otros_cantidad, color);
                double total = desayuno_cantidad + aperitivo_cantidad + comida_cantidad + merienda_cantidad + cena_cantidad + otros_cantidad;
                BarrasCreadas.Add(new Barra(fecha, desayuno, aperitivo, comida, merienda, cena, otros, total));
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Alguno de los valores introducidos esta vacio,porfavor introduzca un valor", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void AleatorioButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            double random = r.Next(0, 1000);
            Desayuno.Text = random.ToString();
            random = r.Next(0, 500);
            Aperitivo.Text = random.ToString();
            random = r.Next(0, 500);
            Comida.Text = random.ToString();
            random = r.Next(0, 1000);
            Merienda.Text = random.ToString();
            random = r.Next(0, 500);
            Cena.Text = random.ToString();
            random = r.Next(0, 1000);
            Otros.Text = random.ToString();
        }

        //https://stackoverflow.com/questions/23512507/validate-a-number-with-regex
        private void NumberFormatText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region Inicializar
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Fecha.SelectedDate = DateTime.Today;
            Desayuno.Text = "0";
            Aperitivo.Text = "0";
            Comida.Text = "0";
            Merienda.Text = "0";
            Cena.Text = "0";
            Otros.Text = "0";
        }
        #endregion


    }
}