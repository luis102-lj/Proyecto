using System;
using Proyecto21.Naipe;
using Proyecto21.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.ComTypes;

namespace Proyecto21.Jugabilidad
{
    public class Jugabilidad
    {
        private int cantidadDeJugadores { get; set; }
        private Jugador[] mesa=new Jugador [6];
        private Models.Naipe naipeDeLaMesa;
        
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
                            repartirCartas();
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
                Console.WriteLine("Ingrese la cantidad de jugadores(Minino 2, maximo 5 jugadores).\nSi ingresa 0 saldra del sistema.");
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


        //Desarollo del metodo repartir

        public void crearMesa(int jugadores) {
            for (int i = 1; i <= jugadores; i++)
            {
                Console.WriteLine("Ingrese el nombre del jugador {0}:", i);
                string nombre = Console.ReadLine();
                mesa[i] = new Jugador(nombre); 
                
            }
           }

        public void repartirCartas()
        {
            naipeDeLaMesa = new Models.Naipe();
            crearMesa(cantidadDeJugadores);

            for(int i=0; i<=cantidadDeJugadores -1 ; i++)
            {
                for(int j=0; j<=1; j++)
                {
                    Console.WriteLine("Hola {0},{1}", i, j);
                }
            }

           /* while (recorridoDeReparticion <= cantidadDeJugadores)
            {
                int darCarta = 1;
                while (darCarta <= 2)
                {
                    mesa[recorridoDeReparticion]._cartasJugador[darCarta] =
                                           naipeDeLaMesa.elMaso[naipeDeLaMesa.getContador()];
                    naipeDeLaMesa.setContador(naipeDeLaMesa.getContador() + 1);
                    Console.WriteLine("Se repartio la carta {0}{1} a la persona{2}",
                        mesa[recorridoDeReparticion]._cartasJugador[darCarta].Valor,
                        mesa[recorridoDeReparticion]._cartasJugador[darCarta].ElPalo,
                        mesa[recorridoDeReparticion].nombreJugador);
                    naipeDeLaMesa.setContador(naipeDeLaMesa.getContador() + 1);
                    darCarta++;
                }
                   
            }*/
        }
    }
}
