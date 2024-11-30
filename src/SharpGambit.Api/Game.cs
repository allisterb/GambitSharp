namespace SharpGambit;

using gambit.Gambit;
public class Game
{
    public Game(GameRep rep)
    {
        Rep = rep.;
    }
    
    public GameRep Rep { get; set; }    
    
    
    public GamePlayerRep NewPlayer() => Rep.NewPlayer().Rep.;
    public static void NewTree() => new Game(game.NewTree().Rep);
    

    public string Title { get; protected set; } 
}
