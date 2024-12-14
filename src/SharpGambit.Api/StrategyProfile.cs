namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gambit;

public class StrategyProfile : GameObject
{
    public StrategyProfile(Game game, nint ptr) : base(ptr)
    {
        this.game = game;
    }

    public StrategyProfile(Game game) : this(game, gambit.game.NewStrategyProfile(game.ptr)) {}
   

    internal Game game;
    public Dictionary<Player, Rational> Outcomes = new();
}

