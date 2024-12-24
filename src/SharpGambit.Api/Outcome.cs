namespace SharpGambit;

using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gambit;

public struct Outcome
{
    public Outcome(Game game, nint ptr)
    {
        this.game = game;
        this.ptr = ptr;
    }

    public Outcome(Game game) : this(game, gambit.game.NewOutcome(game.ptr)) {}

    public Outcome(Game game, ITuple payoffs) :this(game)
    {
        if (payoffs.Length != game.PlayerCount) throw new ArgumentException("The number of items in the payoff tuple must equal the number of players.", nameof(payoffs));
        for (int i = 0; i < payoffs.Length; i++)
        {
            var r = Rational.Convert(payoffs[i]!);
            outcome.SetRationalPayoff(this.ptr, i + 1, (int) r.Numerator, (int) r.Denominator);
        }        
    }

    public int Length => game.PlayerCount;

    public int Index => outcome.GetOutcomeIndex(ptr);

    public Rational Payoff(int pl)
    {
        
        outcome.GetPayoff(ptr, game.FailIfPlayerIndexTooLarge(pl) + 1, out var n, out var d);
        return new Rational(n, d);
    }
    
    public Rational this[int pl] => Payoff(pl);

    public Game game;
    internal nint ptr;
}

