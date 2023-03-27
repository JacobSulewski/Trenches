namespace Trenches.Components;
class CircleCollider : Collider
{
    public readonly float Radius;
    public CircleF BoundingCircle
        => new CircleF(Position, Radius);
    public override IShapeF Bounds 
        => BoundingCircle;
    public CircleCollider(CircleF circle) 
        => (Radius) = (circle.Radius);
    public CircleCollider(float radius) 
        => (Radius) = (radius);
    public override void OnCollision(CollisionEventArgs collisionInfo)
    {
        System.Console.WriteLine("Collided: " + collisionInfo);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawCircle(Position, Radius, 16, Color, 1f, LayerDepth.Debug);
    }
}