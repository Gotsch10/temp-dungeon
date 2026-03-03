using System;

namespace Dungeon;

public class Program
{
    public static void Main()
    {
        Console.Write("Name: ");
        string temp = Console.ReadLine();
        string name = string.IsNullOrWhiteSpace(temp) ? "Held" : temp;

        var held = new Held(name);
        var welt = new Welt("Dunkelwald");

        welt.Enter(held);

        while (!welt.Zielerreicht && held.Health > 0)
        {
            Thread.Sleep(4000);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"HP: {held.Health} | Bewegung: W (Norden), A (Westen), S (Süden), D (Osten) | Q = Quit");
            Console.Write(">> ");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q) break;

            ARaum next = null;

            if (key == ConsoleKey.W)
            {
                next = held.Standort.Norden;
            }
            else if (key == ConsoleKey.S)
            {
                next = held.Standort.Süden;
            }
            else if (key == ConsoleKey.A)
            {
                next = held.Standort.Westen;
            }
            else if (key == ConsoleKey.D)
            {
                next = held.Standort.Osten;
            }

            if (next == null)
            {
                Console.WriteLine("Dort kannst du nicht hin.");
                continue;
            }

            next.Betreten(held, welt);
        }

        Console.WriteLine();

        if (welt.Zielerreicht)
            Console.WriteLine("Spiel beendet: Ziel erreicht!");
        else if (held.Health <= 0)
            Console.WriteLine("Spiel beendet: Du bist gestorben.");
        else
            Console.WriteLine("Spiel beendet.");
    }
}