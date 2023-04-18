namespace Trenches.Components;
interface IState<T>
{
    T Context 
        { init; }
}

interface IUnitActions
{
    IUnitActions Stop();
    IUnitActions Move(Vector2 target);
    IUnitActions Build(Vector2 target);
    void Update(GameTime gameTime, Transform2 transform, Physics physics);
}

class UnitAction : IUnitActions
{
    public ActionState State;
    public IUnitActions Stop()
        => State.Stop();
    public IUnitActions Move(Vector2 target)
        => State.Move(target);
    public IUnitActions Build(Vector2 target)
        => State.Build(target);
    public void Update(GameTime gameTime, Transform2 transform, Physics physics)
        => State.Update(gameTime, transform, physics);

    public abstract class ActionState: IUnitActions, IState<UnitAction>
    {
        /*required*/ public UnitAction Context
            { private get; init; }
        protected ActionState CurrentState
        {
            get
                => Context.State;
            set
                => (PreviousState, Context.State) = (CurrentState, value);
        }
        protected ActionState PreviousState
            { get; private set;}
        private Dictionary<Type, ActionState> _stateCache = new();
        protected T Cache<T>()
            where T : ActionState, new()
        {
            var key = typeof(T);
            if (!_stateCache.ContainsKey(key))
                _stateCache[key] = new T {Context=Context};
            return (T)_stateCache[key];
        }
        public virtual IUnitActions Stop()
            => CurrentState;
        public virtual IUnitActions Move(Vector2 target)
            => CurrentState;
        public virtual IUnitActions Build(Vector2 target)
            => CurrentState;
        public virtual void Update(GameTime gameTime, Transform2 transform, Physics physics) { }
    }
}

class Idle: UnitAction.ActionState
{
    public override IUnitActions Move(Vector2 target)
        => CurrentState = (Moving)Cache<Moving>().Move(target);
    public override IUnitActions Build(Vector2 target)
        => CurrentState = (Building)Cache<Building>().Build(target);
}

class Moving: UnitAction.ActionState
{
    Vector2 _target;
    public override IUnitActions Stop()
        => CurrentState = (Idle)Cache<Idle>().Stop();
    public override IUnitActions Move(Vector2 target)
    {
        _target = target;
        return this;
    }
    public override IUnitActions Build(Vector2 target)
        => CurrentState = (Building)Cache<Building>().Build(target);
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

class Building: UnitAction.ActionState
{
    Vector2 _target;
    public override IUnitActions Stop()
        => CurrentState = (Idle)Cache<Idle>().Stop();
    public override IUnitActions Move(Vector2 target)
        => CurrentState = (Moving)Cache<Moving>().Move(target);
}

