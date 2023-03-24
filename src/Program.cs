using var game = new Trenches.GameMain();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(path: "logs/logfile.log")
    .CreateLogger();

try 
{
    game.Run();
} 
catch (Exception ex) 
{
    Log.Logger?.Error(ex.ToString());
    throw;
}
Log.Logger?.Fatal("Exiting\n");