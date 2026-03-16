namespace Dungeon;

public class Program
{
    public static void Main()
    {
        Console.Write("Name: ");
        string temp = Console.ReadLine();
        string name = string.IsNullOrWhiteSpace(temp) ? "Held" : temp;

        GameController gameController = new GameController(name, "Dunkelwald");
        gameController.Run();
    }
}