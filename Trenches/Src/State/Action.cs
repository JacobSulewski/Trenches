namespace Trenches.State;
interface IState<T>
{
    T Context 
        { init; }
}

interface IActions
{
    IActions Stop();
    IActions Move(Vector2 target);
    IActions Build(Vector2 target);
}

class Action : IActions
{
    protected ActionState State;
    public IActions Stop()
        => State.Stop();
    public IActions Move(Vector2 target)
        => State.Move(target);
    public IActions Build(Vector2 target)
        => State.Build(target);

    public abstract class ActionState: IActions, IState<Action>
    {
        required public Action Context
            { protected get; init; }
        protected IActions Set<T>(T state)
            where T: ActionState
            => Context.State = state;
        public virtual IActions Stop()
            => Context;
        public virtual IActions Move(Vector2 target)
            => Context;
        public virtual IActions Build(Vector2 target)
            => Context;
    }
}

class Idle: Action.ActionState
{
    public override IActions Move(Vector2 target)
        => Set(new Moving{Context=Context}).Move(target);
}

class Moving: Action.ActionState
{
    Vector2 _target;
    public override IActions Stop()
        => Set(new Idle{Context=Context});
    public override IActions Move(Vector2 target)
    {
        _target = target;
        return Context;
    }
}

class Building: Action.ActionState
{
    public override IActions Stop()
        => Set(new Idle{Context=Context});
}

