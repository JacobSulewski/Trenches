using Trenches.Components;

namespace Trenches.Factories;
class MortarmanFactory : EntityFactory {
    required public ContentManager Content
        { private get; init; }
    public override Entity Create()
    {
        var texture = Content.Load<Texture2D>("Sprites/mortarman");
        // TODO Scale and sprite name should be read from config file
        Sprite sprite = new(texture);
        BoxCollider collider = new(sprite) { Scale = Vector2.One * 4 };
        return base.Create()
                   .Add<Sprite>(sprite)
                   .Add<Collider>(collider)
                   .Add<Transform2>(collider)
                   .Add<Physics>(new(50));
    }
}