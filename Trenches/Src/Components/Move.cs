namespace Trenches.Components;
class Move: UnitAction
{
    public readonly Vector2 Target; 
    public Move(Vector2 target) 
        => (Target) = (target); 
}