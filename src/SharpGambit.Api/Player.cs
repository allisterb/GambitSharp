namespace SharpGambit;

using System;
using System.Collections.Generic;
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

    public IEnumerable<Strategy> Strategies
    {
        get
        {
            for (int i = 0; i < StrategyCount; i++)
            {
                yield return new Strategy(this, player.GetPlayerStrategy(ptr, i + 1));
            }
        }
    }
    
    public Strategy this[string label] => Strategies.SingleOrFailure(s => s.Label == label, $"The player does not have a strategy with label {label}.");  
    
    public Strategy this[int index] => Strategies.SingleOrFailure(s => s.Index == index);

    public Strategy AddStrategy() => new Strategy(this);

    public Game game;

    internal nint ptr;  
}

