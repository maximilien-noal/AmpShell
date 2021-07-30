/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Core.Games
{
    using AmpShell.Core.DOSBox;
    using AmpShell.Core.Model;

    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary> Used to start DOSBox with a game in it, or to run the game directly. </summary>
    public class GameProcessController
    {
        private readonly Game _game;

        private readonly Preferences _userData;

        internal GameProcessController(Game game, Preferences userData)
        {
            this._game = game;
            this._userData = userData;
        }

        /// <summary> Run DOSBox itself, without any game. </summary>
        /// <returns> The DOSBox process if it started successfully, null otherwise. </returns>
        public static Process RunOnlyDOSBox(Preferences userData)
        {
            var arguments = new StringBuilder();
            if (StringExt.IsNullOrWhiteSpace(userData.DBDefaultConfFilePath) == false)
            {
                arguments.Append($" -conf \"{userData.DBDefaultConfFilePath}\"");
            }
            if (StringExt.IsNullOrWhiteSpace(userData.DBDefaultLangFilePath) == false)
            {
                arguments.Append($" -lang \"{userData.DBDefaultLangFilePath}\"");
            }
            var proc = Process.Start(userData.DBPath, arguments.ToString());
            if (proc != null)
            {
                proc.EnableRaisingEvents = true;
            }
            return proc;
        }

        /// <summary> Starts DOSBox with <see cref="Game" /> inside it. </summary>
        /// <returns> The DOSBox process. </returns>
        internal Process StartGame() => this.StartGame(this.BuildArgs(false));

        /// <summary>
        /// Starts DOSBox with <see cref="Game" />. <see cref="Game.SetupEXEPath" /> inside it.
        /// </summary>
        /// <returns> The DOSBox process. </returns>
        internal Process StartGameSetup() => this.StartGame(this.BuildArgs(true));

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
            catch
            {
            }
            return null;
        }

        private string AddAdditionalCommands(bool forSetupExe, DOSBoxConfigFile configFile)
        {
            if (configFile.IsAutoExecSectionUsed() == true)
            {
                return string.Empty;
            }

            var commands = new StringBuilder();

            if (StringExt.IsNullOrWhiteSpace(this._game.DOSEXEPath) == false)
            {
                if (!forSetupExe)
                {
                    commands.Append($" {Path.GetFullPath(this._game.DOSEXEPath)}");
                }
                else
                {
                    commands.Append($" {Path.GetFullPath(this._game.SetupEXEPath)}");
                }
            }

            //the game directory mounted as C
            if (StringExt.IsNullOrWhiteSpace(this._game.Directory) == false)
            {
                commands.Append($" -c \"mount c '{this._game.Directory}'\"");
            }

            //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
            if (StringExt.IsNullOrWhiteSpace(this._game.CDPath) == false)
            {
                //put ' and not " after imgmount (or else the path will be misunderstood by DOSBox).
                if (this._game.CDIsAnImage == true)
                {
                    commands.Append(" -c \"imgmount");
                    if (this._game.MountAsFloppy == true)
                    {
                        commands.Append($" a '{this._game.CDPath}' -t floppy\"");
                    }
                    else
                    {
                        commands.Append($" d '{this._game.CDPath}' -t iso\"");
                    }
                }
                else
                {
                    bool addedMountOptions;
                    if (this._game.UseIOCTL == true)
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount d '{this._game.CDPath}' -t cdrom -usecd 0 -ioctl");
                    }
                    else if (this._game.MountAsFloppy == true)
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount a '{this._game.CDPath}' -t floppy");
                    }
                    else
                    {
                        addedMountOptions = true;
                        commands.Append($" -c \"mount d '{this._game.CDPath}'");
                    }
                    if (StringExt.IsNullOrWhiteSpace(this._game.CDLabel) == false && addedMountOptions)
                    {
                        commands.Append($" -label '{this._game.CDLabel}'");
                    }
                    commands.Append('"');
                }
            }

            var gameAdditionnalCommands = this._game.GetAdditionnalCommandsInASingleLine();

            if (StringExt.IsNullOrWhiteSpace(gameAdditionnalCommands) == false)
            {
                commands.Append(gameAdditionnalCommands);
            }

            return commands.ToString();
        }

        private string AddCustomConfigFile()
        {
            string gameConfigFilePath = string.Empty;

            //if the "do not use any config file at all" has not been checked
            if (this._game.NoConfig == false)
            {
                //use at first the game's custom config file
                if (StringExt.IsNullOrWhiteSpace(this._game.DBConfPath) == false)
                {
                    gameConfigFilePath = this._game.DBConfPath;
                }

                //if not, use the default dosbox.conf file
                else if (StringExt.IsNullOrWhiteSpace(_userData.DBDefaultConfFilePath) == false && _userData.DBDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                {
                    gameConfigFilePath = _userData.DBDefaultConfFilePath;
                }
            }
            string dosboxArgs = string.Empty;
            if (StringExt.IsNullOrWhiteSpace(gameConfigFilePath) == false)
            {
                dosboxArgs += $"-conf \"{gameConfigFilePath}\"";
            }

            return dosboxArgs;
        }

        /// <summary> Builds the argument line in order to start DOSBox. </summary>
        /// <param name="forSetupExe">
        /// Whether or not we are starting the game's setup utility or the game itself.
        /// </param>
        /// <returns> The list of command line arguments to pass to DOSBox. </returns>
        private string BuildArgs(bool forSetupExe)
        {
            if (this._game.IsDOSBoxUsed(_userData) == false)
            {
                return string.Empty;
            }
            var configFile = new DOSBoxConfigFile(this._game.DBConfPath);

            var dosboxArgs = new StringBuilder();
            string dosBoxExePath = this._game.GetDOSBoxPath(_userData);
            if (StringExt.IsNullOrWhiteSpace(dosBoxExePath) == true || dosBoxExePath == "dosbox.exe isn't is the same directory as AmpShell.exe!" || File.Exists(dosBoxExePath) == false)
            {
                throw new FileNotFoundException("DOSBox not found!");
            }

            dosboxArgs.Append(this.AddCustomConfigFile());

            if (StringExt.IsNullOrWhiteSpace(_userData.DBDefaultLangFilePath) == false && _userData.DBDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
            {
                dosboxArgs.Append($" -lang \"{_userData.DBDefaultLangFilePath}\"");
            }

            dosboxArgs.Append(this.AddAdditionalCommands(forSetupExe, configFile));

            //corresponds to the Fullscreen checkbox in GameForm
            if (this._game.InFullScreen == true)
            {
                dosboxArgs.Append(" -fullscreen");
            }

            //corresponds to the "no console" checkbox in the GameForm
            if (this._game.NoConsole == true)
            {
                dosboxArgs.Append(" -noconsole");
            }

            //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
            if (this._game.QuitOnExit == true)
            {
                dosboxArgs.Append(" -exit");
            }

            return dosboxArgs.ToString();
        }

        private string[] SplitTargetAndArguments()
        {
            var target = this._game.DOSEXEPath;
            var arguments = string.Empty;
            if (File.Exists(target) == false)
            {
                var directory = Path.GetDirectoryName(target);
                var fileWithArguments = Path.GetFileName(this._game.DOSEXEPath);
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

        /// <summary> Starts DOSBox, and returns its <see cref="Process" />. </summary>
        /// <returns> The DOSBox <see cref="Process" />. </returns>
        private Process StartGame(string args)
        {
            if (this._game.IsDOSBoxUsed(_userData) == false)
            {
                var targetAndArguments = this.SplitTargetAndArguments();
                var nativeLaunchPsi = new ProcessStartInfo(targetAndArguments[0], targetAndArguments[1])
                {
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(this._game.DOSEXEPath)
                };
                return StartProcess(nativeLaunchPsi);
            }
            var psi = new ProcessStartInfo(this._game.GetDOSBoxPath(_userData))
            {
                UseShellExecute = true
            };
            psi.WorkingDirectory = this._game.GetDOSBoxWorkingDirectory(psi.WorkingDirectory, _userData);

            if (StringExt.IsNullOrWhiteSpace(args) == false)
            {
                psi.Arguments = args;
            }
            return StartProcess(psi);
        }
    }
}