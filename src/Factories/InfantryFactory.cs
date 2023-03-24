using Trenches.Extensions;
using Trenches.Components;

namespace Trenches.Factories;
class InfantryFactory : EntityFactory {
    public ContentManager Content { private get; init; }
    
    public override Entity Create()
    {
        var texture = Content.Load<Texture2D>("Sprites/infantry");
        var collider = new BoxCollider(texture.Bounds);
        return base.Create()
                   .Add(new Sprite(texture))
                   .Add(collider)
                   .Add((Transform2)collider)
                   .Add(new Physics{ Speed = 10 });
    }
}