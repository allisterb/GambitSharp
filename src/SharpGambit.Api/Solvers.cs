namespace SharpGambit;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using gambit;
public class Solvers
{
    public unsafe static MixedStrategyProfile[] EnumPureStrategySolve(Game g)
    {
        var ptr = solvers.EnumPureStrategySolve(g.ptr, out var size);
        var sol = new MixedStrategyProfile[size];
        for(int i = 0; i < size; i++)
        {

            var x = ptr[i];
            sol[i] = new MixedStrategyProfile(g, (nint) ptr[i]);
        }
        return sol;
    }
}

