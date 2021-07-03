/*AmpShell : .NET front-end for DOSBox
* Copyright (C) 2009, 2021 Maximilien Noal
*This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with this program.
* If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.WinForms.Views
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using AmpShell.Core.AutoConfig;
    using AmpShell.Core.DAL;
    using AmpShell.Core.Model;

    using View = System.Windows.Forms.View;

    /// <summary> Form to modifiy application-level preferences. </summary>
    public partial class PreferencesForm : Form
    {
        private const string NoTextEditorFound = "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found.";

        public PreferencesForm()
        {
            this.InitializeComponent();
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                this.PortableModeCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().PortableMode;
                this.StatusStripLabel.Text = "Portable Mode : available (but disabled).";
            }
        }

        private void DetailsIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.LargeIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
        }

        private void DOSBoxConfFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var dosboxDefaultConfFileDialog = new OpenFileDialog())
            {
                if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath) == false
                    && Directory.Exists(Path.GetDirectoryName(Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath)))
                {
                    dosboxDefaultConfFileDialog.InitialDirectory = Path.GetDirectoryName(Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath);
                }

                dosboxDefaultConfFileDialog.Title = this.DOSBoxConfLabel.Text;
                dosboxDefaultConfFileDialog.Filter = "DOSBox configuration files (*.conf)|*.conf|All files|*";
                if (dosboxDefaultConfFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //retrieve the selected .conf file path in Amp.DBDefaultConfFilePath
                    Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath = dosboxDefaultConfFileDialog.FileName;
                    this.DOSBoxConfFileTextBox.Text = dosboxDefaultConfFileDialog.FileName;
                }
            }
        }

        private void DOSBoxLangFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (var dosBoxDefaultLangFileDialog = new OpenFileDialog())
            {
                if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath) == false
                    && Directory.Exists(Path.GetDirectoryName(Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath)))
                {
                    dosBoxDefaultLangFileDialog.InitialDirectory = Path.GetDirectoryName(Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath);
                }

                dosBoxDefaultLangFileDialog.Title = this.DOSBoxLangFileLabel.Text;
                dosBoxDefaultLangFileDialog.Filter = "DOSBox language files (*.lng)|*.lng|All files|*";
                if (dosBoxDefaultLangFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    //retrieve the selected .lng file path in Amp.DBDefaultLangFilePath
                    Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath = dosBoxDefaultLangFileDialog.FileName;
                    this.DOSBoxLangFileTextBox.Text = dosBoxDefaultLangFileDialog.FileName;
                }
            }
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
                    Program.UserDataAccessorInstance.WithUserData().DBPath = dosBoxExePathFileDialog.FileName;
                    this.DOSBoxPathTextBox.Text = dosBoxExePathFileDialog.FileName;
                }
                else if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBPath))
                {
                    MessageBox.Show("Location of DOSBox's executable unknown. You will not be able to run games!", "Select DOSBox's executable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LargeIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
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

        private void Main_Prefs_Load(object sender, EventArgs e)
        {
            this.CheckForPortableModeAvailabilityAndUpdateUI();
            this.LargeViewModeSizeComboBox.Text = this.LargeViewModeSizeComboBox.Items[Core.Model.Preferences.LargeViewModeSizes.IndexOf(Program.UserDataAccessorInstance.WithUserData().LargeViewModeSize)].ToString();
            this.CategoyDeletePromptCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().CategoryDeletePrompt;
            this.GameDeletePromptCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().GameDeletePrompt;
            this.WindowPositionCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().RememberWindowPosition;
            this.WindowSizeCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().RememberWindowSize;
            this.ShowMenuBarCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().MenuBarVisible;
            this.ShowToolBarCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().ToolBarVisible;
            this.ShowDetailsBarCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().StatusBarVisible;
            this.QuitOnExitCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().GamesQuitOnExit;
            this.NoConsoleCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().GamesNoConsole;
            this.FullscreenCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().GamesInFullScreen;
            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().GamesAdditionalCommands) == false)
            {
                this.GameAdditionalCommandsTextBox.Text = Program.UserDataAccessorInstance.WithUserData().GamesAdditionalCommands;
            }

            if ((View)Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode == View.Details)
            {
                this.DetailsIconsRadioButton.Checked = true;
            }

            if ((View)Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode == View.LargeIcon)
            {
                this.LargeIconsRadioButton.Checked = true;
            }

            if ((View)Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode == View.List)
            {
                this.ListsIconsRadioButton.Checked = true;
            }

            if ((View)Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode == View.SmallIcon)
            {
                this.SmallIconsRadioButton.Checked = true;
            }

            if ((View)Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode == View.Tile)
            {
                this.TilesIconsRadioButton.Checked = true;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBPath) == false)
            {
                this.DOSBoxPathTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBPath;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath) == false)
            {
                this.DOSBoxConfFileTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath) == false)
            {
                this.DOSBoxLangFileTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath) == false)
            {
                this.EditorBinaryPathTextBox.Text = Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().ConfigEditorAdditionalParameters) == false)
            {
                this.AdditionalParametersTextBox.Text = Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().CDsDefaultDir) == false)
            {
                this.CDImageDirTextBox.Text = Program.UserDataAccessorInstance.WithUserData().CDsDefaultDir;
            }

            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.WithUserData().GamesDefaultDir) == false)
            {
                this.GamesDirTextBox.Text = Program.UserDataAccessorInstance.WithUserData().GamesDefaultDir;
            }

            this.AllOfThemCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().DefaultIconViewOverride;
            this.CategoriesListView.Columns.Add("Name");
            this.CategoriesListView.Columns[0].Width = this.CategoriesListView.Width;
            this.CategoriesListView.Items.Clear();
            for (int i = 0; i < Program.UserDataAccessorInstance.WithUserData().ListChildren.Count; i++)
            {
                Category categoryToDisplay = (Category)Program.UserDataAccessorInstance.WithUserData().ListChildren[i];
                ListViewItem itemToAdd = new ListViewItem(categoryToDisplay.Title)
                {
                    Tag = categoryToDisplay.Signature
                };
                this.CategoriesListView.Items.Add(itemToAdd);
            }
            this.PortableModeCheckBox.Checked = Program.UserDataAccessorInstance.WithUserData().PortableMode;
            this.PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void MoveBackButton_Click(object sender, EventArgs e)
        {
            if (this.CategoriesListView.FocusedItem.Index > 0)
            {
                this.CategoriesListViewItemMoveTo(this.CategoriesListView.FocusedItem.Index - 1);
            }
        }

        private void MoveFirstButton_Click(object sender, EventArgs e)
        {
            this.CategoriesListViewItemMoveTo(0);
        }

        private void MoveLastButton_Click(object sender, EventArgs e) => this.CategoriesListViewItemMoveTo(this.CategoriesListView.Items.Count - 1);

        private void MoveNextButton_Click(object sender, EventArgs e)
        {
            if (this.CategoriesListView.FocusedItem.Index < this.CategoriesListView.Items.Count)
            {
                this.CategoriesListViewItemMoveTo(this.CategoriesListView.FocusedItem.Index + 1);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if (StringExt.IsNullOrWhiteSpace(this.GamesDirTextBox.Text) == false)
            {
                if (Directory.Exists(this.GamesDirTextBox.Text))
                {
                    Program.UserDataAccessorInstance.WithUserData().GamesDefaultDir = this.GamesDirTextBox.Text;
                }
            }
            if (StringExt.IsNullOrWhiteSpace(this.CDImageDirTextBox.Text) == false)
            {
                if (Directory.Exists(this.CDImageDirTextBox.Text))
                {
                    Program.UserDataAccessorInstance.WithUserData().CDsDefaultDir = this.CDImageDirTextBox.Text;
                }
            }
            Program.UserDataAccessorInstance.WithUserData().GamesAdditionalCommands = this.GameAdditionalCommandsTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().GamesUseDOSBox = this.UseDOSBoxToLaunchGamesCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().GamesNoConsole = this.NoConsoleCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().GamesInFullScreen = this.FullscreenCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().GamesQuitOnExit = this.QuitOnExitCheckBox.Checked;
            if (File.Exists(this.EditorBinaryPathTextBox.Text))
            {
                Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath = this.EditorBinaryPathTextBox.Text;
            }

            Program.UserDataAccessorInstance.WithUserData().ConfigEditorAdditionalParameters = this.AdditionalParametersTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().CategoryDeletePrompt = this.CategoyDeletePromptCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().GameDeletePrompt = this.GameDeletePromptCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().RememberWindowSize = this.WindowSizeCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().RememberWindowPosition = this.WindowPositionCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().MenuBarVisible = this.ShowMenuBarCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().ToolBarVisible = this.ShowToolBarCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().StatusBarVisible = this.ShowDetailsBarCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().DefaultIconViewOverride = this.AllOfThemCheckBox.Checked;
            Program.UserDataAccessorInstance.WithUserData().DBPath = this.DOSBoxPathTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath = this.DOSBoxConfFileTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath = this.DOSBoxLangFileTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath = this.EditorBinaryPathTextBox.Text;
            Program.UserDataAccessorInstance.WithUserData().ConfigEditorAdditionalParameters = this.AdditionalParametersTextBox.Text;
            if (this.LargeViewModeSizeComboBox.SelectedIndex >= 0)
            {
                Program.UserDataAccessorInstance.WithUserData().LargeViewModeSize = Core.Model.Preferences.LargeViewModeSizes[this.LargeViewModeSizeComboBox.SelectedIndex];
            }

            if (this.LargeIconsRadioButton.Checked == true)
            {
                Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode = (Core.Model.View)View.LargeIcon;
            }

            if (this.SmallIconsRadioButton.Checked == true)
            {
                Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode = (Core.Model.View)View.SmallIcon;
            }

            if (this.ListsIconsRadioButton.Checked == true)
            {
                Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode = (Core.Model.View)View.List;
            }

            if (this.TilesIconsRadioButton.Checked == true)
            {
                Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode = (Core.Model.View)View.Tile;
            }

            if (this.DetailsIconsRadioButton.Checked == true)
            {
                Program.UserDataAccessorInstance.WithUserData().CategoriesDefaultViewMode = (Core.Model.View)View.Details;
            }

            Program.UserDataAccessorInstance.WithUserData().PortableMode = this.PortableModeCheckBox.Checked;

            this.SyncCategoriesOrderWithTabOrder();

            if (Program.UserDataAccessorInstance.WithUserData().PortableMode)
            {
                Program.UserDataAccessorInstance.SaveUserSettings();
            }

            this.Close();
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
                if (File.Exists(Path.Combine(Application.StartupPath, "dosbox.exe")) && Program.UserDataAccessorInstance.WithUserData().GamesUseDOSBox)
                {
                    this.DOSBoxPathTextBox.Text = Path.Combine(Application.StartupPath, "dosbox.exe");
                }
                else
                {
                    this.DOSBoxPathTextBox.Text = "dosbox.exe isn't is the same directory as AmpShell.exe!";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.conf").Length > 0 && Program.UserDataAccessorInstance.WithUserData().GamesUseDOSBox)
                {
                    this.DOSBoxConfFileTextBox.Text = Directory.GetFiles(Application.StartupPath, "*.conf")[0];
                }
                else
                {
                    this.DOSBoxConfFileTextBox.Text = "No configuration file (*.conf) found in AmpShell's directory.";
                }

                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0 && Program.UserDataAccessorInstance.WithUserData().GamesUseDOSBox)
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
                this.DOSBoxPathTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBPath;
                this.DOSBoxConfFileTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBDefaultConfFilePath;
                this.DOSBoxLangFileTextBox.Text = Program.UserDataAccessorInstance.WithUserData().DBDefaultLangFilePath;
                this.GamesDirTextBox.Text = Program.UserDataAccessorInstance.WithUserData().GamesDefaultDir;
                this.CDImageDirTextBox.Text = Program.UserDataAccessorInstance.WithUserData().CDsDefaultDir;
                this.EditorBinaryPathTextBox.Text = Program.UserDataAccessorInstance.WithUserData().ConfigEditorPath;
            }
        }

        private void ReScanDirButton_Click(object sender, EventArgs e)
        {
            this.PortableModeCheckBox_CheckedChanged(sender, EventArgs.Empty);
        }

        private void SmallIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.LargeIconsRadioButton.Checked = false;
            this.TilesIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }

        private void SortByNameButton_Click(object sender, EventArgs e)
        {
            this.CategoriesListView.Sorting = SortOrder.Ascending;
            this.CategoriesListView.Sort();
            this.CategoriesListView.Sorting = SortOrder.None;
        }

        private void SyncCategoriesOrderWithTabOrder()
        {
            if (this.CategoriesListView.Items.Count < 2)
            {
                return;
            }

            List<ListViewItem> tabs = this.CategoriesListView.Items.Cast<ListViewItem>().ToList();

            for (int i = 0; i < Program.UserDataAccessorInstance.WithUserData().ListChildren.Count; i++)
            {
                Category category = (Category)Program.UserDataAccessorInstance.WithUserData().ListChildren[i];
                Program.UserDataAccessorInstance.WithUserData().MoveChildToPosition(category, tabs.IndexOf(tabs.FirstOrDefault(x => Convert.ToString(x.Tag, CultureInfo.InvariantCulture) == category.Signature)));
            }
        }

        private void TilesIconsRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.SmallIconsRadioButton.Checked = false;
            this.LargeIconsRadioButton.Checked = false;
            this.ListsIconsRadioButton.Checked = false;
            this.DetailsIconsRadioButton.Checked = false;
        }
    }
}