namespace Dungeon;

public class Held : IMoveable
{
    public string Name { get; }
    public int Health = 100;
    public ARaum Standort { get; set; }
    public Welt welt { get; set; }

    public List<Schaetze.ESchatz> Rucksack { get; set; }

    public Held(string name)
    {
        Name = name;
        Rucksack = new List<Schaetze.ESchatz>();
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public void Move(IMoveable.ERichtung richtung)
    {
        if (Standort == null)
        {
            return;
        }

        ARaum next = null;

        switch (richtung)
        {
            case IMoveable.ERichtung.EForward:
                next = Standort.Norden;
                break;

            case IMoveable.ERichtung.EBackward:
                next = Standort.Süden;
                break;

            case IMoveable.ERichtung.ELeft:
                next = Standort.Westen;
                break;

            case IMoveable.ERichtung.ERight:
                next = Standort.Osten;
                break;
        }

        if (next == null)
        {
            Console.WriteLine("Dort kannst du nicht hin.");
            Thread.Sleep(1000);
            return;
        }

        next.Betreten(this, welt);
    }
}