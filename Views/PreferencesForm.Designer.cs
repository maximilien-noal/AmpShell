/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
namespace AmpShell.Views
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.PrefsTabControl = new System.Windows.Forms.TabControl();
            this.DOSBoxTabPage = new System.Windows.Forms.TabPage();
            this.DOSBoxLangFileLabel = new System.Windows.Forms.Label();
            this.DOSBoxConfLabel = new System.Windows.Forms.Label();
            this.DOSBoxExecutableLabel = new System.Windows.Forms.Label();
            this.DOSBoxLangFileBrowseButton = new System.Windows.Forms.Button();
            this.DOSBoxConfFileBrowseButton = new System.Windows.Forms.Button();
            this.DOSBoxPathBrowseButton = new System.Windows.Forms.Button();
            this.DOSBoxConfFileTextBox = new System.Windows.Forms.TextBox();
            this.DOSBoxLangFileTextBox = new System.Windows.Forms.TextBox();
            this.DOSBoxPathTextBox = new System.Windows.Forms.TextBox();
            this.GamesTabPage = new System.Windows.Forms.TabPage();
            this.GamesDirTextBox = new System.Windows.Forms.TextBox();
            this.GamesDirLabel = new System.Windows.Forms.Label();
            this.OtherOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.FullscreenCheckBox = new System.Windows.Forms.CheckBox();
            this.NoConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.QuitOnExitCheckBox = new System.Windows.Forms.CheckBox();
            this.GameAdditionalCommandsLabel = new System.Windows.Forms.Label();
            this.GameAdditionalCommandsTextBox = new System.Windows.Forms.TextBox();
            this.CDImageDirTextBox = new System.Windows.Forms.TextBox();
            this.CDImageDirLabel = new System.Windows.Forms.Label();
            this.BrowseGamesDirButton = new System.Windows.Forms.Button();
            this.BrowseCDImageDirButton = new System.Windows.Forms.Button();
            this.CategoriesTabPage = new System.Windows.Forms.TabPage();
            this.LargeViewModeSizeLabel = new System.Windows.Forms.Label();
            this.LargeViewModeSizeComboBox = new System.Windows.Forms.ComboBox();
            this.AllOfThemCheckBox = new System.Windows.Forms.CheckBox();
            this.LargeIconsRadioButton = new System.Windows.Forms.RadioButton();
            this.SmallIconsRadioButton = new System.Windows.Forms.RadioButton();
            this.TilesIconsRadioButton = new System.Windows.Forms.RadioButton();
            this.ListsIconsRadioButton = new System.Windows.Forms.RadioButton();
            this.DetailsIconsRadioButton = new System.Windows.Forms.RadioButton();
            this.IconsViewLabel = new System.Windows.Forms.Label();
            this.CategoriesListView = new System.Windows.Forms.ListView();
            this.SortByNameButton = new System.Windows.Forms.Button();
            this.MoveLastButton = new System.Windows.Forms.Button();
            this.MoveNextButton = new System.Windows.Forms.Button();
            this.MoveBackButton = new System.Windows.Forms.Button();
            this.MoveFirstButton = new System.Windows.Forms.Button();
            this.ConfigEditorTabPage = new System.Windows.Forms.TabPage();
            this.EditorBinaryPathTextBox = new System.Windows.Forms.TextBox();
            this.AddtionnalEditorParametersLabel = new System.Windows.Forms.Label();
            this.AdditionnalParametersTextBox = new System.Windows.Forms.TextBox();
            this.ConfigEditorPathLabel = new System.Windows.Forms.Label();
            this.BrowseForEditorButton = new System.Windows.Forms.Button();
            this.BehaviorTabPage = new System.Windows.Forms.TabPage();
            this.PromptGroupBox = new System.Windows.Forms.GroupBox();
            this.CategoyDeletePromptCheckBox = new System.Windows.Forms.CheckBox();
            this.GameDeletePromptCheckBox = new System.Windows.Forms.CheckBox();
            this.RememberGroupBox = new System.Windows.Forms.GroupBox();
            this.WindowSizeCheckBox = new System.Windows.Forms.CheckBox();
            this.WindowPositionCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowGroupBox = new System.Windows.Forms.GroupBox();
            this.ShowMenuBarCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowDetailsBarCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowToolBarCheckBox = new System.Windows.Forms.CheckBox();
            this.PortableModeCheckBox = new System.Windows.Forms.CheckBox();
            this.PrefsStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.Label();
            this.ReScanDirButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.FormCancelButton = new System.Windows.Forms.Button();
            this.PrefsTabControl.SuspendLayout();
            this.DOSBoxTabPage.SuspendLayout();
            this.GamesTabPage.SuspendLayout();
            this.OtherOptionsGroupBox.SuspendLayout();
            this.CategoriesTabPage.SuspendLayout();
            this.ConfigEditorTabPage.SuspendLayout();
            this.BehaviorTabPage.SuspendLayout();
            this.PromptGroupBox.SuspendLayout();
            this.RememberGroupBox.SuspendLayout();
            this.ShowGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrefsTabControl
            // 
            this.PrefsTabControl.Controls.Add(this.DOSBoxTabPage);
            this.PrefsTabControl.Controls.Add(this.GamesTabPage);
            this.PrefsTabControl.Controls.Add(this.CategoriesTabPage);
            this.PrefsTabControl.Controls.Add(this.ConfigEditorTabPage);
            this.PrefsTabControl.Controls.Add(this.BehaviorTabPage);
            this.PrefsTabControl.Location = new System.Drawing.Point(0, 0);
            this.PrefsTabControl.Name = "PrefsTabControl";
            this.PrefsTabControl.SelectedIndex = 0;
            this.PrefsTabControl.Size = new System.Drawing.Size(636, 197);
            this.PrefsTabControl.TabIndex = 1;
            // 
            // DOSBoxTabPage
            // 
            this.DOSBoxTabPage.BackColor = System.Drawing.Color.Transparent;
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxLangFileLabel);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxConfLabel);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxExecutableLabel);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxLangFileBrowseButton);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxConfFileBrowseButton);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxPathBrowseButton);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxConfFileTextBox);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxLangFileTextBox);
            this.DOSBoxTabPage.Controls.Add(this.DOSBoxPathTextBox);
            this.DOSBoxTabPage.Location = new System.Drawing.Point(4, 22);
            this.DOSBoxTabPage.Name = "DOSBoxTabPage";
            this.DOSBoxTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DOSBoxTabPage.Size = new System.Drawing.Size(628, 171);
            this.DOSBoxTabPage.TabIndex = 2;
            this.DOSBoxTabPage.Text = "DOSBox";
            this.DOSBoxTabPage.UseVisualStyleBackColor = true;
            // 
            // DOSBoxLangFileLabel
            // 
            this.DOSBoxLangFileLabel.AutoSize = true;
            this.DOSBoxLangFileLabel.Location = new System.Drawing.Point(3, 81);
            this.DOSBoxLangFileLabel.Name = "DOSBoxLangFileLabel";
            this.DOSBoxLangFileLabel.Size = new System.Drawing.Size(198, 13);
            this.DOSBoxLangFileLabel.TabIndex = 0;
            this.DOSBoxLangFileLabel.Text = "default DOSBox language file (optional) :";
            // 
            // DOSBoxConfLabel
            // 
            this.DOSBoxConfLabel.AutoSize = true;
            this.DOSBoxConfLabel.Location = new System.Drawing.Point(3, 42);
            this.DOSBoxConfLabel.Name = "DOSBoxConfLabel";
            this.DOSBoxConfLabel.Size = new System.Drawing.Size(303, 13);
            this.DOSBoxConfLabel.TabIndex = 0;
            this.DOSBoxConfLabel.Text = "default DOSBox configuration file (optional but recommended) :";
            // 
            // DOSBoxExecutableLabel
            // 
            this.DOSBoxExecutableLabel.AutoSize = true;
            this.DOSBoxExecutableLabel.Location = new System.Drawing.Point(3, 3);
            this.DOSBoxExecutableLabel.Name = "DOSBoxExecutableLabel";
            this.DOSBoxExecutableLabel.Size = new System.Drawing.Size(133, 13);
            this.DOSBoxExecutableLabel.TabIndex = 0;
            this.DOSBoxExecutableLabel.Text = "DOSBox executable path :";
            // 
            // DOSBoxLangFileBrowseButton
            // 
            this.DOSBoxLangFileBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.DOSBoxLangFileBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DOSBoxLangFileBrowseButton.Location = new System.Drawing.Point(601, 95);
            this.DOSBoxLangFileBrowseButton.Name = "DOSBoxLangFileBrowseButton";
            this.DOSBoxLangFileBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.DOSBoxLangFileBrowseButton.TabIndex = 7;
            this.DOSBoxLangFileBrowseButton.Text = "...";
            this.DOSBoxLangFileBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DOSBoxLangFileBrowseButton.UseVisualStyleBackColor = true;
            this.DOSBoxLangFileBrowseButton.Click += new System.EventHandler(this.DOSBoxLangFileBrowseButton_Click);
            // 
            // DOSBoxConfFileBrowseButton
            // 
            this.DOSBoxConfFileBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.DOSBoxConfFileBrowseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DOSBoxConfFileBrowseButton.Location = new System.Drawing.Point(601, 56);
            this.DOSBoxConfFileBrowseButton.Name = "DOSBoxConfFileBrowseButton";
            this.DOSBoxConfFileBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.DOSBoxConfFileBrowseButton.TabIndex = 5;
            this.DOSBoxConfFileBrowseButton.Text = "...";
            this.DOSBoxConfFileBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DOSBoxConfFileBrowseButton.UseVisualStyleBackColor = true;
            this.DOSBoxConfFileBrowseButton.Click += new System.EventHandler(this.DOSBoxConfFileBrowseButton_Click);
            // 
            // DOSBoxPathBrowseButton
            // 
            this.DOSBoxPathBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.DOSBoxPathBrowseButton.Location = new System.Drawing.Point(601, 17);
            this.DOSBoxPathBrowseButton.Name = "DOSBoxPathBrowseButton";
            this.DOSBoxPathBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.DOSBoxPathBrowseButton.TabIndex = 3;
            this.DOSBoxPathBrowseButton.Text = "...";
            this.DOSBoxPathBrowseButton.UseVisualStyleBackColor = true;
            this.DOSBoxPathBrowseButton.Click += new System.EventHandler(this.DOSBoxPathBrowseButton_Click);
            // 
            // DOSBoxConfFileTextBox
            // 
            this.DOSBoxConfFileTextBox.Location = new System.Drawing.Point(6, 58);
            this.DOSBoxConfFileTextBox.Name = "DOSBoxConfFileTextBox";
            this.DOSBoxConfFileTextBox.Size = new System.Drawing.Size(589, 20);
            this.DOSBoxConfFileTextBox.TabIndex = 4;
            // 
            // DOSBoxLangFileTextBox
            // 
            this.DOSBoxLangFileTextBox.Location = new System.Drawing.Point(6, 97);
            this.DOSBoxLangFileTextBox.Name = "DOSBoxLangFileTextBox";
            this.DOSBoxLangFileTextBox.Size = new System.Drawing.Size(589, 20);
            this.DOSBoxLangFileTextBox.TabIndex = 6;
            // 
            // DOSBoxPathTextBox
            // 
            this.DOSBoxPathTextBox.Location = new System.Drawing.Point(6, 19);
            this.DOSBoxPathTextBox.Name = "DOSBoxPathTextBox";
            this.DOSBoxPathTextBox.Size = new System.Drawing.Size(589, 20);
            this.DOSBoxPathTextBox.TabIndex = 2;
            // 
            // GamesTabPage
            // 
            this.GamesTabPage.BackColor = System.Drawing.Color.Transparent;
            this.GamesTabPage.Controls.Add(this.GamesDirTextBox);
            this.GamesTabPage.Controls.Add(this.GamesDirLabel);
            this.GamesTabPage.Controls.Add(this.OtherOptionsGroupBox);
            this.GamesTabPage.Controls.Add(this.GameAdditionalCommandsLabel);
            this.GamesTabPage.Controls.Add(this.GameAdditionalCommandsTextBox);
            this.GamesTabPage.Controls.Add(this.CDImageDirTextBox);
            this.GamesTabPage.Controls.Add(this.CDImageDirLabel);
            this.GamesTabPage.Controls.Add(this.BrowseGamesDirButton);
            this.GamesTabPage.Controls.Add(this.BrowseCDImageDirButton);
            this.GamesTabPage.Location = new System.Drawing.Point(4, 22);
            this.GamesTabPage.Name = "GamesTabPage";
            this.GamesTabPage.Size = new System.Drawing.Size(628, 171);
            this.GamesTabPage.TabIndex = 3;
            this.GamesTabPage.Text = "Games";
            this.GamesTabPage.UseVisualStyleBackColor = true;
            // 
            // GamesDirTextBox
            // 
            this.GamesDirTextBox.Location = new System.Drawing.Point(6, 25);
            this.GamesDirTextBox.Name = "GamesDirTextBox";
            this.GamesDirTextBox.Size = new System.Drawing.Size(589, 20);
            this.GamesDirTextBox.TabIndex = 10;
            // 
            // GamesDirLabel
            // 
            this.GamesDirLabel.AutoSize = true;
            this.GamesDirLabel.Location = new System.Drawing.Point(3, 9);
            this.GamesDirLabel.Name = "GamesDirLabel";
            this.GamesDirLabel.Size = new System.Drawing.Size(244, 13);
            this.GamesDirLabel.TabIndex = 0;
            this.GamesDirLabel.Text = "Default directory to open when looking for games :";
            // 
            // OtherOptionsGroupBox
            // 
            this.OtherOptionsGroupBox.Controls.Add(this.FullscreenCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.NoConsoleCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.QuitOnExitCheckBox);
            this.OtherOptionsGroupBox.Location = new System.Drawing.Point(6, 129);
            this.OtherOptionsGroupBox.Name = "OtherOptionsGroupBox";
            this.OtherOptionsGroupBox.Size = new System.Drawing.Size(256, 40);
            this.OtherOptionsGroupBox.TabIndex = 0;
            this.OtherOptionsGroupBox.TabStop = false;
            this.OtherOptionsGroupBox.Text = "Other options for each new game :";
            // 
            // FullscreenCheckBox
            // 
            this.FullscreenCheckBox.AutoSize = true;
            this.FullscreenCheckBox.Location = new System.Drawing.Point(90, 19);
            this.FullscreenCheckBox.Name = "FullscreenCheckBox";
            this.FullscreenCheckBox.Size = new System.Drawing.Size(71, 17);
            this.FullscreenCheckBox.TabIndex = 16;
            this.FullscreenCheckBox.Text = "fullscreen";
            this.FullscreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // NoConsoleCheckBox
            // 
            this.NoConsoleCheckBox.AutoSize = true;
            this.NoConsoleCheckBox.Location = new System.Drawing.Point(6, 19);
            this.NoConsoleCheckBox.Name = "NoConsoleCheckBox";
            this.NoConsoleCheckBox.Size = new System.Drawing.Size(78, 17);
            this.NoConsoleCheckBox.TabIndex = 15;
            this.NoConsoleCheckBox.Text = "no console";
            this.NoConsoleCheckBox.UseVisualStyleBackColor = true;
            // 
            // QuitOnExitCheckBox
            // 
            this.QuitOnExitCheckBox.AutoSize = true;
            this.QuitOnExitCheckBox.Location = new System.Drawing.Point(167, 19);
            this.QuitOnExitCheckBox.Name = "QuitOnExitCheckBox";
            this.QuitOnExitCheckBox.Size = new System.Drawing.Size(77, 17);
            this.QuitOnExitCheckBox.TabIndex = 17;
            this.QuitOnExitCheckBox.Text = "quit on exit";
            this.QuitOnExitCheckBox.UseVisualStyleBackColor = true;
            // 
            // GameAdditionalCommandsLabel
            // 
            this.GameAdditionalCommandsLabel.AutoSize = true;
            this.GameAdditionalCommandsLabel.Location = new System.Drawing.Point(3, 87);
            this.GameAdditionalCommandsLabel.Name = "GameAdditionalCommandsLabel";
            this.GameAdditionalCommandsLabel.Size = new System.Drawing.Size(328, 13);
            this.GameAdditionalCommandsLabel.TabIndex = 0;
            this.GameAdditionalCommandsLabel.Text = "Additional DOSBox commands for each new game (-c \"command\") :";
            // 
            // GameAdditionalCommandsTextBox
            // 
            this.GameAdditionalCommandsTextBox.Location = new System.Drawing.Point(6, 103);
            this.GameAdditionalCommandsTextBox.Name = "GameAdditionalCommandsTextBox";
            this.GameAdditionalCommandsTextBox.Size = new System.Drawing.Size(589, 20);
            this.GameAdditionalCommandsTextBox.TabIndex = 14;
            // 
            // CDImageDirTextBox
            // 
            this.CDImageDirTextBox.Location = new System.Drawing.Point(6, 64);
            this.CDImageDirTextBox.Name = "CDImageDirTextBox";
            this.CDImageDirTextBox.Size = new System.Drawing.Size(589, 20);
            this.CDImageDirTextBox.TabIndex = 12;
            // 
            // CDImageDirLabel
            // 
            this.CDImageDirLabel.AutoSize = true;
            this.CDImageDirLabel.Location = new System.Drawing.Point(3, 48);
            this.CDImageDirLabel.Name = "CDImageDirLabel";
            this.CDImageDirLabel.Size = new System.Drawing.Size(378, 13);
            this.CDImageDirLabel.TabIndex = 0;
            this.CDImageDirLabel.Text = "Default directory to open when looking for image files (such as CD image files) :" +
    "";
            // 
            // BrowseGamesDirButton
            // 
            this.BrowseGamesDirButton.Image = global::AmpShell.Properties.Resources.search;
            this.BrowseGamesDirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseGamesDirButton.Location = new System.Drawing.Point(601, 25);
            this.BrowseGamesDirButton.Name = "BrowseGamesDirButton";
            this.BrowseGamesDirButton.Size = new System.Drawing.Size(24, 23);
            this.BrowseGamesDirButton.TabIndex = 11;
            this.BrowseGamesDirButton.Text = "...";
            this.BrowseGamesDirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BrowseGamesDirButton.UseVisualStyleBackColor = true;
            this.BrowseGamesDirButton.Click += new System.EventHandler(this.BrowseGamesDirButton_Click);
            // 
            // BrowseCDImageDirButton
            // 
            this.BrowseCDImageDirButton.Image = global::AmpShell.Properties.Resources.search;
            this.BrowseCDImageDirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseCDImageDirButton.Location = new System.Drawing.Point(601, 64);
            this.BrowseCDImageDirButton.Name = "BrowseCDImageDirButton";
            this.BrowseCDImageDirButton.Size = new System.Drawing.Size(24, 23);
            this.BrowseCDImageDirButton.TabIndex = 13;
            this.BrowseCDImageDirButton.Text = "...";
            this.BrowseCDImageDirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BrowseCDImageDirButton.UseVisualStyleBackColor = true;
            this.BrowseCDImageDirButton.Click += new System.EventHandler(this.BrowseCDImageDirButton_Click);
            // 
            // CategoriesTabPage
            // 
            this.CategoriesTabPage.BackColor = System.Drawing.Color.Transparent;
            this.CategoriesTabPage.Controls.Add(this.LargeViewModeSizeLabel);
            this.CategoriesTabPage.Controls.Add(this.LargeViewModeSizeComboBox);
            this.CategoriesTabPage.Controls.Add(this.AllOfThemCheckBox);
            this.CategoriesTabPage.Controls.Add(this.LargeIconsRadioButton);
            this.CategoriesTabPage.Controls.Add(this.SmallIconsRadioButton);
            this.CategoriesTabPage.Controls.Add(this.TilesIconsRadioButton);
            this.CategoriesTabPage.Controls.Add(this.ListsIconsRadioButton);
            this.CategoriesTabPage.Controls.Add(this.DetailsIconsRadioButton);
            this.CategoriesTabPage.Controls.Add(this.IconsViewLabel);
            this.CategoriesTabPage.Controls.Add(this.CategoriesListView);
            this.CategoriesTabPage.Controls.Add(this.SortByNameButton);
            this.CategoriesTabPage.Controls.Add(this.MoveLastButton);
            this.CategoriesTabPage.Controls.Add(this.MoveNextButton);
            this.CategoriesTabPage.Controls.Add(this.MoveBackButton);
            this.CategoriesTabPage.Controls.Add(this.MoveFirstButton);
            this.CategoriesTabPage.Location = new System.Drawing.Point(4, 22);
            this.CategoriesTabPage.Name = "CategoriesTabPage";
            this.CategoriesTabPage.Size = new System.Drawing.Size(628, 171);
            this.CategoriesTabPage.TabIndex = 4;
            this.CategoriesTabPage.Text = "Categories";
            this.CategoriesTabPage.UseVisualStyleBackColor = true;
            // 
            // LargeViewModeSizeLabel
            // 
            this.LargeViewModeSizeLabel.AutoSize = true;
            this.LargeViewModeSizeLabel.Location = new System.Drawing.Point(3, 6);
            this.LargeViewModeSizeLabel.Name = "LargeViewModeSizeLabel";
            this.LargeViewModeSizeLabel.Size = new System.Drawing.Size(164, 13);
            this.LargeViewModeSizeLabel.TabIndex = 0;
            this.LargeViewModeSizeLabel.Text = "Large icon size for all categories :";
            // 
            // LargeViewModeSizeComboBox
            // 
            this.LargeViewModeSizeComboBox.FormattingEnabled = true;
            this.LargeViewModeSizeComboBox.Items.AddRange(new object[] {
            "48x48",
            "64x64",
            "80x80",
            "96x96",
            "112x112",
            "128x128",
            "144x144",
            "160x160",
            "176x176",
            "192x192",
            "208x208",
            "224x224",
            "240x240",
            "256x256"});
            this.LargeViewModeSizeComboBox.Location = new System.Drawing.Point(173, 3);
            this.LargeViewModeSizeComboBox.Name = "LargeViewModeSizeComboBox";
            this.LargeViewModeSizeComboBox.Size = new System.Drawing.Size(121, 21);
            this.LargeViewModeSizeComboBox.TabIndex = 32;
            // 
            // AllOfThemCheckBox
            // 
            this.AllOfThemCheckBox.AutoSize = true;
            this.AllOfThemCheckBox.Location = new System.Drawing.Point(544, 150);
            this.AllOfThemCheckBox.Name = "AllOfThemCheckBox";
            this.AllOfThemCheckBox.Size = new System.Drawing.Size(75, 17);
            this.AllOfThemCheckBox.TabIndex = 31;
            this.AllOfThemCheckBox.Text = "All of them";
            this.AllOfThemCheckBox.UseVisualStyleBackColor = true;
            // 
            // LargeIconsRadioButton
            // 
            this.LargeIconsRadioButton.AutoSize = true;
            this.LargeIconsRadioButton.Location = new System.Drawing.Point(262, 150);
            this.LargeIconsRadioButton.Name = "LargeIconsRadioButton";
            this.LargeIconsRadioButton.Size = new System.Drawing.Size(52, 17);
            this.LargeIconsRadioButton.TabIndex = 26;
            this.LargeIconsRadioButton.TabStop = true;
            this.LargeIconsRadioButton.Text = "Large";
            this.LargeIconsRadioButton.UseVisualStyleBackColor = true;
            this.LargeIconsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LargeIconsRadioButton_MouseClick);
            // 
            // SmallIconsRadioButton
            // 
            this.SmallIconsRadioButton.AutoSize = true;
            this.SmallIconsRadioButton.Location = new System.Drawing.Point(320, 150);
            this.SmallIconsRadioButton.Name = "SmallIconsRadioButton";
            this.SmallIconsRadioButton.Size = new System.Drawing.Size(50, 17);
            this.SmallIconsRadioButton.TabIndex = 27;
            this.SmallIconsRadioButton.TabStop = true;
            this.SmallIconsRadioButton.Text = "Small";
            this.SmallIconsRadioButton.UseVisualStyleBackColor = true;
            this.SmallIconsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SmallIconsRadioButton_MouseClick);
            // 
            // TilesIconsRadioButton
            // 
            this.TilesIconsRadioButton.AutoSize = true;
            this.TilesIconsRadioButton.Location = new System.Drawing.Point(376, 150);
            this.TilesIconsRadioButton.Name = "TilesIconsRadioButton";
            this.TilesIconsRadioButton.Size = new System.Drawing.Size(47, 17);
            this.TilesIconsRadioButton.TabIndex = 28;
            this.TilesIconsRadioButton.TabStop = true;
            this.TilesIconsRadioButton.Text = "Tiles";
            this.TilesIconsRadioButton.UseVisualStyleBackColor = true;
            this.TilesIconsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TilesIconsRadioButton_MouseClick);
            // 
            // ListsIconsRadioButton
            // 
            this.ListsIconsRadioButton.AutoSize = true;
            this.ListsIconsRadioButton.Location = new System.Drawing.Point(429, 150);
            this.ListsIconsRadioButton.Name = "ListsIconsRadioButton";
            this.ListsIconsRadioButton.Size = new System.Drawing.Size(46, 17);
            this.ListsIconsRadioButton.TabIndex = 29;
            this.ListsIconsRadioButton.TabStop = true;
            this.ListsIconsRadioButton.Text = "Lists";
            this.ListsIconsRadioButton.UseVisualStyleBackColor = true;
            this.ListsIconsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListsIconsRadioButton_MouseClick);
            // 
            // DetailsIconsRadioButton
            // 
            this.DetailsIconsRadioButton.AutoSize = true;
            this.DetailsIconsRadioButton.Location = new System.Drawing.Point(481, 150);
            this.DetailsIconsRadioButton.Name = "DetailsIconsRadioButton";
            this.DetailsIconsRadioButton.Size = new System.Drawing.Size(57, 17);
            this.DetailsIconsRadioButton.TabIndex = 30;
            this.DetailsIconsRadioButton.TabStop = true;
            this.DetailsIconsRadioButton.Text = "Details";
            this.DetailsIconsRadioButton.UseVisualStyleBackColor = true;
            this.DetailsIconsRadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DetailsIconsRadioButton_MouseClick);
            // 
            // IconsViewLabel
            // 
            this.IconsViewLabel.AutoSize = true;
            this.IconsViewLabel.Location = new System.Drawing.Point(184, 150);
            this.IconsViewLabel.Name = "IconsViewLabel";
            this.IconsViewLabel.Size = new System.Drawing.Size(72, 13);
            this.IconsViewLabel.TabIndex = 0;
            this.IconsViewLabel.Text = "Default view :";
            // 
            // CategoriesListView
            // 
            this.CategoriesListView.LabelWrap = false;
            this.CategoriesListView.Location = new System.Drawing.Point(4, 30);
            this.CategoriesListView.Name = "CategoriesListView";
            this.CategoriesListView.Size = new System.Drawing.Size(621, 112);
            this.CategoriesListView.TabIndex = 19;
            this.CategoriesListView.UseCompatibleStateImageBehavior = false;
            this.CategoriesListView.View = System.Windows.Forms.View.List;
            // 
            // SortByNameButton
            // 
            this.SortByNameButton.Image = global::AmpShell.Properties.Resources.SortHS;
            this.SortByNameButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SortByNameButton.Location = new System.Drawing.Point(128, 145);
            this.SortByNameButton.Name = "SortByNameButton";
            this.SortByNameButton.Size = new System.Drawing.Size(50, 23);
            this.SortByNameButton.TabIndex = 24;
            this.SortByNameButton.Text = "Sort";
            this.SortByNameButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SortByNameButton.UseVisualStyleBackColor = true;
            this.SortByNameButton.Click += new System.EventHandler(this.SortByNameButton_Click);
            // 
            // MoveLastButton
            // 
            this.MoveLastButton.Image = global::AmpShell.Properties.Resources.MoveLast;
            this.MoveLastButton.Location = new System.Drawing.Point(98, 145);
            this.MoveLastButton.Name = "MoveLastButton";
            this.MoveLastButton.Size = new System.Drawing.Size(24, 23);
            this.MoveLastButton.TabIndex = 23;
            this.MoveLastButton.UseVisualStyleBackColor = true;
            this.MoveLastButton.Click += new System.EventHandler(this.MoveLastButton_Click);
            // 
            // MoveNextButton
            // 
            this.MoveNextButton.Image = global::AmpShell.Properties.Resources.MoveNext;
            this.MoveNextButton.Location = new System.Drawing.Point(68, 145);
            this.MoveNextButton.Name = "MoveNextButton";
            this.MoveNextButton.Size = new System.Drawing.Size(24, 23);
            this.MoveNextButton.TabIndex = 22;
            this.MoveNextButton.UseVisualStyleBackColor = true;
            this.MoveNextButton.Click += new System.EventHandler(this.MoveNextButton_Click);
            // 
            // MoveBackButton
            // 
            this.MoveBackButton.Image = global::AmpShell.Properties.Resources.MoveBack;
            this.MoveBackButton.Location = new System.Drawing.Point(38, 145);
            this.MoveBackButton.Name = "MoveBackButton";
            this.MoveBackButton.Size = new System.Drawing.Size(24, 23);
            this.MoveBackButton.TabIndex = 21;
            this.MoveBackButton.UseVisualStyleBackColor = true;
            this.MoveBackButton.Click += new System.EventHandler(this.MoveBackButton_Click);
            // 
            // MoveFirstButton
            // 
            this.MoveFirstButton.Image = global::AmpShell.Properties.Resources.MoveFirst;
            this.MoveFirstButton.Location = new System.Drawing.Point(8, 145);
            this.MoveFirstButton.Name = "MoveFirstButton";
            this.MoveFirstButton.Size = new System.Drawing.Size(24, 23);
            this.MoveFirstButton.TabIndex = 20;
            this.MoveFirstButton.UseVisualStyleBackColor = true;
            this.MoveFirstButton.Click += new System.EventHandler(this.MoveFirstButton_Click);
            // 
            // ConfigEditorTabPage
            // 
            this.ConfigEditorTabPage.BackColor = System.Drawing.Color.Transparent;
            this.ConfigEditorTabPage.Controls.Add(this.EditorBinaryPathTextBox);
            this.ConfigEditorTabPage.Controls.Add(this.AddtionnalEditorParametersLabel);
            this.ConfigEditorTabPage.Controls.Add(this.AdditionnalParametersTextBox);
            this.ConfigEditorTabPage.Controls.Add(this.ConfigEditorPathLabel);
            this.ConfigEditorTabPage.Controls.Add(this.BrowseForEditorButton);
            this.ConfigEditorTabPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigEditorTabPage.Name = "ConfigEditorTabPage";
            this.ConfigEditorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigEditorTabPage.Size = new System.Drawing.Size(628, 171);
            this.ConfigEditorTabPage.TabIndex = 0;
            this.ConfigEditorTabPage.Text = "Config Editor";
            this.ConfigEditorTabPage.UseVisualStyleBackColor = true;
            // 
            // EditorBinaryPathTextBox
            // 
            this.EditorBinaryPathTextBox.Location = new System.Drawing.Point(6, 19);
            this.EditorBinaryPathTextBox.Name = "EditorBinaryPathTextBox";
            this.EditorBinaryPathTextBox.Size = new System.Drawing.Size(586, 20);
            this.EditorBinaryPathTextBox.TabIndex = 33;
            // 
            // AddtionnalEditorParametersLabel
            // 
            this.AddtionnalEditorParametersLabel.AutoSize = true;
            this.AddtionnalEditorParametersLabel.Location = new System.Drawing.Point(3, 42);
            this.AddtionnalEditorParametersLabel.Name = "AddtionnalEditorParametersLabel";
            this.AddtionnalEditorParametersLabel.Size = new System.Drawing.Size(261, 13);
            this.AddtionnalEditorParametersLabel.TabIndex = 0;
            this.AddtionnalEditorParametersLabel.Text = "Additional parameters to use with the editor (optional) :";
            // 
            // AdditionnalParametersTextBox
            // 
            this.AdditionnalParametersTextBox.Location = new System.Drawing.Point(6, 58);
            this.AdditionnalParametersTextBox.Name = "AdditionnalParametersTextBox";
            this.AdditionnalParametersTextBox.Size = new System.Drawing.Size(586, 20);
            this.AdditionnalParametersTextBox.TabIndex = 35;
            // 
            // ConfigEditorPathLabel
            // 
            this.ConfigEditorPathLabel.AutoSize = true;
            this.ConfigEditorPathLabel.Location = new System.Drawing.Point(3, 3);
            this.ConfigEditorPathLabel.Name = "ConfigEditorPathLabel";
            this.ConfigEditorPathLabel.Size = new System.Drawing.Size(327, 13);
            this.ConfigEditorPathLabel.TabIndex = 0;
            this.ConfigEditorPathLabel.Text = "Open DOSBox configuration files with the following editor (optional) :";
            // 
            // BrowseForEditorButton
            // 
            this.BrowseForEditorButton.Image = global::AmpShell.Properties.Resources.search;
            this.BrowseForEditorButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BrowseForEditorButton.Location = new System.Drawing.Point(598, 17);
            this.BrowseForEditorButton.Name = "BrowseForEditorButton";
            this.BrowseForEditorButton.Size = new System.Drawing.Size(24, 23);
            this.BrowseForEditorButton.TabIndex = 34;
            this.BrowseForEditorButton.Text = "...";
            this.BrowseForEditorButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BrowseForEditorButton.UseVisualStyleBackColor = true;
            this.BrowseForEditorButton.Click += new System.EventHandler(this.BrowseForEditorButton_Click);
            // 
            // BehaviorTabPage
            // 
            this.BehaviorTabPage.BackColor = System.Drawing.Color.Transparent;
            this.BehaviorTabPage.Controls.Add(this.PromptGroupBox);
            this.BehaviorTabPage.Controls.Add(this.RememberGroupBox);
            this.BehaviorTabPage.Controls.Add(this.ShowGroupBox);
            this.BehaviorTabPage.Location = new System.Drawing.Point(4, 22);
            this.BehaviorTabPage.Name = "BehaviorTabPage";
            this.BehaviorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BehaviorTabPage.Size = new System.Drawing.Size(628, 171);
            this.BehaviorTabPage.TabIndex = 1;
            this.BehaviorTabPage.Text = "Behavior";
            this.BehaviorTabPage.UseVisualStyleBackColor = true;
            // 
            // PromptGroupBox
            // 
            this.PromptGroupBox.Controls.Add(this.CategoyDeletePromptCheckBox);
            this.PromptGroupBox.Controls.Add(this.GameDeletePromptCheckBox);
            this.PromptGroupBox.Location = new System.Drawing.Point(214, 6);
            this.PromptGroupBox.Name = "PromptGroupBox";
            this.PromptGroupBox.Size = new System.Drawing.Size(200, 72);
            this.PromptGroupBox.TabIndex = 0;
            this.PromptGroupBox.TabStop = false;
            this.PromptGroupBox.Text = "Prompt before...";
            // 
            // CategoyDeletePromptCheckBox
            // 
            this.CategoyDeletePromptCheckBox.AutoSize = true;
            this.CategoyDeletePromptCheckBox.Location = new System.Drawing.Point(6, 19);
            this.CategoyDeletePromptCheckBox.Name = "CategoyDeletePromptCheckBox";
            this.CategoyDeletePromptCheckBox.Size = new System.Drawing.Size(185, 17);
            this.CategoyDeletePromptCheckBox.TabIndex = 38;
            this.CategoyDeletePromptCheckBox.Text = "Prompt before deleting a category";
            this.CategoyDeletePromptCheckBox.UseVisualStyleBackColor = true;
            // 
            // GameDeletePromptCheckBox
            // 
            this.GameDeletePromptCheckBox.AutoSize = true;
            this.GameDeletePromptCheckBox.Location = new System.Drawing.Point(6, 42);
            this.GameDeletePromptCheckBox.Name = "GameDeletePromptCheckBox";
            this.GameDeletePromptCheckBox.Size = new System.Drawing.Size(170, 17);
            this.GameDeletePromptCheckBox.TabIndex = 39;
            this.GameDeletePromptCheckBox.Text = "Prompt before deleting a game";
            this.GameDeletePromptCheckBox.UseVisualStyleBackColor = true;
            // 
            // RememberGroupBox
            // 
            this.RememberGroupBox.Controls.Add(this.WindowSizeCheckBox);
            this.RememberGroupBox.Controls.Add(this.WindowPositionCheckBox);
            this.RememberGroupBox.Location = new System.Drawing.Point(8, 6);
            this.RememberGroupBox.Name = "RememberGroupBox";
            this.RememberGroupBox.Size = new System.Drawing.Size(200, 72);
            this.RememberGroupBox.TabIndex = 0;
            this.RememberGroupBox.TabStop = false;
            this.RememberGroupBox.Text = "Remember...";
            // 
            // WindowSizeCheckBox
            // 
            this.WindowSizeCheckBox.AutoSize = true;
            this.WindowSizeCheckBox.Location = new System.Drawing.Point(6, 19);
            this.WindowSizeCheckBox.Name = "WindowSizeCheckBox";
            this.WindowSizeCheckBox.Size = new System.Drawing.Size(86, 17);
            this.WindowSizeCheckBox.TabIndex = 36;
            this.WindowSizeCheckBox.Text = "Window size";
            this.WindowSizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // WindowPositionCheckBox
            // 
            this.WindowPositionCheckBox.AutoSize = true;
            this.WindowPositionCheckBox.Location = new System.Drawing.Point(6, 42);
            this.WindowPositionCheckBox.Name = "WindowPositionCheckBox";
            this.WindowPositionCheckBox.Size = new System.Drawing.Size(104, 17);
            this.WindowPositionCheckBox.TabIndex = 37;
            this.WindowPositionCheckBox.Text = "Window position";
            this.WindowPositionCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowGroupBox
            // 
            this.ShowGroupBox.Controls.Add(this.ShowMenuBarCheckBox);
            this.ShowGroupBox.Controls.Add(this.ShowDetailsBarCheckBox);
            this.ShowGroupBox.Controls.Add(this.ShowToolBarCheckBox);
            this.ShowGroupBox.Location = new System.Drawing.Point(8, 81);
            this.ShowGroupBox.Name = "ShowGroupBox";
            this.ShowGroupBox.Size = new System.Drawing.Size(200, 84);
            this.ShowGroupBox.TabIndex = 0;
            this.ShowGroupBox.TabStop = false;
            this.ShowGroupBox.Text = "Show...";
            // 
            // ShowMenuBarCheckBox
            // 
            this.ShowMenuBarCheckBox.AutoSize = true;
            this.ShowMenuBarCheckBox.Location = new System.Drawing.Point(6, 19);
            this.ShowMenuBarCheckBox.Name = "ShowMenuBarCheckBox";
            this.ShowMenuBarCheckBox.Size = new System.Drawing.Size(71, 17);
            this.ShowMenuBarCheckBox.TabIndex = 40;
            this.ShowMenuBarCheckBox.Text = "Menu bar";
            this.ShowMenuBarCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowDetailsBarCheckBox
            // 
            this.ShowDetailsBarCheckBox.AutoSize = true;
            this.ShowDetailsBarCheckBox.Location = new System.Drawing.Point(6, 65);
            this.ShowDetailsBarCheckBox.Name = "ShowDetailsBarCheckBox";
            this.ShowDetailsBarCheckBox.Size = new System.Drawing.Size(76, 17);
            this.ShowDetailsBarCheckBox.TabIndex = 42;
            this.ShowDetailsBarCheckBox.Text = "Details bar";
            this.ShowDetailsBarCheckBox.UseVisualStyleBackColor = true;
            // 
            // ShowToolBarCheckBox
            // 
            this.ShowToolBarCheckBox.AutoSize = true;
            this.ShowToolBarCheckBox.Location = new System.Drawing.Point(6, 42);
            this.ShowToolBarCheckBox.Name = "ShowToolBarCheckBox";
            this.ShowToolBarCheckBox.Size = new System.Drawing.Size(65, 17);
            this.ShowToolBarCheckBox.TabIndex = 41;
            this.ShowToolBarCheckBox.Text = "Tool bar";
            this.ShowToolBarCheckBox.UseVisualStyleBackColor = true;
            // 
            // PortableModeCheckBox
            // 
            this.PortableModeCheckBox.AutoSize = true;
            this.PortableModeCheckBox.Location = new System.Drawing.Point(0, 205);
            this.PortableModeCheckBox.Name = "PortableModeCheckBox";
            this.PortableModeCheckBox.Size = new System.Drawing.Size(95, 17);
            this.PortableModeCheckBox.TabIndex = 8;
            this.PortableModeCheckBox.Text = "Portable Mode";
            this.PortableModeCheckBox.UseVisualStyleBackColor = true;
            this.PortableModeCheckBox.CheckedChanged += new System.EventHandler(this.PortableModeCheckBox_CheckedChanged);
            // 
            // PrefsStatusStrip
            // 
            this.PrefsStatusStrip.Location = new System.Drawing.Point(0, 225);
            this.PrefsStatusStrip.Name = "PrefsStatusStrip";
            this.PrefsStatusStrip.Size = new System.Drawing.Size(634, 22);
            this.PrefsStatusStrip.TabIndex = 15;
            this.PrefsStatusStrip.Text = "Portable Mode : inactive";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.AutoSize = true;
            this.StatusStripLabel.Location = new System.Drawing.Point(0, 233);
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusStripLabel.TabIndex = 16;
            // 
            // ReScanDirButton
            // 
            this.ReScanDirButton.Enabled = false;
            this.ReScanDirButton.Image = global::AmpShell.Properties.Resources.autoList;
            this.ReScanDirButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReScanDirButton.Location = new System.Drawing.Point(101, 199);
            this.ReScanDirButton.Name = "ReScanDirButton";
            this.ReScanDirButton.Size = new System.Drawing.Size(165, 23);
            this.ReScanDirButton.TabIndex = 9;
            this.ReScanDirButton.Text = "Re-scan AmpShell\'s directory";
            this.ReScanDirButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ReScanDirButton.UseVisualStyleBackColor = true;
            this.ReScanDirButton.Click += new System.EventHandler(this.ReScanDirButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Image = global::AmpShell.Properties.Resources.saveHS;
            this.OKButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OKButton.Location = new System.Drawing.Point(449, 203);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(102, 23);
            this.OKButton.TabIndex = 43;
            this.OKButton.Text = "&Save and apply";
            this.OKButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OK_Click);
            // 
            // FormCancelButton
            // 
            this.FormCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.FormCancelButton.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.FormCancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FormCancelButton.Location = new System.Drawing.Point(554, 203);
            this.FormCancelButton.Name = "FormCancelButton";
            this.FormCancelButton.Size = new System.Drawing.Size(80, 23);
            this.FormCancelButton.TabIndex = 44;
            this.FormCancelButton.Text = "&Don\'t save";
            this.FormCancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FormCancelButton.UseVisualStyleBackColor = true;
            this.FormCancelButton.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Main_Prefs
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.FormCancelButton;
            this.ClientSize = new System.Drawing.Size(634, 247);
            this.Controls.Add(this.ReScanDirButton);
            this.Controls.Add(this.StatusStripLabel);
            this.Controls.Add(this.PrefsStatusStrip);
            this.Controls.Add(this.PortableModeCheckBox);
            this.Controls.Add(this.PrefsTabControl);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.FormCancelButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main_Prefs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.Main_Prefs_Load);
            this.PrefsTabControl.ResumeLayout(false);
            this.DOSBoxTabPage.ResumeLayout(false);
            this.DOSBoxTabPage.PerformLayout();
            this.GamesTabPage.ResumeLayout(false);
            this.GamesTabPage.PerformLayout();
            this.OtherOptionsGroupBox.ResumeLayout(false);
            this.OtherOptionsGroupBox.PerformLayout();
            this.CategoriesTabPage.ResumeLayout(false);
            this.CategoriesTabPage.PerformLayout();
            this.ConfigEditorTabPage.ResumeLayout(false);
            this.ConfigEditorTabPage.PerformLayout();
            this.BehaviorTabPage.ResumeLayout(false);
            this.PromptGroupBox.ResumeLayout(false);
            this.PromptGroupBox.PerformLayout();
            this.RememberGroupBox.ResumeLayout(false);
            this.RememberGroupBox.PerformLayout();
            this.ShowGroupBox.ResumeLayout(false);
            this.ShowGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FormCancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TabControl PrefsTabControl;
        private System.Windows.Forms.TabPage ConfigEditorTabPage;
        private System.Windows.Forms.TextBox EditorBinaryPathTextBox;
        private System.Windows.Forms.Label AddtionnalEditorParametersLabel;
        private System.Windows.Forms.Button BrowseForEditorButton;
        private System.Windows.Forms.TextBox AdditionnalParametersTextBox;
        private System.Windows.Forms.Label ConfigEditorPathLabel;
        private System.Windows.Forms.TabPage BehaviorTabPage;
        private System.Windows.Forms.CheckBox WindowPositionCheckBox;
        private System.Windows.Forms.CheckBox WindowSizeCheckBox;
        private System.Windows.Forms.TabPage DOSBoxTabPage;
        private System.Windows.Forms.Button DOSBoxLangFileBrowseButton;
        private System.Windows.Forms.Button DOSBoxConfFileBrowseButton;
        private System.Windows.Forms.Button DOSBoxPathBrowseButton;
        private System.Windows.Forms.TextBox DOSBoxConfFileTextBox;
        private System.Windows.Forms.TextBox DOSBoxLangFileTextBox;
        private System.Windows.Forms.TextBox DOSBoxPathTextBox;
        private System.Windows.Forms.Label DOSBoxLangFileLabel;
        private System.Windows.Forms.Label DOSBoxConfLabel;
        private System.Windows.Forms.Label DOSBoxExecutableLabel;
        private System.Windows.Forms.TabPage GamesTabPage;
        private System.Windows.Forms.TabPage CategoriesTabPage;
        private System.Windows.Forms.Button MoveLastButton;
        private System.Windows.Forms.Button MoveNextButton;
        private System.Windows.Forms.Button MoveBackButton;
        private System.Windows.Forms.Button MoveFirstButton;
        private System.Windows.Forms.Button SortByNameButton;
        private System.Windows.Forms.Label GameAdditionalCommandsLabel;
        private System.Windows.Forms.TextBox GameAdditionalCommandsTextBox;
        private System.Windows.Forms.TextBox CDImageDirTextBox;
        private System.Windows.Forms.Button BrowseCDImageDirButton;
        private System.Windows.Forms.Label CDImageDirLabel;
        private System.Windows.Forms.GroupBox OtherOptionsGroupBox;
        private System.Windows.Forms.CheckBox FullscreenCheckBox;
        private System.Windows.Forms.CheckBox NoConsoleCheckBox;
        private System.Windows.Forms.CheckBox QuitOnExitCheckBox;
        private System.Windows.Forms.TextBox GamesDirTextBox;
        private System.Windows.Forms.Button BrowseGamesDirButton;
        private System.Windows.Forms.Label GamesDirLabel;
        private System.Windows.Forms.ListView CategoriesListView;
        private System.Windows.Forms.GroupBox ShowGroupBox;
        private System.Windows.Forms.CheckBox ShowMenuBarCheckBox;
        private System.Windows.Forms.CheckBox ShowDetailsBarCheckBox;
        private System.Windows.Forms.CheckBox ShowToolBarCheckBox;
        private System.Windows.Forms.GroupBox RememberGroupBox;
        private System.Windows.Forms.GroupBox PromptGroupBox;
        private System.Windows.Forms.CheckBox CategoyDeletePromptCheckBox;
        private System.Windows.Forms.CheckBox GameDeletePromptCheckBox;
        private System.Windows.Forms.Label IconsViewLabel;
        private System.Windows.Forms.RadioButton LargeIconsRadioButton;
        private System.Windows.Forms.RadioButton SmallIconsRadioButton;
        private System.Windows.Forms.RadioButton TilesIconsRadioButton;
        private System.Windows.Forms.RadioButton ListsIconsRadioButton;
        private System.Windows.Forms.RadioButton DetailsIconsRadioButton;
        private System.Windows.Forms.CheckBox AllOfThemCheckBox;
        private System.Windows.Forms.CheckBox PortableModeCheckBox;
        private System.Windows.Forms.StatusStrip PrefsStatusStrip;
        private System.Windows.Forms.Label StatusStripLabel;
        private System.Windows.Forms.Button ReScanDirButton;
        private System.Windows.Forms.Label LargeViewModeSizeLabel;
        private System.Windows.Forms.ComboBox LargeViewModeSizeComboBox;
    }
}