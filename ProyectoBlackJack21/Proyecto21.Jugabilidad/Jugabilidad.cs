using System;
using Proyecto21.Naipe;
using Proyecto21.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.ComTypes;

namespace Proyecto21.Jugabilidad
{
    public class Jugabilidad
    {
        //-----------------------Atributos------------------------------//
        private int cantidadDeJugadores { get; set; }
        private Jugador[] mesa=new Jugador [6];
        private Models.Naipe naipeDeLaMesa;
        //-----------------------fin Atributos------------------------------//


        //-----------------------Metodos------------------------------//

        //Sirve para deplegar un menu para jugar 21, donde seleccione alguna opcion y lo lleve a la funcionalidad de esa opcion
        public void Menu21()
        {
            string opcionSeleccionada = "";
            
            while (opcionSeleccionada != "2")
            {
                opcionesDeInformacion(1);
                opcionSeleccionada = Console.ReadLine();
                switch (opcionSeleccionada)
                {
                    case "1":
                        Console.Clear();
                        Jugar21(opcionSeleccionada);
                        break;
                    case "2":
                        opcionesDeInformacion(3);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opcion Invalida, ingrese una opcion valida");
                       break;
                }
            }
        }
        //-----------------------Fin Menu21()------------------------------//

        //Sirve para realizar validaciones antes de iniciar el 21
        public void Jugar21(String opcionSeleccionada)
        {
            string continuarJugando = "1";
            cantidadDeJugadores = validacionDeJugadores();
            while (continuarJugando == "1")
            {
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
                    continuarJugando= jugarUnaMesa();
                }
            }
        }
        //-----------------------Fin Jugar21()------------------------------//

        //Realiza la jugabilidad del 21 en una mesa
        public string jugarUnaMesa()
        {
            string seguir = "1";
            crearMesa(cantidadDeJugadores);
            while (seguir=="1")
            {
                repartirCartas();
                Console.Clear();
                Console.WriteLine("Revisen sus cartas bien y evaluen si quiere mas cartas o quedarse");
                dealer(mesa);
                string ganador = EvaluarGanador(mesa);
                reiniciarJuego();
                if (ganador == "")
                {
                    ganador = "Nadie, porque ambos se pasaron de 21!";
                }
              seguir=seguirJugando(ganador);
            }
            return seguir;
        }
        //-----------------------Fin JugarUnaMesa()------------------------------//


        //Metodo para verificar que el valor que ingrese el usuario sea el correcto en el metodo JugarMesa
        public string seguirJugando(string ganador)
        {
            Console.WriteLine("El ganador es: " + ganador);
            Console.WriteLine("Mano terminada, quieres seguir Jugando?\nDigite 1 si quiere seguir jugando \nEnter si quiere salir del juego");
            String continuarJugando = Console.ReadLine();
            if(continuarJugando == "1"){
                reiniciarJuego();
                return "1";
            }else{
                Console.Clear();
                return "2";
            }
        }
        //-----------------------Fin seguirJugando()------------------------------//

        //Sirve para imprimir las peronas con sus respectivas cartas de una mesa
        public void imprimirMesa()
        { 
            for(int i = 0; i <= cantidadDeJugadores-1; i++){
                mesa[i].imprimirMano();
            }
        }
        //-----------------------Fin imprimirMesa()------------------------------//

        //Se encarga de reiniciar las variables necesarias para empezar un juego nuevo
        public void reiniciarJuego()
        {
            naipeDeLaMesa = new Models.Naipe();
            
            for(int i = 0; i < cantidadDeJugadores; i++){
                mesa[i]._cartasJugador = new Carta[10];
                mesa[i].contadorDeCartasDePersona = 0;
                mesa[i].recibeCarta = true;
                mesa[i].jugadorRetirado = false;
            }
            Console.Clear();
        }
        //-----------------------Fin reinicarJuego()------------------------------//

        //Metodo para ahorrar dialogo repetitivo
        public void opcionesDeInformacion(int numeroDeInformacion)
        {
            if (numeroDeInformacion == 1)
                Console.WriteLine("Bienvenidos a BLACKJACK21 \n1) Jugar. \n2) Salir.");
            else if (numeroDeInformacion == 2)
                Console.WriteLine("Ingrese la cantidad de jugadores(Minino 2, maximo 5 jugadores).\nSi ingresa 0 saldra del sistema.");
            else if (numeroDeInformacion == 3)
                Console.WriteLine("Gracias por utilizar BLACKJACK21");
        }
        //-----------------------Fin OpcionesDeInformacion()------------------------------//

        //Reterno la cantidad de jugadores de la mesa
        public int validacionDeJugadores()
        {
            opcionesDeInformacion(2);
            string cantidadDeJugadores = Console.ReadLine();
            int result;
            try
            {
                result = Int32.Parse(cantidadDeJugadores);
                return verificacionValidacionDeJugadores(result);
               }catch (FormatException){
                Console.Clear();
                Console.WriteLine("Ingrese una opcion valida de jugadores, no se permiten ningun valor que no sea numerico.");
                return -1;
            }
        }
        //-----------------------Fin CrearMesa()------------------------------//

        //Metodo para verificar que el valor que ingrese el usuario sea el correcto en el metodo ValidacionDeJugadores
        public int verificacionValidacionDeJugadores(int result)
        {
            if (result > 1 && result < 6){
                Console.Clear();
                return result;
            }if (result == 0){
                Console.Clear();
                return result;
            }else{
                Console.Clear();
                Console.WriteLine("Ingrese un valor entre 2 y 5");
                return -1;
            }
        }
        //-----------------------Fin verificacionValidacionDeJugadores()------------------------------//

        //Sirve para crear una mesa con las cantidad de jugadores que se requiera 
        public void crearMesa(int jugadores) {
            for (int i = 0; i <= jugadores-1; i++){
                Console.WriteLine("Ingrese el nombre del jugador {0}:", i+1);
                string nombre = Console.ReadLine();
                mesa[i] = new Jugador(nombre); 
            }
        }
        //-----------------------Fin CrearMesa()------------------------------//

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
            Console.Clear();
        }
        //-----------------------Fin RepartirCartas()------------------------------//

        /*Evalua cual de los jugadores de la mesa es el ganador*/
        public String EvaluarGanador(Jugador[] jugadores)
        {
            Jugador ganador = new Jugador();
            int mayor = 0;
            for (int i = 0; i <= jugadores.Length-1; ++i){
                if(jugadores[i]!= null){
                    int totalMano = jugadores[i].DevolverTotalMano();
                    if (mayor < totalMano && totalMano <= 21){
                        mayor = totalMano;
                        ganador = jugadores[i];
                    }
                }
            }
            return ganador.nombreJugador;
        }
        //-----------------------Fin EvaluarJugador()------------------------------//

        // El juego determina por medio de un dealer que ofrezca más cartas al jugador en caso de necesitarlas, o quedarse y continuar preguntando al siguiente jugador
        public void dealer(Jugador[] jugadores)
        {
            for (int i = 0; i <= jugadores.Length-1; ++i) 
            {
                if(jugadores[i] != null){
                    jugadores[i].imprimirMano();
                    jugadores[i].Stay();
                    while (jugadores[i].recibeCarta){
                        Console.Clear();
                        jugadores[i].pedirCarta(naipeDeLaMesa);
                        jugadores[i].imprimirMano();
                        jugadores[i].Stay();
                    }
                } 
            }
        }
        //-----------------------Fin Dealer()------------------------------//


        //-----------------------Fin Metodos------------------------------//


    }
}
