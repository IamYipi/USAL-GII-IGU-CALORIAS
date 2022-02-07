namespace Calorias.src.utils
{
    public class Constantes
    {
        #region Configuracion al iniciar el programa
        public static readonly CanvasConfig DefaultCanvasConfig = new CanvasConfig()
        {
            ParamReps = new ParametrosRep()
            {
                XMin = -16,
                XMax = 160,
                YMin = -350,
                YMax = 3500
            }
        };
        #endregion
    }

    #region Declaraciones
    public struct ParametrosRep
    {
        public double XMin, XMax, YMin, YMax;
    };
    public struct CanvasConfig
    {
        public ParametrosRep ParamReps { get; set; }
    }
    #endregion
}
