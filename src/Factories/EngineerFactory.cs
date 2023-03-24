using Trenches.Extensions;
using Trenches.Components;

namespace Trenches.Factories;
class EngineerFactory : EntityFactory {
    public ContentManager Content { private get; init; }
    
    public override Entity Create()
    {
        var texture = Content.Load<Texture2D>("Sprites/engineer");
        return base.Create()
                   .Add(new Sprite(texture))
                   .Add(new BoxCollider(texture.Bounds))
                   .Add(new Physics{ Speed = 10 });
    }
}