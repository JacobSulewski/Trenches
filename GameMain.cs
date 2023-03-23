using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using Trenches.Systems;
using Trenches.Factories;

namespace Trenches;
class GameMain : GameBase
{
    public GameMain()
    {
    }

    protected override void RegisterDependencies(ContainerBuilder builder)
    {
        builder.RegisterInstance(Content);
        builder.RegisterInstance(new SpriteBatch(GraphicsDevice));
        builder.RegisterInstance(new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height))
               .As<ViewportAdapter>()
               .AsImplementedInterfaces();
        builder.RegisterType<RenderSystem>()
               .PropertiesAutowired();
        builder.RegisterType<InfantryFactory>()
               .PropertiesAutowired()
               .SingleInstance();
        builder.RegisterType<EngineerFactory>()
               .PropertiesAutowired()
               .SingleInstance();
        builder.Register(c => new WorldBuilder()
                              .AddSystem(c.Resolve<RenderSystem>())
                              .Build())
               .SingleInstance();
        builder.Register(c => new OrthographicCamera(c.Resolve<ViewportAdapter>())
                              {
                                  Zoom = 2.0f,
                                  MinimumZoom = 1.5f,
                                  MaximumZoom = 2.5f,
                              })
               .SingleInstance();
    }

    protected override void LoadContent()
    {
        Components.Add(Container.Resolve<World>());
        Container.Resolve<InfantryFactory>().Create();
        Container.Resolve<OrthographicCamera>().LookAt(Vector2.Zero);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Brown);

        base.Draw(gameTime);
    }
}
