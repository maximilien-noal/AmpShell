/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Core.Model
{
    using AmpShell.Core.Games;
    using AmpShell.Core.Notification;

    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Game : PropertyChangedNotifier
    {
        private string additionalCommands = string.Empty;

        private string alternateDOSBoxExePath = string.Empty;

        private bool cdIsAnImage;

        private string cdLabel = string.Empty;

        private string cdPath = string.Empty;

        private string dbConfPath = string.Empty;

        private string directory = string.Empty;

        private string dosboxWorkingDirectory = string.Empty;

        private string dosExePath = string.Empty;

        private string icon = string.Empty;

        private bool inFullScreen;

        private bool mountAsFloppy;

        private string name = string.Empty;

        private bool noConfig;

        private bool noConsole;

        private string notes = string.Empty;

        private bool quitOnExit;

        private DateTime releaseDate = DateTime.Parse("01/01/1980", CultureInfo.InvariantCulture);

        private string setupEXEPath = string.Empty;

        private bool useIOCTL;

        private bool usesDOSBox = true;

        /// <summary> Gets or sets game's additional commands for DOSBox. </summary>
        public string AdditionalCommands
        {
            get => additionalCommands;
            set { Set(ref additionalCommands, value); }
        }

        /// <summary>
        /// Gets or sets the path to DOSBox Daum, ECE, SVN, DOSBox-X, DOSBox Staging, or other fork
        /// instead of the DOSBox version set in the global preferences.
        /// </summary>
        public string AlternateDOSBoxExePath
        {
            get => alternateDOSBoxExePath;
            set { Set(ref alternateDOSBoxExePath, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether if GameCDPath points to a CD image file (false
        /// if it points to a directory).
        /// </summary>
        public bool CDIsAnImage
        {
            get => cdIsAnImage;
            set { Set(ref cdIsAnImage, value); }
        }

        /// <summary>
        /// Gets or sets optional, user-specified CD LABEL (only when it is not an image).
        /// </summary>
        public string CDLabel
        {
            get => cdLabel;
            set { Set(ref cdLabel, value); }
        }

        /// <summary> Gets or sets game's CD image / CD directory (like 'D:\') location. </summary>
        public string CDPath
        {
            get => cdPath;
            set { Set(ref cdPath, value); }
        }

        /// <summary> Gets or sets game's custom DOSBox .conf file path. </summary>
        public string DBConfPath
        {
            get => dbConfPath;
            set { Set(ref dbConfPath, value); }
        }

        /// <summary> Gets or sets game's directory mounted as C:. </summary>
        public string Directory
        {
            get => directory;
            set { Set(ref directory, value); }
        }

        public string DOSBoxWorkingDirectory
        {
            get => dosboxWorkingDirectory;
            set { Set(ref dosboxWorkingDirectory, value); }
        }

        /// <summary> Gets or sets game's main executable location. </summary>
        public string DOSEXEPath
        {
            get => dosExePath;
            set { Set(ref dosExePath, value); }
        }

        public string Icon
        {
            get => icon;
            set { Set(ref icon, value); }
        }

        public bool InFullScreen
        {
            get => inFullScreen;
            set { Set(ref inFullScreen, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether option to use the image file as a floppy (A:).
        /// </summary>
        public bool MountAsFloppy
        {
            get => mountAsFloppy;
            set { Set(ref mountAsFloppy, value); }
        }

        public string Name
        {
            get => name;
            set { Set(ref name, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether boolean if no config is used ("Don't use any
        /// config file at all" checkbox in GameForm) Legacy 0.72 or older DOSBox option.
        /// </summary>
        public bool NoConfig
        {
            get => noConfig;
            set { Set(ref noConfig, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether boolean for displaying DOSBox's console.
        /// </summary>
        public bool NoConsole
        {
            get => noConsole;
            set { Set<bool>(ref noConsole, value); }
        }

        public string Notes
        {
            get => notes;
            set { Set(ref notes, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether boolean for the -exit switch for DOSBox (if set
        /// to true, DOSBox closes when the game exits).
        /// </summary>
        public bool QuitOnExit
        {
            get => quitOnExit;
            set { Set(ref quitOnExit, value); }
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set { Set(ref releaseDate, value); }
        }

        /// <summary> Gets or sets game's setup executable location. </summary>
        public string SetupEXEPath
        {
            get => setupEXEPath;
            set { Set(ref setupEXEPath, value); }
        }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether option to use IOCTL (only available for optical drives).
        /// </summary>
        public bool UseIOCTL
        {
            get => useIOCTL;
            set { Set(ref useIOCTL, value); }
        }

        public bool UsesDOSBox
        {
            get => usesDOSBox;
            set { Set(ref usesDOSBox, value); }
        }

        public string GetAdditionnalCommandsInASingleLine()
        {
            var commandLine = new StringBuilder();
            string[] array = this.AdditionalCommands.Split('\r');
            for (int i = 0; i < array.Length; i++)
            {
                string line = array[i];
                line = line.Trim();
                if (line.ToUpperInvariant().StartsWith("REM") == false && StringExt.IsNullOrWhiteSpace(line) == false)
                {
                    commandLine.Append($"-c \"{line}\"");
                    if (i > 0)
                    {
                        commandLine.Append(' ');
                    }
                }
            }
            return commandLine.ToString();
        }

        public string GetDOSBoxPath(Preferences userData) => StringExt.IsNullOrWhiteSpace(AlternateDOSBoxExePath) ? userData.DBPath : AlternateDOSBoxExePath;

        public string GetFileDialogInitialDirectoryFromModel(Preferences userData)
        {
            if (StringExt.IsNullOrWhiteSpace(this.DOSEXEPath) == false && System.IO.Directory.Exists(Path.GetDirectoryName(this.DOSEXEPath)))
            {
                return Path.GetDirectoryName(this.DOSEXEPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.Directory) == false && System.IO.Directory.Exists(this.Directory))
            {
                return this.Directory;
            }
            else if (StringExt.IsNullOrWhiteSpace(this.SetupEXEPath) == false && System.IO.Directory.Exists(Path.GetDirectoryName(this.SetupEXEPath)))
            {
                return Path.GetDirectoryName(this.SetupEXEPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.Icon) == false && File.Exists(this.Icon))
            {
                return Path.GetDirectoryName(this.Icon);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.DBConfPath) == false && System.IO.Directory.Exists(Path.GetDirectoryName(this.DBConfPath)))
            {
                return Path.GetDirectoryName(this.DBConfPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(userData.GamesDefaultDir) == false && System.IO.Directory.Exists(userData.GamesDefaultDir))
            {
                return userData.GamesDefaultDir;
            }
            return string.Empty;
        }

        public bool IsDOSBoxUsed(Preferences userData) => userData.GamesUseDOSBox == true && this.UsesDOSBox == true;

        public bool IsDOSBoxXUsed(Preferences userData)
        {
            if (IsDOSBoxUsed(userData) == false)
            {
                return false;
            }
            var dbPath = GetDOSBoxPath(userData);
            if (StringExt.IsNullOrWhiteSpace(dbPath) == true)
            {
                return false;
            }
            if (File.Exists(dbPath))
            {
                return false;
            }
            return Path.GetFileNameWithoutExtension(dbPath).ToUpperInvariant().Contains("DOSBOX-X");
        }

        public void OpenGameFolder()
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = GetGameFolder(), UseShellExecute = true });
            }
            catch (Exception)
            {
            }
        }

        public string PutEachAdditionnalCommandsOnANewLine()
        {
            if (StringExt.IsNullOrWhiteSpace(this.AdditionalCommands))
            {
                return string.Empty;
            }
            var commands = new System.Text.StringBuilder();
            var lines = this.AdditionalCommands.Replace("-c", "\r");
            var array = lines.Split('\r');
            for (int i = 0; i < array.Length; i++)
            {
                var line = array[i];
                if (StringExt.IsNullOrWhiteSpace(line) == false && (array.Length == 1 && line.StartsWith("REM Put each command on a new line")) == false)
                {
                    var trimmedLine = line.Trim().TrimStart('"').TrimEnd('"');
                    commands.AppendLine(trimmedLine);
                }
            }

            return commands.ToString();
        }

        public Process Run(Preferences userData) => new GameProcessController(this, userData).StartGame();

        public Process RunSetup(Preferences userData) => new GameProcessController(this, userData).StartGameSetup();

        internal string GetDOSBoxWorkingDirectory(string initialValue, Preferences userData)
        {
            if (IsDOSBoxUsed(userData) == false)
            {
                return initialValue;
            }
            if (StringExt.IsNullOrWhiteSpace(DOSBoxWorkingDirectory) == false)
            {
                return Path.GetDirectoryName(DOSBoxWorkingDirectory);
            }
            else if (IsDOSBoxXUsed(userData))
            {
                return Path.GetDirectoryName(GetDOSBoxPath(userData));
            }
            else if (StringExt.IsNullOrWhiteSpace(DBConfPath) == false)
            {
                return Path.GetDirectoryName(DBConfPath);
            }
            return initialValue;
        }

        private string GetGameFolder() => Path.GetDirectoryName(new string[] { this.DOSEXEPath, this.Directory, this.SetupEXEPath, this.DBConfPath, this.Icon, this.AlternateDOSBoxExePath, this.CDPath }.FirstOrDefault(x => StringExt.IsNullOrWhiteSpace(x) == false && (System.IO.Directory.Exists(x) || File.Exists(x))));
    }
}