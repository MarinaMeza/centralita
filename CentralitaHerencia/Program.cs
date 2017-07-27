using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CentralitaHerencia
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Ejercicio 54";

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Centralita telefonica = new Centralita("Telefonica");
            Local llamada1 = new Local("123", 30, "456", 2.65F);
            Provincial llamada2 = new Provincial("789", Franja.Franja_1, 21, "147");
            Local llamada3 = new Local("258", 45F, "369", 1.99F);
            Provincial llamada4 = new Provincial(Franja.Franja_3, llamada2);

            telefonica.RutaDeArchivo = AppDomain.CurrentDomain.BaseDirectory + "Centralita.xml";

            try
            {
                if (telefonica.DeSerializarse()) {
                    Console.WriteLine("Deserializacion exitosa");
                }
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }

            telefonica.RutaDeArchivo = AppDomain.CurrentDomain.BaseDirectory + "\\Llamadas.txt";

            Console.WriteLine("Primer llamada");
            telefonica += llamada1;
            Console.WriteLine(telefonica.ToString());
            
            Console.WriteLine("Reingreso Primer llamada");
            
            try
            {
                telefonica += llamada1;
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine(telefonica.ToString());

            Console.WriteLine("Segunda llamada");
            telefonica += llamada2;
            Console.WriteLine(telefonica.ToString());
            
            Console.WriteLine("Tercer llamada");
            telefonica += llamada3;
            Console.WriteLine(telefonica.ToString());

            Console.WriteLine("Cuarta llamada");

            try
            {
                telefonica += llamada4;
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine(telefonica.ToString());
            
            Console.WriteLine("Reingreso Cuarta llamada");

            try
            {
                telefonica += llamada4;
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(telefonica.ToString());
            
            Console.WriteLine("Ordenamiento");
            telefonica.OrdenarLLamadas();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(telefonica.ToString());
            Console.WriteLine("---------------------------------------------------------------------");
            telefonica.RutaDeArchivo = AppDomain.CurrentDomain.BaseDirectory + "Centralita.xml";

            try
            {
                if (telefonica.Serializarse()) {

                Console.WriteLine("Serializacion exitosa");
                }
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Llamadas.txt")){
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Llamadas.txt");
                Console.WriteLine(sr.ReadToEnd());
                sr.Close();
            }
            else {
                Console.WriteLine("No existe el archivo");
            }

            try
            {
                if (telefonica.DeSerializarse())
                {
                    Console.WriteLine("Deserializacion exitosa");
                }
            }
            catch (CentralitaException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
