using System;
using System.Collections.ObjectModel;

namespace Calorias.src.model
{
    public class Model
    {
        #region Declaraciones
        private ObservableCollection<Barra> barrasColeccion;
        private ObservableCollection<Tipo> tipoColeccion;
        #endregion

        #region Constructor
        public Model()
        {
            this.barrasColeccion = new ObservableCollection<Barra>();
            this.tipoColeccion = new ObservableCollection<Tipo>();
        }
        #endregion

        #region Funciones que retornan colecciones
        public ObservableCollection<Barra> GetBarraCollection()
        {
            return this.barrasColeccion;
        }

        public ObservableCollection<Tipo> GetComidaEspecificaCollection()
        {
            return this.tipoColeccion;
        }
        #endregion

        #region Añadir/Borrar
        public void AnadirBarra(Barra barra)
        {
            this.barrasColeccion.Add(barra);
        }

        public void BorrarBarra(Barra barra)
        {
            this.barrasColeccion.Remove(barra);
        }

        public void AnadirTipo(Tipo t)
        {
            this.tipoColeccion.Add(t);
        }

        public void ClearTipoCollection()
        {
            this.tipoColeccion.Clear();
        }

        public void ClearBarraCollection()
        {
            this.barrasColeccion.Clear();
        }
        #endregion

        #region Ordenar
        public void SetBarraCollectionOrdenadaAscendente()
        {
            for (int i = 1; i < this.barrasColeccion.Count; ++i)
            {
                Barra barra = this.barrasColeccion[i];
                int j = i - 1;
                while (j >= 0 && this.barrasColeccion[j].Fecha > barra.Fecha)
                {
                    barrasColeccion.Move(j + 1, j);
                    j = j - 1;
                }
                barrasColeccion[j + 1] = barra;
            }
        }

        public void SetBarraCollectionOrdenadaDescendente()
        {
            for (int i = 1; i < this.barrasColeccion.Count; ++i)
            {
                Barra barra = this.barrasColeccion[i];
                int j = i - 1;
                while(j >= 0 && this.barrasColeccion[j].Fecha < barra.Fecha)
                {
                    barrasColeccion.Move(j + 1, j);
                    j = j - 1;
                }
                barrasColeccion[j + 1] = barra;
            }
        }

        public void SetBarraCollectionOrdenadaAscendenteCalorias()
        {
            for (int i = 1; i < this.barrasColeccion.Count; ++i)
            {
                Barra barra = this.barrasColeccion[i];
                int j = i - 1;
                while (j >= 0 && this.barrasColeccion[j].Total > barra.Total)
                {
                    barrasColeccion.Move(j + 1, j);
                    j = j - 1;
                }
                barrasColeccion[j + 1] = barra;
            }
        }

        public void SetBarraCollectionOrdenadaDescendenteCalorias()
        {
            for (int i = 1; i < this.barrasColeccion.Count; ++i)
            {
                Barra barra = this.barrasColeccion[i];
                int j = i - 1;
                while (j >= 0 && this.barrasColeccion[j].Total < barra.Total)
                {
                    barrasColeccion.Move(j + 1, j);
                    j = j - 1;
                }
                barrasColeccion[j + 1] = barra;
            }
        }
        #endregion

        #region Actualizar Datos Editados
        public void ActualizarComida(DateTime fecha, Tipo comida)
        {
            Barra barra = null;
            int i;
            for(i = 0; i < this.barrasColeccion.Count; ++i)
            {
                barra = this.barrasColeccion[i];
                if(barra.Fecha == fecha)
                {
                    break;
                }
            }
            double total = barra.Total;
            double diferencia = 0;
            if (barra.Desayuno.comida == comida.comida)
            {
                if(barra.Desayuno.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Desayuno.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Desayuno.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Desayuno = comida;
            }
            else if (barra.Aperitivo.comida == comida.comida)
            {
                if (barra.Aperitivo.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Aperitivo.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Aperitivo.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Aperitivo = comida;
            }
            else if (barra.Comida.comida == comida.comida)
            {
                if (barra.Comida.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Comida.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Comida.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Comida = comida;
            }
            else if (barra.Merienda.comida == comida.comida)
            {
                if (barra.Merienda.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Merienda.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Merienda.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Merienda = comida;
            }
            else if (barra.Cena.comida == comida.comida)
            {
                if (barra.Cena.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Cena.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Cena.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Cena = comida;
            }
            else if (barra.Otros.comida == comida.comida)
            {
                if (barra.Otros.calorias < comida.calorias)
                {
                    diferencia = comida.calorias - barra.Otros.calorias;
                    barra.Total += diferencia;
                }
                else
                {
                    diferencia = barra.Otros.calorias - comida.calorias;
                    barra.Total -= diferencia;
                }
                barra.Otros = comida;
            }
        }
        #endregion
    }
}
