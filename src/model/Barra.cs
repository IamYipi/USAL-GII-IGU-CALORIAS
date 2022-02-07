using System;
using System.ComponentModel;

namespace Calorias.src.model
{
    public class Barra : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Declaraciones
        private DateTime fecha;
        private Tipo desayuno;
        private Tipo aperitivo;
        private Tipo comida;
        private Tipo merienda;
        private Tipo cena;
        private Tipo otros;
        private double total;
        #endregion

        #region Constructor
        public Barra(DateTime fecha,Tipo desayuno, Tipo aperitivo, Tipo comida,Tipo merienda, Tipo cena, Tipo otros,double total)
        {
            this.fecha = fecha;
            this.desayuno = desayuno;
            this.aperitivo = aperitivo;
            this.comida = comida;
            this.merienda = merienda;
            this.cena = cena;
            this.otros = otros;
            this.total = total;
        }
        #endregion

        #region Propiedades

        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; OnPropertyChanged("fecha"); }
        }
        public Tipo Desayuno
        {
            get { return this.desayuno; }
            set { this.desayuno = value; OnPropertyChanged("desayuno"); }
        }

        public Tipo Aperitivo
        {
            get { return this.aperitivo; }
            set { this.aperitivo = value; OnPropertyChanged("aperitivo"); }
        }

        public Tipo Comida
        {
            get { return this.comida; }
            set { this.comida = value; OnPropertyChanged("comida"); }
        }

        public Tipo Merienda
        {
            get { return this.merienda; }
            set { this.merienda = value; OnPropertyChanged("merienda"); }
        }

        public Tipo Cena
        {
            get { return this.cena; }
            set { this.cena = value; OnPropertyChanged("cena"); }
        }

        public Tipo Otros
        {
            get { return this.otros; }
            set { this.otros = value; OnPropertyChanged("otros"); }
        }

        public double Total
        {
            get { return this.total; }
            set { this.total = value; OnPropertyChanged("total"); }
        }
        #endregion

        #region INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        public override string ToString()
        {
            return (desayuno + "/" + aperitivo + "/" + comida + "/" + merienda + "/" + cena + "/" + otros + "/" + total);
        }

    }
}
