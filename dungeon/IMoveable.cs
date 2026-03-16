namespace Dungeon;

public interface IMoveable
{
    public enum ERichtung
    {
        ENone,
        EForward,
        EBackward,
        ERight,
        ELeft
    }

    void Move(ERichtung richtung);
}