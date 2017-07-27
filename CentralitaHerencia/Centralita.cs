using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CentralitaHerencia
{
    [Serializable]
    [XmlInclude(typeof(Llamada))]
    [XmlInclude(typeof(Local))]
    [XmlInclude(typeof(Provincial))]

    public class Centralita : ISerializable
    {
        private List<Llamada> _listaDeLlamadas;
        private string _ruta;
        protected string _razonSocial;

        public Centralita()
        {
            this._listaDeLlamadas = new List<Llamada>();
        }

        public Centralita(string nombreEmpresa)
            : this()
        {
            this._razonSocial = nombreEmpresa;
        }

        public float GananciaPorLocal { get { return CalcularGanancia(TipoLlamada.Local); } set { } }

        public float GananciaPorProvincial { get { return CalcularGanancia(TipoLlamada.Provincial); } set { } }

        public float GananciaTotal { get { return CalcularGanancia(TipoLlamada.Todas); } set{}}

        public List<Llamada> Llamadas
        {
            get { return this._listaDeLlamadas; }
            set { this._listaDeLlamadas = value; }
        }
        
        public string RazonSocial
        {
            get { return this._razonSocial; }
            set { this._razonSocial = value; }
        }

        public string RutaDeArchivo
        {
            get { return this._ruta; }
            set { this._ruta = value; }
        }


        private void AgregarLlamada(Llamada nuevaLlamada) {
            try
            {
                this._listaDeLlamadas.Add(nuevaLlamada);
                this.GuardarEnArchivo(nuevaLlamada, true);
            }
            catch (CentralitaException)
            {
                
            }

            
        }

        private float CalcularGanancia(TipoLlamada tipo) {
            
            float acumulador = 0;

            foreach (Llamada item in this.Llamadas)
            {

                if (item.GetType().Name.ToString() == tipo.ToString() || tipo==TipoLlamada.Todas)
                {
                    if (item.GetType() == typeof(Local)) {
                        Local aux;
                        aux = (Local)item;
                        acumulador += aux.CostoLlamada;
                    }
                    
                    if (item.GetType() == typeof(Provincial))
                    {
                        Provincial aux;
                        aux = (Provincial)item;
                        acumulador += aux.CostoLlamada;
                    }
                }
            }
            return acumulador;
        }

        public override string ToString() {
            
            StringBuilder datosCentralita = new StringBuilder();
            
            datosCentralita.Append("Razon social: ").AppendLine(this._razonSocial);
            datosCentralita.Append("Ganancia por llamados locales: ").AppendLine(this.GananciaPorLocal.ToString());
            datosCentralita.Append("Ganancia por llamados provinciales: ").AppendLine(this.GananciaPorProvincial.ToString());
            datosCentralita.Append("Ganancia por todos los llamados: ").AppendLine(this.GananciaTotal.ToString());
            
            foreach (Llamada item in this._listaDeLlamadas)
            {
                datosCentralita.AppendLine(item.ToString());
            }
            
            return datosCentralita.ToString();
        }

        public void OrdenarLLamadas() {
            this._listaDeLlamadas.Sort(Llamada.OrdenarPorDuracion);
        }

        public bool Serializarse() {
            try
            {
                using(XmlTextWriter tw = new XmlTextWriter(this._ruta, Encoding.UTF8)){
                    XmlSerializer s = new XmlSerializer(typeof(Centralita));

                    s.Serialize(tw, this);
                    tw.Close();
                }
            }
            catch (Exception e)
            {
                throw new CentralitaException("No se pudo serializar", base.ToString(), "Serializarse", e);
            }
            return true;
        }

        public bool DeSerializarse() {
            try
            {
                XmlTextReader tr = new XmlTextReader(this._ruta);
                XmlSerializer s = new XmlSerializer(typeof(Centralita));

                object ob = s.Deserialize(tr);
                Centralita cent = (Centralita)ob;
                tr.Close();

                return true;
            }
            catch (Exception e)
            {
                throw new CentralitaException("No se pudo deserializar", base.ToString(), "Deserializarse", e);
            }
        }

        private bool GuardarEnArchivo(Llamada unaLlamada, bool agrego) {
            try
            {
                StreamWriter sw = new StreamWriter(this._ruta, agrego);
                sw.WriteLine("---Llamadas---");
                sw.WriteLine(unaLlamada.ToString());
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new CentralitaException("No se pudo guardar el archivo", "Centralita", "GuardarEnArchivo", e);
            }
        }

        public static bool operator ==(Centralita central, Llamada nuevaLlamada) {
            foreach (Llamada item in central._listaDeLlamadas) {
                if (item == nuevaLlamada) {
                    return true;
                }
            }
            return false;
        }

        public static bool operator !=(Centralita central, Llamada nuevaLlamada)
        {
            return !(central == nuevaLlamada);
        }

        public static Centralita operator +(Centralita central, Llamada nuevaLlamada) {
            
            if (central != nuevaLlamada){
                central.AgregarLlamada(nuevaLlamada);
            }
            else {
                throw new CentralitaException("La llamada ya se encuentra registrada", "Centralita", "Sobrecarga del operador +");
            }
            return central;
        }
    }
}