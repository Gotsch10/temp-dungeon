using System.Threading;

namespace Dungeon;

public class GameController
{
    private Welt _welt;
    private Held _held;

    public GameController(string heldenName, string weltName)
    {
        _held = new Held(heldenName);
        _welt = new Welt(weltName);
        _welt.Enter(_held);
    }

    public void Run()
    {
        while (!_welt.Zielerreicht && _held.Health > 0)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine($"HP: {_held.Health} | Bewegung: W (Norden), A (Westen), S (Süden), D (Osten) | Q = Quit");
            Console.WriteLine();

            _welt.ZeigeKarte(_held);

            Console.Write(">> ");

            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }
                

            ARaum next = null;

            if (key == ConsoleKey.W)
            {
                next = _held.Standort.Norden;
            }
            else if (key == ConsoleKey.S)
            {
                next = _held.Standort.Süden;
            }
            else if (key == ConsoleKey.A)
            {
                next = _held.Standort.Westen;
            }
            else if (key == ConsoleKey.D)
            {
                next = _held.Standort.Osten;
            }

            if (next == null)
            {
                Console.WriteLine("Dort kannst du nicht hin.");
                Thread.Sleep(1000);
                continue;
            }

            next.Betreten(_held, _welt);
            
            Thread.Sleep(2500);
        }

        Console.Clear();

        Console.WriteLine();
        Console.WriteLine($"HP: {_held.Health} | Bewegung: W (Norden), A (Westen), S (Süden), D (Osten) | Q = Quit");
        Console.WriteLine();

        if (_held.Health <= 0 && !_welt.Zielerreicht)
        {
            _welt.ZeigeKarte(_held, true);
        }
        else
        {
            _welt.ZeigeKarte(_held);
        }

        if (_welt.Zielerreicht)
            Console.WriteLine("Spiel beendet: Ziel erreicht!");
        else if (_held.Health <= 0)
            Console.WriteLine("Spiel beendet: Du bist gestorben.");
        else
            Console.WriteLine("Spiel beendet.");
    }
}