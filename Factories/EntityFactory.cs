using MonoGame.Extended.Entities;

namespace Trenches.Factories;
abstract class EntityFactory : IFactory<Entity> {
    public World World { private get; init; }
    
    public virtual Entity Create(){
        return World.CreateEntity();
    }    
}