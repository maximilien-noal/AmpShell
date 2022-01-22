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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using View = System.Windows.Forms.View;

    using AmpShell.Core.Games;
    using AmpShell.Core.Model;
    using AmpShell.WinForms.Views.UserControls;
    using AmpShell.WinShell;
    using AmpShell.Core.Config;

    /// <summary> MainWindow content. </summary>
    public partial class MainForm : Form
    {
        private const string ListViewName = "GamesListView";

        private readonly Timer redrawWaitTimer = new Timer();

        private ToolStripMenuItem addCategoryMenuItem;

        private ToolStripMenuItem addCategoryMenuMenuItem;

        private ToolStripMenuItem addGameMenuItem;

        /// <summary> Context Menu for the ListView. </summary>
        private ContextMenuStrip currentListViewContextMenuStrip;

        private ToolStripMenuItem deleteCategoryMenuItem;

        private ToolStripMenuItem deleteCategoryMenuMenuItem;

        private ToolStripMenuItem deleteGameMenuItem;

        private ToolStripMenuItem editCategoryMenuItem;

        private ToolStripMenuItem editCategoryMenuMenuItem;

        private ToolStripMenuItem editGameConfigurationMenuItem;

        private ToolStripMenuItem editGameMenuItem;

        private ToolStripMenuItem gamePropertiesMenuItem;

        private ImageList gamesLargeImageList;

        private ImageList gamesMediumImageList;

        private ImageList gamesSmallImageList;

        private int hoveredTabIndex;

        private bool mainWindowInitialized;

        private ToolStripMenuItem makeGameConfigurationMenuItem;

        private ToolStripMenuItem menuBarMenuItem;

        private ToolStripMenuItem openGameFolderMenuItem;

        private List<TabPage> redrawableTabs = new List<TabPage>();

        private ToolStripMenuItem runGameMenuItem;

        private ToolStripMenuItem runGameSetupMenuItem;

        private ToolStripMenuItem statusBarMenuItem;

        /// <summary> Context Menu for the TabPages. </summary>
        private ContextMenuStrip tabContextMenuStrip;

        private ToolStripMenuItem toolBarMenuItem;

        /// <summary> Top-level context menu. </summary>
        private ContextMenuStrip windowContextMenuStrip;

        /// <summary> Initializes a new instance of the <see cref="MainForm" /> class. </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.SelectedListView.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.CurrentListView_ColumnWidthChanged);
        }

        /// <summary>
        /// Gets the <see cref="ListView" /> instance used mainly to retrieve the current ListView
        /// (in tabcontrol.SelectedTab["GamesListView"]).
        /// </summary>
        private ListView SelectedListView
        {
            get
            {
                if (this.TabControl.HasChildren == false)
                {
                    return new ListView();
                }
                if (this.TabControl.SelectedTab == null)
                {
                    return new ListView();
                }
                return (ListView)this.TabControl.SelectedTab.Controls[ListViewName];
            }
        }

        private static void AwaitBackgroundWorker(BackgroundWorker worker)
        {
            worker.RunWorkerAsync();
            while (worker.IsBusy)
            {
                Application.DoEvents();
            }
        }

        /// <summary> EventHandler for the ? -&gt; About button. </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog(this);
            }
        }

        private void AboutToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.AboutToolStripMenuItem.ToolTipText);

        /// <summary> EventHandler for when AmpShell is closed. </summary>
        private void AmpShell_FormClosing(object sender, FormClosingEventArgs e) => Program.UserDataAccessorInstance.SaveUserData();

        /// <summary> EventHandler for when the window is moved. </summary>
        private void AmpShell_LocationChanged(object sender, EventArgs e)
        {
            if (this.mainWindowInitialized && this.WindowState != FormWindowState.Minimized)
            {
                Program.UserDataAccessorInstance.UpdateWindowLocation(this.Location);
            }
        }

        /// <summary> EventHandler for when the window is (un)maximized. </summary>
        private void AmpShell_Resize(object sender, EventArgs e)
        {
            if (this.mainWindowInitialized == false)
            {
                return;
            }
            Program.UserDataAccessorInstance.UpdateIsWindowFullscreen(this.WindowState == FormWindowState.Maximized);
        }

        /// <summary> EventHandler for when the user has finished resizing the window. </summary>
        private void AmpShell_Resized(object sender, EventArgs e) => Program.UserDataAccessorInstance.UpdateWindowSize(this.Width, this.Height);

        /// <summary> EventHandler for when AmpShell is shown (happens after AmpShell_Load). </summary>
        private void AmpShell_Shown(object sender, EventArgs e)
        {
            this.mainWindowInitialized = true;
            this.CreateAndPopulateContextMenus();
            this.DisplayUserData();
            this.redrawableTabs = this.TabControl.TabPages.Cast<TabPage>().Where(x => ((ListView)x.Controls[ListViewName]).Items.Count == 0).ToList();

            //select the first TabPage of tabcontrol
            if (this.TabControl.HasChildren)
            {
                //select the first TabPage
                this.TabControl.SelectedTab = this.TabControl.TabPages[0];
                this.CategoryEditButton.Enabled = true;
                this.EditSelectedcategoryToolStripMenuItem.Enabled = true;
                this.editCategoryMenuMenuItem.Enabled = true;
                this.deleteCategoryMenuMenuItem.Enabled = true;
                this.CategoryDeleteButton.Enabled = true;
                this.DeleteSelectedCategoryToolStripMenuItem.Enabled = true;
            }

            //if tabcontrol has no children, then it has no TabPages (categories)
            //so we prompt the user for the title of the first category.
            else
            {
                this.CategoryAddButton_Click(sender, e);
            }
        }

        /// <summary> EventHandler when a Category (a TabPage) is added (created). </summary>
        private void CategoryAddButton_Click(object sender, EventArgs e)
        {
            using (var newCategoryForm = new CategoryForm())
            {
                if (newCategoryForm.ShowDialog(this) == DialogResult.OK)
                {
                    this.RedrawAllUserData();
                    if (this.TabControl.HasChildren && this.TabControl.TabCount == 1 && this.TabControl.SelectedTab == null)
                    {
                        this.TabControl.SelectedTab = this.TabControl.TabPages[0];
                    }
                }
            }
        }

        private void CategoryAddButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.CategoryAddButton.ToolTipText);

        /// <summary> EventHandler for when the Category delete button is clicked. </summary>
        private void CategoryDeleteButton_Click(object sender, EventArgs e)
        {
            Category selectedCategory = this.GetSelectedCategory();
            if (Program.UserDataAccessorInstance.GetUserData().CategoryDeletePrompt != true ||
                MessageBox.Show(
                    this,
                    $"Do you really want to delete '{selectedCategory.Title}' and all the games inside it ?",
                    this.deleteCategoryMenuMenuItem.Text,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.UserDataAccessorInstance.DeleteCategory(selectedCategory);
                this.TabControl.TabPages.Remove(this.TabControl.SelectedTab);
            }
            this.UpdateButtonsState();
        }

        private void CategoryDeleteButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.CategoryDeleteButton.ToolTipText);

        /// <summary>
        /// EventHandler for when a category is edited (CategoryEditButton has been clicked).
        /// </summary>
        private void CategoryEditButton_Click(object sender, EventArgs e)
        {
            if (this.TabControl.SelectedTab == null)
            {
                return;
            }
            using (var catEditForm = new CategoryForm((string)this.TabControl.SelectedTab.Tag))
            {
                if (catEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    this.TabControl.SelectedTab.Text = catEditForm.ViewModel.Name;
                }
            }
        }

        private void CategoryEditButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.CategoryEditButton.ToolTipText);

        private void ChangeTabDisplayMode(View mode)
        {
            var selectedIndex = this.TabControl.SelectedIndex;
            this.GetSelectedCategory().ViewMode = (Core.Model.View)mode;
            this.RedrawAllUserData();
            this.TabControl.SelectedIndex = selectedIndex;
        }

        private void CreateAndPopulateContextMenus()
        {
            this.addCategoryMenuMenuItem = new ToolStripMenuItem();
            this.deleteCategoryMenuMenuItem = new ToolStripMenuItem();
            this.editCategoryMenuMenuItem = new ToolStripMenuItem();
            this.addCategoryMenuItem = new ToolStripMenuItem();
            this.deleteCategoryMenuItem = new ToolStripMenuItem();
            this.editCategoryMenuItem = new ToolStripMenuItem();
            this.addGameMenuItem = new ToolStripMenuItem();
            this.deleteGameMenuItem = new ToolStripMenuItem();
            this.editGameMenuItem = new ToolStripMenuItem();
            this.editGameConfigurationMenuItem = new ToolStripMenuItem();
            this.makeGameConfigurationMenuItem = new ToolStripMenuItem();
            this.runGameMenuItem = new ToolStripMenuItem();
            this.openGameFolderMenuItem = new ToolStripMenuItem();
            this.gamePropertiesMenuItem = new ToolStripMenuItem();
            this.runGameSetupMenuItem = new ToolStripMenuItem();
            this.menuBarMenuItem = new ToolStripMenuItem("Menu bar");
            this.toolBarMenuItem = new ToolStripMenuItem("Tool bar");
            this.statusBarMenuItem = new ToolStripMenuItem("Details bar");
            this.menuBarMenuItem.Click += new EventHandler(this.MenuBar_ContextMenu_Click);
            this.toolBarMenuItem.Click += new EventHandler(this.ToolBar_ContextMenu_Click);
            this.statusBarMenuItem.Click += new EventHandler(this.StatusBar_ContextMenu_Click);
            this.windowContextMenuStrip = new ContextMenuStrip();
            this.windowContextMenuStrip.Items.Add(this.menuBarMenuItem);
            this.windowContextMenuStrip.Items.Add(this.toolBarMenuItem);
            this.windowContextMenuStrip.Items.Add(this.statusBarMenuItem);
            this.ContextMenuStrip = this.windowContextMenuStrip;
            this.TabControl.AllowDrop = true;

            //adding text, images, and EventHandlers to the context pop-up menu
            this.addGameMenuItem.Image = this.GameAddButton.Image;
            this.addGameMenuItem.Text = this.GameAddButton.Text;
            this.addGameMenuItem.Click += new EventHandler(this.GameAddButton_Click);
            this.addGameMenuItem.MouseEnter += new EventHandler(this.GameAddButton_MouseEnter);
            this.addGameMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.currentListViewContextMenuStrip = new ContextMenuStrip();
            this.currentListViewContextMenuStrip.Items.Add(this.addGameMenuItem);
            this.runGameMenuItem.Image = this.RunGameButton.Image;
            this.runGameMenuItem.Text = this.RunGameButton.Text;
            this.runGameMenuItem.Click += new EventHandler(this.CurrentListView_ItemActivate);
            this.runGameMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.runGameMenuItem.MouseEnter += new EventHandler(this.RunGameButton_MouseEnter);

            this.openGameFolderMenuItem.Image = this.OpenGameFolderButton.Image;
            this.openGameFolderMenuItem.Text = this.OpenGameFolderButton.Text;
            this.openGameFolderMenuItem.Click += (s, e) => this.OpenGameFolderButton_Click(s, e);
            this.openGameFolderMenuItem.MouseLeave += (s, e) => this.CurrentListView_ItemSelectionChanged(s, e);
            this.openGameFolderMenuItem.MouseEnter += (s, e) => this.DisplayHelpMessage(this.OpenGameFolderButton.ToolTipText);

            //Only Enabled when a game is selected
            this.openGameFolderMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.openGameFolderMenuItem);

            //Only Enabled when a game is selected
            this.runGameMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.runGameMenuItem);
            this.runGameSetupMenuItem.Image = this.RunGameSetupButton.Image;
            this.runGameSetupMenuItem.Text = this.RunGameSetupButton.Text;
            this.runGameSetupMenuItem.Click += new EventHandler(this.RunGameSetupButton_Click);
            this.runGameSetupMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.runGameSetupMenuItem.MouseEnter += new EventHandler(this.RunGameSetupButton_MouseEnter);

            //Only Enabled when a game is selected
            this.runGameSetupMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.runGameSetupMenuItem);
            this.deleteGameMenuItem.Image = this.GameDeleteButton.Image;
            this.deleteGameMenuItem.Text = this.GameDeleteButton.Text;
            this.deleteGameMenuItem.Click += new EventHandler(this.GameDeleteButton_Click);
            this.deleteGameMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.deleteGameMenuItem.MouseEnter += new EventHandler(this.GameDeleteButton_MouseEnter);

            //Only Enabled when a game is selected
            this.deleteGameMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.deleteGameMenuItem);
            this.editGameMenuItem.Image = this.GameEditButton.Image;
            this.editGameMenuItem.Text = this.GameEditButton.Text;
            this.editGameMenuItem.Click += new EventHandler(this.GameEditButton_Click);
            this.editGameMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.editGameMenuItem.MouseEnter += new EventHandler(this.GameEditButton_MouseEnter);

            //Only Enabled when a game is selected
            this.editGameMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.editGameMenuItem);
            if (Program.UserDataAccessorInstance.GetUserData().GamesUseDOSBox)
            {
                this.editGameConfigurationMenuItem.Image = this.GameEditConfigurationButton.Image;
                this.editGameConfigurationMenuItem.Text = this.GameEditConfigurationButton.Text;
                this.editGameConfigurationMenuItem.Click += new EventHandler(this.GameEditConfigurationButton_Click);
                this.editGameConfigurationMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
                this.editGameConfigurationMenuItem.MouseEnter += new EventHandler(this.GameEditConfigurationButton_MouseEnter);

                //Only Enabled when a game is selected
                this.editGameConfigurationMenuItem.Enabled = false;
                this.currentListViewContextMenuStrip.Items.Add(this.editGameConfigurationMenuItem);
                this.makeGameConfigurationMenuItem.Image = this.MakeConfigButton.Image;
                this.makeGameConfigurationMenuItem.Text = this.MakeConfigButton.Text;
                this.makeGameConfigurationMenuItem.Click += new EventHandler(this.MakeConfigButton_Click);
                this.makeGameConfigurationMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
                this.makeGameConfigurationMenuItem.MouseEnter += new EventHandler(this.MakeConfigurationFileToolStripMenuItem_MouseEnter);

                //Only Enabled when a game is selected
                this.makeGameConfigurationMenuItem.Enabled = false;
                this.currentListViewContextMenuStrip.Items.Add(this.makeGameConfigurationMenuItem);
            }
            this.currentListViewContextMenuStrip.Items.Add(new ToolStripSeparator());

            //The Categories are the tabs inside the this.TabControl. Each tab has only one ListView.
            this.addCategoryMenuMenuItem.Image = this.CategoryAddButton.Image;
            this.addCategoryMenuMenuItem.Text = this.CategoryAddButton.Text;
            this.addCategoryMenuMenuItem.Click += new EventHandler(this.CategoryAddButton_Click);
            this.addCategoryMenuMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.addCategoryMenuMenuItem.MouseEnter += new EventHandler(this.CategoryAddButton_MouseEnter);
            this.addCategoryMenuItem.Image = this.CategoryAddButton.Image;
            this.addCategoryMenuItem.Text = this.CategoryAddButton.Text;
            this.addCategoryMenuItem.Click += new EventHandler(this.CategoryAddButton_Click);
            this.addCategoryMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.addCategoryMenuItem.MouseEnter += new EventHandler(this.CategoryAddButton_MouseEnter);
            this.currentListViewContextMenuStrip.Items.Add(this.addCategoryMenuMenuItem);
            this.tabContextMenuStrip = new ContextMenuStrip();
            this.tabContextMenuStrip.Items.Add(this.addCategoryMenuItem);
            this.editCategoryMenuMenuItem.Image = this.CategoryEditButton.Image;
            this.editCategoryMenuMenuItem.Text = this.CategoryEditButton.Text;
            this.editCategoryMenuMenuItem.Click += new EventHandler(this.CategoryEditButton_Click);
            this.editCategoryMenuMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.editCategoryMenuMenuItem.MouseEnter += new EventHandler(this.CategoryEditButton_MouseEnter);
            this.editCategoryMenuItem.Image = this.CategoryEditButton.Image;
            this.editCategoryMenuItem.Text = this.CategoryEditButton.Text;
            this.editCategoryMenuItem.Click += new EventHandler(this.CategoryEditButton_Click);
            this.editCategoryMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.editCategoryMenuItem.MouseEnter += new EventHandler(this.CategoryEditButton_MouseEnter);
            this.currentListViewContextMenuStrip.Items.Add(this.editCategoryMenuMenuItem);
            this.tabContextMenuStrip.Items.Add(this.editCategoryMenuItem);
            this.deleteCategoryMenuMenuItem.Image = this.CategoryDeleteButton.Image;
            this.deleteCategoryMenuMenuItem.Text = this.CategoryDeleteButton.Text;
            this.deleteCategoryMenuMenuItem.Click += new EventHandler(this.CategoryDeleteButton_Click);
            this.deleteCategoryMenuMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.deleteCategoryMenuMenuItem.MouseEnter += new EventHandler(this.CategoryDeleteButton_MouseEnter);
            this.deleteCategoryMenuItem.Image = this.CategoryDeleteButton.Image;
            this.deleteCategoryMenuItem.Text = this.CategoryDeleteButton.Text;
            this.deleteCategoryMenuItem.Click += new EventHandler(this.CategoryDeleteButton_Click);
            this.deleteCategoryMenuItem.MouseLeave += new EventHandler(this.CurrentListView_ItemSelectionChanged);
            this.deleteCategoryMenuItem.MouseEnter += new EventHandler(this.CategoryDeleteButton_MouseEnter);
            this.currentListViewContextMenuStrip.Items.Add(this.deleteCategoryMenuMenuItem);

            this.currentListViewContextMenuStrip.Items.Add(new ToolStripSeparator());

            this.gamePropertiesMenuItem.Text = "Properties";
            this.gamePropertiesMenuItem.Click += (s, e) => this.ViewGameProperties_Click(s, e);
            this.gamePropertiesMenuItem.MouseLeave += (s, e) => this.CurrentListView_ItemSelectionChanged(s, e);
            this.gamePropertiesMenuItem.MouseEnter += (s, e) => this.DisplayHelpMessage("View the target file's properties");

            //Only Enabled when a game is selected
            this.gamePropertiesMenuItem.Enabled = false;
            this.currentListViewContextMenuStrip.Items.Add(this.gamePropertiesMenuItem);

            this.tabContextMenuStrip.Items.Add(this.deleteCategoryMenuItem);
            this.TabControl.ContextMenuStrip = this.tabContextMenuStrip;
        }

        private void CurrentListView_ColumnWidthChanged(object sender, EventArgs e)
        {
            var category = this.GetSelectedCategory();
            if (((View)category.ViewMode) != View.Details || this.SelectedListView.Columns.Count == 0)
            {
                return;
            }
            category.NameColumnWidth = this.SelectedListView.Columns["NameColumn"].Width;
            category.ReleaseDateColumnWidth = this.SelectedListView.Columns["ReleaseDateColumn"].Width;
            category.ExecutableColumnWidth = this.SelectedListView.Columns["ExecutableColumn"].Width;
            category.CMountColumnWidth = this.SelectedListView.Columns["CMountColumn"].Width;
            category.SetupExecutableColumnWidth = this.SelectedListView.Columns["SetupExecutableColumn"].Width;
            category.CustomConfigurationColumnWidth = this.SelectedListView.Columns["CustomConfigurationColumn"].Width;
            category.DMountColumnWidth = this.SelectedListView.Columns["DMountColumn"].Width;
            category.MountingOptionsColumnWidth = this.SelectedListView.Columns["MountingOptionsColumn"].Width;
            category.AdditionnalCommandsColumnWidth = this.SelectedListView.Columns["AdditionalCommandsColumn"].Width;
            category.NoConsoleColumnWidth = this.SelectedListView.Columns["NoConsoleColumn"].Width;
            category.FullscreenColumnWidth = this.SelectedListView.Columns["FullscreenColumn"].Width;
            category.QuitOnExitColumnWidth = this.SelectedListView.Columns["QuitOnExitColumn"].Width;
            category.NotesColumnWidth = this.SelectedListView.Columns["NotesColumn"].Width;
        }

        private void CurrentListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                this.GameAddButton_Click(this, e);
            }
        }

        private void CurrentListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// EventHandler for when a game is double-clicked (activated), or activated by the Enter key.
        /// </summary>
        private void CurrentListView_ItemActivate(object sender, EventArgs e) => this.StartDOSBox(false);

        /// <summary> EventHandler for when a drag &amp; drop is initiated (drag). </summary>
        private void CurrentListView_ItemDrag(object sender, EventArgs e)
        {
            if (this.SelectedListView.FocusedItem != null)
            {
                this.SelectedListView.DoDragDrop(this.SelectedListView.FocusedItem.Text, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// EventHandler for when this.SelectedListView (the current tab's ListView) item selection changes.
        /// </summary>
        private void CurrentListView_ItemSelectionChanged(object sender, EventArgs e)
        {
            this.AdditionalCommandsLabel.Text = string.Empty;
            this.ExecutablePathLabel.Text = string.Empty;
            this.CMountLabel.Text = string.Empty;
            this.SetupPathLabel.Text = string.Empty;
            this.DMountLabel.Text = string.Empty;
            this.CustomConfigurationLabel.Text = string.Empty;
            this.QuitOnExitLabel.Text = string.Empty;
            this.FullscreenLabel.Text = string.Empty;
            this.NoConsoleLabel.Text = string.Empty;
            this.NotesLabel.Text = string.Empty;

            //several games can be selected at once, but it is only meant for drag&drop between categories
            //Besides, running more than one game (or DOSBox instance) at once can be CPU intensive...
            //if 1 game has been selected
            if (this.SelectedListView.SelectedItems.Count == 1)
            {
                this.deleteGameMenuItem.Enabled = true;
                this.DeleteSelectedGameToolStripMenuItem.Enabled = true;
                this.GameDeleteButton.Enabled = true;
                this.editGameMenuItem.Enabled = true;
                this.EditSelectedgameToolStripMenuItem.Enabled = true;
                this.GameEditButton.Enabled = true;
                this.MakeConfigButton.Enabled = true;
                this.MakeConfigurationFileToolStripMenuItem.Enabled = true;
                this.makeGameConfigurationMenuItem.Enabled = true;
                this.RunGameToolStripMenuItem.Enabled = true;
                this.runGameMenuItem.Enabled = true;
                this.RunGameButton.Enabled = true;
                this.OpenGameFolderButton.Enabled = true;
                this.openGameFolderMenuItem.Enabled = true;
                this.OpenGameFolderToolStripMenuItem.Enabled = true;
                Game selectedGame = this.GetSelectedGame();

                //if the selected game has a setup executable
                if (StringExt.IsNullOrWhiteSpace(selectedGame.SetupEXEPath) == false)
                {
                    this.RunGameSetupToolStripMenuItem.Enabled = true;
                    this.runGameSetupMenuItem.Enabled = true;
                    this.RunGameSetupButton.Enabled = true;
                    this.SetupPathLabel.Text = $"Setup: {selectedGame.SetupEXEPath}";
                }
                else
                {
                    this.RunGameSetupToolStripMenuItem.Enabled = false;
                    this.runGameSetupMenuItem.Enabled = false;
                    this.RunGameSetupButton.Enabled = false;
                    this.SetupPathLabel.Text = "Setup: none";
                }
                if (StringExt.IsNullOrWhiteSpace(selectedGame.DOSEXEPath) == false)
                {
                    this.ExecutablePathLabel.Text = $"Executable: {selectedGame.DOSEXEPath}";
                    this.gamePropertiesMenuItem.Enabled = true;
                }
                else
                {
                    this.ExecutablePathLabel.Text = "Executable: none";
                    this.gamePropertiesMenuItem.Enabled = false;
                }

                if (StringExt.IsNullOrWhiteSpace(selectedGame.Directory) == false)
                {
                    this.CMountLabel.Text = $"'C:' mount: {selectedGame.Directory}";
                }
                else
                {
                    this.CMountLabel.Text = "'C:' mount: none";
                }

                if (selectedGame.NoConfig == false)
                {
                    if (StringExt.IsNullOrWhiteSpace(selectedGame.DBConfPath) == false)
                    {
                        this.CustomConfigurationLabel.Text = $"Configuration: {selectedGame.DBConfPath}";
                        this.editGameConfigurationMenuItem.Enabled = true;
                        this.GameEditConfigurationButton.Enabled = true;
                        this.EditConfigToolStripMenuItem.Enabled = true;
                    }
                    else if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath) == false)
                    {
                        this.CustomConfigurationLabel.Text = "Configuration: default";
                        this.editGameConfigurationMenuItem.Enabled = false;
                        this.GameEditConfigurationButton.Enabled = false;
                        this.EditConfigToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        this.CustomConfigurationLabel.Text = "Configuration: none at all";
                        this.editGameConfigurationMenuItem.Enabled = false;
                        this.GameEditConfigurationButton.Enabled = false;
                        this.EditConfigToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    this.CustomConfigurationLabel.Text = "Configuration: none at all";
                    this.editGameConfigurationMenuItem.Enabled = false;
                    this.GameEditConfigurationButton.Enabled = false;
                    this.EditConfigToolStripMenuItem.Enabled = false;
                }
                if (StringExt.IsNullOrWhiteSpace(selectedGame.CDPath) == false)
                {
                    if (selectedGame.MountAsFloppy == false)
                    {
                        this.DMountLabel.Text = $"'D:' mount: {selectedGame.CDPath}";
                        if (selectedGame.UseIOCTL)
                        {
                            this.DMountLabel.Text += " (IOCTL in use)";
                        }
                    }
                    else
                    {
                        this.DMountLabel.Text = $"'A:' mount: {selectedGame.CDPath}";
                    }
                }
                else
                {
                    if (selectedGame.MountAsFloppy == false)
                    {
                        this.DMountLabel.Text = "'D:' mount: none";
                    }

                    if (selectedGame.MountAsFloppy)
                    {
                        this.DMountLabel.Text = "'A:' mount: none.";
                    }
                }
                if (selectedGame.NoConsole == true)
                {
                    this.NoConsoleLabel.Text = "No console: yes";
                }
                else
                {
                    this.NoConsoleLabel.Text = "No console: no";
                }

                if (selectedGame.InFullScreen == true)
                {
                    this.FullscreenLabel.Text = "Fullscreen: yes";
                }
                else
                {
                    this.FullscreenLabel.Text = "Fullscreen: no";
                }

                if (selectedGame.QuitOnExit == true)
                {
                    this.QuitOnExitLabel.Text = "Quit on exit: yes";
                }
                else
                {
                    this.QuitOnExitLabel.Text = "Quit on exit: no";
                }

                if (StringExt.IsNullOrWhiteSpace(selectedGame.AdditionalCommands) == false)
                {
                    this.AdditionalCommandsLabel.Text = $"Additional commands: {selectedGame.GetAdditionnalCommandsInASingleLine()}";
                }
                else
                {
                    this.AdditionalCommandsLabel.Text = "Additional commands: none";
                }

                if (StringExt.IsNullOrWhiteSpace(selectedGame.Directory) == false)
                {
                    this.NotesLabel.Text = $"Notes: {selectedGame.Notes}";
                }
                else
                {
                    this.CMountLabel.Text = "Notes: none";
                }
            }

            //if more than one game have been selected
            else if (this.SelectedListView.SelectedItems.Count > 1)
            {
                //make all the game buttons disabled (except the ones for deleting games)
                this.editGameMenuItem.Enabled = false;
                this.EditSelectedgameToolStripMenuItem.Enabled = false;
                this.GameEditButton.Enabled = false;
                this.RunGameToolStripMenuItem.Enabled = false;
                this.RunGameSetupButton.Enabled = false;
                this.RunGameSetupToolStripMenuItem.Enabled = false;
                this.runGameSetupMenuItem.Enabled = false;
                this.runGameMenuItem.Enabled = false;
                this.RunGameButton.Enabled = false;
                this.OpenGameFolderButton.Enabled = false;
                this.openGameFolderMenuItem.Enabled = false;
                this.OpenGameFolderToolStripMenuItem.Enabled = false;
                this.editGameConfigurationMenuItem.Enabled = false;
                this.GameEditConfigurationButton.Enabled = false;
                this.EditConfigToolStripMenuItem.Enabled = false;
                this.MakeConfigButton.Enabled = true;
                this.MakeConfigurationFileToolStripMenuItem.Enabled = true;
                this.makeGameConfigurationMenuItem.Enabled = true;
            }

            //if no game has been selected
            else if (this.SelectedListView.SelectedItems.Count == 0)
            {
                this.deleteGameMenuItem.Enabled = false;
                this.DeleteSelectedGameToolStripMenuItem.Enabled = false;
                this.GameDeleteButton.Enabled = false;
                this.editGameMenuItem.Enabled = false;
                this.EditSelectedgameToolStripMenuItem.Enabled = false;
                this.GameEditButton.Enabled = false;
                this.RunGameToolStripMenuItem.Enabled = false;
                this.RunGameSetupButton.Enabled = false;
                this.RunGameSetupToolStripMenuItem.Enabled = false;
                this.runGameSetupMenuItem.Enabled = false;
                this.runGameMenuItem.Enabled = false;
                this.RunGameButton.Enabled = false;
                this.OpenGameFolderButton.Enabled = false;
                this.openGameFolderMenuItem.Enabled = false;
                this.OpenGameFolderToolStripMenuItem.Enabled = false;
                this.editGameConfigurationMenuItem.Enabled = false;
                this.GameEditConfigurationButton.Enabled = false;
                this.EditConfigToolStripMenuItem.Enabled = false;
                this.MakeConfigButton.Enabled = false;
                this.MakeConfigurationFileToolStripMenuItem.Enabled = false;
                this.makeGameConfigurationMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// EventHandler for when a key is pressed while this.SelectedListView has focus.
        /// </summary>
        private void CurrentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }
            while (this.SelectedListView.SelectedItems.Count > 0)
            {
                var selectedItem = this.SelectedListView.SelectedItems[0];
                var correspondingGame = Program.UserDataAccessorInstance.GetGameWithSignature((string)selectedItem.Tag);
                if (MessageBox.Show(this, $"Do you really want to delete this game : {correspondingGame.Name} ?", this.GameDeleteButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (correspondingGame != null)
                    {
                        this.GetSelectedCategory().RemoveChild(correspondingGame);
                    }
                    this.SelectedListView.Items.Remove(selectedItem);
                }
                else
                {
                    break;
                }
            }
        }

        private void DetailsViewButton_Click(object sender, EventArgs e) => this.ChangeTabDisplayMode(View.Details);

        private void DetailsViewButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.DetailsViewButton.ToolTipText);

        private void DisplayHelpMessage(string toolTipText)
        {
            this.AdditionalCommandsLabel.Text = string.Empty;
            this.ExecutablePathLabel.Text = string.Empty;
            this.CMountLabel.Text = string.Empty;
            this.DMountLabel.Text = string.Empty;
            this.CustomConfigurationLabel.Text = string.Empty;
            this.QuitOnExitLabel.Text = string.Empty;
            this.FullscreenLabel.Text = string.Empty;
            this.NoConsoleLabel.Text = string.Empty;
            this.SetupPathLabel.Text = string.Empty;
            this.ExecutablePathLabel.Text = toolTipText;
            this.NotesLabel.Text = string.Empty;
        }

        /// <summary> Create the TabPages (categories) ListViews, and games inside the ListViews. </summary>
        private void DisplayUserData()
        {
            var userData = Program.UserDataAccessorInstance.GetUserData();
            this.TabControl.TabPages.Clear();
            if (StringExt.IsNullOrWhiteSpace(userData.DBDefaultConfFilePath) == false && StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath) == false)
            {
                this.EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                this.EditDefaultConfigurationButton.Enabled = true;
            }

            if (StringExt.IsNullOrWhiteSpace(userData.ConfigEditorPath))
            {
                this.RunConfigurationEditorButton.Enabled = false;
                this.RunConfigurationEditorToolStripMenuItem.Enabled = false;
            }

            if (StringExt.IsNullOrWhiteSpace(userData.DBPath) || File.Exists(userData.DBPath) == false)
            {
                this.RunDOSBoxToolStripMenuItem.Enabled = false;
                this.RunDOSBoxButton.Enabled = false;
            }

            //applying the Height and Width previously saved.
            if (userData.RememberWindowSize != false)
            {
                this.Width = userData.Width;
                this.Height = userData.Height;
                if (userData.Fullscreen == true)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            if (userData.RememberWindowPosition != false && Program.UserDataAccessorInstance.IsThisTheFirstRun() == false)
            {
                this.SetDesktopLocation(userData.X, userData.Y);
            }

            this.menuStrip.Visible = userData.MenuBarVisible;
            this.menuBarMenuItem.Checked = userData.MenuBarVisible;
            this.toolStrip.Visible = userData.ToolBarVisible;
            this.toolBarMenuItem.Checked = userData.ToolBarVisible;
            this.statusStrip.Visible = userData.StatusBarVisible;
            this.statusBarMenuItem.Checked = userData.StatusBarVisible;
            for (int i = 0; i < userData.ListChildren.Count; i++)
            {
                Category categoryToDisplay = (Category)userData.ListChildren[i];
                ListView tabltview = new CustomListView
                {
                    Sorting = SortOrder.Ascending,
                    TileSize = new Size(400, 45),
                    AllowDrop = true,
                    AutoArrange = true,
                    AllowColumnReorder = true,
                    LabelWrap = true
                };
                tabltview.ColumnClick += (o, e) => { tabltview.ListViewItemSorter = new CustomListViewItemSorter(e.Column); };
                tabltview.Columns.Add("NameColumn", "Name", categoryToDisplay.NameColumnWidth);
                tabltview.Columns.Add("ReleaseDateColumn", "Release Date", categoryToDisplay.ReleaseDateColumnWidth);
                tabltview.Columns.Add("ExecutableColumn", "Executable", categoryToDisplay.ExecutableColumnWidth);
                tabltview.Columns.Add("CMountColumn", "C: Mount", categoryToDisplay.CMountColumnWidth);
                tabltview.Columns.Add("SetupExecutableColumn", "Setup executable", categoryToDisplay.SetupExecutableColumnWidth);
                tabltview.Columns.Add("CustomConfigurationColumn", "Custom configuration", categoryToDisplay.CustomConfigurationColumnWidth);
                tabltview.Columns.Add("DMountColumn", "D: Mount", categoryToDisplay.DMountColumnWidth);
                tabltview.Columns.Add("MountingOptionsColumn", "Mounting options", categoryToDisplay.MountingOptionsColumnWidth);
                tabltview.Columns.Add("AdditionalCommandsColumn", "Additional commands", categoryToDisplay.AdditionnalCommandsColumnWidth);
                tabltview.Columns.Add("NoConsoleColumn", "No Console ?", categoryToDisplay.NoConsoleColumnWidth);
                tabltview.Columns.Add("FullscreenColumn", "Fullscreen ?", categoryToDisplay.FullscreenColumnWidth);
                tabltview.Columns.Add("QuitOnExitColumn", "Quit on exit ?", categoryToDisplay.QuitOnExitColumnWidth);
                tabltview.Columns.Add("NotesColumn", "Notes", categoryToDisplay.NotesColumnWidth);

                //for each game, create a ListViewItem instance.
                for (int i1 = 0; i1 < categoryToDisplay.ListChildren.Count; i1++)
                {
                    Game gameToDisplay = (Game)categoryToDisplay.ListChildren[i1];
                    ListViewItem gameforlt = new ListViewItem(gameToDisplay.Name)
                    {
                        Tag = gameToDisplay.Signature
                    };
                    if (this.gamesLargeImageList == null)
                    {
                        this.gamesLargeImageList = new ImageList();
                        this.gamesSmallImageList = new ImageList();
                        this.gamesMediumImageList = new ImageList();
                    }
                    tabltview.SmallImageList = this.gamesSmallImageList;
                    this.gamesSmallImageList.ImageSize = new Size(16, 16);
                    tabltview.LargeImageList = this.gamesLargeImageList;
                    this.gamesLargeImageList.ImageSize = new Size(userData.LargeViewModeSize, userData.LargeViewModeSize);
                    this.gamesMediumImageList.ImageSize = new Size(32, 32);
                    this.gamesLargeImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application.GetThumbnailImage(userData.LargeViewModeSize, userData.LargeViewModeSize, null, IntPtr.Zero));
                    this.gamesMediumImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application1.GetThumbnailImage(32, 32, null, IntPtr.Zero));
                    this.gamesSmallImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application1.GetThumbnailImage(16, 16, null, IntPtr.Zero));
                    if (StringExt.IsNullOrWhiteSpace(gameToDisplay.Icon) == false && File.Exists(gameToDisplay.Icon))
                    {
                        this.gamesLargeImageList.Images.Add(gameToDisplay.Signature, FileIconLoader.GetGameIconAsImage(gameToDisplay).GetThumbnailImage(userData.LargeViewModeSize, userData.LargeViewModeSize, null, IntPtr.Zero));
                        this.gamesMediumImageList.Images.Add(gameToDisplay.Signature, FileIconLoader.GetGameIconAsImage(gameToDisplay).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        this.gamesSmallImageList.Images.Add(gameToDisplay.Signature, FileIconLoader.GetGameIconAsImage(gameToDisplay).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        gameforlt.ImageKey = gameToDisplay.Signature;
                    }
                    else
                    {
                        gameforlt.ImageKey = "DefaultIcon";
                    }
                    ListViewItem.ListViewSubItem gameReleaseDateLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.ReleaseDate.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture)
                    };
                    gameforlt.SubItems.Add(gameReleaseDateLVSubItem);
                    ListViewItem.ListViewSubItem gameDOSEXEPathLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.DOSEXEPath
                    };
                    gameforlt.SubItems.Add(gameDOSEXEPathLVSubItem);
                    ListViewItem.ListViewSubItem gameCMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.Directory
                    };
                    gameforlt.SubItems.Add(gameCMountLVSubItem);
                    ListViewItem.ListViewSubItem gameSetupLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.SetupEXEPath
                    };
                    gameforlt.SubItems.Add(gameSetupLVSubItem);
                    ListViewItem.ListViewSubItem gameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Name = "GameCustomConfiguration"
                    };
                    gameCustomConfigurationLVSubItem.Text = gameToDisplay.GetCustomConfigDescription();

                    gameforlt.SubItems.Add(gameCustomConfigurationLVSubItem);
                    ListViewItem.ListViewSubItem gameDMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.CDPath
                    };
                    gameforlt.SubItems.Add(gameDMountLVSubItem);
                    ListViewItem.ListViewSubItem gameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                    gameMountingOptionsLVSubItem.Text = gameToDisplay.GetMountingOptionsDescription();
                    gameforlt.SubItems.Add(gameMountingOptionsLVSubItem);
                    ListViewItem.ListViewSubItem gameAdditionalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.GetAdditionnalCommandsInASingleLine()
                    };
                    gameforlt.SubItems.Add(gameAdditionalCommandsLVSubItem);
                    ListViewItem.ListViewSubItem gameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.NoConsole.ToString(CultureInfo.InvariantCulture)
                    };
                    gameforlt.SubItems.Add(gameNoConsoleLVSubItem);
                    ListViewItem.ListViewSubItem gameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.InFullScreen.ToString(CultureInfo.InvariantCulture)
                    };
                    gameforlt.SubItems.Add(gameFullscreenLVSubItem);
                    ListViewItem.ListViewSubItem gameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.QuitOnExit.ToString(CultureInfo.InvariantCulture)
                    };
                    gameforlt.SubItems.Add(gameQuitOnExitLVSubItem);

                    ListViewItem.ListViewSubItem gameNotesLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.Notes
                    };
                    gameforlt.SubItems.Add(gameNotesLVSubItem);

                    tabltview.Items.Add(gameforlt);
                }

                //the context menu of the ListView created earlier is the same for all of them.
                tabltview.ContextMenuStrip = this.currentListViewContextMenuStrip;

                //Name property used only inside the code. Never displayed.
                tabltview.Name = ListViewName;
                tabltview.Dock = DockStyle.Fill;
                if (userData.DefaultIconViewOverride == false)
                {
                    tabltview.View = (View)categoryToDisplay.ViewMode;
                }
                else
                {
                    tabltview.View = (View)userData.CategoriesDefaultViewMode;
                }

                if (tabltview.View == View.Tile)
                {
                    tabltview.LargeImageList = this.gamesMediumImageList;
                }

                if (tabltview.View == View.Details && tabltview.Columns.Count > 0)
                {
                    tabltview.Columns[0].Width = categoryToDisplay.NameColumnWidth;
                }

                tabltview.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.CurrentListView_ColumnWidthChanged);
                tabltview.ItemActivate += new EventHandler(this.CurrentListView_ItemActivate);
                tabltview.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.CurrentListView_ItemSelectionChanged);
                tabltview.KeyDown += new KeyEventHandler(this.CurrentListView_KeyDown);
                tabltview.Width = this.TabControl.Width;
                tabltview.Height = this.TabControl.Height;
                this.TabControl.TabPages.Add(categoryToDisplay.Title);
                this.TabControl.TabPages[this.TabControl.TabPages.Count - 1].Tag = categoryToDisplay.Signature;
                this.TabControl.DragOver += new DragEventHandler(this.TabControl_DragOver);
                this.TabControl.DragEnter += new DragEventHandler(this.TabControl_DragEnter);
                this.TabControl.DragDrop += new DragEventHandler(this.TabControl_DragDrop);
                this.TabControl.TabPages[this.TabControl.TabPages.Count - 1].Controls.Add(tabltview);
                var listView = (ListView)this.TabControl.TabPages[this.TabControl.TabPages.Count - 1].Controls[ListViewName];
                if (listView != null)
                {
                    listView.ItemDrag += new ItemDragEventHandler(this.CurrentListView_ItemDrag);
                    listView.DragOver += this.CurrentListView_DragOver;
                    listView.DragDrop += this.CurrentListView_DragDrop;
                    this.GameAddButton.Enabled = true;
                    this.NewGameToolStripMenuItem.Enabled = true;
                    this.SelectedListView.Sort();
                }
            }
        }

        private void EditDefaultConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath) == false && File.Exists(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath) && StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath) == false && Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath != "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found." && File.Exists(Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath))
            {
                ConfigEditorRunner.EditDefaultConfigFile(Program.UserDataAccessorInstance, Program.UserDataAccessorInstance.GetUserData());
            }
            else
            {
                MessageBox.Show("Default configuration or configuration editor missing. Please set them in the preferences.");
            }
        }

        private void EditDefaultConfigurationToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.EditDefaultConfigurationToolStripMenuItem.ToolTipText);

        private void EditToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.EditToolStripMenuItem.ToolTipText);

        private void FileToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.FileToolStripMenuItem.ToolTipText);

        /// <summary> EventHandler for when the user has clicked on the GameAddButton. </summary>
        private void GameAddButton_Click(object sender, EventArgs e)
        {
            var newGame = new Game();
            if (e is DragEventArgs dragArgs)
            {
                string[] files = (string[])dragArgs.Data.GetData(DataFormats.FileDrop);
                var firstFile = files.FirstOrDefault();
                if (StringExt.IsNullOrWhiteSpace(firstFile) == false)
                {
                    if (Path.GetExtension(firstFile).ToUpperInvariant() == ".LNK")
                    {
                        firstFile = NativeMethods.ResolveShortcut(firstFile);
                        newGame.Name = Path.GetDirectoryName(firstFile);
                        newGame.Directory = Path.GetDirectoryName(firstFile);
                    }
                    if (Path.GetExtension(firstFile).ToUpperInvariant() == ".PIF")
                    {
                        var pifFileParser = new PifFileParser(firstFile);
                        firstFile = pifFileParser.GetTargetFilePath();
                        newGame.Directory = pifFileParser.GetWorkingDirectory();
                        newGame.Name = pifFileParser.GetGameName();
                    }
                    if (Path.GetExtension(firstFile).ToUpperInvariant() == ".COM" || Path.GetExtension(firstFile).ToUpperInvariant() == ".EXE" || Path.GetExtension(firstFile).ToUpperInvariant() == ".BAT")
                    {
                        newGame.DOSEXEPath = firstFile;
                    }
                }
            }

            newGame.Signature = Program.UserDataAccessorInstance.GetAnUniqueSignature();

            using (var newGameForm = new GameForm(newGame, true))
            {
                if (newGameForm.ShowDialog(this) == DialogResult.OK)
                {
                    Category concernedCategory = this.GetSelectedCategory();
                    concernedCategory.AddChild(newGameForm.GameInstance);
                    this.RedrawAllUserData();
                }
            }
        }

        private void GameAddButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.GameAddButton.ToolTipText);

        /// <summary> EventHandler for when the delete button game is clicked. </summary>
        private void GameDeleteButton_Click(object sender, EventArgs e) => this.CurrentListView_KeyDown(sender, new KeyEventArgs(Keys.Delete));

        private void GameDeleteButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.GameDeleteButton.ToolTipText);

        /// <summary> Called when the user wants to edit an existing game. </summary>
        private void GameEditButton_Click(object sender, EventArgs e)
        {
            Game selectedGame = this.GetSelectedGame();
            using (var gameEditForm = new GameForm(selectedGame))
            {
                if (gameEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    this.RedrawAllUserData();
                }
            }
        }

        private void GameEditButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.GameEditButton.ToolTipText);

        private void GameEditConfigurationButton_Click(object sender, EventArgs e)
        {
            if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath) == false)
            {
                Game selectedGame = this.GetSelectedGame();
                Process.Start(Program.UserDataAccessorInstance.GetConfigEditorPath(), $"{selectedGame.DBConfPath} {Program.UserDataAccessorInstance.GetUserData().ConfigEditorAdditionalParameters}");
            }
        }

        private void GameEditConfigurationButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.GameEditConfigurationButton.ToolTipText);

        private Category GetSelectedCategory()
        {
            if (this.TabControl.SelectedTab == null)
            {
                return new Category();
            }
            var selectedCategory = Program.UserDataAccessorInstance.GetCategoryWithSignature((string)this.TabControl.SelectedTab.Tag);
            if (selectedCategory == null)
            {
                return new Category();
            }
            return selectedCategory;
        }

        private Category GetSelectedCategory(int hoveredTabIndex)
        {
            var selectedCategory = Program.UserDataAccessorInstance.GetCategoryWithSignature((string)this.TabControl.TabPages[hoveredTabIndex].Tag);
            if (selectedCategory == null)
            {
                return new Category();
            }
            return selectedCategory;
        }

        private Game GetSelectedGame()
        {
            if (this.SelectedListView.FocusedItem == null)
            {
                return new Game();
            }
            var selectedGame = Program.UserDataAccessorInstance.GetGameWithSignature((string)this.SelectedListView.FocusedItem.Tag);
            if (selectedGame == null)
            {
                return new Game();
            }
            return selectedGame;
        }

        private void HelpToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.HelpToolStripMenuItem.ToolTipText);

        private void ImportGamesAndCategoriesToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.ImportGamesAndCategoriesToolStripMenuItem.ToolTipText);

        private void ImportGamesAndCategoriesToolStripMenuItem_MouseLeave(object sender, EventArgs e) => this.CurrentListView_ItemSelectionChanged(sender, e);

        private void ImportGamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Multiselect = false, Title = "Importing an existing AmpShell.xml file...", CheckFileExists = true, DefaultExt = ".xml" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var worker = new BackgroundWorker();
                var needToRedrawTabs = false;
                worker.DoWork += (s, eventargs) =>
                {
                    try
                    {
                        needToRedrawTabs = Program.UserDataAccessorInstance.ImportGamesAndCategories(ofd.FileName);
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show(this, $"File not found: {ofd.FileName}");
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show(this, "This is the currently in-use file. Nothing to import.");
                    }
                };
                worker.RunWorkerCompleted += (sndr, eventarg) =>
                {
                    if (needToRedrawTabs)
                    {
                        this.RedrawAllUserData();
                        this.DisplayHelpMessage("Import successful.");
                    }
                };
                AwaitBackgroundWorker(worker);
            }
        }

        private void LargeIconViewButton_Click(object sender, EventArgs e) => this.ChangeTabDisplayMode(View.LargeIcon);

        private void LargeIconViewButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.LargeIconViewButton.ToolTipText);

        private void ListViewButton_Click(object sender, EventArgs e) => this.ChangeTabDisplayMode(View.List);

        private void ListViewButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.ListViewButton.ToolTipText);

        private void MakeConfigButton_Click(object sender, EventArgs e)
        {
            var selectedGame = this.GetSelectedGame();
            var configPath = Path.Combine(selectedGame.Directory, Path.GetFileName(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath));
            if ((!File.Exists(configPath)) ||
                (MessageBox.Show(this, $"{configPath} already exists, do you want to overwrite it ?", this.MakeConfigButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                File.Copy(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath, configPath, true);
                selectedGame.DBConfPath = configPath;
            }
        }

        private void MakeConfigurationFileToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.MakeConfigurationFileToolStripMenuItem.ToolTipText);

        private void MenuBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (this.menuStrip.Visible == true)
            {
                this.menuBarMenuItem.Checked = false;
                this.menuStrip.Visible = false;
            }
            else
            {
                this.menuBarMenuItem.Checked = true;
                this.menuStrip.Visible = true;
            }
            Program.UserDataAccessorInstance.UpdateIsMenuBarVisible(this.menuStrip.Visible);
        }

        private void OnDOSBoxExit(object sender, EventArgs e) => this.Invoke((MethodInvoker)(() => { this.WindowState = FormWindowState.Normal; }));

        private void OpenGameFolderButton_Click(object sender, EventArgs e) => this.GetSelectedGame().OpenGameFolder();

        private void OpenGameFolderButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.OpenGameFolderButton.ToolTipText);

        private void OpenGameFolderToolStripMenuItem_Click(object sender, EventArgs e) => this.GetSelectedGame().OpenGameFolder();

        private void OpenGameFolderToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.OpenGameFolderToolStripMenuItem.ToolTipText);

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var prefsForm = new PreferencesForm())
            {
                if (prefsForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (this.gamesLargeImageList != null)
                    {
                        this.gamesLargeImageList.ImageSize = new Size(Program.UserDataAccessorInstance.GetUserData().LargeViewModeSize, Program.UserDataAccessorInstance.GetUserData().LargeViewModeSize);
                    }
                    this.menuStrip.Visible = Program.UserDataAccessorInstance.GetUserData().MenuBarVisible;
                    this.menuBarMenuItem.Checked = Program.UserDataAccessorInstance.GetUserData().MenuBarVisible;
                    this.toolStrip.Visible = Program.UserDataAccessorInstance.GetUserData().ToolBarVisible;
                    this.toolBarMenuItem.Checked = Program.UserDataAccessorInstance.GetUserData().ToolBarVisible;
                    this.statusStrip.Visible = Program.UserDataAccessorInstance.GetUserData().StatusBarVisible;
                    this.statusBarMenuItem.Checked = Program.UserDataAccessorInstance.GetUserData().StatusBarVisible;
                    this.RedrawAllUserData();
                }
            }
            this.UpdateButtonsState();
        }

        private void PreferencesToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.PreferencesToolStripMenuItem.ToolTipText);

        private void QuitterToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.QuitterToolStripMenuItem.ToolTipText);

        /// <summary> EventHander for File -&gt; Quit. </summary>
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void RedrawAllUserData()
        {
            Game selectedGame = this.GetSelectedGame();
            Category selectedCategory = this.GetSelectedCategory();
            this.DisplayUserData();
            this.SelectCategory(selectedCategory.Signature);
            this.SelectedListView.FocusedItem = this.SelectedListView.Items.Cast<ListViewItem>().FirstOrDefault(x => (string)x.Tag == selectedGame.Signature);
            if (this.SelectedListView.FocusedItem != null)
            {
                this.SelectedListView.FocusedItem.Selected = true;
            }
        }

        /// <summary>
        /// We use a timer to let the drag &amp; drop finish first (or else it loops forever).
        /// </summary>
        private void RedrawWaitTimer_Tick(object sender, EventArgs e)
        {
            this.redrawWaitTimer.Enabled = false;
            if (this.redrawableTabs.Count == 0)
            {
                return;
            }

            //we need to redraw only when needed, as the drag&drop operation loops a few times otherwise
            if (this.redrawableTabs.Contains((TabPage)this.redrawWaitTimer.Tag))
            {
                this.redrawableTabs.Remove((TabPage)this.redrawWaitTimer.Tag);
            }
            else
            {
                return;
            }
            this.TabControl.AllowDrop = false;
            this.RedrawAllUserData();
            this.TabControl.AllowDrop = true;
        }

        private void RunConfigurationEditorButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigEditorRunner.CanRunConfigEditor(Program.UserDataAccessorInstance.GetUserData()))
                {
                    ConfigEditorRunner.RunConfigEditor(Program.UserDataAccessorInstance, Program.UserDataAccessorInstance.GetUserData());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The configuration editor cannot be run (was it deleted ?). Please set it in the preferences. Here is the error:{Environment.NewLine} {ex.GetBaseException().Message}", "Editor Launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunConfigurationEditorButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.RunConfigurationEditorButton.ToolTipText);

        /// <summary>
        /// EventHandler when the user clicks on Tools -&gt; Run DOSBox which runs DOSBox only with
        /// the default .conf (configuration) and .lng (language) files.
        /// </summary>
        private void RunDOSBox_Click(object sender, EventArgs e)
        {
            var dosboxProcess = GameProcessController.RunOnlyDOSBox(Program.UserDataAccessorInstance.GetUserData());

            if (dosboxProcess != null)
            {
                this.WindowState = FormWindowState.Minimized;
                dosboxProcess.Exited += this.OnDOSBoxExit;
            }
        }

        private void RunDOSBoxButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.RunDOSBoxButton.ToolTipText);

        private void RunGameButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.RunGameButton.ToolTipText);

        /// <summary> EventHandler for when the RunGameSetupButton is clicked. </summary>
        private void RunGameSetupButton_Click(object sender, EventArgs e) => this.StartDOSBox(true);

        private void RunGameSetupButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.RunGameSetupButton.ToolTipText);

        private void SelectCategory(string signature) => this.TabControl.SelectedTab = this.TabControl.TabPages.Cast<TabPage>().FirstOrDefault(x => (string)x.Tag == signature);

        private void SmallIconViewButton_Click(object sender, EventArgs e) => this.ChangeTabDisplayMode(View.SmallIcon);

        private void SmallIconViewButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.SmallIconViewButton.ToolTipText);

        private void StartDOSBox(bool runSetup)
        {
            var selectedGame = this.GetSelectedGame();
            if (selectedGame is null)
            {
                return;
            }
            try
            {
                Process dosboxProcess;
                if (runSetup)
                {
                    dosboxProcess = selectedGame.RunSetup(Program.UserDataAccessorInstance.GetUserData());
                }
                else
                {
                    dosboxProcess = selectedGame.Run(Program.UserDataAccessorInstance.GetUserData());
                }
                if (dosboxProcess != null)
                {
                    this.WindowState = FormWindowState.Minimized;
                    dosboxProcess.Exited += this.OnDOSBoxExit;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Game cannot be run. Was it deleted ? \r\n Missing file: {(ex is FileNotFoundException fn ? fn.FileName : "none")} \r\n Base message: {ex.GetBaseException().Message} \r\n Exception type: {ex.GetBaseException().GetType()}", "Game Launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StatusBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (this.statusStrip.Visible == true)
            {
                this.statusBarMenuItem.Checked = false;
                this.statusStrip.Visible = false;
            }
            else
            {
                this.statusBarMenuItem.Checked = true;
                this.statusStrip.Visible = true;
            }
            Program.UserDataAccessorInstance.UpdateStatusBarVisibility(this.statusStrip.Visible);
        }

        /// <summary> EventHandler for when a drop ends. </summary>
        private void TabControl_DragDrop(object sender, DragEventArgs e)
        {
            System.Collections.IList list = this.SelectedListView.SelectedItems;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem itemToMove = (ListViewItem)list[i];
                this.SelectedListView.Items.Remove(itemToMove);
                var droppedGame = Program.UserDataAccessorInstance.GetUserData().ListChildren.Cast<Category>().Select(x => x.ListChildren.Cast<Game>()).SelectMany(x => x).FirstOrDefault(x => x.Signature == (string)itemToMove.Tag);
                this.GetSelectedCategory().RemoveChild(droppedGame);
                this.TabControl.SelectTab(this.hoveredTabIndex);
                this.SelectedListView.Items.Add((ListViewItem)itemToMove.Clone());
                this.GetSelectedCategory(this.hoveredTabIndex).AddChild(droppedGame);
            }

            //Avoid yet again a nasty UI bug where the very first item in a TabPage has no icon.
            if (this.SelectedListView.Items.Count == 1)
            {
                this.redrawWaitTimer.Interval = 1;
                this.redrawWaitTimer.Tick += this.RedrawWaitTimer_Tick;
                this.redrawWaitTimer.Enabled = true;
                this.redrawWaitTimer.Tag = this.TabControl.TabPages[this.hoveredTabIndex];
            }
        }

        /// <summary> EventHandler for when a drop begins. </summary>
        private void TabControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.UnicodeText))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary> EventHandler for when items are dragged over a Tab. </summary>
        private void TabControl_DragOver(object sender, DragEventArgs e)
        {
            Point pos = this.TabControl.PointToClient(MousePosition);
            for (int ix = 0; ix < this.TabControl.TabCount; ix++)
            {
                if (this.TabControl.GetTabRect(ix).Contains(pos))
                {
                    this.hoveredTabIndex = ix;
                    break;
                }
            }
        }

        private void TilesViewButton_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.TilesViewButton.ToolTipText);

        private void TileViewButton_Click(object sender, EventArgs e) => this.ChangeTabDisplayMode(View.Tile);

        private void ToolBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (this.toolStrip.Visible == true)
            {
                this.toolBarMenuItem.Checked = false;
                this.toolStrip.Visible = false;
            }
            else
            {
                this.toolBarMenuItem.Checked = true;
                this.toolStrip.Visible = true;
            }
            Program.UserDataAccessorInstance.UpdateToolBarVisibility(this.toolStrip.Visible);
        }

        private void ToolsToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.ToolsToolStripMenuItem.ToolTipText);

        private void UpdateButtonsState()
        {
            this.LargeIconViewButton.Enabled = false;
            this.LargeIconToolStripMenuItem.Enabled = false;
            this.SmallIconToolStripMenuItem.Enabled = false;
            this.SmallIconViewButton.Enabled = false;
            this.TilesViewButton.Enabled = false;
            this.TileToolStripMenuItem.Enabled = false;
            this.DetailsViewButton.Enabled = false;
            this.DetailsToolStripMenuItem.Enabled = false;
            this.ListViewButton.Enabled = false;
            this.ListToolStripMenuItem.Enabled = false;
            this.NewGameToolStripMenuItem.Enabled = false;
            this.addGameMenuItem.Enabled = false;
            this.GameAddButton.Enabled = false;
            this.RunGameButton.Enabled = false;
            this.OpenGameFolderButton.Enabled = false;
            this.openGameFolderMenuItem.Enabled = false;
            this.OpenGameFolderToolStripMenuItem.Enabled = false;
            this.runGameMenuItem.Enabled = false;
            this.RunGameToolStripMenuItem.Enabled = false;
            this.GameEditButton.Enabled = false;
            this.EditSelectedgameToolStripMenuItem.Enabled = false;
            this.editGameMenuItem.Enabled = false;
            this.RunGameSetupButton.Enabled = false;
            this.RunGameSetupToolStripMenuItem.Enabled = false;
            this.runGameSetupMenuItem.Enabled = false;
            this.CategoryEditButton.Enabled = false;
            this.EditSelectedcategoryToolStripMenuItem.Enabled = false;
            this.editCategoryMenuMenuItem.Enabled = false;
            this.CategoryDeleteButton.Enabled = false;
            this.DeleteSelectedCategoryToolStripMenuItem.Enabled = false;
            this.deleteCategoryMenuMenuItem.Enabled = false;
            this.editGameConfigurationMenuItem.Enabled = false;
            this.GameEditConfigurationButton.Enabled = false;
            this.EditConfigToolStripMenuItem.Enabled = false;
            this.RunConfigurationEditorButton.Enabled = false;
            this.RunConfigurationEditorToolStripMenuItem.Enabled = false;
            this.RunDOSBoxButton.Enabled = false;
            this.RunDOSBoxToolStripMenuItem.Enabled = false;
            this.EditDefaultConfigurationToolStripMenuItem.Enabled = false;
            this.EditDefaultConfigurationButton.Enabled = false;
            this.makeGameConfigurationMenuItem.Enabled = false;
            this.MakeConfigButton.Enabled = false;
            this.MakeConfigurationFileToolStripMenuItem.Enabled = false;
            if (this.TabControl.HasChildren != false)
            {
                this.LargeIconViewButton.Enabled = true;
                this.LargeIconToolStripMenuItem.Enabled = true;
                this.SmallIconToolStripMenuItem.Enabled = true;
                this.SmallIconViewButton.Enabled = true;
                this.TilesViewButton.Enabled = true;
                this.TileToolStripMenuItem.Enabled = true;
                this.DetailsViewButton.Enabled = true;
                this.DetailsToolStripMenuItem.Enabled = true;
                this.ListViewButton.Enabled = true;
                this.ListToolStripMenuItem.Enabled = true;
                this.NewGameToolStripMenuItem.Enabled = true;
                this.addGameMenuItem.Enabled = true;
                this.GameAddButton.Enabled = true;
                this.OpenGameFolderButton.Enabled = true;
                this.openGameFolderMenuItem.Enabled = true;
                this.OpenGameFolderToolStripMenuItem.Enabled = true;
                if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().DBPath) == false && this.GetSelectedGame().IsDOSBoxUsed(Program.UserDataAccessorInstance.GetUserData()))
                {
                    this.RunGameButton.Enabled = true;
                    this.runGameMenuItem.Enabled = true;
                    this.RunGameToolStripMenuItem.Enabled = true;
                    this.RunGameSetupButton.Enabled = true;
                    this.RunGameSetupToolStripMenuItem.Enabled = true;
                    this.runGameSetupMenuItem.Enabled = true;
                    this.RunDOSBoxButton.Enabled = true;
                    this.RunDOSBoxToolStripMenuItem.Enabled = true;
                }
                else if (this.GetSelectedGame().IsDOSBoxUsed(Program.UserDataAccessorInstance.GetUserData()) == false)
                {
                    this.RunGameButton.Enabled = true;
                    this.runGameMenuItem.Enabled = true;
                    this.RunGameToolStripMenuItem.Enabled = true;
                    this.RunGameSetupButton.Enabled = true;
                    this.RunGameSetupToolStripMenuItem.Enabled = true;
                    this.runGameSetupMenuItem.Enabled = true;
                }
                this.CategoryEditButton.Enabled = true;
                this.editCategoryMenuMenuItem.Enabled = true;
                this.EditSelectedcategoryToolStripMenuItem.Enabled = true;
                this.CategoryDeleteButton.Enabled = true;
                this.DeleteSelectedCategoryToolStripMenuItem.Enabled = true;
                this.deleteCategoryMenuMenuItem.Enabled = true;
                this.GameEditButton.Enabled = true;
                if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().ConfigEditorPath) == false)
                {
                    this.RunConfigurationEditorButton.Enabled = true;
                    this.RunConfigurationEditorToolStripMenuItem.Enabled = true;
                }
                if (StringExt.IsNullOrWhiteSpace(Program.UserDataAccessorInstance.GetUserData().DBDefaultConfFilePath) == false && this.GetSelectedGame().IsDOSBoxUsed(Program.UserDataAccessorInstance.GetUserData()))
                {
                    this.EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                    this.EditDefaultConfigurationButton.Enabled = true;
                    this.makeGameConfigurationMenuItem.Enabled = true;
                    this.MakeConfigButton.Enabled = true;
                    this.MakeConfigurationFileToolStripMenuItem.Enabled = true;
                }
                this.CurrentListView_ItemSelectionChanged(this, EventArgs.Empty);
            }
        }

        private void ViewGameProperties_Click(object sender, EventArgs e)
        {
            var game = this.GetSelectedGame();
            if (StringExt.IsNullOrWhiteSpace(game.DOSEXEPath) || File.Exists(game.DOSEXEPath) == false)
            {
                return;
            }
            try
            {
                NativeMethods.ShowFileProperties(game.DOSEXEPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().StackTrace, ex.GetBaseException().Message);
            }
        }

        private void ViewToolStripMenuItem_MouseEnter(object sender, EventArgs e) => this.DisplayHelpMessage(this.ViewToolStripMenuItem.ToolTipText);
    }
}