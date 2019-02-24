/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
using AmpShell.UserData;
using System;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.WinForms
{
    public partial class Main_Prefs : Form
    {
        private UserPrefs _savedUserPrefs;
        public Main_Prefs(UserPrefs currentUserPrefs)
        {
            InitializeComponent();
            SavedUserPrefs = currentUserPrefs;
        }

        public UserPrefs SavedUserPrefs
        {
            get => _savedUserPrefs;
            set
            {
                if (_savedUserPrefs != value)
                {
                    _savedUserPrefs = value;
                }
            }
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
                    SavedUserPrefs.GamesDefaultDir = GamesDirTextBox.Text;
                }
            }
            if (string.IsNullOrWhiteSpace(CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(CDImageDirTextBox.Text))
                {
                    SavedUserPrefs.CDsDefaultDir = CDImageDirTextBox.Text;
                }
            }
            SavedUserPrefs.GamesAdditionalCommands = GameAdditionalCommandsTextBox.Text;
            SavedUserPrefs.GamesNoConsole = NoConsoleCheckBox.Checked;
            SavedUserPrefs.GamesInFullScreen = FullscreenCheckBox.Checked;
            SavedUserPrefs.GamesQuitOnExit = QuitOnExitCheckBox.Checked;
            SavedUserPrefs.OnlyNames = GameIconsCheckBox.Checked;
            if (File.Exists(EditorBinaryPathTextBox.Text))
            {
                SavedUserPrefs.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            }

            SavedUserPrefs.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            SavedUserPrefs.CategoryDeletePrompt = CategoyDeletePromptCheckBox.Checked;
            SavedUserPrefs.GameDeletePrompt = GameDeletePromptCheckBox.Checked;
            SavedUserPrefs.RememberWindowSize = WindowSizeCheckBox.Checked;
            SavedUserPrefs.RememberWindowPosition = WindowPositionCheckBox.Checked;
            SavedUserPrefs.MenuBarVisible = ShowMenuBarCheckBox.Checked;
            SavedUserPrefs.ToolBarVisible = ShowToolBarCheckBox.Checked;
            SavedUserPrefs.StatusBarVisible = ShowDetailsBarCheckBox.Checked;
            SavedUserPrefs.DefaultIconViewOverride = AllOfThemCheckBox.Checked;
            SavedUserPrefs.DBPath = DOSBoxPathTextBox.Text;
            SavedUserPrefs.DBDefaultConfFilePath = DOSBoxConfFileTextBox.Text;
            SavedUserPrefs.DBDefaultLangFilePath = DOSBoxLangFileTextBox.Text;
            SavedUserPrefs.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            SavedUserPrefs.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            if (LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                SavedUserPrefs.LargeViewModeSize = UserPrefs.LargeViewModeSizes[LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (LargeIconsRadioButton.Checked == true)
            {
                SavedUserPrefs.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (SmallIconsRadioButton.Checked == true)
            {
                SavedUserPrefs.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (ListsIconsRadioButton.Checked == true)
            {
                SavedUserPrefs.CategoriesDefaultViewMode = View.List;
            }

            if (TilesIconsRadioButton.Checked == true)
            {
                SavedUserPrefs.CategoriesDefaultViewMode = View.Tile;
            }

            if (DetailsIconsRadioButton.Checked == true)
            {
                SavedUserPrefs.CategoriesDefaultViewMode = View.Details;
            }

            foreach (UserCategory ConcernedCategory in SavedUserPrefs.ListChildren)
            {
                SavedUserPrefs.MoveChildToPosition(ConcernedCategory, CategoriesListView.Items[ConcernedCategory.Signature].Index);
            }

            SavedUserPrefs.PortableMode = PortableModeCheckBox.Checked;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            if (Directory.GetDirectoryRoot(Application.StartupPath) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(Application.StartupPath) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + " (x86)")
            {
                PortableModeCheckBox.Enabled = false;
                PortableModeCheckBox.Checked = false;
                StatusStripLabel.Text = "Portable Mode : unavailable (AmpShell is in the Program Files system directory).";
            }
            else
            {
                PortableModeCheckBox.Checked = SavedUserPrefs.PortableMode;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
            LargeViewModeSizeComboBox.Text = LargeViewModeSizeComboBox.Items[UserPrefs.LargeViewModeSizes.IndexOf(SavedUserPrefs.LargeViewModeSize)].ToString();
            CategoyDeletePromptCheckBox.Checked = SavedUserPrefs.CategoryDeletePrompt;
            GameDeletePromptCheckBox.Checked = SavedUserPrefs.GameDeletePrompt;
            WindowPositionCheckBox.Checked = SavedUserPrefs.RememberWindowPosition;
            WindowSizeCheckBox.Checked = SavedUserPrefs.RememberWindowSize;
            ShowMenuBarCheckBox.Checked = SavedUserPrefs.MenuBarVisible;
            ShowToolBarCheckBox.Checked = SavedUserPrefs.ToolBarVisible;
            ShowDetailsBarCheckBox.Checked = SavedUserPrefs.StatusBarVisible;
            GameIconsCheckBox.Checked = SavedUserPrefs.OnlyNames;
            QuitOnExitCheckBox.Checked = SavedUserPrefs.GamesQuitOnExit;
            NoConsoleCheckBox.Checked = SavedUserPrefs.GamesNoConsole;
            FullscreenCheckBox.Checked = SavedUserPrefs.GamesInFullScreen;
            if (string.IsNullOrWhiteSpace(SavedUserPrefs.GamesAdditionalCommands) == false)
            {
                GameAdditionalCommandsTextBox.Text = SavedUserPrefs.GamesAdditionalCommands;
            }

            if (SavedUserPrefs.CategoriesDefaultViewMode == View.Details)
            {
                DetailsIconsRadioButton.Checked = true;
            }

            if (SavedUserPrefs.CategoriesDefaultViewMode == View.LargeIcon)
            {
                LargeIconsRadioButton.Checked = true;
            }

            if (SavedUserPrefs.CategoriesDefaultViewMode == View.List)
            {
                ListsIconsRadioButton.Checked = true;
            }

            if (SavedUserPrefs.CategoriesDefaultViewMode == View.SmallIcon)
            {
                SmallIconsRadioButton.Checked = true;
            }

            if (SavedUserPrefs.CategoriesDefaultViewMode == View.Tile)
            {
                TilesIconsRadioButton.Checked = true;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBPath) == false)
            {
                DOSBoxPathTextBox.Text = SavedUserPrefs.DBPath;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBDefaultConfFilePath) == false)
            {
                DOSBoxConfFileTextBox.Text = SavedUserPrefs.DBDefaultConfFilePath;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBDefaultLangFilePath) == false)
            {
                DOSBoxLangFileTextBox.Text = SavedUserPrefs.DBDefaultLangFilePath;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.ConfigEditorPath) == false)
            {
                EditorBinaryPathTextBox.Text = SavedUserPrefs.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.ConfigEditorAdditionalParameters) == false)
            {
                AdditionnalParametersTextBox.Text = SavedUserPrefs.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.CDsDefaultDir) == false)
            {
                CDImageDirTextBox.Text = SavedUserPrefs.CDsDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(SavedUserPrefs.GamesDefaultDir) == false)
            {
                GamesDirTextBox.Text = SavedUserPrefs.GamesDefaultDir;
            }

            AllOfThemCheckBox.Checked = SavedUserPrefs.DefaultIconViewOverride;
            CategoriesListView.Columns.Add("Name");
            CategoriesListView.Columns[0].Width = CategoriesListView.Width;
            CategoriesListView.Items.Clear();
            foreach (UserCategory CategoryToDisplay in SavedUserPrefs.ListChildren)
            {
                ListViewItem ItemToAdd = new ListViewItem(CategoryToDisplay.Title)
                {
                    Name = CategoryToDisplay.Signature
                };
                CategoriesListView.Items.Add(ItemToAdd);
            }
            PortableModeCheckBox.Checked = SavedUserPrefs.PortableMode;
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
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
                SavedUserPrefs.DBPath = dosBoxExePathFileDialog.FileName;
                DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
            }
            else if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBPath))
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosboxDefaultConfFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBDefaultConfFilePath) == false
                && Directory.Exists(Directory.GetParent(SavedUserPrefs.DBDefaultConfFilePath).FullName))
            {
                dosboxDefaultConfFileDialog.InitialDirectory = Directory.GetParent(SavedUserPrefs.DBDefaultConfFilePath).FullName;
            }

            dosboxDefaultConfFileDialog.Title = DOSBoxConfLabel.Text;
            dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                SavedUserPrefs.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosBoxDefaultLangFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(SavedUserPrefs.DBDefaultLangFilePath) == false
                && Directory.Exists(Directory.GetParent(SavedUserPrefs.DBDefaultLangFilePath).FullName))
            {
                dosBoxDefaultLangFileDialog.InitialDirectory = Directory.GetParent(SavedUserPrefs.DBDefaultLangFilePath).FullName;
            }

            dosBoxDefaultLangFileDialog.Title = DOSBoxLangFileLabel.Text;
            dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                SavedUserPrefs.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
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
                Name = CategoriesListView.FocusedItem.Name
            };
            CategoriesListView.Items.RemoveAt(CategoriesListView.FocusedItem.Index);
            CategoriesListView.Items.Insert(index, listViewItemCopy);
            CategoriesListView.FocusedItem = CategoriesListView.Items[listViewItemCopy.Name];
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
                DOSBoxPathTextBox.Text = SavedUserPrefs.DBPath;
                DOSBoxConfFileTextBox.Text = SavedUserPrefs.DBDefaultConfFilePath;
                DOSBoxLangFileTextBox.Text = SavedUserPrefs.DBDefaultLangFilePath;
                GamesDirTextBox.Text = SavedUserPrefs.GamesDefaultDir;
                CDImageDirTextBox.Text = SavedUserPrefs.CDsDefaultDir;
                EditorBinaryPathTextBox.Text = SavedUserPrefs.ConfigEditorPath;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}
