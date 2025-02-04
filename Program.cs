using System;

class Program
{
    static void Main()
    {
        Random rand = new Random();
        int tiempoBase = 80; // Tiempo base en minutos (1 hora 20 minutos)
        int capacidadBus = 70;
        int horaInicio = 8;
        int horaFin = 18;
        int pasajerosNoDesmontados = 0;
        int[] estaciones = new int[2]; // Personas esperando en cada estación
        int horaActual = horaInicio;

        while (horaActual <= horaFin)
        {
            for (int direccion = 0; direccion < 2; direccion++)
            {
                int origen = direccion;
                int destino = 1 - direccion;
                int personasEnBus = pasajerosNoDesmontados;
                int tiempoTotal = tiempoBase;

                Console.WriteLine("====================================================");
                Console.WriteLine($"[{horaActual}:00] Bus sale de Estación {origen + 1} a Estación {destino + 1}");
                Console.WriteLine("====================================================");

                estaciones[origen] += rand.Next(20, 51); // Llegada de nuevas personas

                int espacioDisponible = capacidadBus - personasEnBus;
                int personasSuben = Math.Min(estaciones[origen], espacioDisponible);
                personasEnBus += personasSuben;
                estaciones[origen] -= personasSuben;

                Console.WriteLine($"Estación {origen + 1}: Suben {personasSuben} personas, quedan esperando {estaciones[origen]}.");
                Console.WriteLine($"Pasajeros en el bus: {personasEnBus}");

                // Variables que pueden afectar el tiempo
                bool ponchadura = rand.Next(1, 11) <= 2;
                bool lluvia = rand.Next(1, 11) <= 3;
                bool trafico = rand.Next(1, 11) <= 4;
                bool accidente = rand.Next(1, 21) == 1;

                if (ponchadura)
                {
                    Console.WriteLine("Se ha ponchado una goma. +20 min");
                    tiempoTotal += 20;
                }
                if (lluvia)
                {
                    int retraso = rand.Next(10, 31);
                    Console.WriteLine($"Lluvia intensa. +{retraso} min");
                    tiempoTotal += retraso;
                }
                if (trafico)
                {
                    int retraso = rand.Next(5, 16);
                    Console.WriteLine($"Tráfico pesado. +{retraso} min");
                    tiempoTotal += retraso;
                }
                if (accidente)
                {
                    int retraso = rand.Next(15, 46);
                    Console.WriteLine($"Accidente en la vía. +{retraso} min");
                    tiempoTotal += retraso;
                }

                int horaLlegada = horaActual + (tiempoTotal / 60);
                int minutosLlegada = tiempoTotal % 60;
                Console.WriteLine($"Tiempo estimado del viaje: {tiempoTotal} minutos. Llegada: {horaLlegada}:{minutosLlegada:D2}");

                // Determinar si es el último viaje del día
                if (horaActual == horaFin && direccion == 1)
                {
                    Console.WriteLine("Último viaje del día: Todos los pasajeros se desmontan.");
                    personasEnBus = 0;
                }
                else
                {
                    // Determinar cuántas personas se desmontan en la estación de destino
                    int personasDesmontan = 0;
                    for (int i = 0; i < personasEnBus; i++)
                    {
                        if (rand.Next(1, 101) > 3)
                        {
                            personasDesmontan++;
                        }
                    }
                    personasEnBus -= personasDesmontan;
                    pasajerosNoDesmontados = personasEnBus;
                    Console.WriteLine($"{personasDesmontan} personas se desmontaron en Estación {destino + 1}.");
                }

                Console.WriteLine($"Pasajeros que permanecen en el bus: {personasEnBus}\n");

                horaActual = horaLlegada + 1; // El siguiente bus sale una hora después de la llegada
            }
        }

        Console.WriteLine("====================================================");
        Console.WriteLine("Resumen final:");
        Console.WriteLine("====================================================");
        for (int i = 0; i < estaciones.Length; i++)
        {
            Console.WriteLine($"Estación {i + 1}: {estaciones[i]} personas quedaron esperando.");
        }
    }
}
