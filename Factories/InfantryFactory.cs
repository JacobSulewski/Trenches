using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;

namespace Trenches.Factories;
class InfantryFactory: UnitFactory {
    public ContentManager Content { private get; init; }
    
    public override Entity Create(){
        var texture = Content.Load<Texture2D>("Sprites/infantry");
        
        var entity = base.Create();
        entity.Attach(new Sprite(texture));
        return entity;
    }
}