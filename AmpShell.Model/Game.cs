/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using System.Xml.Serialization;

namespace AmpShell.Model
{
    public class Game
    {
        public Game()
        {
            Name = string.Empty;
            SetupEXEPath = string.Empty;
            Directory = string.Empty;
            DBConfPath = string.Empty;
            DOSEXEPath = string.Empty;
            CDPath = string.Empty;
            AdditionalCommands = string.Empty;
            AlternateDOSBoxExePath = string.Empty;
            Icon = string.Empty;
            UseIOCTL = false;
            MountAsFloppy = false;
            NoConfig = false;
            NoConsole = false;
            InFullScreen = false;
            QuitOnExit = false;
            CDIsAnImage = false;
        }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Game's directory mounted as C:
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Game's CD image / CD directory (like 'D:\') location
        /// </summary>
        public string CDPath { get; set; }

        /// <summary>
        /// Game's setup executable location
        /// </summary>
        public string SetupEXEPath { get; set; }

        /// <summary>
        /// Game's custom DOSBox .conf file path
        /// </summary>
        public string DBConfPath { get; set; }

        /// <summary>
        /// Game's additionnal commands for DOSBox
        /// </summary>
        public string AdditionalCommands { get; set; }

        /// <summary>
        /// Option to use IOCTL (only available for optical drives)
        /// </summary>
        public bool UseIOCTL { get; set; }

        /// <summary>
        /// Option to use the image file as a floppy (A:)
        /// </summary>
        public bool MountAsFloppy { get; set; }

        /// <summary>
        /// Boolean if no config is used ("Don't use any config file at all" checkbox in GameForm)
        /// Legacy 0.72 or older DOSBox option
        /// </summary>
        public bool NoConfig { get; set; }

        public bool InFullScreen { get; set; }

        /// <summary>
        /// Boolean for displaying DOSBox's console
        /// </summary>
        public bool NoConsole { get; set; }

        /// <summary>
        /// Boolean for the -exit switch for DOSBox (if set to true, DOSBox closes when the game exits)
        /// </summary>
        public bool QuitOnExit { get; set; }

        /// <summary>
        /// Game's main executable location
        /// </summary>
        public string DOSEXEPath { get; set; }

        /// <summary>
        /// True if GameCDPath points to a CD image file, wrong if it points to a directory
        /// </summary>
        public bool CDIsAnImage { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// If we want to use DOSBox Daum, ECE, SVN, or other instead of the one set in the global preferences
        /// </summary>
        public string AlternateDOSBoxExePath { get; set; }
    }
}