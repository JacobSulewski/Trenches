namespace Trenches.Components;
class Move : UnitCommand
{
    public readonly Point2 Target;
    public Move(Point2 target) 
        => (Target) = (target); 
}