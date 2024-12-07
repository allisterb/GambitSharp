namespace SharpGambit;

using gambit;

public class Player : GameObject
{
    internal Player(Game game, nint ptr) : base(ptr) 
    {
        this.game = game;
    }

    public Player(Game game) :this(game, sharpgambit.AddPlayerToGame(game.ptr)) {}

    //public
    public string Label
    {
        get => sharpgambit.GetPlayerLabel(ptr);
        set => sharpgambit.SetPlayerLabel(ptr, value);
    }


    public Game game;
}

