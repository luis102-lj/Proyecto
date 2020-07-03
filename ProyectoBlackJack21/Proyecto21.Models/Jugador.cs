using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto21.Models
{
    public class Jugador
    {

        public string nombreJugador { get; set; }
        public Boolean recibeCarta { get; set; }
        public Boolean jugadorRetirado { get; set; }
        public Carta[] _cartasJugador;
        public int contadorDeCartasDePersona { get; set; }


        public Jugador(string nombre)
        {
            nombreJugador = nombre;
            _cartasJugador = new Carta[10];
            jugadorRetirado = false;
            recibeCarta = true;
            contadorDeCartasDePersona = 0;
            

        }

        public Boolean Stay()
        {
            Console.WriteLine("Quiere quedarse o pedir? \n1)Marque 1 si quiere quedarse.\n2)Marque 2 si quiere pedir.");
            int recibirCartaSolicitud=Convert.ToInt32( Console.ReadLine());
            recibeCarta = (recibirCartaSolicitud==1) ? recibeCarta =false : recibeCarta = true;
            return recibeCarta;
        }

        public void pedirCarta(Naipe naipe)
        {

            Boolean solicitudDeCarta=Stay();

            if (solicitudDeCarta)
            {
                _cartasJugador[contadorDeCartasDePersona+1] = naipe.elMaso[naipe.getContador()];
                Console.WriteLine("La nueva carta es un {0} {1}", _cartasJugador[contadorDeCartasDePersona +1 ].Numero, 
                    _cartasJugador[contadorDeCartasDePersona +1].ElPalo);
                contadorDeCartasDePersona++;
                naipe.setContador(naipe.getContador()+1);
            }
            else
            {
                Console.WriteLine("Espera que los otros jugadores terminen sus movimientos");
            }
        }


        public Boolean getRecibeCarta()
        {
            return recibeCarta;
        }
      }
}
