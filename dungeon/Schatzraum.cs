namespace Dungeon;

public class Schatzraum : ARaum
{
    public override char Symbol => 'C';

    public Schaetze.ESchatz Schatz { get; set; }
    private bool _gefunden = false;

    public Schatzraum(Schaetze.ESchatz schatz)
    {
        Schatz = schatz;
    }

    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;
        Erkundet = true;

        if (!_gefunden)
        {
            held.Rucksack.Add(Schatz);
            _gefunden = true;
            Console.WriteLine($"Du hast einen Schatz gefunden: {Schatz}");
        }
        else
        {
            Console.WriteLine("Dieser Schatzraum ist leer.");
        }
    }
}