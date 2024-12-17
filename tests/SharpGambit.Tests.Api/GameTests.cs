namespace SharpGambit.Tests.Api
{
    public class Game
    {
        [Fact]
        public void CanConstructNormalFormGame()
        {
            var g =new NormalFormGame("test", ["A",  "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]]);
            Assert.Equal("test", g.Title);
            Assert.Equal("A", g.Players[0].Label);
            Assert.Equal("B", g.Players[1].Label);
        }
    }
}