namespace SharpGambit;

using gambit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class Game : GameObject
{
    protected Game(nint ptr) : base(ptr) {}
    
    public string Title
    {
        get => game.GetTitle(ptr);
        set => game.SetTitle(value, ptr);
    }

    public int PlayerCount => game.NumPlayers(ptr); 

    public IEnumerable<Player> Players
    {
        get
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                yield return new Player(this, game.GetPlayer(ptr, i + 1));
            }
        }
    }

    public IEnumerable<PureStrategy[]> Strategies
    {
        get
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                yield return new Player(this, game.GetPlayer(ptr, i + 1)).Strategies.ToArray();
            }
        }
    }

    public static bool HtmlLatexMode { get; set; }  = false;

    public int[] StrategyCounts => Players.Select(p => p.StrategyCount).ToArray();

    public string Latex => game.GetLatex(ptr);

    public virtual string Html => game.GetHtml(ptr);

    public Player this[int pl] => GetPlayer(pl);

    public Player this[string label] => GetPlayer(label);

    public Player GetPlayer(int player) => new Player(this, game.GetPlayer(ptr, FailIfPlayerIndexTooLarge(player) + 1));

    public Player GetPlayer(string label) => Players.SingleOrFailure(p => p.Label == label, $"The player with label {label} does not exist in the game.");

    public Player AddPlayer() => new Player(this);

    internal int FailIfPlayerIndexTooLarge(int pl) => pl >= PlayerCount ? throw new ArgumentException($"This game has {PlayerCount} players.") : pl;  
}

public class NormalFormGame : Game
{
    public NormalFormGame(string title, string[] players, string[][] strategies, Array? payoffs = null) :
        base(game.NewNormalFormGame(title, players.Length, players, strategies.Select(s => s.Length).ToArray()))
    {
        if (strategies.Length != players.Length) throw new ArgumentException("The rank of the strategies array must equal the number of players.");
        for (int i = 0; i < players.Length; i++)
        {
            var p = game.GetPlayer(ptr, i + 1);
            for (int j = 0; j < strategies[i].Length; j++)
            {
                var s = player.GetPlayerStrategy(p, j + 1);
                strategy.SetStrategyLabel(s, strategies[i][j]);
            }
        }
        if (payoffs is not null && payoffs.Length > 0)
        {
            var sdims = strategies.Select(s => s.Length).ToArray();
            if (!(payoffs.GetType().GetElementType()?.IsAssignableTo(typeof(ITuple)) ?? false))
            {
                throw new ArgumentException("The outcomes array must have a tuple element type.");
            }
            if (payoffs.Rank == 1)
            {
                if (payoffs.Length != sdims.Product()) throw new ArgumentException("The length of a 1-dimensional payoffs array must equal the total number of strategy combinations.");
            }
            else if (payoffs.Rank != PlayerCount)
            {
                throw new ArgumentException("The rank of the payoffs array must equal the number of players.", nameof(payoffs));
            }
            else
            {
                for (int i = 0; i < strategies.Length; i++)
                {
                    if (payoffs.GetLength(i) != sdims[i])
                    {
                        throw new ArgumentException($"Dimension {i} of the payoffs array has length {payoffs.GetLength(i)}, not {sdims[i]}.");
                    }
                }
            }
            var p = (ITuple)payoffs.GetValue(payoffs.GetIndices(0))!;
            if (p.Length != PlayerCount)
            {
                throw new ArgumentException("Each element of the payoffs array must have length equal to the number of players.");
            }

            if (payoffs.Rank == PlayerCount)
            {
                for (int i = 0; i < payoffs.Length; i++)
                {
                    var indices = payoffs.GetIndices(i);
                    var profile = new PureStrategyProfile(this, indices);
                    p = (ITuple)payoffs.GetValue(indices)!;
                    profile.Outcome = new Outcome(this, p);
                }
            }
            else
            {
                for (int i = 0; i < payoffs.Length; i++)
                {
                    var indices = payoffs.GetIndices(i, sdims);
                    var profile = new PureStrategyProfile(this, indices);
                    p = (ITuple)payoffs.GetValue(i)!;
                    profile.Outcome = new Outcome(this, p);
                }
            }
        }
    }

    public PureStrategyProfile this[params PureStrategy[] strategies]
    {
        get
        {
            if (strategies.Length != PlayerCount) throw new ArgumentException("The number of strategies specified must equal the the number of players.");
            var psp = new PureStrategyProfile(this, strategies);
            return psp;
        }
    }

    public MixedStrategyProfile this[params (string, double)[] probs] => this.NewMixedStrategyProfile(probs);

    public PureStrategyProfile this[params string[] strategies] => this[strategies.Select((s, i) => this[i][s]).ToArray()];

    public MixedStrategyProfile NewMixedStrategyProfile() => new MixedStrategyProfile(this);

    public MixedStrategyProfile NewMixedStrategyProfile((string, double)[][] probs) => new MixedStrategyProfile(this, probs);

    public MixedStrategyProfile NewMixedStrategyProfile((string, double)[] probs) => new MixedStrategyProfile(this, probs);

    public override string Html
    {
        get
        {
            if (PlayerCount != 2) return base.Html;
            int p = PlayerCount;
            string sstag = HtmlLatexMode ? "$$" : "<tt>";
            string setag = HtmlLatexMode ? "$$" : "</tt>";
            StringBuilder html = new StringBuilder();
            html.AppendLine($"<div class=\"nfg_{p}p\">");
            html.AppendLine($"<div class=\"title\" style=\"text-align:center\">{Title}</div>");
            html.AppendLine($"<div style=\"float:left;margin-top:35pt;margin-right:15pt\"><b>{this[0].Label}</b></div>");
            html.AppendLine($"<div style=\"margin-left:75pt\"><b>{this[1].Label}</b></div>");
            html.AppendLine("<table style=\"border: 1px solid black; border-collapse: collapse;\">");
            html.AppendLine("<tbody>");
            html.AppendLine("<tr>");
            html.AppendLine("<td></td>");
            for (int j = 0; j < this[1].StrategyCount; j++)
            {
                html.AppendLine($"<td>{sstag}{this[1][j].Label}{setag}</td>");
            }
            html.AppendLine("</tr>");
           
            for (int i = 0; i < this[0].StrategyCount; i++)
            {
                html.AppendLine("<tr>");
                var s1 = this[0][i].Label;
                html.AppendLine($"<td>{sstag}{s1}{setag}</td>");
                for (int j = 0; j < this[1].StrategyCount; j++)
                {
                    var s2 = this[1][j].Label;
                    var pr = this[s1, s2];
                    html.Append($"<td align=\"center\">({pr[0]},{pr[1]})</td>");
                }
                html.AppendLine("</tr>");
            }
            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div>");
            return html.ToString();
        }
    }
    public static NormalFormGame TwoPlayerGame(string title, string[] strategies1, string[] strategies2, ITuple[][]? payoffs = null, string player1 = "Player 1", string player2 = "Player 2") =>
        new NormalFormGame(title, [player1, player2], [strategies1, strategies2], payoffs?.To2D());

    public static NormalFormGame TwoPlayerGame(string title, string[] strategies1, string[] strategies2, ITuple[] payoffs, string player1 = "Player 1", string player2 = "Player 2") =>
        new NormalFormGame(title, [player1, player2], [strategies1, strategies2], payoffs);
    //public static NormalFormGame SymmetricTwoPlayerGame(string title, string player1, string player2, string[] strategies, ITuple[] payoffs) =>
    //    NormalFormGame.TwoPlayerGame(title, player1, player2, [strategies, strategies], [payoffs, payoffs.Permute<Rational>()]);


}
