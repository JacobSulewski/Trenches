namespace Trenches;
abstract class GameBase : Game
{
    // ReSharper disable once NotAccessedField.Local
    protected readonly GraphicsDeviceManager GraphicsDeviceManager;
    protected IContainer Container 
        { get; private set; }
    public readonly int Width;
    public readonly int Height; 
    protected GameBase(int width = 800, int height = 480)
    {
        Width = width;
        Height = height;
        GraphicsDeviceManager = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height
        };
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Content.RootDirectory = "Content";
    }
    protected override void Initialize()
    {
        var containerBuilder = new ContainerBuilder();
        RegisterDependencies(containerBuilder);
        Container = containerBuilder.Build();
        
        base.Initialize();
    }
    protected abstract void RegisterDependencies(ContainerBuilder builder);
}
