namespace Trenches.Components;
abstract class Collider : Transform2, ICollisionActor
{
    public Color Color = Color.Red;
    public abstract IShapeF Bounds 
        { get; }
    public abstract void OnCollision(CollisionEventArgs collisionInfo);
    public abstract void Draw (SpriteBatch spriteBatch);
}