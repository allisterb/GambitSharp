namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using gambit;

public class PureStrategyProfile : GameObject
{
    public PureStrategyProfile(Game game, nint ptr) : base(ptr)
    {
        this.game = game;
    }

    public PureStrategyProfile(Game game) : this(game, gambit.game.NewPureStrategyProfile(game.ptr)) {}
   

    internal Game game;
    public Dictionary<Player, Rational> Outcomes = new();
}

