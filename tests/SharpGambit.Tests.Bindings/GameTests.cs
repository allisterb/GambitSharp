namespace SharpGambit.Tests.Bindings;

using gambit.Gambit;

public class GameTests
{
    [Fact]
    public void Test1()
    {
        var p = game.NewTree();
        var n = p.Rep.NewPlayer();
        Assert.NotNull(n);



    }
}
