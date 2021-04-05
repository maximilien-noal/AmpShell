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
namespace AmpShell.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using AmpShell.Core.DAL;
    using AmpShell.Core.Model;
    using AmpShell.WinForms.Views;
    using AmpShell.WinShell;

    internal static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);

            //The thread exception handler has to be assigned before any control is presented on the screen.
            if (args is null || args.Any() == false)
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(UIThreadExceptionMethod);

                // Set the unhandled exception mode to force all Windows Forms errors to go through
                // our handler.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                // Add the event handler for handling non-UI thread exceptions to the event.
                AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomainUnhandledExceptionMethod);
            }

            UserDataAccessor.LoadUserSettingsAndRunAutoConfig();

            if (UserDataAccessor.UserData.GamesUseDOSBox && StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) && IsWindows98())
            {
                UserDataAccessor.UserData.GamesUseDOSBox = false;
            }

            var localDosbox = Path.Combine(Application.StartupPath, "dosbox.exe");
            if (UserDataAccessor.UserData.PortableMode && File.Exists(localDosbox))
            {
                UserDataAccessor.UserData.DBPath = localDosbox;
            }

            // if DOSBoxPath is still empty and we must use DOSBOx, say to the user that dosbox's executable cannot be found.
            else if (UserDataAccessor.UserData.GamesUseDOSBox && (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) || File.Exists(UserDataAccessor.UserData.DBPath) == false))
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

        private static void UIThreadExceptionMethod(object sender, ThreadExceptionEventArgs e) => ShowException(e.Exception.GetBaseException());

        private static void ShowException(Exception baseException) => MessageBox.Show($"Please report this error to the author:{Environment.NewLine}{baseException.Message}{Environment.NewLine}{baseException.StackTrace}");

        private static void CurrentDomainUnhandledExceptionMethod(object sender, UnhandledExceptionEventArgs e) => ShowException(((Exception)e.ExceptionObject).GetBaseException());

        private static bool IsWindows98() => Environment.OSVersion.Version.Minor == 10;

        private static void OutputHelpText(Options options)
        {
            List<CommandLineOption> list = new List<CommandLineOption>() { options.Game, options.Setup, options.Verbose };
            for (int i = 0; i < list.Count; i++)
            {
                CommandLineOption item = list[i];
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
            game = UserDataAccessor.GetFirstGameWithName(options.Game.Value);
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

        private static void RunMainForm()
        {
            if (IsWindows98() == false)
            {
                Application.EnableVisualStyles();
            }

            var mainForm = new MainForm();
            using (mainForm)
            {
                Application.Run(mainForm);
            }
        }
    }
}