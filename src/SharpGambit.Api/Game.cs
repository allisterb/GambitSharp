namespace SharpGambit;

using gambit;
using System.Linq;

public class Game : GameObject
{
    protected Game(nint ptr) : base(ptr) {}
    public Player NewPlayer() => new Player(this);
}

public class StrategicGame : Game
{
    public StrategicGame(string title, string[] players, string[][] strategies) : 
        base(sharpgambit.NewStrategicGame(title, players.Length, players, strategies.Select(s => s.Length).ToArray()))
    {
        for (int i = 0; i < players.Length; i++)
        {
            var p = sharpgambit.GetPlayer(ptr, i + 1);
            for (int j = 0; j < strategies[i].Length; i++)
            {
                var s = sharpgambit.GetPlayerStrategy(p, j + 1);
            }
            
        }
    }
}
