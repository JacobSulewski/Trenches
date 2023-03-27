namespace Trenches.Factories;
abstract class EntityFactory : IFactory<Entity> {
    required public World World
        { private get; init; }

    public virtual Entity Create()
    {
        return World.CreateEntity();
    }    
}