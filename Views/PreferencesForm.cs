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
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using AmpShell.AutoConfig;
    using AmpShell.DAL;
    using AmpShell.Model;

    /// <summary>
    /// Form to modifiy application-level preferences.
    /// </summary>
    public partial class PreferencesForm : Form
    {
        private const string NoTextEditorFound = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";

        public PreferencesForm()
        {
            this.InitializeComponent();
        }

        private void BrowseForEditorButton_Click(object sender, EventArgs e)
        {
            using (var textEditorFileDialog = new OpenFileDialog())
            {
                if (StringExt.IsNullOrWhiteSpace(this.EditorBinaryPathTextBox.Text) == false)
                {
                    if (Directory.Exists(Path.GetDirectoryName(this.EditorBinaryPathTextBox.Text).ToString(CultureInfo.InvariantCulture)))
                    {
                        textEditorFileDialog.InitialDirectory = Path.GetDirectoryName(this.EditorBinaryPathTextBox.Text).ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        textEditorFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    }
                }
                if (textEditorFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.EditorBinaryPathTextBox.Text = textEditorFileDialog.FileName;
                }
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (StringExt.IsNullOrWhiteSpace(this.GamesDirTextBox.Text) == false)
            {
                if (Directory.Exists(this.GamesDirTextBox.Text))
                {
                    UserDataAccessor.UserData.GamesDefaultDir = this.GamesDirTextBox.Text;
                }
            }
            if (StringExt.IsNullOrWhiteSpace(this.CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(this.CDImageDirTextBox.Text))
                {
                    UserDataAccessor.UserData.CDsDefaultDir = this.CDImageDirTextBox.Text;
                }
            }
            UserDataAccessor.UserData.GamesAdditionalCommands = this.GameAdditionalCommandsTextBox.Text;
            UserDataAccessor.UserData.GamesUseDOSBox = this.UseDOSBoxToLaunchGamesCheckBox.Checked;
            UserDataAccessor.UserData.GamesNoConsole = this.NoConsoleCheckBox.Checked;
            UserDataAccessor.UserData.GamesInFullScreen = this.FullscreenCheckBox.Checked;
            UserDataAccessor.UserData.GamesQuitOnExit = this.QuitOnExitCheckBox.Checked;
            if (File.Exists(this.EditorBinaryPathTextBox.Text))
            {
                UserDataAccessor.UserData.ConfigEditorPath = this.EditorBinaryPathTextBox.Text;
            }

            UserDataAccessor.UserData.ConfigEditorAdditionalParameters = this.AdditionalParametersTextBox.Text;
            UserDataAccessor.UserData.CategoryDeletePrompt = this.CategoyDeletePromptCheckBox.Checked;
            UserDataAccessor.UserData.GameDeletePrompt = this.GameDeletePromptCheckBox.Checked;
            UserDataAccessor.UserData.RememberWindowSize = this.WindowSizeCheckBox.Checked;
            UserDataAccessor.UserData.RememberWindowPosition = this.WindowPositionCheckBox.Checked;
            UserDataAccessor.UserData.MenuBarVisible = this.ShowMenuBarCheckBox.Checked;
            UserDataAccessor.UserData.ToolBarVisible = this.ShowToolBarCheckBox.Checked;
            UserDataAccessor.UserData.StatusBarVisible = this.ShowDetailsBarCheckBox.Checked;
            UserDataAccessor.UserData.DefaultIconViewOverride = this.AllOfThemCheckBox.Checked;
            UserDataAccessor.UserData.DBPath = this.DOSBoxPathTextBox.Text;
            UserDataAccessor.UserData.DBDefaultConfFilePath = this.DOSBoxConfFileTextBox.Text;
            UserDataAccessor.UserData.DBDefaultLangFilePath = this.DOSBoxLangFileTextBox.Text;
            UserDataAccessor.UserData.ConfigEditorPath = this.EditorBinaryPathTextBox.Text;
            UserDataAccessor.UserData.ConfigEditorAdditionalParameters = this.AdditionalParametersTextBox.Text;
            if (this.LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                UserDataAccessor.UserData.LargeViewModeSize = Preferences.LargeViewModeSizes[this.LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (this.LargeIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (this.SmallIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (this.ListsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.List;
            }

            if (this.TilesIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Tile;
            }

            if (this.DetailsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Details;
            }

            UserDataAccessor.UserData.PortableMode = this.PortableModeCheckBox.Checked;

            this.SyncCategoriesOrderWithTabOrder();

            if (UserDataAccessor.UserData.PortableMode)
            {
                UserDataAccessor.SaveUserSettings();
            }

            this.Close();
        }

        private void SyncCategoriesOrderWithTabOrder()
        {
            if (this.CategoriesListView.Items.Count < 2)
            {
                return;
            }

            List<ListViewItem> tabs = this.CategoriesListView.Items.Cast<ListViewItem>().ToList();

            foreach (Category category in UserDataAccessor.UserData.ListChildren)
            {
                UserDataAccessor.UserData.MoveChildToPosition(category, tabs.IndexOf(tabs.FirstOrDefault(x => Convert.ToString(x.Tag, CultureInfo.InvariantCulture) == category.Signature)));
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            this.CheckForPortableModeAvailabilityAndUpdateUI();
            this.UseDOSBoxToLaunchGamesCheckBox.Checked = UserDataAccessor.UserData.GamesUseDOSBox;
            this.LargeViewModeSizeComboBox.Text = this.LargeViewModeSizeComboBox.Items[Preferences.LargeViewModeSizes.IndexOf(UserDataAccessor.UserData.LargeViewModeSize)].ToString();
            this.CategoyDeletePromptCheckBox.Checked = UserDataAccessor.UserData.CategoryDeletePrompt;
            this.GameDeletePromptCheckBox.Checked = UserDataAccessor.UserData.GameDeletePrompt;
            this.WindowPositionCheckBox.Checked = UserDataAccessor.UserData.RememberWindowPosition;
            this.WindowSizeCheckBox.Checked = UserDataAccessor.UserData.RememberWindowSize;
            this.ShowMenuBarCheckBox.Checked = UserDataAccessor.UserData.MenuBarVisible;
            this.ShowToolBarCheckBox.Checked = UserDataAccessor.UserData.ToolBarVisible;
            this.ShowDetailsBarCheckBox.Checked = UserDataAccessor.UserData.StatusBarVisible;
            this.QuitOnExitCheckBox.Checked = UserDataAccessor.UserData.GamesQuitOnExit;
            this.NoConsoleCheckBox.Checked = UserDataAccessor.UserData.GamesNoConsole;
            this.FullscreenCheckBox.Checked = UserDataAccessor.UserData.GamesInFullScreen;
            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesAdditionalCommands) == false)
            {
                this.GameAdditionalCommandsTextBox.Text = UserDataAccessor.UserData.GamesAdditionalCommands;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Details)
            {
                this.DetailsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.LargeIcon)
            {
                this.LargeIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.List)
            {
                this.ListsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.SmallIcon)
            {
                this.SmallIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Tile)
            {
                this.TilesIconsRadioButton.Checked = true;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false)
            {
                this.DOSBoxPathTextBox.Text = UserDataAccessor.UserData.DBPath;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false)
            {
                this.DOSBoxConfFileTextBox.Text = UserDataAccessor.UserData.DBDefaultConfFilePath;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false)
            {
                this.DOSBoxLangFileTextBox.Text = UserDataAccessor.UserData.DBDefaultLangFilePath;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
            {
                this.EditorBinaryPathTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorAdditionalParameters) == false)
            {
                this.AdditionalParametersTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false)
            {
                this.CDImageDirTextBox.Text = UserDataAccessor.UserData.CDsDefaultDir;
            }

            if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesDefaultDir) == false)
            {
                this.GamesDirTextBox.Text = UserDataAccessor.UserData.GamesDefaultDir;
            }

            this.AllOfThemCheckBox.Checked = UserDataAccessor.UserData.DefaultIconViewOverride;
            this.CategoriesListView.Columns.Add("Name");
            this.CategoriesListView.Columns[0].Width = this.CategoriesListView.Width;
            this.CategoriesListView.Items.Clear();
            foreach (Category categoryToDisplay in UserDataAccessor.UserData.ListChildren)
            {
                ListViewItem itemToAdd = new ListViewItem(categoryToDisplay.Title)
                {
                    Tag = categoryToDisplay.Signature
                };
                this.CategoriesListView.Items.Add(itemToAdd);
            }
            this.PortableModeCheckBox.Checked = UserDataAccessor.UserData.PortableMode;
            this.PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void CheckForPortableModeAvailabilityAndUpdateUI()
        {
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                this.PortableModeCheckBox.Enabled = false;
                this.PortableModeCheckBox.Checked = false;
                this.StatusStripLabel.Text = "Portable Mode : unavailable (AmpShell cannot write in the folder where it is located).";
            }
            else
            {
                this.PortableModeCheckBox.Checked = UserDataAccessor.UserData.PortableMode;
                this.StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            this.CategoriesListViewItemMoveTo(0);
        }

        private void DOSBoxPathBrowseButton_Click(object sender, EventArgs e)
        {
            using (var dosBoxExePathFileDialog = new OpenFileDialog())
            {
                dosBoxExePathFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                dosBoxExePathFileDialog.Title = this.DOSBoxExecutableLabel.Text;
                dosBoxExePathFileDialog.Filter = "DOSBox executable (dosbox*)|dosbox*|All files|*";
                if (dosBoxExePathFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //retrieve the selected dosbox.exe path into Amp.DBPath
                    UserDataAccessor.UserData.DBPath = dosBoxExePathFileDialog.FileName;
                    this.DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
                }
                else if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath))
                {
                    MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var dosboxDefaultConfFileDialog = new OpenFileDialog())
            {
                if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false
                    && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath)))
                {
                    dosboxDefaultConfFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath);
                }

                dosboxDefaultConfFileDialog.Title = this.DOSBoxConfLabel.Text;
                dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
                if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                    UserDataAccessor.UserData.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                    this.DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
                }
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var dosBoxDefaultLangFileDialog = new OpenFileDialog())
            {
                if (StringExt.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false
                    && Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath)))
                {
                    dosBoxDefaultLangFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath);
                }

                dosBoxDefaultLangFileDialog.Title = this.DOSBoxLangFileLabel.Text;
                dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
                if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                    UserDataAccessor.UserData.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
                    this.DOSBoxLangFileTextBox.Text = dosBoxDefaultLangFileDialog.FileName;
                }
            }
        }

        private void BrowseGamesDirButton_Click(object sender, EventArgs e)
        {
            using (var gamesFolderBrowserDialog = new FolderBrowserDialog())
            {
                if (gamesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.GamesDirTextBox.Text = gamesFolderBrowserDialog.SelectedPath;
                }
            }
        }

        private void BrowseCDImageDirButton_Click(object sender, EventArgs e)
        {
            using (var cdImagesFolderBrowserDialog = new FolderBrowserDialog())
            {
                if (cdImagesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.CDImageDirTextBox.Text = cdImagesFolderBrowserDialog.SelectedPath;
                }
            }
        }

        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            this.CategoriesListView.Sorting = SortOrder.Ascending;
            this.CategoriesListView.Sort();
            this.CategoriesListView.Sorting = SortOrder.None;
        }

        private void MoveLastButton_Click(object sender, EventArgs e) => this.CategoriesListViewItemMoveTo(this.CategoriesListView.Items.Count - 1);

        private void LargeIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }

        private void SmallIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.LargeIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }

        private void TilesIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.LargeIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }

        private void ListsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.LargeIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }

        private void DetailsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.LargeIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
        }

        private void MoveBackButton_Click(object sender, EventArgs e)
        {
            if (this.CategoriesListView.FocusedItem.Index > 0)
            {
                this.CategoriesListViewItemMoveTo(this.CategoriesListView.FocusedItem.Index - 1);
            }
        }

        private void MoveNextButton_Click(object sender, EventArgs e)
        {
            if (this.CategoriesListView.FocusedItem.Index < this.CategoriesListView.Items.Count)
            {
                this.CategoriesListViewItemMoveTo(this.CategoriesListView.FocusedItem.Index + 1);
            }
        }

        private void CategoriesListViewItemMoveTo(int index)
        {
            ListViewItem listViewItemCopy = new ListViewItem
            {
                Text = this.CategoriesListView.FocusedItem.Text,
                Tag = this.CategoriesListView.FocusedItem.Tag
            };
            this.CategoriesListView.Items.RemoveAt(this.CategoriesListView.FocusedItem.Index);
            this.CategoriesListView.Items.Insert(index, listViewItemCopy);
            this.CategoriesListView.FocusedItem = this.CategoriesListView.Items[(string)listViewItemCopy.Tag];
        }

        private void PortableModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.PortableModeCheckBox.Checked == true)
            {
                this.ReScanDirButton.Enabled = true;
                this.DOSBoxPathBrowseButton.Enabled = false;
                this.DOSBoxConfFileTextBox.Enabled = false;
                this.DOSBoxConfFileBrowseButton.Enabled = false;
                this.DOSBoxLangFileTextBox.Enabled = false;
                this.DOSBoxLangFileBrowseButton.Enabled = false;
                this.DOSBoxPathTextBox.Enabled = false;
                this.GamesDirTextBox.Enabled = false;
                this.BrowseGamesDirButton.Enabled = false;
                this.CDImageDirTextBox.Enabled = false;
                this.BrowseCDImageDirButton.Enabled = false;
                this.EditorBinaryPathTextBox.Enabled = false;
                this.BrowseForEditorButton.Enabled = false;
                if (File.Exists(Path.Combine(Application.StartupPath, "dosbox.exe")) && UserDataAccessor.UserData.GamesUseDOSBox)
                {
                    this.DOSBoxPathTextBox.Text = Path.Combine(Application.StartupPath, "dosbox.exe");
                }
                else
                {
                    this.DOSBoxPathTextBox.Text = "dosbox.exe isn't is the same directory as AmpShell.exe!";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.conf").Length > 0 && UserDataAccessor.UserData.GamesUseDOSBox)
                {
                    this.DOSBoxConfFileTextBox.Text = Directory.GetFiles(Application.StartupPath, "*.conf")[0];
                }
                else
                {
                    this.DOSBoxConfFileTextBox.Text = "No configuration file (*.conf) found in AmpShell's directory.";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0 && UserDataAccessor.UserData.GamesUseDOSBox)
                {
                    this.DOSBoxLangFileTextBox.Text = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
                }
                else
                {
                    this.DOSBoxLangFileTextBox.Text = "No language file (*.lng) found in AmpShell's directory.";
                }

                if (File.Exists(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe")))
                {
                    this.EditorBinaryPathTextBox.Text = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe");
                }
                else if (File.Exists(Path.Combine(Application.StartupPath, "TextEditor.exe")))
                {
                    this.EditorBinaryPathTextBox.Text = Path.Combine(Application.StartupPath, "TextEditor.exe");
                }
                else
                {
                    this.EditorBinaryPathTextBox.Text = NoTextEditorFound;
                }

                this.StatusStripLabel.Text = "Portable Mode : active (all files (or at least DOSBox, and all the games) must be in the same directory as AmpShell).";
            }
            else
            {
                this.ReScanDirButton.Enabled = false;
                this.DOSBoxPathBrowseButton.Enabled = true;
                this.DOSBoxConfFileTextBox.Enabled = true;
                this.DOSBoxConfFileBrowseButton.Enabled = true;
                this.DOSBoxLangFileTextBox.Enabled = true;
                this.DOSBoxLangFileBrowseButton.Enabled = true;
                this.DOSBoxPathTextBox.Enabled = true;
                this.GamesDirTextBox.Enabled = true;
                this.BrowseGamesDirButton.Enabled = true;
                this.CDImageDirTextBox.Enabled = true;
                this.BrowseCDImageDirButton.Enabled = true;
                this.EditorBinaryPathTextBox.Enabled = true;
                this.BrowseForEditorButton.Enabled = true;
                this.DOSBoxPathTextBox.Text = UserDataAccessor.UserData.DBPath;
                this.DOSBoxConfFileTextBox.Text = UserDataAccessor.UserData.DBDefaultConfFilePath;
                this.DOSBoxLangFileTextBox.Text = UserDataAccessor.UserData.DBDefaultLangFilePath;
                this.GamesDirTextBox.Text = UserDataAccessor.UserData.GamesDefaultDir;
                this.CDImageDirTextBox.Text = UserDataAccessor.UserData.CDsDefaultDir;
                this.EditorBinaryPathTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            this.PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}