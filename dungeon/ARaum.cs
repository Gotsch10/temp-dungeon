namespace Dungeon;

public abstract class ARaum
{
    public ARaum Norden { get; set; }
    public ARaum Süden { get; set; }
    public ARaum Westen { get; set; }
    public ARaum Osten { get; set; }

    public bool Erkundet { get; set; } = false;
    public abstract char Symbol { get; }

    public abstract void Betreten(Held held, Welt welt);
}