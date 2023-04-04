using Trenches.Commands;

namespace Trenches.Components;
abstract class UnitCommand : ICommand
{
    public abstract float Progress { get; }
    public abstract bool IsComplete { get; }
    public abstract void Execute();
}

