using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto21.Models
{
    public class Jugador
    {
        //Atributos
        public string nombreJugador { get; set; }
        public Boolean recibeCarta { get; set; }
        public Boolean jugadorRetirado { get; set; }
        public Carta[] _cartasJugador;
        public int contadorDeCartasDePersona { get; set; }

        //-----------------------Constructores------------------------------//
        public Jugador()
        {
            this.nombreJugador = "";
            this._cartasJugador = new Carta[52];
        }

        public Jugador(string nombre)
        {
            nombreJugador = nombre;
            _cartasJugador = new Carta[10];
            jugadorRetirado = false;
            recibeCarta = true;
            contadorDeCartasDePersona = 0;
        }
        //-----------------------Fin Constructores------------------------------//




        //-----------------------Metodos------------------------------//

        //Sirve para consultarle al jugador si quiere parar de recibir cartas
        public Boolean Stay()
        {
            Boolean confirmacion = false;
            if (this.DevolverTotalMano() <= 21){
                Console.WriteLine(this.nombreJugador + " quiere quedarse o pedir? \n1)Marque 1 si quiere quedarse.\n2)Marque 2 si quiere pedir.");
                ConfirmacionDeStay(confirmacion);
            }else{
                recibeCarta = false;
                Console.Clear();
                Console.WriteLine(this.nombreJugador + " se ha pasado de 21, por lo que ya no tiene oportunidad de ganar ni solicitar mas cartas\n");
            }
            return recibeCarta;    
        }
        //-----------------------Fin Stay()------------------------------//

        //Metodo para verificar que el valor que ingrese el usuario sea el correcto en el metodo stay()
        public void ConfirmacionDeStay(Boolean confirmacion)
        {
            while (confirmacion != true){
                string valor = Console.ReadLine();
                if (valor == "1"){
                    Console.Clear();
                    recibeCarta = false;
                    confirmacion = true;
                }else if (valor == "2"){
                    recibeCarta = true;
                    confirmacion = true;
                }else{
                    Console.WriteLine("Ingrese una opcion valida");
                }
            }
        }
        //-----------------------Fin ConfirmacionDeStay()------------------------------//

        //Sirve para agregar una carta del maso de cartas de juego a las cartas que posee el jugador
        //Parametros: "naipe" se pide el maso de donde van a ser tomadas las cartas
        //"CantidadDeCartas"se pide la cantidad de cartas que requiere el jugador
        public void pedirCarta(Naipe naipe)
        {
             _cartasJugador[contadorDeCartasDePersona] = naipe.elMaso[naipe.getContador()]; 
            contadorDeCartasDePersona++;
            naipe.setContador(naipe.getContador() + 1);
        }
        //-----------------------Fin PedirCarta()------------------------------//

        //Sirve para devolver la suma de todos los valores de las cartas que posee el jugador
        public int DevolverTotalMano()
        {
            int resultado = 0;
            if (this._cartasJugador[0].Valor != 0){
                for (int i = 0; i < this._cartasJugador.Length; ++i){
                    if (this._cartasJugador[i] != null){
                        if (this._cartasJugador[i].Numero == "A"){
                            RecibioAs(this._cartasJugador[i]);
                        }
                        resultado += this._cartasJugador[i].Valor;
                    }
                }
            }
                return resultado;
        }
        //-----------------------Fin DevolverTotalMano()------------------------------//

        //Sirve para recorrer todas las cartas de jugador e imprimirlas
        public void imprimirMano()
        {
            Console.WriteLine("{0} sus cartas son:",nombreJugador);
             for (int i = 0; i <contadorDeCartasDePersona; i++){
                Console.WriteLine("{0}{1}", 
                    _cartasJugador[i].Numero,
                    _cartasJugador[i].ElPalo);   
             }
        }
        //-----------------------Fin ImprimirMano()------------------------------//

        //Sirve para confirmar el valor que se le quiere dar al As ya sea uno u once, es usada en DevolverTotalMano para evaluar si la carta en mano es un A
        //Parametros: "carta" es la carta a la cual se le va hacer la consulta si es un As o cualquier otra carta
        public void RecibioAs(Carta carta)
        {
            Boolean confirmacion = false;
            Console.Clear();
            this.imprimirMano();
            Console.WriteLine(this.nombreJugador +" posee un As, desea valuar el As como 11 o 1, digite 11 si desea ese valor o 1 en caso que desee 1: ");
            ConfirmacionRecibioAs(confirmacion,carta);    
        }
        //-----------------------Fin RecibioAs()------------------------------//


        //Metodo para verificar que el valor que ingrese el usuario sea el correcto en el metodo recibioAS()
        public void ConfirmacionRecibioAs(Boolean confirmacion, Carta carta)
        {
            while (confirmacion != true)
            {
                string valor = Console.ReadLine();
                if (valor == "11"){
                    carta.Valor = 11;
                    confirmacion = true;
                }else if (valor == "1"){
                    carta.Valor = 1;
                    confirmacion = true;
                }else{
                    Console.WriteLine("Ingrese una opcion valida");
                }
            }
        }
        //-----------------------Fin ConfirmacionRecibioAs()------------------------------//

        //-----------------------Fin metodos------------------------------//
    }
}
