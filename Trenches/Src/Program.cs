using var game = new Trenches.GameMain();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(path: "logs/logfile.log")
    .CreateLogger();

game.Run();