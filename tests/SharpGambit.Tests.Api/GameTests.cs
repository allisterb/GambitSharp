namespace SharpGambit.Tests.Api
{
    public class Game
    {
        [Fact]
        public void CanConstructNormalFormGame()
        {
            var g = new NormalFormGame("test", ["A",  "B"], [["Snitch", "Mute"], ["Snitch", "Mute", "Foo"]]);
            Assert.Equal("test", g.Title);
            
            Assert.Equal("A", g.Players.First().Label);
            Assert.Equal("B", g.Players.Last().Label);

            Assert.Equal(2, g.StrategyCounts[0]);
            Assert.Equal(3, g.StrategyCounts[1]);

            Assert.Equal("Snitch", g[1][0].Label);
            Assert.Equal("Foo", g[1][2].Label);

            var sp = g["Mute", "Foo"];
            Assert.Equal("Mute", sp.GetStrategy(0).Label);
            Assert.Equal("Foo", sp.GetStrategy(1).Label);
            sp = g["Mute", "Foo"];
        }

        [Fact]
        public void CanConstructNormalFormGameWithPayoffs()
        {
            var g = new NormalFormGame("test", ["A", "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]], new [,] { { (4, 5), (7, 8) }, { (4, 5), (7, 8) } });
            Assert.Equal("test", g.Title);

            Assert.Equal("A", g.Players.First().Label);
            Assert.Equal("B", g.Players.Last().Label);

            Assert.Equal(2, g.StrategyCounts[0]);
            Assert.Equal(2, g.StrategyCounts[1]);

            Assert.Equal("Snitch", g[1][0].Label);
            Assert.Equal("Mute", g[1][1].Label);

            var sp = g["Mute", "Snitch"];
            Assert.Equal("Mute", sp.GetStrategy(0).Label);
            Assert.Equal("Snitch", sp.GetStrategy(1).Label);
            sp = g["Mute", "Snitch"];
            Assert.Equal(4, sp[0]);
            Assert.Equal(5, sp[1]);
        }
    }
}