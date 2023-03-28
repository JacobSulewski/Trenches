using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;

using Trenches.Components;
using Trenches.Systems;
using Trenches.Factories;
using Trenches.Pathing;

namespace Trenches;
public class GameMain : GameBase
{
    public GameMain(int width = 1200, int height = 800)
        : base(width, height) { }
    protected override void RegisterDependencies(ContainerBuilder builder)
    {

        RegisterGameComponents(builder);
        RegisterGraphics(builder);
        RegisterInput(builder);
        RegisterFactories(builder);
        RegisterWorld(builder);
    }
    private void RegisterGameComponents(ContainerBuilder builder)
    {
        builder.RegisterInstance(Content);
        builder.RegisterInstance(this)
            .As<Game>();
        builder.Register(c => new CollisionComponent(new(0, 0, Width, Height)));
        builder.Register(c => new GridGraph(Width, Height, 32));
    }
    private void RegisterGraphics(ContainerBuilder builder)
    {
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height))
            .As<ViewportAdapter>()
            .AsImplementedInterfaces();
        builder.Register(c => new OrthographicCamera(c.Resolve<ViewportAdapter>())
                {
                    Position = Vector2.Zero,
                    Zoom = 1.25f
                })
            .SingleInstance();
    }
    private void RegisterInput(ContainerBuilder builder)
    {
        builder.Register(c => new InputListenerComponent(
                c.Resolve<Game>(),
                c.Resolve<MouseListener>(),
                c.Resolve<KeyboardListener>()))
            .SingleInstance();
        builder.Register(c => new MouseListenerSettings
                {
                    ViewportAdapter = c.Resolve<ViewportAdapter>()
                }.CreateListener())
            .SingleInstance();
        builder.Register(c => new KeyboardListenerSettings{}.CreateListener())
            .SingleInstance();
    }
    private void RegisterFactories(ContainerBuilder builder)
    {
        builder.RegisterType<InfantryFactory>()
            .PropertiesAutowired()
            .SingleInstance();
        builder.RegisterType<MortarmanFactory>()
            .PropertiesAutowired()
            .SingleInstance();
    }
    private void RegisterWorld(ContainerBuilder builder)
    {
        builder.RegisterType<MoveSystem>()
            .PropertiesAutowired()
            .SingleInstance();
        builder.RegisterType<PhysicsSystem>()
            .PropertiesAutowired()
            .SingleInstance();
        builder.RegisterType<RenderSystem>()
            .PropertiesAutowired()
            .SingleInstance();
        builder.RegisterType<CameraSystem>()
            .PropertiesAutowired()
            .SingleInstance();
        
        builder.Register(c => new WorldBuilder()
                .AddSystem(c.Resolve<MoveSystem>())
                .AddSystem(c.Resolve<PhysicsSystem>())
                .AddSystem(c.Resolve<RenderSystem>())
                .AddSystem(c.Resolve<CameraSystem>())
                .Build())
            .SingleInstance();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Components.Add(Container.Resolve<World>());
        Components.Add(Container.Resolve<InputListenerComponent>());
        Utils.Utils.Initialize(Container.Resolve<ContentManager>(), Container.Resolve<SpriteBatch>());
        BindControls();
    }

    private void BindControls()
    {
        // TODO Put this control binding in its own class
        // TODO Keybindings should be read in from a config file
        var keyboard = Container.Resolve<KeyboardListener>();
        var mouse = Container.Resolve<MouseListener>();
        var renderer = Container.Resolve<RenderSystem>();
        var camera = Container.Resolve<OrthographicCamera>();
        var infantryman = Container.Resolve<InfantryFactory>().Create().Add<Camera>();

        Log.Information($"Camera: {camera.Position}");
        Log.Information($"ViewportAdapter Bounds: {Container.Resolve<ViewportAdapter>().BoundingRectangle}");
        Log.Information($"ViewportAdapter Bounds: {Container.Resolve<ViewportAdapter>().Viewport}");

        mouse.MouseClicked += (sender, args) => 
        {
            if (args.Button == MouseButton.Left)
            {
                infantryman.Add<UnitCommand>(new Move(camera.ScreenToWorld(args.Position)));
                Log.Debug($"WorldToScreen: {camera.WorldToScreen(args.Position)}");
                Log.Debug($"ScreenToWorld: {camera.ScreenToWorld(args.Position)}");
                Log.Information($"Move from : {infantryman.Get<Transform2>().Position}");
                Log.Information($"Move to : {args.Position}");
            }
            if (args.Button == MouseButton.Right)
            {
                infantryman.Detach<UnitCommand>();
                infantryman.Get<Physics>().Direction = Vector2.Zero;
            }
        };
        keyboard.KeyPressed += (sender, args) => 
        {
            var physics = infantryman.Get<Physics>();
            Log.Information(physics.ToString());
            if (args.Key == Keys.D)
                physics.Direction += Vector2.UnitX;
            if (args.Key == Keys.A)
                physics.Direction -= Vector2.UnitX;
            if (args.Key == Keys.S)
                physics.Direction += Vector2.UnitY;
            if (args.Key == Keys.W)
                physics.Direction -= Vector2.UnitY;
            if (args.Key == Keys.G)
                renderer.Debug = ! renderer.Debug;
            if (args.Key == Keys.Space)
                Log.Information($"Position: {infantryman.Get<Transform2>().Position}");
        };
        keyboard.KeyReleased += (sender, args) => 
        {
            var physics = infantryman.Get<Physics>();
            if( args.Key == Keys.D || args.Key == Keys.A )
                physics.Direction *= Vector2.UnitY;
            if( args.Key == Keys.S ||  args.Key == Keys.W)
                physics.Direction *= Vector2.UnitX;
        };
    }

    protected override void LoadContent()
    {
    }
    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.DarkGreen);

        base.Draw(gameTime);
    }
}
