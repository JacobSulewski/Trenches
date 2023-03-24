using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;

using Trenches.Components;
using Trenches.Systems;
using Trenches.Factories;

namespace Trenches;
class GameMain : GameBase
{
    public GameMain() { }

    protected override void RegisterDependencies(ContainerBuilder builder)
    {
        /* RegisterInstance */
        builder.RegisterInstance(Content);
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(this)
            .As<Game>();
        builder.RegisterInstance(new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height))
            .As<ViewportAdapter>()
            .AsImplementedInterfaces();

        /* RegisterType<T> */
        builder.RegisterType<InfantryFactory>()
            .PropertiesAutowired()
            .SingleInstance();
        builder.RegisterType<EngineerFactory>()
            .PropertiesAutowired()
            .SingleInstance();

        builder.RegisterType<RenderSystem>()
            .PropertiesAutowired();
        builder.RegisterType<PhysicsSystem>()
            .PropertiesAutowired();

        /* Register */
        builder.Register(c => new OrthographicCamera(c.Resolve<ViewportAdapter>())
                {
                    Zoom = 4.0f, 
                    MinimumZoom = 4.0f,
                    MaximumZoom = 8.0f,
                })
            .SingleInstance();
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
        builder.Register(c => new WorldBuilder()
                .AddSystem(c.Resolve<PhysicsSystem>())
                .AddSystem(c.Resolve<RenderSystem>())
                .Build())
            .SingleInstance();
    }

    protected override void Initialize()
    {
        base.Initialize();
        Components.Add(Container.Resolve<World>());
        Components.Add(Container.Resolve<InputListenerComponent>());
    }

    protected override void LoadContent()
    {
        var e = Container.Resolve<InfantryFactory>().Create();
        Container.Resolve<MouseListener>().MouseClicked += (sender, args) => {
            e.Get<Physics>().Velocity = Vector2.Zero;
        };
        Container.Resolve<KeyboardListener>().KeyPressed += (sender, args) => {
            if( args.Key == Keys.D )
                e.Get<Physics>().Velocity = new Vector2(100, 0);
            if( args.Key == Keys.A )
                e.Get<Physics>().Velocity = new Vector2(-100, 0);
            if( args.Key == Keys.S )
                e.Get<Physics>().Velocity = new Vector2(0, 100);
            if( args.Key == Keys.W )
                e.Get<Physics>().Velocity = new Vector2(0, -100);
        };

        var camera = Container.Resolve<OrthographicCamera>();
        camera.LookAt(Vector2.Zero);

        Log.Information(e.Get<Transform2>().Position.ToString());
        Log.Information(camera.GetViewMatrix().ToString());
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
