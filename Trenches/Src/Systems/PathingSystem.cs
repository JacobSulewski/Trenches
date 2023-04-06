using Trenches.Components;

namespace Trenches.Systems;
class PathingSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Physics> _physics;
    private ComponentMapper<UnitAction> _unitActions;
    public PathingSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics), typeof(UnitAction))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transforms = mapperService.GetMapper<Transform2>();
        _physics = mapperService.GetMapper<Physics>();
        _unitActions = mapperService.GetMapper<UnitAction>();
    }
    public override void Process(GameTime gameTime, int entityId)
    {
        var moveCommand = (Move)_unitActions.Get(entityId);
        if (moveCommand is null) return;
        var transfrom = _transforms.Get(entityId); 
        var physics = _physics.Get(entityId);

        Vector2 direction = moveCommand.Target - transfrom.Position;
        if (direction.Length() < 3)
        {
            physics.Magnitude = 0;
            _unitActions.Delete(entityId);
        }
        physics.Direction = direction;
        physics.Magnitude = physics.Speed;
    }
}