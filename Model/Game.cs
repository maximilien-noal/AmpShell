/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Notification;
using System.Xml.Serialization;

namespace AmpShell.Model
{
    public class Game : PropertyChangedNotifier
    {
        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set { Set<string>(ref _name, value); }
        }

        private string _directory;

        /// <summary>
        /// Game's directory mounted as C:
        /// </summary>
        public string Directory
        {
            get => _directory;
            set { Set<string>(ref _directory, value); }
        }

        private string _cdPath;

        /// <summary>
        /// Game's CD image / CD directory (like 'D:\') location
        /// </summary>
        public string CDPath
        {
            get => _cdPath;
            set { Set<string>(ref _cdPath, value); }
        }

        private string _setupEEXEPath;

        /// <summary>
        /// Game's setup executable location
        /// </summary>
        public string SetupEXEPath
        {
            get => _setupEEXEPath;
            set { Set<string>(ref _setupEEXEPath, value); }
        }

        private string _dbConfPath;

        /// <summary>
        /// Game's custom DOSBox .conf file path
        /// </summary>
        public string DBConfPath
        {
            get => _dbConfPath;
            set { Set<string>(ref _dbConfPath, value); }
        }

        private string _additionalCommands;

        /// <summary>
        /// Game's additionnal commands for DOSBox
        /// </summary>
        public string AdditionalCommands
        {
            get => _additionalCommands;
            set { Set<string>(ref _additionalCommands, value); }
        }

        private bool _useIOCTL;

        /// <summary>
        /// Option to use IOCTL (only available for optical drives)
        /// </summary>
        public bool UseIOCTL
        {
            get => _useIOCTL;
            set { Set<bool>(ref _useIOCTL, value); }
        }

        private bool _mountAsFloppy;

        /// <summary>
        /// Option to use the image file as a floppy (A:)
        /// </summary>
        public bool MountAsFloppy
        {
            get => _mountAsFloppy;
            set { Set<bool>(ref _mountAsFloppy, value); }
        }

        private bool _noConfig;

        /// <summary>
        /// Boolean if no config is used ("Don't use any config file at all" checkbox in GameForm)
        /// Legacy 0.72 or older DOSBox option
        /// </summary>
        public bool NoConfig
        {
            get => _noConfig;
            set { Set<bool>(ref _noConfig, value); }
        }

        private bool _inFullScreen;

        public bool InFullScreen
        {
            get => _inFullScreen;
            set { Set<bool>(ref _inFullScreen, value); }
        }

        private bool _noConsole;

        /// <summary>
        /// Boolean for displaying DOSBox's console
        /// </summary>
        public bool NoConsole
        {
            get => _noConsole;
            set { Set<bool>(ref _noConsole, value); }
        }

        private bool _quitOnExit;

        /// <summary>
        /// Boolean for the -exit switch for DOSBox (if set to true, DOSBox closes when the game exits)
        /// </summary>
        public bool QuitOnExit
        {
            get => _quitOnExit;
            set { Set<bool>(ref _quitOnExit, value); }
        }

        private string _dosExePath;

        /// <summary>
        /// Game's main executable location
        /// </summary>
        public string DOSEXEPath
        {
            get => _dosExePath;
            set { Set<string>(ref _dosExePath, value); }
        }

        private bool _cdIsAnImage;

        /// <summary>
        /// True if GameCDPath points to a CD image file, wrong if it points to a directory
        /// </summary>
        public bool CDIsAnImage
        {
            get => _cdIsAnImage;
            set { Set<bool>(ref _cdIsAnImage, value); }
        }

        private string _icon;

        public string Icon
        {
            get => _icon;
            set { Set<string>(ref _icon, value); }
        }

        private string _alternateDOSBoxExePath;

        /// <summary>
        /// If we want to use DOSBox Daum, ECE, SVN, or other instead of the one set in the global preferences
        /// </summary>
        public string AlternateDOSBoxExePath
        {
            get => _alternateDOSBoxExePath;
            set { Set<string>(ref _alternateDOSBoxExePath, value); }
        }
    }
}