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

using AmpShell.AutoConfig;
using AmpShell.DAL;
using AmpShell.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmpShell.Views
{
    public partial class PreferencesForm : Form
    {
        private const string _noTextEditorFound = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";

        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void BrowseForEditorButton_Click(object sender, EventArgs e)
        {
            using var textEditorFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(EditorBinaryPathTextBox.Text) == false)
            {
                if (Directory.Exists(Path.GetDirectoryName(EditorBinaryPathTextBox.Text).ToString(CultureInfo.InvariantCulture)))
                {
                    textEditorFileDialog.InitialDirectory = Path.GetDirectoryName(EditorBinaryPathTextBox.Text).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    textEditorFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                }
            }
            if (textEditorFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                EditorBinaryPathTextBox.Text = textEditorFileDialog.FileName;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GamesDirTextBox.Text) == false)
            {
                if (Directory.Exists(GamesDirTextBox.Text))
                {
                    UserDataAccessor.UserData.GamesDefaultDir = GamesDirTextBox.Text;
                }
            }
            if (string.IsNullOrWhiteSpace(CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(CDImageDirTextBox.Text))
                {
                    UserDataAccessor.UserData.CDsDefaultDir = CDImageDirTextBox.Text;
                }
            }
            UserDataAccessor.UserData.GamesAdditionalCommands = GameAdditionalCommandsTextBox.Text;
            UserDataAccessor.UserData.GamesNoConsole = NoConsoleCheckBox.Checked;
            UserDataAccessor.UserData.GamesInFullScreen = FullscreenCheckBox.Checked;
            UserDataAccessor.UserData.GamesQuitOnExit = QuitOnExitCheckBox.Checked;
            if (File.Exists(EditorBinaryPathTextBox.Text))
            {
                UserDataAccessor.UserData.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            }

            UserDataAccessor.UserData.ConfigEditorAdditionalParameters = AdditionalParametersTextBox.Text;
            UserDataAccessor.UserData.CategoryDeletePrompt = CategoyDeletePromptCheckBox.Checked;
            UserDataAccessor.UserData.GameDeletePrompt = GameDeletePromptCheckBox.Checked;
            UserDataAccessor.UserData.RememberWindowSize = WindowSizeCheckBox.Checked;
            UserDataAccessor.UserData.RememberWindowPosition = WindowPositionCheckBox.Checked;
            UserDataAccessor.UserData.MenuBarVisible = ShowMenuBarCheckBox.Checked;
            UserDataAccessor.UserData.ToolBarVisible = ShowToolBarCheckBox.Checked;
            UserDataAccessor.UserData.StatusBarVisible = ShowDetailsBarCheckBox.Checked;
            UserDataAccessor.UserData.DefaultIconViewOverride = AllOfThemCheckBox.Checked;
            UserDataAccessor.UserData.DBPath = DOSBoxPathTextBox.Text;
            UserDataAccessor.UserData.DBDefaultConfFilePath = DOSBoxConfFileTextBox.Text;
            UserDataAccessor.UserData.DBDefaultLangFilePath = DOSBoxLangFileTextBox.Text;
            UserDataAccessor.UserData.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            UserDataAccessor.UserData.ConfigEditorAdditionalParameters = AdditionalParametersTextBox.Text;
            if (LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                UserDataAccessor.UserData.LargeViewModeSize = Preferences.LargeViewModeSizes[LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (LargeIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (SmallIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (ListsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.List;
            }

            if (TilesIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Tile;
            }

            if (DetailsIconsRadioButton.Checked == true)
            {
                UserDataAccessor.UserData.CategoriesDefaultViewMode = View.Details;
            }

            UserDataAccessor.UserData.PortableMode = PortableModeCheckBox.Checked;

            SyncCategoriesOrderWithTabOrder();

            if (UserDataAccessor.UserData.PortableMode)
            {
                UserDataAccessor.SaveUserSettings();
            }

            Close();
        }

        private void SyncCategoriesOrderWithTabOrder()
        {
            if (CategoriesListView.Items.Count < 2)
            {
                return;
            }

            List<ListViewItem> tabs = CategoriesListView.Items.Cast<ListViewItem>().ToList();

            foreach (Category ConcernedCategory in UserDataAccessor.UserData.ListChildren)
            {
                UserDataAccessor.UserData.MoveChildToPosition(ConcernedCategory, tabs.IndexOf(tabs.FirstOrDefault(x => Convert.ToString(x.Tag, CultureInfo.InvariantCulture) == ConcernedCategory.Signature)));
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            CheckForPortableModeAvailabilityAndUpdateUI();
            LargeViewModeSizeComboBox.Text = LargeViewModeSizeComboBox.Items[Preferences.LargeViewModeSizes.IndexOf(UserDataAccessor.UserData.LargeViewModeSize)].ToString();
            CategoyDeletePromptCheckBox.Checked = UserDataAccessor.UserData.CategoryDeletePrompt;
            GameDeletePromptCheckBox.Checked = UserDataAccessor.UserData.GameDeletePrompt;
            WindowPositionCheckBox.Checked = UserDataAccessor.UserData.RememberWindowPosition;
            WindowSizeCheckBox.Checked = UserDataAccessor.UserData.RememberWindowSize;
            ShowMenuBarCheckBox.Checked = UserDataAccessor.UserData.MenuBarVisible;
            ShowToolBarCheckBox.Checked = UserDataAccessor.UserData.ToolBarVisible;
            ShowDetailsBarCheckBox.Checked = UserDataAccessor.UserData.StatusBarVisible;
            QuitOnExitCheckBox.Checked = UserDataAccessor.UserData.GamesQuitOnExit;
            NoConsoleCheckBox.Checked = UserDataAccessor.UserData.GamesNoConsole;
            FullscreenCheckBox.Checked = UserDataAccessor.UserData.GamesInFullScreen;
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesAdditionalCommands) == false)
            {
                GameAdditionalCommandsTextBox.Text = UserDataAccessor.UserData.GamesAdditionalCommands;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Details)
            {
                DetailsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.LargeIcon)
            {
                LargeIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.List)
            {
                ListsIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.SmallIcon)
            {
                SmallIconsRadioButton.Checked = true;
            }

            if (UserDataAccessor.UserData.CategoriesDefaultViewMode == View.Tile)
            {
                TilesIconsRadioButton.Checked = true;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false)
            {
                DOSBoxPathTextBox.Text = UserDataAccessor.UserData.DBPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false)
            {
                DOSBoxConfFileTextBox.Text = UserDataAccessor.UserData.DBDefaultConfFilePath;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false)
            {
                DOSBoxLangFileTextBox.Text = UserDataAccessor.UserData.DBDefaultLangFilePath;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
            {
                EditorBinaryPathTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorAdditionalParameters) == false)
            {
                AdditionalParametersTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.CDsDefaultDir) == false)
            {
                CDImageDirTextBox.Text = UserDataAccessor.UserData.CDsDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.GamesDefaultDir) == false)
            {
                GamesDirTextBox.Text = UserDataAccessor.UserData.GamesDefaultDir;
            }

            AllOfThemCheckBox.Checked = UserDataAccessor.UserData.DefaultIconViewOverride;
            CategoriesListView.Columns.Add("Name");
            CategoriesListView.Columns[0].Width = CategoriesListView.Width;
            CategoriesListView.Items.Clear();
            foreach (Category CategoryToDisplay in UserDataAccessor.UserData.ListChildren)
            {
                ListViewItem ItemToAdd = new ListViewItem(CategoryToDisplay.Title)
                {
                    Tag = CategoryToDisplay.Signature
                };
                CategoriesListView.Items.Add(ItemToAdd);
            }
            PortableModeCheckBox.Checked = UserDataAccessor.UserData.PortableMode;
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void CheckForPortableModeAvailabilityAndUpdateUI()
        {
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                PortableModeCheckBox.Enabled = false;
                PortableModeCheckBox.Checked = false;
                StatusStripLabel.Text = "Portable Mode : unavailable (AmpShell cannot write in the folder where it is located).";
            }
            else
            {
                PortableModeCheckBox.Checked = UserDataAccessor.UserData.PortableMode;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            CategoriesListViewItemMoveTo(0);
        }

        private void DOSBoxPathBrowseButton_Click(object sender, EventArgs e)
        {
            using var dosBoxExePathFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Title = DOSBoxExecutableLabel.Text,
                Filter = "DOSBox executable (dosbox*)|dosbox*|All files|*"
            };
            if (dosBoxExePathFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected dosbox.exe path into Amp.DBPath
                UserDataAccessor.UserData.DBPath = dosBoxExePathFileDialog.FileName;
                DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
            }
            else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath))
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            using var dosboxDefaultConfFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false
&& Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath)))
            {
                dosboxDefaultConfFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultConfFilePath);
            }

            dosboxDefaultConfFileDialog.Title = DOSBoxConfLabel.Text;
            dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                UserDataAccessor.UserData.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            using var dosBoxDefaultLangFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultLangFilePath) == false
&& Directory.Exists(Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath)))
            {
                dosBoxDefaultLangFileDialog.InitialDirectory = Path.GetDirectoryName(UserDataAccessor.UserData.DBDefaultLangFilePath);
            }

            dosBoxDefaultLangFileDialog.Title = DOSBoxLangFileLabel.Text;
            dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                UserDataAccessor.UserData.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
                DOSBoxLangFileTextBox.Text = dosBoxDefaultLangFileDialog.FileName;
            }
        }

        private void BrowseGamesDirButton_Click(object sender, EventArgs e)
        {
            using var gamesFolderBrowserDialog = new FolderBrowserDialog();
            if (gamesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                GamesDirTextBox.Text = gamesFolderBrowserDialog.SelectedPath;
            }
        }

        private void BrowseCDImageDirButton_Click(object sender, EventArgs e)
        {
            using var cdImagesFolderBrowserDialog = new FolderBrowserDialog();
            if (cdImagesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                CDImageDirTextBox.Text = cdImagesFolderBrowserDialog.SelectedPath;
            }
        }

        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            CategoriesListView.Sorting = SortOrder.Ascending;
            CategoriesListView.Sort();
            CategoriesListView.Sorting = SortOrder.None;
        }

        private void MoveLastButton_Click(object sender, EventArgs e)
        {
            CategoriesListViewItemMoveTo(CategoriesListView.Items.Count - 1);
        }

        private void LargeIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void SmallIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            LargeIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void TilesIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void ListsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
            DetailsIconsRadioButton.Checked = false;
        }

        private void DetailsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            SmallIconsRadioButton.Checked = false;
            LargeIconsRadioButton.Checked = false;
            ListsIconsRadioButton.Checked = false;
            TilesIconsRadioButton.Checked = false;
        }

        private void MoveBackButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index > 0)
            {
                CategoriesListViewItemMoveTo(CategoriesListView.FocusedItem.Index - 1);
            }
        }

        private void MoveNextButton_Click(object sender, EventArgs e)
        {
            if (CategoriesListView.FocusedItem.Index < CategoriesListView.Items.Count)
            {
                CategoriesListViewItemMoveTo(CategoriesListView.FocusedItem.Index + 1);
            }
        }

        private void CategoriesListViewItemMoveTo(int index)
        {
            ListViewItem listViewItemCopy = new ListViewItem
            {
                Text = CategoriesListView.FocusedItem.Text,
                Tag = CategoriesListView.FocusedItem.Tag
            };
            CategoriesListView.Items.RemoveAt(CategoriesListView.FocusedItem.Index);
            CategoriesListView.Items.Insert(index, listViewItemCopy);
            CategoriesListView.FocusedItem = CategoriesListView.Items[(string)listViewItemCopy.Tag];
        }

        private void PortableModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (PortableModeCheckBox.Checked == true)
            {
                ReScanDirButton.Enabled = true;
                DOSBoxPathBrowseButton.Enabled = false;
                DOSBoxConfFileTextBox.Enabled = false;
                DOSBoxConfFileBrowseButton.Enabled = false;
                DOSBoxLangFileTextBox.Enabled = false;
                DOSBoxLangFileBrowseButton.Enabled = false;
                DOSBoxPathTextBox.Enabled = false;
                GamesDirTextBox.Enabled = false;
                BrowseGamesDirButton.Enabled = false;
                CDImageDirTextBox.Enabled = false;
                BrowseCDImageDirButton.Enabled = false;
                EditorBinaryPathTextBox.Enabled = false;
                BrowseForEditorButton.Enabled = false;
                if (File.Exists(Path.Combine(Application.StartupPath, "\\dosbox.exe")))
                {
                    DOSBoxPathTextBox.Text = Path.Combine(Application.StartupPath, "\\dosbox.exe");
                }
                else
                {
                    DOSBoxPathTextBox.Text = "dosbox.exe isn't is the same directory as AmpShell.exe!";
                }

                if (Directory.GetFiles((Application.StartupPath), "*.conf").Length > 0)
                {
                    DOSBoxConfFileTextBox.Text = Directory.GetFiles((Application.StartupPath), "*.conf")[0];
                }
                else
                {
                    DOSBoxConfFileTextBox.Text = "No configuration file (*.conf) found in AmpShell's directory.";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0)
                {
                    DOSBoxLangFileTextBox.Text = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
                }
                else
                {
                    DOSBoxLangFileTextBox.Text = "No language file (*.lng) found in AmpShell's directory.";
                }

                if (File.Exists(Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe")))
                {
                    EditorBinaryPathTextBox.Text = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "notepad.exe");
                }
                else if (File.Exists(Path.Combine(Application.StartupPath, "\\TextEditor.exe")))
                {
                    EditorBinaryPathTextBox.Text = Path.Combine(Application.StartupPath, "\\TextEditor.exe");
                }
                else
                {
                    EditorBinaryPathTextBox.Text = _noTextEditorFound;
                }

                StatusStripLabel.Text = "Portable Mode : active (all files (or at least DOSBox, and all the games) must be in the same directory as AmpShell).";
            }
            else
            {
                ReScanDirButton.Enabled = false;
                DOSBoxPathBrowseButton.Enabled = true;
                DOSBoxConfFileTextBox.Enabled = true;
                DOSBoxConfFileBrowseButton.Enabled = true;
                DOSBoxLangFileTextBox.Enabled = true;
                DOSBoxLangFileBrowseButton.Enabled = true;
                DOSBoxPathTextBox.Enabled = true;
                GamesDirTextBox.Enabled = true;
                BrowseGamesDirButton.Enabled = true;
                CDImageDirTextBox.Enabled = true;
                BrowseCDImageDirButton.Enabled = true;
                EditorBinaryPathTextBox.Enabled = true;
                BrowseForEditorButton.Enabled = true;
                DOSBoxPathTextBox.Text = UserDataAccessor.UserData.DBPath;
                DOSBoxConfFileTextBox.Text = UserDataAccessor.UserData.DBDefaultConfFilePath;
                DOSBoxLangFileTextBox.Text = UserDataAccessor.UserData.DBDefaultLangFilePath;
                GamesDirTextBox.Text = UserDataAccessor.UserData.GamesDefaultDir;
                CDImageDirTextBox.Text = UserDataAccessor.UserData.CDsDefaultDir;
                EditorBinaryPathTextBox.Text = UserDataAccessor.UserData.ConfigEditorPath;
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}