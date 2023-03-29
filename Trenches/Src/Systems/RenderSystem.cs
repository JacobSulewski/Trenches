using Trenches.Components;
using Trenches.Pathing;

namespace Trenches.Systems;
class RenderSystem : EntityDrawSystem
{
    private ComponentMapper<AnimatedSprite> _animatedSprites;
    private ComponentMapper<Sprite> _sprites;
    private ComponentMapper<Transform2> _transforms;
    private ComponentMapper<Collider> _colliders;
    public bool Debug;
    public Grid<int> Grid 
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
        => (_transforms, _animatedSprites, _sprites, _colliders) = 
            (mapperService.Get<Transform2, AnimatedSprite, Sprite, Collider>());

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Camera.GetViewMatrix());

        foreach (var entity in ActiveEntities)
        {
            var transform = _transforms.Get(entity);
            var sprite = _animatedSprites.Has(entity)
                ? _animatedSprites.Get(entity)
                : _sprites.Get(entity);

            if (sprite is AnimatedSprite animatedSprite)
                animatedSprite.Update(gameTime.GetElapsedSeconds());

            SpriteBatch.Draw(sprite, transform);

            if (Debug)
                _colliders.Get(entity)?.Draw(SpriteBatch);
        }
        
        if (Debug)
        {
            Grid?.Draw(SpriteBatch);
            SpriteBatch.DrawText(Camera.Position.ToString(), Camera.Position, Color.Black, LayerDepth.Debug);
        }
        SpriteBatch.End();
    }
}
