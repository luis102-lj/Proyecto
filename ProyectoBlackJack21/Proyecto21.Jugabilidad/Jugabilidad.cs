using System;
using Proyecto21.Naipe;
using Proyecto21.Models;
namespace Proyecto21.Jugabilidad
{
    public class Jugabilidad
    {
        private int cantidadDeJugadores { get; set; }
        
        public void Menu21()
        {
            string opcionSeleccionada = "";
            opcionesDeInformacion(1);
            while (opcionSeleccionada != "2")
            {

                opcionSeleccionada = Console.ReadLine();
                switch (opcionSeleccionada)
                {
                    case "1":
                        cantidadDeJugadores = validacionDeJugadores();
                        while (cantidadDeJugadores == -1)
                        {
                            cantidadDeJugadores = validacionDeJugadores();
                        }
                        if (cantidadDeJugadores == 0)
                        {
                            opcionesDeInformacion(3);
                            opcionSeleccionada = "2";
                        }
                        else
                        {
                            jugar21(cantidadDeJugadores);
                        }

                        break;
                    case "2":
                        opcionesDeInformacion(3);
                        break;
                    default:
                        Console.WriteLine("Opcion Invalida, ingrese una opcion valida");
                        opcionesDeInformacion(1);
                        break;
                }
            }
        }

        public void jugar21(int cantidadDeJugadores)
        {
            Console.WriteLine("Empieza el juego");
            Acciones jugar21 = new Acciones();
            Models.Naipe naipe = new Models.Naipe();


        }

        public void opcionesDeInformacion(int numeroDeInformacion)
        {
            if (numeroDeInformacion == 1)
                Console.WriteLine("Bienvenidos a BLACKJACK21 \n1) Jugar. \n2) Salir.");
            else if (numeroDeInformacion == 2)
                Console.WriteLine("Ingrese la cantidad de jugadores(Maximo 5 jugadores).\nSi ingresa 0 saldra del sistema.");
            else if (numeroDeInformacion == 3)
                Console.WriteLine("Gracias por utilizar BLACKJACK21");
        }

        public int validacionDeJugadores()
        {
            opcionesDeInformacion(2);
            string cantidadDeJugadores = Console.ReadLine();
            int result;
            try
            {
                result = Int32.Parse(cantidadDeJugadores);
                if (result > 1 && result < 6)
                {
                    return result;
                }
                if (result == 0)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Ingrese un valor entre 2 y 5");
                    return -1;
                }



            }
            catch (FormatException)
            {
                Console.WriteLine("Ingrese una opcion valida de jugadores, no se permiten ningun valor que no sea numerico.");
                return -1;
            }
        }
    }
}
