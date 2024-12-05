namespace SharpGambit;

public abstract class GameObject
{
    protected GameObject(nint ptr)
    {
        this.ptr = ptr;
    }

    internal nint ptr;
}
