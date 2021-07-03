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
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(TypeName = "Window")]
    public class Preferences : ModelWithChildren
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };

        private View categoriesDefaultViewMode = View.LargeIcon;

        private bool categoryDeletePrompt = true;

        private string cdsDefaultDir = string.Empty;

        private string configEditorAdditionalParameters = string.Empty;

        private string configEditorPath = string.Empty;

        private string dbDefaultConfFilePath = string.Empty;

        private string dbDefaultLangFilePath = string.Empty;

        private string dbPath = string.Empty;

        private bool defaultIconViewOverride = false;

        private bool fullScreen;

        private bool gameDeletePrompt = true;

        private string gamesAdditionalCommands;

        private string gamesDefaultDir = string.Empty;

        private bool gamesInFullScreen = true;

        private bool gamesNoConsole = true;

        private bool gamesQuitOnExit = true;

        private bool gamesUseDOSBox = true;

        private int height = 400;

        private int largeViewModeSize = 48;

        private bool menuBarVisible = true;

        private bool portableMode = false;

        private bool rememberWindowPosition = true;

        private bool rememberWindowSize = true;

        private bool statusBarVisible = true;

        private bool toolBarVisible = true;

        private int width = 640;

        public View CategoriesDefaultViewMode
        {
            get => categoriesDefaultViewMode;
            set { Set(ref categoriesDefaultViewMode, value); }
        }

        public bool CategoryDeletePrompt
        {
            get => categoryDeletePrompt;
            set { Set(ref categoryDeletePrompt, value); }
        }

        public string CDsDefaultDir
        {
            get => cdsDefaultDir;
            set { Set(ref cdsDefaultDir, value); }
        }

        public string ConfigEditorAdditionalParameters
        {
            get => configEditorAdditionalParameters;
            set { Set(ref configEditorAdditionalParameters, value); }
        }

        public string ConfigEditorPath
        {
            get => configEditorPath;
            set { Set(ref configEditorPath, value); }
        }

        public string DBDefaultConfFilePath
        {
            get => dbDefaultConfFilePath;
            set { Set(ref dbDefaultConfFilePath, value); }
        }

        public string DBDefaultLangFilePath
        {
            get => dbDefaultLangFilePath;
            set { Set(ref dbDefaultLangFilePath, value); }
        }

        /// <summary> Main DOSBox executable path </summary>
        public string DBPath
        {
            get => dbPath;
            set { Set(ref dbPath, value); }
        }

        public bool DefaultIconViewOverride
        {
            get => defaultIconViewOverride;
            set { Set(ref defaultIconViewOverride, value); }
        }

        public bool Fullscreen
        {
            get => fullScreen;
            set { Set(ref fullScreen, value); }
        }

        public bool GameDeletePrompt
        {
            get => gameDeletePrompt;
            set { Set(ref gameDeletePrompt, value); }
        }

        public string GamesAdditionalCommands
        {
            get => gamesAdditionalCommands;
            set { Set(ref gamesAdditionalCommands, value); }
        }

        public string GamesDefaultDir
        {
            get => gamesDefaultDir;
            set { Set(ref gamesDefaultDir, value); }
        }

        public bool GamesInFullScreen
        {
            get => gamesInFullScreen;
            set { Set(ref gamesInFullScreen, value); }
        }

        public bool GamesNoConsole
        {
            get => gamesNoConsole;
            set { Set(ref gamesNoConsole, value); }
        }

        public bool GamesQuitOnExit
        {
            get => gamesQuitOnExit;
            set { Set(ref gamesQuitOnExit, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether we use DOSBox to play games or not.
        /// </summary>
        public bool GamesUseDOSBox
        {
            get => gamesUseDOSBox;
            set { Set(ref gamesUseDOSBox, value); }
        }

        public int Height
        {
            get => height;
            set { Set(ref height, value); }
        }

        public int LargeViewModeSize
        {
            get => largeViewModeSize;
            set { Set(ref largeViewModeSize, value); }
        }

        public bool MenuBarVisible
        {
            get => menuBarVisible;
            set { Set(ref menuBarVisible, value); }
        }

        public bool PortableMode
        {
            get => portableMode;
            set { Set(ref portableMode, value); }
        }

        public bool RememberWindowPosition
        {
            get => rememberWindowPosition;
            set { Set(ref rememberWindowPosition, value); }
        }

        public bool RememberWindowSize
        {
            get => rememberWindowSize;
            set { Set(ref rememberWindowSize, value); }
        }

        public bool StatusBarVisible
        {
            get => statusBarVisible;
            set { Set(ref statusBarVisible, value); }
        }

        public bool ToolBarVisible
        {
            get => toolBarVisible;
            set { Set(ref toolBarVisible, value); }
        }

        public int Width
        {
            get => width;
            set { Set(ref width, value); }
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}