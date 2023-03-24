namespace Trenches.Systems;
class RenderSystem : EntityDrawSystem
{
    public SpriteBatch SpriteBatch { private get; init; }
    public OrthographicCamera Camera { private get; init; }

    private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;
    private ComponentMapper<Sprite> _spriteMapper;
    private ComponentMapper<Transform2> _transformMapper;

    public RenderSystem()
        : base(Aspect.All(typeof(Transform2)).One(typeof(AnimatedSprite), typeof(Sprite))) { }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        _spriteMapper = mapperService.GetMapper<Sprite>();
    }

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());

        foreach (var entity in ActiveEntities)
        {
            var sprite = _animatedSpriteMapper.Has(entity)
                ? _animatedSpriteMapper.Get(entity)
                : _spriteMapper.Get(entity);
            var transform = _transformMapper.Get(entity);

            if (sprite is AnimatedSprite animatedSprite)
                animatedSprite.Update(gameTime.GetElapsedSeconds());

            SpriteBatch.Draw(sprite, transform);

        }

        SpriteBatch.End();
    }
}
