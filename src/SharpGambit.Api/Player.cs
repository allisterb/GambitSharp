namespace SharpGambit;

using gambit;

public struct Player 
{
    internal Player(Game game, nint ptr) 
    {
        this.game = game;
        this.ptr = ptr;
    }

    public Player(Game game) :this(game, gambit.game.NewPlayer(game.ptr)) {}

    public string Label
    {
        get => player.GetPlayerLabel(ptr);
        set => player.SetPlayerLabel(ptr, value);
    }

    public int StrategyCount => player.GetPlayerNumStrategies(ptr); 

    public Strategy AddStrategy() => new Strategy(this);

    public Game game;

    internal nint ptr;  
}

