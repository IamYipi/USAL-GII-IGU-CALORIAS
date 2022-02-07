using System.Windows.Media;

namespace Calorias.src.model
{
    public class Tipo
    {
        #region Declaraciones
        public string comida;
        public double calorias;
        public Color color;
        #endregion

        #region Constructor
        public Tipo(string comida,double calorias,Color color)
        {
            this.comida = comida;
            this.calorias = calorias;
            this.color = color;
        }
        #endregion

        #region Propiedades
        public string Comida {
            get { return this.comida; }
            set { this.comida = value;}
        }
        public double Calorias
        {
            get { return this.calorias; }
            set { this.calorias = value; }
        }
        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        #endregion
    }
}
