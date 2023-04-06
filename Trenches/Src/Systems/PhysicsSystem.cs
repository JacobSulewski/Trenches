using Trenches.Components;

namespace Trenches.Systems;
class PhysicsSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Physics> _physics;
    public PhysicsSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics))) { }
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms, _physics) = (mapperService.Get<Transform2, Physics>());
    public override void Process(GameTime gameTime, int entityId)
    {
        var transform = _transforms.Get(entityId);
        var physics = _physics.Get(entityId);

        transform.Position += physics.Velocity * gameTime.GetElapsedSeconds();
        physics.Velocity += physics.Acceleration * gameTime.GetElapsedSeconds();
    }
}