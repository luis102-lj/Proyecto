using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto21.Models
{
    public class Jugador
    {

        private string nombreJugador { get; set; }
        private Boolean recibeCarta { get; set; }
        private Boolean jugadorRetirado { get; set; }
        private Carta[] _cartasJugador = new Carta[52];
        private int contadorDeCartasDePersona { get; set; }

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
