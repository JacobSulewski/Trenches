namespace Trenches.Components.UnitActions;
interface IUnitAction
{
    IUnitAction Stop();
    IUnitAction Move(Vector2 target);
    IUnitAction Build(Vector2 target);
    void Update(GameTime gameTime, Transform2 transform, Physics physics);
}