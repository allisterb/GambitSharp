namespace SharpGambit.Tests.Api
{
    public class Game
    {
        [Fact]
        public void CanConstructNormalFormGame()
        {
            var g = new NormalFormGame("test", ["A", "B"], [["Snitch", "Mute"], ["Snitch", "Mute", "Foo"]]);
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
            var g = new NormalFormGame("test", ["A", "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]], new[,] { { (4, 5), (7, 8) }, { (4, 5), (7, 8) } });
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
            var x = sp[0];
            Assert.Equal(4, (int) x);
            Assert.Equal(5, (int) sp[1]);
            Assert.Equal(5, (int) g["Mute", "Snitch"][1]);

            var h = new NormalFormGame("test", ["A", "B"], [["Snitch", "Mute"], ["Snitch", "Mute"]], new[,] { { (4, 5), (7, 8) }, { (4, 5), (7, 8) } });

        }

        [Fact]
        public void CanConstructTwoPlayerNormalFormGame()
        {
            var g = NormalFormGame.TwoPlayerGame("Prisoner's Dilemna", ["Fink", "Cheat"], ["Fink", "Cheat"],
                [[(0, 1), (2, 3)], [(4, 5), (6, 7)]]);
            Assert.Equal("Player 2", g[1].Label);
            Assert.Equal(7, (int) g["Cheat", "Cheat"][1]);

            //g = NormalFormGame.SymmetricTwoPlayerGame("Prisoner's Dilemna", "Player 1", "Player2", ["Finck Cheat"], [(0, 1), (2, 3)]);
            //Assert.Equal(3, g["Cheat", "Finck"][0]);
        }

        [Fact]
        public void CanConstructMixedStrategyProfile()
        {
            var g = NormalFormGame.TwoPlayerGame("Prisoner's Dilemna", ["Fink", "Cheat"], ["Fink", "Cheat"],
                [
                    [(3, 3), (2, 2)],
                    [(4, 5), (6, 7)]
                ]);
            var mp = g.NewMixedStrategyProfile();
            Assert.Equal(0.5, mp[0]["Fink"]);

            mp[0, "Fink"] = 0.4;
            Assert.Equal(0.4, mp[0]["Fink"]);
            Assert.Equal(2, mp[g[0]]);
            g = NormalFormGame.TwoPlayerGame("Prisoner's Dilemna", ["A", "B", "C"], ["D", "E", "F"],
                [[(0, 1), (2, 3), (2, 3)], [(4, 5), (6, 7), (2, 3)], [(4, 5), (6, 7), (2, 3)]]);
            mp = g.NewMixedStrategyProfile();
            Assert.Equal(0.333, mp[0]["A"], 0.001);
            mp[0, "A"] = 0.5;
            Assert.Equal(2, mp[g[0]]);
        }


        [Fact]
        public void CanGetHtml()
        {
            var g = NormalFormGame.TwoPlayerGame("Prisoner's Dilemna", ["Fink", "Cheat"], ["Fink", "Cheat"],
                [
                    [(3, 3), (2, 2)],
                    [(4, 5), (6, 7)]
                ]);
            var l = g.Html;
            Assert.NotNull(l);  
        }
    }
}