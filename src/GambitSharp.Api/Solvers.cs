namespace GambitSharp;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using gambit;

public struct PureStrategySolution
{
    public PureStrategySolution(Game game, MixedStrategyProfile[] profiles)
    {
        this.game = game;   
        this.profiles = profiles;
        this.solutions =
            profiles
            .Select(p => p.game.Strategies
            .SelectMany(s => s)
            .Where(s => p.GetStrategyProbability(s) == 1.0)
            .ToArray())
            .ToArray();
    }

    public string Html
    {
        get
        {
            switch (this.game)
            {
                case NormalFormGame nfg:
                    int p = nfg.PlayerCount;
                    if (p != 2) throw new NotImplementedException();
                    if (solutions.Length == 0) return $"{game.Title} has no pure strategy solutions.";
                    StringBuilder html = new StringBuilder();
                    string sstag = Game.HtmlLatexMode ? "$$" : "<tt>";
                    string setag = Game.HtmlLatexMode ? "$$" : "</tt>";
                    string borderstyle = "style =\"border: 1px solid black; border-collapse: collapse;\"";
                    string solborderstyle = "style =\"border: 1px solid black; border-collapse: collapse;background-color:lightblue\"";
                    html.AppendLine($"<div class=\"nfg_{p}p\">");
                    html.AppendLine($"<div class=\"title\" style=\"text-align:center\">{nfg.Title} - {solutions.Length} pure strategy solution(s)</div>");
                    html.AppendLine($"<div style=\"float:left;margin-top:35pt;margin-right:15pt\"><b>{nfg[0].Label}</b></div>");
                    html.AppendLine($"<div style=\"margin-left:75pt\"><b>{nfg[1].Label}</b></div>");
                    html.AppendLine($"<table>");
                    html.AppendLine("<tbody>");
                    html.AppendLine($"<tr >");
                    html.AppendLine($"<td {borderstyle}></td>");
                    for (int j = 0; j < nfg[1].StrategyCount; j++)
                    {
                        html.AppendLine($"<td {borderstyle}>{sstag}{nfg[1][j].Label}{setag}</td>");
                    }
                    html.AppendLine("</tr>");

                    for (int i = 0; i < nfg[0].StrategyCount; i++)
                    {
                        html.AppendLine("<tr>");
                        var s1 = nfg[0][i].Label;
                        html.AppendLine($"<td {borderstyle}>{sstag}{s1}{setag}</td>");
                        for (int j = 0; j < nfg[1].StrategyCount; j++)
                        {
                            var s2 = nfg[1][j].Label;
                            var pr = nfg[s1, s2];
                            bool issol = false;
                            for (int k1 = 0; k1 < solutions.Length; k1++)
                            {
                                if (i == solutions[k1][0].Index && j == solutions[k1][1].Index)
                                {
                                    html.Append($"<td align=\"center\" {solborderstyle}>(<u>{pr[0]}</u>,<u>{pr[1]}</u>)</td>");
                                    issol = true;
                                    break;
                                }
                            }
                            if (!issol)
                            {
                                html.Append($"<td {borderstyle} align=\"center\">({pr[0]},{pr[1]})</td>");
                            }
                        }
                        html.AppendLine("</tr>");
                    }
                    html.AppendLine("</tbody>");
                    html.AppendLine("</table>");
                    html.AppendLine("</div>");
                    return html.ToString();
                
                default: throw new NotImplementedException();
            }
        }
    }
    internal MixedStrategyProfile[] profiles;
    internal Game game;
    public PureStrategy[][] solutions;
}

public struct MixedStrategySolution
{
    public MixedStrategySolution(Game game, MixedStrategyProfile[] profiles)
    {
        this.game = game;
        this.profiles = profiles;
        solutions = profiles
            .Select(p => p.game.Strategies
            .Select(ss => ss.Select(s => (s,p.GetStrategyProbabilityRational(s))).ToArray()).ToArray()).ToArray();
    }

    public string Html
    {
        get
        {
            switch (this.game)
            {
                case NormalFormGame nfg:
                    int p = nfg.PlayerCount;
                    if (p != 2) throw new NotImplementedException();
                    if (solutions.Length == 0) return $"{game.Title} has no mixed strategy solutions.";
                    var p1 = solutions[0][0];
                    var p2 = solutions[0][1];  
                    StringBuilder html = new StringBuilder();
                    string sstag = Game.HtmlLatexMode ? "$$" : "<tt>";
                    string setag = Game.HtmlLatexMode ? "$$" : "</tt>";
                    string borderstyle = "style =\"border: 1px solid black; border-collapse: collapse;\"";
                    html.AppendLine($"<div class=\"nfg_{p}p\">");
                    html.AppendLine($"<div class=\"title\" style=\"text-align:center\">{nfg.Title} - {solutions.Length} mixed strategy solution(s)</div>");
                    html.AppendLine($"<div style=\"float:left;margin-top:35pt;margin-right:15pt\"><b>{nfg[0].Label}</b></div>");
                    html.AppendLine($"<div style=\"margin-left:75pt\"><b>{nfg[1].Label}</b></div>");
                    html.AppendLine($"<table>");
                    html.AppendLine("<tbody>");
                    html.AppendLine($"<tr >");
                    html.AppendLine($"<td {borderstyle}></td>");
                    for (int j = 0; j < nfg[1].StrategyCount; j++)
                    {
                        html.AppendLine($"<td {borderstyle}>{sstag}{nfg[1][j].Label}{setag}<sup>{p2[j].Item2.ToLatex()}</sup></td>");
                    }
                    html.AppendLine("</tr>");

                    for (int i = 0; i < nfg[0].StrategyCount; i++)
                    {
                        html.AppendLine("<tr>");
                        var s1 = nfg[0][i].Label;
                        html.AppendLine($"<td {borderstyle}>{sstag}{s1}{setag}<sup>{p1[i].Item2.ToLatex()}</sup></td>");
                        for (int j = 0; j < nfg[1].StrategyCount; j++)
                        {
                            var s2 = nfg[1][j].Label;
                            var pr = nfg[s1, s2];
                            var e1 = p1[i].Item2 * pr[0];
                            var e2 = p1[j].Item2 * pr[1];
                            
                            html.Append($"<td align=\"center\" {borderstyle}>({pr[0]},{pr[1]}) (<u>{e1.ToLatex()}</u>,<u>{e2.ToLatex()}</u>)</td>");
                        }
                        html.AppendLine("</tr>");
                    }
                    html.AppendLine("</tbody>");
                    html.AppendLine("</table>");
                    html.AppendLine("</div>");
                    return html.ToString();

                default: throw new NotImplementedException();
            }
        }
    }
    internal MixedStrategyProfile[] profiles;
    internal Game game;
    public (PureStrategy, Rational)[][][] solutions;
}

public class Solvers
{
    public static PureStrategySolution EnumPureStrategySolve(Game g) => 
        new PureStrategySolution(g, NativeHelpers.EnumPureStrategySolve(g.ptr).Select(p => new MixedStrategyProfile(g, p)).ToArray());

    public static MixedStrategySolution EnumMixedStrategySolve(Game g) =>
        new MixedStrategySolution(g, NativeHelpers.EnumMixedStrategySolve(g.ptr).Select(p => new MixedStrategyProfile(g, p)).ToArray());
}

