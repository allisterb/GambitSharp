namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using gambit;
public struct PureStrategy 
{
    internal PureStrategy(Player player, nint ptr, string? label= null)
    {
        this.Player = player;
        this.ptr = ptr;
        if (label is not null)
        {
            strategy.SetStrategyLabel(ptr, label);
        }
    }

    public PureStrategy(Player player, string? label = null) : this(player, gambit.player.NewPlayerStrategy(player.ptr), label) { }

    public string Label
    {
        get => strategy.GetStrategyLabel(ptr);
        set => strategy.SetStrategyLabel(ptr, value);
    }

    public int Index => strategy.GetStrategyIndex(ptr) - 1;

    //public override string? ToString() => string.IsNullOrEmpty(Label) ? base.ToString() : Label;

    public Player Player;

    internal nint ptr;
}

public struct MixedStrategy
{
    public MixedStrategy(MixedStrategyProfile msp, Player player)
    {
        this.msp = msp;
        this.player = player;
    }

    public IEnumerable<PureStrategy> Strategies => player.Strategies;

    public double this[PureStrategy s]
    {
        get => s.Player.ptr == this.player.ptr ? this.msp.GetStrategyProbability(s) : throw new ArgumentException($"The strategy {s.Label} does not belong to the player {player.Label}.");
        set => this.msp.SetStrategyProbability(s, (s.Player.ptr == this.player.ptr) ? value : throw new ArgumentException($"The strategy {s.Label} does not belong to the player {player.Label}.")); 
    }

    public double this[string s]
    {
        get => this[this.player[s]];
        set => this[this.player[s]] = value;
    }

    public MixedStrategyProfile msp;
    public Player player;
}