using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Calorias.src.view
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        #region Declaraciones
        public double calorias_max { get; set; }
        public string seguimiento { get; set; }
        #endregion

        #region Constructor
        public Configuracion()
        {
            InitializeComponent();
        }
        #endregion

        #region Botones Cancelar/Guardar
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if(CaloriasMaxTextBox.Text.Length > 0)
            {
                calorias_max = Convert.ToDouble(CaloriasMaxTextBox.Text);
                if (calorias_max < 1000)
                {
                    MessageBox.Show("Porfavor introduzca un valor igual o superior a 1000", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    calorias_max = 0;
                    CaloriasMaxTextBox.Text = null;
                }
                else if (calorias_max % 10 != 0)
                {
                    MessageBox.Show("Porfavor introduzca un valor múltipo de 10", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    calorias_max = 0;
                    CaloriasMaxTextBox.Text = null;
                }
                else
                {
                    seguimiento = ComboBoxSeguimiento.SelectedItem.ToString();
                    this.DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show("Porfavor introduzca un valor de calorias maximas valido", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }          
        }

        private void CaloriasMaxTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
