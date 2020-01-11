/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
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
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(TypeName = "Window")]
    public class Preferences : ModelWithChildren
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };

        private bool portableMode = false;

        public bool PortableMode
        {
            get => portableMode;
            set { Set(ref portableMode, value); }
        }

        private bool defaultIconViewOverride = false;

        public bool DefaultIconViewOverride
        {
            get => defaultIconViewOverride;
            set { Set(ref defaultIconViewOverride, value); }
        }

        public int X { get; set; }

        public int Y { get; set; }

        private System.Windows.Forms.View categoriesDefaultViewMode = System.Windows.Forms.View.LargeIcon;

        public System.Windows.Forms.View CategoriesDefaultViewMode
        {
            get => categoriesDefaultViewMode;
            set { Set<System.Windows.Forms.View>(ref categoriesDefaultViewMode, value); }
        }

        private bool rememberWindowPosition = true;

        public bool RememberWindowPosition
        {
            get => rememberWindowPosition;
            set { Set(ref rememberWindowPosition, value); }
        }

        private bool rememberWindowSize = true;

        public bool RememberWindowSize
        {
            get => rememberWindowSize;
            set { Set(ref rememberWindowSize, value); }
        }

        private bool gameDeletePrompt = true;

        public bool GameDeletePrompt
        {
            get => gameDeletePrompt;
            set { Set(ref gameDeletePrompt, value); }
        }

        private bool categoryDeletePrompt = true;

        public bool CategoryDeletePrompt
        {
            get => categoryDeletePrompt;
            set { Set(ref categoryDeletePrompt, value); }
        }

        private bool gamesNoConsole = true;

        public bool GamesNoConsole
        {
            get => gamesNoConsole;
            set { Set(ref gamesNoConsole, value); }
        }

        private bool gamesInFullScreen = true;

        public bool GamesInFullScreen
        {
            get => gamesInFullScreen;
            set { Set(ref gamesInFullScreen, value); }
        }

        private bool gamesQuitOnExit = true;

        public bool GamesQuitOnExit
        {
            get => gamesQuitOnExit;
            set { Set(ref gamesQuitOnExit, value); }
        }

        private string gamesAdditionalCommands;

        public string GamesAdditionalCommands
        {
            get => gamesAdditionalCommands;
            set { Set(ref gamesAdditionalCommands, value); }
        }

        private string gamesDefaultDir = string.Empty;

        public string GamesDefaultDir
        {
            get => gamesDefaultDir;
            set { Set(ref gamesDefaultDir, value); }
        }

        private string cdsDefaultDir = string.Empty;

        public string CDsDefaultDir
        {
            get => cdsDefaultDir;
            set { Set(ref cdsDefaultDir, value); }
        }

        private string configEditorPath = string.Empty;

        public string ConfigEditorPath
        {
            get => configEditorPath;
            set { Set(ref configEditorPath, value); }
        }

        private string configEditorAdditionalParameters = string.Empty;

        public string ConfigEditorAdditionalParameters
        {
            get => configEditorAdditionalParameters;
            set { Set(ref configEditorAdditionalParameters, value); }
        }

        private bool fullScreen;

        public bool Fullscreen
        {
            get => fullScreen;
            set { Set(ref fullScreen, value); }
        }

        private bool menuBarVisible = true;

        public bool MenuBarVisible
        {
            get => menuBarVisible;
            set { Set(ref menuBarVisible, value); }
        }

        private bool toolBarVisible = true;

        public bool ToolBarVisible
        {
            get => toolBarVisible;
            set { Set(ref toolBarVisible, value); }
        }

        private bool statusBarVisible = true;

        public bool StatusBarVisible
        {
            get => statusBarVisible;
            set { Set(ref statusBarVisible, value); }
        }

        private string dbPath = string.Empty;

        public string DBPath
        {
            get => dbPath;
            set { Set(ref dbPath, value); }
        }

        private string dbDefaultConfFilePath = string.Empty;

        public string DBDefaultConfFilePath
        {
            get => dbDefaultConfFilePath;
            set { Set(ref dbDefaultConfFilePath, value); }
        }

        private string dbDefaultLangFilePath = string.Empty;

        public string DBDefaultLangFilePath
        {
            get => dbDefaultLangFilePath;
            set { Set(ref dbDefaultLangFilePath, value); }
        }

        private int width = 640;

        public int Width
        {
            get => width;
            set { Set(ref width, value); }
        }

        private int height = 400;

        public int Height
        {
            get => height;
            set { Set(ref height, value); }
        }

        private int largeViewModeSize = 48;

        public int LargeViewModeSize
        {
            get => largeViewModeSize;
            set { Set(ref largeViewModeSize, value); }
        }
    }
}