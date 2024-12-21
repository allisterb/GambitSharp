namespace SharpGambit.Tests.Api
{
    public class Game
    {
        [Fact]
        public void CanConstructNormalFormGame()
        {
            var g =new NormalFormGame("test", ["A",  "B"], [["Snitch", "Mute"], ["Snitch", "Mute", "Foo"]]);
            Assert.Equal("test", g.Title);
            
            Assert.Equal("A", g.Players.First().Label);
            Assert.Equal("B", g.Players.Last().Label);

            Assert.Equal(2, g.StrategyCounts[0]);
            Assert.Equal(3, g.StrategyCounts[1]);

            Assert.Equal("Snitch", g["B"][0].Label);
            Assert.Equal("Foo", g["B"][2].Label);
        }
    }
}