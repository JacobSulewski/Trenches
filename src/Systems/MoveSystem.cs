using Trenches.Components;

namespace Trenches.Systems;
class MoveSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transformMapper;
    private ComponentMapper<Physics> _physicsMapper;
    private ComponentMapper<UnitCommand> _unitCommandMapper;
    public MoveSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics), typeof(UnitCommand))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _physicsMapper = mapperService.GetMapper<Physics>();
        _unitCommandMapper = mapperService.GetMapper<UnitCommand>();
    }
    public override void Process(GameTime gameTime, int entityId)
    {
        var moveCommand = (Move)_unitCommandMapper.Get(entityId);
        if (moveCommand is null) return;
        var transfrom = _transformMapper.Get(entityId); 
        var physics = _physicsMapper.Get(entityId);

        Vector2 direction = moveCommand.Target - transfrom.Position;
        if (direction.Length() < 3)
        {
            physics.Magnitude = 0;
            _unitCommandMapper.Delete(entityId);
        }
        physics.Direction = direction;
        physics.Magnitude = physics.Speed;
    }
}