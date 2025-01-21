namespace SharpGambit.Tests.Api.Fs

module NormalFormTests =
    open System
    open Xunit

    open SharpGambit

    [<Fact>]
    let ``Can create normal-form 2p game`` () =
        let g1 = nfg_2p "Prisoner's Dilemna" ["Quiet"; "Fink"] ["Quiet"; "Fink"] [
            (3,3); (4,0)
            (0,4); (2,2)
        ]
        Assert.Equal(4, g1["Quiet", "Fink"][0])
        Assert.Equal(3, g1["Quiet", "Quiet"][0])
        Assert.Equal(2, g1["Fink", "Fink"][1])
        Assert.Equal(3, g1["Quiet", "Quiet"][1])

        let s = solve_enum_pure g1
        Assert.Equal(g1[0]["Quiet"], s.solutions[0][0])

        let msp = mixed_strategy_profile g1 [
                ("Fink", 0.4); ("Quiet", 0.6)
                ("Fink", 0.4); ("Quiet", 0.6)
            ] 
        let x = msp[0, "Quiet"]
        Assert.Equal(0.6, x)

    [<Fact>]
    let ``Can create normal-form 2p zero-sum game`` () =
        let g1 = nfg_2p_zs "Rock Paper Scissors" ["Rock"; "Paper"; "Scissors"] ["Rock"; "Paper"; "Scissors"] [
            1; 1; 1
            1; 1; 1
            1; 1; 1
        ]
        Assert.NotNull g1