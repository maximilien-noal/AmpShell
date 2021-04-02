/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.DOSBox
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using AmpShell.DAL;
    using AmpShell.Model;

    /// <summary>
    /// Used to start DOSBox with a game in it.
    /// </summary>
    public class DOSBoxController
    {
        private readonly Game gameInstance;

        public DOSBoxController(Game game)
        {
            this.gameInstance = game;
        }

        /// <summary>
        /// Run DOSBox itself, without any game.
        /// </summary>
        /// <returns>The DOSBox process if it started successfully, null otherwise.</returns>
        public static Process RunOnlyDOSBox()
        {
            var arguments = new StringBuilder();
            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false)
            {
                arguments.Append($" -conf \"{UserDataAccessor.UserData.DBDefaultConfFilePath}\"");
            }
            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false)
            {
                arguments.Append($" -lang \"{UserDataAccessor.UserData.DBDefaultLangFilePath}\"");
            }
            var proc = Process.Start(UserDataAccessor.UserData.DBPath, arguments.ToString());
            if (proc != null)
            {
                proc.EnableRaisingEvents = true;
            }
            return proc;
        }

        /// <summary>
        /// Starts DOSBox with <see cref="Game"/> inside it.
        /// </summary>
        /// <returns>The DOSBox process.</returns>
        public Process StartGame() => this.StartGame(this.BuildArgs(false));

        /// <summary>
        /// Starts DOSBox with <see cref="Game"/>.<see cref="Game.SetupEXEPath"/> inside it.
        /// </summary>
        /// <returns>The DOSBox process.</returns>
        public Process StartGameSetup() => this.StartGame(this.BuildArgs(true));

        private static Process StartProcess(ProcessStartInfo psi)
        {
            try
            {
                Process process = Process.Start(psi);

                if (process != null)
                {
                    process.EnableRaisingEvents = true;
                }
                return process;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Start Process error: {e.GetBaseException().Message} {Environment.NewLine} Tried to start: {psi.FileName} in: {psi.WorkingDirectory}");
            }
            return null;
        }

        /// <summary>
        /// Builds the argument line in order to start DOSBox.
        /// </summary>
        /// <param name="forSetupExe">
        /// Whether or not we are starting the game's setup utility or the game itself.
        /// </param>
        /// <returns>The list of command line arguments to pass to DOSBox.</returns>
        private string BuildArgs(bool forSetupExe)
        {
            if (UserDataAccessor.UserData.GamesUseDOSBox == false)
            {
                return string.Empty;
            }
            var configFile = new DOSBoxConfigFile(this.gameInstance.DBConfPath);

            var dosboxArgs = new StringBuilder();
            string dosBoxExePath = this.gameInstance.GetDOSBoxPath();
            if (StringExt.IsNullOrWhiteSpace(dosBoxExePath) == true || dosBoxExePath == "dosbox.exe isn't is the same directory as AmpShell.exe!" || File.Exists(dosBoxExePath) == false)
            {
                throw new FileNotFoundException("DOSBox not found!");
            }

            dosboxArgs.Append(this.AddCustomConfigFile());

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false && UserDataAccessor.UserData.DBDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
            {
                dosboxArgs.Append($" -lang \"{UserDataAccessor.UserData.DBDefaultLangFilePath}\"");
            }

            dosboxArgs.Append(this.AddAdditionalCommands(forSetupExe, configFile));

            //corresponds to the Fullscreen checkbox in GameForm
            if (this.gameInstance.InFullScreen == true)
            {
                dosboxArgs.Append(" -fullscreen");
            }

            //corresponds to the "no console" checkbox in the GameForm
            if (this.gameInstance.NoConsole == true)
            {
                dosboxArgs.Append(" -noconsole");
            }

            //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
            if (this.gameInstance.QuitOnExit == true)
            {
                dosboxArgs.Append(" -exit");
            }

            return dosboxArgs.ToString();
        }

        /// <summary>
        /// Starts DOSBox, and returns its <see cref="Process" />.
        /// </summary>
        /// <returns>The DOSBox <see cref="Process" />.</returns>
        private Process StartGame(string args)
        {
            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.DOSEXEPath))
            {
                throw new ArgumentNullException(nameof(this.gameInstance.DOSEXEPath));
            }
            if (UserDataAccessor.UserData.GamesUseDOSBox == false)
            {
                var targetAndArguments = this.SplitTargetAndArguments();
                var nativeLaunchPsi = new ProcessStartInfo(targetAndArguments[0], targetAndArguments[1]);
                nativeLaunchPsi.UseShellExecute = true;
                nativeLaunchPsi.WorkingDirectory = Path.GetDirectoryName(this.gameInstance.DOSEXEPath);
                return StartProcess(nativeLaunchPsi);
            }
            var psi = new ProcessStartInfo(this.gameInstance.GetDOSBoxPath());
            psi.UseShellExecute = true;
            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.DBConfPath) == false)
            {
                psi.WorkingDirectory = Path.GetDirectoryName(this.gameInstance.DBConfPath);
            }

            if (StringExt.IsNullOrWhiteSpace(args) == false)
            {
                psi.Arguments = args;
            }
            return StartProcess(psi);
        }

        private string[] SplitTargetAndArguments()
        {
            var target = this.gameInstance.DOSEXEPath;
            var arguments = string.Empty;
            if (File.Exists(target) == false)
            {
                var directory = Path.GetDirectoryName(target);
                var fileWithArguments = Path.GetFileName(this.gameInstance.DOSEXEPath);
                if (StringExt.IsNullOrWhiteSpace(fileWithArguments) == false && fileWithArguments.Split(' ').Length > 1)
                {
                    var fileAndArguments = fileWithArguments.Split(' ');
                    var fileName = fileAndArguments[0];
                    target = Path.Combine(directory, fileName);
                    arguments = fileWithArguments.Remove(0, fileName.Length);
                }
            }
            return new string[] { target, arguments };
        }

        private string AddAdditionalCommands(bool forSetupExe, DOSBoxConfigFile configFile)
        {
            if (configFile.IsAutoExecSectionUsed() == true)
            {
                return string.Empty;
            }

            var commands = new StringBuilder();

            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.DOSEXEPath) == false)
            {
                if (!forSetupExe)
                {
                    commands.Append($" {Path.GetFullPath(this.gameInstance.DOSEXEPath)}");
                }
                else
                {
                    commands.Append($" {Path.GetFullPath(this.gameInstance.SetupEXEPath)}");
                }
            }

            //the game directory mounted as C
            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.Directory) == false)
            {
                commands.Append($" -c \"mount c '{this.gameInstance.Directory}'\"");
            }

            //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.CDPath) == false)
            {
                //put ' and not " after imgmount (or else the path will be misunderstood by DOSBox).
                if (this.gameInstance.CDIsAnImage == true)
                {
                    commands.Append(" -c \"imgmount");
                    if (this.gameInstance.MountAsFloppy == true)
                    {
                        commands.Append($" a '{this.gameInstance.CDPath}' -t floppy\"");
                    }
                    else
                    {
                        commands.Append($" d '{this.gameInstance.CDPath}' -t iso\"");
                    }
                }
                else
                {
                    var addedMountOptions = false;
                    if (this.gameInstance.UseIOCTL == true)
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount d '{this.gameInstance.CDPath}' -t cdrom -usecd 0 -ioctl");
                    }
                    else if (this.gameInstance.MountAsFloppy == true)
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount a '{this.gameInstance.CDPath}' -t floppy");
                    }
                    else
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount d '{this.gameInstance.CDPath}'");
                    }
                    if (StringExt.IsNullOrWhiteSpace(this.gameInstance.CDLabel) == false && addedMountOptions)
                    {
                        commands.Append($" -label '{this.gameInstance.CDLabel}'");
                    }
                    commands.Append("\"");
                }
            }

            if (StringExt.IsNullOrWhiteSpace(this.gameInstance.AdditionalCommands) == false)
            {
                commands.Append(this.gameInstance.AdditionalCommands);
            }

            return commands.ToString();
        }

        private string AddCustomConfigFile()
        {
            string gameConfigFilePath = string.Empty;

            //if the "do not use any config file at all" has not been checked
            if (this.gameInstance.NoConfig == false)
            {
                //use at first the game's custom config file
                if (StringExt.IsNullOrWhiteSpace(this.gameInstance.DBConfPath) == false)
                {
                    gameConfigFilePath = this.gameInstance.DBConfPath;
                }

                //if not, use the default dosbox.conf file
                else if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false && UserDataAccessor.UserData.DBDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                {
                    gameConfigFilePath = UserDataAccessor.UserData.DBDefaultConfFilePath;
                }
            }
            string dosboxArgs = string.Empty;
            if (StringExt.IsNullOrWhiteSpace(gameConfigFilePath) == false)
            {
                dosboxArgs += $"-conf \"{gameConfigFilePath}\"";
            }

            return dosboxArgs;
        }
    }
}