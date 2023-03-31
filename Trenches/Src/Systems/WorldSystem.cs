namespace Trenches.Systems;
class WorldSystem : EntityUpdateSystem
{
    private ComponentMapper<Transform2> _transforms;
    public readonly Size2 Size;
    public readonly int Width;
    public readonly int Height;
    public OrthographicCamera Camera
        { private get; init;}
    public WorldSystem(int width, int height)
        : base(Aspect.All(typeof(Transform2)))
        => (Width, Height) = (width, height);
    public override void Initialize(IComponentMapperService mapperService)
        => (_transforms) = (mapperService.Get<Transform2>());
    public override void Update(GameTime gameTime)
    {
        float x, y;
        foreach (var entity in ActiveEntities)
        {
            var transform = _transforms.Get(entity);
            transform.Clamp(new(0, 0, Width, Height));
        }
        Camera.Clamp(new(0, 0, Width, Height));
    }
}