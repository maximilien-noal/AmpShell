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
using System.IO;
using System.Windows.Forms;

namespace AmpShell
{
    public partial class Main_Prefs : Form
    {
        private Window _ampInstance;
        public Main_Prefs(Window ampCurrentInstance)
        {
            InitializeComponent();
            AmpInstance = ampCurrentInstance;
        }

        public Window AmpInstance
        {
            get => _ampInstance;
            set
            {
                if (_ampInstance != value)
                {
                    _ampInstance = value;
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
                    AmpInstance.GamesDefaultDir = GamesDirTextBox.Text;
                }
            }
            if (string.IsNullOrWhiteSpace(CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(CDImageDirTextBox.Text))
                {
                    AmpInstance.CDsDefaultDir = CDImageDirTextBox.Text;
                }
            }
            AmpInstance.GamesAdditionalCommands = GameAdditionalCommandsTextBox.Text;
            AmpInstance.GamesNoConsole = NoConsoleCheckBox.Checked;
            AmpInstance.GamesInFullScreen = FullscreenCheckBox.Checked;
            AmpInstance.GamesQuitOnExit = QuitOnExitCheckBox.Checked;
            AmpInstance.OnlyNames = GameIconsCheckBox.Checked;
            if (File.Exists(EditorBinaryPathTextBox.Text))
            {
                AmpInstance.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            }

            AmpInstance.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            AmpInstance.CategoryDeletePrompt = CategoyDeletePromptCheckBox.Checked;
            AmpInstance.GameDeletePrompt = GameDeletePromptCheckBox.Checked;
            AmpInstance.RememberWindowSize = WindowSizeCheckBox.Checked;
            AmpInstance.RememberWindowPosition = WindowPositionCheckBox.Checked;
            AmpInstance.MenuBarVisible = ShowMenuBarCheckBox.Checked;
            AmpInstance.ToolBarVisible = ShowToolBarCheckBox.Checked;
            AmpInstance.StatusBarVisible = ShowDetailsBarCheckBox.Checked;
            AmpInstance.DefaultIconViewOverride = AllOfThemCheckBox.Checked;
            AmpInstance.DBPath = DOSBoxPathTextBox.Text;
            AmpInstance.DBDefaultConfFilePath = DOSBoxConfFileTextBox.Text;
            AmpInstance.DBDefaultLangFilePath = DOSBoxLangFileTextBox.Text;
            AmpInstance.ConfigEditorPath = EditorBinaryPathTextBox.Text;
            AmpInstance.ConfigEditorAdditionalParameters = AdditionnalParametersTextBox.Text;
            if (LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                AmpInstance.LargeViewModeSize = Window.LargeViewModeSizes[LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (LargeIconsRadioButton.Checked == true)
            {
                AmpInstance.CategoriesDefaultViewMode = View.LargeIcon;
            }

            if (SmallIconsRadioButton.Checked == true)
            {
                AmpInstance.CategoriesDefaultViewMode = View.SmallIcon;
            }

            if (ListsIconsRadioButton.Checked == true)
            {
                AmpInstance.CategoriesDefaultViewMode = View.List;
            }

            if (TilesIconsRadioButton.Checked == true)
            {
                AmpInstance.CategoriesDefaultViewMode = View.Tile;
            }

            if (DetailsIconsRadioButton.Checked == true)
            {
                AmpInstance.CategoriesDefaultViewMode = View.Details;
            }

            foreach (Category ConcernedCategory in AmpInstance.ListChildren)
            {
                AmpInstance.MoveChildToPosition(ConcernedCategory, CategoriesListView.Items[ConcernedCategory.Signature].Index);
            }

            AmpInstance.PortableMode = PortableModeCheckBox.Checked;
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
                PortableModeCheckBox.Checked = AmpInstance.PortableMode;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
            LargeViewModeSizeComboBox.Text = LargeViewModeSizeComboBox.Items[Window.LargeViewModeSizes.IndexOf(AmpInstance.LargeViewModeSize)].ToString();
            CategoyDeletePromptCheckBox.Checked = AmpInstance.CategoryDeletePrompt;
            GameDeletePromptCheckBox.Checked = AmpInstance.GameDeletePrompt;
            WindowPositionCheckBox.Checked = AmpInstance.RememberWindowPosition;
            WindowSizeCheckBox.Checked = AmpInstance.RememberWindowSize;
            ShowMenuBarCheckBox.Checked = AmpInstance.MenuBarVisible;
            ShowToolBarCheckBox.Checked = AmpInstance.ToolBarVisible;
            ShowDetailsBarCheckBox.Checked = AmpInstance.StatusBarVisible;
            GameIconsCheckBox.Checked = AmpInstance.OnlyNames;
            QuitOnExitCheckBox.Checked = AmpInstance.GamesQuitOnExit;
            NoConsoleCheckBox.Checked = AmpInstance.GamesNoConsole;
            FullscreenCheckBox.Checked = AmpInstance.GamesInFullScreen;
            if (string.IsNullOrWhiteSpace(AmpInstance.GamesAdditionalCommands) == false)
            {
                GameAdditionalCommandsTextBox.Text = AmpInstance.GamesAdditionalCommands;
            }

            if (AmpInstance.CategoriesDefaultViewMode == View.Details)
            {
                DetailsIconsRadioButton.Checked = true;
            }

            if (AmpInstance.CategoriesDefaultViewMode == View.LargeIcon)
            {
                LargeIconsRadioButton.Checked = true;
            }

            if (AmpInstance.CategoriesDefaultViewMode == View.List)
            {
                ListsIconsRadioButton.Checked = true;
            }

            if (AmpInstance.CategoriesDefaultViewMode == View.SmallIcon)
            {
                SmallIconsRadioButton.Checked = true;
            }

            if (AmpInstance.CategoriesDefaultViewMode == View.Tile)
            {
                TilesIconsRadioButton.Checked = true;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.DBPath) == false)
            {
                DOSBoxPathTextBox.Text = AmpInstance.DBPath;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.DBDefaultConfFilePath) == false)
            {
                DOSBoxConfFileTextBox.Text = AmpInstance.DBDefaultConfFilePath;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.DBDefaultLangFilePath) == false)
            {
                DOSBoxLangFileTextBox.Text = AmpInstance.DBDefaultLangFilePath;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.ConfigEditorPath) == false)
            {
                EditorBinaryPathTextBox.Text = AmpInstance.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.ConfigEditorAdditionalParameters) == false)
            {
                AdditionnalParametersTextBox.Text = AmpInstance.ConfigEditorPath;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.CDsDefaultDir) == false)
            {
                CDImageDirTextBox.Text = AmpInstance.CDsDefaultDir;
            }

            if (string.IsNullOrWhiteSpace(AmpInstance.GamesDefaultDir) == false)
            {
                GamesDirTextBox.Text = AmpInstance.GamesDefaultDir;
            }

            AllOfThemCheckBox.Checked = AmpInstance.DefaultIconViewOverride;
            CategoriesListView.Columns.Add("Name");
            CategoriesListView.Columns[0].Width = CategoriesListView.Width;
            CategoriesListView.Items.Clear();
            foreach (Category CategoryToDisplay in AmpInstance.ListChildren)
            {
                ListViewItem ItemToAdd = new ListViewItem(CategoryToDisplay.Title)
                {
                    Name = CategoryToDisplay.Signature
                };
                CategoriesListView.Items.Add(ItemToAdd);
            }
            PortableModeCheckBox.Checked = AmpInstance.PortableMode;
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
                AmpInstance.DBPath = dosBoxExePathFileDialog.FileName;
                DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
            }
            else if (string.IsNullOrWhiteSpace(AmpInstance.DBPath))
            {
                MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosboxDefaultConfFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(AmpInstance.DBDefaultConfFilePath) == false
                && Directory.Exists(Directory.GetParent(AmpInstance.DBDefaultConfFilePath).FullName))
            {
                dosboxDefaultConfFileDialog.InitialDirectory = Directory.GetParent(AmpInstance.DBDefaultConfFilePath).FullName;
            }

            dosboxDefaultConfFileDialog.Title = DOSBoxConfLabel.Text;
            dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
            if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                AmpInstance.DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosBoxDefaultLangFileDialog = new OpenFileDialog();
            if (string.IsNullOrWhiteSpace(AmpInstance.DBDefaultLangFilePath) == false
                && Directory.Exists(Directory.GetParent(AmpInstance.DBDefaultLangFilePath).FullName))
            {
                dosBoxDefaultLangFileDialog.InitialDirectory = Directory.GetParent(AmpInstance.DBDefaultLangFilePath).FullName;
            }

            dosBoxDefaultLangFileDialog.Title = DOSBoxLangFileLabel.Text;
            dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
            if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                AmpInstance.DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
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
                if (File.Exists(Application.StartupPath + "/dosbox.exe"))
                {
                    DOSBoxPathTextBox.Text = Application.StartupPath + "/dosbox.exe";
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
                else if (File.Exists(Application.StartupPath + "/TextEditor.exe"))
                {
                    EditorBinaryPathTextBox.Text = Application.StartupPath + "/TextEditor.exe";
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
                DOSBoxPathTextBox.Text = AmpInstance.DBPath;
                DOSBoxConfFileTextBox.Text = AmpInstance.DBDefaultConfFilePath;
                DOSBoxLangFileTextBox.Text = AmpInstance.DBDefaultLangFilePath;
                GamesDirTextBox.Text = AmpInstance.GamesDefaultDir;
                CDImageDirTextBox.Text = AmpInstance.CDsDefaultDir;
                EditorBinaryPathTextBox.Text = AmpInstance.ConfigEditorPath;
                StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }
    }
}
