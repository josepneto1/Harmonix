namespace Harmonix.Shared.Models;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool Removed { get; private set; } = false;

    public virtual void Remove() => Removed = true;

    public virtual void Restore() => Removed = false;
}
