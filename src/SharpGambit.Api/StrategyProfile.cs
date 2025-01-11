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

    public int[] StrategyCounts => game.Strategies.Select(t => t.Length).ToArray();

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
    public MixedStrategyProfile(Game game, nint ptr)
    {
        this.game = game;
        this.ptr = ptr;
    }

    public MixedStrategyProfile(Game game) : this(game, strategyprofile.MSP_RationalNew(game.ptr)) {}

    public MixedStrategyProfile(Game game, (string, double)[][] probs) : this(game)
    {
        if (probs.Length != game.PlayerCount) throw new ArgumentException();
        for (int i = 0; i < probs.Length; i++)
        {
            if (probs[i].Length != game[i].StrategyCount) throw new ArgumentException();
            for (int j = 0; j < probs[i].Length; j++)
            {
                var p = probs[i][j];
                SetStrategyProbability(game[i][p.Item1], p.Item2);
            }
        }
        
    }

    public MixedStrategyProfile(Game game, (string, double)[] probs) : this(game)
    {
        if (probs.Length != game.Strategies.Select(t => t.Length).ToArray().Product()) throw new ArgumentException();
        for (int i = 0; i < probs.Length; i++)
        {
            var indices = probs.GetIndices(i, game.StrategyCounts);
            var p = probs[i];
            SetStrategyProbability(game[indices[0]][p.Item1], p.Item2);
        }

    }
    public double GetStrategyProbability(PureStrategy s) => strategyprofile.MSP_RationalGetStrategyProbability(ptr, s.ptr);

    public void SetStrategyProbability(PureStrategy s, double prob) => strategyprofile.MSP_RationalSetStrategyProbability(ptr, s.ptr, prob);

    public double GetPlayerPayoff(int pl) => strategyprofile.MSP_RationalGetPlayerNumPayoff(ptr, pl + 1);

    public double GetPlayerPayoff(Player pl) => strategyprofile.MSP_RationalGetPlayerPayoff(ptr, pl.ptr);

    public double GetStrategyPayoff(PureStrategy s) => strategyprofile.MSP_RationalGetStrategyPayoff(ptr, s.ptr);

    public double this[PureStrategy s] => GetStrategyPayoff(s);

    public double this[Player p] => GetPlayerPayoff(p);

    public double this[int player, string strategy]
    {
        get => this.GetStrategyProbability(game[player][strategy]);
        set => this.SetStrategyProbability(game[player][strategy], value);
    }

    public MixedStrategy this[int player] => new MixedStrategy(this, game[player]);
        
    internal nint ptr;
    public Game game;
}


