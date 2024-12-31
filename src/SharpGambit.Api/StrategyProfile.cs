namespace SharpGambit;

using System;
using System.Collections.Generic;
using System.Linq;

using gambit;

public struct PureStrategyProfile : IStrategyProfile
{
    public PureStrategyProfile(Game game, params PureStrategy[] strategies)
    {
        if (strategies.Length != game.PlayerCount) throw new ArgumentException("The length of the strategies array must equal the number of players.");
        this.game = game;
        this.ptr = strategyprofile.PSP_New(game.ptr);
        foreach (var strategy in strategies)
        {
            strategyprofile.PSP_SetStrategy(ptr, strategy.ptr);
        }
    }

    public PureStrategyProfile(Game game, params int[] strategies) : this(game, strategies.Select((s, i) => game[i][s]).ToArray()) { }
    
    Game IStrategyProfile.Game => game;

    public IEnumerable<PureStrategy> Strategies
    {
        get
        {
            for (int i = 0; i < Length; i++)
            {
                yield return new PureStrategy(game.GetPlayer(i), strategyprofile.PSP_GetStrategy(ptr, i + 1));
            }
        }
    }

    public PureStrategy GetStrategy(int pl) => new PureStrategy(game.GetPlayer(pl), strategyprofile.PSP_GetStrategy(ptr, pl + 1));

    public Outcome Outcome
    { 
        get => new Outcome(this.game, strategyprofile.PSP_GetOutcome(this.ptr));
        set => strategyprofile.PSP_SetOutcome(this.ptr, value.ptr);
    }
    
    public int Length => game.PlayerCount;

    public int Index => strategyprofile.PSP_GetIndex(ptr);
    
    public Rational this[int player] => this.Outcome[player];

    public void SetStrategy(PureStrategy s) => strategyprofile.PSP_SetStrategy(ptr, s.ptr);
    
    internal Game game;
    internal nint ptr;
}

public struct MixedStrategyProfile
{
    public MixedStrategyProfile(Game game)
    {
        ptr = strategyprofile.MSP_New(game.ptr);
        this.game = game;
    }

    public double GetStrategyProbability(PureStrategy s) => strategyprofile.MSP_GetStrategyProbability(ptr, s.ptr);

    public void SetStrategyProbability(PureStrategy s, double prob) => strategyprofile.MSP_SetStrategyProbability(ptr, s.ptr, prob);

    public double this[PureStrategy s]
    {
        get => strategyprofile.MSP_GetStrategyPayoff(ptr, s.ptr);   
    }

    public double this[int player, string strategy]
    {
        get => this.GetStrategyProbability(game[player][strategy]);
        set => this.SetStrategyProbability(game[player][strategy], value);
    }

    public MixedStrategy this[int player] => new MixedStrategy(this, game[player]);

    internal nint ptr;
    public Game game;
    public Player player;
}


