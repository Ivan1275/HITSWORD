using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego_1
{
    public class Player : Personaje
    {
        public int fullvida = 10;
        public int vida;
        public int subturno;
        public int borrax;
        public int borray;
        public bool muerto;
        public string nombre;
        //public List<Mochila> animales = new List<Mochila>();

        public Player(Mapa map) : base(map)
        {
            vida = fullvida;
            muerto = false;
        }

        public override void Dibuja()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write("☻");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Mueve(int muevex, int muevey)
        {
            if (mapa.celda[x + muevex, y + muevey].tipo != 1)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(' ');

                Static.turnojug++;

                this.x = x + muevex;
                this.y = y + muevey;
            }
        }
        public void Moverse(ConsoleKey tecla)
        {
            switch (tecla)
            {
                case ConsoleKey.UpArrow:
                    Mueve(0, -1);
                    break;
                case ConsoleKey.RightArrow:
                    Mueve(1, 0);
                    break;
                case ConsoleKey.DownArrow:
                    Mueve(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    Mueve(-1, 0);
                    break;
            }
        }

        public void UseTool(int muevex, int muevey, int tipo)
        {
            if(tipo == 0) //Ataca
            {
                if (mapa.celda[x + muevex, y + muevey].tipo != 1)
                {
                
                    if (muevey == -1)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        Console.Write('▲');
                        borrax = x;
                        borray = y - 1;
                    } 
                    else if (muevex == 1)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        Console.Write('►');
                        borrax = x + 1;
                        borray = y;
                    }
                    else if (muevey == 1)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        Console.Write('▼');
                        borrax = x;
                        borray = y + 1;
                    }
                    else if (muevex == -1)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write('◄');
                        borrax = x - 1;
                        borray = y;
                    }

                    subturno = Static.turnogn;
                    
                    turnojug++;
                }
            }
            
            if (tipo == 1) //Defiende
            {
                if (mapa.celda[x + muevex, y + muevey].tipo == 0)
                {

                    if (muevey == -1)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        mapa.celda[x, y - 1].tipo = 1;
                        Console.Write('─');
                    }
                    else if (muevex == 1)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        mapa.celda[x + 1, y].tipo = 1;
                        Console.Write('|');
                    }
                    else if (muevey == 1)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        mapa.celda[x, y + 1].tipo = 1;
                        Console.Write('─');
                    }
                    else if (muevex == -1)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        mapa.celda[x - 1, y].tipo = 1;
                        Console.Write('|');
                    }

                    turnojug++;
                }
            }
            
            if (tipo == 2) //Pico
            {
                if (mapa.celda[x + muevex, y + muevey].tipo == 1)
                {

                    if (muevey == -1)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        mapa.celda[x, y - 1].tipo = 0;
                        Console.Write('†');
                        borrax = x;
                        borray = y - 1;
                    }
                    else if (muevex == 1)
                    {
                        Console.SetCursorPosition(x + 1, y);
                        mapa.celda[x + 1, y].tipo = 0;
                        Console.Write('†');
                        borrax = x + 1;
                        borray = y;
                    }
                    else if (muevey == 1)
                    {
                        Console.SetCursorPosition(x, y + 1);
                        mapa.celda[x, y + 1].tipo = 0;
                        Console.Write('†');
                        borrax = x;
                        borray = y + 1;
                    }
                    else if (muevex == -1)
                    {
                        Console.SetCursorPosition(x - 1, y);
                        mapa.celda[x - 1, y].tipo = 0;
                        Console.Write('†');
                        borrax = x - 1;
                        borray = y;
                    }

                    subturno = Static.turnogn;

                    turnojug++;
                }
            }
        }
        public void Atacar_Defender(ConsoleKey tecla, int tipo)
        {
            if (tipo == 0)
            {
                switch (tecla)
                {
                    case ConsoleKey.W:
                        UseTool(0, -1, tipo);
                        break;
                    case ConsoleKey.D:
                        UseTool(1, 0, tipo);
                        break;
                    case ConsoleKey.S:
                        UseTool(0, 1, tipo);
                        break;
                    case ConsoleKey.A:
                        UseTool(-1, 0, tipo);
                        break;
                }
            } 
            else if(tipo == 1)
            {
                switch (tecla)
                {
                    case ConsoleKey.W:
                        UseTool(0, -1, tipo);
                        break;
                    case ConsoleKey.D:
                        UseTool(1, 0, tipo);
                        break;
                    case ConsoleKey.S:
                        UseTool(0, 1, tipo);
                        break;
                    case ConsoleKey.A:
                        UseTool(-1, 0, tipo);
                        break;
                }
            }
            else if (tipo == 2)
            {
                switch (tecla)
                {
                    case ConsoleKey.W:
                        UseTool(0, -1, tipo);
                        break;
                    case ConsoleKey.D:
                        UseTool(1, 0, tipo);
                        break;
                    case ConsoleKey.S:
                        UseTool(0, 1, tipo);
                        break;
                    case ConsoleKey.A:
                        UseTool(-1, 0, tipo);
                        break;
                }
            }
        }
    }
}