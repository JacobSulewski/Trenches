using Trenches.Components;

namespace Trenches.Systems;
class UnitActionSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Physics> _physics;
    private ComponentMapper<UnitAction> _unitActions;
    public UnitActionSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics), typeof(UnitAction))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transforms = mapperService.GetMapper<Transform2>();
        _physics = mapperService.GetMapper<Physics>();
        _unitActions = mapperService.GetMapper<UnitAction>();
    }
    public override void Process(GameTime gameTime, int entityId)
    {
        var unitAction = _unitActions.Get(entityId);
        var transfrom = _transforms.Get(entityId); 
        var physics = _physics.Get(entityId);
        unitAction.up
    }
}