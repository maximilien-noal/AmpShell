using System.Globalization;
/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.DAL;
using AmpShell.Model;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.Views
{
    public partial class GameForm : Form
    {
        public GameForm(Game editedGame, bool newGame = false)
        {
            if(editedGame == null)
            {
                return;
            }
            InitializeComponent();
            GameInstance = editedGame;

            //fill the form with the Game's data.
            Text = "Editing " + GameInstance.Name + "...";
            if (string.IsNullOrWhiteSpace(GameInstance.Icon) == false && GameInstance.Icon != null)
            {
                string realLocation;
                realLocation = GameInstance.Icon.Replace("AppPath", Application.StartupPath, true, CultureInfo.InvariantCulture);
                if (File.Exists(realLocation))
                {
                    GameIconPictureBox.Image = Image.FromFile(realLocation).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                    GameIconPictureBox.ImageLocation = realLocation;
                }
            }

            GameNameTextbox.Text = GameInstance.Name;
            GameLocationTextbox.Text = GameInstance.DOSEXEPath;
            GameDirectoryTextbox.Text = GameInstance.Directory;
            GameCustomConfigurationTextbox.Text = GameInstance.DBConfPath;
            NoConfigCheckBox.Checked = GameInstance.NoConfig;
            GameCDPathTextBox.Text = GameInstance.CDPath;
            DiscLabelTextBox.Text = GameInstance.CDLabel;
            GameAdditionalCommandsTextBox.Text = GameInstance.AdditionalCommands;
            AlternateDOSBoxLocationTextbox.Text = GameInstance.AlternateDOSBoxExePath;
            NoConsoleCheckBox.Checked = GameInstance.NoConsole;
            QuitOnExitCheckBox.Checked = GameInstance.QuitOnExit;
            FullscreenCheckBox.Checked = GameInstance.InFullScreen;
            GameSetupTextBox.Text = GameInstance.SetupEXEPath;
            UseIOCTLRadioButton.Checked = GameInstance.UseIOCTL;
            IsAFloppyDiskRadioButton.Checked = GameInstance.MountAsFloppy;
            if (UseIOCTLRadioButton.Checked == false && IsAFloppyDiskRadioButton.Checked == false)
            {
                NoneRadioButton.Checked = true;
            }

            GameLocationTextbox.Text = GameLocationTextbox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            GameDirectoryTextbox.Text = GameDirectoryTextbox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            GameCustomConfigurationTextbox.Text = GameCustomConfigurationTextbox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            GameCDPathTextBox.Text = GameCDPathTextBox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            GameAdditionalCommandsTextBox.Text = GameAdditionalCommandsTextBox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            GameSetupTextBox.Text = GameSetupTextBox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);
            AlternateDOSBoxLocationTextbox.Text = AlternateDOSBoxLocationTextbox.Text.Replace("AppPath", Application.StartupPath, false, CultureInfo.InvariantCulture);

            if (newGame)
            {
                NoConsoleCheckBox.Checked = UserDataAccessor.UserData.GamesNoConsole;
                FullscreenCheckBox.Checked = UserDataAccessor.UserData.GamesInFullScreen;
                QuitOnExitCheckBox.Checked = UserDataAccessor.UserData.GamesQuitOnExit;
                GameAdditionalCommandsTextBox.Text = UserDataAccessor.UserData.GamesAdditionalCommands;
            }
            else
            {
                OK.Text = "&Save and apply";
                OK.Width = 102;
                OK.Location = new Point(Cancel.Location.X - 106, Cancel.Location.Y);
                OK.Image = Properties.Resources.saveHS;
                Cancel.Text = "&Don't save";
            }

        }

        public Game GameInstance { get; private set; }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// EventHandler for when the user has clicked on "OK"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            //if the game has a name but no executable nor directory mounted as C: specified...
            if (string.IsNullOrWhiteSpace(GameNameTextbox.Text) == false)
            {
                if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) && string.IsNullOrWhiteSpace(GameDirectoryTextbox.Text))
                {
                    MessageBox.Show(this, "You must enter the game's executable location or the directory that will be mounted as C:", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);//... show an error.
                }
                //else : the game has a name and a directory mounted has C:
                else
                {
                    //close the form
                    GameInstance.DOSEXEPath = GameLocationTextbox.Text;
                    GameInstance.DBConfPath = GameCustomConfigurationTextbox.Text;
                    GameInstance.NoConfig = NoConfigCheckBox.Checked;
                    GameInstance.AdditionalCommands = GameAdditionalCommandsTextBox.Text;
                    GameInstance.NoConsole = NoConsoleCheckBox.Checked;
                    GameInstance.InFullScreen = FullscreenCheckBox.Checked;
                    GameInstance.QuitOnExit = QuitOnExitCheckBox.Checked;
                    GameInstance.Directory = GameDirectoryTextbox.Text;
                    GameInstance.Name = GameNameTextbox.Text;
                    GameInstance.CDPath = GameCDPathTextBox.Text;
                    GameInstance.CDLabel = DiscLabelTextBox.Text;
                    GameInstance.SetupEXEPath = GameSetupTextBox.Text;
                    GameInstance.AlternateDOSBoxExePath = AlternateDOSBoxLocationTextbox.Text;
                    if (string.IsNullOrWhiteSpace(GameIconPictureBox.ImageLocation) == false)
                    {
                        GameInstance.Icon = GameIconPictureBox.ImageLocation;
                    }
                    else
                    {
                        GameInstance.Icon = string.Empty;
                    }

                    GameInstance.UseIOCTL = UseIOCTLRadioButton.Checked;
                    GameInstance.MountAsFloppy = IsAFloppyDiskRadioButton.Checked;
                    if (string.IsNullOrWhiteSpace(GameCDPathTextBox.Text) == false)
                    {
                        if (File.Exists(GameCDPathTextBox.Text))
                        {
                            GameInstance.CDIsAnImage = true;
                        }
                        else
                        {
                            GameInstance.CDIsAnImage = false;
                        }
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            //if the game has no name
            else
            {
                MessageBox.Show(this, "You must enter the game's name.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// EventHandler to choose the Game's executable location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLocationBrowseButton_Click(object sender, EventArgs e)
        {
            using var gameExeFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                gameExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameLocationTextbox.Text)))
            {
                gameExeFileDialog.InitialDirectory = Path.GetDirectoryName(GameLocationTextbox.Text);
            }
            else
            {
                gameExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            gameExeFileDialog.Title = GameLocationLabel.Text;
            gameExeFileDialog.Filter = "DOS executable files (*.bat;*.cmd;*.com;*.exe)|*.bat;*.cmd;*.com;*.exe;*.BAT;*.CMD;*.COM;*.EXE";
            if (gameExeFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameLocationTextbox.Text = gameExeFileDialog.FileName;
            }
        }

        /// <summary>
        /// EventHandler to choose the game .conf (config) file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCustomConfigurationBrowseButton_Click(object sender, EventArgs e)
        {
            using var customConfigFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                customConfigFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(GameCustomConfigurationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameCustomConfigurationTextbox.Text)))
            {
                customConfigFileDialog.InitialDirectory = Path.GetDirectoryName(GameCustomConfigurationTextbox.Text);
            }
            else
            {
                customConfigFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            customConfigFileDialog.Title = GameCustomConfigurationLabel.Text;
            customConfigFileDialog.Filter = "DOSBox configuration file (*.conf)|*.conf;*.CONF";
            if (customConfigFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameCustomConfigurationTextbox.Text = customConfigFileDialog.FileName;
            }
        }

        /// <summary>
        /// EventHandler for when the "Do not use any config file at all" checkbox is (un)checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoConfigCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoConfigCheckBox.Checked == true)
            {
                GameCustomConfigurationTextbox.Enabled = false;
                GameCustomConfigurationBrowseButton.Enabled = false;
            }
            else
            {
                GameCustomConfigurationTextbox.Enabled = true;
                GameCustomConfigurationBrowseButton.Enabled = true;
            }
        }

        /// <summary>
        /// EventHandler to choose the game's CD image file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDPathBrowseButton_Click(object sender, EventArgs e)
        {
            using var cdImageFileDialog = new OpenFileDialog
            {
                Title = GameCDPathLabel.Text,
                Filter = "DOSBox compatible CD images (*.bin;*.cue;*.iso;*.img)|*.bin;*.cue;*.iso;*.img;*.BIN;*.CUE;*.ISO;*.IMG"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                cdImageFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.CDsDefaultDir))
            {
                cdImageFileDialog.InitialDirectory = UserDataAccessor.UserData.CDsDefaultDir;
            }
            else
            {
                cdImageFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            if (cdImageFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameCDPathTextBox.Text = cdImageFileDialog.FileName;
            }
        }

        /// <summary>
        /// EventHandler for when the GameLocationTextbox (for the game's executable location) text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameLocationTextbox_TextChanged(object sender, EventArgs e)
        {
            //if a location for the game's executable has been entered
            if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false)
            {
                //then the directory mounted has C: TextBox, BrowseButton, and Labeled are disabled
                //(because DOSBox already mounts the executable's directory path as C: )
                GameDirectoryTextbox.Enabled = false;
                GameDirectoryBrowseButton.Enabled = false;
                GameDirectoryLabel.Enabled = false;
            }
            else
            {
                //if not, they are enabled
                GameDirectoryTextbox.Enabled = true;
                GameDirectoryBrowseButton.Enabled = true;
                GameDirectoryLabel.Enabled = true;
            }
            //if the entered executable does exist
            if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false)
            {
                //the directory mounted has C: is displayed : it is the game's executable full directory path
                if (File.Exists(GameLocationTextbox.Text))
                {
                    //(even if the GameDirectory controls are not enabled : it's just to inform the user)
                    GameDirectoryTextbox.Text = Path.GetDirectoryName(GameLocationTextbox.Text);
                }
            }
        }

        /// <summary>
        /// EventHandler for when the GameDirectoryTextbox's Text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDirectoryTextbox_TextChanged(object sender, EventArgs e)
        {
            //if the textBox is not empty
            if (string.IsNullOrWhiteSpace(GameDirectoryTextbox.Text) == false)
            {
                //if the game location textbox is not empty
                if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false)
                {
                    //and if the specified directory does not equals to the game executable's directory
                    if (Path.GetDirectoryName(GameLocationTextbox.Text) != GameDirectoryTextbox.Text)
                    {
                        //then this textbox has been entered first by the user
                        //so the controls for the game's executable location will be made empty and disabled
                        //because DOSBox cannot mount a directory as C: and have an executable specified.
                        //(it's one or the other)
                        GameLocationTextbox.Text = string.Empty;
                        GameLocationTextbox.Enabled = false;
                        GameLocationBrowseButton.Enabled = false;
                        GameLocationLabel.Enabled = false;
                    }
                    //if this textbox is empty
                    else
                    {
                        //make the controls for the game's executable location available
                        GameLocationTextbox.Enabled = true;
                        GameLocationBrowseButton.Enabled = true;
                        GameLocationLabel.Enabled = true;
                    }
                }
                else
                {
                    GameLocationTextbox.Text = string.Empty;
                    GameLocationTextbox.Enabled = false;
                    GameLocationBrowseButton.Enabled = false;
                    GameLocationLabel.Enabled = false;
                }
            }
            else
            {
                GameLocationTextbox.Enabled = true;
                GameLocationBrowseButton.Enabled = true;
                GameLocationLabel.Enabled = true;
            }
        }

        /// <summary>
        /// EventHandler to choose the directory mounted has C:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            using var cMountFolderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = GameDirectoryLabel.Text
            };
            if (cMountFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameDirectoryTextbox.Text = cMountFolderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// EventHandler for when the GameCDPathTextBox's text is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GameCDPathTextBox.Text))
            {
                MountingOptionsGroupBox.Enabled = false;
            }
            else
            {
                MountingOptionsGroupBox.Enabled = true;
                if (File.Exists(GameCDPathTextBox.Text) == false)
                {
                    DiscLabelTextBox.Enabled = true;
                    UseIOCTLRadioButton.Enabled = true;
                    IsAFloppyDiskRadioButton.Enabled = false;
                }
                else
                {
                    DiscLabelTextBox.Enabled = false;
                    UseIOCTLRadioButton.Enabled = false;
                    IsAFloppyDiskRadioButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// EventHandler to choose the game's setup executable location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSetupBrowseButton_Click(object sender, EventArgs e)
        {
            using var setupExeFileDialog = new OpenFileDialog
            {
                Title = GameSetupLabel.Text,
                Filter = "DOS executable files (*.bat;*.com;*.exe)|*.bat;*.com;*.exe;*.BAT;*.COM;*.EXE"
            };
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                setupExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else
            {
                setupExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            if (setupExeFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameSetupTextBox.Text = setupExeFileDialog.FileName;
            }
        }

        /// <summary>
        /// EventHandler to choose the directory mounted as D:
        /// Because a CD can be a CD Image, or a drive.
        /// Or, the user just wants to mount another directory as D:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameCDDirBrowseButton_Click(object sender, EventArgs e)
        {
            using var cdDriveFolderBrowserDialog = new FolderBrowserDialog();
            if (cdDriveFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                GameCDPathTextBox.Text = cdDriveFolderBrowserDialog.SelectedPath;
            }
        }

        private void QuitOnExitCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (QuitOnExitCheckBox.Enabled == false)
            {
                QuitOnExitCheckBox.Checked = false;
            }
        }

        private void GameIconPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                using var iconFileDialog = new OpenFileDialog
                {
                    Filter = "Image files (*.bmp;*.exif;*.gif;*.ico;*.jp*;*.png;*.tif*)|*.bmp;*.BMP;*.exif;*.EXIF;*.gif;*.GIF;*.ico;*.ICO;*.jp*;*.JP*;*.png;*.PNG;*.tif*;*.TIF*"
                };
                if (UserDataAccessor.UserData.PortableMode == true)
                {
                    iconFileDialog.InitialDirectory = Application.StartupPath;
                }
                else if (string.IsNullOrWhiteSpace(GameIconPictureBox.ImageLocation) == false && Directory.Exists(Path.GetDirectoryName(GameIconPictureBox.ImageLocation)))
                {
                    iconFileDialog.InitialDirectory = Path.GetDirectoryName(GameIconPictureBox.ImageLocation);
                }
                else
                {
                    iconFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
                }

                if (iconFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    GameIconPictureBox.Image = Image.FromFile(iconFileDialog.FileName).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                    GameIconPictureBox.ImageLocation = iconFileDialog.FileName;
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(this, "There was an error in the image file, or it's format is not supported. Please check the file.", "Changing the game's icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (string.IsNullOrWhiteSpace(GameInstance.Icon))
                {
                    GameIconPictureBox.Image = Properties.Resources.Generic_Application1;
                }
                else
                {
                    GameIconPictureBox.Image = Image.FromFile(GameInstance.Icon).GetThumbnailImage(64, 64, null, IntPtr.Zero);
                }
            }
        }

        private void ResetIconButton_Click(object sender, EventArgs e)
        {
            GameIconPictureBox.Image = Properties.Resources.Generic_Application1;
            GameIconPictureBox.ImageLocation = string.Empty;
        }

        private void AlternateDOSBoxLocationBrowsSearchButton_Click(object sender, EventArgs e)
        {
            using var alternateDOSBoxExeFileDialog = new OpenFileDialog();
            if (UserDataAccessor.UserData.PortableMode == true)
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = Application.StartupPath;
            }
            else if (string.IsNullOrWhiteSpace(AlternateDOSBoxLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(AlternateDOSBoxLocationTextbox.Text)))
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = Path.GetDirectoryName(AlternateDOSBoxLocationTextbox.Text);
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBPath)))
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = UserDataAccessor.UserData.DBPath;
            }
            else
            {
                alternateDOSBoxExeFileDialog.InitialDirectory = SearchFolderDialogStartDirectory();
            }

            alternateDOSBoxExeFileDialog.Title = AlternateDOSBoxLocationLabel.Text;
            alternateDOSBoxExeFileDialog.Filter = "DOSBox executable file (*.exe)|*.exe;*.EXE";
            if (alternateDOSBoxExeFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                AlternateDOSBoxLocationTextbox.Text = alternateDOSBoxExeFileDialog.FileName;
            }
        }

        private string SearchFolderDialogStartDirectory()
        {
            string initialDirectory = string.Empty;
            if (string.IsNullOrWhiteSpace(GameInstance.DOSEXEPath) == false && Directory.Exists(Path.GetDirectoryName(GameInstance.DOSEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(GameInstance.DOSEXEPath);
            }
            else if (string.IsNullOrWhiteSpace(GameInstance.Directory) == false && Directory.Exists(GameInstance.Directory))
            {
                initialDirectory = GameInstance.Directory;
            }
            else if (string.IsNullOrWhiteSpace(GameInstance.SetupEXEPath) == false && Directory.Exists(Path.GetDirectoryName(GameInstance.SetupEXEPath)))
            {
                initialDirectory = Path.GetDirectoryName(GameInstance.SetupEXEPath);
            }
            else if (string.IsNullOrWhiteSpace(GameInstance.Icon) == false && File.Exists(GameInstance.Icon))
            {
                initialDirectory = Path.GetDirectoryName(GameInstance.Icon);
            }
            else if (string.IsNullOrWhiteSpace(GameInstance.DBConfPath) == false && Directory.Exists(Path.GetDirectoryName(GameInstance.DBConfPath)))
            {
                initialDirectory = Path.GetDirectoryName(GameInstance.DBConfPath);
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesDefaultDir) == false && Directory.Exists(UserDataAccessor.UserData.GamesDefaultDir))
            {
                initialDirectory = UserDataAccessor.UserData.GamesDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(initialDirectory))
            {
                if (string.IsNullOrWhiteSpace(GameLocationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameLocationTextbox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(GameLocationTextbox.Text);
                }
                else if (string.IsNullOrWhiteSpace(GameSetupTextBox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameSetupTextBox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(GameSetupTextBox.Text);
                }
                else if (string.IsNullOrWhiteSpace(GameCustomConfigurationTextbox.Text) == false && Directory.Exists(Path.GetDirectoryName(GameCustomConfigurationTextbox.Text)))
                {
                    initialDirectory = Path.GetDirectoryName(GameCustomConfigurationTextbox.Text);
                }
                else if (string.IsNullOrWhiteSpace(GameIconPictureBox.ImageLocation) == false && Directory.Exists(Path.GetDirectoryName(GameIconPictureBox.ImageLocation)))
                {
                    initialDirectory = Path.GetDirectoryName(GameIconPictureBox.ImageLocation);
                }
            }
            return initialDirectory;
        }
    }
}