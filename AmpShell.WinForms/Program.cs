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
    using AmpShell.Core.DAL;
    using AmpShell.WinForms.Views;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        internal static UserDataAccessor UserDataAccessorInstance = new UserDataAccessor();

        private static void CurrentDomainUnhandledExceptionMethod(object sender, UnhandledExceptionEventArgs e) => ShowException(((Exception)e.ExceptionObject).GetBaseException());

        private static bool IsWindows98() => Environment.OSVersion.Version.Minor == 10;

        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Any())
            {
                Process.Start("AmpShell.Cli", string.Join(" ", args));
                return;
            }
            if (IsWindows98() == false)
            {
                Application.EnableVisualStyles();
            }
            Application.SetCompatibleTextRenderingDefault(false);

            //The thread exception handler has to be assigned before any control is presented on the screen.
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadExceptionMethod);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomainUnhandledExceptionMethod);

            if (UserDataAccessorInstance.GetUserData().GamesUseDOSBox && StringExt.IsNullOrWhiteSpace(UserDataAccessorInstance.GetUserData().DBPath) && IsWindows98())
            {
                UserDataAccessorInstance.DisableDOSBoxUsage();
            }

            // AutoConfig is run when UserDataAccessor is instantiated above. If DBPath is still
            // empty and we must use DOSBox, say to the user that DOSBox's executable cannot be found.
            else if (UserDataAccessorInstance.GetUserData().GamesUseDOSBox && (StringExt.IsNullOrWhiteSpace(UserDataAccessorInstance.GetUserData().DBPath) || File.Exists(UserDataAccessorInstance.GetUserData().DBPath) == false))
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
                                    UserDataAccessorInstance.UpdateDOSBoxPath(dosboxExeFileDialog.FileName);
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }
                            }
                        }
                        break;

                    case DialogResult.No:
                        UserDataAccessorInstance.UpdateDOSBoxPath(string.Empty);
                        break;
                }
            }
            RunMainForm();
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

        private static void ShowException(Exception baseException) => MessageBox.Show($"Please report this error to the author:{Environment.NewLine}{baseException.Message}{Environment.NewLine}{baseException.StackTrace}");

        private static void UIThreadExceptionMethod(object sender, ThreadExceptionEventArgs e) => ShowException(e.Exception.GetBaseException());
    }
}