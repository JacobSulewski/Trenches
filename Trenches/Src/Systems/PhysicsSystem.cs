using Trenches.Components;

namespace Trenches.Systems;
class PhysicsSystem : EntityProcessingSystem
{
    private ComponentMapper<Transform2> _transformMapper;
    private ComponentMapper<Physics> _physicsMapper;
    public PhysicsSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Physics))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _physicsMapper = mapperService.GetMapper<Physics>();
    }
    public override void Process(GameTime gameTime, int entityId)
    {
        var transform = _transformMapper.Get(entityId);
        var physics = _physicsMapper.Get(entityId);
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        transform.Position += physics.Velocity * elapsedTime;
        // See Physics Component for explanation
        //physics.Velocity += physics.Acceleration * elapsedTime;
    }
}