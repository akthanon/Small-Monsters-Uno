using System;
using System.Collections.Generic;
using System.Drawing;

internal class Program
{
    // Método principal que inicia el juego
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

        // Mensaje de bienvenida y creación de instancias de SmallMonster
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
        int vida = 50;
        int ataque = 10;
        int defensa = 20;

        Console.ReadLine();

        while (gameStart)
        {
            Console.Clear();
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(texto);

            // Mostrar instancias de SmallMonster en la consola
            foreach (SmallMonster instancia in listaDeInstancias)
            {
                Console.SetCursorPosition(instancia.coordenadaX, instancia.coordenadaY);
                Console.Write("X");

                // Iniciar combate si el jugador se encuentra en la misma posición que una instancia de SmallMonster
                if (playerX == instancia.coordenadaX && playerY == instancia.coordenadaY)
                {
                    Combate(instancia, rand, vida, ataque, defensa);
                    combate = true;
                }
            }

            // Mover al jugador si no está en combate
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

    // Método que controla el combate
    public static void Combate(SmallMonster enemigo, Random rand, int vida, int ataque, int defensa)
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine($"Inicia el Combate con {enemigo.nombre}");
        int vidaTemp = enemigo.vida;
        int mainVida = vida;
        bool select = false;
        int dano;

        int opcion = 0;
        while (vidaTemp > 0 && mainVida > 0 && !(opcion == 3 && select == true))
        {
            // Mostrar opciones de combate y la representación gráfica del jugador
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

            // Mostrar la representación gráfica del jugador durante el combate
            string face = "  n n   \n\t\t\t<(^W^)>\n\t\t\t  V V   ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n\n\t\t\t{face}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"(T_T)");
            Console.WriteLine("  O");
            Console.WriteLine("**O**");
            Console.WriteLine("* O *");
            Console.WriteLine("  O  ");
            Console.WriteLine(" O O ");
            Console.WriteLine(" O O ");

            // Leer la tecla presionada por el usuario
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            // Mover el cursor según la tecla presionada
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

            Console.WriteLine("\n");
            if (opcion == 0 && select == true)
            {
                // Realizar un ataque
                Console.WriteLine($"Haz Atacado al enemigo {enemigo.nombre}");
                Console.ReadLine();
                dano = rand.Next(ataque) * (1 - enemigo.defensa / 100);
                vidaTemp -= dano;

                if (vidaTemp < 0) { vidaTemp = 0; }

                Console.WriteLine($"Le quitas {dano} de vida al enemigo {enemigo.nombre}, le quedan {vidaTemp} puntos de vida");
                Console.ReadLine();

                if (vidaTemp > 0) { dano = enemigo.AtaqueGenerico(defensa); mainVida = mainVida - dano; }
                if (mainVida < 0) { mainVida = 0; }
                Console.WriteLine($"Te quedan {mainVida} puntos de vida");
                Console.ReadLine();
            }
            else if (opcion == 1 && select == true)
            {
                // Realizar una defensa
                Console.WriteLine($"Te estás preparando para defenderte del ataque enemigo {enemigo.nombre}");
                Console.ReadLine();
                if (vidaTemp > 0) { dano = enemigo.AtaqueDefensa(defensa); mainVida = mainVida - dano; }

                if (mainVida < 0) { mainVida = 0; }
                Console.WriteLine($"Te haz defendido del ataque, ahora te quedan {mainVida} puntos de vida");

                Console.ReadLine();
            }
            else if (opcion == 2 && select == true)
            {
                // Realizar una esquiva
                Console.WriteLine($"Te estás preparando para esquivar el ataque de {enemigo.nombre}");
                Console.ReadLine();
                if (rand.Next(6) == 1)
                {
                    Console.WriteLine("Te han acertado el ataque");
                    Console.ReadLine();
                    if (vidaTemp > 0) { dano = enemigo.AtaqueGenerico(0); mainVida = mainVida - dano; }
                    if (mainVida < 0) { mainVida = 0; }
                    Console.WriteLine($"Te quedan {mainVida} puntos de vida");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("El enemigo ha fallado el ataque, aprovechas para recuperar 1 punto de vida");
                    mainVida++;
                    Console.ReadLine();
                    if (mainVida < 0) { mainVida = 0; }
                    Console.WriteLine($"Ahora tienes {mainVida} puntos de vida");
                    Console.ReadLine();
                }
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

// Clase que representa a un pequeño monstruo
class SmallMonster
{
    public string nombre = "muppy";
    public int coordenadaX = 0;
    public int coordenadaY = 0;
    public int ataque = 0;
    public int defensa = 0;
    public int vida = 0;

    private static List<SmallMonster> listaDeSM = new List<SmallMonster>();

    Random rand = new Random();

    // Constructor
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

    // Método que simula un ataque genérico del monstruo
    public int AtaqueGenerico(int defensaEnemigo)
    {
        int dano = (int)(rand.Next(ataque) * (1 - Convert.ToDouble((defensaEnemigo) / 100.0)));
        Console.WriteLine($"El enemigo {nombre} te ha atacado con {dano} puntos de daño");
        Console.ReadLine();
        return dano;
    }

    // Método que simula un ataque con posibilidad de defensa
    public int AtaqueDefensa(int defensaEnemigo)
    {
        int dano = (int)(rand.Next(ataque) * (1 - Convert.ToDouble(rand.Next(0, 200) / 100.0)));
        Console.WriteLine($"El enemigo {nombre} te ha atacado con {dano} puntos de daño");

        if (dano < 0)
        {
            Console.ReadLine();
            Console.Write("Le robas la energía a tu enemigo y te recuperas vida\n");
        }
        Console.ReadLine();
        return dano;
    }

    // Método estático para obtener la lista de todos los SmallMonsters
    public static List<SmallMonster> ObtenerTodosLosSM()
    {
        return listaDeSM;
    }
}
