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

namespace AmpShell.Model
{
    public class Category : RootModel
    {
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
        public string Title { get; set; }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        public int NameColumnWidth { get; set; }

        public int ExecutableColumnWidth { get; set; }

        public int CMountColumnWidth { get; set; }

        public int SetupExecutableColumnWidth { get; set; }

        public int CustomConfigurationColumnWidth { get; set; }

        public int DMountColumnWidth { get; set; }

        public int MountingOptionsColumnWidth { get; set; }

        public int AdditionnalCommandsColumnWidth { get; set; }

        public int NoConsoleColumnWidth { get; set; }

        public int FullscreenColumnWidth { get; set; }

        public int QuitOnExitColumnWidth { get; set; }

        public System.Windows.Forms.View ViewMode { get; set; }
    }
}