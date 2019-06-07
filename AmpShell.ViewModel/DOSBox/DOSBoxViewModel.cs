/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Model.Configuration;
using AmpShell.Model.Core;
using AmpShell.Model.DOSBox;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.ViewModel.DOSBox
{
    /// <summary>
    /// Used to start DOSBox
    /// </summary>
    public static class DOSBoxViewModel
    {
        public static void AskForDOSBox()
        {
            //if DOSBoxPath is still empty, say to the user that dosbox's executable cannot be found
            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserPrefs.DBPath))
            {
                switch (MessageBox.Show("AmpShell cannot find DOSBox, do you want to indicate DOSBox's executable location now ? Choose 'Cancel' to quit.", "Cannot find DOSBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        Environment.Exit(0);
                        break;

                    case DialogResult.Yes:
                        OpenFileDialog dosboxExeFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                            Title = "Please indicate DOSBox's executable location...",
                            Filter = "DOSBox executable (dosbox*)|dosbox*"
                        };
                        if (dosboxExeFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            UserDataLoaderSaver.UserPrefs.DBPath = dosboxExeFileDialog.FileName;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        break;

                    case DialogResult.No:
                        UserDataLoaderSaver.UserPrefs.DBPath = string.Empty;
                        break;
                }
            }
        }

        /// <summary>
        /// Starts DOSBox, and returns whether it was successful.
        /// </summary>
        /// <param name="dosboxPath">Path to DOSBox.exe</param>
        /// <param name="args">Command line args passed to DOSBox</param>
        /// <returns>True if DOSBox started, false if it did not</returns>
        public static Process StartDOSBox(string dosboxPath, string args, string workingDir = "")
        {
            var psi = new ProcessStartInfo(dosboxPath);

            if (string.IsNullOrWhiteSpace(workingDir) == false)
            {
                psi.WorkingDirectory = workingDir;
            }

            if (string.IsNullOrWhiteSpace(args) == false)
            {
                psi.Arguments = args;
            }
            Process dosboxProcess = Process.Start(psi);
            if (dosboxProcess != null)
            {
                dosboxProcess.EnableRaisingEvents = true;
            }
            return dosboxProcess;
        }

        /// <summary>
        /// Builds the argument line in order to start DOSBox
        /// </summary>
        /// <param name="selectedGame">Game the user wants to start</param>
        /// <param name="forSetupExe">Whether or not we are starting the game's setup utility or the game itself</param>
        /// <param name="dosBoxExePath">The path to DOSBox.exe</param>
        /// <param name="dosboxDefaultConfFilePath">The .conf file to use for DOSBox</param>
        /// <param name="dosboxDefaultLangFilePath">The .lng file to use for DOSBox</param>
        /// <returns></returns>
        public static string BuildArgs(Game selectedGame, bool forSetupExe, string dosBoxExePath, string dosboxDefaultConfFilePath, string dosboxDefaultLangFilePath)
        {
            var configFile = new DOSBoxConfigFile(selectedGame.DBConfPath);

            //Arguments string for DOSBox.exe
            string dosboxArgs = string.Empty;
            if (string.IsNullOrWhiteSpace(dosBoxExePath) == false && dosBoxExePath != "dosbox.exe isn't is the same directory as AmpShell.exe!" && File.Exists(dosBoxExePath))
            {
                string quote = char.ToString('"');

                //string for the Game's configuration file.
                string dosboxConfigPath = string.Empty;
                //if the "do not use any config file at all" has not been checked
                if (selectedGame.NoConfig == false)
                {
                    //use at first the game's custom config file
                    if (string.IsNullOrWhiteSpace(selectedGame.DBConfPath) == false)
                    {
                        dosboxConfigPath = selectedGame.DBConfPath;
                    }
                    //if not, use the default dosbox.conf file
                    else if (string.IsNullOrWhiteSpace(dosboxDefaultConfFilePath) == false && dosboxDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                    {
                        dosboxConfigPath = dosboxDefaultConfFilePath;
                    }
                }

                //puting DBCfgPath and Arguments together
                if (string.IsNullOrWhiteSpace(dosboxConfigPath) == false)
                {
                    dosboxArgs = dosboxArgs + " -conf " + '"' + dosboxConfigPath + '"';
                }
                //Path for the default language file used for DOSBox and specified by the user in the Tools menu
                if (string.IsNullOrWhiteSpace(dosboxDefaultLangFilePath) == false && dosboxDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
                {
                    dosboxArgs = dosboxArgs + " -lang " + '"' + dosboxDefaultLangFilePath + '"';
                }

                if (configFile.IsAutoExecSectionUsed() == false)
                {
                    //Additionnal user commands for the game
                    if (string.IsNullOrWhiteSpace(selectedGame.AdditionalCommands) == false)
                    {
                        dosboxArgs = dosboxArgs + " " + selectedGame.AdditionalCommands;
                    }

                    //The arguments for DOSBox begins with the game executable (.exe, .bat, or .com)
                    if (string.IsNullOrWhiteSpace(selectedGame.DOSEXEPath) == false)
                    {
                        if (!forSetupExe)
                        {
                            dosboxArgs = '"' + selectedGame.DOSEXEPath + '"';
                        }
                        else
                        {
                            dosboxArgs = '"' + selectedGame.SetupEXEPath + '"';
                        }
                    }
                    //the game directory mounted as C (if the DOSEXEPath is specified, the DOSEXEPath parent directory will be mounted as C: by DOSBox
                    //hence the "else if" instead of "if".
                    else if (string.IsNullOrWhiteSpace(selectedGame.Directory) == false)
                    {
                        dosboxArgs = " -c " + '"' + "mount c " + quote + selectedGame.Directory + quote + '"';
                    }

                    //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
                    if (string.IsNullOrWhiteSpace(selectedGame.CDPath) == false)
                    {
                        //put ' and _not_ " after imgmount (or else the path will be misunderstood by DOSBox).
                        if (selectedGame.CDIsAnImage == true)
                        {
                            dosboxArgs = dosboxArgs + " -c " + '"' + "imgmount";
                            if (selectedGame.MountAsFloppy == true)
                            {
                                dosboxArgs = dosboxArgs + " a " + quote + selectedGame.CDPath + quote + " -t floppy" + '"';
                            }
                            else
                            {
                                dosboxArgs = dosboxArgs + " d " + quote + selectedGame.CDPath + quote + " -t iso" + '"';
                            }
                        }
                        else
                        {
                            if (selectedGame.UseIOCTL == true)
                            {
                                dosboxArgs = dosboxArgs + " -c " + '"' + "mount d " + quote + selectedGame.CDPath + quote + " -t cdrom -usecd 0 -ioctl" + '"';
                            }
                            else if (selectedGame.MountAsFloppy == true)
                            {
                                dosboxArgs = dosboxArgs + " -c " + '"' + "mount a " + quote + selectedGame.CDPath + quote + " -t floppy" + '"';
                            }
                            else
                            {
                                dosboxArgs = dosboxArgs + " -c " + '"' + "mount d " + quote + selectedGame.CDPath + quote;
                            }
                        }
                    }
                }
                //corresponds to the Fullscreen checkbox in GameForm
                if (selectedGame.InFullScreen == true)
                {
                    dosboxArgs += " -fullscreen";
                }
                //corresponds to the "no console" checkbox in the GameForm
                if (selectedGame.NoConsole == true)
                {
                    dosboxArgs += " -noconsole";
                }
                //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
                if (selectedGame.QuitOnExit == true)
                {
                    dosboxArgs += " -exit";
                }

                return dosboxArgs;
            }
            else
            {
                MessageBox.Show("DOSBox cannot be run (was it deleted ?) !", "Game Launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

        /// <summary>
        /// Run DOSBox itself, without any game
        /// </summary>
        /// <param name="dosboxPath">Path to DOSBox.exe</param>
        /// <param name="dosboxDefaultConfFilePath">Path to DOSBox.conf</param>
        /// <param name="dosboxDefaultLangFilePath">Path to DOSBox.lng</param>
        /// <returns>The DOSBox process if it started successfully, null otherwise</returns>
        public static Process RunDOSBox(string dosboxPath, string dosboxDefaultConfFilePath, string dosboxDefaultLangFilePath)
        {
            if (string.IsNullOrWhiteSpace(dosboxPath) == true)
            {
                return null;
            }
            //check first for the lang file
            string languageFile = string.Empty;
            if (string.IsNullOrWhiteSpace(dosboxDefaultConfFilePath) == false)
            {
                languageFile = " -lang " + '"' + dosboxDefaultConfFilePath + '"';
            }
            //then for the conf file
            if (string.IsNullOrWhiteSpace(dosboxDefaultLangFilePath) == false)
            {
                return DOSBoxViewModel.StartDOSBox(dosboxPath, " -conf " + '"' + dosboxDefaultConfFilePath + '"' + languageFile);
            }
            else
            {
                return DOSBoxViewModel.StartDOSBox(dosboxPath, languageFile);
            }
        }
    }
}