namespace SharpGambit

    [<AutoOpen>]
    module Solvers =
        let solve_enum_pure (g:Game) = Solvers.EnumPureStrategySolve(g)

        let solve_enum_mixed (g:Game) = Solvers.EnumMixedStrategySolve(g)

  
