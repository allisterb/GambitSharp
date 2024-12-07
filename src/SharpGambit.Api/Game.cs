namespace SharpGambit;

using gambit;
using System.Linq;

public class Game : GameObject
{
    protected Game(nint ptr) : base(ptr) {}
    public Player NewPlayer() => new Player(this);
}

public class StrategicFormGame : Game
{
    public StrategicFormGame(string title, string[] players, string[][] strategies) : 
        base(game.NewStrategicFormGame(title, players.Length, players, strategies.Select(s => s.Length).ToArray()))
    {
        for (int i = 0; i < players.Length; i++)
        {
            var p = game.GetPlayer(ptr, i + 1);
            for (int j = 0; j < strategies[i].Length; i++)
            {
                var s = player.GetPlayerStrategy(p, j + 1);
                strategy.SetPlayerStrategyLabel(s, strategies[i][j]);
            }
            
        }
    }
}
