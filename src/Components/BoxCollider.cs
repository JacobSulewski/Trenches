using Trenches.Extensions;

namespace Trenches.Components;
class BoxCollider : Transform2, ICollisionActor
{
    public bool Visible { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public RectangleF BoundingRectangle 
    { 
        get => new RectangleF(Position, new Vector2(Width, Height));
        set => (Position, (Width, Height)) = value;
    }
    public IShapeF Bounds => BoundingRectangle;

    public BoxCollider(RectangleF rect) {
        BoundingRectangle = rect;
    }
    public BoxCollider(float x, float y, float width, float height)
        : this(new RectangleF(x, y, width, height)) { }

    public virtual void OnCollision(CollisionEventArgs collisionInfo)
    {
        System.Console.WriteLine("Collided: " + collisionInfo);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Visible)
            spriteBatch.DrawRectangle(BoundingRectangle, Color.Red);
    }
}