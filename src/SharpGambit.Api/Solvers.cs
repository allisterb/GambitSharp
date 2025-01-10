namespace SharpGambit;

using System.Linq;

using gambit;

public class Solvers
{
    public static MixedStrategyProfile[] EnumPureStrategySolve(Game g) => 
        NativeHelpers.EnumPureStrategySolve(g.ptr).Select(p => new MixedStrategyProfile(g, p)).ToArray();
}

