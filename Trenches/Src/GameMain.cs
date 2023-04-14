using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;

using Trenches.Components;
using Trenches.Systems;
using Trenches.Factories;

namespace Trenches;
public class GameMain : GameBase
{
    public const int PIXELS = 32;
    public GameMain(int width = 800, int height = 480)
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
        builder.RegisterType<CollisionComponent>()
            .WithParameter("boundary", new RectangleF(0, 0, Width, Height))
            .SingleInstance();
        builder.RegisterType<Map>()
            .WithParameter("width", Width)
            .WithParameter("height", Height)
            .SingleInstance();
    }
    private void RegisterGraphics(ContainerBuilder builder)
    {
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height))
            .As<ViewportAdapter>()
            .AsImplementedInterfaces();
        builder.Register(c => new OrthographicCamera(c.Resolve<ViewportAdapter>()))
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
        builder.RegisterType<WorldSystem>()
            .PropertiesAutowired()
            .SingleInstance();
        
        builder.Register(c => new WorldBuilder()
                .AddSystem(c.Resolve<MoveSystem>())
                .AddSystem(c.Resolve<PhysicsSystem>())
                .AddSystem(c.Resolve<RenderSystem>())
                .AddSystem(c.Resolve<CameraSystem>())
                .AddSystem(c.Resolve<WorldSystem>())
                .Build())
            .SingleInstance();
    }
    protected override void Initialize()
    {
        base.Initialize();
        Components.Add(Container.Resolve<World>());
        Components.Add(Container.Resolve<InputListenerComponent>());
        Utils.Initialize(Container.Resolve<ContentManager>());
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
        var infantryman = Container.Resolve<InfantryFactory>().Create();

        var entity = Container.Resolve<World>().CreateEntity().Add<Transform2>(new()).Add<Physics>(new(200)).Add<Camera>(new());

        mouse.MouseClicked += (sender, args) => 
        {
            if (args.Button == MouseButton.Left)
            {
                Log.Information($"MouseWorld:{args.Position}"); // Gives the position on the screen
                Log.Information($"ScreenToWorld{camera.ScreenToWorld(args.Position)}");
                Log.Information($"Camera:{camera.Position} Rectangle:{camera.BoundingRectangle} Center:{camera.Center} Origin:{camera.Origin}");
                Log.Information($"Infantryman:{infantryman.Get<Transform2>().Position} World:{infantryman.Get<Transform2>().WorldPosition}");
                Log.Information($"Entity:{entity.Get<Transform2>().Position} World:{entity.Get<Transform2>().WorldPosition}");
                Log.Debug("");
            }
            if (args.Button == MouseButton.Left)
            {
                infantryman.Add<UnitAction>(new Move(camera.ScreenToWorld(args.Position)));
            }
        };
        keyboard.KeyPressed += (sender, args) => 
        {
            var physics = entity.Get<Physics>();
            if (args.Key == Keys.D)
                physics.Velocity += Vector2.UnitX * physics.Speed;
            if (args.Key == Keys.A)
                physics.Velocity -= Vector2.UnitX * physics.Speed;
            if (args.Key == Keys.S)
                physics.Velocity += Vector2.UnitY * physics.Speed;
            if (args.Key == Keys.W)
                physics.Velocity -= Vector2.UnitY * physics.Speed;
            if (args.Key == Keys.G)
                renderer.Debug = ! renderer.Debug;
            var transform = infantryman.Get<Transform2>();
            physics = infantryman.Get<Physics>();
            var move = (Move)infantryman.Get<UnitAction>();
            if (args.Key == Keys.Space)
                Log.Information($"Pos:{transform.Position} Vel:{physics.Velocity} Tar:{move.Target}");
        };
        keyboard.KeyReleased += (sender, args) => 
        {
            var physics = entity.Get<Physics>();
            if( args.Key == Keys.D || args.Key == Keys.A )
                physics.Velocity *= Vector2.UnitY;
            if( args.Key == Keys.S ||  args.Key == Keys.W)
                physics.Velocity *= Vector2.UnitX;
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
