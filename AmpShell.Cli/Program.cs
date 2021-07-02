namespace AmpShell.Cli
{
    using AmpShell.Core.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Any() && args.Contains("-g"))
            {
                RunCli(new CliOptions(args));
            }
            else
            {
                OutputHelpText(new CliOptions());
            }
        }

        private static void OutputHelpText(CliOptions options)
        {
            List<CommandLineOption> list = new List<CommandLineOption>() { options.Game, options.Setup, options.Verbose };
            for (int i = 0; i < list.Count; i++)
            {
                CommandLineOption item = list[i];
                Console.WriteLine(item.HelpText);
            }
        }

        private static void RunCli(CliOptions options)
        {
            if (StringExt.IsNullOrWhiteSpace(options.Game.Value))
            {
                Console.WriteLine($"Empty game specified. Exiting...");
            }
            var game = UserDataAccessor.GetFirstGameWithName(options.Game.Value);
            if (StringExt.IsNullOrWhiteSpace(game.DOSEXEPath))
            {
                game = UserDataAccessor.GetGameWithMainExecutable(options.Game.Value);
            }
            if (options.Setup.IsProvided)
            {
                if (options.Verbose.IsProvided)
                {
                    Console.WriteLine($"Running '{game.Name}''s setup executable: {game.SetupEXEPath}...");
                }
                game.RunSetup();
            }
            else
            {
                if (options.Verbose.IsProvided)
                {
                    Console.WriteLine($"Running the game named '{game.Name}' via the main executable at {game.DOSEXEPath}...");
                }

                game.Run();
            }
        }
    }
}