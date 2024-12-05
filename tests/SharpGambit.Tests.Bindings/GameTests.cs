namespace SharpGambit.Tests.Bindings;

using gambit;
using System.Runtime.InteropServices;

public class GameTests
{
    [Fact]
    public void Test1()
    {
        var g = sharpgambit.NewEmptyGame();
        //var n = p.Rep.NewPlayer();
        //Marshal.
        //Assert.NotEqual(g, nint.Zero);
        var p = sharpgambit.AddPlayerToGame(g);
        Assert.NotEqual(p, nint.Zero);
        sharpgambit.SetPlayerTitle(p, "foo");
        
        Assert.Equal("foo", sharpgambit.GetPlayerTitle(p));

        g = sharpgambit.NewGame("untitled game", 0, null);
        Assert.NotEqual(nint.Zero, g);
        p = sharpgambit.AddPlayerToGame(g);
        Assert.NotEqual(p, nint.Zero);
        g = sharpgambit.NewGame("my title", 2, ["A", "B"]);
        Assert.NotEqual(nint.Zero, g);
        p = sharpgambit.GetPlayer(g, 1);
        Assert.Equal("A", sharpgambit.GetPlayerTitle(p));
        p = sharpgambit.GetPlayer(g, 2);
        Assert.Equal("B", sharpgambit.GetPlayerTitle(p));

    }
}
