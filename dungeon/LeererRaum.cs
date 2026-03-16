namespace Dungeon;

public class LeererRaum : ARaum
{
    public override char Symbol => 'L';

    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;
        Erkundet = true;
        Console.WriteLine("Du befindest dich in einem Leerem Raum und du fühlst wie dich etwas aus der dunkelheit anstarrt");
    }
}