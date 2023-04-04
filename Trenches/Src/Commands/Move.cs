using Trenches.Components;



// ! Get rid of this and just make it a systme. Becasue hwat if the entity loses
// ! a component, now you have a reference of it and it cant get collected.
// ! If its a system then this wont happen. I guess you could just have the system.
// ! create the ICommand pass in everything? Or the UnitCommand can be a Enumerator/Factory
// ! That spits out the the next command. But what would be the dependencies???

namespace Trenches.Commands;
class Move : UnitCommand
{
    readonly Vector2 _target;
    readonly IMovable _movable;
    readonly Physics _physics;
    readonly float _distance;
    public Move(Entity entity, Vector2 target) 
    {
        (_target,(_movable, _physics)) = (target, entity.Get<Transform2, Physics>());
        _distance = (_target - _movable.Position).Length(); // Initial distance from target
    }
    public override float Progress
        => (_target - _movable.Position).Length() / _distance * 100;

    public override bool IsComplete
        => Math.Ceiling(Progress) == 100;

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}