namespace Trenches;
abstract class GameBase : Game
{
    // ReSharper disable once NotAccessedField.Local
    protected GraphicsDeviceManager GraphicsDeviceManager { get; }
    protected IContainer Container { get; private set; }

    public int Width { get; }
    public int Height { get; }

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
        try 
        {
            Container = containerBuilder.Build();
        } 
        catch (Exception)
        {
            Log.Logger?.Error("Container failed to build.");
            throw;
        }

        base.Initialize();
    }

    protected abstract void RegisterDependencies(ContainerBuilder builder);
}
