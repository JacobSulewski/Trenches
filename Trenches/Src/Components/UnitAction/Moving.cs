namespace Trenches.Components.UnitActions;
class Moving: UnitAction.State
{
    Vector2 _target;
    public Moving(UnitAction context)
        : base(context) { }
    public override IUnitAction Stop()
        => CurrentState = (Idle)this[typeof(Idle)].Stop();
    public override IUnitAction Move(Vector2 target)
    {
        _target = target;
        return this;
    }
    public override IUnitAction Build(Vector2 target)
        => CurrentState = (Building)this[typeof(Building)].Build(target);
    public override void Update(GameTime gameTime, Transform2 transform, Physics physics)
    {
        Vector2 direction = _target - transform.Position;
        physics.Velocity = direction * physics.Speed;

        if (direction.Length() < 1)
        {
            physics.Velocity = Vector2.Zero;
            CurrentState = PreviousState;
        }
    }
}