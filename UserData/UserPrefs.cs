/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AmpShell.UserData
{
    [XmlType(TypeName="Window")]
    public class UserPrefs : UserDataRootNode
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };
        /// <summary>
        /// Integer for AmpShell's window Width
        /// </summary>
        private int _width;
        /// <summary>
        /// Integer for AmpShell's window Height
        /// </summary>
        private int _height;
        private int _x;
        private int _y;
        private bool _portableMode;
        private bool _defaultIconViewOverride;
        /// <summary>
        /// String for DOSBox.exe location
        /// </summary>
        private string _dbPath;
        /// <summary>
        /// string for the default dosbox.conf location
        /// </summary>
        private string _dbDefaultConfFilePath;
        /// <summary>
        /// string for the default language (.lng) file location
        /// </summary>
        private string _dbDefaultLangFilePath;
        private string _CDsDefaultDir;
        private string _gamesDefaultDir;
        private bool _gamesNoConsole;
        private bool _gamesInFullScreen;
        private bool _gamesQuitOnExit;
        private string _gamesAdditionalCommands;
        private bool _onlyNames;
        private bool _fullscreen;
        private bool _menuBarVisible;
        private bool _toolBarVisible;
        private bool _statusBarVisible;
        private string _configEditorPath;
        private string _configEditorAdditionalParameters;
        private bool _categoryDeletePrompt;
        private bool _gameDeletePrompt;
        private bool _rememberWindowSize;
        private bool _rememberWindowPosition;
        private System.Windows.Forms.View _categoriesDefaultViewMode;
        private int _largeViewModeSize;

        public UserPrefs() : base()
        {
            Width = 0;
            Height = 0;
            X = 0;
            Y = 0;
            PortableMode = false;
            DefaultIconViewOverride = false;
            GameDeletePrompt = true;
            CategoryDeletePrompt = true;
            ConfigEditorPath = string.Empty;
            ConfigEditorAdditionalParameters = string.Empty;
            MenuBarVisible = true;
            ToolBarVisible = true;
            StatusBarVisible = true;
            DBPath = string.Empty;
            GamesNoConsole = false;
            GamesInFullScreen = false;
            GamesQuitOnExit = false;
            OnlyNames = false;
            Fullscreen = false;
            GamesAdditionalCommands = string.Empty;
            GamesDefaultDir = string.Empty;
            CDsDefaultDir = string.Empty;
            DBDefaultConfFilePath = string.Empty;
            DBDefaultLangFilePath = string.Empty;
            RememberWindowPosition = true;
            RememberWindowSize = true;
            LargeViewModeSize = 48;
            CategoriesDefaultViewMode = System.Windows.Forms.View.LargeIcon;
        }

        public bool PortableMode
        {
            get => _portableMode;
            set
            {
                if (value != _portableMode)
                {
                    _portableMode = value;
                }
            }
        }

        public bool DefaultIconViewOverride
        {
            get => _defaultIconViewOverride;
            set
            {
                if (value != _defaultIconViewOverride)
                {
                    _defaultIconViewOverride = value;
                }
            }
        }

        public int X
        {
            get => _x;
            set
            {
                if (value != _x)
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (value != _y)
                {
                    _y = value;
                }
            }
        }

        public System.Windows.Forms.View CategoriesDefaultViewMode
        {
            get => _categoriesDefaultViewMode;
            set
            {
                if (value != _categoriesDefaultViewMode)
                {
                    _categoriesDefaultViewMode = value;
                }
            }
        }

        public bool RememberWindowPosition
        {
            get => _rememberWindowPosition;
            set
            {
                if (value != _rememberWindowPosition)
                {
                    _rememberWindowPosition = value;
                }
            }
        }

        public bool RememberWindowSize
        {
            get => _rememberWindowSize;
            set
            {
                if (value != _rememberWindowSize)
                {
                    _rememberWindowSize = value;
                }
            }
        }

        public bool GameDeletePrompt
        {
            get => _gameDeletePrompt;
            set
            {
                if (value != _gameDeletePrompt)
                {
                    _gameDeletePrompt = value;
                }
            }
        }

        public bool CategoryDeletePrompt
        {
            get => _categoryDeletePrompt;
            set
            {
                if (value != _categoryDeletePrompt)
                {
                    _categoryDeletePrompt = value;
                }
            }
        }

        public bool OnlyNames
        {
            get => _onlyNames;
            set
            {
                if (value != _onlyNames)
                {
                    _onlyNames = value;
                }
            }
        }

        public bool GamesNoConsole
        {
            get => _gamesNoConsole;
            set
            {
                if (value != _gamesNoConsole)
                {
                    _gamesNoConsole = value;
                }
            }
        }

        public bool GamesInFullScreen
        {
            get => _gamesInFullScreen;
            set
            {
                if (value != _gamesInFullScreen)
                {
                    _gamesInFullScreen = value;
                }
            }
        }

        public bool GamesQuitOnExit
        {
            get => _gamesQuitOnExit;
            set
            {
                if (value != _gamesQuitOnExit)
                {
                    _gamesQuitOnExit = value;
                }
            }
        }

        public string GamesAdditionalCommands
        {
            get => _gamesAdditionalCommands;
            set
            {
                if (value != _gamesAdditionalCommands)
                {
                    _gamesAdditionalCommands = value;
                }
            }
        }

        public string GamesDefaultDir
        {
            get => _gamesDefaultDir;
            set
            {
                if (value != _gamesDefaultDir)
                {
                    _gamesDefaultDir = value;
                }
            }
        }

        public string CDsDefaultDir
        {
            get => _CDsDefaultDir;
            set
            {
                if (value != _CDsDefaultDir)
                {
                    _CDsDefaultDir = value;
                }
            }
        }

        public string ConfigEditorPath
        {
            get => _configEditorPath;
            set
            {
                if (value != _configEditorPath)
                {
                    _configEditorPath = value;
                }
            }
        }

        public string ConfigEditorAdditionalParameters
        {
            get => _configEditorAdditionalParameters;
            set
            {
                if (value != _configEditorAdditionalParameters)
                {
                    _configEditorAdditionalParameters = value;
                }
            }
        }

        public bool Fullscreen
        {
            get => _fullscreen;
            set
            {
                if (value != _fullscreen)
                {
                    _fullscreen = value;
                }
            }
        }

        public bool MenuBarVisible
        {
            get => _menuBarVisible;
            set
            {
                if (_menuBarVisible != value)
                {
                    _menuBarVisible = value;
                }
            }
        }

        public bool ToolBarVisible
        {
            get => _toolBarVisible;
            set
            {
                if (_toolBarVisible != value)
                {
                    _toolBarVisible = value;
                }
            }
        }

        public bool StatusBarVisible
        {
            get => _statusBarVisible;
            set
            {
                if (_statusBarVisible != value)
                {
                    _statusBarVisible = value;
                }
            }
        }

        public string DBPath
        {
            get => _dbPath;
            set
            {
                if (value != _dbPath)
                {
                    _dbPath = value;
                }
            }
        }

        public string DBDefaultConfFilePath
        {
            get => _dbDefaultConfFilePath;
            set
            {
                if (value != _dbDefaultConfFilePath)
                {
                    _dbDefaultConfFilePath = value;
                }
            }
        }

        public string DBDefaultLangFilePath
        {
            get => _dbDefaultLangFilePath;
            set
            {
                if (value != _dbDefaultLangFilePath)
                {
                    _dbDefaultLangFilePath = value;
                }
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                if (value != _width)
                {
                    _width = value;
                }
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                if (value != _height)
                {
                    _height = value;
                }
            }
        }

        public int LargeViewModeSize
        {
            get => _largeViewModeSize;
            set
            {
                if (LargeViewModeSizes.Contains(value))
                {
                    _largeViewModeSize = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", "Possible values : " + LargeViewModeSizes.ToArray().ToString());
                }
            }
        }
    }
}