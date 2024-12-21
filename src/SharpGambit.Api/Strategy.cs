namespace SharpGambit;

using gambit;

public struct Strategy 
{
    internal Strategy(Player player, nint ptr, string? label= null)
    {
        this.Player = player;
        this.ptr = ptr;
        if (label is not null)
        {
            strategy.SetStrategyLabel(ptr, label);
        }
    }

    public Strategy(Player player, string? label = null) : this(player, gambit.player.NewPlayerStrategy(player.ptr), label) { }

    //public S
    
    public string Label
    {
        get => strategy.GetStrategyLabel(ptr);
        set => strategy.SetStrategyLabel(ptr, value);
    }

    public int Index => strategy.GetStrategyIndex(ptr) - 1;

    public override string? ToString() => string.IsNullOrEmpty(Label) ? base.ToString() : Label;

    public Player Player;

    internal nint ptr;
}

