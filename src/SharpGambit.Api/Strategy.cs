namespace SharpGambit;

using gambit;

public class Strategy : GameObject
{
    internal Strategy(Player player, nint ptr) : base(ptr)
    {
        this.player = player;
    }

    public Strategy(Player player) : this(player, gambit.player.NewPlayerStrategy(player.ptr)) { }

    public string Label
    {
        get => strategy.GetStrategyLabel(ptr);
        set => strategy.SetStrategyLabel(ptr, value);
    }


    public Player player;
}

