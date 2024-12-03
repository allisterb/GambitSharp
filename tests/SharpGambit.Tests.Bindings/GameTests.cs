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
        
        Assert.Equal("foo", sharpgambit.GetPlayerTitle(p)); 

        g = sharpgambit.NewGame("my title", 2, ["A", "B"]);
        Assert.NotNull(g);
        p = sharpgambit.GetPlayer(g, 1);
        Assert.Equal("A", sharpgambit.GetPlayerTitle(p));
        p = sharpgambit.GetPlayer(g, 2);
        Assert.Equal("B", sharpgambit.GetPlayerTitle(p));
     
    }
}
