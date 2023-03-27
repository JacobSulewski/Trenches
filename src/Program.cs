using var game = new Trenches.GameMain();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(path: "logs/logfile.log")
    .CreateLogger();

Log.Information("Starting");
System.Console.WriteLine("Console");
System.Diagnostics.Debug.WriteLine("Debug");
try 
{
    game.Run();
} 
catch (Exception ex) 
{
    Log.Logger?.Error(ex.ToString());
    throw;
}
Log.Information("Exiting");