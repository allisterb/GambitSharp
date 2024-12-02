namespace SharpGambit.Tests.Bindings;

using gambit;
using gambit.Gambit;

public class GameTests
{
    [Fact]
    public void Test1()
    {
        var g = sharpgambit.NewGame();
        //var n = p.Rep.NewPlayer();
        
        Assert.NotNull(g);
        var p = sharpgambit.AddPlayerToGame(g);
        Assert.NotNull(p);
        sharpgambit.SetPlayerTitle(p, "foo");
        var t = sharpgambit.GetPlayerTitle(p);
        Assert.Equal("foo", sharpgambit.GetPlayerTitle(p)); 

    }
}
