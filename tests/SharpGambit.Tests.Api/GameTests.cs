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

            var sp = g["Snitch", "Foo"];
            Assert.Equal("Snitch", sp[0].Label);
        }
    }
}