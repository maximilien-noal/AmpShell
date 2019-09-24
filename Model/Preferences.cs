/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using System.Collections.Generic;
using System.Xml.Serialization;

namespace AmpShell.Model
{
    [XmlType(TypeName = "Window")]
    public class Preferences : ModelWithChildren
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };

        private bool _portableMode = false;

        public bool PortableMode
        {
            get => _portableMode;
            set { Set<bool>(ref _portableMode, value); }
        }

        private bool _defaultIconViewOverride = false;

        public bool DefaultIconViewOverride
        {
            get => _defaultIconViewOverride;
            set { Set<bool>(ref _defaultIconViewOverride, value); }
        }

        public int X { get; set; }

        public int Y { get; set; }

        private System.Windows.Forms.View _categoriesDefaultViewMode = System.Windows.Forms.View.LargeIcon;

        public System.Windows.Forms.View CategoriesDefaultViewMode
        {
            get => _categoriesDefaultViewMode;
            set { Set<System.Windows.Forms.View>(ref _categoriesDefaultViewMode, value); }
        }

        private bool _rememberWindowPosition = true;

        public bool RememberWindowPosition
        {
            get => _rememberWindowPosition;
            set { Set<bool>(ref _rememberWindowPosition, value); }
        }

        private bool _rememberWindowSize = true;

        public bool RememberWindowSize
        {
            get => _rememberWindowSize;
            set { Set<bool>(ref _rememberWindowSize, value); }
        }

        private bool _gameDeletePrompt = true;

        public bool GameDeletePrompt
        {
            get => _gameDeletePrompt;
            set { Set<bool>(ref _gameDeletePrompt, value); }
        }

        private bool _categoryDeletePrompt = true;

        public bool CategoryDeletePrompt
        {
            get => _categoryDeletePrompt;
            set { Set<bool>(ref _categoryDeletePrompt, value); }
        }

        private bool _gamesNoConsole = true;

        public bool GamesNoConsole
        {
            get => _gamesNoConsole;
            set { Set<bool>(ref _gamesNoConsole, value); }
        }

        private bool _gamesInFullScreen = true;

        public bool GamesInFullScreen
        {
            get => _gamesInFullScreen;
            set { Set<bool>(ref _gamesInFullScreen, value); }
        }

        private bool _gamesQuitOnExit = true;

        public bool GamesQuitOnExit
        {
            get => _gamesQuitOnExit;
            set { Set<bool>(ref _gamesQuitOnExit, value); }
        }

        private string _gamesAdditionalCommands;

        public string GamesAdditionalCommands
        {
            get => _gamesAdditionalCommands;
            set { Set<string>(ref _gamesAdditionalCommands, value); }
        }

        private string _gamesDefaultDir = "";

        public string GamesDefaultDir
        {
            get => _gamesDefaultDir;
            set { Set<string>(ref _gamesDefaultDir, value); }
        }

        private string _cdsDefaultDir = "";

        public string CDsDefaultDir
        {
            get => _cdsDefaultDir;
            set { Set<string>(ref _cdsDefaultDir, value); }
        }

        private string _configEditorPath = "";

        public string ConfigEditorPath
        {
            get => _configEditorPath;
            set { Set<string>(ref _configEditorPath, value); }
        }

        private string _configEditorAdditionalParameters = "";

        public string ConfigEditorAdditionalParameters
        {
            get => _configEditorAdditionalParameters;
            set { Set<string>(ref _configEditorAdditionalParameters, value); }
        }

        private bool _fullScreen;

        public bool Fullscreen
        {
            get => _fullScreen;
            set { Set<bool>(ref _fullScreen, value); }
        }

        private bool _menuBarVisible = true;

        public bool MenuBarVisible
        {
            get => _menuBarVisible;
            set { Set<bool>(ref _menuBarVisible, value); }
        }

        private bool _toolBarVisible = true;

        public bool ToolBarVisible
        {
            get => _toolBarVisible;
            set { Set<bool>(ref _toolBarVisible, value); }
        }

        private bool _statusBarVisible = true;

        public bool StatusBarVisible
        {
            get => _statusBarVisible;
            set { Set<bool>(ref _statusBarVisible, value); }
        }

        private string _dbPath = "";

        public string DBPath
        {
            get => _dbPath;
            set { Set<string>(ref _dbPath, value); }
        }

        private string _dbDefaultConfFilePath = "";

        public string DBDefaultConfFilePath
        {
            get => _dbDefaultConfFilePath;
            set { Set<string>(ref _dbDefaultConfFilePath, value); }
        }

        private string _dbDefaultLangFilePath = "";

        public string DBDefaultLangFilePath
        {
            get => _dbDefaultLangFilePath;
            set { Set<string>(ref _dbDefaultLangFilePath, value); }
        }

        private int _width = 640;

        public int Width
        {
            get => _width;
            set { Set<int>(ref _width, value); }
        }

        private int _height = 400;

        public int Height
        {
            get => _height;
            set { Set<int>(ref _height, value); }
        }

        private int _largeViewModeSize = 48;

        public int LargeViewModeSize
        {
            get => _largeViewModeSize;
            set { Set<int>(ref _largeViewModeSize, value); }
        }
    }
}