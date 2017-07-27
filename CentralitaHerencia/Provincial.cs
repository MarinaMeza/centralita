using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralitaHerencia
{
    [Serializable]
    public class Provincial : Llamada
    {
        protected Franja _franjaHoraria;

        public Provincial() { }

        public Provincial(Franja miFranja, Llamada unallamada)
            : this(unallamada.NroOrigen, miFranja, unallamada.Duracion, unallamada.NroDestino)
        {

        }

        public Provincial(string origen, Franja miFranja, float duracion, string destino)
            : base(origen, destino, duracion)
        {

            this._franjaHoraria = miFranja;
        }

        public override float CostoLlamada { get { return CalcularCosto(); } set { } }

        private float CalcularCosto() {
            
            return (float)this._franjaHoraria * base._duracion;
        }

        protected override string Mostrar() {
            
            StringBuilder datosLocal = new StringBuilder();
            base.Mostrar();
            datosLocal.Append("Costo llamada provincial: ");
            datosLocal.Append(this.CostoLlamada.ToString());

            return datosLocal.ToString();
        }

        public override bool Equals(object obj) {
            if (obj.GetType().Equals(typeof(Provincial))) {
                return true;
            }
            
            return false;
        }

        public override string ToString() {
            
            return this.Mostrar();
        }
    }
}
