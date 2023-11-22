using System;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.CursorVisible = false;
        int playerX = Console.WindowWidth / 2;  // Posición inicial del jugador en el centro horizontal
        int playerY = Console.WindowHeight / 2; // Posición inicial del jugador en el centro vertical
        string texto = "O";
        bool gameStart = true;

        Console.WriteLine("JUEGO INICIADO");
        SmallMonster smallmonster = new("Mongo", 10, 10, 15, 20, 25);
        Console.ReadLine();
        smallmonster.AtaqueGenerico();
        Console.ReadLine();

        while (gameStart)
        {
            Console.Clear();
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(texto);

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

        Console.WriteLine("JUEGO FINALIZADO");
        Console.ReadKey();
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

    //Constructor
    public SmallMonster(string Nombre, int CoordenadaX, int CoordenadaY, int Ataque, int Defensa, int Vida)
    {
        nombre = Nombre;
        coordenadaX = CoordenadaX;
        coordenadaY = CoordenadaY;
        ataque = Ataque;
        int defensa = Defensa;
        int vida = Vida;
    }

    public void AtaqueGenerico()
    {
        Console.WriteLine($"El enemigo {nombre} te ha atacado con {ataque} puntos de daño");
        Console.ReadLine();
    }
}