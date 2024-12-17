namespace SharpGambit.Tests.Api
{
    public class Game
    {
        [Fact]
        public void CanConstructNormalFormGame()
        {
            var g =new NormalFormGame("test", ["A",  "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]]);
            Assert.Equal("test", g.Title);
            
            Assert.Equal("A", g.Players.First().Label);
            Assert.Equal("B", g.Players.Last().Label);
        }
    }
}