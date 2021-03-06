﻿/*AmpShell : .NET front-end for DOSBox
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
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunGameSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenGameFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.QuitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSelectedgameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeConfigurationFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditSelectedcategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectedGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteSelectedCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.PreferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunDOSBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunConfigurationEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDefaultConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportGamesAndCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LargeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SmallIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.CategoryAddButton = new System.Windows.Forms.ToolStripButton();
            this.GameAddButton = new System.Windows.Forms.ToolStripButton();
            this.RunGameButton = new System.Windows.Forms.ToolStripButton();
            this.RunGameSetupButton = new System.Windows.Forms.ToolStripButton();
            this.OpenGameFolderButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.GameEditButton = new System.Windows.Forms.ToolStripButton();
            this.GameEditConfigurationButton = new System.Windows.Forms.ToolStripButton();
            this.MakeConfigButton = new System.Windows.Forms.ToolStripButton();
            this.CategoryEditButton = new System.Windows.Forms.ToolStripButton();
            this.GameDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.CategoryDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.RunDOSBoxButton = new System.Windows.Forms.ToolStripButton();
            this.RunConfigurationEditorButton = new System.Windows.Forms.ToolStripButton();
            this.EditDefaultConfigurationButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.LargeIconViewButton = new System.Windows.Forms.ToolStripButton();
            this.SmallIconViewButton = new System.Windows.Forms.ToolStripButton();
            this.TilesViewButton = new System.Windows.Forms.ToolStripButton();
            this.ListViewButton = new System.Windows.Forms.ToolStripButton();
            this.DetailsViewButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.ExecutablePathLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CMountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SetupPathLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CustomConfigurationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DMountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.NoConsoleLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FullscreenLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.QuitOnExitLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.AdditionalCommandsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.NotesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // menuStrip
            //
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(624, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            //
            // FileToolStripMenuItem
            //
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewCategoryToolStripMenuItem,
            this.NewGameToolStripMenuItem,
            this.RunGameToolStripMenuItem,
            this.RunGameSetupToolStripMenuItem,
            this.OpenGameFolderToolStripMenuItem,
            this.toolStripSeparator1,
            this.QuitterToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "&File";
            this.FileToolStripMenuItem.ToolTipText = "Manage categories and games";
            this.FileToolStripMenuItem.MouseEnter += new System.EventHandler(this.FileToolStripMenuItem_MouseEnter);
            this.FileToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // NewCategoryToolStripMenuItem
            //
            this.NewCategoryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewCategoryToolStripMenuItem.Image")));
            this.NewCategoryToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewCategoryToolStripMenuItem.Name = "NewCategoryToolStripMenuItem";
            this.NewCategoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.NewCategoryToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.NewCategoryToolStripMenuItem.Text = "New &category...";
            this.NewCategoryToolStripMenuItem.ToolTipText = "Add a new category of games";
            this.NewCategoryToolStripMenuItem.Click += new System.EventHandler(this.CategoryAddButton_Click);
            this.NewCategoryToolStripMenuItem.MouseEnter += new System.EventHandler(this.CategoryAddButton_MouseEnter);
            this.NewCategoryToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // NewGameToolStripMenuItem
            //
            this.NewGameToolStripMenuItem.Enabled = false;
            this.NewGameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NewGameToolStripMenuItem.Image")));
            this.NewGameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewGameToolStripMenuItem.Name = "NewGameToolStripMenuItem";
            this.NewGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.NewGameToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.NewGameToolStripMenuItem.Text = "&New game...";
            this.NewGameToolStripMenuItem.ToolTipText = "Add a new game for the current category";
            this.NewGameToolStripMenuItem.Click += new System.EventHandler(this.GameAddButton_Click);
            this.NewGameToolStripMenuItem.MouseEnter += new System.EventHandler(this.GameAddButton_MouseEnter);
            this.NewGameToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunGameToolStripMenuItem
            //
            this.RunGameToolStripMenuItem.Enabled = false;
            this.RunGameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunGameToolStripMenuItem.Image")));
            this.RunGameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.RunGameToolStripMenuItem.Name = "RunGameToolStripMenuItem";
            this.RunGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.RunGameToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.RunGameToolStripMenuItem.Text = "&Run selected game";
            this.RunGameToolStripMenuItem.ToolTipText = "Run the selected game in DOSBox";
            this.RunGameToolStripMenuItem.Click += new System.EventHandler(this.CurrentListView_ItemActivate);
            this.RunGameToolStripMenuItem.MouseEnter += new System.EventHandler(this.RunGameButton_MouseEnter);
            this.RunGameToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunGameSetupToolStripMenuItem
            //
            this.RunGameSetupToolStripMenuItem.Enabled = false;
            this.RunGameSetupToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunGameSetupToolStripMenuItem.Image")));
            this.RunGameSetupToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.RunGameSetupToolStripMenuItem.Name = "RunGameSetupToolStripMenuItem";
            this.RunGameSetupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.RunGameSetupToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.RunGameSetupToolStripMenuItem.Text = "Run game &setup";
            this.RunGameSetupToolStripMenuItem.ToolTipText = "Run the game\'s setup in DOSBox";
            this.RunGameSetupToolStripMenuItem.Click += new System.EventHandler(this.RunGameSetupButton_Click);
            this.RunGameSetupToolStripMenuItem.MouseEnter += new System.EventHandler(this.RunGameSetupButton_MouseEnter);
            this.RunGameSetupToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // OpenGameFolderToolStripMenuItem
            //
            this.OpenGameFolderToolStripMenuItem.Enabled = false;
            this.OpenGameFolderToolStripMenuItem.Image = global::AmpShell.WinForms.Properties.Resources.Folder_Open;
            this.OpenGameFolderToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.OpenGameFolderToolStripMenuItem.Name = "OpenGameFolderToolStripMenuItem";
            this.OpenGameFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.OpenGameFolderToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.OpenGameFolderToolStripMenuItem.Text = "Open game &folder";
            this.OpenGameFolderToolStripMenuItem.ToolTipText = "Open the game\'s folder in Windows Explorer";
            this.OpenGameFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenGameFolderToolStripMenuItem_Click);
            this.OpenGameFolderToolStripMenuItem.MouseEnter += new System.EventHandler(this.OpenGameFolderToolStripMenuItem_MouseEnter);
            this.OpenGameFolderToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(212, 6);
            //
            // QuitterToolStripMenuItem
            //
            this.QuitterToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("QuitterToolStripMenuItem.Image")));
            this.QuitterToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.QuitterToolStripMenuItem.Name = "QuitterToolStripMenuItem";
            this.QuitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.QuitterToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.QuitterToolStripMenuItem.Text = "&Quit";
            this.QuitterToolStripMenuItem.ToolTipText = "Save all the data, and exit AmpShell";
            this.QuitterToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            this.QuitterToolStripMenuItem.MouseEnter += new System.EventHandler(this.QuitterToolStripMenuItem_MouseEnter);
            this.QuitterToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditToolStripMenuItem
            //
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditSelectedgameToolStripMenuItem,
            this.EditConfigToolStripMenuItem,
            this.MakeConfigurationFileToolStripMenuItem,
            this.EditSelectedcategoryToolStripMenuItem,
            this.DeleteSelectedGameToolStripMenuItem,
            this.DeleteSelectedCategoryToolStripMenuItem,
            this.toolStripSeparator2,
            this.PreferencesToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            this.EditToolStripMenuItem.ToolTipText = "Modify categories, games, and preferences";
            this.EditToolStripMenuItem.MouseEnter += new System.EventHandler(this.EditToolStripMenuItem_MouseEnter);
            this.EditToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditSelectedgameToolStripMenuItem
            //
            this.EditSelectedgameToolStripMenuItem.Enabled = false;
            this.EditSelectedgameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("EditSelectedgameToolStripMenuItem.Image")));
            this.EditSelectedgameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditSelectedgameToolStripMenuItem.Name = "EditSelectedgameToolStripMenuItem";
            this.EditSelectedgameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.EditSelectedgameToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.EditSelectedgameToolStripMenuItem.Text = "&Edit selected game...";
            this.EditSelectedgameToolStripMenuItem.ToolTipText = "Modify the selected game";
            this.EditSelectedgameToolStripMenuItem.Click += new System.EventHandler(this.GameEditButton_Click);
            this.EditSelectedgameToolStripMenuItem.MouseEnter += new System.EventHandler(this.GameEditButton_MouseEnter);
            this.EditSelectedgameToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditConfigToolStripMenuItem
            //
            this.EditConfigToolStripMenuItem.Enabled = false;
            this.EditConfigToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("EditConfigToolStripMenuItem.Image")));
            this.EditConfigToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Yellow;
            this.EditConfigToolStripMenuItem.Name = "EditConfigToolStripMenuItem";
            this.EditConfigToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.EditConfigToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.EditConfigToolStripMenuItem.Text = "Edit game configuration &file...";
            this.EditConfigToolStripMenuItem.ToolTipText = "Modify the selected game\'s custom config file";
            this.EditConfigToolStripMenuItem.Click += new System.EventHandler(this.GameEditConfigurationButton_Click);
            this.EditConfigToolStripMenuItem.MouseEnter += new System.EventHandler(this.GameEditConfigurationButton_MouseEnter);
            this.EditConfigToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // MakeConfigurationFileToolStripMenuItem
            //
            this.MakeConfigurationFileToolStripMenuItem.Enabled = false;
            this.MakeConfigurationFileToolStripMenuItem.Image = global::AmpShell.WinForms.Properties.Resources.MakeConfig;
            this.MakeConfigurationFileToolStripMenuItem.Name = "MakeConfigurationFileToolStripMenuItem";
            this.MakeConfigurationFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.MakeConfigurationFileToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.MakeConfigurationFileToolStripMenuItem.Text = "&Make configuration file...";
            this.MakeConfigurationFileToolStripMenuItem.ToolTipText = "Copy the default configuration file into the game\'s directory, to be used as a cu" +
    "stom configuration file";
            this.MakeConfigurationFileToolStripMenuItem.Click += new System.EventHandler(this.MakeConfigButton_Click);
            this.MakeConfigurationFileToolStripMenuItem.MouseEnter += new System.EventHandler(this.MakeConfigurationFileToolStripMenuItem_MouseEnter);
            this.MakeConfigurationFileToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditSelectedcategoryToolStripMenuItem
            //
            this.EditSelectedcategoryToolStripMenuItem.Enabled = false;
            this.EditSelectedcategoryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("EditSelectedcategoryToolStripMenuItem.Image")));
            this.EditSelectedcategoryToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.EditSelectedcategoryToolStripMenuItem.Name = "EditSelectedcategoryToolStripMenuItem";
            this.EditSelectedcategoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.EditSelectedcategoryToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.EditSelectedcategoryToolStripMenuItem.Text = "Edit selected &category...";
            this.EditSelectedcategoryToolStripMenuItem.ToolTipText = "Modify the current category name";
            this.EditSelectedcategoryToolStripMenuItem.Click += new System.EventHandler(this.CategoryEditButton_Click);
            this.EditSelectedcategoryToolStripMenuItem.MouseEnter += new System.EventHandler(this.CategoryEditButton_MouseEnter);
            this.EditSelectedcategoryToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // DeleteSelectedGameToolStripMenuItem
            //
            this.DeleteSelectedGameToolStripMenuItem.Enabled = false;
            this.DeleteSelectedGameToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedGameToolStripMenuItem.Image")));
            this.DeleteSelectedGameToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.DeleteSelectedGameToolStripMenuItem.Name = "DeleteSelectedGameToolStripMenuItem";
            this.DeleteSelectedGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.DeleteSelectedGameToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.DeleteSelectedGameToolStripMenuItem.Text = "&Delete selected game";
            this.DeleteSelectedGameToolStripMenuItem.ToolTipText = "Delete the selected game of the current category";
            this.DeleteSelectedGameToolStripMenuItem.Click += new System.EventHandler(this.GameDeleteButton_Click);
            this.DeleteSelectedGameToolStripMenuItem.MouseEnter += new System.EventHandler(this.GameDeleteButton_MouseEnter);
            this.DeleteSelectedGameToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // DeleteSelectedCategoryToolStripMenuItem
            //
            this.DeleteSelectedCategoryToolStripMenuItem.Enabled = false;
            this.DeleteSelectedCategoryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteSelectedCategoryToolStripMenuItem.Image")));
            this.DeleteSelectedCategoryToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteSelectedCategoryToolStripMenuItem.Name = "DeleteSelectedCategoryToolStripMenuItem";
            this.DeleteSelectedCategoryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.DeleteSelectedCategoryToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.DeleteSelectedCategoryToolStripMenuItem.Text = "Delete &selected category...";
            this.DeleteSelectedCategoryToolStripMenuItem.ToolTipText = "Delete current category and all it\'s games";
            this.DeleteSelectedCategoryToolStripMenuItem.Click += new System.EventHandler(this.CategoryDeleteButton_Click);
            this.DeleteSelectedCategoryToolStripMenuItem.MouseEnter += new System.EventHandler(this.CategoryDeleteButton_MouseEnter);
            this.DeleteSelectedCategoryToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            //
            // PreferencesToolStripMenuItem
            //
            this.PreferencesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PreferencesToolStripMenuItem.Image")));
            this.PreferencesToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.PreferencesToolStripMenuItem.Name = "PreferencesToolStripMenuItem";
            this.PreferencesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.PreferencesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.PreferencesToolStripMenuItem.Text = "&Preferences...";
            this.PreferencesToolStripMenuItem.ToolTipText = "Manage parameters and options";
            this.PreferencesToolStripMenuItem.Click += new System.EventHandler(this.PreferencesToolStripMenuItem_Click);
            this.PreferencesToolStripMenuItem.MouseEnter += new System.EventHandler(this.PreferencesToolStripMenuItem_MouseEnter);
            this.PreferencesToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // ToolsToolStripMenuItem
            //
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunDOSBoxToolStripMenuItem,
            this.RunConfigurationEditorToolStripMenuItem,
            this.EditDefaultConfigurationToolStripMenuItem,
            this.ImportGamesAndCategoriesToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ToolsToolStripMenuItem.Text = "&Tools";
            this.ToolsToolStripMenuItem.ToolTipText = "Run DOSBox or your text editor";
            this.ToolsToolStripMenuItem.MouseEnter += new System.EventHandler(this.ToolsToolStripMenuItem_MouseEnter);
            this.ToolsToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunDOSBoxToolStripMenuItem
            //
            this.RunDOSBoxToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunDOSBoxToolStripMenuItem.Image")));
            this.RunDOSBoxToolStripMenuItem.Name = "RunDOSBoxToolStripMenuItem";
            this.RunDOSBoxToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.RunDOSBoxToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.RunDOSBoxToolStripMenuItem.Text = "&Run DOSBox";
            this.RunDOSBoxToolStripMenuItem.ToolTipText = "Run DOSBox with the default configuration and language files";
            this.RunDOSBoxToolStripMenuItem.Click += new System.EventHandler(this.RunDOSBox_Click);
            this.RunDOSBoxToolStripMenuItem.MouseEnter += new System.EventHandler(this.RunDOSBoxButton_MouseEnter);
            this.RunDOSBoxToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunConfigurationEditorToolStripMenuItem
            //
            this.RunConfigurationEditorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunConfigurationEditorToolStripMenuItem.Image")));
            this.RunConfigurationEditorToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunConfigurationEditorToolStripMenuItem.Name = "RunConfigurationEditorToolStripMenuItem";
            this.RunConfigurationEditorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.RunConfigurationEditorToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.RunConfigurationEditorToolStripMenuItem.Text = "Run configuration &editor";
            this.RunConfigurationEditorToolStripMenuItem.ToolTipText = "Run your text editor";
            this.RunConfigurationEditorToolStripMenuItem.Click += new System.EventHandler(this.RunConfigurationEditorButton_Click);
            this.RunConfigurationEditorToolStripMenuItem.MouseEnter += new System.EventHandler(this.RunConfigurationEditorButton_MouseEnter);
            this.RunConfigurationEditorToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditDefaultConfigurationToolStripMenuItem
            //
            this.EditDefaultConfigurationToolStripMenuItem.Enabled = false;
            this.EditDefaultConfigurationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("EditDefaultConfigurationToolStripMenuItem.Image")));
            this.EditDefaultConfigurationToolStripMenuItem.Name = "EditDefaultConfigurationToolStripMenuItem";
            this.EditDefaultConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.EditDefaultConfigurationToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.EditDefaultConfigurationToolStripMenuItem.Text = "Edit default configuration";
            this.EditDefaultConfigurationToolStripMenuItem.ToolTipText = "Open the default configuration in your text editor";
            this.EditDefaultConfigurationToolStripMenuItem.Click += new System.EventHandler(this.EditDefaultConfigurationToolStripMenuItem_Click);
            this.EditDefaultConfigurationToolStripMenuItem.MouseEnter += new System.EventHandler(this.EditDefaultConfigurationToolStripMenuItem_MouseEnter);
            this.EditDefaultConfigurationToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // ImportGamesAndCategoriesToolStripMenuItem
            //
            this.ImportGamesAndCategoriesToolStripMenuItem.Image = global::AmpShell.WinForms.Properties.Resources.game;
            this.ImportGamesAndCategoriesToolStripMenuItem.Name = "ImportGamesAndCategoriesToolStripMenuItem";
            this.ImportGamesAndCategoriesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.ImportGamesAndCategoriesToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.ImportGamesAndCategoriesToolStripMenuItem.Text = "&Import games and categories...";
            this.ImportGamesAndCategoriesToolStripMenuItem.ToolTipText = "Copy games and categories from a previous AmpShell.xml file...";
            this.ImportGamesAndCategoriesToolStripMenuItem.Click += new System.EventHandler(this.ImportGamesToolStripMenuItem_Click);
            this.ImportGamesAndCategoriesToolStripMenuItem.MouseEnter += new System.EventHandler(this.ImportGamesAndCategoriesToolStripMenuItem_MouseEnter);
            this.ImportGamesAndCategoriesToolStripMenuItem.MouseLeave += new System.EventHandler(this.ImportGamesAndCategoriesToolStripMenuItem_MouseLeave);
            //
            // ViewToolStripMenuItem
            //
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LargeIconToolStripMenuItem,
            this.SmallIconToolStripMenuItem,
            this.TileToolStripMenuItem,
            this.ListToolStripMenuItem,
            this.DetailsToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.ViewToolStripMenuItem.Text = "&View";
            this.ViewToolStripMenuItem.ToolTipText = "Change the current category view";
            this.ViewToolStripMenuItem.MouseEnter += new System.EventHandler(this.ViewToolStripMenuItem_MouseEnter);
            this.ViewToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // LargeIconToolStripMenuItem
            //
            this.LargeIconToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("LargeIconToolStripMenuItem.Image")));
            this.LargeIconToolStripMenuItem.Name = "LargeIconToolStripMenuItem";
            this.LargeIconToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.L)));
            this.LargeIconToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.LargeIconToolStripMenuItem.Text = "&Large icons";
            this.LargeIconToolStripMenuItem.ToolTipText = "The current category will display large icons";
            this.LargeIconToolStripMenuItem.Click += new System.EventHandler(this.LargeIconViewButton_Click);
            this.LargeIconToolStripMenuItem.MouseEnter += new System.EventHandler(this.LargeIconViewButton_MouseEnter);
            this.LargeIconToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // SmallIconToolStripMenuItem
            //
            this.SmallIconToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SmallIconToolStripMenuItem.Image")));
            this.SmallIconToolStripMenuItem.Name = "SmallIconToolStripMenuItem";
            this.SmallIconToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.S)));
            this.SmallIconToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.SmallIconToolStripMenuItem.Text = "&Small icons";
            this.SmallIconToolStripMenuItem.ToolTipText = "The current category will display small icons";
            this.SmallIconToolStripMenuItem.Click += new System.EventHandler(this.SmallIconViewButton_Click);
            this.SmallIconToolStripMenuItem.MouseEnter += new System.EventHandler(this.SmallIconViewButton_MouseEnter);
            this.SmallIconToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // TileToolStripMenuItem
            //
            this.TileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("TileToolStripMenuItem.Image")));
            this.TileToolStripMenuItem.Name = "TileToolStripMenuItem";
            this.TileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.T)));
            this.TileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.TileToolStripMenuItem.Text = "&Tiles";
            this.TileToolStripMenuItem.ToolTipText = "The current category will display tiles";
            this.TileToolStripMenuItem.Click += new System.EventHandler(this.TileViewButton_Click);
            this.TileToolStripMenuItem.MouseEnter += new System.EventHandler(this.TilesViewButton_MouseEnter);
            this.TileToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // ListToolStripMenuItem
            //
            this.ListToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ListToolStripMenuItem.Image")));
            this.ListToolStripMenuItem.Name = "ListToolStripMenuItem";
            this.ListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ListToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ListToolStripMenuItem.Text = "&List";
            this.ListToolStripMenuItem.ToolTipText = "The current category will display a list";
            this.ListToolStripMenuItem.Click += new System.EventHandler(this.ListViewButton_Click);
            this.ListToolStripMenuItem.MouseEnter += new System.EventHandler(this.ListViewButton_MouseEnter);
            this.ListToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // DetailsToolStripMenuItem
            //
            this.DetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DetailsToolStripMenuItem.Image")));
            this.DetailsToolStripMenuItem.Name = "DetailsToolStripMenuItem";
            this.DetailsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D)));
            this.DetailsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.DetailsToolStripMenuItem.Text = "&Details";
            this.DetailsToolStripMenuItem.ToolTipText = "The current category will display games in a list with columns";
            this.DetailsToolStripMenuItem.Click += new System.EventHandler(this.DetailsViewButton_Click);
            this.DetailsToolStripMenuItem.MouseEnter += new System.EventHandler(this.DetailsViewButton_MouseEnter);
            this.DetailsToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // HelpToolStripMenuItem
            //
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            this.HelpToolStripMenuItem.ToolTipText = "Contains the about dialog";
            this.HelpToolStripMenuItem.MouseEnter += new System.EventHandler(this.HelpToolStripMenuItem_MouseEnter);
            this.HelpToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // AboutToolStripMenuItem
            //
            this.AboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AboutToolStripMenuItem.Image")));
            this.AboutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.AboutToolStripMenuItem.Text = "&About...";
            this.AboutToolStripMenuItem.ToolTipText = "Display the about dialog";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            this.AboutToolStripMenuItem.MouseEnter += new System.EventHandler(this.AboutToolStripMenuItem_MouseEnter);
            this.AboutToolStripMenuItem.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStrip
            //
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CategoryAddButton,
            this.GameAddButton,
            this.RunGameButton,
            this.RunGameSetupButton,
            this.OpenGameFolderButton,
            this.toolStripSeparator7,
            this.GameEditButton,
            this.GameEditConfigurationButton,
            this.MakeConfigButton,
            this.CategoryEditButton,
            this.GameDeleteButton,
            this.CategoryDeleteButton,
            this.toolStripSeparator4,
            this.RunDOSBoxButton,
            this.RunConfigurationEditorButton,
            this.EditDefaultConfigurationButton,
            this.toolStripSeparator5,
            this.LargeIconViewButton,
            this.SmallIconViewButton,
            this.TilesViewButton,
            this.ListViewButton,
            this.DetailsViewButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(624, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            //
            // CategoryAddButton
            //
            this.CategoryAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CategoryAddButton.Image = ((System.Drawing.Image)(resources.GetObject("CategoryAddButton.Image")));
            this.CategoryAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CategoryAddButton.Name = "CategoryAddButton";
            this.CategoryAddButton.Size = new System.Drawing.Size(23, 22);
            this.CategoryAddButton.Text = "Add a category...";
            this.CategoryAddButton.ToolTipText = "Add a new category of games";
            this.CategoryAddButton.Click += new System.EventHandler(this.CategoryAddButton_Click);
            this.CategoryAddButton.MouseEnter += new System.EventHandler(this.CategoryAddButton_MouseEnter);
            this.CategoryAddButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // GameAddButton
            //
            this.GameAddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GameAddButton.Enabled = false;
            this.GameAddButton.Image = ((System.Drawing.Image)(resources.GetObject("GameAddButton.Image")));
            this.GameAddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GameAddButton.Name = "GameAddButton";
            this.GameAddButton.Size = new System.Drawing.Size(23, 22);
            this.GameAddButton.Text = "Add a game...";
            this.GameAddButton.ToolTipText = "Add a new game for the current category";
            this.GameAddButton.Click += new System.EventHandler(this.GameAddButton_Click);
            this.GameAddButton.MouseEnter += new System.EventHandler(this.GameAddButton_MouseEnter);
            this.GameAddButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunGameButton
            //
            this.RunGameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunGameButton.Enabled = false;
            this.RunGameButton.Image = ((System.Drawing.Image)(resources.GetObject("RunGameButton.Image")));
            this.RunGameButton.ImageTransparentColor = System.Drawing.Color.White;
            this.RunGameButton.Name = "RunGameButton";
            this.RunGameButton.Size = new System.Drawing.Size(23, 22);
            this.RunGameButton.Text = "Run selected game (Enter)";
            this.RunGameButton.ToolTipText = "Run the selected game in DOSBox";
            this.RunGameButton.Click += new System.EventHandler(this.CurrentListView_ItemActivate);
            this.RunGameButton.MouseEnter += new System.EventHandler(this.RunGameButton_MouseEnter);
            this.RunGameButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunGameSetupButton
            //
            this.RunGameSetupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunGameSetupButton.Enabled = false;
            this.RunGameSetupButton.Image = ((System.Drawing.Image)(resources.GetObject("RunGameSetupButton.Image")));
            this.RunGameSetupButton.ImageTransparentColor = System.Drawing.Color.White;
            this.RunGameSetupButton.Name = "RunGameSetupButton";
            this.RunGameSetupButton.Size = new System.Drawing.Size(23, 22);
            this.RunGameSetupButton.Text = "Run game setup";
            this.RunGameSetupButton.ToolTipText = "Run the game\'s setup in DOSBox";
            this.RunGameSetupButton.Click += new System.EventHandler(this.RunGameSetupButton_Click);
            this.RunGameSetupButton.MouseEnter += new System.EventHandler(this.RunGameSetupButton_MouseEnter);
            this.RunGameSetupButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // OpenGameFolderButton
            //
            this.OpenGameFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenGameFolderButton.Enabled = false;
            this.OpenGameFolderButton.Image = global::AmpShell.WinForms.Properties.Resources.Folder_Open;
            this.OpenGameFolderButton.ImageTransparentColor = System.Drawing.Color.White;
            this.OpenGameFolderButton.Name = "OpenGameFolderButton";
            this.OpenGameFolderButton.Size = new System.Drawing.Size(23, 22);
            this.OpenGameFolderButton.Text = "Open Game Folder";
            this.OpenGameFolderButton.ToolTipText = "Open Game Folder in Windows Explorer";
            this.OpenGameFolderButton.Click += new System.EventHandler(this.OpenGameFolderButton_Click);
            this.OpenGameFolderButton.MouseEnter += new System.EventHandler(this.OpenGameFolderButton_MouseEnter);
            this.OpenGameFolderButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStripSeparator7
            //
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            //
            // GameEditButton
            //
            this.GameEditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GameEditButton.Enabled = false;
            this.GameEditButton.Image = ((System.Drawing.Image)(resources.GetObject("GameEditButton.Image")));
            this.GameEditButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GameEditButton.Name = "GameEditButton";
            this.GameEditButton.Size = new System.Drawing.Size(23, 22);
            this.GameEditButton.Text = "Edit selected game...";
            this.GameEditButton.ToolTipText = "Modify the selected game";
            this.GameEditButton.Click += new System.EventHandler(this.GameEditButton_Click);
            this.GameEditButton.MouseEnter += new System.EventHandler(this.GameEditButton_MouseEnter);
            this.GameEditButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // GameEditConfigurationButton
            //
            this.GameEditConfigurationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GameEditConfigurationButton.Enabled = false;
            this.GameEditConfigurationButton.Image = ((System.Drawing.Image)(resources.GetObject("GameEditConfigurationButton.Image")));
            this.GameEditConfigurationButton.ImageTransparentColor = System.Drawing.Color.Yellow;
            this.GameEditConfigurationButton.Name = "GameEditConfigurationButton";
            this.GameEditConfigurationButton.Size = new System.Drawing.Size(23, 22);
            this.GameEditConfigurationButton.Text = "Edit game configuration file...";
            this.GameEditConfigurationButton.ToolTipText = "Modify the selected game\'s custom config file";
            this.GameEditConfigurationButton.Click += new System.EventHandler(this.GameEditConfigurationButton_Click);
            this.GameEditConfigurationButton.MouseEnter += new System.EventHandler(this.GameEditConfigurationButton_MouseEnter);
            this.GameEditConfigurationButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // MakeConfigButton
            //
            this.MakeConfigButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MakeConfigButton.Enabled = false;
            this.MakeConfigButton.Image = global::AmpShell.WinForms.Properties.Resources.MakeConfig;
            this.MakeConfigButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MakeConfigButton.Name = "MakeConfigButton";
            this.MakeConfigButton.Size = new System.Drawing.Size(23, 22);
            this.MakeConfigButton.Text = "Make configuration file";
            this.MakeConfigButton.ToolTipText = "Copy the default configuration file into the game\'s directory, to be used as a cu" +
    "stom configuration file";
            this.MakeConfigButton.Click += new System.EventHandler(this.MakeConfigButton_Click);
            this.MakeConfigButton.MouseEnter += new System.EventHandler(this.MakeConfigurationFileToolStripMenuItem_MouseEnter);
            this.MakeConfigButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // CategoryEditButton
            //
            this.CategoryEditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CategoryEditButton.Enabled = false;
            this.CategoryEditButton.Image = ((System.Drawing.Image)(resources.GetObject("CategoryEditButton.Image")));
            this.CategoryEditButton.ImageTransparentColor = System.Drawing.Color.White;
            this.CategoryEditButton.Name = "CategoryEditButton";
            this.CategoryEditButton.Size = new System.Drawing.Size(23, 22);
            this.CategoryEditButton.Text = "Edit selected category...";
            this.CategoryEditButton.ToolTipText = "Modify the current category name";
            this.CategoryEditButton.Click += new System.EventHandler(this.CategoryEditButton_Click);
            this.CategoryEditButton.MouseEnter += new System.EventHandler(this.CategoryEditButton_MouseEnter);
            this.CategoryEditButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // GameDeleteButton
            //
            this.GameDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GameDeleteButton.Enabled = false;
            this.GameDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("GameDeleteButton.Image")));
            this.GameDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GameDeleteButton.Name = "GameDeleteButton";
            this.GameDeleteButton.Size = new System.Drawing.Size(23, 22);
            this.GameDeleteButton.Text = "Delete selected game";
            this.GameDeleteButton.ToolTipText = "Delete the selected game of the current category";
            this.GameDeleteButton.Click += new System.EventHandler(this.GameDeleteButton_Click);
            this.GameDeleteButton.MouseEnter += new System.EventHandler(this.GameDeleteButton_MouseEnter);
            this.GameDeleteButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // CategoryDeleteButton
            //
            this.CategoryDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CategoryDeleteButton.Enabled = false;
            this.CategoryDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("CategoryDeleteButton.Image")));
            this.CategoryDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CategoryDeleteButton.Name = "CategoryDeleteButton";
            this.CategoryDeleteButton.Size = new System.Drawing.Size(23, 22);
            this.CategoryDeleteButton.Text = "Delete selected category...";
            this.CategoryDeleteButton.ToolTipText = "Delete current category and all it\'s games";
            this.CategoryDeleteButton.Click += new System.EventHandler(this.CategoryDeleteButton_Click);
            this.CategoryDeleteButton.MouseEnter += new System.EventHandler(this.CategoryDeleteButton_MouseEnter);
            this.CategoryDeleteButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStripSeparator4
            //
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            //
            // RunDOSBoxButton
            //
            this.RunDOSBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunDOSBoxButton.Image = ((System.Drawing.Image)(resources.GetObject("RunDOSBoxButton.Image")));
            this.RunDOSBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunDOSBoxButton.Name = "RunDOSBoxButton";
            this.RunDOSBoxButton.Size = new System.Drawing.Size(23, 22);
            this.RunDOSBoxButton.Text = "Run DOSBox";
            this.RunDOSBoxButton.ToolTipText = "Run DOSBox with the default configuration and language files";
            this.RunDOSBoxButton.Click += new System.EventHandler(this.RunDOSBox_Click);
            this.RunDOSBoxButton.MouseEnter += new System.EventHandler(this.RunDOSBoxButton_MouseEnter);
            this.RunDOSBoxButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // RunConfigurationEditorButton
            //
            this.RunConfigurationEditorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunConfigurationEditorButton.Image = ((System.Drawing.Image)(resources.GetObject("RunConfigurationEditorButton.Image")));
            this.RunConfigurationEditorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunConfigurationEditorButton.Name = "RunConfigurationEditorButton";
            this.RunConfigurationEditorButton.Size = new System.Drawing.Size(23, 22);
            this.RunConfigurationEditorButton.Text = "Run configuration editor";
            this.RunConfigurationEditorButton.ToolTipText = "Run your text editor";
            this.RunConfigurationEditorButton.Click += new System.EventHandler(this.RunConfigurationEditorButton_Click);
            this.RunConfigurationEditorButton.MouseEnter += new System.EventHandler(this.RunConfigurationEditorButton_MouseEnter);
            this.RunConfigurationEditorButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // EditDefaultConfigurationButton
            //
            this.EditDefaultConfigurationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditDefaultConfigurationButton.Enabled = false;
            this.EditDefaultConfigurationButton.Image = global::AmpShell.WinForms.Properties.Resources.DBConfEdit;
            this.EditDefaultConfigurationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditDefaultConfigurationButton.Name = "EditDefaultConfigurationButton";
            this.EditDefaultConfigurationButton.Size = new System.Drawing.Size(23, 22);
            this.EditDefaultConfigurationButton.Text = "Edit default configuration";
            this.EditDefaultConfigurationButton.ToolTipText = "Open the default configuration in your text editor";
            this.EditDefaultConfigurationButton.Click += new System.EventHandler(this.EditDefaultConfigurationToolStripMenuItem_Click);
            this.EditDefaultConfigurationButton.MouseEnter += new System.EventHandler(this.EditDefaultConfigurationToolStripMenuItem_MouseEnter);
            this.EditDefaultConfigurationButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // toolStripSeparator5
            //
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            //
            // LargeIconViewButton
            //
            this.LargeIconViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LargeIconViewButton.Image = ((System.Drawing.Image)(resources.GetObject("LargeIconViewButton.Image")));
            this.LargeIconViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LargeIconViewButton.Name = "LargeIconViewButton";
            this.LargeIconViewButton.Size = new System.Drawing.Size(23, 22);
            this.LargeIconViewButton.Text = "Large icons";
            this.LargeIconViewButton.ToolTipText = "The current category will display large icons";
            this.LargeIconViewButton.Click += new System.EventHandler(this.LargeIconViewButton_Click);
            this.LargeIconViewButton.MouseEnter += new System.EventHandler(this.LargeIconViewButton_MouseEnter);
            this.LargeIconViewButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // SmallIconViewButton
            //
            this.SmallIconViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SmallIconViewButton.Image = ((System.Drawing.Image)(resources.GetObject("SmallIconViewButton.Image")));
            this.SmallIconViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SmallIconViewButton.Name = "SmallIconViewButton";
            this.SmallIconViewButton.Size = new System.Drawing.Size(23, 22);
            this.SmallIconViewButton.Text = "Small icons";
            this.SmallIconViewButton.ToolTipText = "The current category will display small icons";
            this.SmallIconViewButton.Click += new System.EventHandler(this.SmallIconViewButton_Click);
            this.SmallIconViewButton.MouseEnter += new System.EventHandler(this.SmallIconViewButton_MouseEnter);
            this.SmallIconViewButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // TilesViewButton
            //
            this.TilesViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TilesViewButton.Image = ((System.Drawing.Image)(resources.GetObject("TilesViewButton.Image")));
            this.TilesViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TilesViewButton.Name = "TilesViewButton";
            this.TilesViewButton.Size = new System.Drawing.Size(23, 22);
            this.TilesViewButton.Text = "Tiles";
            this.TilesViewButton.ToolTipText = "The current category will display tiles";
            this.TilesViewButton.Click += new System.EventHandler(this.TileViewButton_Click);
            this.TilesViewButton.MouseEnter += new System.EventHandler(this.TilesViewButton_MouseEnter);
            this.TilesViewButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // ListViewButton
            //
            this.ListViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ListViewButton.Image = ((System.Drawing.Image)(resources.GetObject("ListViewButton.Image")));
            this.ListViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ListViewButton.Name = "ListViewButton";
            this.ListViewButton.Size = new System.Drawing.Size(23, 22);
            this.ListViewButton.Text = "List";
            this.ListViewButton.ToolTipText = "The current category will display a list";
            this.ListViewButton.Click += new System.EventHandler(this.ListViewButton_Click);
            this.ListViewButton.MouseEnter += new System.EventHandler(this.ListViewButton_MouseEnter);
            this.ListViewButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // DetailsViewButton
            //
            this.DetailsViewButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DetailsViewButton.Image = ((System.Drawing.Image)(resources.GetObject("DetailsViewButton.Image")));
            this.DetailsViewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DetailsViewButton.Name = "DetailsViewButton";
            this.DetailsViewButton.Size = new System.Drawing.Size(23, 22);
            this.DetailsViewButton.Text = "Details";
            this.DetailsViewButton.ToolTipText = "The current category will display games in a list with columns";
            this.DetailsViewButton.Click += new System.EventHandler(this.DetailsViewButton_Click);
            this.DetailsViewButton.MouseEnter += new System.EventHandler(this.DetailsViewButton_MouseEnter);
            this.DetailsViewButton.MouseLeave += new System.EventHandler(this.CurrentListView_ItemSelectionChanged);
            //
            // statusStrip
            //
            this.statusStrip.AllowMerge = false;
            this.statusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExecutablePathLabel,
            this.CMountLabel,
            this.SetupPathLabel,
            this.CustomConfigurationLabel,
            this.DMountLabel,
            this.NoConsoleLabel,
            this.FullscreenLabel,
            this.QuitOnExitLabel,
            this.AdditionalCommandsLabel,
            this.NotesLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 339);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(624, 22);
            this.statusStrip.TabIndex = 8;
            //
            // ExecutablePathLabel
            //
            this.ExecutablePathLabel.Name = "ExecutablePathLabel";
            this.ExecutablePathLabel.Size = new System.Drawing.Size(0, 17);
            //
            // CMountLabel
            //
            this.CMountLabel.Name = "CMountLabel";
            this.CMountLabel.Size = new System.Drawing.Size(0, 17);
            //
            // SetupPathLabel
            //
            this.SetupPathLabel.Name = "SetupPathLabel";
            this.SetupPathLabel.Size = new System.Drawing.Size(0, 17);
            //
            // CustomConfigurationLabel
            //
            this.CustomConfigurationLabel.Name = "CustomConfigurationLabel";
            this.CustomConfigurationLabel.Size = new System.Drawing.Size(0, 17);
            //
            // DMountLabel
            //
            this.DMountLabel.Name = "DMountLabel";
            this.DMountLabel.Size = new System.Drawing.Size(0, 17);
            //
            // NoConsoleLabel
            //
            this.NoConsoleLabel.Name = "NoConsoleLabel";
            this.NoConsoleLabel.Size = new System.Drawing.Size(0, 17);
            //
            // FullscreenLabel
            //
            this.FullscreenLabel.Name = "FullscreenLabel";
            this.FullscreenLabel.Size = new System.Drawing.Size(0, 17);
            //
            // QuitOnExitLabel
            //
            this.QuitOnExitLabel.Name = "QuitOnExitLabel";
            this.QuitOnExitLabel.Size = new System.Drawing.Size(0, 17);
            //
            // AdditionalCommandsLabel
            //
            this.AdditionalCommandsLabel.Name = "AdditionalCommandsLabel";
            this.AdditionalCommandsLabel.Size = new System.Drawing.Size(0, 17);
            //
            // NotesLabel
            //
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(0, 17);
            //
            // TabControl
            //
            this.TabControl.AllowDrop = true;
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.HotTrack = true;
            this.TabControl.Location = new System.Drawing.Point(0, 49);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(624, 290);
            this.TabControl.TabIndex = 1;
            //
            // MainForm
            //
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "AmpShell";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AmpShell_FormClosing);
            this.Shown += new System.EventHandler(this.AmpShell_Shown);
            this.ResizeEnd += new System.EventHandler(this.AmpShell_Resized);
            this.LocationChanged += new System.EventHandler(this.AmpShell_LocationChanged);
            this.Resize += new System.EventHandler(this.AmpShell_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem NewCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunGameSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem QuitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditSelectedgameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditSelectedcategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectedGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectedCategoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem PreferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunDOSBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunConfigurationEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton CategoryAddButton;
        private System.Windows.Forms.ToolStripButton GameAddButton;
        private System.Windows.Forms.ToolStripButton RunGameButton;
        private System.Windows.Forms.ToolStripButton RunGameSetupButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton CategoryEditButton;
        private System.Windows.Forms.ToolStripButton GameEditButton;
        private System.Windows.Forms.ToolStripButton GameEditConfigurationButton;
        private System.Windows.Forms.ToolStripButton GameDeleteButton;
        private System.Windows.Forms.ToolStripButton CategoryDeleteButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton RunDOSBoxButton;
        private System.Windows.Forms.ToolStripButton RunConfigurationEditorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton SmallIconViewButton;
        private System.Windows.Forms.ToolStripButton LargeIconViewButton;
        private System.Windows.Forms.ToolStripButton TilesViewButton;
        private System.Windows.Forms.ToolStripButton ListViewButton;
        private System.Windows.Forms.ToolStripButton DetailsViewButton;
        private System.Windows.Forms.ToolStripStatusLabel ExecutablePathLabel;
        private System.Windows.Forms.ToolStripStatusLabel CMountLabel;
        private System.Windows.Forms.ToolStripStatusLabel SetupPathLabel;
        private System.Windows.Forms.ToolStripStatusLabel CustomConfigurationLabel;
        private System.Windows.Forms.ToolStripStatusLabel DMountLabel;
        private System.Windows.Forms.ToolStripStatusLabel NoConsoleLabel;
        private System.Windows.Forms.ToolStripStatusLabel FullscreenLabel;
        private System.Windows.Forms.ToolStripStatusLabel QuitOnExitLabel;
        private System.Windows.Forms.ToolStripStatusLabel AdditionalCommandsLabel;
        private System.Windows.Forms.ToolStripStatusLabel NotesLabel;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LargeIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SmallIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditDefaultConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton MakeConfigButton;
        private System.Windows.Forms.ToolStripButton EditDefaultConfigurationButton;
        private System.Windows.Forms.ToolStripMenuItem MakeConfigurationFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton OpenGameFolderButton;
        private System.Windows.Forms.ToolStripMenuItem OpenGameFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportGamesAndCategoriesToolStripMenuItem;
    }
}