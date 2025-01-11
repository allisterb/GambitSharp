namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using gambit;

public struct Player
{
    internal Player(Game game, nint ptr)
    {
        this.game = game;
        this.ptr = ptr;
    }

    public Player(Game game) : this(game, gambit.game.NewPlayer(game.ptr)) { }

    public int Index => player.GetPlayerNumber(ptr) - 1;

    public string Label
    {
        get => player.GetPlayerLabel(ptr);
        set => player.SetPlayerLabel(ptr, value);
    }

    public int StrategyCount => player.GetPlayerNumStrategies(ptr);

    public IEnumerable<PureStrategy> Strategies
    {
        get
        {
            for (int i = 0; i < StrategyCount; i++)
            {
                yield return new PureStrategy(this, player.GetPlayerStrategy(ptr, i + 1));
            }
        }
    }
    
    public PureStrategy this[string label] => Strategies.SingleOrFailure(s => s.Label == label, $"The player does not have a strategy with label {label}.");  
    
    public PureStrategy this[int index] => Strategies.SingleOrFailure(s => s.Index == index);

    public PureStrategy AddStrategy() => new PureStrategy(this);

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Player p)
        {
            return p.ptr == this.ptr;
        }
        else
        {
            return false;
        }

    }

    public override int GetHashCode() => this.ptr.GetHashCode();

    public Game game;
    internal nint ptr;  
}

