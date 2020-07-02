using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto21.Models
{
    public class Naipe
    {

        //Variables--------------------------------------------------------------------
        private int _contador = 0;
        private Carta[] _elMaso = new Carta[52];
        public Carta[] elMaso
        {
            get { return _elMaso; }
            set { }
        }



        //Constructores-----------------------------------------------------------------
        public Naipe()
        {
            Carta[] elResultado = new Carta[52];
            int contador = 0;
            CrearCartasDelPalo(ref contador, ref elResultado, Palo.Corazones);
            CrearCartasDelPalo(ref contador, ref elResultado, Palo.Espadas);
            CrearCartasDelPalo(ref contador, ref elResultado, Palo.Diamantes);
            CrearCartasDelPalo(ref contador, ref elResultado, Palo.Treboles);
            elResultado.CopyTo(elMaso, 0);
        }


        //Metodos-------------------------------------------------------------------------
        private void CrearCartasDelPalo(ref int contador, ref Carta[] elResultado, Palo elPalo)
        {
            elResultado[contador++] = new Carta("A", elPalo);
            for (int i = 2; i <= 10; i++)
            {
                elResultado[contador++] = new Carta(i, elPalo);
            }
            elResultado[contador++] = new Carta("J", elPalo);
            elResultado[contador++] = new Carta("Q", elPalo);
            elResultado[contador++] = new Carta("K", elPalo);
        }

        public int getContador()
        {
            return _contador;
        }

    }
}
