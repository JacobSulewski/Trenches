using MonoGame.Extended.Input.InputListeners;

namespace Trenches.Systems;
class InputSystem : UpdateSystem {
    public MouseListener Mouse { private get; init; }
    public KeyboardListener Keyboard { private get; init; }

    public InputSystem() {
        Mouse.MouseClicked += (sender, args) => System.Console.WriteLine("Clicked");
    }
    public override void Update(GameTime gameTime)
    {
    }
}