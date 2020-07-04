using System;
using Proyecto21.Models;
namespace Proyecto21.Naipe
{
    public class Acciones
    {
        //-----------------------Metodos------------------------------//

        /*El metodo consiste en devovler la primera carta del maso*/
        public Carta DemeCartaDeEncima(Models.Naipe naipe)
        {
            Console.WriteLine(string.Format("La carta es {0} de {1}", naipe.elMaso[naipe.getContador()].Numero, naipe.elMaso[naipe.getContador()].ElPalo.ToString()));
            return naipe.elMaso[naipe.getContador()];
        }
        //-----------------------Fin DemeCartaDeEncime()------------------------------//

        /*El metodo consiste en pedir un nuevo maso en caso de que se haya acabado el existente o necesite uno para comenzar*/
        public Models.Naipe DemeNuevoMaso()
        {
            //Modelo.Naipe elNaipe = new Modelo.Naipe();
            var elNaipe = new Models.Naipe();
            Barajar(elNaipe);
            return elNaipe;
        }
        //-----------------------Fin DemeNuevoMaso------------------------------//

        /*Metodo barajar consite en crear dos variables random para poder intercambiar los valores aleatoriamente.
        1)Se crea una variable revolver que va a ser la cantidad de veces que se va a realizar los intercambios 
        aleatorios y la cual se va a aumentar en uno cada vez que se realice un cambio.
        2)Se crean dos numeros aleatorios para realizar los intercambios.
        3)Se crear una carta temporal para guardar la carta1 ahi, luego la informacion de la carta1 se reemplaza
        con la informacio de la carta2, luego la informacion de la carta2 se reemplaza con la informacion de la
        carta temporal 
        4)Aumentar la variable revolver.
      */
        public void Barajar(Models.Naipe naipe)
        {
            Random numeroBuscador = new Random();
            int revolver = 0;
            while (revolver != 52){
                int numeroIntercambiar1 = numeroBuscador.Next(52);
                int numeroIntercambiar2 = numeroBuscador.Next(52);

                Carta CartaTemporal = naipe.elMaso[numeroIntercambiar1];
                naipe.elMaso[numeroIntercambiar1] = naipe.elMaso[numeroIntercambiar2];
                naipe.elMaso[numeroIntercambiar2] = CartaTemporal;
                revolver++;
            }
        }
        //-----------------------Fin Barajar()------------------------------//

        //-----------------------Fin Metodos------------------------------//
    }
}
