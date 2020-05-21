/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

[assembly: System.Resources.NeutralResourcesLanguage("en-US")]

namespace AmpShell
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using AmpShell.DAL;
    using AmpShell.DOSBox;
    using AmpShell.Extensions;
    using AmpShell.Model;
    using AmpShell.Views;
    using AmpShell.WinShell;
    using CommandLine;

    internal static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            UserDataAccessor.LoadUserSettings();
            DOSBoxController.AskForDOSBoxIfNotFound();
            if (args is null || args.Any() == false)
            {
                RunMainForm();
            }
            else
            {
                // get console output
                if (!NativeMethods.AttachConsole(-1))
                {
                    NativeMethods.AllocConsole();
                }

                CommandLine.Parser.Default.ParseArguments<Options>(args)
                    .WithParsed(RunCli)
                    .WithNotParsed(OutputErrors);

                // detach console
                NativeMethods.FreeConsole();

                // gives command prompt back to the user
                SendKeys.SendWait("{ENTER}");
            }
        }

        private static void OutputErrors(IEnumerable<Error> errs)
        {
            if (errs is null || errs.Any(x => x is VersionRequestedError))
            {
                return;
            }
            else
            {
                foreach (var err in errs)
                {
                    Console.WriteLine($"Command line option parse error: {err.Tag}");
                }
            }
        }

        private static void RunCli(Options options)
        {
            var request = options.Game;
            if (string.IsNullOrWhiteSpace(request))
            {
                Console.WriteLine($"Empty game specified. Exiting...");
            }
            Game game;
            game = DAL.UserDataAccessor.GetFirstGameWithName(request);
            if (string.IsNullOrWhiteSpace(game.DOSEXEPath))
            {
                game = DAL.UserDataAccessor.GetGameWithMainExecutable(request);
            }
            if (options.Setup)
            {
                if (options.Verbose)
                {
                    Console.WriteLine($"Running '{game.Name}''s setup executable: {game.SetupEXEPath}...");
                }
                game.RunSetup();
            }
            else
            {
                if (options.Verbose)
                {
                    Console.WriteLine($"Running the game named '{game.Name}' via the main executable at {game.DOSEXEPath}...");
                }

                game.Run();
            }
        }

        private static void RunMainForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new MainForm();
            using (mainForm)
            {
                Application.Run(mainForm);
            }
        }
    }
}