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

    Outcome IStrategyProfile.Outcome => new Outcome(this.game, strategyprofile.PSP_GetOutcome(this.ptr));
    
    public int Length => game.PlayerCount;

    public int Index => strategyprofile.PSP_GetIndex(ptr);

    public Strategy this[int player] => new Strategy(game.GetPlayer(player), strategyprofile.PSP_GetStrategy(ptr, player + 1));

    public Strategy this[string label]
    {
        get
        {
            for (int i = 0; i < Length; i++)
            {
                var s = strategyprofile.PSP_GetStrategy(ptr, i + 1);
                if (strategy.GetStrategyLabel(s) == label)
                {
                    return new Strategy(game.GetPlayer(i), s);  
                }
            }
            throw new ArgumentException($"This strategy profile does not contain the strategy {label}.");
        }
    }
    
    public void SetStrategy(Strategy s) => strategyprofile.PSP_SetStrategy(ptr, s.ptr);
    internal Game game;
    internal nint ptr;
}

