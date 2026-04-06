using System;
using System.Collections.Generic;
using System.IO;

namespace Dungeon;

public class Welt
{
    public string Name { get; }
    public bool Zielerreicht = false;
    private ARaum _start;
    private ARaum[,] _karte;

    private bool _randomMap = true;
    private string _dateipfad = "";

    public Welt(string name)
    {
        Name = name;
        _randomMap = true;
        Erschaffen();
    }

    public Welt(string name, string dateipfad)
    {
        Name = name;
        _dateipfad = dateipfad;
        _randomMap = false;
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

    public void ZeigeKarte(Held held, bool zielAnzeigen = false)
    {
        for (int y = 0; y < _karte.GetLength(0); y++)
        {
            for (int x = 0; x < _karte.GetLength(1); x++)
            {
                ARaum raum = _karte[y, x];

                if (raum == null)
                {
                    Console.Write("  ");
                }
                else if (raum == held.Standort)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{raum.Symbol} ");
                    Console.ResetColor();
                }
                else if (zielAnzeigen && raum is ZielRaum)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
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
        if (_randomMap)
        {
            ErschaffenRandom();
        }
        else
        {
            ErschaffenAusDatei();
        }
    }

    private void ErschaffenRandom()
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

    private void ErschaffenAusDatei()
    {
        Dictionary<string, ARaum> raeume = new Dictionary<string, ARaum>();
        List<(string von, string richtung, string nach)> verbindungen = new List<(string von, string richtung, string nach)>();

        int startAnzahl = 0;

        using (StreamReader sr = new StreamReader(_dateipfad))
        {
            bool verbindungenLesen = false;

            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();

                if (string.IsNullOrWhiteSpace(zeile))
                {
                    verbindungenLesen = true;
                    continue;
                }

                string[] teile = zeile.Split(',');

                if (!verbindungenLesen)
                {
                    string name = teile[0].Trim();
                    string typ = teile[1].Trim();

                    ARaum raum = null;

                    if (typ == "StartRaum")
                    {
                        raum = new StartRaum();
                        startAnzahl++;
                    }
                    else if (typ == "LeererRaum")
                    {
                        raum = new LeererRaum();
                    }
                    else if (typ == "FallenRaum")
                    {
                        raum = new FallenRaum();
                    }
                    else if (typ == "ZielRaum")
                    {
                        raum = new ZielRaum();
                    }
                    else if (typ == "SchatzRaum")
                    {
                        if (teile.Length > 2 && teile[2].Trim() == "Krone")
                        {
                            raum = new Schatzraum(Schaetze.ESchatz.Krone);
                        }
                        else
                        {
                            raum = new Schatzraum(Schaetze.ESchatz.Trank);
                        }
                    }

                    if (raum != null)
                    {
                        raeume.Add(name, raum);
                    }
                }
                else
                {
                    string von = teile[0].Trim();
                    string richtung = teile[1].Trim();
                    string nach = teile[2].Trim();

                    verbindungen.Add((von, richtung, nach));

                    ARaum raumVon = raeume[von];
                    ARaum raumNach = raeume[nach];

                    if (richtung == "Norden")
                    {
                        raumVon.Norden = raumNach;
                    }
                    else if (richtung == "Süden")
                    {
                        raumVon.Süden = raumNach;
                    }
                    else if (richtung == "Westen")
                    {
                        raumVon.Westen = raumNach;
                    }
                    else if (richtung == "Osten")
                    {
                        raumVon.Osten = raumNach;
                    }
                }
            }
        }

        if (startAnzahl != 1)
        {
            throw new Exception("Es muss genau einen Startraum geben.");
        }

        foreach (var eintrag in raeume)
        {
            if (eintrag.Value is StartRaum)
            {
                _start = eintrag.Value;
                break;
            }
        }

        Dictionary<ARaum, (int x, int y)> positionen = new Dictionary<ARaum, (int x, int y)>();
        Queue<ARaum> queue = new Queue<ARaum>();

        positionen[_start] = (0, 0);
        queue.Enqueue(_start);

        while (queue.Count > 0)
        {
            ARaum aktuellerRaum = queue.Dequeue();
            (int x, int y) pos = positionen[aktuellerRaum];

            if (aktuellerRaum.Norden != null && !positionen.ContainsKey(aktuellerRaum.Norden))
            {
                positionen[aktuellerRaum.Norden] = (pos.x, pos.y - 1);
                queue.Enqueue(aktuellerRaum.Norden);
            }

            if (aktuellerRaum.Süden != null && !positionen.ContainsKey(aktuellerRaum.Süden))
            {
                positionen[aktuellerRaum.Süden] = (pos.x, pos.y + 1);
                queue.Enqueue(aktuellerRaum.Süden);
            }

            if (aktuellerRaum.Westen != null && !positionen.ContainsKey(aktuellerRaum.Westen))
            {
                positionen[aktuellerRaum.Westen] = (pos.x - 1, pos.y);
                queue.Enqueue(aktuellerRaum.Westen);
            }

            if (aktuellerRaum.Osten != null && !positionen.ContainsKey(aktuellerRaum.Osten))
            {
                positionen[aktuellerRaum.Osten] = (pos.x + 1, pos.y);
                queue.Enqueue(aktuellerRaum.Osten);
            }
        }

        int minX = 0;
        int maxX = 0;
        int minY = 0;
        int maxY = 0;

        foreach (var pos in positionen.Values)
        {
            if (pos.x < minX) minX = pos.x;
            if (pos.x > maxX) maxX = pos.x;
            if (pos.y < minY) minY = pos.y;
            if (pos.y > maxY) maxY = pos.y;
        }

        int breite = maxX - minX + 1;
        int hoehe = maxY - minY + 1;

        _karte = new ARaum[hoehe, breite];

        foreach (var eintrag in positionen)
        {
            ARaum raum = eintrag.Key;
            int x = eintrag.Value.x - minX;
            int y = eintrag.Value.y - minY;

            _karte[y, x] = raum;
        }
    }
}