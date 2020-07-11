using System;
using Proyecto21.Naipe;
using Proyecto21.Models;

namespace Proyecto21.Jugabilidad
{
    public class Jugabilidad
    {
        //-----------------------Atributos------------------------------//
        private int cantidadDeJugadores { get; set; }
        private Jugador[] mesa=new Jugador [7];
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
                    break;
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
                if (ganador == "")
                {
                    ganador = "No hay ganador porque todos los jugadores se pasaron de 21!";
                }
              seguir=seguirJugando(ganador);
            }
            return seguir;
        }
        //-----------------------Fin JugarUnaMesa()------------------------------//


        //Metodo para verificar que el valor que ingrese el usuario sea el correcto en el metodo JugarMesa
        public string seguirJugando(string ganador)
        {
            imprimirResultado();
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
        public void imprimirResultado()
        {
            Console.Clear();
            for (int i = 0; i <= cantidadDeJugadores ; i++)
            {
                mesa[i].imprimirMano();
                Console.WriteLine("Con un total de puntos: {0}", mesa[i].DevolverTotalMano());
            }
        }
        //-----------------------Fin imprimirMesa()------------------------------//

        //Se encarga de reiniciar las variables necesarias para empezar un juego nuevo
        public void reiniciarJuego()
        {
            naipeDeLaMesa = new Models.Naipe();
            
            for(int i = 0; i < cantidadDeJugadores+1; i++){
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
            mesa[cantidadDeJugadores]= new Jugador("La Casa");
        }
        //-----------------------Fin CrearMesa()------------------------------//

        //Sirve para repartir las cartas iniciales a un jugador
        public void repartirCartas()
        {
            naipeDeLaMesa = new Models.Naipe();
            Acciones barajar=new Acciones();
            barajar.Barajar(naipeDeLaMesa);
            
            for(int i=0; i<=cantidadDeJugadores; i++)
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
                    ValidacionDeCheater();
                    while (jugadores[i].recibeCarta){
                        jugadores[i].RecibioAs(jugadores[i]);
                        ValidacionDeRangoDeJuego(jugadores[i]);
                    }
                } 
            }
        }
        //-----------------------Fin Dealer()------------------------------//

        public void verLaSiguieteCarta()
        {
            Console.WriteLine("La siguiente carta es: {0} {1}", naipeDeLaMesa.elMaso[naipeDeLaMesa.getContador()].Numero,
               naipeDeLaMesa.elMaso[naipeDeLaMesa.getContador()].ElPalo);
        }
        public void verLaSiguientesCartasPorCantidad()
        {
            int numeroDeCartas=-1;
            while (numeroDeCartas == -1){
                try {
                    Console.WriteLine("Ingrese la cantidad de cartas a consultar(Maximo 5)");
                    numeroDeCartas = Int32.Parse(Console.ReadLine());
                    if (verificacionDeCantidadDeCartas(numeroDeCartas) == true){
                        Console.Clear();
                        Console.WriteLine("Estas son las siguientes {0} cartas:", numeroDeCartas);
                        for (int i = naipeDeLaMesa.getContador(); i <= numeroDeCartas + naipeDeLaMesa.getContador() - 1; i++){
                            Console.WriteLine("Carta {0} {1}", naipeDeLaMesa.elMaso[i].Numero,
                                 naipeDeLaMesa.elMaso[i].ElPalo);
                        }
                    }else{
                        numeroDeCartas = -1;
                    }
                }catch (FormatException){
                    Console.WriteLine("Ingrese una opcion valida");
                    numeroDeCartas = -1;
                }
            }
        }

        public Boolean verificacionDeCantidadDeCartas(int result)
        {
            if (result < 6){
                return true;
            }else{
                Console.WriteLine("Ingrese menor o igual a 5");
                return false;
            }
        }
        public void ValidacionDeRangoDeJuego(Jugador jugador)
        {
            if (jugador.DevolverTotalMano() >= 15){
                jugador.Stay();
                if (jugador.recibeCarta){
                    jugador.pedirCarta(naipeDeLaMesa);
                    jugador.imprimirMano();
                }
            }
            else{
                Console.WriteLine("Se le agrego otra carta mas, ya que contaba con una puntuacion menor a 15 puntos.");
                jugador.pedirCarta(naipeDeLaMesa);
                jugador.imprimirMano();
            }
        }

        

        public void ValidacionDeCheater()
        {
            Console.WriteLine("Quieres hacer trampa?\n1)Ingrese 1 si quiere hacer trampa.\n2)Ingrese 2 si quiere seguir jugando");
            string hacerTrampa= "";
            while (hacerTrampa != "2")
            {
                hacerTrampa = Console.ReadLine();
                if (hacerTrampa == "1"){
                    ValidacionDeCualTrampa();
                    break;
                }else if(hacerTrampa=="2"){
                    break;
                }else{
                    Console.WriteLine("Ingrese un valor correcto");
                }
            }
            
        }   
        

        public void ValidacionDeCualTrampa()
        {
            Console.WriteLine("Que tipo de trampa quieres hacer." +
                "\n1)Ingresa uno si solo quieres ver la carta siguiente" +
                "\n2)Ingresa 2 si quieres ver varias cartas." +
                "\nx)Ingresa x si no quieres hacer trampa");
            string cualTrampa = "";
            while (cualTrampa != "x"){
                cualTrampa = Console.ReadLine();
                if (cualTrampa == "1"){
                    verLaSiguieteCarta();
                    break;
                }else if (cualTrampa=="2"){
                    verLaSiguientesCartasPorCantidad();
                    break;
                }else if (cualTrampa == "x"){
                    break;
                }else{
                    Console.WriteLine("Ingrese un valor correcto");
                }
            }

        }
    

        public void imprimirMaso()
        {
            for(int i=0; i <= naipeDeLaMesa.elMaso.Length-1; i++)
            {
                Console.WriteLine("Carta {0} {1}", naipeDeLaMesa.elMaso[i].Numero,
                     naipeDeLaMesa.elMaso[i].ElPalo);
            }
            Console.WriteLine(naipeDeLaMesa.elMaso.Length);
        }

        //-----------------------Fin Metodos------------------------------//

    }
}
