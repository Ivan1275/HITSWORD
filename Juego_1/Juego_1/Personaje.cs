using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego_1
{
    public abstract class Personaje : IPersonaje
    {
        public int x;
        public int y;
        public int turnogn;
        public int turnojug;

        public Mapa mapa;

        public Personaje(Mapa mapa)
        {
            this.mapa = mapa;
        }

        public void Respawn()
        {
            //si su posicion no gusta(es muro, hay bonificacion...)
            do
            {
                x = Static.rng.Next(1, mapa.width);
                y = Static.rng.Next(1, mapa.height);
            } while (mapa.celda[x, y].tipo == 1);
        }

        public void Mueve()
        {

        }
        public virtual void Dibuja()
        {

        }
    }
}