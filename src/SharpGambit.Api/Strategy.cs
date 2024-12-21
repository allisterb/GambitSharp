namespace SharpGambit;

using gambit;

public struct Strategy 
{
    internal Strategy(Player player, nint ptr)
    {
        this.Player = player;
        this.ptr = ptr;
    }

    public Strategy(Player player) : this(player, gambit.player.NewPlayerStrategy(player.ptr)) { }

    
    public string Label
    {
        get => strategy.GetStrategyLabel(ptr);
        set => strategy.SetStrategyLabel(ptr, value);
    }

    public int Index => strategy.GetIndex(ptr) - 1;

    public override string? ToString() => string.IsNullOrEmpty(Label) ? base.ToString() : Label;

    public Player Player;

    internal nint ptr;
}

