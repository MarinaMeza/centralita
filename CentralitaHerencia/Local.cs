using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralitaHerencia
{
    [Serializable]
    public class Local : Llamada
    {
        protected float __costo;

        public Local() { }

        public Local(Llamada unaLLamada, float costo)
            : this(unaLLamada.NroOrigen, unaLLamada.Duracion, unaLLamada.NroDestino, costo) 
        {

        }

        public Local(string origen, float duracion, string destino, float costo) : base(origen, destino, duracion) {

            this.__costo = costo;
        }

        public override float CostoLlamada { get { return this.CalcularCosto(); } set { } }

        private float CalcularCosto() {

            return (float)base._duracion * this.__costo;
        }

        protected override string Mostrar() {
            
            StringBuilder datosLocal = new StringBuilder();
            base.Mostrar();
            datosLocal.Append("Costo llamada local: ");
            datosLocal.Append(this.CostoLlamada.ToString());
            
            return datosLocal.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Local)) {
                return true;
            }
            return false;
        }

        public override string ToString() {

            return this.Mostrar();
        }

    }
}
