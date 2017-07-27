using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralitaHerencia
{
    [Serializable]
    public abstract class Llamada
    {
        protected float _duracion;
        protected string _nroDestino;
        protected string _nroOrigen;

        public Llamada() { }

        public Llamada(string origen, string destino, float duracion)
        {
            this._duracion = duracion;
            this._nroDestino = destino;
            this._nroOrigen = origen;
        }

        public abstract float CostoLlamada { get; set; }
        
        public float Duracion { get { return this._duracion; } }

        public string NroDestino { get { return this._nroDestino; } }

        public string NroOrigen { get { return this._nroOrigen; } }

        protected virtual string Mostrar() {
            
            StringBuilder datosLlamada = new StringBuilder();
            datosLlamada.Append("Duracion: ");
            datosLlamada.AppendLine(this._duracion.ToString());
            datosLlamada.Append("Numero destino: ");
            datosLlamada.AppendLine(this._nroDestino);
            datosLlamada.Append("Numero origen: ");
            datosLlamada.AppendLine(this._nroOrigen);
            return datosLlamada.ToString();
        }

        public static bool operator ==(Llamada uno, Llamada dos) {
            if (uno.GetType().Equals(dos.GetType()) && (uno.NroDestino == dos.NroDestino)) {
                return true;
            }
            return false;
        }

        public static bool operator !=(Llamada uno, Llamada dos)
        {
            return !(uno == dos);
        }

        public static int OrdenarPorDuracion(Llamada uno, Llamada dos) {
            if (uno._duracion < dos._duracion) {
                return -1;
            }
            if (uno._duracion > dos._duracion) {
                return 1;
            }
            else{
                return 0;
            }
        }
    }
}
