namespace Trenches.Components;
abstract class Build : Move
{
    public float Remaining;
    protected abstract float Duration 
        { get; }
    public bool IsComplete
        => Remaining < 0f;
    public float Progress
        => (1 - Remaining / Duration) * 100f;
    public Build(Vector2 target) 
        : base(target)
        => (Remaining) = (Duration);
}