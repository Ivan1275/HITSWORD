using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego_1
{
    public class Mapa
    {
        public int look;
        public int height;
        public int width;
        public Celda[,] celda;

        public Mapa(int altura, int ancho)
        {
            width = altura;
            height = ancho;
            celda = new Celda[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    celda[i, j] = new Celda();
                }
            }
            Wallmaker();
        }

        public void Wallmaker()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        celda[i,j].tipo = 1;
                    }
                    else if (height <= 10)
                    {
                        if(width >= 40)
                        {
                            if (Static.rng.Next(0, 100) < 20)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                        else
                        {
                            if (Static.rng.Next(0, 100) < 10)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                    }
                    else if (height > 10)
                    {
                        if (width >= 40)
                        {
                            if (Static.rng.Next(0, 100) < 25)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                        else
                        {
                            if (Static.rng.Next(0, 100) < 15)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                    }
                    else if (height > 17)
                    {
                        if (width >= 40)
                        {
                            if (Static.rng.Next(0, 100) < 35)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                        else
                        {
                            if (Static.rng.Next(0, 100) < 30)
                            {
                                celda[i, j].tipo = 1;
                            }
                        }
                    }
                    else
                    {
                        celda[i, j].tipo = 0;
                    }
                    celda[i, j].check = 0;
                }
            }

            //Bitmasking
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    //Bitmasking chequeador
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                if (/*right*/celda[i + 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 2;
                                }
                                if (/*down*/celda[i, j + 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 4;
                                }
                            }
                            else if (j == height - 1)
                            {
                                if (/*arriba*/celda[i, j - 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 1;
                                }
                                if (/*right*/celda[i + 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 2;
                                }
                            }
                            else
                            {
                                if (/*arriba*/celda[i, j - 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 1;
                                }
                                if (/*right*/celda[i + 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 2;
                                }
                                if (/*down*/celda[i, j + 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 4;
                                }
                            }
                        }

                        if (i == width - 1)
                        {
                            if (j == 0)
                            {
                                if (/*down*/celda[i, j + 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 4;
                                }
                                if (/*left*/celda[i - 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 8;
                                }
                            }
                            else if (j == height - 1)
                            {
                                if (/*arriba*/celda[i, j - 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 1;
                                }
                                if (/*left*/celda[i - 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 8;
                                }
                            }
                            else
                            {
                                if (/*arriba*/celda[i, j - 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 1;
                                }
                                if (/*down*/celda[i, j + 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 4;
                                }
                                if (/*left*/celda[i - 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 8;
                                }
                            }
                        }

                        if (j == 0)
                        {
                            if (i == 0)
                            {
                                celda[i, j].check = celda[i, j].check;
                            }
                            else if (i == width - 1)
                            {
                                celda[i, j].check = celda[i, j].check;
                            }
                            else
                            {
                                if (/*right*/celda[i + 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 2;
                                }
                                if (/*down*/celda[i, j + 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 4;
                                }
                                if (/*left*/celda[i - 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 8;
                                }
                            }
                        }

                        if (j == height - 1)
                        {
                            if (i == 0)
                            {
                                celda[i, j].check = celda[i, j].check;
                            }
                            else if (i == width - 1)
                            {
                                celda[i, j].check = celda[i, j].check;
                            }
                            else
                            {
                                if (/*arriba*/celda[i, j - 1].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 1;
                                }
                                if (/*right*/celda[i + 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 2;
                                }
                                if (/*left*/celda[i - 1, j].tipo == 1)
                                {
                                    celda[i, j].check = celda[i, j].check + 8;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (/*arriba*/celda[i, j - 1].tipo == 1)
                        {
                            celda[i, j].check = celda[i, j].check + 1;
                        }
                        if (/*right*/celda[i + 1, j].tipo == 1)
                        {
                            celda[i, j].check = celda[i, j].check + 2;
                        }
                        if (/*down*/celda[i, j + 1].tipo == 1)
                        {
                            celda[i, j].check = celda[i, j].check + 4;
                            // celdas[i,j].check
                        }
                        if (/*left*/celda[i - 1, j].tipo == 1)
                        {
                            celda[i, j].check = celda[i, j].check + 8;
                        }
                    } 
                }
            }
            /*Todos los tipo = 1 y q son check 0 (sin muros alrededor) los creo en suelo
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (celda[i, j].check == 0)
                    {
                        celda[i, j].tipo = 2;
                    }
                }
            }*/
        }

        public void Dibuja()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.SetCursorPosition(i, j);

                    string muro = "█╨╞╚╥║╔╠╡╝═╩╗╣╦╬";

                    if (celda[i, j].tipo == 1)
                    {
                        if (celda[i, j].check == 0)
                        {
                            if (look >= 0)
                            {
                                celda[i, j].tipo = 0;
                            }
                            if (look == 0)
                            {
                                look++;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("m");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else
                        {
                            Console.WriteLine(muro[celda[i, j].check]);
                        }
                    }
                }
            }
        }
    }
}