/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Views
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using AmpShell.DAL;
    using AmpShell.Model;

    /// <summary>
    /// Form to add or modify a Game.
    /// </summary>
    public partial class GameForm : Form
    {
        public GameForm(Game editedGame, bool newGame = false)
        {
            if (editedGame == null)
            {
                return;
            }
            this.InitializeComponent();
            this.GameInstance = editedGame;

            //fill the form with the Game's data.
            this.Text = $"Editing {this.GameInstance.Name} ...";
            if (StringExt.IsNullOrWhiteSpace(this.GameInstance.Icon) == false && this.GameInstance.Icon != null)
            {
                string realLocation;
                realLocation = this.GameInstance.Icon.Replace("AppPath", Application.StartupPath);
                if (File.Exists(realLocation))
                {
                    this.GameIconPictureBox.Image = Image.FromFile(realLocation).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                    this.GameIconPictureBox.ImageLocation = realLocation;
                }
            }
            this.GameNameTextbox.Text = this.GameInstance.Name;
            this.GameReleaseDatePicker.Value = this.GameInstance.ReleaseDate;
            this.GameLocationTextbox.Text = this.GameInstance.DOSEXEPath;
            if (UserDataAccessor.UserData.GamesUseDOSBox)
            {
                this.GameDirectoryTextbox.Text = this.GameInstance.Directory;
                this.GameCustomConfigurationTextbox.Text = this.GameInstance.DBConfPath;
                this.NoConfigCheckBox.Checked = this.GameInstance.NoConfig;
                this.GameCDPathTextBox.Text = this.GameInstance.CDPath;
                this.DiscLabelTextBox.Text = this.GameInstance.CDLabel;
                this.GameAdditionalCommandsTextBox.Text = $"REM Put each command on a new line.\r\n";
                this.GameAdditionalCommandsTextBox.Text += $"REM Examples of DOSBox commands: 'core=normal'\r\n";
                this.GameAdditionalCommandsTextBox.Text += $"REM or 'IMGMOUNT D C:\\Temp\\MyCDImage.iso -t iso'\r\n";
                this.GameAdditionalCommandsTextBox.Text += "REM or anything recognized by DOSBox.\r\n\r\n";
                this.GameAdditionalCommandsTextBox.Text += this.GameInstance.PutEachAdditionnalCommandsOnANewLine();
                this.AlternateDOSBoxLocationTextbox.Text = this.GameInstance.AlternateDOSBoxExePath;
                this.NoConsoleCheckBox.Checked = this.GameInstance.NoConsole;
                this.QuitOnExitCheckBox.Checked = this.GameInstance.QuitOnExit;
                this.FullscreenCheckBox.Checked = this.GameInstance.InFullScreen;
                this.UseIOCTLRadioButton.Checked = this.GameInstance.UseIOCTL;
                this.IsAFloppyDiskRadioButton.Checked = this.GameInstance.MountAsFloppy;
                if (this.UseIOCTLRadioButton.Checked == false && this.IsAFloppyDiskRadioButton.Checked == false)
                {
                    this.NoneRadioButton.Checked = true;
                }
            }
            this.GameSetupTextBox.Text = this.GameInstance.SetupEXEPath;
            this.NotesRichTextBox.Text = this.GameInstance.Notes;

            if (newGame && UserDataAccessor.UserData.GamesUseDOSBox)
            {
                this.NoConsoleCheckBox.Checked = UserDataAccessor.UserData.GamesNoConsole;
                this.FullscreenCheckBox.Checked = UserDataAccessor.UserData.GamesInFullScreen;
                this.QuitOnExitCheckBox.Checked = UserDataAccessor.UserData.GamesQuitOnExit;
            }
            if (newGame == false)
            {
                this.OK.Text = "&Save and apply";
                this.OK.Width = 102;
                this.OK.Location = new Point(this.Cancel.Location.X - 106, this.Cancel.Location.Y);
                this.OK.Image = Properties.Resources.saveHS;
                this.Cancel.Text = "&Don't save";
            }
            if (UserDataAccessor.UserData.GamesUseDOSBox == false)
            {
                this.GameDirectoryTextbox.Enabled = false;
                this.GameDirectoryBrowseButton.Enabled = false;
                this.GameCustomConfigurationTextbox.Enabled = false;
                this.GameCustomConfigurationBrowseButton.Enabled = false;
                this.NoConfigCheckBox.Enabled = false;
                this.GameCDPathTextBox.Enabled = false;
                this.GameCDDirBrowseButton.Enabled = false;
                this.GameCDPathBrowseButton.Enabled = false;
                this.MountingOptionsGroupBox.Enabled = false;
                this.OtherOptionsGroupBox.Enabled = false;
                this.AlternateDOSBoxLocationTextbox.Enabled = false;
                this.AlternateDOSBoxLocationBrowseButton.Enabled = false;
                this.GameAdditionalCommandsTextBox.Enabled = false;
            }
            this.GameCustomConfigurationTextbox.Text = this.GameCustomConfigurationTextbox.Text.Replace("AppPath", Application.StartupPath);
            this.GameAdditionalCommandsTextBox.Text = this.GameAdditionalCommandsTextBox.Text.Replace("AppPath", Application.StartupPath);
            this.GameCDPathTextBox.Text = this.GameCDPathTextBox.Text.Replace("AppPath", Application.StartupPath);
            this.GameDirectoryTextbox.Text = this.GameDirectoryTextbox.Text.Replace("AppPath", Application.StartupPath);
            this.GameLocationTextbox.Text = this.GameLocationTextbox.Text.Replace("AppPath", Application.StartupPath);
            this.GameSetupTextBox.Text = this.GameSetupTextBox.Text.Replace("AppPath", Application.StartupPath);
            this.AlternateDOSBoxLocationTextbox.Text = this.AlternateDOSBoxLocationTextbox.Text.Replace("AppPath", Application.StartupPath);
        }

        public Game GameInstance { get; private set; }

        private void Cancel_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// EventHandler for when the user has clicked on "OK".
        /// </summary>
        private void OK_Click(object sender, EventArgs e)
        {
            //if the game has a name but no executable nor directory mounted as C: specified...
            if (StringExt.IsNullOrWhiteSpace(this.GameNameTextbox.Text) == true)
            {
                MessageBox.Show(this, "You must enter the game's name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //else : the game has a name and a directory mounted has C:
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            this.GameInstance.DOSEXEPath = this.GameLocationTextbox.Text;
            this.GameInstance.DBConfPath = this.GameCustomConfigurationTextbox.Text;
            this.GameInstance.NoConfig = this.NoConfigCheckBox.Checked;
            if (StringExt.IsNullOrWhiteSpace(this.GameAdditionalCommandsTextBox.Text) == false)
            {
                this.GameInstance.AdditionalCommands = this.GetAdditionnalCommandsInASingleLine();
            }
            this.GameInstance.NoConsole = this.NoConsoleCheckBox.Checked;
            this.GameInstance.InFullScreen = this.FullscreenCheckBox.Checked;
            this.GameInstance.QuitOnExit = this.QuitOnExitCheckBox.Checked;
            this.GameInstance.Directory = this.GameDirectoryTextbox.Text;
            this.GameInstance.Name = this.GameNameTextbox.Text;
            this.GameInstance.ReleaseDate = this.GameReleaseDatePicker.Value;
            this.GameInstance.CDPath = this.GameCDPathTextBox.Text;
            this.GameInstance.CDLabel = this.DiscLabelTextBox.Text;
            this.GameInstance.SetupEXEPath = this.GameSetupTextBox.Text;
            this.GameInstance.AlternateDOSBoxExePath = this.AlternateDOSBoxLocationTextbox.Text;
            if (StringExt.IsNullOrWhiteSpace(this.GameIconPictureBox.ImageLocation) == false)
            {
                this.GameInstance.Icon = this.GameIconPictureBox.ImageLocation;
            }
            else
            {
                this.GameInstance.Icon = string.Empty;
            }

            this.GameInstance.UseIOCTL = this.UseIOCTLRadioButton.Checked;
            this.GameInstance.MountAsFloppy = this.IsAFloppyDiskRadioButton.Checked;
            if (StringExt.IsNullOrWhiteSpace(this.GameCDPathTextBox.Text) == false)
            {
                if (File.Exists(this.GameCDPathTextBox.Text))
                {
                    this.GameInstance.CDIsAnImage = true;
                }
                else
                {
                    this.GameInstance.CDIsAnImage = false;
                }
            }
            this.GameInstance.Notes = this.NotesRichTextBox.Text;
        }

        private string GetAdditionnalCommandsInASingleLine()
        {
            var commandLine = new StringBuilder();
            string[] array = this.GameAdditionalCommandsTextBox.Text.Split('\r');
            for (int i = 0; i < array.Length; i++)
            {
                string line = array[i];
                line = line.Trim();
                if (line.ToUpperInvariant().StartsWith("REM") == false && StringExt.IsNullOrWhiteSpace(line) == false)
                {
                    commandLine.Append($"-c \"{line}\"");
                    if (i > 0)
                    {
                        commandLine.Append(' ');
                    }
                }
            }
            return commandLine.ToString();
        }

        /// <summary>
        /// EventHandler to choose the Game's executable location.
        /// </summary>
        private void GameLocationBrowseButton_Click(object sender, EventArgs e)
        {
            using (var gameExeFileDialog = new OpenFileDialog())
            {
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    gameExeFileDialog.InitialDirectory = Application.StartupPath;
                }
                else if (StringExt.IsNullOrWhiteSpace(this.GameLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.GameLocationTextbox.Text)))
                {
                    gameExeFileDialog.InitialDirectory = Path.GetDirectoryName(this.GameLocationTextbox.Text);
                }
                else
                {
                    gameExeFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                }

                gameExeFileDialog.Title = this.GameLocationLabel.Text;
                gameExeFileDialog.Filter = "DOS executable files (*.bat;*.cmd;*.com;*.exe)|*.bat;*.cmd;*.com;*.exe;*.BAT;*.CMD;*.COM;*.EXE";
                if (gameExeFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameLocationTextbox.Text = gameExeFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// EventHandler to choose the game .conf (config) file.
        /// </summary>
        private void GameCustomConfigurationBrowseButton_Click(object sender, EventArgs e)
        {
            using (var customConfigFileDialog = new OpenFileDialog())
            {
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    customConfigFileDialog.InitialDirectory = Application.StartupPath;
                }
                else if (StringExt.IsNullOrWhiteSpace(this.GameCustomConfigurationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.GameCustomConfigurationTextbox.Text)))
                {
                    customConfigFileDialog.InitialDirectory = Path.GetDirectoryName(this.GameCustomConfigurationTextbox.Text);
                }
                else
                {
                    customConfigFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                }

                customConfigFileDialog.Title = this.GameCustomConfigurationLabel.Text;
                customConfigFileDialog.Filter = "DOSBox configuration file (*.conf)|*.conf;*.CONF";
                if (customConfigFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameCustomConfigurationTextbox.Text = customConfigFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// EventHandler for when the "Do not use any config file at all" checkbox is (un)checked.
        /// </summary>
        private void NoConfigCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NoConfigCheckBox.Checked == true)
            {
                this.GameCustomConfigurationTextbox.Enabled = false;
                this.GameCustomConfigurationBrowseButton.Enabled = false;
            }
            else
            {
                this.GameCustomConfigurationTextbox.Enabled = true;
                this.GameCustomConfigurationBrowseButton.Enabled = true;
            }
        }

        /// <summary>
        /// EventHandler to choose the game's CD image file.
        /// </summary>
        private void GameCDPathBrowseButton_Click(object sender, EventArgs e)
        {
            using (var cdImageFileDialog = new OpenFileDialog())
            {
                cdImageFileDialog.Title = this.GameCDPathLabel.Text;
                cdImageFileDialog.Filter = "DOSBox compatible CD or Floppy image files (*.bin;*.cue;*.iso;*.img;*.ima)|*.bin;*.cue;*.iso;*.img;*.ima;*.BIN;*.CUE;*.ISO;*.IMG;*.IMA";
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    cdImageFileDialog.InitialDirectory = Application.StartupPath;
                }
                else if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.CDsDefaultDir))
                {
                    cdImageFileDialog.InitialDirectory = UserDataAccessor.UserData.CDsDefaultDir;
                }
                else
                {
                    cdImageFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                }

                if (cdImageFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameCDPathTextBox.Text = cdImageFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// EventHandler for when the this.GameLocationTextbox (for the game's executable location)
        /// text is changed.
        /// </summary>
        private void GameLocationTextbox_TextChanged(object sender, EventArgs e)
        {
            if (UserDataAccessor.UserData.GamesUseDOSBox == false)
            {
                return;
            }

            //if a location for the game's executable has been entered
            if (StringExt.IsNullOrWhiteSpace(this.GameLocationTextbox.Text) == false)
            {
                //then the directory mounted has C: TextBox, BrowseButton, and Labeled are disabled
                //(because DOSBox already mounts the executable's directory path as C: )
                this.GameDirectoryTextbox.Enabled = false;
                this.GameDirectoryBrowseButton.Enabled = false;
                this.GameDirectoryLabel.Enabled = false;
            }
            else
            {
                //if not, they are enabled
                this.GameDirectoryTextbox.Enabled = true;
                this.GameDirectoryBrowseButton.Enabled = true;
                this.GameDirectoryLabel.Enabled = true;
            }

            //if the entered executable does exist
            if (StringExt.IsNullOrWhiteSpace(this.GameLocationTextbox.Text) == false)
            {
                //the directory mounted has C: is displayed : it is the game's executable full directory path
                if (File.Exists(this.GameLocationTextbox.Text))
                {
                    //(even if the GameDirectory controls are not enabled : it's just to inform the user)
                    this.GameDirectoryTextbox.Text = Path.GetDirectoryName(this.GameLocationTextbox.Text);
                }
            }
        }

        /// <summary>
        /// EventHandler for when the this.GameDirectoryTextbox's this.Text has changed.
        /// </summary>
        private void GameDirectoryTextbox_TextChanged(object sender, EventArgs e)
        {
            if (UserDataAccessor.UserData.GamesUseDOSBox == false)
            {
                return;
            }

            //if the textBox is not empty
            if (StringExt.IsNullOrWhiteSpace(this.GameDirectoryTextbox.Text) == false)
            {
                //if the game location textbox is not empty
                if (StringExt.IsNullOrWhiteSpace(this.GameLocationTextbox.Text) == false)
                {
                    //and if the specified directory does not equals to the game executable's directory
                    if (Path.GetDirectoryName(this.GameLocationTextbox.Text) != this.GameDirectoryTextbox.Text)
                    {
                        //then this textbox has been entered first by the user
                        //so the controls for the game's executable location will be made empty and disabled
                        //because DOSBox cannot mount a directory as C: and have an executable specified.
                        //(it's one or the other)
                        this.GameLocationTextbox.Text = string.Empty;
                        this.GameLocationTextbox.Enabled = false;
                        this.GameLocationBrowseButton.Enabled = false;
                        this.GameLocationLabel.Enabled = false;
                    }

                    //if this textbox is empty
                    else
                    {
                        //make the controls for the game's executable location available
                        this.GameLocationTextbox.Enabled = true;
                        this.GameLocationBrowseButton.Enabled = true;
                        this.GameLocationLabel.Enabled = true;
                    }
                }
                else
                {
                    this.GameLocationTextbox.Text = string.Empty;
                    this.GameLocationTextbox.Enabled = false;
                    this.GameLocationBrowseButton.Enabled = false;
                    this.GameLocationLabel.Enabled = false;
                }
            }
            else
            {
                this.GameLocationTextbox.Enabled = true;
                this.GameLocationBrowseButton.Enabled = true;
                this.GameLocationLabel.Enabled = true;
            }
        }

        /// <summary>
        /// EventHandler to choose the directory mounted as C:.
        /// </summary>
        private void GameDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            using (var cMountFolderBrowserDialog = new FolderBrowserDialog())
            {
                cMountFolderBrowserDialog.ShowNewFolderButton = true;
                cMountFolderBrowserDialog.Description = this.GameDirectoryLabel.Text;
                if (cMountFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameDirectoryTextbox.Text = cMountFolderBrowserDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// EventHandler for when the this.GameCDPathTextBox's text is changed.
        /// </summary>
        private void GameCDPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (StringExt.IsNullOrWhiteSpace(this.GameCDPathTextBox.Text))
            {
                this.MountingOptionsGroupBox.Enabled = false;
            }
            else
            {
                this.MountingOptionsGroupBox.Enabled = true;
                if (File.Exists(this.GameCDPathTextBox.Text) == false)
                {
                    this.DiscLabelTextBox.Enabled = true;
                    this.UseIOCTLRadioButton.Enabled = true;
                    this.IsAFloppyDiskRadioButton.Enabled = false;
                }
                else
                {
                    this.DiscLabelTextBox.Enabled = false;
                    this.UseIOCTLRadioButton.Enabled = false;
                    this.IsAFloppyDiskRadioButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// EventHandler to choose the game's setup executable location.
        /// </summary>
        private void GameSetupBrowseButton_Click(object sender, EventArgs e)
        {
            using (var setupExeFileDialog = new OpenFileDialog())
            {
                setupExeFileDialog.Title = this.GameSetupLabel.Text;
                setupExeFileDialog.Filter = "DOS executable files (*.bat;*.com;*.exe)|*.bat;*.com;*.exe;*.BAT;*.COM;*.EXE";
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    setupExeFileDialog.InitialDirectory = Application.StartupPath;
                }
                else
                {
                    setupExeFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                }

                if (setupExeFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameSetupTextBox.Text = setupExeFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// EventHandler to choose the directory mounted as D: Because a CD can be a CD Image, or a
        /// drive. Or, the user just wants to mount another directory as D:.
        /// </summary>
        private void GameCDDirBrowseButton_Click(object sender, EventArgs e)
        {
            using (var cdDriveFolderBrowserDialog = new FolderBrowserDialog())
            {
                if (cdDriveFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GameCDPathTextBox.Text = cdDriveFolderBrowserDialog.SelectedPath;
                }
            }
        }

        private void QuitOnExitCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (this.QuitOnExitCheckBox.Enabled == false)
            {
                this.QuitOnExitCheckBox.Checked = false;
            }
        }

        private void GameIconPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using (var iconFileDialog = new OpenFileDialog())
                {
                    iconFileDialog.Filter = "Image files (*.bmp;*.exif;*.gif;*.ico;*.jp*;*.png;*.tif*)|*.bmp;*.BMP;*.exif;*.EXIF;*.gif;*.GIF;*.ico;*.ICO;*.jp*;*.JP*;*.png;*.PNG;*.tif*;*.TIF*";
                    if (UserDataAccessor.UserData.PortableMode == true)
                    {
                        iconFileDialog.InitialDirectory = Application.StartupPath;
                    }
                    else if (StringExt.IsNullOrWhiteSpace(this.GameIconPictureBox.ImageLocation) == false && Directory.Exists(Path.GetDirectoryName(this.GameIconPictureBox.ImageLocation)))
                    {
                        iconFileDialog.InitialDirectory = Path.GetDirectoryName(this.GameIconPictureBox.ImageLocation);
                    }
                    else
                    {
                        iconFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                    }

                    if (iconFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        this.GameIconPictureBox.Image = Image.FromFile(iconFileDialog.FileName).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                        this.GameIconPictureBox.ImageLocation = iconFileDialog.FileName;
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(this, "There is an error in the image file, or its format is not supported. Please check the file.", "Changing the game's icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (StringExt.IsNullOrWhiteSpace(this.GameInstance.Icon))
                {
                    this.GameIconPictureBox.Image = Properties.Resources.Generic_Application1;
                }
                else
                {
                    this.GameIconPictureBox.Image = Image.FromFile(this.GameInstance.Icon).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                }
            }
        }

        private void ResetIconButton_Click(object sender, EventArgs e)
        {
            this.GameIconPictureBox.Image = Properties.Resources.Generic_Application1;
            this.GameIconPictureBox.ImageLocation = string.Empty;
        }

        private void AlternateDOSBoxLocationBrowsSearchButton_Click(object sender, EventArgs e)
        {
            using (var alternateDOSBoxExeFileDialog = new OpenFileDialog())
            {
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    alternateDOSBoxExeFileDialog.InitialDirectory = Application.StartupPath;
                }
                else if (StringExt.IsNullOrWhiteSpace(this.AlternateDOSBoxLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.AlternateDOSBoxLocationTextbox.Text)))
                {
                    alternateDOSBoxExeFileDialog.InitialDirectory = Path.GetDirectoryName(this.AlternateDOSBoxLocationTextbox.Text);
                }
                else if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBPath)))
                {
                    alternateDOSBoxExeFileDialog.InitialDirectory = UserDataAccessor.UserData.DBPath;
                }
                else
                {
                    alternateDOSBoxExeFileDialog.InitialDirectory = this.SearchFolderDialogStartDirectory();
                }

                alternateDOSBoxExeFileDialog.Title = this.AlternateDOSBoxLocationLabel.Text;
                alternateDOSBoxExeFileDialog.Filter = "DOSBox executable file (*.exe)|*.exe;*.EXE";
                if (alternateDOSBoxExeFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.AlternateDOSBoxLocationTextbox.Text = alternateDOSBoxExeFileDialog.FileName;
                }
            }
        }

        private string SearchFolderDialogStartDirectory()
        {
            string initialDirectory = string.Empty;
            if (StringExt.IsNullOrWhiteSpace(this.GameInstance.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(this.GameInstance.DOSEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(this.GameInstance.DOSEXEPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.GameInstance.Directory) == false && Directory.Exists(this.GameInstance.Directory))
            {
                initialDirectory = this.GameInstance.Directory;
            }
            else if (StringExt.IsNullOrWhiteSpace(this.GameInstance.SetupEXEPath) == false && Directory.Exists(Path.GetDirectoryName(this.GameInstance.SetupEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(this.GameInstance.SetupEXEPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.GameInstance.Icon) == false && File.Exists(this.GameInstance.Icon))
            {
                initialDirectory = Path.GetDirectoryName(this.GameInstance.Icon);
            }
            else if (StringExt.IsNullOrWhiteSpace(this.GameInstance.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(this.GameInstance.DBConfPath)))
            {
                initialDirectory = Path.GetDirectoryName(this.GameInstance.DBConfPath);
            }
            else if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.GamesDefaultDir))
            {
                initialDirectory = UserDataAccessor.UserData.GamesDefaultDir;
            }

            if (StringExt.IsNullOrWhiteSpace(initialDirectory))
            {
                if (StringExt.IsNullOrWhiteSpace(this.GameLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.GameLocationTextbox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(this.GameLocationTextbox.Text);
                }
                else if (StringExt.IsNullOrWhiteSpace(this.GameSetupTextBox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.GameSetupTextBox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(this.GameSetupTextBox.Text);
                }
                else if (StringExt.IsNullOrWhiteSpace(this.GameCustomConfigurationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(this.GameCustomConfigurationTextbox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(this.GameCustomConfigurationTextbox.Text);
                }
                else if (StringExt.IsNullOrWhiteSpace(this.GameIconPictureBox.ImageLocation) == false && Directory.Exists(Path.GetDirectoryName(this.GameIconPictureBox.ImageLocation)))
                {
                    initialDirectory = Path.GetDirectoryName(this.GameIconPictureBox.ImageLocation);
                }
            }
            return initialDirectory;
        }

        private void GameForm_Shown(object sender, EventArgs e) => this.GameNameTextbox.Focus();
    }
}