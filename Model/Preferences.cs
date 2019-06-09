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
    public class Preferences : RootModel
    {
        public static readonly List<int> LargeViewModeSizes = new List<int> { 48, 64, 80, 96, 112, 128, 144, 160, 176, 192, 208, 224, 240, 256 };

        public Preferences() : base()
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

        public bool PortableMode { get; set; }

        public bool DefaultIconViewOverride { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public System.Windows.Forms.View CategoriesDefaultViewMode { get; set; }

        public bool RememberWindowPosition { get; set; }

        public bool RememberWindowSize { get; set; }

        public bool GameDeletePrompt { get; set; }

        public bool CategoryDeletePrompt { get; set; }

        public bool GamesNoConsole { get; set; }

        public bool GamesInFullScreen { get; set; }

        public bool GamesQuitOnExit { get; set; }

        public string GamesAdditionalCommands { get; set; }

        public string GamesDefaultDir { get; set; }

        public string CDsDefaultDir { get; set; }

        public string ConfigEditorPath { get; set; }

        public string ConfigEditorAdditionalParameters { get; set; }

        public bool Fullscreen { get; set; }

        public bool MenuBarVisible { get; set; }

        public bool ToolBarVisible { get; set; }

        public bool StatusBarVisible { get; set; }

        public string DBPath { get; set; }

        public string DBDefaultConfFilePath { get; set; }

        public string DBDefaultLangFilePath { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int LargeViewModeSize { get; set; }
    }
}