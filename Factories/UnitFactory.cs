using System;
using MonoGame.Extended;
using MonoGame.Extended.Entities;

namespace Trenches.Factories;
abstract class UnitFactory: EntityFactory {
    public override Entity Create(){
        var entity = base.Create();
        entity.Attach(new Transform2());
        return entity;
    }    
}