using System;
using System.Collections.Generic;
using System.Drawing;

internal class Program
{

    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        int playerX = Console.WindowWidth / 2;  // Posición inicial del jugador en el centro horizontal
        int playerY = Console.WindowHeight / 2; // Posición inicial del jugador en el centro vertical

        int centroX = playerX;
        int centroY = playerY;
        string texto = "O";
        bool gameStart = true;
        bool combate = false;

        //Funcion para crear random
        Random rand = new Random();

        Console.WriteLine("BIENVENIDO A SMALL MONSTERS \npresione ENTER para continuar...");
        SmallMonster smallMongo = new("Mongo", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);
        SmallMonster smallNono = new("Nono", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);
        SmallMonster smallRana = new("Rana", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);
        SmallMonster smallMichelle = new("Michelle", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);
        SmallMonster smallMurciegata = new("Murciegata", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);
        SmallMonster smallCheto = new("Cheto", rand.Next(Console.WindowWidth), rand.Next(Console.WindowHeight), 15, 20, 25);

        // Obtener la lista de todos los SM de forma automática
        List<SmallMonster> listaDeInstancias = SmallMonster.ObtenerTodosLosSM();

        // Inicializar vida personaje principal

        int vida = 100;

        Console.ReadLine();

        while (gameStart)
        {
            Console.Clear();
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(texto);

            // Agregar instancias a la listaDeInstancias

            foreach (SmallMonster instancia in listaDeInstancias)
            {
                Console.SetCursorPosition(instancia.coordenadaX, instancia.coordenadaY);
                Console.Write("X");

                if (playerX==instancia.coordenadaX && playerY == instancia.coordenadaY)
                {
                    Combate(instancia, rand, vida);
                    combate = true;
                }
            }
            if (combate == false)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                // Mover el jugador según la tecla presionada
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        playerY = Math.Max(0, playerY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        playerY = Math.Min(Console.WindowHeight - 1, playerY + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        playerX = Math.Max(0, playerX - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        playerX = Math.Min(Console.WindowWidth - 1, playerX + 1);
                        break;
                }
            }
            combate = false;


        }

        Console.WriteLine("JUEGO FINALIZADO");
        Console.ReadKey();


    }

    public static void Combate(SmallMonster enemigo, Random rand, int vida)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Inicia el Combate con {enemigo.nombre}");
        int vidaTemp = enemigo.vida;
        bool select = false;

        int opcion = 0;
        while ((vidaTemp > 0 && vida > 0 && !(opcion == 3 && select == true)))
        {
            Console.Clear();
            select = false;
            Console.WriteLine("Selecciona Una Opción: ");

            switch (opcion)
            {
                case 0:
                    Console.WriteLine(">Atacar");
                    Console.WriteLine(" Defender");
                    Console.WriteLine(" Esquivar");
                    Console.WriteLine(" Escapar");
                    break;
                case 1:
                    Console.WriteLine(" Atacar");
                    Console.WriteLine(">Defender");
                    Console.WriteLine(" Esquivar");
                    Console.WriteLine(" Escapar");
                    break;
                case 2:
                    Console.WriteLine(" Atacar");
                    Console.WriteLine(" Defender");
                    Console.WriteLine(">Esquivar");
                    Console.WriteLine(" Escapar");
                    break;
                case 3:
                    Console.WriteLine(" Atacar");
                    Console.WriteLine(" Defender");
                    Console.WriteLine(" Esquivar");
                    Console.WriteLine(">Escapar");
                    break;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            // Mover el jugador según la tecla presionada
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    opcion--;
                    if (opcion < 0) { opcion = 0; }
                    break;
                case ConsoleKey.DownArrow:
                    opcion++;
                    if (opcion > 3) { opcion = 3; }
                    break;
                case ConsoleKey.Enter:
                    select = true;
                    break;
            }

            
        }
        Console.Clear();
        Console.WriteLine("COMBATE FINALIZADO\npresione ENTER para continuar");
        enemigo.coordenadaX = rand.Next(Console.WindowWidth);
        enemigo.coordenadaY = rand.Next(Console.WindowHeight);
        Console.ReadLine();
        Console.Clear();
    }
}


class SmallMonster
{
    public string nombre = "muppy";
    public int coordenadaX = 0;
    public int coordenadaY = 0;
    public int ataque = 0;
    public int defensa = 0;
    public int vida = 0;

    private static List<SmallMonster> listaDeSM = new List<SmallMonster>();

    //Constructor
    public SmallMonster(string Nombre, int CoordenadaX, int CoordenadaY, int Ataque, int Defensa, int Vida)
    {
        nombre = Nombre;
        coordenadaX = CoordenadaX;
        coordenadaY = CoordenadaY;
        ataque = Ataque;
        defensa = Defensa;
        vida = Vida;
        listaDeSM.Add(this);
    }

    public void AtaqueGenerico()
    {
        Console.WriteLine($"El enemigo {nombre} te ha atacado con {ataque} puntos de daño");
        Console.ReadLine();
    }

    // Método estático para obtener la lista de todos los Pokemons
    public static List<SmallMonster> ObtenerTodosLosSM()
    {
        return listaDeSM;
    }
}