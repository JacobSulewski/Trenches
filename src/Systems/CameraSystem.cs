using Trenches.Components;

namespace Trenches.Systems;
class CameraSystem : EntityUpdateSystem
{
    private ComponentMapper<Transform2> _transformMapper;
    required public OrthographicCamera Camera 
        { private get; init; }
    public CameraSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Camera))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
    }
    public override void Update(GameTime gameTime)
    {
        foreach (var entityId in ActiveEntities)
        {
                    var transform = _transformMapper.Get(entityId);

        Camera.LookAt(transform.WorldPosition);
        }
        //float x = Math.Clamp(Camera.Position.X, 0, 10000);
        //float y = Math.Clamp(Camera.Position.Y, 0, 10000);
        //Camera.Position = new(x, y);
    }
}