using Calorias.src.utils;
using System;
using System.Collections.ObjectModel;

namespace Calorias.src.model
{
    public class ViewModel
    {
        #region Declaraciones
        //Events
        public event EventHandler AnadirBarraEvent;
        public event EventHandler CanvasMaxCaloriasEvent;
        //Variables
        private CanvasConfig canvasConfig;
        //Variables
        private Model model;
        #endregion

        #region Constructor
        public ViewModel()
        {
            this.model = new Model();
            this.canvasConfig = Constantes.DefaultCanvasConfig;
        }
        #endregion

        #region Propiedades
        public ParametrosRep ParametrosRepresentacion
        {
            get
            {
                return new ParametrosRep()
                {
                    XMin = this.canvasConfig.ParamReps.XMin,
                    XMax = this.canvasConfig.ParamReps.XMax,
                    YMin = this.canvasConfig.ParamReps.YMin,
                    YMax = this.canvasConfig.ParamReps.YMax
                };
            }
        }     

        public CanvasConfig _CanvasConfig
        {
            get { return this.canvasConfig; }
            set
            {
                this.canvasConfig = value;
                OnCanvasConfigCambioEvent(new EventArgs());
            }
        }
        #endregion

        #region Funcion Obtener la Coleccion de Barras y tipo Especifico
        public ObservableCollection<Barra> GetBarraCollection()
        {
            return model.GetBarraCollection();
        }

        public ObservableCollection<Tipo> GetTipoCollection()
        {
            return model.GetComidaEspecificaCollection();
        }
        #endregion

        #region Actions
        public void AnadirBarra(Barra barra)
        {
            this.model.AnadirBarra(barra);
            OnAnadirBarraEvent(new EventArgs());
        }

        public void ClearBarras()
        {
            this.model.ClearBarraCollection();
        }

        public void AnadirTipo(Tipo t)
        {
            this.model.AnadirTipo(t);
        }

        public void ClearListaTipo()
        {
            this.model.ClearTipoCollection();
        }
        public void BorrarBarraEspecifica(Barra barra)
        {
            this.model.BorrarBarra(barra);
        }

        public void ActualizarComida(DateTime fecha, Tipo comida)
        {
            this.model.ActualizarComida(fecha, comida);
        }
        #endregion

        #region Eventos
        protected virtual void OnAnadirBarraEvent(EventArgs e)
        {
            AnadirBarraEvent?.Invoke(this, e);
        }

        protected virtual void OnCanvasConfigCambioEvent(EventArgs e)
        {
            CanvasMaxCaloriasEvent?.Invoke(this, e);
        }
        #endregion

        #region Funciones ordenar
        public void SetBarraCollectionOrdenadaAscendente()
        {
            this.model.SetBarraCollectionOrdenadaAscendente();
        }

        public void SetBarraCollectionOrdenadaDescendente()
        {
            this.model.SetBarraCollectionOrdenadaDescendente();
        }

        public void SetBarraCollectionOrdenadaDescendenteCalorias()
        {
            this.model.SetBarraCollectionOrdenadaAscendenteCalorias();
        }

        public void SetBarraCollectionOrdenadaAscendenteCalorias()
        {
            this.model.SetBarraCollectionOrdenadaDescendenteCalorias();
        }
        #endregion
    }
}