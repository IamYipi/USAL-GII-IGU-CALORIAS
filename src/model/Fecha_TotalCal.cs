namespace Calorias.src.model
{
    public class Fecha_TotalCal
    {
        private string fecha;
        private double total;
        
        public Fecha_TotalCal(string fecha,double total)
        {
            this.fecha = fecha;
            this.total = total;
        } 

        public string Fecha { get { return fecha; } set { fecha = value; } }
        public double Total { get { return total; } set { total = value; } }

        public override string ToString()
        {
            string res = fecha + " " + total;
            return res;
        }
    }
}
