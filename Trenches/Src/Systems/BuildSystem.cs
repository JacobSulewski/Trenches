using Trenches.Components;

namespace Trenches.Systems;
class BuildSystem: EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<UnitAction> _unitActions;
    public BuildSystem()
        : base(Aspect.All(typeof(Transform2), typeof(UnitAction))) { }
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms, _unitActions) =
            (mapperService.Get<Transform2, UnitAction>());
    public override void Process(GameTime gameTime, int entityId)
    {
        var action = _unitActions.Get(entityId);
        var transfrom = _transforms.Get(entityId); 

        if (action is not Build build) return;
        build.Remaining -= gameTime.GetElapsedSeconds();
    }
}