namespace Dungeon;

public class StartRaum : ARaum
{
    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;

        Console.WriteLine($"Hallo {held.Name} du bist endlich erwacht. Wir sind in {welt.Name}.");
    }
}