/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

#pragma warning disable SA1201 // Elements should appear in the correct order
#pragma warning disable SA1101 // Prefix local calls with this

namespace AmpShell.Model
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Xml.Serialization;

    using AmpShell.DAL;
    using AmpShell.DOSBox;
    using AmpShell.Notification;

    public class Game : PropertyChangedNotifier
    {
        private string notes = string.Empty;

        public string Notes
        {
            get => notes;
            set { Set(ref notes, value); }
        }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        private string name = string.Empty;

        private bool usesDOSBox = true;

        public bool UsesDOSBox
        {
            get => usesDOSBox;
            set { Set(ref usesDOSBox, value); }
        }

        public string Name
        {
            get => name;
            set { Set(ref name, value); }
        }

        private DateTime releaseDate = DateTime.Parse("01/01/1980", CultureInfo.InvariantCulture);

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set { Set(ref releaseDate, value); }
        }

        private string directory = string.Empty;

        /// <summary>
        /// Gets or sets game's directory mounted as C:.
        /// </summary>
        public string Directory
        {
            get => directory;
            set { Set(ref directory, value); }
        }

        private string cdPath = string.Empty;

        /// <summary>
        /// Gets or sets game's CD image / CD directory (like 'D:\') location.
        /// </summary>
        public string CDPath
        {
            get => cdPath;
            set { Set(ref cdPath, value); }
        }

        private string cdLabel = string.Empty;

        /// <summary>
        /// Gets or sets optional, user-specified CD LABEL (only when it is not an image).
        /// </summary>
        public string CDLabel
        {
            get => cdLabel;
            set { Set(ref cdLabel, value); }
        }

        private string setupEXEPath = string.Empty;

        /// <summary>
        /// Gets or sets game's setup executable location.
        /// </summary>
        public string SetupEXEPath
        {
            get => setupEXEPath;
            set { Set(ref setupEXEPath, value); }
        }

        private string dbConfPath = string.Empty;

        internal string PutEachAdditionnalCommandsOnANewLine()
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
                if (StringExt.IsNullOrWhiteSpace(line) == false)
                {
                    var trimmedLine = line.Trim().TrimStart('"').TrimEnd('"');
                    commands.AppendLine(trimmedLine);
                }
            }

            return commands.ToString();
        }

        /// <summary>
        /// Gets or sets game's custom DOSBox .conf file path.
        /// </summary>
        public string DBConfPath
        {
            get => dbConfPath;
            set { Set(ref dbConfPath, value); }
        }

        internal bool IsDOSBoxUsed() => UserDataAccessor.UserData.GamesUseDOSBox == true && this.UsesDOSBox == true;

        private string additionalCommands = string.Empty;

        /// <summary>
        /// Gets or sets game's additional commands for DOSBox.
        /// </summary>
        public string AdditionalCommands
        {
            get => additionalCommands;
            set { Set(ref additionalCommands, value); }
        }

        private bool useIOCTL;

        /// <summary>
        /// Gets or sets a value indicating whether option to use IOCTL (only available for optical drives).
        /// </summary>
        public bool UseIOCTL
        {
            get => useIOCTL;
            set { Set(ref useIOCTL, value); }
        }

        private bool mountAsFloppy;

        /// <summary>
        /// Gets or sets a value indicating whether option to use the image file as a floppy (A:).
        /// </summary>
        public bool MountAsFloppy
        {
            get => mountAsFloppy;
            set { Set(ref mountAsFloppy, value); }
        }

        private bool noConfig;

        /// <summary>
        /// Gets or sets a value indicating whether boolean if no config is used ("Don't use any
        /// config file at all" checkbox in GameForm) Legacy 0.72 or older DOSBox option.
        /// </summary>
        public bool NoConfig
        {
            get => noConfig;
            set { Set(ref noConfig, value); }
        }

        private bool inFullScreen;

        public bool InFullScreen
        {
            get => inFullScreen;
            set { Set(ref inFullScreen, value); }
        }

        private bool noConsole;

        /// <summary>
        /// Gets or sets a value indicating whether boolean for displaying DOSBox's console.
        /// </summary>
        public bool NoConsole
        {
            get => noConsole;
            set { Set<bool>(ref noConsole, value); }
        }

        private bool quitOnExit;

        /// <summary>
        /// Gets or sets a value indicating whether boolean for the -exit switch for DOSBox (if set
        /// to true, DOSBox closes when the game exits).
        /// </summary>
        public bool QuitOnExit
        {
            get => quitOnExit;
            set { Set(ref quitOnExit, value); }
        }

        private string dosExePath = string.Empty;

        /// <summary>
        /// Gets or sets game's main executable location.
        /// </summary>
        public string DOSEXEPath
        {
            get => dosExePath;
            set { Set(ref dosExePath, value); }
        }

        private bool cdIsAnImage;

        /// <summary>
        /// Gets or sets a value indicating whether if GameCDPath points to a CD image file (false
        /// if it points to a directory).
        /// </summary>
        public bool CDIsAnImage
        {
            get => cdIsAnImage;
            set { Set(ref cdIsAnImage, value); }
        }

        private string icon = string.Empty;

        public string Icon
        {
            get => icon;
            set { Set(ref icon, value); }
        }

        private string alternateDOSBoxExePath = string.Empty;

        /// <summary>
        /// Gets or sets the path to DOSBox Daum, ECE, SVN, DOSBox-X, DOSBox Staging,
        /// or other fork instead of the DOSBox version set in the global preferences.
        /// </summary>
        public string AlternateDOSBoxExePath
        {
            get => alternateDOSBoxExePath;
            set { Set(ref alternateDOSBoxExePath, value); }
        }

        public string GetDOSBoxPath() => StringExt.IsNullOrWhiteSpace(AlternateDOSBoxExePath) ? UserDataAccessor.UserData.DBPath : AlternateDOSBoxExePath;

        public Process Run() => new DOSBoxController(this).StartGame();

        public Process RunSetup() => new DOSBoxController(this).StartGameSetup();

        internal void OpenGameFolder()
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = Path.GetDirectoryName(this.DOSEXEPath), UseShellExecute = true });
            }
            catch (Exception)
            {
            }
        }
    }
}