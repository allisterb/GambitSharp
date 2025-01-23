namespace GambitSharp;

public abstract class GameObject
{
    protected GameObject(nint ptr)
    {
        this.ptr = ptr;
    }

    internal nint ptr;
}

interface IStrategyProfile
{
    Game Game { get; }

    int Index { get; }

    int Length => Game.PlayerCount;

    Outcome Outcome { get; }    
}
