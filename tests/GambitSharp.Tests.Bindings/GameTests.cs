namespace GambitSharp.Tests.Bindings;

using gambit;

public unsafe class GameTests
{
    [Fact]
    public void CanConstructNormalFormGame()
    {
        var g = game.NewNormalFormGame("foo", 1, ["A"], [1]);
        var p = game.NewPlayer(g);
        Assert.NotEqual(p, nint.Zero);
        player.SetPlayerLabel(p, "foo");
        Assert.Equal("foo", player.GetPlayerLabel(p));
        var s = player.NewPlayerStrategy(p);
        //var n = player.GetPlayerStrategies(p, var ref size);

        Assert.Equal(2, player.GetPlayerNumStrategies(p));

        g = game.NewNormalFormGame("Prisoner's dilemna", 2, ["Prisoner 1", "Prisoner 2"], [2, 2]);
        var payoffs = new[,] { { (4, 5), (7, 8) }, { (4, 5), (7, 8) } };


    }

    [Fact]
    public void CanGetPlayers()
    {
        var g = game.NewNormalFormGame("test game", 3, ["A", "B", "C"], [4, 5, 6]);
        var players = game.GetPlayers(g, out var size);
        for (int i =0; i < size; i++)
        {
            Assert.Equal(game.GetPlayer(g, i + 1), players[i]); 
        }
    }
    [Fact]
    public void CanGetSetOutcomrd()
    {
        var g = game.NewNormalFormGame("foo", 1, ["A"], [1]);
        var p = game.NewPlayer(g);
        Assert.NotEqual(p, nint.Zero);
        player.SetPlayerLabel(p, "foo");
        Assert.Equal("foo", player.GetPlayerLabel(p));
        var o = game.NewOutcome(g);
        outcome.SetRationalPayoff(o, 1, 2, 3);
        outcome.GetPayoff(o, 1, out var n, out var d);   
        Assert.Equal(1, player.GetPlayerNumStrategies(p));

        var obj = new[,] { { (4, 5), (7, 8) }, { (4, 5), (7, 8) } };

    }
    //var n = p.Rep.NewPlayer();
    //Marshal.
    //Assert.NotEqual(g, nint.Zero);

    /*
    g = GambitSharp.NewGame("untitled game", 0, null);
    Assert.NotEqual(nint.Zero, g);
    p = GambitSharp.AddPlayerToGame(g);
    Assert.NotEqual(p, nint.Zero);
    g = GambitSharp.NewGame("my title", 2, ["A", "B"]);
    Assert.NotEqual(nint.Zero, g);
    p = GambitSharp.GetPlayer(g, 1);
    Assert.Equal("A", GambitSharp.GetPlayerLabel(p));
    p = GambitSharp.GetPlayer(g, 2);
    Assert.Equal("B", GambitSharp.GetPlayerLabel(p));

    var s = GambitSharp.NewPlayerStrategy(p);
    Assert.Equal(1, GambitSharp.GetPlayerStrategies(p));
    */
}

