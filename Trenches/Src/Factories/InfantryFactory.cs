using Trenches.Components;

namespace Trenches.Factories;
class InfantryFactory : EntityFactory, IFactory<Entity, Vector2>{
    required public ContentManager Content
        { private get; init; }
    public override Entity Create()
        => Create(Vector2.Zero);

    public Entity Create(Vector2 position){
        var texture = Content.Load<Texture2D>("Sprites/infantry");
        // TODO Scale and sprite name should be read from config files
        Sprite sprite = new(texture) { Depth = LayerDepth.Object };
        BoxCollider collider = new(sprite) { Position = position, Scale = Vector2.One * 4 };
        //CircleCollider collider = new(5);
        return base.Create()
                   .Add<Sprite>(sprite)
                   .Add<Collider>(collider)
                   .Add<Transform2>(collider)
                   .Add<Physics>(new(50));
    }
}