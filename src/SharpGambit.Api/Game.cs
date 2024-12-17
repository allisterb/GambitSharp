namespace SharpGambit;

using gambit;
using System;
using System.Collections.Generic;
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

    public Player[] Players
    {
        get
        {
            List<Player> list = new List<Player>(); 
            unsafe
            {
                var pptr = game.GetPlayers(ptr, out var size);
                //Player[] players = new Player[size];
                for (int i = 0; i < size; i++)
                {
                    list.Add(new Player(this, new IntPtr(pptr[i])));
                }
                return list.ToArray();
            }
            
        }
    }

    public Player NewPlayer() => new Player(this);
}

public class NormalFormGame : Game
{
    public NormalFormGame(string title, string[] players, string[][] strategies) :
        base(game.NewNormalFormGame(title, players.Length, players, strategies.Select(s => s.Length).ToArray()))
    {
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
}
