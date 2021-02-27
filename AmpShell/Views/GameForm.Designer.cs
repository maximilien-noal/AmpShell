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
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.ConfigTabControl = new System.Windows.Forms.TabControl();
            this.BasicTabPage = new System.Windows.Forms.TabPage();
            this.ReleaseDateLabel = new System.Windows.Forms.Label();
            this.GameReleaseDatePicker = new System.Windows.Forms.DateTimePicker();
            this.MountingOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.DiscLabelTextBox = new System.Windows.Forms.TextBox();
            this.DiscLabelLabel = new System.Windows.Forms.Label();
            this.NoneRadioButton = new System.Windows.Forms.RadioButton();
            this.UseIOCTLRadioButton = new System.Windows.Forms.RadioButton();
            this.IsAFloppyDiskRadioButton = new System.Windows.Forms.RadioButton();
            this.ResetIconButton = new System.Windows.Forms.Button();
            this.GameIconPictureBox = new System.Windows.Forms.PictureBox();
            this.GameCDDirBrowseButton = new System.Windows.Forms.Button();
            this.GameSetupBrowseButton = new System.Windows.Forms.Button();
            this.GameSetupLabel = new System.Windows.Forms.Label();
            this.GameSetupTextBox = new System.Windows.Forms.TextBox();
            this.GameDirectoryBrowseButton = new System.Windows.Forms.Button();
            this.GameDirectoryLabel = new System.Windows.Forms.Label();
            this.GameDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.OtherOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.FullscreenCheckBox = new System.Windows.Forms.CheckBox();
            this.NoConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.QuitOnExitCheckBox = new System.Windows.Forms.CheckBox();
            this.NoConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.GameCDPathBrowseButton = new System.Windows.Forms.Button();
            this.GameCDPathLabel = new System.Windows.Forms.Label();
            this.GameCDPathTextBox = new System.Windows.Forms.TextBox();
            this.GameNameTextbox = new System.Windows.Forms.TextBox();
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.GameCustomConfigurationBrowseButton = new System.Windows.Forms.Button();
            this.GameCustomConfigurationLabel = new System.Windows.Forms.Label();
            this.GameCustomConfigurationTextbox = new System.Windows.Forms.TextBox();
            this.GameLocationBrowseButton = new System.Windows.Forms.Button();
            this.GameLocationLabel = new System.Windows.Forms.Label();
            this.GameLocationTextbox = new System.Windows.Forms.TextBox();
            this.AdvancedTabPage = new System.Windows.Forms.TabPage();
            this.DOSBoxWorkingDirButton = new System.Windows.Forms.Button();
            this.DOSBoxWorkingDirLabel = new System.Windows.Forms.Label();
            this.DOSBoxWorkingDirTextBox = new System.Windows.Forms.TextBox();
            this.DontUseDOSBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.GameAdditionalCommandsLabel = new System.Windows.Forms.Label();
            this.GameAdditionalCommandsTextBox = new System.Windows.Forms.TextBox();
            this.AlternateDOSBoxLocationBrowseButton = new System.Windows.Forms.Button();
            this.AlternateDOSBoxLocationTextbox = new System.Windows.Forms.TextBox();
            this.AlternateDOSBoxLocationLabel = new System.Windows.Forms.Label();
            this.NotesTabPage = new System.Windows.Forms.TabPage();
            this.NotesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.AdditionnalCommandsTipLabel = new System.Windows.Forms.Label();
            this.ConfigTabControl.SuspendLayout();
            this.BasicTabPage.SuspendLayout();
            this.MountingOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).BeginInit();
            this.OtherOptionsGroupBox.SuspendLayout();
            this.AdvancedTabPage.SuspendLayout();
            this.NotesTabPage.SuspendLayout();
            this.SuspendLayout();
            //
            // ConfigTabControl
            //
            this.ConfigTabControl.Controls.Add(this.BasicTabPage);
            this.ConfigTabControl.Controls.Add(this.AdvancedTabPage);
            this.ConfigTabControl.Controls.Add(this.NotesTabPage);
            this.ConfigTabControl.Location = new System.Drawing.Point(0, 0);
            this.ConfigTabControl.Name = "ConfigTabControl";
            this.ConfigTabControl.SelectedIndex = 0;
            this.ConfigTabControl.Size = new System.Drawing.Size(464, 576);
            this.ConfigTabControl.TabIndex = 0;
            //
            // BasicTabPage
            //
            this.BasicTabPage.Controls.Add(this.ReleaseDateLabel);
            this.BasicTabPage.Controls.Add(this.GameReleaseDatePicker);
            this.BasicTabPage.Controls.Add(this.MountingOptionsGroupBox);
            this.BasicTabPage.Controls.Add(this.ResetIconButton);
            this.BasicTabPage.Controls.Add(this.GameIconPictureBox);
            this.BasicTabPage.Controls.Add(this.GameCDDirBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameSetupBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameSetupLabel);
            this.BasicTabPage.Controls.Add(this.GameSetupTextBox);
            this.BasicTabPage.Controls.Add(this.GameDirectoryBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameDirectoryLabel);
            this.BasicTabPage.Controls.Add(this.GameDirectoryTextbox);
            this.BasicTabPage.Controls.Add(this.OtherOptionsGroupBox);
            this.BasicTabPage.Controls.Add(this.NoConfigCheckBox);
            this.BasicTabPage.Controls.Add(this.GameCDPathBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameCDPathLabel);
            this.BasicTabPage.Controls.Add(this.GameCDPathTextBox);
            this.BasicTabPage.Controls.Add(this.GameNameTextbox);
            this.BasicTabPage.Controls.Add(this.GameNameLabel);
            this.BasicTabPage.Controls.Add(this.GameCustomConfigurationBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameCustomConfigurationLabel);
            this.BasicTabPage.Controls.Add(this.GameCustomConfigurationTextbox);
            this.BasicTabPage.Controls.Add(this.GameLocationBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameLocationLabel);
            this.BasicTabPage.Controls.Add(this.GameLocationTextbox);
            this.BasicTabPage.Location = new System.Drawing.Point(4, 22);
            this.BasicTabPage.Name = "BasicTabPage";
            this.BasicTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BasicTabPage.Size = new System.Drawing.Size(456, 550);
            this.BasicTabPage.TabIndex = 0;
            this.BasicTabPage.Text = "Basic Configuration";
            this.BasicTabPage.UseVisualStyleBackColor = true;
            //
            // ReleaseDateLabel
            //
            this.ReleaseDateLabel.AutoSize = true;
            this.ReleaseDateLabel.Image = global::AmpShell.Properties.Resources.DateOrTimePicker_675;
            this.ReleaseDateLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReleaseDateLabel.Location = new System.Drawing.Point(3, 47);
            this.ReleaseDateLabel.Name = "ReleaseDateLabel";
            this.ReleaseDateLabel.Size = new System.Drawing.Size(140, 13);
            this.ReleaseDateLabel.TabIndex = 60;
            this.ReleaseDateLabel.Text = "      Release date (optional) :";
            //
            // GameReleaseDatePicker
            //
            this.GameReleaseDatePicker.Location = new System.Drawing.Point(7, 65);
            this.GameReleaseDatePicker.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.GameReleaseDatePicker.Name = "GameReleaseDatePicker";
            this.GameReleaseDatePicker.Size = new System.Drawing.Size(200, 20);
            this.GameReleaseDatePicker.TabIndex = 59;
            //
            // MountingOptionsGroupBox
            //
            this.MountingOptionsGroupBox.Controls.Add(this.DiscLabelTextBox);
            this.MountingOptionsGroupBox.Controls.Add(this.DiscLabelLabel);
            this.MountingOptionsGroupBox.Controls.Add(this.NoneRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.UseIOCTLRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.IsAFloppyDiskRadioButton);
            this.MountingOptionsGroupBox.Enabled = false;
            this.MountingOptionsGroupBox.Location = new System.Drawing.Point(8, 349);
            this.MountingOptionsGroupBox.Name = "MountingOptionsGroupBox";
            this.MountingOptionsGroupBox.Size = new System.Drawing.Size(438, 139);
            this.MountingOptionsGroupBox.TabIndex = 51;
            this.MountingOptionsGroupBox.TabStop = false;
            this.MountingOptionsGroupBox.Text = "Mounting options";
            //
            // DiscLabelTextBox
            //
            this.DiscLabelTextBox.Location = new System.Drawing.Point(6, 113);
            this.DiscLabelTextBox.Name = "DiscLabelTextBox";
            this.DiscLabelTextBox.Size = new System.Drawing.Size(429, 20);
            this.DiscLabelTextBox.TabIndex = 35;
            //
            // DiscLabelLabel
            //
            this.DiscLabelLabel.AutoSize = true;
            this.DiscLabelLabel.Image = global::AmpShell.Properties.Resources.CD_ROM_Label;
            this.DiscLabelLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DiscLabelLabel.Location = new System.Drawing.Point(6, 97);
            this.DiscLabelLabel.Name = "DiscLabelLabel";
            this.DiscLabelLabel.Size = new System.Drawing.Size(183, 13);
            this.DiscLabelLabel.TabIndex = 34;
            this.DiscLabelLabel.Text = "      Disc label (only if it is a directory) :";
            this.DiscLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // NoneRadioButton
            //
            this.NoneRadioButton.AutoSize = true;
            this.NoneRadioButton.Location = new System.Drawing.Point(6, 65);
            this.NoneRadioButton.Name = "NoneRadioButton";
            this.NoneRadioButton.Size = new System.Drawing.Size(51, 17);
            this.NoneRadioButton.TabIndex = 23;
            this.NoneRadioButton.TabStop = true;
            this.NoneRadioButton.Text = "None";
            this.NoneRadioButton.UseVisualStyleBackColor = true;
            //
            // UseIOCTLRadioButton
            //
            this.UseIOCTLRadioButton.AutoSize = true;
            this.UseIOCTLRadioButton.Enabled = false;
            this.UseIOCTLRadioButton.Location = new System.Drawing.Point(6, 19);
            this.UseIOCTLRadioButton.Name = "UseIOCTLRadioButton";
            this.UseIOCTLRadioButton.Size = new System.Drawing.Size(152, 17);
            this.UseIOCTLRadioButton.TabIndex = 21;
            this.UseIOCTLRadioButton.TabStop = true;
            this.UseIOCTLRadioButton.Text = "Use IOCTL (for a CD drive)";
            this.UseIOCTLRadioButton.UseVisualStyleBackColor = true;
            //
            // IsAFloppyDiskRadioButton
            //
            this.IsAFloppyDiskRadioButton.AutoSize = true;
            this.IsAFloppyDiskRadioButton.Enabled = false;
            this.IsAFloppyDiskRadioButton.Location = new System.Drawing.Point(6, 42);
            this.IsAFloppyDiskRadioButton.Name = "IsAFloppyDiskRadioButton";
            this.IsAFloppyDiskRadioButton.Size = new System.Drawing.Size(203, 17);
            this.IsAFloppyDiskRadioButton.TabIndex = 22;
            this.IsAFloppyDiskRadioButton.TabStop = true;
            this.IsAFloppyDiskRadioButton.Text = "Is a floppy disk image (mounted as A:)";
            this.IsAFloppyDiskRadioButton.UseVisualStyleBackColor = true;
            //
            // ResetIconButton
            //
            this.ResetIconButton.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.ResetIconButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetIconButton.Location = new System.Drawing.Point(366, 76);
            this.ResetIconButton.Name = "ResetIconButton";
            this.ResetIconButton.Size = new System.Drawing.Size(80, 23);
            this.ResetIconButton.TabIndex = 57;
            this.ResetIconButton.Text = "Reset icon";
            this.ResetIconButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ResetIconButton.UseVisualStyleBackColor = true;
            this.ResetIconButton.Click += new System.EventHandler(this.ResetIconButton_Click);
            //
            // GameIconPictureBox
            //
            this.GameIconPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameIconPictureBox.Image = global::AmpShell.Properties.Resources.Generic_Application1;
            this.GameIconPictureBox.Location = new System.Drawing.Point(373, 4);
            this.GameIconPictureBox.Name = "GameIconPictureBox";
            this.GameIconPictureBox.Size = new System.Drawing.Size(66, 66);
            this.GameIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GameIconPictureBox.TabIndex = 55;
            this.GameIconPictureBox.TabStop = false;
            this.GameIconPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameIconPictureBox_MouseClick);
            //
            // GameCDDirBrowseButton
            //
            this.GameCDDirBrowseButton.Image = global::AmpShell.Properties.Resources.SearchFolderHS;
            this.GameCDDirBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCDDirBrowseButton.Location = new System.Drawing.Point(416, 320);
            this.GameCDDirBrowseButton.Name = "GameCDDirBrowseButton";
            this.GameCDDirBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCDDirBrowseButton.TabIndex = 50;
            this.GameCDDirBrowseButton.Text = "...";
            this.GameCDDirBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCDDirBrowseButton.UseVisualStyleBackColor = true;
            this.GameCDDirBrowseButton.Click += new System.EventHandler(this.GameCDDirBrowseButton_Click);
            //
            // GameSetupBrowseButton
            //
            this.GameSetupBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameSetupBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameSetupBrowseButton.Location = new System.Drawing.Point(416, 196);
            this.GameSetupBrowseButton.Name = "GameSetupBrowseButton";
            this.GameSetupBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameSetupBrowseButton.TabIndex = 42;
            this.GameSetupBrowseButton.Text = "...";
            this.GameSetupBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameSetupBrowseButton.UseVisualStyleBackColor = true;
            this.GameSetupBrowseButton.Click += new System.EventHandler(this.GameSetupBrowseButton_Click);
            //
            // GameSetupLabel
            //
            this.GameSetupLabel.AutoSize = true;
            this.GameSetupLabel.Image = ((System.Drawing.Image)(resources.GetObject("GameSetupLabel.Image")));
            this.GameSetupLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameSetupLabel.Location = new System.Drawing.Point(6, 180);
            this.GameSetupLabel.Name = "GameSetupLabel";
            this.GameSetupLabel.Size = new System.Drawing.Size(226, 13);
            this.GameSetupLabel.TabIndex = 40;
            this.GameSetupLabel.Text = "     Game setup executable location (optional) :";
            this.GameSetupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameSetupTextBox
            //
            this.GameSetupTextBox.Location = new System.Drawing.Point(8, 196);
            this.GameSetupTextBox.Name = "GameSetupTextBox";
            this.GameSetupTextBox.Size = new System.Drawing.Size(402, 20);
            this.GameSetupTextBox.TabIndex = 41;
            //
            // GameDirectoryBrowseButton
            //
            this.GameDirectoryBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameDirectoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameDirectoryBrowseButton.Location = new System.Drawing.Point(416, 150);
            this.GameDirectoryBrowseButton.Name = "GameDirectoryBrowseButton";
            this.GameDirectoryBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameDirectoryBrowseButton.TabIndex = 39;
            this.GameDirectoryBrowseButton.Text = "...";
            this.GameDirectoryBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameDirectoryBrowseButton.UseVisualStyleBackColor = true;
            this.GameDirectoryBrowseButton.Click += new System.EventHandler(this.GameDirectoryBrowseButton_Click);
            //
            // GameDirectoryLabel
            //
            this.GameDirectoryLabel.AutoSize = true;
            this.GameDirectoryLabel.Image = global::AmpShell.Properties.Resources.Folder_Open;
            this.GameDirectoryLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameDirectoryLabel.Location = new System.Drawing.Point(6, 134);
            this.GameDirectoryLabel.Name = "GameDirectoryLabel";
            this.GameDirectoryLabel.Size = new System.Drawing.Size(187, 13);
            this.GameDirectoryLabel.TabIndex = 37;
            this.GameDirectoryLabel.Text = "     Directory mounted as C: (optional) :";
            this.GameDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameDirectoryTextbox
            //
            this.GameDirectoryTextbox.Location = new System.Drawing.Point(8, 150);
            this.GameDirectoryTextbox.Name = "GameDirectoryTextbox";
            this.GameDirectoryTextbox.Size = new System.Drawing.Size(402, 20);
            this.GameDirectoryTextbox.TabIndex = 38;
            this.GameDirectoryTextbox.TextChanged += new System.EventHandler(this.GameDirectoryTextbox_TextChanged);
            //
            // OtherOptionsGroupBox
            //
            this.OtherOptionsGroupBox.Controls.Add(this.FullscreenCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.NoConsoleCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.QuitOnExitCheckBox);
            this.OtherOptionsGroupBox.Location = new System.Drawing.Point(3, 494);
            this.OtherOptionsGroupBox.Name = "OtherOptionsGroupBox";
            this.OtherOptionsGroupBox.Size = new System.Drawing.Size(437, 50);
            this.OtherOptionsGroupBox.TabIndex = 54;
            this.OtherOptionsGroupBox.TabStop = false;
            this.OtherOptionsGroupBox.Text = "Other options";
            //
            // FullscreenCheckBox
            //
            this.FullscreenCheckBox.AutoSize = true;
            this.FullscreenCheckBox.Location = new System.Drawing.Point(90, 19);
            this.FullscreenCheckBox.Name = "FullscreenCheckBox";
            this.FullscreenCheckBox.Size = new System.Drawing.Size(71, 17);
            this.FullscreenCheckBox.TabIndex = 28;
            this.FullscreenCheckBox.Text = "fullscreen";
            this.FullscreenCheckBox.UseVisualStyleBackColor = true;
            //
            // NoConsoleCheckBox
            //
            this.NoConsoleCheckBox.AutoSize = true;
            this.NoConsoleCheckBox.Location = new System.Drawing.Point(6, 19);
            this.NoConsoleCheckBox.Name = "NoConsoleCheckBox";
            this.NoConsoleCheckBox.Size = new System.Drawing.Size(78, 17);
            this.NoConsoleCheckBox.TabIndex = 27;
            this.NoConsoleCheckBox.Text = "no console";
            this.NoConsoleCheckBox.UseVisualStyleBackColor = true;
            //
            // QuitOnExitCheckBox
            //
            this.QuitOnExitCheckBox.AutoSize = true;
            this.QuitOnExitCheckBox.Location = new System.Drawing.Point(167, 19);
            this.QuitOnExitCheckBox.Name = "QuitOnExitCheckBox";
            this.QuitOnExitCheckBox.Size = new System.Drawing.Size(77, 17);
            this.QuitOnExitCheckBox.TabIndex = 29;
            this.QuitOnExitCheckBox.Text = "quit on exit";
            this.QuitOnExitCheckBox.UseVisualStyleBackColor = true;
            this.QuitOnExitCheckBox.EnabledChanged += new System.EventHandler(this.QuitOnExitCheckBox_EnabledChanged);
            //
            // NoConfigCheckBox
            //
            this.NoConfigCheckBox.AutoSize = true;
            this.NoConfigCheckBox.Location = new System.Drawing.Point(8, 273);
            this.NoConfigCheckBox.Name = "NoConfigCheckBox";
            this.NoConfigCheckBox.Size = new System.Drawing.Size(319, 17);
            this.NoConfigCheckBox.TabIndex = 46;
            this.NoConfigCheckBox.Text = "No config file at all (may not work with DOSBox 0.73 or newer)";
            this.NoConfigCheckBox.UseVisualStyleBackColor = true;
            this.NoConfigCheckBox.CheckedChanged += new System.EventHandler(this.NoConfigCheckBox_CheckedChanged);
            //
            // GameCDPathBrowseButton
            //
            this.GameCDPathBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameCDPathBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCDPathBrowseButton.Location = new System.Drawing.Point(416, 297);
            this.GameCDPathBrowseButton.Name = "GameCDPathBrowseButton";
            this.GameCDPathBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCDPathBrowseButton.TabIndex = 49;
            this.GameCDPathBrowseButton.Text = "...";
            this.GameCDPathBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCDPathBrowseButton.UseVisualStyleBackColor = true;
            this.GameCDPathBrowseButton.Click += new System.EventHandler(this.GameCDPathBrowseButton_Click);
            //
            // GameCDPathLabel
            //
            this.GameCDPathLabel.AutoSize = true;
            this.GameCDPathLabel.Image = global::AmpShell.Properties.Resources.CD_ROM;
            this.GameCDPathLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameCDPathLabel.Location = new System.Drawing.Point(6, 297);
            this.GameCDPathLabel.Name = "GameCDPathLabel";
            this.GameCDPathLabel.Size = new System.Drawing.Size(266, 13);
            this.GameCDPathLabel.TabIndex = 47;
            this.GameCDPathLabel.Text = "      CD image file or directory mounted as D: (optional) :";
            this.GameCDPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameCDPathTextBox
            //
            this.GameCDPathTextBox.Location = new System.Drawing.Point(8, 313);
            this.GameCDPathTextBox.Name = "GameCDPathTextBox";
            this.GameCDPathTextBox.Size = new System.Drawing.Size(402, 20);
            this.GameCDPathTextBox.TabIndex = 48;
            this.GameCDPathTextBox.TextChanged += new System.EventHandler(this.GameCDPathTextBox_TextChanged);
            //
            // GameNameTextbox
            //
            this.GameNameTextbox.Location = new System.Drawing.Point(8, 24);
            this.GameNameTextbox.Name = "GameNameTextbox";
            this.GameNameTextbox.Size = new System.Drawing.Size(350, 20);
            this.GameNameTextbox.TabIndex = 33;
            //
            // GameNameLabel
            //
            this.GameNameLabel.AutoSize = true;
            this.GameNameLabel.Image = global::AmpShell.Properties.Resources.Rename;
            this.GameNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameNameLabel.Location = new System.Drawing.Point(5, 6);
            this.GameNameLabel.Name = "GameNameLabel";
            this.GameNameLabel.Size = new System.Drawing.Size(91, 13);
            this.GameNameLabel.TabIndex = 32;
            this.GameNameLabel.Text = "      Game name : ";
            this.GameNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameCustomConfigurationBrowseButton
            //
            this.GameCustomConfigurationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameCustomConfigurationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameCustomConfigurationBrowseButton.Location = new System.Drawing.Point(416, 246);
            this.GameCustomConfigurationBrowseButton.Name = "GameCustomConfigurationBrowseButton";
            this.GameCustomConfigurationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCustomConfigurationBrowseButton.TabIndex = 45;
            this.GameCustomConfigurationBrowseButton.Text = "...";
            this.GameCustomConfigurationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCustomConfigurationBrowseButton.UseVisualStyleBackColor = true;
            this.GameCustomConfigurationBrowseButton.Click += new System.EventHandler(this.GameCustomConfigurationBrowseButton_Click);
            //
            // GameCustomConfigurationLabel
            //
            this.GameCustomConfigurationLabel.AutoSize = true;
            this.GameCustomConfigurationLabel.Image = global::AmpShell.Properties.Resources.Configuration;
            this.GameCustomConfigurationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameCustomConfigurationLabel.Location = new System.Drawing.Point(6, 226);
            this.GameCustomConfigurationLabel.Name = "GameCustomConfigurationLabel";
            this.GameCustomConfigurationLabel.Size = new System.Drawing.Size(213, 13);
            this.GameCustomConfigurationLabel.TabIndex = 43;
            this.GameCustomConfigurationLabel.Text = "     Custom configuration location (optional) :";
            this.GameCustomConfigurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameCustomConfigurationTextbox
            //
            this.GameCustomConfigurationTextbox.Location = new System.Drawing.Point(8, 246);
            this.GameCustomConfigurationTextbox.Name = "GameCustomConfigurationTextbox";
            this.GameCustomConfigurationTextbox.Size = new System.Drawing.Size(402, 20);
            this.GameCustomConfigurationTextbox.TabIndex = 44;
            //
            // GameLocationBrowseButton
            //
            this.GameLocationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameLocationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameLocationBrowseButton.Location = new System.Drawing.Point(416, 108);
            this.GameLocationBrowseButton.Name = "GameLocationBrowseButton";
            this.GameLocationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameLocationBrowseButton.TabIndex = 36;
            this.GameLocationBrowseButton.Text = "...";
            this.GameLocationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameLocationBrowseButton.UseVisualStyleBackColor = true;
            this.GameLocationBrowseButton.Click += new System.EventHandler(this.GameLocationBrowseButton_Click);
            //
            // GameLocationLabel
            //
            this.GameLocationLabel.AutoSize = true;
            this.GameLocationLabel.Image = global::AmpShell.Properties.Resources.GameEditExecutableLabelImage;
            this.GameLocationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameLocationLabel.Location = new System.Drawing.Point(6, 90);
            this.GameLocationLabel.Name = "GameLocationLabel";
            this.GameLocationLabel.Size = new System.Drawing.Size(200, 13);
            this.GameLocationLabel.TabIndex = 34;
            this.GameLocationLabel.Text = "      Game executable location (optional) :";
            this.GameLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameLocationTextbox
            //
            this.GameLocationTextbox.Location = new System.Drawing.Point(8, 108);
            this.GameLocationTextbox.Name = "GameLocationTextbox";
            this.GameLocationTextbox.Size = new System.Drawing.Size(402, 20);
            this.GameLocationTextbox.TabIndex = 35;
            this.GameLocationTextbox.TextChanged += new System.EventHandler(this.GameLocationTextbox_TextChanged);
            //
            // AdvancedTabPage
            // 
            this.AdvancedTabPage.Controls.Add(this.AdditionnalCommandsTipLabel);
            this.AdvancedTabPage.Controls.Add(this.DOSBoxWorkingDirButton);
            this.AdvancedTabPage.Controls.Add(this.DOSBoxWorkingDirLabel);
            this.AdvancedTabPage.Controls.Add(this.DOSBoxWorkingDirTextBox);
            this.AdvancedTabPage.Controls.Add(this.DontUseDOSBoxCheckBox);
            this.AdvancedTabPage.Controls.Add(this.GameAdditionalCommandsLabel);
            this.AdvancedTabPage.Controls.Add(this.GameAdditionalCommandsTextBox);
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationBrowseButton);
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationTextbox);
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationLabel);
            this.AdvancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.AdvancedTabPage.Name = "AdvancedTabPage";
            this.AdvancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AdvancedTabPage.Size = new System.Drawing.Size(456, 550);
            this.AdvancedTabPage.TabIndex = 1;
            this.AdvancedTabPage.Text = "Advanced Configuration";
            this.AdvancedTabPage.UseVisualStyleBackColor = true;
            // 
            // DOSBoxWorkingDirButton
            // 
            this.DOSBoxWorkingDirButton.Image = global::AmpShell.Properties.Resources.SearchFolderHS;
            this.DOSBoxWorkingDirButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.DOSBoxWorkingDirButton.Location = new System.Drawing.Point(423, 84);
            this.DOSBoxWorkingDirButton.Name = "DOSBoxWorkingDirButton";
            this.DOSBoxWorkingDirButton.Size = new System.Drawing.Size(24, 23);
            this.DOSBoxWorkingDirButton.TabIndex = 59;
            this.DOSBoxWorkingDirButton.Text = "...";
            this.DOSBoxWorkingDirButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DOSBoxWorkingDirButton.UseVisualStyleBackColor = true;
            this.DOSBoxWorkingDirButton.Click += new System.EventHandler(this.DOSBoxWorkingDirButton_Click);
            // 
            // DOSBoxWorkingDirLabel
            // 
            this.DOSBoxWorkingDirLabel.AutoSize = true;
            this.DOSBoxWorkingDirLabel.Image = global::AmpShell.Properties.Resources.Folder_Open;
            this.DOSBoxWorkingDirLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DOSBoxWorkingDirLabel.Location = new System.Drawing.Point(8, 68);
            this.DOSBoxWorkingDirLabel.Name = "DOSBoxWorkingDirLabel";
            this.DOSBoxWorkingDirLabel.Size = new System.Drawing.Size(219, 13);
            this.DOSBoxWorkingDirLabel.TabIndex = 58;
            this.DOSBoxWorkingDirLabel.Text = "      Working directory for DOSBox (optional) :";
            this.DOSBoxWorkingDirLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DOSBoxWorkingDirTextBox
            // 
            this.DOSBoxWorkingDirTextBox.Location = new System.Drawing.Point(10, 84);
            this.DOSBoxWorkingDirTextBox.Name = "DOSBoxWorkingDirTextBox";
            this.DOSBoxWorkingDirTextBox.Size = new System.Drawing.Size(407, 20);
            this.DOSBoxWorkingDirTextBox.TabIndex = 57;
            // 
            // DontUseDOSBoxCheckBox
            //
            this.DontUseDOSBoxCheckBox.AutoSize = true;
            this.DontUseDOSBoxCheckBox.Location = new System.Drawing.Point(11, 110);
            this.DontUseDOSBoxCheckBox.Name = "DontUseDOSBoxCheckBox";
            this.DontUseDOSBoxCheckBox.Size = new System.Drawing.Size(239, 17);
            this.DontUseDOSBoxCheckBox.TabIndex = 56;
            this.DontUseDOSBoxCheckBox.Text = "Don\'t use DOSBox for this game. Run it as is.";
            this.DontUseDOSBoxCheckBox.UseVisualStyleBackColor = true;
            this.DontUseDOSBoxCheckBox.CheckedChanged += new System.EventHandler(this.DontUseDOSBoxCheckBox_CheckedChanged);
            //
            // GameAdditionalCommandsLabel
            //
            this.GameAdditionalCommandsLabel.AutoSize = true;
            this.GameAdditionalCommandsLabel.Image = global::AmpShell.Properties.Resources.cmd;
            this.GameAdditionalCommandsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameAdditionalCommandsLabel.Location = new System.Drawing.Point(8, 139);
            this.GameAdditionalCommandsLabel.Name = "GameAdditionalCommandsLabel";
            this.GameAdditionalCommandsLabel.Size = new System.Drawing.Size(221, 13);
            this.GameAdditionalCommandsLabel.TabIndex = 54;
            this.GameAdditionalCommandsLabel.Text = "      Additional DOSBox commands (optional) :";
            this.GameAdditionalCommandsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // GameAdditionalCommandsTextBox
            // 
            this.GameAdditionalCommandsTextBox.Location = new System.Drawing.Point(8, 155);
            this.GameAdditionalCommandsTextBox.Multiline = true;
            this.GameAdditionalCommandsTextBox.Name = "GameAdditionalCommandsTextBox";
            this.GameAdditionalCommandsTextBox.Size = new System.Drawing.Size(433, 341);
            this.GameAdditionalCommandsTextBox.TabIndex = 55;
            //
            // AlternateDOSBoxLocationBrowseButton
            //
            this.AlternateDOSBoxLocationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.AlternateDOSBoxLocationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AlternateDOSBoxLocationBrowseButton.Location = new System.Drawing.Point(424, 44);
            this.AlternateDOSBoxLocationBrowseButton.Name = "AlternateDOSBoxLocationBrowseButton";
            this.AlternateDOSBoxLocationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.AlternateDOSBoxLocationBrowseButton.TabIndex = 37;
            this.AlternateDOSBoxLocationBrowseButton.Text = "...";
            this.AlternateDOSBoxLocationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AlternateDOSBoxLocationBrowseButton.UseVisualStyleBackColor = true;
            this.AlternateDOSBoxLocationBrowseButton.Click += new System.EventHandler(this.AlternateDOSBoxLocationBrowsSearchButton_Click);
            //
            // AlternateDOSBoxLocationTextbox
            //
            this.AlternateDOSBoxLocationTextbox.Location = new System.Drawing.Point(11, 45);
            this.AlternateDOSBoxLocationTextbox.Name = "AlternateDOSBoxLocationTextbox";
            this.AlternateDOSBoxLocationTextbox.Size = new System.Drawing.Size(407, 20);
            this.AlternateDOSBoxLocationTextbox.TabIndex = 35;
            //
            // AlternateDOSBoxLocationLabel
            //
            this.AlternateDOSBoxLocationLabel.AutoSize = true;
            this.AlternateDOSBoxLocationLabel.Image = global::AmpShell.Properties.Resources.DOSBox;
            this.AlternateDOSBoxLocationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AlternateDOSBoxLocationLabel.Location = new System.Drawing.Point(8, 3);
            this.AlternateDOSBoxLocationLabel.Name = "AlternateDOSBoxLocationLabel";
            this.AlternateDOSBoxLocationLabel.Size = new System.Drawing.Size(447, 39);
            this.AlternateDOSBoxLocationLabel.TabIndex = 34;
            this.AlternateDOSBoxLocationLabel.Text = "\r\n           Use another DOSBox executable, such as DOSBox ECE, DOSBox SVN, ... (" +
    "optional) :\r\n\r\n";
            this.AlternateDOSBoxLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // NotesTabPage
            //
            this.NotesTabPage.Controls.Add(this.NotesRichTextBox);
            this.NotesTabPage.Location = new System.Drawing.Point(4, 22);
            this.NotesTabPage.Name = "NotesTabPage";
            this.NotesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.NotesTabPage.Size = new System.Drawing.Size(456, 550);
            this.NotesTabPage.TabIndex = 2;
            this.NotesTabPage.Text = "Notes";
            this.NotesTabPage.UseVisualStyleBackColor = true;
            //
            // NotesRichTextBox
            //
            this.NotesRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotesRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.NotesRichTextBox.Name = "NotesRichTextBox";
            this.NotesRichTextBox.Size = new System.Drawing.Size(450, 544);
            this.NotesRichTextBox.TabIndex = 0;
            this.NotesRichTextBox.Text = "";
            //
            // OK
            //
            this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
            this.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OK.Location = new System.Drawing.Point(275, 582);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(96, 23);
            this.OK.TabIndex = 56;
            this.OK.Text = "&Add this game";
            this.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            //
            // Cancel
            //
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancel.Location = new System.Drawing.Point(377, 582);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 23);
            this.Cancel.TabIndex = 58;
            this.Cancel.Text = "&Don\'t add it";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AdditionnalCommandsTipLabel
            // 
            this.AdditionnalCommandsTipLabel.AutoSize = true;
            this.AdditionnalCommandsTipLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AdditionnalCommandsTipLabel.Location = new System.Drawing.Point(86, 499);
            this.AdditionnalCommandsTipLabel.Name = "AdditionnalCommandsTipLabel";
            this.AdditionnalCommandsTipLabel.Size = new System.Drawing.Size(291, 39);
            this.AdditionnalCommandsTipLabel.TabIndex = 60;
            this.AdditionnalCommandsTipLabel.Text = "Put each command on a new line.\r\nFor example \'IMGMOUNT D C:\\Temp\\MyCDImage.iso -t" +
    " iso\'\r\nor anything recognized by DOSBox.";
            this.AdditionnalCommandsTipLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(464, 611);
            this.Controls.Add(this.ConfigTabControl);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 230);
            this.Name = "GameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a game...";
            this.Shown += new System.EventHandler(this.GameForm_Shown);
            this.ConfigTabControl.ResumeLayout(false);
            this.BasicTabPage.ResumeLayout(false);
            this.BasicTabPage.PerformLayout();
            this.MountingOptionsGroupBox.ResumeLayout(false);
            this.MountingOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).EndInit();
            this.OtherOptionsGroupBox.ResumeLayout(false);
            this.OtherOptionsGroupBox.PerformLayout();
            this.AdvancedTabPage.ResumeLayout(false);
            this.AdvancedTabPage.PerformLayout();
            this.NotesTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.TabControl ConfigTabControl;
        private System.Windows.Forms.TabPage BasicTabPage;
        private System.Windows.Forms.GroupBox MountingOptionsGroupBox;
        private System.Windows.Forms.RadioButton NoneRadioButton;
        private System.Windows.Forms.RadioButton UseIOCTLRadioButton;
        private System.Windows.Forms.RadioButton IsAFloppyDiskRadioButton;
        private System.Windows.Forms.Button ResetIconButton;
        private System.Windows.Forms.PictureBox GameIconPictureBox;
        private System.Windows.Forms.Button GameCDDirBrowseButton;
        private System.Windows.Forms.Button GameSetupBrowseButton;
        private System.Windows.Forms.Label GameSetupLabel;
        private System.Windows.Forms.TextBox GameSetupTextBox;
        private System.Windows.Forms.Button GameDirectoryBrowseButton;
        private System.Windows.Forms.Label GameDirectoryLabel;
        private System.Windows.Forms.TextBox GameDirectoryTextbox;
        private System.Windows.Forms.GroupBox OtherOptionsGroupBox;
        private System.Windows.Forms.CheckBox FullscreenCheckBox;
        private System.Windows.Forms.CheckBox NoConsoleCheckBox;
        private System.Windows.Forms.CheckBox QuitOnExitCheckBox;
        private System.Windows.Forms.CheckBox NoConfigCheckBox;
        private System.Windows.Forms.Button GameCDPathBrowseButton;
        private System.Windows.Forms.Label GameCDPathLabel;
        private System.Windows.Forms.TextBox GameCDPathTextBox;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TextBox GameNameTextbox;
        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.Button GameCustomConfigurationBrowseButton;
        private System.Windows.Forms.Label GameCustomConfigurationLabel;
        private System.Windows.Forms.TextBox GameCustomConfigurationTextbox;
        private System.Windows.Forms.Button GameLocationBrowseButton;
        private System.Windows.Forms.Label GameLocationLabel;
        private System.Windows.Forms.TextBox GameLocationTextbox;
        private System.Windows.Forms.TabPage AdvancedTabPage;
        private System.Windows.Forms.Button AlternateDOSBoxLocationBrowseButton;
        private System.Windows.Forms.TextBox AlternateDOSBoxLocationTextbox;
        private System.Windows.Forms.Label AlternateDOSBoxLocationLabel;
        private System.Windows.Forms.TextBox DiscLabelTextBox;
        private System.Windows.Forms.Label DiscLabelLabel;
        private System.Windows.Forms.Label ReleaseDateLabel;
        private System.Windows.Forms.DateTimePicker GameReleaseDatePicker;
        private System.Windows.Forms.TabPage NotesTabPage;
        private System.Windows.Forms.RichTextBox NotesRichTextBox;
        private System.Windows.Forms.Label GameAdditionalCommandsLabel;
        private System.Windows.Forms.TextBox GameAdditionalCommandsTextBox;
        private System.Windows.Forms.CheckBox DontUseDOSBoxCheckBox;
        private System.Windows.Forms.Button DOSBoxWorkingDirButton;
        private System.Windows.Forms.Label DOSBoxWorkingDirLabel;
        private System.Windows.Forms.TextBox DOSBoxWorkingDirTextBox;
        private System.Windows.Forms.Label AdditionnalCommandsTipLabel;
    }
}