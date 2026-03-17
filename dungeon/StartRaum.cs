namespace Dungeon;

public class StartRaum : ARaum
{
    public override char Symbol => 'S';

    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;
        Erkundet = true;
        Console.WriteLine($"Hallo {held.Name} du bist endlich erwacht. Wir sind in {welt.Name}.");
        Thread.Sleep(2000);
    }
}