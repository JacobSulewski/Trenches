using Autofac;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using Trenches.Factories;
using Trenches.Systems;

namespace Trenches;
class GameMain : GameBase
{
    protected override void RegisterDependencies(ContainerBuilder builder)
    {
        builder.RegisterInstance<GraphicsDevice>(GraphicsDevice);
        builder.RegisterInstance<ContentManager>(Content);
        builder.RegisterType<SpriteBatch>();
        builder.RegisterType<RenderSystem>()
               .PropertiesAutowired();
        builder.Register<OrthographicCamera>(c => 
                    new OrthographicCamera(c.Resolve<GraphicsDevice>())
                    {
                        Zoom = 2,
                        Origin = Vector2.Zero
                    })
               .AsSelf()
               .As<Camera<Vector2>>()
               .SingleInstance();
        builder.RegisterType<EntityFactory>()
               .PropertiesAutowired()
               .AsSelf()
               .SingleInstance();
        builder.Register<World>(c => 
                    new WorldBuilder()
                    .AddSystem(c.Resolve<RenderSystem>())
                    .Build())
                .SingleInstance();
    }

    protected override void LoadContent()
    {
        Components.Add(Container.Resolve<World>());

        /* TOOD: Load maps and collision data more nicely :)
        _map = Content.Load<TiledMap>("test-map");
        _renderer = new TiledMapRenderer(GraphicsDevice, _map);

        foreach (var tileLayer in _map.TileLayers)
        {
            for (var x = 0; x < tileLayer.Width; x++)
            {
                for (var y = 0; y < tileLayer.Height; y++)
                {
                    var tile = tileLayer.GetTile((ushort)x, (ushort)y);

                    if (tile.GlobalIdentifier == 1)
                    {
                        var tileWidth = _map.TileWidth;
                        var tileHeight = _map.TileHeight;
                        _entityFactory.CreateTile(x, y, tileWidth, tileHeight);
                    }
                }
            }
        } */
        Container.Resolve<EntityFactory>().CreateInfantryman(new Vector2(1, 1));
        Container.Resolve<EntityFactory>().CreateEngineer(new Vector2(5, 5));
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Using global shared input state is really bad!

        //var keyboardState = KeyboardExtended.GetState();

        //if (keyboardState.IsKeyDown(Keys.Escape))
        //    Exit();

        //_renderer.Update(gameTime);
        //_camera.LookAt(_playerEntity.Get<Transform2>().Position);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        //_renderer.Draw(_camera.GetViewMatrix());

        base.Draw(gameTime);
    }
}
