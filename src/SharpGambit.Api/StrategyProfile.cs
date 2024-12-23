namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Linq;

using gambit;

public struct PureStrategyProfile : IStrategyProfile
{
    public PureStrategyProfile(Game game, params Strategy[] strategies)
    {
        if (strategies.Length != game.PlayerCount) throw new ArgumentException("The length of the strategies array must equal the number of players.");
        this.game = game;
        this.ptr = strategyprofile.PSP_New(game.ptr);
        foreach (var strategy in strategies)
        {
            strategyprofile.PSP_SetStrategy(ptr, strategy.ptr); 
        }
    }

    Game IStrategyProfile.Game => game;

    public IEnumerable<Strategy> Strategies
    {
        get
        {
            for (int i = 0; i < Length; i++)
            {
                yield return new Strategy(game.GetPlayer(i), strategyprofile.PSP_GetStrategy(ptr, i + 1));
            }
        }
    }

    public Strategy GetStrategy(int pl) => new Strategy(game.GetPlayer(pl), strategyprofile.PSP_GetStrategy(ptr, pl + 1));

    public Outcome Outcome => new Outcome(this.game, strategyprofile.PSP_GetOutcome(this.ptr));
    
    public int Length => game.PlayerCount;

    public int Index => strategyprofile.PSP_GetIndex(ptr);
    
    public Rational this[int player] => new Outcome(game, strategyprofile.PSP_GetOutcome(ptr))[player];

    public void SetStrategy(Strategy s) => strategyprofile.PSP_SetStrategy(ptr, s.ptr);
    internal Game game;
    internal nint ptr;
}

