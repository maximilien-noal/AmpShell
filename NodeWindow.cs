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

namespace AmpShell
{
    public class Window : AmpShell
    {
        public static List<int> LargeViewModeSizes= new List<int> {48,64,80,96,112,128,144,160,176,192,208,224,240,256};
        /// <summary>
        /// Integer for AmpShell's window Width
        /// </summary>
        private int _Width;
        /// <summary>
        /// Integer for AmpShell's window Height
        /// </summary>
        private int _Height;
        private int _X;
        private int _Y;
        private bool _PortableMode;
        private bool _DefaultIconViewOverride;
        /// <summary>
        /// String for DOSBox.exe location
        /// </summary>
        private string _DBPath;
        /// <summary>
        /// string for the default dosbox.conf location
        /// </summary>
        private string _DBDefaultConfFilePath;
        /// <summary>
        /// string for the default language (.lng) file location
        /// </summary>
        private string _DBDefaultLangFilePath;
        private string _CDsDefaultDir;
        private string _GamesDefaultDir;
        private bool _GamesNoConsole;
        private bool _GamesInFullScreen;
        private bool _GamesQuitOnExit;
        private string _GamesAdditionalCommands;
        private bool _OnlyNames;
        private bool _Fullscreen;
        private bool _menuBarVisible;
        private bool _ToolBarVisible;
        private bool _StatusBarVisible;
        private string _ConfigEditorPath;
        private string _ConfigEditorAdditionalParameters;
        private bool _CategoryDeletePrompt;
        private bool _GameDeletePrompt;
        private bool _RememberWindowSize;
        private bool _RememberWindowPosition;
        private System.Windows.Forms.View _CategoriesDefaultViewMode;
        private int _LargeViewModeSize;

        public Window() : base()
        {
            Width = 0;
            Height = 0;
            X = 0;
            Y = 0;
            PortableMode = false;
            DefaultIconViewOverride = false;
            GameDeletePrompt = true;
            CategoryDeletePrompt = true;
            ConfigEditorPath = String.Empty;
            ConfigEditorAdditionalParameters = String.Empty;
            MenuBarVisible = true;
            ToolBarVisible = true;
            StatusBarVisible = true;
            DBPath = String.Empty;
            GamesNoConsole=false;
            GamesInFullScreen=false;
            GamesQuitOnExit = false;
            OnlyNames = false;
            Fullscreen = false;
            GamesAdditionalCommands = String.Empty;
            GamesDefaultDir = String.Empty;
            CDsDefaultDir = String.Empty;
            DBDefaultConfFilePath = String.Empty;
            DBDefaultLangFilePath = String.Empty;
            RememberWindowPosition = true;
            RememberWindowSize = true;
            LargeViewModeSize = 48;
            CategoriesDefaultViewMode = System.Windows.Forms.View.LargeIcon;
        }

        public bool PortableMode
        {
            get { return _PortableMode; }
            set
            {
                if (value != _PortableMode)
                    _PortableMode = value;
            }
        }

        public bool DefaultIconViewOverride
        {
            get { return _DefaultIconViewOverride; }
            set
            {
                if (value != _DefaultIconViewOverride)
                    _DefaultIconViewOverride = value;
            }
        }

        public int X
        {
            get { return _X; }
            set
            {
                if (value != _X)
                    _X = value;
            }
        }

        public int Y
        {
            get { return _Y; }
            set
            {
                if (value != _Y)
                    _Y = value;
            }
        }

        public System.Windows.Forms.View CategoriesDefaultViewMode
        {
            get { return _CategoriesDefaultViewMode; }
            set
            {
                if (value != _CategoriesDefaultViewMode)
                    _CategoriesDefaultViewMode = value;
            }
        }

        public bool RememberWindowPosition
        {
            get { return _RememberWindowPosition; }
            set
            {
                if (value != _RememberWindowPosition)
                    _RememberWindowPosition = value;
            }
        }

        public bool RememberWindowSize
        {
            get { return _RememberWindowSize; }
            set
            {
                if (value != _RememberWindowSize)
                    _RememberWindowSize = value;
            }
        }

        public bool GameDeletePrompt
        {
            get { return _GameDeletePrompt; }
            set
            {
                if (value != _GameDeletePrompt)
                    _GameDeletePrompt = value;
            }
        }

        public bool CategoryDeletePrompt
        {
            get { return _CategoryDeletePrompt; }
            set
            {
                if (value != _CategoryDeletePrompt)
                    _CategoryDeletePrompt = value;
            }
        }

        public bool OnlyNames
        {
            get { return _OnlyNames; }
            set
            {
                if (value != _OnlyNames)
                    _OnlyNames = value;
            }
        }

        public bool GamesNoConsole
        {
            get { return _GamesNoConsole; }
            set
            {
                if (value != _GamesNoConsole)
                    _GamesNoConsole = value;
            }
        }

        public bool GamesInFullScreen
        {
            get { return _GamesInFullScreen; }
            set
            {
                if (value != _GamesInFullScreen)
                    _GamesInFullScreen = value;
            }
        }

        public bool GamesQuitOnExit
        {
            get { return _GamesQuitOnExit; }
            set
            {
                if (value != _GamesQuitOnExit)
                    _GamesQuitOnExit = value;
            }
        }

        public String GamesAdditionalCommands
        {
            get { return _GamesAdditionalCommands; }
            set
            {
                if (value != _GamesAdditionalCommands)
                    _GamesAdditionalCommands = value;
            }
        }

        public String GamesDefaultDir
        {
            get { return _GamesDefaultDir; }
            set
            {
                if (value != _GamesDefaultDir)
                    _GamesDefaultDir = value;
            }
        }

        public String CDsDefaultDir
        {
            get { return _CDsDefaultDir; }
            set
            {
                if (value != _CDsDefaultDir)
                    _CDsDefaultDir = value;
            }
        }

        public String ConfigEditorPath
        {
            get { return _ConfigEditorPath; }
            set
            {
                if (value != _ConfigEditorPath)
                    _ConfigEditorPath = value;
            }
        }

        public String ConfigEditorAdditionalParameters
        {
            get { return _ConfigEditorAdditionalParameters; }
            set
            {
                if (value != _ConfigEditorAdditionalParameters)
                    _ConfigEditorAdditionalParameters = value;
            }
        }

        public bool Fullscreen
        {
            get { return _Fullscreen; }
            set
            {
                if (value != _Fullscreen)
                    _Fullscreen = value;
            }
        }

        public bool MenuBarVisible
        {
            get { return _menuBarVisible; }
            set
            {
                if (_menuBarVisible != value)
                    _menuBarVisible = value;
            }
        }

        public bool ToolBarVisible
        {
            get { return _ToolBarVisible; }
            set
            {
                if (_ToolBarVisible != value)
                    _ToolBarVisible = value;
            }
        }

        public bool StatusBarVisible
        {
            get { return _StatusBarVisible; }
            set
            {
                if (_StatusBarVisible != value)
                    _StatusBarVisible = value;
            }
        }

        public String DBPath
        {
            get { return _DBPath; }
            set
            {
                if (value != _DBPath)
                    _DBPath = value;
            }
        }

        public String DBDefaultConfFilePath
        {
            get { return _DBDefaultConfFilePath; }
            set
            {
                if (value != _DBDefaultConfFilePath)
                    _DBDefaultConfFilePath = value;
            }
        }

        public String DBDefaultLangFilePath
        {
            get { return _DBDefaultLangFilePath; }
            set
            {
                if (value != _DBDefaultLangFilePath)
                    _DBDefaultLangFilePath = value;
            }
        }

        public int Width
        {
            get { return _Width; }
            set
            {
                if (value != _Width)
                    _Width = value;
            }
        }

        public int Height
        {
            get { return _Height; }
            set
            {
                if (value != _Height)
                    _Height = value;
            }
        }

        public int LargeViewModeSize
        {
            get { return _LargeViewModeSize; }
            set
            {
                if (LargeViewModeSizes.Contains(value))
                    _LargeViewModeSize = value;
                else
                    throw new ArgumentOutOfRangeException("value","Possible values : "+LargeViewModeSizes.ToArray().ToString());
            }
        }
    }
}