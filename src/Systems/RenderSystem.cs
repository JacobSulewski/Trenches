using Trenches.Components;
using Trenches.Pathing;

namespace Trenches.Systems;
class RenderSystem : EntityDrawSystem
{
    private ComponentMapper<AnimatedSprite> _animatedSpriteMapper;
    private ComponentMapper<Sprite> _spriteMapper;
    private ComponentMapper<Transform2> _transformMapper;
    private ComponentMapper<Collider> _colliderMapper;
    public bool Debug;
    public GridGraph Grid 
        { private get; init; }
    required public SpriteBatch SpriteBatch 
        { private get; init; }
    required public OrthographicCamera Camera 
        { private get; init; }
    required public ContentManager Content
        { private get; init; }
    public RenderSystem()
        : base(Aspect.All(typeof(Transform2)).One(typeof(AnimatedSprite), typeof(Sprite), typeof(Collider))) { }
    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _animatedSpriteMapper = mapperService.GetMapper<AnimatedSprite>();
        _spriteMapper = mapperService.GetMapper<Sprite>();
        _colliderMapper = mapperService.GetMapper<Collider>();
    }
    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());

        foreach (var entity in ActiveEntities)
        {
            var transform = _transformMapper.Get(entity);
            var sprite = _animatedSpriteMapper.Has(entity)
                ? _animatedSpriteMapper.Get(entity)
                : _spriteMapper.Get(entity);

            if (sprite is AnimatedSprite animatedSprite)
                animatedSprite.Update(gameTime.GetElapsedSeconds());

            SpriteBatch.Draw(sprite, transform);

            if (Debug)
                _colliderMapper.Get(entity)?.Draw(SpriteBatch);
        }
        
        if (Debug)
        {
            Grid?.Draw(SpriteBatch);
            Utils.Utils.DrawText(Camera.Position.ToString(), Camera.Center, Color.Black, LayerDepth.Debug);
        }
        SpriteBatch.End();
    }
}
