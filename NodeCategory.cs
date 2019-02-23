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
using System.Xml.Serialization;

namespace AmpShell
{
    public class Category : AmpShell
    {
        /// <summary>
        /// Category's name
        /// </summary>
        private string _title;
        /// <summary>
        /// Category's unique random number ("signature").
        /// </summary>
        private string _signature;
        private System.Windows.Forms.View _viewMode;
        private int _nameColumnWidth;
        private int _executableColumnWidth;
        private int _cMountColumnWidth;
        private int _setupExecutableColumnWidth;
        private int _customConfigurationColumnWidth;
        private int _dMountColumnWidth;
        private int _mountingOptionsColumnWidth;
        private int _additionnalCommandsColumnWidth;
        private int _noConsoleColumnWidth;
        private int _fullscreenColumnWidth;
        private int _quitOnExitColumnWidth;

        public Category() : base()
        {
            Title = string.Empty;
            Random RandSignature = new Random();
            Signature = RandSignature.Next(1048576).ToString();
            ViewMode = System.Windows.Forms.View.List;
            NameColumnWidth = 150;
            ExecutableColumnWidth = 150;
            CMountColumnWidth = 150;
            SetupExecutableColumnWidth = 150;
            CustomConfigurationColumnWidth = 150;
            DMountColumnWidth = 150;
            MountingOptionsColumnWidth = 100;
            AdditionnalCommandsColumnWidth = 150;
            NoConsoleColumnWidth = 100;
            FullscreenColumnWidth = 100;
            QuitOnExitColumnWidth = 100;
        }

        public Category(string CategoryTitle, string CategorySignature)
        {
            Title = CategoryTitle;
            Signature = CategorySignature;
        }

        [XmlAttribute("Title")]
        public string Title
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                }
            }
        }

        [XmlAttribute("Signature")]
        public string Signature
        {
            get => _signature;
            set
            {
                if (value != _signature)
                {
                    _signature = value;
                }
            }
        }

        public int NameColumnWidth
        {
            get => _nameColumnWidth;
            set
            {
                if (_nameColumnWidth != value)
                {
                    _nameColumnWidth = value;
                }
            }
        }

        public int ExecutableColumnWidth
        {
            get => _executableColumnWidth;
            set
            {
                if (_executableColumnWidth != value)
                {
                    _executableColumnWidth = value;
                }
            }
        }

        public int CMountColumnWidth
        {
            get => _cMountColumnWidth;
            set
            {
                if (_cMountColumnWidth != value)
                {
                    _cMountColumnWidth = value;
                }
            }
        }

        public int SetupExecutableColumnWidth
        {
            get => _setupExecutableColumnWidth;
            set
            {
                if (_setupExecutableColumnWidth != value)
                {
                    _setupExecutableColumnWidth = value;
                }
            }
        }

        public int CustomConfigurationColumnWidth
        {
            get => _customConfigurationColumnWidth;
            set
            {
                if (_customConfigurationColumnWidth != value)
                {
                    _customConfigurationColumnWidth = value;
                }
            }
        }

        public int DMountColumnWidth
        {
            get => _dMountColumnWidth;
            set
            {
                if (_dMountColumnWidth != value)
                {
                    _dMountColumnWidth = value;
                }
            }
        }

        public int MountingOptionsColumnWidth
        {
            get => _mountingOptionsColumnWidth;
            set
            {
                if (_mountingOptionsColumnWidth != value)
                {
                    _mountingOptionsColumnWidth = value;
                }
            }
        }

        public int AdditionnalCommandsColumnWidth
        {
            get => _additionnalCommandsColumnWidth;
            set
            {
                if (_additionnalCommandsColumnWidth != value)
                {
                    _additionnalCommandsColumnWidth = value;
                }
            }
        }

        public int NoConsoleColumnWidth
        {
            get => _noConsoleColumnWidth;
            set
            {
                if (_noConsoleColumnWidth != value)
                {
                    _noConsoleColumnWidth = value;
                }
            }
        }

        public int FullscreenColumnWidth
        {
            get => _fullscreenColumnWidth;
            set
            {
                if (_fullscreenColumnWidth != value)
                {
                    _fullscreenColumnWidth = value;
                }
            }
        }

        public int QuitOnExitColumnWidth
        {
            get => _quitOnExitColumnWidth;
            set
            {
                if (_quitOnExitColumnWidth != value)
                {
                    _quitOnExitColumnWidth = value;
                }
            }
        }

        public System.Windows.Forms.View ViewMode
        {
            get => _viewMode;
            set
            {
                if (value != _viewMode)
                {
                    _viewMode = value;
                }
            }
        }
    }
}