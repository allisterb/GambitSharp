namespace SharpGambit;

using gambit;

public class Strategy : GameObject
{
    internal Strategy(Player player, nint ptr) : base(ptr)
    {
        this.player = player;
    }

    //public Player(Game game) : this(game, sharpgambit.AddPlayerToGame(game.ptr)) { }

    //public
    public string Label
    {
        get => sharpgambit.GetPlayerTitle(ptr);
        set => sharpgambit.SetPlayerTitle(ptr, value);
    }


    public Player player;
}

