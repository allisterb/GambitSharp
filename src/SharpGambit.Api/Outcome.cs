namespace SharpGambit;

using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Outcome : GameObject
{
    public Outcome(Game game, nint ptr) : base(ptr)
    {
        this.game = game;
    }

    public Outcome(Game game) : this(game, gambit.game.NewOutcome(game.ptr)) {}

    public Outcome(Game game, Array payoffs) :this(game)
    {
        if (!(payoffs.GetType().GetElementType()?.IsAssignableTo(typeof(ITuple)) ?? false)) throw new ArgumentException("The elements of the payoffs array must be tuples.", nameof(payoffs));  
        if (payoffs.Rank != game.PlayerCount) throw new ArgumentException("The rank of the payoffs array must equal the number of players.", nameof(payoffs));
        
    }

    protected Game game;
}

