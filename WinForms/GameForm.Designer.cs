/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
namespace AmpShell.WinForms
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.BasicTabPage = new System.Windows.Forms.TabPage();
            this.MountingOptionsGroupBox = new System.Windows.Forms.GroupBox();
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
            this.GameAdditionalCommandsLabel = new System.Windows.Forms.Label();
            this.GameAdditionalCommandsTextBox = new System.Windows.Forms.TextBox();
            this.NoConfigCheckBox = new System.Windows.Forms.CheckBox();
            this.GameCDPathBrowseButton = new System.Windows.Forms.Button();
            this.GameCDPathLabel = new System.Windows.Forms.Label();
            this.GameCDPathTextBox = new System.Windows.Forms.TextBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.GameNameTextbox = new System.Windows.Forms.TextBox();
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.GameCustomConfigurationBrowseButton = new System.Windows.Forms.Button();
            this.GameCustomCofigurationLabel = new System.Windows.Forms.Label();
            this.GameCustomConfigurationTextbox = new System.Windows.Forms.TextBox();
            this.GameLocationBrowseButton = new System.Windows.Forms.Button();
            this.GameLocationLabel = new System.Windows.Forms.Label();
            this.GameLocationTextbox = new System.Windows.Forms.TextBox();
            this.AdvancedTabPage = new System.Windows.Forms.TabPage();
            this.AlternateDOSBoxLocationBrowsSearchButton = new System.Windows.Forms.Button();
            this.AlternateDOSBoxLocationTextbox = new System.Windows.Forms.TextBox();
            this.AlternateDOSBoxLocationLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.BasicTabPage.SuspendLayout();
            this.MountingOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).BeginInit();
            this.OtherOptionsGroupBox.SuspendLayout();
            this.AdvancedTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.BasicTabPage);
            this.tabControl1.Controls.Add(this.AdvancedTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(433, 480);
            this.tabControl1.TabIndex = 0;
            // 
            // BasicTabPage
            // 
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
            this.BasicTabPage.Controls.Add(this.GameAdditionalCommandsLabel);
            this.BasicTabPage.Controls.Add(this.GameAdditionalCommandsTextBox);
            this.BasicTabPage.Controls.Add(this.NoConfigCheckBox);
            this.BasicTabPage.Controls.Add(this.GameCDPathBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameCDPathLabel);
            this.BasicTabPage.Controls.Add(this.GameCDPathTextBox);
            this.BasicTabPage.Controls.Add(this.GameNameTextbox);
            this.BasicTabPage.Controls.Add(this.GameNameLabel);
            this.BasicTabPage.Controls.Add(this.GameCustomConfigurationBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameCustomCofigurationLabel);
            this.BasicTabPage.Controls.Add(this.GameCustomConfigurationTextbox);
            this.BasicTabPage.Controls.Add(this.GameLocationBrowseButton);
            this.BasicTabPage.Controls.Add(this.GameLocationLabel);
            this.BasicTabPage.Controls.Add(this.GameLocationTextbox);
            this.BasicTabPage.Location = new System.Drawing.Point(4, 22);
            this.BasicTabPage.Name = "BasicTabPage";
            this.BasicTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BasicTabPage.Size = new System.Drawing.Size(425, 454);
            this.BasicTabPage.TabIndex = 0;
            this.BasicTabPage.Text = "Basic Configuration";
            this.BasicTabPage.UseVisualStyleBackColor = true;
            // 
            // MountingOptionsGroupBox
            // 
            this.MountingOptionsGroupBox.Controls.Add(this.NoneRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.UseIOCTLRadioButton);
            this.MountingOptionsGroupBox.Controls.Add(this.IsAFloppyDiskRadioButton);
            this.MountingOptionsGroupBox.Enabled = false;
            this.MountingOptionsGroupBox.Location = new System.Drawing.Point(8, 300);
            this.MountingOptionsGroupBox.Name = "MountingOptionsGroupBox";
            this.MountingOptionsGroupBox.Size = new System.Drawing.Size(411, 38);
            this.MountingOptionsGroupBox.TabIndex = 51;
            this.MountingOptionsGroupBox.TabStop = false;
            this.MountingOptionsGroupBox.Text = "Mounting options";
            // 
            // NoneRadioButton
            // 
            this.NoneRadioButton.AutoSize = true;
            this.NoneRadioButton.Location = new System.Drawing.Point(356, 19);
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
            this.IsAFloppyDiskRadioButton.Location = new System.Drawing.Point(164, 19);
            this.IsAFloppyDiskRadioButton.Name = "IsAFloppyDiskRadioButton";
            this.IsAFloppyDiskRadioButton.Size = new System.Drawing.Size(186, 17);
            this.IsAFloppyDiskRadioButton.TabIndex = 22;
            this.IsAFloppyDiskRadioButton.TabStop = true;
            this.IsAFloppyDiskRadioButton.Text = "Floppy disk image (mounted as A:)";
            this.IsAFloppyDiskRadioButton.UseVisualStyleBackColor = true;
            // 
            // ResetIconButton
            // 
            this.ResetIconButton.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.ResetIconButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResetIconButton.Location = new System.Drawing.Point(270, 6);
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
            this.GameIconPictureBox.Location = new System.Drawing.Point(353, 6);
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
            this.GameCDDirBrowseButton.Location = new System.Drawing.Point(396, 271);
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
            this.GameSetupBrowseButton.Location = new System.Drawing.Point(396, 161);
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
            this.GameSetupLabel.Location = new System.Drawing.Point(6, 148);
            this.GameSetupLabel.Name = "GameSetupLabel";
            this.GameSetupLabel.Size = new System.Drawing.Size(226, 13);
            this.GameSetupLabel.TabIndex = 40;
            this.GameSetupLabel.Text = "     Game setup executable location (optional) :";
            this.GameSetupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameSetupTextBox
            // 
            this.GameSetupTextBox.Location = new System.Drawing.Point(8, 164);
            this.GameSetupTextBox.Name = "GameSetupTextBox";
            this.GameSetupTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameSetupTextBox.TabIndex = 41;
            // 
            // GameDirectoryBrowseButton
            // 
            this.GameDirectoryBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameDirectoryBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameDirectoryBrowseButton.Location = new System.Drawing.Point(396, 123);
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
            this.GameDirectoryLabel.Location = new System.Drawing.Point(6, 109);
            this.GameDirectoryLabel.Name = "GameDirectoryLabel";
            this.GameDirectoryLabel.Size = new System.Drawing.Size(187, 13);
            this.GameDirectoryLabel.TabIndex = 37;
            this.GameDirectoryLabel.Text = "     Directory mounted as C: (optional) :";
            this.GameDirectoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameDirectoryTextbox
            // 
            this.GameDirectoryTextbox.Location = new System.Drawing.Point(8, 125);
            this.GameDirectoryTextbox.Name = "GameDirectoryTextbox";
            this.GameDirectoryTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameDirectoryTextbox.TabIndex = 38;
            this.GameDirectoryTextbox.TextChanged += new System.EventHandler(this.GameDirectoryTextbox_TextChanged);
            // 
            // OtherOptionsGroupBox
            // 
            this.OtherOptionsGroupBox.Controls.Add(this.FullscreenCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.NoConsoleCheckBox);
            this.OtherOptionsGroupBox.Controls.Add(this.QuitOnExitCheckBox);
            this.OtherOptionsGroupBox.Location = new System.Drawing.Point(5, 383);
            this.OtherOptionsGroupBox.Name = "OtherOptionsGroupBox";
            this.OtherOptionsGroupBox.Size = new System.Drawing.Size(414, 40);
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
            // GameAdditionalCommandsLabel
            // 
            this.GameAdditionalCommandsLabel.AutoSize = true;
            this.GameAdditionalCommandsLabel.Image = global::AmpShell.Properties.Resources.cmd;
            this.GameAdditionalCommandsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameAdditionalCommandsLabel.Location = new System.Drawing.Point(5, 341);
            this.GameAdditionalCommandsLabel.Name = "GameAdditionalCommandsLabel";
            this.GameAdditionalCommandsLabel.Size = new System.Drawing.Size(298, 13);
            this.GameAdditionalCommandsLabel.TabIndex = 52;
            this.GameAdditionalCommandsLabel.Text = "      Additional DOSBox commands (-c \"command\") (optional) :";
            this.GameAdditionalCommandsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameAdditionalCommandsTextBox
            // 
            this.GameAdditionalCommandsTextBox.Location = new System.Drawing.Point(7, 357);
            this.GameAdditionalCommandsTextBox.Name = "GameAdditionalCommandsTextBox";
            this.GameAdditionalCommandsTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameAdditionalCommandsTextBox.TabIndex = 53;
            // 
            // NoConfigCheckBox
            // 
            this.NoConfigCheckBox.AutoSize = true;
            this.NoConfigCheckBox.Location = new System.Drawing.Point(8, 229);
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
            this.GameCDPathBrowseButton.Location = new System.Drawing.Point(396, 249);
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
            this.GameCDPathLabel.Location = new System.Drawing.Point(6, 249);
            this.GameCDPathLabel.Name = "GameCDPathLabel";
            this.GameCDPathLabel.Size = new System.Drawing.Size(266, 13);
            this.GameCDPathLabel.TabIndex = 47;
            this.GameCDPathLabel.Text = "      CD image file or directory mounted as D: (optional) :";
            this.GameCDPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameCDPathTextBox
            // 
            this.GameCDPathTextBox.Location = new System.Drawing.Point(8, 265);
            this.GameCDPathTextBox.Name = "GameCDPathTextBox";
            this.GameCDPathTextBox.Size = new System.Drawing.Size(382, 20);
            this.GameCDPathTextBox.TabIndex = 48;
            this.GameCDPathTextBox.TextChanged += new System.EventHandler(this.GameCDPathTextBox_TextChanged);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Image = global::AmpShell.Properties.Resources.DeleteHS;
            this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cancel.Location = new System.Drawing.Point(347, 482);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 23);
            this.Cancel.TabIndex = 58;
            this.Cancel.Text = "&Don\'t add it";
            this.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OK
            // 
            this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
            this.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OK.Location = new System.Drawing.Point(245, 482);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(96, 23);
            this.OK.TabIndex = 56;
            this.OK.Text = "&Add this game";
            this.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // GameNameTextbox
            // 
            this.GameNameTextbox.Location = new System.Drawing.Point(8, 47);
            this.GameNameTextbox.Name = "GameNameTextbox";
            this.GameNameTextbox.Size = new System.Drawing.Size(342, 20);
            this.GameNameTextbox.TabIndex = 33;
            // 
            // GameNameLabel
            // 
            this.GameNameLabel.AutoSize = true;
            this.GameNameLabel.Image = global::AmpShell.Properties.Resources.Rename;
            this.GameNameLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameNameLabel.Location = new System.Drawing.Point(5, 31);
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
            this.GameCustomConfigurationBrowseButton.Location = new System.Drawing.Point(396, 200);
            this.GameCustomConfigurationBrowseButton.Name = "GameCustomConfigurationBrowseButton";
            this.GameCustomConfigurationBrowseButton.Size = new System.Drawing.Size(24, 23);
            this.GameCustomConfigurationBrowseButton.TabIndex = 45;
            this.GameCustomConfigurationBrowseButton.Text = "...";
            this.GameCustomConfigurationBrowseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.GameCustomConfigurationBrowseButton.UseVisualStyleBackColor = true;
            this.GameCustomConfigurationBrowseButton.Click += new System.EventHandler(this.GameCustomConfigurationBrowseButton_Click);
            // 
            // GameCustomCofigurationLabel
            // 
            this.GameCustomCofigurationLabel.AutoSize = true;
            this.GameCustomCofigurationLabel.Image = global::AmpShell.Properties.Resources.Configuration;
            this.GameCustomCofigurationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GameCustomCofigurationLabel.Location = new System.Drawing.Point(6, 187);
            this.GameCustomCofigurationLabel.Name = "GameCustomCofigurationLabel";
            this.GameCustomCofigurationLabel.Size = new System.Drawing.Size(213, 13);
            this.GameCustomCofigurationLabel.TabIndex = 43;
            this.GameCustomCofigurationLabel.Text = "     Custom configuration location (optional) :";
            this.GameCustomCofigurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameCustomConfigurationTextbox
            // 
            this.GameCustomConfigurationTextbox.Location = new System.Drawing.Point(8, 203);
            this.GameCustomConfigurationTextbox.Name = "GameCustomConfigurationTextbox";
            this.GameCustomConfigurationTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameCustomConfigurationTextbox.TabIndex = 44;
            // 
            // GameLocationBrowseButton
            // 
            this.GameLocationBrowseButton.Image = global::AmpShell.Properties.Resources.search;
            this.GameLocationBrowseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.GameLocationBrowseButton.Location = new System.Drawing.Point(396, 83);
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
            this.GameLocationLabel.Location = new System.Drawing.Point(6, 70);
            this.GameLocationLabel.Name = "GameLocationLabel";
            this.GameLocationLabel.Size = new System.Drawing.Size(341, 13);
            this.GameLocationLabel.TabIndex = 34;
            this.GameLocationLabel.Text = "      Game executable location (optional if a directory is mounted as C:) :";
            this.GameLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GameLocationTextbox
            // 
            this.GameLocationTextbox.Location = new System.Drawing.Point(8, 86);
            this.GameLocationTextbox.Name = "GameLocationTextbox";
            this.GameLocationTextbox.Size = new System.Drawing.Size(382, 20);
            this.GameLocationTextbox.TabIndex = 35;
            this.GameLocationTextbox.TextChanged += new System.EventHandler(this.GameLocationTextbox_TextChanged);
            // 
            // AdvancedTabPage
            // 
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationBrowsSearchButton);
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationTextbox);
            this.AdvancedTabPage.Controls.Add(this.AlternateDOSBoxLocationLabel);
            this.AdvancedTabPage.Location = new System.Drawing.Point(4, 22);
            this.AdvancedTabPage.Name = "AdvancedTabPage";
            this.AdvancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AdvancedTabPage.Size = new System.Drawing.Size(425, 454);
            this.AdvancedTabPage.TabIndex = 1;
            this.AdvancedTabPage.Text = "Advanced Configuration";
            this.AdvancedTabPage.UseVisualStyleBackColor = true;
            // 
            // AlternateDOSBoxLocationBrowsSearchButton
            // 
            this.AlternateDOSBoxLocationBrowsSearchButton.Image = global::AmpShell.Properties.Resources.search;
            this.AlternateDOSBoxLocationBrowsSearchButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AlternateDOSBoxLocationBrowsSearchButton.Location = new System.Drawing.Point(393, 45);
            this.AlternateDOSBoxLocationBrowsSearchButton.Name = "AlternateDOSBoxLocationBrowsSearchButton";
            this.AlternateDOSBoxLocationBrowsSearchButton.Size = new System.Drawing.Size(24, 23);
            this.AlternateDOSBoxLocationBrowsSearchButton.TabIndex = 37;
            this.AlternateDOSBoxLocationBrowsSearchButton.Text = "...";
            this.AlternateDOSBoxLocationBrowsSearchButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AlternateDOSBoxLocationBrowsSearchButton.UseVisualStyleBackColor = true;
            this.AlternateDOSBoxLocationBrowsSearchButton.Click += new System.EventHandler(this.AlternateDOSBoxLocationBrowsSearchButton_Click);
            // 
            // AlternateDOSBoxLocationTextbox
            // 
            this.AlternateDOSBoxLocationTextbox.Location = new System.Drawing.Point(11, 45);
            this.AlternateDOSBoxLocationTextbox.Name = "AlternateDOSBoxLocationTextbox";
            this.AlternateDOSBoxLocationTextbox.Size = new System.Drawing.Size(376, 20);
            this.AlternateDOSBoxLocationTextbox.TabIndex = 35;
            // 
            // AlternateDOSBoxLocationLabel
            // 
            this.AlternateDOSBoxLocationLabel.AutoSize = true;
            this.AlternateDOSBoxLocationLabel.Image = global::AmpShell.Properties.Resources.DOSBox;
            this.AlternateDOSBoxLocationLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AlternateDOSBoxLocationLabel.Location = new System.Drawing.Point(8, 3);
            this.AlternateDOSBoxLocationLabel.Name = "AlternateDOSBoxLocationLabel";
            this.AlternateDOSBoxLocationLabel.Size = new System.Drawing.Size(364, 39);
            this.AlternateDOSBoxLocationLabel.TabIndex = 34;
            this.AlternateDOSBoxLocationLabel.Text = "\r\n           Use another DOSBox executable (DOSBox ECE, DOSBox SVN, ...) :\r\n\r\n";
            this.AlternateDOSBoxLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(433, 507);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Cancel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add a game...";
            this.tabControl1.ResumeLayout(false);
            this.BasicTabPage.ResumeLayout(false);
            this.BasicTabPage.PerformLayout();
            this.MountingOptionsGroupBox.ResumeLayout(false);
            this.MountingOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameIconPictureBox)).EndInit();
            this.OtherOptionsGroupBox.ResumeLayout(false);
            this.OtherOptionsGroupBox.PerformLayout();
            this.AdvancedTabPage.ResumeLayout(false);
            this.AdvancedTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.Label GameAdditionalCommandsLabel;
        private System.Windows.Forms.TextBox GameAdditionalCommandsTextBox;
        private System.Windows.Forms.CheckBox NoConfigCheckBox;
        private System.Windows.Forms.Button GameCDPathBrowseButton;
        private System.Windows.Forms.Label GameCDPathLabel;
        private System.Windows.Forms.TextBox GameCDPathTextBox;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TextBox GameNameTextbox;
        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.Button GameCustomConfigurationBrowseButton;
        private System.Windows.Forms.Label GameCustomCofigurationLabel;
        private System.Windows.Forms.TextBox GameCustomConfigurationTextbox;
        private System.Windows.Forms.Button GameLocationBrowseButton;
        private System.Windows.Forms.Label GameLocationLabel;
        private System.Windows.Forms.TextBox GameLocationTextbox;
        private System.Windows.Forms.TabPage AdvancedTabPage;
        private System.Windows.Forms.Button AlternateDOSBoxLocationBrowsSearchButton;
        private System.Windows.Forms.TextBox AlternateDOSBoxLocationTextbox;
        private System.Windows.Forms.Label AlternateDOSBoxLocationLabel;
    }
}