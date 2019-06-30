/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Configuration;
using AmpShell.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmpShell.WinForms
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void BrowseForEditorButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog textEdtiorFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(EditorBinaryPathTextBox.Text) == false)
            {
                if (Directory.Exists(Directory.GetParent(EditorBinaryPathTextBox.Text).ToString()))
                {
                    textEdtiorFileDialog.InitialDirectory = Directory.GetParent(EditorBinaryPathTextBox.Text).ToString();
                }
                else
                {
                    textEdtiorFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                }
            }
            if (textEdtiorFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                EditorBinaryPathTextBox.Text = textEdtiorFileDialog.FileName;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GamesDirTextBox.Text) == false)
            {
                if (Directory.Exists(GamesDirTextBox.Text))
                {
                    UserDataLoaderSaver.UserData.GamesDefaultDir = GamesDirTextBox.Text;
                }
            }
            if (string.IsNullOrWhiteSpace(CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(CDImageDirTextBox.Text))
                {
                    UserDataLoaderSaver.UserData.CDsDefaultDir = CDImageDirTextBox.Text;
                }
            }
            UserDataLoaderSaver.UserData.GamesAdditionalCommands = GameAdditionalCommandsTextBox.Text;
            UserDataLoaderSaver.UserData.GamesNoConsole = NoConsoleCheckBox.Checked;
            UserDataLoaderSaver.UserData.GamesInFullScreen = FullscreenCheckBox.Checked;
            UserDataLoaderSaver.UserData.GamesQuitOnExit = QuitOnExitCheckBox.Checked;
            if (File.Exists(EditorBinaryPathTextBox.Text))
            {
                UserDataLoaderSaver.UserData.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            }

            UserDataLoaderSaver.UserData.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            UserDataLoaderSaver.UserData.CategoryDeletePrompt = CategoyDeletePromptCheckBox.Checked;
            UserDataLoaderSaver.UserData.GameDeletePrompt = GameDeletePromptCheckBox.Checked;
            UserDataLoaderSaver.UserData.RememberWindowSize = WindowSizeCheckBox.Checked;
            UserDataLoaderSaver.UserData.RememberWindowPosition = WindowPositionCheckBox.Checked;
            UserDataLoaderSaver.UserData.MenuBarVisible = ShowMenuBarCheckBox.Checked;
            UserDataLoaderSaver.UserData.ToolBarVisible = ShowToolBarCheckBox.Checked;
            UserDataLoaderSaver.UserData.StatusBarVisible = ShowDetailsBarCheckBox.Checked;
            UserDataLoaderSaver.UserData.DefaultIconViewOverride = AllOfThemCheckBox.Checked;
            UserDataLoaderSaver.UserData.DBPath = DOSBoxPathTextBox.Text;
            UserDataLoaderSaver.UserData.DBDefaultConfFilePath = DOSBoxConfFileTextBox.Text;
            UserDataLoaderSaver.UserData.DBDefaultLangFilePath = DOSBoxLangFileTextBox.Text;
            UserDataLoaderSaver.UserData.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            UserDataLoaderSaver.UserData.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            if (LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                UserDataLoaderSaver.UserData.LargeViewModeSize = RootModel.LargeViewModeSizes[LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (LargeIconsRadioButton.Checked == true)
            {
                UserDataLoaderSaver.UserData.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (SmallIconsRadioButton.Checked == true)
            {
                UserDataLoaderSaver.UserData.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (ListsIconsRadioButton.Checked == true)
            {
                UserDataLoaderSaver.UserData.CategoriesDefaultViewMode = View.List;
            }

            if (TilesIconsRadioButton.Checked == true)
            {
                UserDataLoaderSaver.UserData.CategoriesDefaultViewMode = View.Tile;
            }

            if (DetailsIconsRadioButton.Checked == true)
            {
                UserDataLoaderSaver.UserData.CategoriesDefaultViewMode = View.Details;
            }

            UserDataLoaderSaver.UserData.PortableMode = PortableModeCheckBox.Checked;

            SyncCategoriesOrderWithTabOrder();

            if (UserDataLoaderSaver.UserData.PortableMode)
            {
                UserDataLoaderSaver.SaveUserSettings();
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

            foreach (Category ConcernedCategory in UserDataLoaderSaver.UserData.ListChildren)
            {
                UserDataLoaderSaver.UserData.MoveChildToPosition(ConcernedCategory, tabs.IndexOf(tabs.FirstOrDefault(x => Convert.ToString(x.Tag) == ConcernedCategory.Signature)));
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            CheckForPortableModeAvailabilityAndUpdateUI();
            LargeViewModeSizeComboBox.Text = LargeViewModeSizeComboBox.Items[RootModel.LargeViewModeSizes.IndexOf(UserDataLoaderSaver.UserData.LargeViewModeSize)].ToString();
            CategoyDeletePromptCheckBox.Checked = UserDataLoaderSaver.UserData.CategoryDeletePrompt;
            GameDeletePromptCheckBox.Checked = UserDataLoaderSaver.UserData.GameDeletePrompt;
            WindowPositionCheckBox.Checked = UserDataLoaderSaver.UserData.RememberWindowPosition;
            WindowSizeCheckBox.Checked = UserDataLoaderSaver.UserData.RememberWindowSize;
            ShowMenuBarCheckBox.Checked = UserDataLoaderSaver.UserData.MenuBarVisible;
            ShowToolBarCheckBox.Checked = UserDataLoaderSaver.UserData.ToolBarVisible;
            ShowDetailsBarCheckBox.Checked = UserDataLoaderSaver.UserData.StatusBarVisible;
            QuitOnExitCheckBox.Checked = UserDataLoaderSaver.UserData.GamesQuitOnExit;
            NoConsoleCheckBox.Checked = UserDataLoaderSaver.UserData.GamesNoConsole;
            FullscreenCheckBox.Checked = UserDataLoaderSaver.UserData.GamesInFullScreen;
            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.GamesAdditionalCommands) == false)
            {
                GameAdditionalCommandsTextBox.Text = UserDataLoaderSaver.UserData.GamesAdditionalCommands;
            }

            if (UserDataLoaderSaver.UserData.CategoriesDefaultViewMode == View.Details)
            {
                DetailsIconsRadioButton.Checked = true;
            }

            if (UserDataLoaderSaver.UserData.CategoriesDefaultViewMode == View.LargeIcon)
            {
                LargeIconsRadioButton.Checked = true;
            }

            if (UserDataLoaderSaver.UserData.CategoriesDefaultViewMode == View.List)
            {
                ListsIconsRadioButton.Checked = true;
            }

            if (UserDataLoaderSaver.UserData.CategoriesDefaultViewMode == View.SmallIcon)
            {
                SmallIconsRadioButton.Checked = true;
            }

            if (UserDataLoaderSaver.UserData.CategoriesDefaultViewMode == View.Tile)
            {
                TilesIconsRadioButton.Checked = true;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBPath) == false)
            {
                DOSBoxPathTextBox.Text = UserDataLoaderSaver.UserData.DBPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBDefaultConfFilePath) == false)
            {
                DOSBoxConfFileTextBox.Text = UserDataLoaderSaver.UserData.DBDefaultConfFilePath;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBDefaultLangFilePath) == false)
            {
                DOSBoxLangFileTextBox.Text = UserDataLoaderSaver.UserData.DBDefaultLangFilePath;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.ConfigEditorPath) == false)
            {
                EditorBinaryPathTextBox.Text = UserDataLoaderSaver.UserData.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.ConfigEditorAdditionalParameters) == false)
            {
                AdditionnalParametersTextBox.Text = UserDataLoaderSaver.UserData.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.CDsDefaultDir) == false)
            {
                CDImageDirTextBox.Text = UserDataLoaderSaver.UserData.CDsDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.GamesDefaultDir) == false)
            {
                GamesDirTextBox.Text = UserDataLoaderSaver.UserData.GamesDefaultDir;
            }

            AllOfThemCheckBox.Checked = UserDataLoaderSaver.UserData.DefaultIconViewOverride;
            CategoriesListView.Columns.Add("Name");
            CategoriesListView.Columns[0].Width = CategoriesListView.Width;
            CategoriesListView.Items.Clear();
            foreach (Category CategoryToDisplay in UserDataLoaderSaver.UserData.ListChildren)
            {
                ListViewItem ItemToAdd = new ListViewItem(CategoryToDisplay.Title)
                {
                    Tag = CategoryToDisplay.Signature
                };
                CategoriesListView.Items.Add(ItemToAdd);
            }
            PortableModeCheckBox.Checked = UserDataLoaderSaver.UserData.PortableMode;
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
                PortableModeCheckBox.Checked = UserDataLoaderSaver.UserData.PortableMode;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            CategoriesListViewItemMoveTo(0);
        }

        private void DOSBoxPathBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosBoxExePathFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Title = DOSBoxExecutableLabel.Text,
                Filter = "DOSBox executable (dosbox*)|dosbox*|All files|*"
            };
            if (dosBoxExePathFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected dosbox.exe path into Amp.DBPath
                UserDataLoaderSaver.UserData.DBPath = dosBoxExePathFileDialog.FileName;
                DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
            }
            else if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBPath))
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosboxDefaultConfFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBDefaultConfFilePath) == false
                && Directory.Exists(Directory.GetParent(UserDataLoaderSaver.UserData.DBDefaultConfFilePath).FullName))
            {
                dosboxDefaultConfFileDialog.InitialDirectory = Directory.GetParent(UserDataLoaderSaver.UserData.DBDefaultConfFilePath).FullName;
            }

            dosboxDefaultConfFileDialog.Title = DOSBoxConfLabel.Text;
            dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                UserDataLoaderSaver.UserData.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosBoxDefaultLangFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(UserDataLoaderSaver.UserData.DBDefaultLangFilePath) == false
                && Directory.Exists(Directory.GetParent(UserDataLoaderSaver.UserData.DBDefaultLangFilePath).FullName))
            {
                dosBoxDefaultLangFileDialog.InitialDirectory = Directory.GetParent(UserDataLoaderSaver.UserData.DBDefaultLangFilePath).FullName;
            }

            dosBoxDefaultLangFileDialog.Title = DOSBoxLangFileLabel.Text;
            dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                UserDataLoaderSaver.UserData.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
                DOSBoxLangFileTextBox.Text = dosBoxDefaultLangFileDialog.FileName;
            }
        }

        private void BrowseGamesDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog gamesFolderBrowserDialog = new FolderBrowserDialog();
            if (gamesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                GamesDirTextBox.Text = gamesFolderBrowserDialog.SelectedPath;
            }
        }

        private void BrowseCDImageDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog cdImagesFolderBrowserDialog = new FolderBrowserDialog();
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
                if (File.Exists(Application.StartupPath + "\\dosbox.exe"))
                {
                    DOSBoxPathTextBox.Text = Application.StartupPath + "\\dosbox.exe";
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

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe"))
                {
                    EditorBinaryPathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe";
                }
                else if (File.Exists(Application.StartupPath + "\\TextEditor.exe"))
                {
                    EditorBinaryPathTextBox.Text = Application.StartupPath + "\\TextEditor.exe";
                }
                else
                {
                    EditorBinaryPathTextBox.Text = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";
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
                DOSBoxPathTextBox.Text = UserDataLoaderSaver.UserData.DBPath;
                DOSBoxConfFileTextBox.Text = UserDataLoaderSaver.UserData.DBDefaultConfFilePath;
                DOSBoxLangFileTextBox.Text = UserDataLoaderSaver.UserData.DBDefaultLangFilePath;
                GamesDirTextBox.Text = UserDataLoaderSaver.UserData.GamesDefaultDir;
                CDImageDirTextBox.Text = UserDataLoaderSaver.UserData.CDsDefaultDir;
                EditorBinaryPathTextBox.Text = UserDataLoaderSaver.UserData.ConfigEditorPath;
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}