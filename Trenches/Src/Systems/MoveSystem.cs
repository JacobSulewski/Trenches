using Trenches.Components;

namespace Trenches.Systems;
class MoveSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Physics> _physics;
    private ComponentMapper<UnitAction> _unitActions;
    public MoveSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics), typeof(UnitAction))) { }
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms, _physics, _unitActions) =
            (mapperService.Get<Transform2, Physics, UnitAction>());
    public override void Process(GameTime gameTime, int entityId)
    {
        var action = _unitActions.Get(entityId);
        var transfrom = _transforms.Get(entityId); 
        var physics = _physics.Get(entityId);

        if (action is not Move move) return;
        Vector2 direction = move.Target - transfrom.Position;
        physics.Velocity = direction * physics.Speed;

        if (direction.Length() < 1)
            physics.Velocity = Vector2.Zero;
    }
}