namespace SharpGambit;

using gambit;

public class Game : GameObject
{
    protected Game(nint ptr) : base(ptr) {}

    public Game() : this(sharpgambit.NewEmptyGame()) {}

    public Game(string title, params string[] players): base(sharpgambit.NewGame(title, players.Length, players)) {}
    
    public Player NewPlayer() => new Player(this);
}
