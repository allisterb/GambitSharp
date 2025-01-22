namespace SharpGambit.Tests.Api.Fs

module NormalFormTests =
    open System
    open Xunit

    open SharpGambit

    [<Fact>]
    let ``Can create normal-form 2p game`` () =
        let g1 = nfg_2p "Prisoner's Dilemna" ["Quiet"; "Fink"] ["Quiet"; "Fink"] [
            (3,3); (0,4)
            (4,0); (2,2)
        ]
        Assert.Equal(0, g1["Quiet", "Fink"][0])
        Assert.Equal(3, g1["Quiet", "Quiet"][0])
        Assert.Equal(2, g1["Fink", "Fink"][1])
        Assert.Equal(3, g1["Quiet", "Quiet"][1])

        let s = solve_enum_pure g1
        Assert.Equal(g1[0]["Fink"], s.solutions[0][0])

        let msp = mixed_strategy_profile g1 [
                ("Fink", 0.4); ("Quiet", 0.6)
                ("Fink", 0.4); ("Quiet", 0.6)
            ] 
        let x = msp[0, "Quiet"]
        Assert.Equal(0.6, x)
        let h = s.Html
        Assert.NotNull h

    [<Fact>]
    let ``Can create normal-form 2p zero-sum game`` () =
        let g1 = nfg_2p_zs "Rock Paper Scissors" ["Rock"; "Paper"; "Scissors"] ["Rock"; "Paper"; "Scissors"] [
            0; -1; 1
            1; 0; -1
            -1; 1; 0
        ]
        Assert.NotNull g1
        let s1 = solve_enum_mixed g1
        Assert.NotNull s1
        let h = s1.Html;
        Assert.NotNull h;