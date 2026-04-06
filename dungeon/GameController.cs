using System.Threading;

namespace Dungeon;

public class GameController
{
    private Welt _welt;
    private Held _held;

    public GameController(string heldenName, string weltName)
    {
        _held = new Held(heldenName);

        Console.Clear();
        Console.WriteLine("Map auswählen:");
        Console.WriteLine("1 = Vorgebaute Map");
        Console.WriteLine("2 = Random Map");
        Console.Write(">> ");

        string mapAuswahl = Console.ReadLine();

        if (mapAuswahl == "1")
        {
            Console.Clear();
            Console.WriteLine("Welche Welt willst du?");
            Console.WriteLine("1 = level1");
            Console.WriteLine("2 = level2");
            Console.WriteLine("3 = level3");
            Console.Write(">> ");

            string weltAuswahl = Console.ReadLine();
            string dateiPfad;

            if (weltAuswahl == "1")
            {
                dateiPfad = "C:\\Users\\Gotsc\\RiderProjects\\dungeon-teil-1-Gotsch10\\dungeon\\level1.txt";
            }
            else if (weltAuswahl == "2")
            {
                dateiPfad = "C:\\Users\\Gotsc\\RiderProjects\\dungeon-teil-1-Gotsch10\\dungeon\\level2.txt";
            }
            else if (weltAuswahl == "3")
            {
                dateiPfad = "C:\\Users\\Gotsc\\RiderProjects\\dungeon-teil-1-Gotsch10\\dungeon\\level3.txt";
            }
            else
            {
                dateiPfad = "C:\\Users\\Gotsc\\RiderProjects\\dungeon-teil-1-Gotsch10\\dungeon\\level1.txt";
            }

            _welt = new Welt(weltName, dateiPfad);
        }
        else
        {
            _welt = new Welt(weltName);
        }

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
                break;

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

            if (!_welt.Zielerreicht && _held.Health > 0)
            {
                Thread.Sleep(2000);
            }
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