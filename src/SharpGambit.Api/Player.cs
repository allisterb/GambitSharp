namespace SharpGambit;

using gambit;

public class Player : GameObject
{
    internal Player(Game game, nint ptr) : base(ptr) 
    {
        this.game = game;
    }

    public Player(Game game) :this(game, gambit.game.AddPlayer(game.ptr)) {}

    public string Label
    {
        get => player.GetPlayerLabel(ptr);
        set => player.SetPlayerLabel(ptr, value);
    }

    public Strategy AddStrategy() => new Strategy(this);

    public Game game;
}

