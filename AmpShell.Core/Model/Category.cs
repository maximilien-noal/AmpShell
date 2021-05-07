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
    using System.Xml.Serialization;

    public class Category : ModelWithChildren
    {
        private int additionalCommandsColumnWidth = 150;

        private int cMountColumnWidth = 150;

        private int customConfigurationColumnWidth = 150;

        private int dMountColumnWidth = 150;

        private int executableColumnWith = 150;

        private int fullscreenColumnWidth = 100;

        private int mountingOptionsColumnWidth = 100;

        private int nameColumnWidth = 150;

        private int noConsoleColumnWidth = 100;

        private int notesColumnWidth = 400;

        private int quitOnExitColumnWidth = 100;

        private int releaseDateColumnWidth = 150;

        private int setupExecutableColumnWidth = 150;

        private View viewMode = View.LargeIcon;

        public Category()
                                                                                                                            : base()
        {
        }

        public Category(string categoryTitle, string categorySignature)
        {
            this.Title = categoryTitle;
            this.Signature = categorySignature;
        }

        public int AdditionnalCommandsColumnWidth
        {
            get => additionalCommandsColumnWidth;
            set { Set(ref additionalCommandsColumnWidth, value); }
        }

        public int CMountColumnWidth
        {
            get => cMountColumnWidth;
            set { Set(ref cMountColumnWidth, value); }
        }

        public int CustomConfigurationColumnWidth
        {
            get => customConfigurationColumnWidth;
            set { Set(ref customConfigurationColumnWidth, value); }
        }

        public int DMountColumnWidth
        {
            get => dMountColumnWidth;
            set { Set(ref dMountColumnWidth, value); }
        }

        public int ExecutableColumnWidth
        {
            get => executableColumnWith;
            set { Set(ref executableColumnWith, value); }
        }

        public int FullscreenColumnWidth
        {
            get => fullscreenColumnWidth;
            set { Set(ref fullscreenColumnWidth, value); }
        }

        public int MountingOptionsColumnWidth
        {
            get => mountingOptionsColumnWidth;
            set { Set(ref mountingOptionsColumnWidth, value); }
        }

        public int NameColumnWidth
        {
            get => nameColumnWidth;
            set { Set(ref nameColumnWidth, value); }
        }

        public int NoConsoleColumnWidth
        {
            get => noConsoleColumnWidth;
            set { Set(ref noConsoleColumnWidth, value); }
        }

        public int NotesColumnWidth
        {
            get => notesColumnWidth;
            set { Set(ref notesColumnWidth, value); }
        }

        public int QuitOnExitColumnWidth
        {
            get => quitOnExitColumnWidth;
            set { Set(ref quitOnExitColumnWidth, value); }
        }

        public int ReleaseDateColumnWidth
        {
            get => releaseDateColumnWidth;
            set { Set(ref releaseDateColumnWidth, value); }
        }

        public int SetupExecutableColumnWidth
        {
            get => setupExecutableColumnWidth;
            set { Set(ref setupExecutableColumnWidth, value); }
        }

        [XmlAttribute("Signature")]
        public string Signature { get; set; }

        [XmlAttribute("Title")]
        public string Title { get; set; }

        public View ViewMode
        {
            get => viewMode;
            set { Set(ref viewMode, value); }
        }
    }
}