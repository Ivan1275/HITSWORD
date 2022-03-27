using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juego_1
{
    public class GameManager
    {
        string filename = @".\data\save.txt";

        private Mapa map;
        public Interface interfaz;
        public Player jugador;
        public List<Enemigo> ejercito;

        int fight = 0;
        string nombreprota;
        bool gameover = false;

        public GameManager()
        {
            //Clase Mapa
            map = new Mapa(25, 25);

            //Clase Interface
            interfaz = new Interface(map);

            //Array enemigo
            ejercito = new List<Enemigo>();

            //Clase Player
            jugador = new Player(map);

            //Construyo enemigo
            for (int i = 0; i < 5; i++)
            {
                ejercito.Add(new Enemigo(map));
            }

            //Abre el menu, que iniciara la partida nueva o cargada
            do
            {
                Menu();
            } while (Console.ReadKey(true).Key != ConsoleKey.C && Console.ReadKey(true).Key != ConsoleKey.X && Console.ReadKey(true).Key != ConsoleKey.Z);
        }

        public void GameLoop()
        {
            ConsoleKey tecla;

            jugador.nombre = nombreprota;
            interfaz.nombrejug = jugador.nombre;
            interfaz.vidatotal = jugador.fullvida;

            do
            {
                do
                {
                    //dibuja la interfaz, el jugador y enemigos si el gameover no esta activo
                    if (gameover == false)
                    {
                        //interfaz provisional
                        interfaz.Dibuja();

                        //dibuja el jugador
                        jugador.Dibuja();

                        //dibuja los enemigos
                        for (int i = 0; i < ejercito.Count; i++)
                        {
                            ejercito[i].Dibuja();
                        }
                    }

                    interfaz.vida = jugador.vida;
                    interfaz.tipo = fight;

                    tecla = Console.ReadKey(true).Key;

                    //Funcion mover jugador 
                    jugador.Moverse(tecla);
                    jugador.Atacar_Defender(tecla, fight);

                    //Funciones del enemigo
                    for (int i = 0; i < ejercito.Count; i++)
                    {
                        //Funcion mover enemigo
                        if (ejercito[i].x > jugador.x)
                        {
                            ejercito[i].Mueve(-1, 0);
                        }
                        if (ejercito[i].x < jugador.x)
                        {
                            ejercito[i].Mueve(1, 0);
                        }
                        if (ejercito[i].y > jugador.y)
                        {
                            ejercito[i].Mueve(0, -1);
                        }
                        if (ejercito[i].y < jugador.y)
                        {
                            ejercito[i].Mueve(0, 1);
                        }

                        //Resta vida cuando el enemigo te capture
                        if (EnemigoPega(ejercito[i]))
                        {
                            ejercito[i].Respawn();
                            jugador.vida--;

                            if (jugador.vida == 0)
                            {
                                jugador.muerto = true;
                                gameover = true;
                            }
                        }

                        //Matas al enemigo cuando tu espada lo alcance
                        if (Espada(ejercito[i], tecla))
                        {
                            ejercito[i].Respawn();
                            interfaz.puntostotales++;
                            interfaz.puntos++;
                        }
                    }

                    //Acciones con botones
                    switch (tecla)
                    {
                        case ConsoleKey.R:
                            ReiniciaMapa();
                            break;
                        case ConsoleKey.Escape:
                            gameover = true;
                            break;
                        case ConsoleKey.Q:
                            CambioArma();
                            Static.turnojug++;
                            break;
                    }

                    //
                    if (jugador.subturno == Static.turnogn - 1)
                    {
                        Console.SetCursorPosition(jugador.borrax, jugador.borray);
                        Console.Write(' ');
                    }
                    
                    //Subir nivel cuando consigas 7 puntos
                    if (interfaz.puntos == 7)
                    {
                        interfaz.puntos = 0;
                        interfaz.nivel++;
                        ReiniciaMapa();
                    }

                    Static.turnogn++;

                } while (tecla != ConsoleKey.Escape && !jugador.muerto && gameover == false && interfaz.nivel > 5);

                AcabaPartida();

            } while (tecla != ConsoleKey.X && tecla != ConsoleKey.C && tecla != ConsoleKey.Z);
        }

        public bool EnemigoPega(Enemigo EsteEnemigo)
        {
            if (jugador.x == EsteEnemigo.x && jugador.y == EsteEnemigo.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CambioArma()
        {
            if(fight < 2)
            {
                fight++;
            }
            else
            {
                fight = 0;
            }
        }

        public bool Espada(Enemigo EsteEnemigo, ConsoleKey tecla)
        {
            if (jugador.x == EsteEnemigo.x && jugador.y - 1 == EsteEnemigo.y && tecla == ConsoleKey.W)
            {
                return true;
            }
            else if (jugador.x + 1 == EsteEnemigo.x && jugador.y == EsteEnemigo.y && tecla == ConsoleKey.D)
            {
                return true;
            }
            else if (jugador.x == EsteEnemigo.x && jugador.y + 1 == EsteEnemigo.y && tecla == ConsoleKey.S)
            {
                return true;
            }
            else if (jugador.x - 1 == EsteEnemigo.x && jugador.y == EsteEnemigo.y && tecla == ConsoleKey.A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReiniciaPartida()
        {
            jugador.vida = jugador.fullvida;
            jugador.muerto = false;
            interfaz.puntos = 0;
            interfaz.puntostotales = 0;
            interfaz.nivel = 1;
            ReiniciaMapa();
        }

        public void ReiniciaMapa()
        {
            Console.Clear();

            map = new Mapa(Static.rng.Next(10, 100), Static.rng.Next(10, 25));

            interfaz.mapa = this.map;

            jugador.mapa = this.map;

            for (int i = 0; i < ejercito.Count; i++)
            {
                ejercito[i].mapa = this.map;
            }
            // redibuja el mapa
            map.Wallmaker();

            // posicionamos el jugador
            jugador.Respawn();

            //posicionamos los enemigos
            for (int i = 0; i < ejercito.Count; i++)
            {
                ejercito[i].Respawn();
            }
            map.Dibuja();
        }

        public void AcabaPartida()
        {
            Console.Clear();

            Console.WriteLine(@"
             ██████╗  █████╗ ███╗   ███╗███████╗   █████╗ ██╗   ██╗███████╗██████╗ 
            ██╔════╝ ██╔══██╗████╗ ████║██╔════╝  ██╔══██╗██║   ██║██╔════╝██╔══██╗
            ██║  ██╗ ███████║██╔████╔██║█████╗    ██║  ██║╚██╗ ██╔╝█████╗  ██████╔╝
            ██║  ╚██╗██╔══██║██║╚██╔╝██║██╔══╝    ██║  ██║ ╚████╔╝ ██╔══╝  ██╔══██╗
            ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗  ╚█████╔╝  ╚██╔╝  ███████╗██║  ██║
             ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝   ╚════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝

            Has muerto o salido de la partida
            Si te has salido sin querer y deseas continuar tu partida, pulsa:
            tecla -> C
            Si deseas salir del juego, pulsa:
            tecla -> X
            Si quieres volver a jugar una partida porque has muerto o deseas reiniciarla pulsa:
            tecla -> Z
            Si deseas guardar la partida, pulsa:
            tecla -> G
            ");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.C:
                    Console.Clear();
                    gameover = false;
                    map.Dibuja();
                    GameLoop();
                    break;
                case ConsoleKey.X:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Z:
                    Nombre();
                    Console.Clear();
                    gameover = false;
                    ReiniciaPartida();
                    GameLoop();
                    break;
                case ConsoleKey.G:
                    Descargar();
                    Console.WriteLine("TU PARTIDA HA SIDO GUARDADA CON EXITO!!");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.WriteLine(@"
            █▀▀ █▀▀ █▀▀█ 　 ▀▀█▀▀ █▀▀ █▀▀ █   █▀▀█ 　 █▀▀▄ █▀▀█ 　 █▀▀ █▀▀ 　 ▀█ █▀ █▀▀█ █    ▀  █▀▀▄ █▀▀█ 
            █▀▀ ▀▀█ █▄▄█ 　   █   █▀▀ █   █   █▄▄█ 　 █  █ █  █ 　 █▀▀ ▀▀█ 　  █▄█  █▄▄█ █   ▀█▀ █  █ █▄▄█ 
            ▀▀▀ ▀▀▀ ▀  ▀ 　   ▀   ▀▀▀ ▀▀▀ ▀▀▀ ▀  ▀ 　 ▀  ▀ ▀▀▀▀ 　 ▀▀▀ ▀▀▀ 　   ▀   ▀  ▀ ▀▀▀ ▀▀▀ ▀▀▀  ▀  ▀
            ");
                    break;
            }
        }

        public void Menu()
        {
            Console.Clear();
            
            Console.WriteLine(@"

            ██████╗ ██╗███████╗███╗  ██╗██╗   ██╗███████╗███╗  ██╗██╗██████╗  █████╗    █████╗ 
            ██╔══██╗██║██╔════╝████╗ ██║██║   ██║██╔════╝████╗ ██║██║██╔══██╗██╔══██╗  ██╔══██╗
            ██████╦╝██║█████╗  ██╔██╗██║╚██╗ ██╔╝█████╗  ██╔██╗██║██║██║  ██║██║  ██║  ███████║
            ██╔══██╗██║██╔══╝  ██║╚████║ ╚████╔╝ ██╔══╝  ██║╚████║██║██║  ██║██║  ██║  ██╔══██║
            ██████╦╝██║███████╗██║ ╚███║  ╚██╔╝  ███████╗██║ ╚███║██║██████╔╝╚█████╔╝  ██║  ██║
            ╚═════╝ ╚═╝╚══════╝╚═╝  ╚══╝   ╚═╝   ╚══════╝╚═╝  ╚══╝╚═╝╚═════╝  ╚════╝   ╚═╝  ╚═╝

                    ██╗  ██╗██╗████████╗ ██████╗ ██╗       ██╗ █████╗ ██████╗ ██████╗ 
                    ██║  ██║██║╚══██╔══╝██╔════╝ ██║  ██╗  ██║██╔══██╗██╔══██╗██╔══██╗
                    ███████║██║   ██║   ╚█████╗  ╚██╗████╗██╔╝██║  ██║██████╔╝██║  ██║
                    ██╔══██║██║   ██║    ╚═══██╗  ████╔═████║ ██║  ██║██╔══██╗██║  ██║
                    ██║  ██║██║   ██║   ██████╔╝  ╚██╔╝ ╚██╔╝ ╚█████╔╝██║  ██║██████╔╝
                    ╚═╝  ╚═╝╚═╝   ╚═╝   ╚═════╝    ╚═╝   ╚═╝   ╚════╝ ╚═╝  ╚═╝╚═════╝ 

            Inicia una nueva partida o carga tu partida
            Si quieres cargar una partida, pulsa:
            tecla -> C
            Si deseas salir del juego, pulsa:
            tecla -> X
            Para iniciar una nueva partida, pulsa:
            tecla -> Z
            ");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.C:
                    Cargar();
                    Console.WriteLine("CARGANDO PARTIDA...");
                    Thread.Sleep(3000);
                    ReiniciaMapa();
                    GameLoop();
                    break;
                case ConsoleKey.X:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Z:
                    Nombre();
                    ReiniciaPartida();
                    GameLoop();
                    break;
                default:
                    Console.WriteLine(@"
            █▀▀ █▀▀ █▀▀█ 　 ▀▀█▀▀ █▀▀ █▀▀ █   █▀▀█ 　 █▀▀▄ █▀▀█ 　 █▀▀ █▀▀ 　 ▀█ █▀ █▀▀█ █    ▀  █▀▀▄ █▀▀█ 
            █▀▀ ▀▀█ █▄▄█ 　   █   █▀▀ █   █   █▄▄█ 　 █  █ █  █ 　 █▀▀ ▀▀█ 　  █▄█  █▄▄█ █   ▀█▀ █  █ █▄▄█ 
            ▀▀▀ ▀▀▀ ▀  ▀ 　   ▀   ▀▀▀ ▀▀▀ ▀▀▀ ▀  ▀ 　 ▀  ▀ ▀▀▀▀ 　 ▀▀▀ ▀▀▀ 　   ▀   ▀  ▀ ▀▀▀ ▀▀▀ ▀▀▀  ▀  ▀
            ");
                    break;
            }
        }

        public void Nombre()
        {
            Console.WriteLine("Indique su nombre");

            nombreprota = Console.ReadLine();
        }

        public void Cargar()
        {
            StreamReader archivo = new(filename);
            String texto = archivo.ReadToEnd();
            archivo.Close();

            string Nombre;
            int Vida;
            int Puntos;
            int Nivel;

            string[] palabras = texto.Split(",");

            Nombre = palabras[0];
            nombreprota = Nombre;
            Vida = int.Parse(palabras[1]);
            jugador.vida = Vida;
            Puntos = int.Parse(palabras[2]);
            interfaz.puntostotales = Puntos;
            Nivel = int.Parse(palabras[3]);
            interfaz.nivel = Nivel;
        }

        public void Descargar()
        {
            StreamWriter salida = new(filename);

            salida.WriteLine($"{jugador.nombre},{jugador.vida},{interfaz.puntostotales},{interfaz.nivel}");

            salida.Close();
        }
    }
}