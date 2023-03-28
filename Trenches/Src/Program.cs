using var game = new Trenches.GameMain();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Hello from console");

game.Run();