using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using Trenches.Factories;

namespace Trenches.Factories;
sealed class InfantryMan { };
class EntityFactory : IFactory<Entity>
{
    public World World { private get; init; }
    public ContentManager Content { private get; init; }

    public Entity Create<T>()
    {
        return typeof(T).Name switch
        {
            nameof(InfantryMan) => CreateInfantryman(),
            _ => throw new NotSupportedException(typeof(T).Name)
        };
    }

    Entity CreateInfantryman()
    {
        var texture = Content.Load<Texture2D>("Sprites/infantry");

        var entity = World.CreateEntity();
        entity.Attach(new Transform2());
        entity.Attach(new Sprite(texture));
        return entity;
    }
}
