using System;
using System.Collections.Generic;

namespace MonitoreoSensores
{
    // Clase base
    public class Sensor
    {
        public string Identificador { get; set; }
        public double ValorMedio { get; set; }
        public bool Activo { get; set; }

        public Sensor(string identificador, double valorMedio, bool activo)
        {
            Identificador = identificador;
            ValorMedio = valorMedio;
            Activo = activo;
        }

        public string ObtenerEstado()
        {
            return Activo ? "Activo" : "Inactivo";
        }

        public virtual void MostrarDatos()
        {
            Console.WriteLine($"Identificador: {Identificador}");
            Console.WriteLine($"Valor medio:   {ValorMedio}");
            Console.WriteLine($"Estado:        {ObtenerEstado()}");
        }

        public virtual void Evaluar()
        {
            if (!Activo)
            {
                Console.WriteLine("El sensor está inactivo, no se puede evaluar.");
            }
        }
    }

    // Sensor de temperatura (límite 80 °C)
    public class SensorTemperatura : Sensor
    {
        public const double Limite = 80.0;

        public SensorTemperatura(string id, double valor, bool activo)
            : base(id, valor, activo) { }

        public override void Evaluar()
        {
            base.Evaluar();
            if (!Activo) return;

            if (ValorMedio > Limite)
                Console.WriteLine($"¡ATENCIÓN! Temperatura de {ValorMedio} °C supera el límite de {Limite} °C.");
            else
                Console.WriteLine($"Temperatura de {ValorMedio} °C dentro del criterio establecido (límite {Limite} °C).");
        }

        public override void MostrarDatos()
        {
            Console.WriteLine("-------- Sensor de Temperatura --------");
            base.MostrarDatos();
            Console.WriteLine("Unidad:        °C");
        }
    }

    // Sensor de presión (límite 10 bar)
    public class SensorPresion : Sensor
    {
        public const double Limite = 10.0;

        public SensorPresion(string id, double valor, bool activo)
            : base(id, valor, activo) { }

        public override void Evaluar()
        {
            base.Evaluar();
            if (!Activo) return;

            if (ValorMedio > Limite)
                Console.WriteLine($"¡ATENCIÓN! Presión de {ValorMedio} bar supera el límite de {Limite} bar.");
            else
                Console.WriteLine($"Presión de {ValorMedio} bar dentro del criterio establecido (límite {Limite} bar).");
        }

        public override void MostrarDatos()
        {
            Console.WriteLine("-------- Sensor de Presión --------");
            base.MostrarDatos();
            Console.WriteLine("Unidad:        bar");
        }
    }

    // Sensor de distancia (objeto detectado a menos de 20 cm)
    public class SensorDistancia : Sensor
    {
        public const double Limite = 20.0;

        public SensorDistancia(string id, double valor, bool activo)
            : base(id, valor, activo) { }

        public override void Evaluar()
        {
            base.Evaluar();
            if (!Activo) return;

            if (ValorMedio < Limite)
                Console.WriteLine($"¡ATENCIÓN! Objeto detectado a {ValorMedio} cm, está a menos de {Limite} cm.");
            else
                Console.WriteLine($"Objeto a {ValorMedio} cm, dentro del criterio establecido (mínimo {Limite} cm).");
        }

        public override void MostrarDatos()
        {
            Console.WriteLine("-------- Sensor de Distancia --------");
            base.MostrarDatos();
            Console.WriteLine("Unidad:        cm");
        }
    }

    class Program
    {
        static double LeerDouble(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Intenta de nuevo: ");
            }
            return valor;
        }

        static bool LeerActivo()
        {
            Console.Write("¿Sensor activo? (s/n): ");
            string r = Console.ReadLine()?.Trim().ToLower();
            return r == "s" || r == "si" || r == "sí";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("SISTEMA DE MONITOREO DE SENSORES INDUSTRIALES ACT 14\n");

            var sensores = new List<Sensor>();

            // 1. Ingresar Sensor de Temperatura
            Console.WriteLine("--- Registrar Sensor de Temperatura ---");
            Console.Write("Identificador: ");
            string idTemp = Console.ReadLine();
            double valTemp = LeerDouble("Valor medido (°C):  ");
            bool actTemp = LeerActivo();
            sensores.Add(new SensorTemperatura(idTemp, valTemp, actTemp));

            Console.WriteLine();

            // 2. Ingresar Sensor de Presión
            Console.WriteLine("--- Registrar Sensor de Presión ---");
            Console.Write("Identificador: ");
            string idPres = Console.ReadLine();
            double valPres = LeerDouble("Valor medido (bar):   ");
            bool actPres = LeerActivo();
            sensores.Add(new SensorPresion(idPres, valPres, actPres));

            Console.WriteLine();

            // 3. Ingresar Sensor de Distancia
            Console.WriteLine("--- Registrar Sensor de Distancia ---");
            Console.Write("Identificador: ");
            string idDist = Console.ReadLine();
            double valDist = LeerDouble("Valor medido (cm):  ");
            bool actDist = LeerActivo();
            sensores.Add(new SensorDistancia(idDist, valDist, actDist));

            // Mostrar y evaluar todos automáticamente al finalizar el 
            Console.WriteLine();
            Console.WriteLine("       EVALUACIÓN DE LOS SENSORES       ");
            foreach (var s in sensores)
            {
                s.MostrarDatos();
                s.Evaluar();
                Console.WriteLine();
            }
        }
    }
}