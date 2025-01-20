namespace SharpGambit.Tests.Api;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SolverTests
{
    [Fact]
    public void CanEnumPureSolve()
    {
        var g = NormalFormGame.TwoPlayerGame("Prisoner's Dilemna", ["Quiet", "Fink"], ["Quiet", "Fink"], [
                    [(2,2), (0,3)],
                    [(3,0), (1,1)]
                ]);
        var sol = Solvers.EnumPureStrategySolve(g);
        var h = sol.Html;
        //Assert.Equal(1.0, sol.solutions[0][0, "Fink"]);   
    }
}

