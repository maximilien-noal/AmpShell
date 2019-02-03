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
        private string _Title; //Category's name
        private String _Signature; //Category's unique random number ("signature").
        private System.Windows.Forms.View _ViewMode;
        private int _NameColumnWidth;
        private int _ExecutableColumnWidth;
        private int _CMountColumnWidth;
        private int _SetupExecutableColumnWidth;
        private int _CustomConfigurationColumnWidth;
        private int _DMountColumnWidth;
        private int _MountingOptionsColumnWidth;
        private int _AdditionnalCommandsColumnWidth;
        private int _NoConsoleColumnWidth;
        private int _FullscreenColumnWidth;
        private int _QuitOnExitColumnWidth;
        public Category() : base()
        {
            Title = String.Empty;
            Random RandSignature = new Random();
            Signature =  RandSignature.Next(1048576).ToString();
            ViewMode = System.Windows.Forms.View.List;
            NameColumnWidth = 150;
            ExecutableColumnWidth=150;
            CMountColumnWidth = 150;
            SetupExecutableColumnWidth = 150;
            CustomConfigurationColumnWidth = 150;
            DMountColumnWidth = 150;
            MountingOptionsColumnWidth=100;
            AdditionnalCommandsColumnWidth = 150;
            NoConsoleColumnWidth=100;
            FullscreenColumnWidth=100;
            QuitOnExitColumnWidth=100;
        }
        public Category(string CategoryTitle, string CategorySignature)
        {
            Title = CategoryTitle;
            Signature = CategorySignature;
        }
        [XmlAttribute("Title")]
        public String Title
        {
            get{return _Title;}
            set
            {
                if (value != _Title)
                    _Title = value;
            }
        }
        [XmlAttribute("Signature")]
        public String Signature
        {
            get { return _Signature; }
            set
            {
                if (value != _Signature)
                    _Signature = value;
            }
        }
        public int NameColumnWidth
        {
            get { return _NameColumnWidth; }
            set
            {
                if (_NameColumnWidth != value)
                    _NameColumnWidth = value;
            }
        }
        public int ExecutableColumnWidth
        {
            get { return _ExecutableColumnWidth; }
            set
            {
                if (_ExecutableColumnWidth != value)
                    _ExecutableColumnWidth = value;
            }
        }
        public int CMountColumnWidth
        {
            get { return _CMountColumnWidth; }
            set
            {
                if (_CMountColumnWidth != value)
                    _CMountColumnWidth = value;
            }
        }
        public int SetupExecutableColumnWidth
        {
            get { return _SetupExecutableColumnWidth; }
            set
            {
                if (_SetupExecutableColumnWidth != value)
                    _SetupExecutableColumnWidth = value;
            }
        }
        public int CustomConfigurationColumnWidth
        {
            get { return _CustomConfigurationColumnWidth; }
            set
            {
                if (_CustomConfigurationColumnWidth != value)
                    _CustomConfigurationColumnWidth = value;
            }
        }
        public int DMountColumnWidth
        {
            get { return _DMountColumnWidth; }
            set
            {
                if (_DMountColumnWidth != value)
                    _DMountColumnWidth = value;
            }
        }
        public int MountingOptionsColumnWidth
        {
            get { return _MountingOptionsColumnWidth; }
            set
            {
                if (_MountingOptionsColumnWidth != value)
                    _MountingOptionsColumnWidth = value;
            }
        }
        public int AdditionnalCommandsColumnWidth
        {
            get { return _AdditionnalCommandsColumnWidth; }
            set
            {
                if (_AdditionnalCommandsColumnWidth != value)
                    _AdditionnalCommandsColumnWidth = value;
            }
        }
        public int NoConsoleColumnWidth
        {
            get { return _NoConsoleColumnWidth; }
            set
            {
                if (_NoConsoleColumnWidth != value)
                    _NoConsoleColumnWidth = value;
            }
        }
        public int FullscreenColumnWidth
        {
            get { return _FullscreenColumnWidth; }
            set
            {
                if (_FullscreenColumnWidth != value)
                    _FullscreenColumnWidth = value;
            }
        }
        public int QuitOnExitColumnWidth
        {
            get { return _QuitOnExitColumnWidth; }
            set
            {
                if (_QuitOnExitColumnWidth != value)
                    _QuitOnExitColumnWidth = value;
            }
        }
        public System.Windows.Forms.View ViewMode
        {
            get { return _ViewMode; }
            set
            {
                if (value != _ViewMode)
                    _ViewMode = value;
            }
        }
    }
}
