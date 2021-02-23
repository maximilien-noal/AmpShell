/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
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
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using AmpShell.DAL;
    using AmpShell.Model;
    using AmpShell.Views;
    using AmpShell.WinShell;

    internal static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            UserDataAccessor.LoadUserSettingsAndRunAutoConfig();

            // if DOSBoxPath is still empty, say to the user that dosbox's executable cannot be found
            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) || File.Exists(UserDataAccessor.UserData.DBPath) == false)
            {
                switch (MessageBox.Show("AmpShell cannot find DOSBox, do you want to indicate DOSBox's executable location now ? Choose 'Cancel' to quit.", "Cannot find DOSBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        Environment.Exit(0);
                        break;

                    case DialogResult.Yes:
                        {
                            using (var dosboxExeFileDialog = new OpenFileDialog())
                            {
                                dosboxExeFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                                dosboxExeFileDialog.Title = "Please indicate DOSBox's executable location...";
                                dosboxExeFileDialog.Filter = "DOSBox executable (dosbox*)|dosbox*";
                                if (dosboxExeFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    UserDataAccessor.UserData.DBPath = dosboxExeFileDialog.FileName;
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }
                            }
                        }
                        break;

                    case DialogResult.No:
                        UserDataAccessor.UserData.DBPath = string.Empty;
                        break;
                }
            }
            else
            {
#if NET461
                //JumpListUpdater.InitJumpTasks();
#endif
            }
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
                if (args.Contains("-g"))
                {
                    RunCli(new Options(args));
                }
                else
                {
                    OutputHelpText(new Options());
                }

                // detach console
                NativeMethods.FreeConsole();

                // gives command prompt back to the user
                SendKeys.SendWait("{ENTER}");
            }
        }

        private static void OutputHelpText(Options options)
        {
            foreach (var item in new List<CommandLineOption>() { options.Game, options.Setup, options.Verbose })
            {
                Console.WriteLine(item.HelpText);
            }
        }

        private static void RunCli(Options options)
        {
            if (StringExt.IsNullOrWhiteSpace(options.Game.Value))
            {
                Console.WriteLine($"Empty game specified. Exiting...");
            }
            Game game;
            game = DAL.UserDataAccessor.GetFirstGameWithName(options.Game.Value);
            if (StringExt.IsNullOrWhiteSpace(game.DOSEXEPath))
            {
                game = DAL.UserDataAccessor.GetGameWithMainExecutable(options.Game.Value);
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