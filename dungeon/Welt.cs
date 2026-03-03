using System;

namespace Dungeon;

public class Welt
{
    public string Name { get; }
    public bool Zielerreicht = false;

    private ARaum _start;

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

    private void Erschaffen()
    {
        var A = new StartRaum();    
        var B = new LeererRaum();   
        var C = new FallenRaum();   
        var D = new LeererRaum();   
        var E = new FallenRaum();   
        var F = new LeererRaum();   
        var G = new ZielRaum();     

        _start = A;
    
        A.Norden = B;
        B.Süden = A;
        
        B.Norden = C;
        C.Süden = B;
        
        C.Osten = D;
        D.Westen = C;
        
        D.Norden = E;
        E.Süden = D;
        
        D.Süden = F;
        F.Norden = D;
        
        B.Osten = F;
        F.Westen = B;
        
        D.Osten = G;
        G.Westen = D;

      
      
    }
}