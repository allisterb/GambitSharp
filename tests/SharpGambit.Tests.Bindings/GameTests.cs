namespace SharpGambit.Tests.Bindings;

using gambit;
using System.Runtime.InteropServices;

public class GameTests
{
    [Fact]
    public void Test1()
    {
        var g = sharpgambit.NewStrategicGame("foo", 1, ["A"], [1]);
        var p = sharpgambit.AddPlayerToGame(g);
        Assert.NotEqual(p, nint.Zero);
        sharpgambit.SetPlayerLabel(p, "foo");
        Assert.Equal("foo", sharpgambit.GetPlayerLabel(p));
        var s = sharpgambit.NewPlayerStrategy(p);
        var n = sharpgambit.GetPlayerStrategies(p);

        Assert.Equal(2, sharpgambit.GetPlayerStrategies(p));
        //var n = p.Rep.NewPlayer();
        //Marshal.
        //Assert.NotEqual(g, nint.Zero);

        /*
        g = sharpgambit.NewGame("untitled game", 0, null);
        Assert.NotEqual(nint.Zero, g);
        p = sharpgambit.AddPlayerToGame(g);
        Assert.NotEqual(p, nint.Zero);
        g = sharpgambit.NewGame("my title", 2, ["A", "B"]);
        Assert.NotEqual(nint.Zero, g);
        p = sharpgambit.GetPlayer(g, 1);
        Assert.Equal("A", sharpgambit.GetPlayerLabel(p));
        p = sharpgambit.GetPlayer(g, 2);
        Assert.Equal("B", sharpgambit.GetPlayerLabel(p));

        var s = sharpgambit.NewPlayerStrategy(p);
        Assert.Equal(1, sharpgambit.GetPlayerStrategies(p));
        */
    }
}
