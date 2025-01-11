namespace SharpGambit.Tests.Api.Fs

module NormalFormTests =
    open System
    open Xunit

    open SharpGambit

    [<Fact>]
    let ``Can create normal-form game`` () =
        let g1 = nfg_2p "Prisoner's Dilemna" ["Quiet"; "Fink"] ["Quiet"; "Fink"] [
            (3,3); (4,0)
            (0,4); (2,2)
        ]
        Assert.Equal(4, g1["Quiet", "Fink"][0])
        Assert.Equal(3, g1["Quiet", "Quiet"][0])
        Assert.Equal(2, g1["Fink", "Fink"][1])
        Assert.Equal(3, g1["Quiet", "Quiet"][1])

        let s = solve_enum_pure g1
        Assert.Equal(g1[0]["Fink"], s.solutions[0][0])

        let msp = g1 |> mixed_strategy_profile [
            ("Fink", 0.5); ("Quiet", 0.5)
            ("Fink", 0.5); ("Quiet", 0.5)
        ] 
        let x = msp[0, "Quiet"]