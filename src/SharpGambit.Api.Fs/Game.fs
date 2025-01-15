namespace SharpGambit

open System.Linq
open System.Runtime.CompilerServices

[<AutoOpen>]
module Game =
    let nfg_2p (title:string) (player1_strategies:string seq) (player2_strategies:string seq) (payoffs:ITuple seq)= 
        NormalFormGame.TwoPlayerGame(title, player1_strategies.ToArray(), player2_strategies.ToArray(), payoffs.ToArray())

    let mixed_strategy_profile (g:NormalFormGame) (probs:struct(string*double) seq) = g.NewMixedStrategyProfile(probs.ToArray()) 

   
