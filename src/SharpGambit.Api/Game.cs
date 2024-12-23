﻿namespace SharpGambit;

using gambit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

public class Game : GameObject
{
    protected Game(nint ptr) : base(ptr) {}
    
    public string Title
    {
        get => game.GetTitle(ptr);
        set => game.SetTitle(value, ptr);
    }

    public int PlayerCount => game.NumPlayers(ptr); 


    public Player GetPlayer(int player) => new Player(this, game.GetPlayer(ptr, FailIfPlayerIndexTooLarge(player) + 1));

    public IEnumerable<Player> Players
    {
        get
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                yield return new Player(this, game.GetPlayer(ptr, i + 1));
            }
        }
    }

    public Player this[int pl] => GetPlayer(pl);

    public Player this[string label] => Players.SingleOrFailure(pl => pl.Label == label, $"No player has the label {label}.");


    public Player NewPlayer() => new Player(this);

    //public 
    internal int FailIfPlayerIndexTooLarge(int pl) => pl >= PlayerCount ? throw new ArgumentException($"This game has {PlayerCount} players.") : pl;  
}

public class NormalFormGame : Game
{
    public NormalFormGame(string title, string[] players, string[][] strategies) :
        base(game.NewNormalFormGame(title, players.Length, players, strategies.Select(s => s.Length).ToArray()))
    {
        if (strategies.Length != players.Length) throw new ArgumentException("The rank of the strategies array must equal the number of players.");
        for (int i = 0; i < players.Length; i++)
        {
            var p = game.GetPlayer(ptr, i + 1); 
            for (int j = 0; j < strategies[i].Length; j++)
            {
                var s = player.GetPlayerStrategy(p, j + 1);
                strategy.SetStrategyLabel(s, strategies[i][j]);
            }
        }
    }

    public int[] StrategyCounts => Players.Select(p => p.StrategyCount).ToArray();
    
    
    public PureStrategyProfile this[params Strategy[] strategies]
    {
        get
        {
            if (strategies.Length != PlayerCount) throw new ArgumentException("The number of strategies specified must equal the the number of players.");
            var psp  = new PureStrategyProfile(this, strategies);
            return psp;
        }
    }

    public PureStrategyProfile this[params string[] strategies] => this[strategies.Select((s,i) => this[i][s]).ToArray()];
    
}
