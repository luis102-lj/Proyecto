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

    }
}
