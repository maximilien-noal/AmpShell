/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
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
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class Category : ModelWithChildren
    {
        public Category()
            : base()
        {
        }

        public Category(string categoryTitle, string categorySignature)
        {
            this.Title = categoryTitle;
            this.Signature = categorySignature;
        }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        private int nameColumnWidth = 150;

        public int NameColumnWidth
        {
            get => nameColumnWidth;
            set { Set(ref nameColumnWidth, value); }
        }

        private int releaseDateColumnWidth = 150;

        public int ReleaseDateColumnWidth
        {
            get => releaseDateColumnWidth;
            set { Set(ref releaseDateColumnWidth, value); }
        }

        private int executableColumnWith = 150;

        public int ExecutableColumnWidth
        {
            get => executableColumnWith;
            set { Set(ref executableColumnWith, value); }
        }

        private int cMountColumnWidth = 150;

        public int CMountColumnWidth
        {
            get => cMountColumnWidth;
            set { Set(ref cMountColumnWidth, value); }
        }

        private int setupExecutableColumnWidth = 150;

        public int SetupExecutableColumnWidth
        {
            get => setupExecutableColumnWidth;
            set { Set(ref setupExecutableColumnWidth, value); }
        }

        private int customConfigurationColumnWidth = 150;

        public int CustomConfigurationColumnWidth
        {
            get => customConfigurationColumnWidth;
            set { Set(ref customConfigurationColumnWidth, value); }
        }

        private int dMountColumnWidth = 150;

        public int DMountColumnWidth
        {
            get => dMountColumnWidth;
            set { Set(ref dMountColumnWidth, value); }
        }

        private int mountingOptionsColumnWidth = 100;

        public int MountingOptionsColumnWidth
        {
            get => mountingOptionsColumnWidth;
            set { Set(ref mountingOptionsColumnWidth, value); }
        }

        private int additionalCommandsColumnWidth = 150;

        public int AdditionnalCommandsColumnWidth
        {
            get => additionalCommandsColumnWidth;
            set { Set(ref additionalCommandsColumnWidth, value); }
        }

        private int noConsoleColumnWidth = 100;

        public int NoConsoleColumnWidth
        {
            get => noConsoleColumnWidth;
            set { Set(ref noConsoleColumnWidth, value); }
        }

        private int fullscreenColumnWidth = 100;

        public int FullscreenColumnWidth
        {
            get => fullscreenColumnWidth;
            set { Set(ref fullscreenColumnWidth, value); }
        }

        private int quitOnExitColumnWidth = 100;

        public int QuitOnExitColumnWidth
        {
            get => quitOnExitColumnWidth;
            set { Set(ref quitOnExitColumnWidth, value); }
        }

        private int notesColumnWidth = 400;

        public int NotesColumnWidth
        {
            get => notesColumnWidth;
            set { Set(ref notesColumnWidth, value); }
        }

        private View viewMode = View.LargeIcon;

        public View ViewMode
        {
            get => viewMode;
            set { Set(ref viewMode, value); }
        }
    }
}