// See https://aka.ms/new-console-template for more information

using AOC2023;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<PuzzleCommand>("puzzle");
});

return app.Run(args);