namespace SharpGambit.Tests.Api
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var g =new NormalFormGame("test", ["A",  "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]]);
            Assert.Equal("B", g.Players[0].Label);
        }
    }
}