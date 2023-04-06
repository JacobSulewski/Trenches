namespace Trenches.Systems;
class WorldSystem : EntityUpdateSystem
{
    ComponentMapper<Transform2> _transforms;
    Map _map;
    public OrthographicCamera Camera
        { private get; init;}
    public WorldSystem(Map map)
        : base(Aspect.All(typeof(Transform2)))
        => (_map) = (map);
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms) = (mapperService.Get<Transform2>());
    public override void Update(GameTime gameTime)
    {
        foreach (var entity in ActiveEntities)
        {
            var transform = _transforms.Get(entity);
            transform.ClampWithin(_map.Bounds);
        }
        Camera.ClampWithin(_map.Bounds);
    }
}