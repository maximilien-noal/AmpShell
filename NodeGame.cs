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

namespace AmpShell
{
    public class Game : Category
    {
        private string _Name; // Game's name
        private string _SetupEXEPath; // game's setup executable location
        private string _Directory; // Game's directory mounted as C:
        private string _DBConfPath; // game's custom .conf file path
        private string _DOSEXEPath; // game's main executable location
        private string _CDPath; // game's CD image / CD directory (like 'D:\') location
        private bool _CDImage; //True if GameCDPath points to a CD image file, wrong if it points to a directory
        private bool _UseIOCTL;
        private bool _MountAsFloppy;
        private string _AdditionalCommands; //game's additionnal commands for DOSBox
        private bool _NoConfig; //boolean if no config is used ("Don't use any config file at all" checkbox in GameForm)
        private bool _InFullScreen; //boolean for fullscreen mode
        private bool _NoConsole; //boolean for DOSBox's console
        private bool _QuitOnExit; //boolean for the -exit switch for DOSBox
        private string _Icon; //Image containing the game's icon
        public Game() : base()
        {
            Name = String.Empty;
            SetupEXEPath = String.Empty;
            Directory = String.Empty;
            DBConfPath = String.Empty;
            DOSEXEPath = String.Empty;
            CDPath = String.Empty;
            AdditionalCommands = String.Empty;
            Icon = String.Empty;
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
            get{return _Name;}
            set
            {
                if (value != _Name)
                    _Name = value;
            }
        }
        public string Directory
        {
            get { return _Directory; }
            set
            {
                if (value != _Directory)
                    _Directory = value;
            }
        }
        public string CDPath
        {
            get { return _CDPath; }
            set
            {
                if (value != _CDPath)
                    _CDPath = value;
            }
        }
        public string SetupEXEPath
        {
            get { return _SetupEXEPath; }
            set
            {
                if (value != _SetupEXEPath)
                    _SetupEXEPath = value;
            }
        }
        public string DBConfPath
        {
            get{return _DBConfPath;}
            set
            {
                if (value != _DBConfPath)
                    _DBConfPath = value;
            }
        }
        public string AdditionalCommands
        {
            get { return _AdditionalCommands; }
            set
            {
                if (value != _AdditionalCommands)
                    _AdditionalCommands = value;
            }
        }
        public bool UseIOCTL
        {
            get { return _UseIOCTL; }
            set
            {
                if (value != _UseIOCTL)
                    _UseIOCTL = value;
            }
        }
        public bool MountAsFloppy
        {
            get { return _MountAsFloppy; }
            set
            {
                if (value != _MountAsFloppy)
                    _MountAsFloppy = value;
            }
        }
        public bool NoConfig
        {
            get { return _NoConfig; }
            set
            {
                if (value != _NoConfig)
                    _NoConfig = value;
            }
        }
        public bool InFullScreen
        {
            get { return _InFullScreen; }
            set
            {
                if (value != _InFullScreen)
                    _InFullScreen = value;
            }
        }
        public bool NoConsole
        {
            get { return _NoConsole; }
            set
            {
                if (value != _NoConsole)
                    _NoConsole = value;
            }
        }
        public bool QuitOnExit
        {
            get { return _QuitOnExit; }
            set
            {
                if (value != _QuitOnExit)
                    _QuitOnExit = value;
            }
        }
        public string DOSEXEPath
        {
            get{return _DOSEXEPath;}
            set
            {
                if (value != _DOSEXEPath)
                    _DOSEXEPath = value;
            }
        }
        public bool CDIsAnImage
        {
            get { return _CDImage; }
            set
            {
                if (value != _CDImage)
                    _CDImage = value;
            }
        }
        public string Icon
        {
            get { return _Icon; }
            set
            {
                if (value != _Icon)
                    _Icon = value;
            }
        }
    }
}
