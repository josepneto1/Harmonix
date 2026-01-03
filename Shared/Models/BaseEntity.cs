namespace Harmonix.Shared.Models;

public abstract class BaseEntity
{
    public bool Removed { get; private set; }

    public virtual void Remove()
    {
        Removed = true;
    }

    public virtual void Activate()
    {
        Removed = false;
    }
}