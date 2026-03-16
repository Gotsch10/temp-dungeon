using System;

namespace Dungeon;

public class Welt
{
    public string Name { get; }
    public bool Zielerreicht = false;
    private ARaum _start;
    private ARaum[,] _karte;

    public Welt(string name)
    {
        Name = name;
        Erschaffen();
    }

    public void Enter(Held held)
    {
        held.welt = this;
        _start.Betreten(held, this);
    }

    public void Ziel()
    {
        if (Zielerreicht == true)
        {
            Environment.Exit(0);
        }
    }

    public void ZeigeKarte(Held held)
    {
        for (int y = 0; y < _karte.GetLength(0); y++)
        {
            for (int x = 0; x < _karte.GetLength(1); x++)
            {
                ARaum raum = _karte[y, x];

                if (raum == held.Standort)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{raum.Symbol} ");
                    Console.ResetColor();
                }
                else if (raum.Erkundet)
                {
                    Console.Write($"{raum.Symbol} ");
                }
                else
                {
                    Console.Write("X ");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void Erschaffen()
    {
        Random rnd = new Random();

        _karte = new ARaum[10, 10];

        int startY = 9;
        int startX = 0;

        int zielY;
        int zielX;

        do
        {
            zielY = rnd.Next(0, 10);
            zielX = rnd.Next(0, 10);
        }
        while (zielY == startY && zielX == startX);

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (y == startY && x == startX)
                {
                    _karte[y, x] = new StartRaum();
                }
                else if (y == zielY && x == zielX)
                {
                    _karte[y, x] = new ZielRaum();
                }
                else
                {
                    int random = rnd.Next(1, 11);

                    if (random <= 5)
                    {
                        _karte[y, x] = new LeererRaum();
                    }
                    else if (random <= 8)
                    {
                        _karte[y, x] = new FallenRaum();
                    }
                    else if (random == 9)
                    {
                        _karte[y, x] = new Schatzraum(Schaetze.ESchatz.Krone);
                    }
                    else
                    {
                        _karte[y, x] = new Schatzraum(Schaetze.ESchatz.Trank);
                    }
                }
            }
        }

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (y > 0)
                {
                    _karte[y, x].Norden = _karte[y - 1, x];
                }

                if (y < 9)
                {
                    _karte[y, x].Süden = _karte[y + 1, x];
                }

                if (x > 0)
                {
                    _karte[y, x].Westen = _karte[y, x - 1];
                }

                if (x < 9)
                {
                    _karte[y, x].Osten = _karte[y, x + 1];
                }
            }
        }

        _start = _karte[startY, startX];
    }
}