using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralitaHerencia
{
    public interface ISerializable
    {
        string RutaDeArchivo { get; set; }

        bool Serializarse();

        bool DeSerializarse();
    }
}
