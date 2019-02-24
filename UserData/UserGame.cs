/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.UserData
{
    public class UserGame : UserCategory
    {
        /// <summary>
        /// Game's name
        /// </summary>
        private string _name;
        /// <summary>
        /// Game's setup executable location
        /// </summary>
        private string _setupExePath;
        /// <summary>
        /// Game's directory mounted as C:
        /// </summary>
        private string _directory;
        /// <summary>
        /// Game's custom DOSBox .conf file path
        /// </summary>
        private string _confFilePath;
        /// <summary>
        /// Game's main executable location
        /// </summary>
        private string _dosExePath;
        /// <summary>
        /// Game's CD image / CD directory (like 'D:\') location
        /// </summary>
        private string _cdPath;
        /// <summary>
        /// True if GameCDPath points to a CD image file, wrong if it points to a directory
        /// </summary>
        private bool _cdImage;
        /// <summary>
        /// Option to use IOCTL (only available for optical drives)
        /// </summary>
        private bool _useIoctl;
        /// <summary>
        /// Option to use the image file as a floppy (A:)
        /// </summary>
        private bool _mountAsFloppy;
        /// <summary>
        /// Game's additionnal commands for DOSBox
        /// </summary>
        private string _AdditionalCommands;
        /// <summary>
        /// Boolean if no config is used ("Don't use any config file at all" checkbox in GameForm)
        /// Legacy 0.73 or older DOSBox option
        /// </summary>
        private bool _noConfig;
        /// <summary>
        /// Boolean for fullscreen mode
        /// </summary>
        private bool _inFullScreen;
        /// <summary>
        /// Boolean for displaying DOSBox's console
        /// </summary>
        private bool _noConsole;
        /// <summary>
        /// Boolean for the -exit switch for DOSBox (if True, DOSBox closes when the game exits)
        /// </summary>
        private bool _quitOnExit;
        /// <summary>
        /// Image containing the game's icon
        /// </summary>
        private string _icon;

        public UserGame() : base()
        {
            Name = string.Empty;
            SetupEXEPath = string.Empty;
            Directory = string.Empty;
            DBConfPath = string.Empty;
            DOSEXEPath = string.Empty;
            CDPath = string.Empty;
            AdditionalCommands = string.Empty;
            Icon = string.Empty;
            UseIOCTL = false;
            MountAsFloppy = false;
            NoConfig = false;
            NoConsole = false;
            InFullScreen = false;
            QuitOnExit = false;
            CDIsAnImage = false;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                }
            }
        }

        public string Directory
        {
            get => _directory;
            set
            {
                if (value != _directory)
                {
                    _directory = value;
                }
            }
        }

        public string CDPath
        {
            get => _cdPath;
            set
            {
                if (value != _cdPath)
                {
                    _cdPath = value;
                }
            }
        }

        public string SetupEXEPath
        {
            get => _setupExePath;
            set
            {
                if (value != _setupExePath)
                {
                    _setupExePath = value;
                }
            }
        }

        public string DBConfPath
        {
            get => _confFilePath;
            set
            {
                if (value != _confFilePath)
                {
                    _confFilePath = value;
                }
            }
        }

        public string AdditionalCommands
        {
            get => _AdditionalCommands;
            set
            {
                if (value != _AdditionalCommands)
                {
                    _AdditionalCommands = value;
                }
            }
        }

        public bool UseIOCTL
        {
            get => _useIoctl;
            set
            {
                if (value != _useIoctl)
                {
                    _useIoctl = value;
                }
            }
        }

        public bool MountAsFloppy
        {
            get => _mountAsFloppy;
            set
            {
                if (value != _mountAsFloppy)
                {
                    _mountAsFloppy = value;
                }
            }
        }

        public bool NoConfig
        {
            get => _noConfig;
            set
            {
                if (value != _noConfig)
                {
                    _noConfig = value;
                }
            }
        }

        public bool InFullScreen
        {
            get => _inFullScreen;
            set
            {
                if (value != _inFullScreen)
                {
                    _inFullScreen = value;
                }
            }
        }

        public bool NoConsole
        {
            get => _noConsole;
            set
            {
                if (value != _noConsole)
                {
                    _noConsole = value;
                }
            }
        }

        public bool QuitOnExit
        {
            get => _quitOnExit;
            set
            {
                if (value != _quitOnExit)
                {
                    _quitOnExit = value;
                }
            }
        }

        public string DOSEXEPath
        {
            get => _dosExePath;
            set
            {
                if (value != _dosExePath)
                {
                    _dosExePath = value;
                }
            }
        }

        public bool CDIsAnImage
        {
            get => _cdImage;
            set
            {
                if (value != _cdImage)
                {
                    _cdImage = value;
                }
            }
        }

        public string Icon
        {
            get => _icon;
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                }
            }
        }
    }
}