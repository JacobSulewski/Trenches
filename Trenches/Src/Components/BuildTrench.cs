namespace Trenches.Components;
class BuildTrench: Build
{
    protected override float Duration
        => 3f;
    public BuildTrench(Vector2 target)
        : base(target) { }
}