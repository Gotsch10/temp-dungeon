namespace Dungeon;

public class Held
{
    public string Name { get;}
    public int Health = 100;
    public ARaum Standort { get; set; }
    public Welt welt { get; set; }

    public Held(string name)
    {
        Name = name;
    }
}