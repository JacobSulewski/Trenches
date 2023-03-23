using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace Trenches.Systems;
class InputSystem : EntityUpdateSystem
{
    public IController Controller {private get; init;}

    private ComponentMapper<Transform2> _transformMapper;
    private ComponentMapper<Input> _inputMapper;

    public InputSystem()
        : base(Aspect.All(typeof(Transform2), typeof(Input)))
    {
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _transformMapper = mapperService.GetMapper<Transform2>();
        _inputMapper = mapperService.GetMapper<Input>();
    }
    public override void Update(GameTime gameTime)
    {
        foreach (var entity in ActiveEntities)
        {


        }
    }
}
