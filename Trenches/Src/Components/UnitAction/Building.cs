namespace Trenches.Components.UnitActions;
class Building: UnitAction.State
{
    Vector2 _target;
    public Building(UnitAction context)
        : base(context) { }
    public override IUnitAction Stop()
        => CurrentState = (Idle)this[typeof(Idle)].Stop();
    public override IUnitAction Move(Vector2 target)
        => CurrentState = (Moving)this[typeof(Moving)].Move(target);
}