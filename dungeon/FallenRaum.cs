namespace Dungeon;

public class FallenRaum : ARaum
{
    public override char Symbol => 'F';

    public virtual int Falle()
    {
        Random rnd = new Random();
        int random = rnd.Next(1, 10);
        return random;
    }

    public override void Betreten(Held held, Welt welt)
    {
        held.Standort = this;
        Erkundet = true;
        int random = Falle();

        switch (random)
        {
            case 1:
                Console.WriteLine("Du bist durch eine Falltür gefallen!!");
                Console.WriteLine("Es waren Stacheln unten. Du bist jetzt ein Kebap.");
                held.Health = 0;
                break;
            case 2:
                Console.WriteLine("Der Boden ist rutschig. Du fällst hin");
                Console.WriteLine("Du nimmst 7 Schaden.");
                held.Health -= 7;
                break;
            case 3:
                Console.WriteLine("Der Boden unter dir kippt weg!");
                Console.WriteLine("Er ist nur schräg geworden und du hast dich verknöchelt.");
                Console.WriteLine("Du nimmst 5 Schaden");
                held.Health -= 5;
                break;
            case 4:
                Console.WriteLine("Es fliegen Kies und Sand auf dich herunter.");
                Console.WriteLine("Du musst so stark, durch den Sand, husten dass du 3 Schaden nimmst");
                held.Health -= 3;
                break;
            case 5:
                Console.WriteLine("Stacheln kommen aus dem Boden!");
                Console.WriteLine("Sie sind nur kurz und nur deine Fußsohlen sind verletzt.");
                Console.WriteLine("Du nimmst 15 Schaden");
                held.Health -= 15;
                break;
            case 6:
                Console.WriteLine("Der ganze Raum wird mit heißem Dampf gefühlt!");
                Console.WriteLine("Dein Körper wird verbrüht und du nimmst 40 Schaden");
                held.Health -= 40;
                break;
            case 7:
                Console.WriteLine("Es spritzt Salzsäure auf deinen ganzen Körper!");
                Console.WriteLine("Es war nicht genug zu töten aber du nimmst 70 Schaden");
                held.Health -= 70;
                break;
            case 8:
                Console.WriteLine("Es fahlen Brocken auf dich herab.");
                Console.WriteLine("Du nimmst 30 Schaden");
                held.Health -= 30;
                break;
            case 9:
                Console.WriteLine("Ein Holzklotz schwingt gegen deinen Kopf!");
                Console.WriteLine("Du nimmst 50 Schaden");
                held.Health -= 50;
                break;
            case 10:
                Console.WriteLine("Der ganze Raum wird mit Rauch gefühlt.");
                Console.WriteLine("Du hast glück, es ist nicht giftig. Du nimmst keinen Schaden");
                break;
        }
    }
}