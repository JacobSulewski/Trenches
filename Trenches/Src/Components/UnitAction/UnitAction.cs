using System.Reflection;

namespace Trenches.Components.UnitActions;
class UnitAction : IUnitAction
{
    private State CurrentState;
    public IUnitAction Stop()
        => CurrentState.Stop();
    public IUnitAction Move(Vector2 target)
        => CurrentState.Move(target);
    public IUnitAction Build(Vector2 target)
        => CurrentState.Build(target);
    public void Update(GameTime gameTime, Transform2 transform, Physics physics)
        => CurrentState.Update(gameTime, transform, physics);

    public abstract class State: IUnitAction
    {
        Dictionary<Type, State> _stateCache = new();
        UnitAction _context;

        protected State CurrentState
        {
            get
                => _context.CurrentState;
            set
                => (PreviousState, _context.CurrentState) = (CurrentState, value);
        }
        protected State PreviousState
            { get; private set;}
        protected State this[Type type]
        {
            get
            {
                if (!_stateCache.ContainsKey(type))
                {
                    _stateCache[type] = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { Context }, null) as ActionState;
                }
                return _stateCache[type];
            }
        }

        protected State (UnitAction context)
            => _context = context;

        public virtual IUnitAction Stop()
            => CurrentState;
        public virtual IUnitAction Move(Vector2 target)
            => CurrentState;
        public virtual IUnitAction Build(Vector2 target)
            => CurrentState;
        public virtual void Update(GameTime gameTime, Transform2 transform, Physics physics) { }
    }
}
