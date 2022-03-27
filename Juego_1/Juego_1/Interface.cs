using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego_1
{
    public class Interface
    {
        public int puntos = 0;
        public int puntostotales = 0;
        public int nivel = 1;
        public int vidatotal;
        public int vida;
        public int tipo;
        public string nombrejug;

        public Mapa mapa;
        public Interface(Mapa mapa)
        {
            this.mapa = mapa;
        }
        public virtual void Dibuja()
        {
            //Muestra Nombre del jugador
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, mapa.height);
            Console.Write(nombrejug);
            Console.ForegroundColor = ConsoleColor.White;

            //Muestra Puntos
            Console.SetCursorPosition(0, mapa.height + 1);
            Console.Write("Puntos: ");
            for (int i = 0; i < puntostotales; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("♠");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //Muestra Nivel
            Console.SetCursorPosition(0, mapa.height + 2);
            Console.Write("NIVEL:" + nivel);

            //Muestra Vida
            Console.SetCursorPosition(0, mapa.height + 3);
            Console.Write("Vida ");
            for(int i = 0; i < vidatotal; i++)
            {
                if(i < vida)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("█");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.SetCursorPosition(0, mapa.height + 4);
            if(tipo == 0)
            {
                Console.Write($"Tipo de combate:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Pelea");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (tipo == 1)
            {
                Console.Write($"Tipo de combate:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Defns");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                
                Console.Write($"Tipo de combate:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Picar");
                Console.ForegroundColor = ConsoleColor.White;
            }

            //Muestra Nivel
            Console.SetCursorPosition(0, mapa.height + 5);
            Console.Write("Tus turnos:" + Static.turnojug + " / Turnos generales: " + Static.turnogn);
        }
    }
}