using MonoGame.Extended.Input.InputListeners;

namespace Trenches.Systems;
class MouseController : SimpleDrawableGameComponent {
    public MouseListener Listener { private get; init; }

    public override void Initialize()
    {
        Listener.MouseMoved += (sender, args) => Log.Information("Clicked");
        Log.Information("Mouse init");
        base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {
    }

    public override void Update(GameTime gameTime)
    {
    }
}