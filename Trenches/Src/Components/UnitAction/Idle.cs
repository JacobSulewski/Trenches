namespace Trenches.Components.UnitActions;
class Idle: UnitAction.State
{
    public Idle(UnitAction context)
        : base(context) { }
    public override IUnitAction Move(Vector2 target)
        => CurrentState = (Moving)this[typeof(Moving)].Move(target);
    public override IUnitAction Build(Vector2 target)
        => CurrentState = (Building)this[typeof(Building)].Build(target);
}