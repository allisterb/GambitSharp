namespace SharpGambit;

using System.Linq;

using gambit;

public struct PureStrategySolution
{
    public PureStrategySolution(MixedStrategyProfile[] profiles)
    {
        this.profiles = profiles;
        this.solutions = 
            profiles
            .Select(p => p.game.Strategies
            .SelectMany(s=>s)
            .Where(s => p.GetStrategyProbability(s) == 1.0)
            .ToArray())
            .ToArray();
    }

    internal MixedStrategyProfile[] profiles;
    public PureStrategy[][] solutions;
}


public class Solvers
{
    public static PureStrategySolution EnumPureStrategySolve(Game g) => 
        new PureStrategySolution(NativeHelpers.EnumPureStrategySolve(g.ptr).Select(p => new MixedStrategyProfile(g, p)).ToArray());
}

