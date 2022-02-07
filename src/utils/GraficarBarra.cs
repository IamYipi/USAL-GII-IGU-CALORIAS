using System.Windows.Media;
using System.Windows.Shapes;

namespace Calorias.src.utils
{
    public class GraficarBarra
    {
        #region Funciones para obtener XY ejes
        public static Line GetEjeX(double canvasWidth,double canvasHeight,ParametrosRep rp)
        {
            Line ejeX = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = 0,
                Y1 = yReal_to_yPant(0, canvasHeight, rp.YMin, rp.YMax),
                X2 = canvasWidth,
                Y2 = yReal_to_yPant(0, canvasHeight, rp.YMin, rp.YMax)
            };
            return ejeX;
        }

        public static Line GetEjeY(double canvasWidth, double canvasHeight, ParametrosRep rp)
        {
            Line ejeY = new Line()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                X1 = xReal_to_xPant(0, canvasWidth, rp.XMin, rp.XMax),
                Y1 = 0,
                X2 = xReal_to_xPant(0, canvasWidth, rp.XMin, rp.XMax),
                Y2 = canvasHeight
            };
            return ejeY;
        }

        #endregion

        #region Funciones para obtener la posición de los puntos en el grafico
        public static double xPant_to_xReal(double xPant, double canvasWidth, double xRealMin, double xRealMax)
        {
            return (xPant * (xRealMax - xRealMin) / canvasWidth) + xRealMin;
        }

        public static double xReal_to_xPant(double xReal, double canvasWidth, double xRealMin, double xRealMax)
        {
            return ((xReal - xRealMin) * canvasWidth / (xRealMax - xRealMin));
        }

        public static double yPant_to_yReal(double yPant, double canvasHeight, double yRealMin, double yRealMax)
        {
            return yRealMin - ((yRealMax - yRealMin) * (yPant - canvasHeight) / canvasHeight);
        }

        public static double yReal_to_yPant(double yReal, double canvasHeight, double yRealMin, double yRealMax)
        {
            return (1 - (yReal - yRealMin) / (yRealMax - yRealMin)) * canvasHeight;
        }
        #endregion
    }
}
