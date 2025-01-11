namespace SharpGambit.Tests.Api.Fs

module NormalFormTests =
    open System
    open Xunit

    open SharpGambit
    open NormalFormGame

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
