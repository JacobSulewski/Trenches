namespace Trenches.Components;
class BoxCollider : Collider
{
    Sprite _sprite;
    float? _width;
    float? _height;
    public float Width
        => _width ?? _sprite.GetBoundingRectangle(this).Width;
    public float Height
        => _height ?? _sprite.GetBoundingRectangle(this).Width;
    public Size2 Size
        => new(Width, Height);
    public RectangleF BoundingRectangle 
        => new(Position - new Vector2(Width, Height) / 2, new(Width, Height));
    public override IShapeF Bounds 
        => BoundingRectangle;
    public BoxCollider(Size2 size) 
        => ((_width, _height), Position) = (size, Vector2.Zero);
    public BoxCollider(Sprite sprite)
        => (_sprite, Position) = (sprite, Vector2.Zero);
    public override void OnCollision(CollisionEventArgs collisionInfo)
    {
        System.Console.WriteLine("Collided: " + collisionInfo);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        // TODO Thickness should scale with camera zoom
        spriteBatch.DrawRectangle(BoundingRectangle, Color, 1.5f, LayerDepth.Debug);
    }
}