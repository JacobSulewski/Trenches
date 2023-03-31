using Trenches.Components;

namespace Trenches.Systems;
class MoveSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Physics> _physics;
    private ComponentMapper<UnitCommand> _unitCommands;
    public MoveSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics), typeof(UnitCommand))) { }
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms, _physics, _unitCommands) =
            (mapperService.Get<Transform2, Physics, UnitCommand>());
    public override void Process(GameTime gameTime, int entityId)
    {
        var command = _unitCommands.Get(entityId);
        var transfrom = _transforms.Get(entityId); 
        var physics = _physics.Get(entityId);

        if (command is not Move move) return;
        Vector2 direction = move.Target - transfrom.Position;
        physics.Velocity = direction * physics.Speed;

        if (direction.Length() < 1)
        {
            physics.Velocity = Vector2.Zero;
            _unitCommands.Delete(entityId);
        }
    }
}