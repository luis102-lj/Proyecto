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
                        Jugar21(opcionSeleccionada);
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


        public void Jugar21(String opcionSeleccionada)
        {
            cantidadDeJugadores = validacionDeJugadores();
            while (cantidadDeJugadores == -1)
            {
                cantidadDeJugadores = validacionDeJugadores();
            }
            if (cantidadDeJugadores == 0)
            {
                opcionesDeInformacion(3);
                opcionSeleccionada = "2";
            }else{
                string continuarJugando = "1";
                crearMesa(cantidadDeJugadores);
                repartirCartas();
                while (continuarJugando=="1")
                {
                    imprimirMesa();
                    Console.WriteLine("Mano terminada, quieres seguir Jugando?\nDigite 1 si quiere seguir jugando y 2 si quiere salir del juego");
                    continuarJugando= Console.ReadLine();
                    if (continuarJugando=="1")
                    {
                        reiniciarJuego();
                        
                    }
                }
                
            }
        }

        public void imprimirMesa()
        { 

            for(int i = 0; i <= cantidadDeJugadores-1; i++)
            {
                mesa[i].imprimirMano();
            }
        }

        public void reiniciarJuego()
        {
            naipeDeLaMesa = new Models.Naipe();
            for(int i = 0; i < cantidadDeJugadores; i++)
            {
                mesa[i]._cartasJugador = new Carta[10];
                mesa[i].contadorDeCartasDePersona = 0;
                mesa[i].recibeCarta = true;
            }
            repartirCartas();

        }

        //Probado, se le puede agregar algun dialogo repetitivo
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
               }catch (FormatException){
                Console.WriteLine("Ingrese una opcion valida de jugadores, no se permiten ningun valor que no sea numerico.");
                return -1;
            }
        }


        //Desarollo del metodo repartir

        
        //Sirve para crear una mesa con las cantidad de jugadores que se requiera 
        public void crearMesa(int jugadores) {
            for (int i = 0; i <= jugadores-1; i++)
            {
                Console.WriteLine("Ingrese el nombre del jugador {0}:", i+1);
                string nombre = Console.ReadLine();
                mesa[i] = new Jugador(nombre); 
                
            }
           }
         
        //Sirve para repartir las cartas iniciales a un jugador
        public void repartirCartas()
        {
            naipeDeLaMesa = new Models.Naipe();
            Acciones barajar=new Acciones();
            barajar.Barajar(naipeDeLaMesa);
            
            for(int i=0; i<=cantidadDeJugadores-1; i++)
            {
                for(int j = 0; j <= 1; j++)
                {
                 mesa[i].pedirCarta(naipeDeLaMesa);
                }
            }
        }


        //Actualizacion LUISQUESADA

        //Sirve para confirmar el valor que se le quiere dar al As ya sea uno u once
        //Parametros: "carta" es la carta a la cual se le va hacer la consulta si es un As o cualquier otra carta
        public void RecibioAs(Carta carta)
        {
            Boolean confirmacion=false;
            Console.WriteLine("Usted acaba de recibir un As, desea valuar el as como 11 o 1, digite 11 si desea ese valor o 1 en caso que desee 1: ");
            if (carta.Numero == "A")
            {
                while (confirmacion!=true)
                {
                    string valor = Console.ReadLine();
                    if (valor == "11")   {
                        carta.Valor = 11;
                        confirmacion = true;
                    }
                    else if (valor == "1"){
                        carta.Valor = 1;
                        confirmacion = true;
                    }
                    else{
                        Console.WriteLine("Ingrese una opcion valida");
                    }
                }
            }
        }

        /*Evalua cual de los jugadores de la mesa es el ganador*/
        public String EvaluarGanador(Jugador[] jugadores)
        {
            Jugador ganador = new Jugador();
            int mayor = 0;
            for (int i = 0; i <= jugadores.Length; ++i)
            {
                int totalMano = jugadores[i].DevolverTotalMano();
                if (mayor < totalMano)
                {
                    mayor = totalMano;
                    ganador = jugadores[i];
                }
            }
            return ganador.getNombre();
        }

    }
}
