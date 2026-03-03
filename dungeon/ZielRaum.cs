namespace Dungeon;

public class ZielRaum : ARaum
{
    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;

        Console.WriteLine($"Gratulation {held.Name} du hast es geschafft");
        Console.WriteLine($"{welt.Name} wurde geschlagen");
        welt.Zielerreicht = true;
    }
}