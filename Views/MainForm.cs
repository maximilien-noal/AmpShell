/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.DAL;
using AmpShell.DOSBox;
using AmpShell.Model;
using AmpShell.Views.UserControls;
using AmpShell.WinShell;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AmpShell.Views
{
    public partial class MainForm : Form
    {
        private const string _listViewName = "GamesListView";
        private bool _ampShellShown;
        private int _hoveredTabIndex;
        private readonly Timer _redrawWaitTimer = new Timer();
        private List<TabPage> _redrawableTabs = new List<TabPage>();

        private readonly ImageList _gamesLargeImageList = new ImageList();
        private readonly ImageList _gamesSmallImageList = new ImageList();
        private readonly ImageList _gamesMediumImageList = new ImageList();

        /// <summary>
        /// Context Menu for the ListView
        /// </summary>
        private readonly ContextMenuStrip _currentListViewContextMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// Context Menu for the TabPages
        /// </summary>
        private readonly ContextMenuStrip _tabContextMenu = new ContextMenuStrip();

        private readonly ToolStripMenuItem _addCategoryMenuMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _deleteCategoryMenuMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _editCategoryMenuMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _addCategoryMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _deleteCategoryMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _editCategoryMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _addGameMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _deleteGameMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _editGameMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _editGameConfigurationMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _makeGameConfigurationMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _runGameMenuItem = new ToolStripMenuItem();
        private readonly ToolStripMenuItem _runGameSetupMenuItem = new ToolStripMenuItem();
        private readonly ContextMenuStrip _windowContextMenu = new ContextMenuStrip();
        private readonly ToolStripMenuItem _menuBarMenuItem = new ToolStripMenuItem("Menu bar");
        private readonly ToolStripMenuItem _toolBarMenuItem = new ToolStripMenuItem("Tool bar");
        private readonly ToolStripMenuItem _statusBarMenuItem = new ToolStripMenuItem("Details bar");

        public MainForm()
        {
            InitializeComponent();
            _menuBarMenuItem.Click += new EventHandler(MenuBar_ContextMenu_Click);
            _toolBarMenuItem.Click += new EventHandler(ToolBar_ContextMenu_Click);
            _statusBarMenuItem.Click += new EventHandler(StatusBar_ContextMenu_Click);
            _windowContextMenu.Items.Add(_menuBarMenuItem);
            _windowContextMenu.Items.Add(_toolBarMenuItem);
            _windowContextMenu.Items.Add(_statusBarMenuItem);
            ContextMenuStrip = _windowContextMenu;
            TabControl.AllowDrop = true;
            //adding text, images, and EventHandlers to the context pop-up menu
            _addGameMenuItem.Image = GameAddButton.Image;
            _addGameMenuItem.Text = GameAddButton.Text;
            _addGameMenuItem.Click += new EventHandler(GameAddButton_Click);
            _addGameMenuItem.MouseEnter += new EventHandler(GameAddButton_MouseEnter);
            _addGameMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _currentListViewContextMenuStrip.Items.Add(_addGameMenuItem);
            _runGameMenuItem.Image = RunGameButton.Image;
            _runGameMenuItem.Text = RunGameButton.Text;
            _runGameMenuItem.Click += new EventHandler(CurrentListView_ItemActivate);
            _runGameMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _runGameMenuItem.MouseEnter += new EventHandler(RunGameButton_MouseEnter);
            //Only Enabled when a game is selected
            _runGameMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_runGameMenuItem);
            _runGameSetupMenuItem.Image = RunGameSetupButton.Image;
            _runGameSetupMenuItem.Text = RunGameSetupButton.Text;
            _runGameSetupMenuItem.Click += new EventHandler(RunGameSetupButton_Click);
            _runGameSetupMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _runGameSetupMenuItem.MouseEnter += new EventHandler(RunGameSetupButton_MouseEnter);
            //Only Enabled when a game is selected
            _runGameSetupMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_runGameSetupMenuItem);
            _deleteGameMenuItem.Image = GameDeleteButton.Image;
            _deleteGameMenuItem.Text = GameDeleteButton.Text;
            _deleteGameMenuItem.Click += new EventHandler(GameDeleteButton_Click);
            _deleteGameMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _deleteGameMenuItem.MouseEnter += new EventHandler(GameDeleteButton_MouseEnter);
            //Only Enabled when a game is selected
            _deleteGameMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_deleteGameMenuItem);
            _editGameMenuItem.Image = GameEditButton.Image;
            _editGameMenuItem.Text = GameEditButton.Text;
            _editGameMenuItem.Click += new EventHandler(GameEditButton_Click);
            _editGameMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _editGameMenuItem.MouseEnter += new EventHandler(GameEditButton_MouseEnter);
            //Only Enabled when a game is selected
            _editGameMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_editGameMenuItem);
            _editGameConfigurationMenuItem.Image = GameEditConfigurationButton.Image;
            _editGameConfigurationMenuItem.Text = GameEditConfigurationButton.Text;
            _editGameConfigurationMenuItem.Click += new EventHandler(GameEditConfigurationButton_Click);
            _editGameConfigurationMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _editGameConfigurationMenuItem.MouseEnter += new EventHandler(GameEditConfigurationButton_MouseEnter);
            //Only Enabled when a game is selected
            _editGameConfigurationMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_editGameConfigurationMenuItem);
            _makeGameConfigurationMenuItem.Image = MakeConfigButton.Image;
            _makeGameConfigurationMenuItem.Text = MakeConfigButton.Text;
            _makeGameConfigurationMenuItem.Click += new EventHandler(MakeConfigButton_Click);
            _makeGameConfigurationMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _makeGameConfigurationMenuItem.MouseEnter += new EventHandler(MakeConfigurationFileToolStripMenuItem_MouseEnter);
            //Only Enabled when a game is selected
            _makeGameConfigurationMenuItem.Enabled = false;
            _currentListViewContextMenuStrip.Items.Add(_makeGameConfigurationMenuItem);
            ToolStripSeparator ltview_ContextMenuStripSeparator = new ToolStripSeparator();
            _currentListViewContextMenuStrip.Items.Add(ltview_ContextMenuStripSeparator);
            //The Categories are the tabs inside the TabControl. Each tab has only one ListView.
            _addCategoryMenuMenuItem.Image = CategoryAddButton.Image;
            _addCategoryMenuMenuItem.Text = CategoryAddButton.Text;
            _addCategoryMenuMenuItem.Click += new EventHandler(CategoryAddButton_Click);
            _addCategoryMenuMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _addCategoryMenuMenuItem.MouseEnter += new EventHandler(CategoryAddButton_MouseEnter);
            _addCategoryMenuItem.Image = CategoryAddButton.Image;
            _addCategoryMenuItem.Text = CategoryAddButton.Text;
            _addCategoryMenuItem.Click += new EventHandler(CategoryAddButton_Click);
            _addCategoryMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _addCategoryMenuItem.MouseEnter += new EventHandler(CategoryAddButton_MouseEnter);
            _currentListViewContextMenuStrip.Items.Add(_addCategoryMenuMenuItem);
            _tabContextMenu.Items.Add(_addCategoryMenuItem);
            _editCategoryMenuMenuItem.Image = CategoryEditButton.Image;
            _editCategoryMenuMenuItem.Text = CategoryEditButton.Text;
            _editCategoryMenuMenuItem.Click += new EventHandler(CategoryEditButton_Click);
            _editCategoryMenuMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _editCategoryMenuMenuItem.MouseEnter += new EventHandler(CategoryEditButton_MouseEnter);
            _editCategoryMenuItem.Image = CategoryEditButton.Image;
            _editCategoryMenuItem.Text = CategoryEditButton.Text;
            _editCategoryMenuItem.Click += new EventHandler(CategoryEditButton_Click);
            _editCategoryMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _editCategoryMenuItem.MouseEnter += new EventHandler(CategoryEditButton_MouseEnter);
            _currentListViewContextMenuStrip.Items.Add(_editCategoryMenuMenuItem);
            _tabContextMenu.Items.Add(_editCategoryMenuItem);
            _deleteCategoryMenuMenuItem.Image = CategoryDeleteButton.Image;
            _deleteCategoryMenuMenuItem.Text = CategoryDeleteButton.Text;
            _deleteCategoryMenuMenuItem.Click += new EventHandler(CategoryDeleteButton_Click);
            _deleteCategoryMenuMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _deleteCategoryMenuMenuItem.MouseEnter += new EventHandler(CategoryDeleteButton_MouseEnter);
            _deleteCategoryMenuItem.Image = CategoryDeleteButton.Image;
            _deleteCategoryMenuItem.Text = CategoryDeleteButton.Text;
            _deleteCategoryMenuItem.Click += new EventHandler(CategoryDeleteButton_Click);
            _deleteCategoryMenuItem.MouseLeave += new EventHandler(CurrentListView_ItemSelectionChanged);
            _deleteCategoryMenuItem.MouseEnter += new EventHandler(CategoryDeleteButton_MouseEnter);
            _currentListViewContextMenuStrip.Items.Add(_deleteCategoryMenuMenuItem);
            _tabContextMenu.Items.Add(_deleteCategoryMenuItem);
            TabControl.ContextMenuStrip = _tabContextMenu;
            SelectedListView.ColumnWidthChanged += new ColumnWidthChangedEventHandler(CurrentListView_ColumnWidthChanged);
        }

        /// <summary>
        /// ListView instance used mainly to retrieve the current ListView (in tabcontrol.SelectedTab["GamesListView"])
        /// </summary>
        private ListView SelectedListView
        {
            get
            {
                if (TabControl.HasChildren == false)
                {
                    return new ListView();
                }
                return (ListView)TabControl.SelectedTab.Controls[_listViewName];
            }
        }

        private void AmpShell_Load(object sender, EventArgs e)
        {
            UserDataAccessor.LoadUserSettings();
            DOSBoxController.AskForDOSBox();
            DisplayUserData();
            _redrawableTabs = TabControl.TabPages.Cast<TabPage>().Where(x => ((ListView)x.Controls[_listViewName]).Items.Count == 0).ToList();
        }

        /// <summary>
        /// Create the TabPages (categories) ListViews, and games inside the ListViews
        /// </summary>
        /// <param name="noIcons">whether we display icons for games or not at all</param>
        private void DisplayUserData()
        {
            var userData = UserDataAccessor.UserData;
            TabControl.TabPages.Clear();
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false && string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
            {
                EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                EditDefaultConfigurationButton.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath))
            {
                RunConfigurationEditorButton.Enabled = false;
                RunConfigurationEditorToolStripMenuItem.Enabled = false;
            }

            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) || File.Exists(UserDataAccessor.UserData.DBPath) == false)
            {
                RunDOSBoxToolStripMenuItem.Enabled = false;
                RunDOSBoxButton.Enabled = false;
            }

            //applying the Height and Width previously saved.
            if (userData.RememberWindowSize != false)
            {
                Width = userData.Width;
                Height = userData.Height;
                if (userData.Fullscreen == true)
                {
                    WindowState = FormWindowState.Maximized;
                }
            }
            if (userData.RememberWindowPosition != false)
            {
                SetDesktopLocation(userData.X, userData.Y);
            }

            menuStrip.Visible = userData.MenuBarVisible;
            _menuBarMenuItem.Checked = userData.MenuBarVisible;
            toolStrip.Visible = userData.ToolBarVisible;
            _toolBarMenuItem.Checked = userData.ToolBarVisible;
            statusStrip.Visible = userData.StatusBarVisible;
            _statusBarMenuItem.Checked = userData.StatusBarVisible;
            foreach (Category categoryToDisplay in userData.ListChildren)
            {
                ListView tabltview = new CustomListView
                {
                    Sorting = SortOrder.Ascending,
                    TileSize = new Size(400, 45),
                    AllowDrop = true,
                    AutoArrange = true,
                    AllowColumnReorder = true,
                    LabelWrap = true
                };
                tabltview.Columns.Add("NameColumn", "Name", categoryToDisplay.NameColumnWidth);
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
                //for each game, create a ListViewItem instance.
                foreach (Game gameToDisplay in categoryToDisplay.ListChildren)
                {
                    ListViewItem gameforlt = new ListViewItem(gameToDisplay.Name)
                    {
                        Tag = gameToDisplay.Signature
                    };
                    tabltview.SmallImageList = _gamesSmallImageList;
                    _gamesSmallImageList.ImageSize = new Size(16, 16);
                    tabltview.LargeImageList = _gamesLargeImageList;
                    _gamesLargeImageList.ImageSize = new Size(userData.LargeViewModeSize, userData.LargeViewModeSize);
                    _gamesMediumImageList.ImageSize = new Size(32, 32);
                    _gamesLargeImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application.GetThumbnailImage(userData.LargeViewModeSize, userData.LargeViewModeSize, null, IntPtr.Zero));
                    _gamesMediumImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application1.GetThumbnailImage(32, 32, null, IntPtr.Zero));
                    _gamesSmallImageList.Images.Add("DefaultIcon", Properties.Resources.Generic_Application1.GetThumbnailImage(16, 16, null, IntPtr.Zero));
                    if (string.IsNullOrWhiteSpace(gameToDisplay.Icon) == false && File.Exists(gameToDisplay.Icon))
                    {
                        _gamesLargeImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(userData.LargeViewModeSize, userData.LargeViewModeSize, null, IntPtr.Zero));
                        _gamesMediumImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        _gamesSmallImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        gameforlt.ImageKey = gameToDisplay.Signature;
                    }
                    else
                    {
                        gameforlt.ImageKey = "DefaultIcon";
                    }
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
                    if (gameToDisplay.NoConfig == true)
                    {
                        gameCustomConfigurationLVSubItem.Text = "None at all";
                    }
                    else
                    {
                        gameCustomConfigurationLVSubItem.Text = gameToDisplay.DBConfPath;
                    }

                    gameforlt.SubItems.Add(gameCustomConfigurationLVSubItem);
                    ListViewItem.ListViewSubItem gameDMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.CDPath
                    };
                    gameforlt.SubItems.Add(gameDMountLVSubItem);
                    ListViewItem.ListViewSubItem gameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                    if (gameToDisplay.UseIOCTL == true)
                    {
                        gameMountingOptionsLVSubItem.Text = "Use IOCTL";
                    }
                    else if (gameToDisplay.MountAsFloppy == true)
                    {
                        gameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                    }
                    else
                    {
                        gameMountingOptionsLVSubItem.Text = "None";
                    }

                    gameforlt.SubItems.Add(gameMountingOptionsLVSubItem);
                    ListViewItem.ListViewSubItem gameAdditionalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.AdditionalCommands
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
                    tabltview.Items.Add(gameforlt);
                }
                //the context menu of the ListView created earlier is the same for all of them.
                tabltview.ContextMenuStrip = _currentListViewContextMenuStrip;
                //Name property used only inside the code. Never displayed.
                tabltview.Name = _listViewName;
                tabltview.Dock = DockStyle.Fill;
                if (userData.DefaultIconViewOverride == false)
                {
                    tabltview.View = categoryToDisplay.ViewMode;
                }
                else
                {
                    tabltview.View = userData.CategoriesDefaultViewMode;
                }

                if (tabltview.View == View.Tile)
                {
                    tabltview.LargeImageList = _gamesMediumImageList;
                }

                if (tabltview.View == View.Details && tabltview.Columns.Count > 0)
                {
                    tabltview.Columns[0].Width = categoryToDisplay.NameColumnWidth;
                }

                tabltview.ColumnWidthChanged += new ColumnWidthChangedEventHandler(CurrentListView_ColumnWidthChanged);
                tabltview.ItemActivate += new EventHandler(CurrentListView_ItemActivate);
                tabltview.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(CurrentListView_ItemSelectionChanged);
                tabltview.KeyDown += new KeyEventHandler(CurrentListView_KeyDown);
                tabltview.Width = TabControl.Width;
                tabltview.Height = TabControl.Height;
                TabControl.TabPages.Add(categoryToDisplay.Title);
                TabControl.TabPages[TabControl.TabPages.Count - 1].Tag = categoryToDisplay.Signature;
                TabControl.DragOver += new DragEventHandler(TabControl_DragOver);
                TabControl.DragEnter += new DragEventHandler(TabControl_DragEnter);
                TabControl.DragDrop += new DragEventHandler(TabControl_DragDrop);
                TabControl.TabPages[TabControl.TabPages.Count - 1].Controls.Add(tabltview);
                var listView = (ListView)TabControl.TabPages[TabControl.TabPages.Count - 1].Controls[_listViewName];
                if (listView != null)
                {
                    listView.ItemDrag += new ItemDragEventHandler(CurrentListView_ItemDrag);
                    listView.DragOver += CurrentListView_DragOver;
                    listView.DragDrop += CurrentListView_DragDrop;
                    GameAddButton.Enabled = true;
                    NewGameToolStripMenuItem.Enabled = true;
                    SelectedListView.Sort();
                }
            }
        }

        private void CurrentListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                GameAddButton_Click(this, e);
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
        /// EventHandler for when a drag&drop is initiated (drag)
        /// </summary>
        private void CurrentListView_ItemDrag(object sender, EventArgs e)
        {
            if (SelectedListView.FocusedItem != null)
            {
                SelectedListView.DoDragDrop(SelectedListView.FocusedItem.Text, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// EventHandler for when items are dragged over a Tab
        /// </summary>
        private void TabControl_DragOver(object sender, DragEventArgs e)
        {
            Point pos = TabControl.PointToClient(MousePosition);
            for (int ix = 0; ix < TabControl.TabCount; ix++)
            {
                if (TabControl.GetTabRect(ix).Contains(pos))
                {
                    _hoveredTabIndex = ix;
                    break;
                }
            }
        }

        /// <summary>
        /// EventHandler for when a drop begins
        /// </summary>
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

        /// <summary>
        /// EventHandler for when a drop ends
        /// </summary>
        private void TabControl_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem itemToMove in SelectedListView.SelectedItems)
            {
                SelectedListView.Items.Remove(itemToMove);
                var droppedGame = UserDataAccessor.UserData.ListChildren.Cast<Category>().Select(x => x.ListChildren.Cast<Game>()).SelectMany(x => x).FirstOrDefault(x => x.Signature == (string)itemToMove.Tag);
                GetSelectedCategory()?.RemoveChild(droppedGame);
                TabControl.SelectTab(_hoveredTabIndex);
                SelectedListView.Items.Add((ListViewItem)itemToMove.Clone());
                GetSelectedCategory(_hoveredTabIndex).AddChild(droppedGame);
            }
            //Avoid yet again a nasty UI bug where the very first item in a TabPage has no icon.
            if (SelectedListView.Items.Count == 1)
            {
                _redrawWaitTimer.Interval = 1;
                _redrawWaitTimer.Tick += RedrawWaitTimer_Tick;
                _redrawWaitTimer.Enabled = true;
                _redrawWaitTimer.Tag = TabControl.TabPages[_hoveredTabIndex];
            }
        }

        /// <summary>
        /// We use a timer to let the drag&drop finish first (or else it loops forever)
        /// </summary>
        private void RedrawWaitTimer_Tick(object sender, EventArgs e)
        {
            _redrawWaitTimer.Enabled = false;
            if (_redrawableTabs.Count == 0)
            {
                return;
            }
            //we need to redraw only when needed, as the drag&drop operation loops a few times otherwise
            if (_redrawableTabs.Contains((TabPage)_redrawWaitTimer.Tag))
            {
                _redrawableTabs.Remove((TabPage)_redrawWaitTimer.Tag);
            }
            else
            {
                return;
            }
            TabControl.AllowDrop = false;
            RedrawAllUserData();
            TabControl.AllowDrop = true;
        }

        private Game GetSelectedGame()
        {
            if (SelectedListView.FocusedItem == null)
            {
                return null;
            }
            return UserDataAccessor.GetGameWithSignature((string)SelectedListView.FocusedItem.Tag);
        }

        private Category GetSelectedCategory()
        {
            if (TabControl.SelectedTab == null)
            {
                return null;
            }
            return UserDataAccessor.GetCategoryWithSignature((string)TabControl.SelectedTab.Tag);
        }

        private Category GetSelectedCategory(int hoveredTabIndex)
        {
            return UserDataAccessor.GetCategoryWithSignature((string)TabControl.TabPages[hoveredTabIndex].Tag);
        }

        /// <summary>
        /// Called when the user wants to edit an existing game
        /// </summary>
        private void GameEditButton_Click(object sender, EventArgs e)
        {
            Game selectedGame = GetSelectedGame();
            using (var gameEditForm = new GameForm(selectedGame))
            {
                if (gameEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    RedrawAllUserData();
                }
            }
        }

        private void RedrawAllUserData()
        {
            Game selectedGame = GetSelectedGame();
            Category selectedCategory = GetSelectedCategory();
            DisplayUserData();
            if (selectedCategory != null)
            {
                SelectCategory(selectedCategory.Signature);
            }
            if (selectedGame != null)
            {
                SelectedListView.FocusedItem = SelectedListView.Items.Cast<ListViewItem>().FirstOrDefault(x => (string)x.Tag == selectedGame.Signature);
                SelectedListView.FocusedItem.Selected = true;
            }
        }

        /// <summary>
        /// EventHandler for when SelectedListView (the current tab's ListView) item selection changes
        /// </summary>
        private void CurrentListView_ItemSelectionChanged(object sender, EventArgs e)
        {
            AdditionalCommandsLabel.Text = string.Empty;
            ExecutablePathLabel.Text = string.Empty;
            CMountLabel.Text = string.Empty;
            SetupPathLabel.Text = string.Empty;
            DMountLabel.Text = string.Empty;
            CustomConfigurationLabel.Text = string.Empty;
            QuitOnExitLabel.Text = string.Empty;
            FullscreenLabel.Text = string.Empty;
            NoConsoleLabel.Text = string.Empty;
            //several games can be selected at once, but it is only meant for drag&drop between categories
            //Besides, running more than one game (one DOSBox instance) at once can be CPU intensive...
            //if 1 game has been selected
            if (SelectedListView.SelectedItems.Count == 1)
            {
                _deleteGameMenuItem.Enabled = true;
                DeleteSelectedGameToolStripMenuItem.Enabled = true;
                GameDeleteButton.Enabled = true;
                _editGameMenuItem.Enabled = true;
                EditSelectedgameToolStripMenuItem.Enabled = true;
                GameEditButton.Enabled = true;
                MakeConfigButton.Enabled = true;
                MakeConfigurationFileToolStripMenuItem.Enabled = true;
                _makeGameConfigurationMenuItem.Enabled = true;
                RunGameToolStripMenuItem.Enabled = true;
                _runGameMenuItem.Enabled = true;
                RunGameButton.Enabled = true;
                Game selectedGame = GetSelectedGame();
                //if the selected game has a setup executable
                if (string.IsNullOrWhiteSpace(selectedGame.SetupEXEPath) == false)
                {
                    RunGameSetupToolStripMenuItem.Enabled = true;
                    _runGameSetupMenuItem.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                    SetupPathLabel.Text = "Setup : " + selectedGame.SetupEXEPath;
                }
                else
                {
                    RunGameSetupToolStripMenuItem.Enabled = false;
                    _runGameSetupMenuItem.Enabled = false;
                    RunGameSetupButton.Enabled = false;
                    SetupPathLabel.Text = "Setup : none";
                }
                if (string.IsNullOrWhiteSpace(selectedGame.DOSEXEPath) == false)
                {
                    ExecutablePathLabel.Text = "Executable : " + selectedGame.DOSEXEPath;
                }
                else
                {
                    ExecutablePathLabel.Text = "Executable : none";
                }

                if (string.IsNullOrWhiteSpace(selectedGame.Directory) == false)
                {
                    CMountLabel.Text = "'C:' mount : " + selectedGame.Directory;
                }
                else
                {
                    CMountLabel.Text = "'C:' mount : none";
                }

                if (selectedGame.NoConfig == false)
                {
                    if (string.IsNullOrWhiteSpace(selectedGame.DBConfPath) == false)
                    {
                        CustomConfigurationLabel.Text = "Configuration : " + selectedGame.DBConfPath;
                        _editGameConfigurationMenuItem.Enabled = true;
                        GameEditConfigurationButton.Enabled = true;
                        EditConfigToolStripMenuItem.Enabled = true;
                    }
                    else if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false)
                    {
                        CustomConfigurationLabel.Text = "Configuration : default";
                        _editGameConfigurationMenuItem.Enabled = false;
                        GameEditConfigurationButton.Enabled = false;
                        EditConfigToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        CustomConfigurationLabel.Text = "Configuration : none at all";
                        _editGameConfigurationMenuItem.Enabled = false;
                        GameEditConfigurationButton.Enabled = false;
                        EditConfigToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    CustomConfigurationLabel.Text = "Configuration : none at all";
                    _editGameConfigurationMenuItem.Enabled = false;
                    GameEditConfigurationButton.Enabled = false;
                    EditConfigToolStripMenuItem.Enabled = false;
                }
                if (string.IsNullOrWhiteSpace(selectedGame.CDPath) == false)
                {
                    if (selectedGame.MountAsFloppy == false)
                    {
                        DMountLabel.Text = "'D:' mount :" + selectedGame.CDPath;
                        if (selectedGame.UseIOCTL)
                        {
                            DMountLabel.Text += " (IOCTL in use)";
                        }
                    }
                    else
                    {
                        DMountLabel.Text = "'A:' mount :" + selectedGame.CDPath;
                    }
                }
                else
                {
                    if (selectedGame.MountAsFloppy == false)
                    {
                        DMountLabel.Text = "'D:' mount : none";
                    }

                    if (selectedGame.MountAsFloppy)
                    {
                        DMountLabel.Text = "'A:' mount : none.";
                    }
                }
                if (selectedGame.NoConsole == true)
                {
                    NoConsoleLabel.Text = "No console : " + "yes";
                }
                else
                {
                    NoConsoleLabel.Text = "No console : " + "no";
                }

                if (selectedGame.InFullScreen == true)
                {
                    FullscreenLabel.Text = "Fullscreen : " + "yes";
                }
                else
                {
                    FullscreenLabel.Text = "Fullscreen : " + "no";
                }

                if (selectedGame.QuitOnExit == true)
                {
                    QuitOnExitLabel.Text = "Quit on exit : " + "yes";
                }
                else
                {
                    QuitOnExitLabel.Text = "Quit on exit : " + "no";
                }

                if (string.IsNullOrWhiteSpace(selectedGame.AdditionalCommands) == false)
                {
                    AdditionalCommandsLabel.Text = "Additional commands : " + selectedGame.AdditionalCommands;
                }
                else
                {
                    AdditionalCommandsLabel.Text = "Additional commands : none";
                }
            }
            //if more than one game have been selected
            else if (SelectedListView.SelectedItems.Count > 1)
            {
                //make all the game buttons disabled (except the ones for deleting games)
                _editGameMenuItem.Enabled = false;
                EditSelectedgameToolStripMenuItem.Enabled = false;
                GameEditButton.Enabled = false;
                RunGameToolStripMenuItem.Enabled = false;
                RunGameSetupButton.Enabled = false;
                RunGameSetupToolStripMenuItem.Enabled = false;
                _runGameSetupMenuItem.Enabled = false;
                _runGameMenuItem.Enabled = false;
                RunGameButton.Enabled = false;
                _editGameConfigurationMenuItem.Enabled = false;
                GameEditConfigurationButton.Enabled = false;
                EditConfigToolStripMenuItem.Enabled = false;
                MakeConfigButton.Enabled = true;
                MakeConfigurationFileToolStripMenuItem.Enabled = true;
                _makeGameConfigurationMenuItem.Enabled = true;
            }
            //if no game has been selected
            else if (SelectedListView.SelectedItems.Count == 0)
            {
                _deleteGameMenuItem.Enabled = false;
                DeleteSelectedGameToolStripMenuItem.Enabled = false;
                GameDeleteButton.Enabled = false;
                _editGameMenuItem.Enabled = false;
                EditSelectedgameToolStripMenuItem.Enabled = false;
                GameEditButton.Enabled = false;
                RunGameToolStripMenuItem.Enabled = false;
                RunGameSetupButton.Enabled = false;
                RunGameSetupToolStripMenuItem.Enabled = false;
                _runGameSetupMenuItem.Enabled = false;
                _runGameMenuItem.Enabled = false;
                RunGameButton.Enabled = false;
                _editGameConfigurationMenuItem.Enabled = false;
                GameEditConfigurationButton.Enabled = false;
                EditConfigToolStripMenuItem.Enabled = false;
                MakeConfigButton.Enabled = false;
                MakeConfigurationFileToolStripMenuItem.Enabled = false;
                _makeGameConfigurationMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// EventHandler when a Category (a TabPage) is added (created)
        /// </summary>
        private void CategoryAddButton_Click(object sender, EventArgs e)
        {
            using (var newCategoryForm = new CategoryForm())
            {
                if (newCategoryForm.ShowDialog(this) == DialogResult.OK)
                {
                    RedrawAllUserData();
                }
            }
            
        }

        /// <summary>
        /// EventHandler for when a game is double-clicked (activated), or activated by the Enter key.
        /// </summary>
        private void CurrentListView_ItemActivate(object sender, EventArgs e)
        {
            StartDOSBox(GetDOSBoxPath(), GetSelectedGame(), false, UserDataAccessor.UserData.DBDefaultConfFilePath, UserDataAccessor.UserData.DBDefaultLangFilePath);
        }

        private void StartDOSBox(string dosboxPath, Game selectedGame, bool runSetup, string confFile, string langFile)
        {
            try
            {
                var dosboxProcess = DOSBoxController.StartDOSBox(dosboxPath, DOSBoxController.BuildArgs(selectedGame, runSetup, dosboxPath, confFile, langFile), selectedGame.DBConfPath);
                if (dosboxProcess != null)
                {
                    this.WindowState = FormWindowState.Minimized;
                    dosboxProcess.Exited += OnDOSBoxExit;
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("DOSBox cannot be run (was it deleted ?) !", "Game Launch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnDOSBoxExit(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)(() => { this.WindowState = FormWindowState.Normal; }));
        }

        /// <summary>
        /// EventHandler for when a key is pressed while SelectedListView has focus
        /// </summary>
        private void CurrentListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }
            ListViewItem selectedItem;
            do
            {
                selectedItem = SelectedListView.Items.Cast<ListViewItem>().FirstOrDefault(x => (string)x.Tag == GetSelectedGame()?.Signature);
                if (selectedItem == null)
                {
                    return;
                }
                if (MessageBox.Show(this, "Do you really want to delete this game : " + GetSelectedGame()?.Name + " ?", GameDeleteButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    continue;
                }
                SelectedListView.Items.Remove(selectedItem);
                GetSelectedCategory()?.RemoveChild(GetSelectedGame());
            } while (selectedItem != null);
        }

        /// <summary>
        /// EventHandler for when AmpShell is closed
        /// </summary>
        private void AmpShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserDataAccessor.SaveUserSettings();
        }

        /// <summary>
        /// EventHandler for the ? -> About button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) { using (var aboutBox = new AboutBox()) { aboutBox.ShowDialog(this); } }

        /// <summary>
        /// EventHandler for when the delete button game is clicked
        /// </summary>
        private void GameDeleteButton_Click(object sender, EventArgs e) { KeyEventArgs k = new KeyEventArgs(Keys.Delete); CurrentListView_KeyDown(sender, k); }

        /// <summary>
        /// EventHandler for when the Category delete button is clicked
        /// </summary>
        private void CategoryDeleteButton_Click(object sender, EventArgs e)
        {
            Category selectedCategory = GetSelectedCategory();
            if (UserDataAccessor.UserData.CategoryDeletePrompt != true ||
                MessageBox.Show(this, "Do you really want to delete " + "'" + selectedCategory.Title + "'" + " and all the games inside it ?",
                _deleteCategoryMenuMenuItem.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserDataAccessor.UserData.RemoveChild(selectedCategory);
                TabControl.TabPages.Remove(TabControl.SelectedTab);
            }
            UpdateButtonsState();
        }

        /// <summary>
        /// EventHandler when the user clicks on Tools -> Run DOSBox
        /// which runs DOSBox only with the default .conf (configuration) and .lng (language) files.
        /// </summary>
        private void RunDOSBox_Click(object sender, EventArgs e)
        {
            var dosboxProcess = DOSBoxController.RunOnlyDOSBox(UserDataAccessor.UserData.DBPath, UserDataAccessor.UserData.DBDefaultConfFilePath, UserDataAccessor.UserData.DBDefaultLangFilePath);

            if (dosboxProcess != null)
            {
                this.WindowState = FormWindowState.Minimized;
                dosboxProcess.Exited += OnDOSBoxExit;
            }
        }

        /// <summary>
        /// EventHandler for when AmpShell is shown (happens after AmpShell_Load)
        /// </summary>
        private void AmpShell_Shown(object sender, EventArgs e)
        {
            _ampShellShown = true;
            //select the first TabPage of tabcontrol
            if (TabControl.HasChildren)
            {
                //select the first TabPage
                TabControl.SelectedTab = TabControl.TabPages[0];
                //make the Category edit & delete buttons Enabled
                CategoryEditButton.Enabled = true;
                EditSelectedcategoryToolStripMenuItem.Enabled = true;
                _editCategoryMenuMenuItem.Enabled = true;
                _deleteCategoryMenuMenuItem.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                DeleteSelectedCategoryToolStripMenuItem.Enabled = true;
            }
            //if tabcontrol has no children, then it has no TabPages (categories)
            //so we prompt the user for the title of the first category.
            else
            {
                CategoryAddButton_Click(sender, e);
            }
        }

        /// <summary>
        /// EventHander for File -> Quit.
        /// </summary>
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        /// <summary>
        /// EventHandler for when the user has clicked on the GameAddButton
        /// </summary>
        private void GameAddButton_Click(object sender, EventArgs e)
        {
            var newGame = new Game();
            if (e is DragEventArgs dragArgs)
            {
                string[] files = (string[])dragArgs.Data.GetData(DataFormats.FileDrop);
                var firstFile = files.FirstOrDefault();
                if (string.IsNullOrWhiteSpace(firstFile) == false)
                {
                    if (Path.GetExtension(firstFile).ToUpper(CultureInfo.InvariantCulture) == ".LNK")
                    {
                        firstFile = NativeMethods.ResolveShortcut(firstFile);
                    }
                    newGame.Name = Path.GetDirectoryName(firstFile);
                    newGame.Directory = Path.GetDirectoryName(firstFile);
                    if (File.Exists(firstFile) && (Path.GetExtension(firstFile).ToUpper(CultureInfo.InvariantCulture) == ".COM" || Path.GetExtension(firstFile).ToUpper(CultureInfo.InvariantCulture) == ".EXE" || Path.GetExtension(firstFile).ToUpper(CultureInfo.InvariantCulture) == ".BAT"))
                    {
                        newGame.DOSEXEPath = firstFile;
                    }
                }
            }

            newGame.Signature = UserDataAccessor.GetAUniqueSignature();

            using (var newGameForm = new GameForm(newGame, true))
            {
                if (newGameForm.ShowDialog(this) == DialogResult.OK)
                {
                    Category concernedCategory = GetSelectedCategory();
                    concernedCategory.AddChild(newGameForm.GameInstance);
                    RedrawAllUserData();
                }
            }
        }

        private void SelectCategory(string signature)
        {
            TabControl.SelectedTab = TabControl.TabPages.Cast<TabPage>().FirstOrDefault(x => (string)x.Tag == signature);
        }

        /// <summary>
        /// EventHandler for when the user has finished resizing the window.
        /// </summary>
        private void AmpShell_Resized(object sender, EventArgs e)
        {
            //change the data about the Window's dimensions (restored on next session).
            if (UserDataAccessor.UserData.RememberWindowSize == true)
            {
                UserDataAccessor.UserData.Height = Height;
                UserDataAccessor.UserData.Width = Width;
            }
        }

        /// <summary>
        /// EventHandler for when a category is edited (CategoryEditButton has been clicked)
        /// </summary>
        private void CategoryEditButton_Click(object sender, EventArgs e)
        {
            using (var catEditForm = new CategoryForm((string)TabControl.SelectedTab.Tag))
            {
                if (catEditForm.ShowDialog(this) == DialogResult.OK)
                {
                    TabControl.SelectedTab.Text = catEditForm.ViewModel.Name;
                }
            }
        }

        /// <summary>
        /// EventHandler for when the RunGameSetupButton is clicked
        /// </summary>
        private void RunGameSetupButton_Click(object sender, EventArgs e)
        {
            StartDOSBox(GetDOSBoxPath(), GetSelectedGame(), true, UserDataAccessor.UserData.DBDefaultConfFilePath, UserDataAccessor.UserData.DBDefaultLangFilePath);
        }

        private string GetDOSBoxPath()
        {
            string dosboxPath = GetSelectedGame()?.AlternateDOSBoxExePath;
            if (string.IsNullOrWhiteSpace(dosboxPath))
            {
                dosboxPath = UserDataAccessor.UserData.DBPath;
            }

            return dosboxPath;
        }

        /// <summary>
        /// EventHandler for when the window is (un)maximized
        /// </summary>
        private void AmpShell_Resize(object sender, EventArgs e)
        {
            if (_ampShellShown == true)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    UserDataAccessor.UserData.Fullscreen = true;
                }
                else
                {
                    UserDataAccessor.UserData.Fullscreen = false;
                }
            }
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(var prefsForm = new PreferencesForm())
            {
                if (prefsForm.ShowDialog(this) == DialogResult.OK)
                {
                    _gamesLargeImageList.ImageSize = new Size(UserDataAccessor.UserData.LargeViewModeSize, UserDataAccessor.UserData.LargeViewModeSize);
                    menuStrip.Visible = UserDataAccessor.UserData.MenuBarVisible;
                    _menuBarMenuItem.Checked = UserDataAccessor.UserData.MenuBarVisible;
                    toolStrip.Visible = UserDataAccessor.UserData.ToolBarVisible;
                    _toolBarMenuItem.Checked = UserDataAccessor.UserData.ToolBarVisible;
                    statusStrip.Visible = UserDataAccessor.UserData.StatusBarVisible;
                    _statusBarMenuItem.Checked = UserDataAccessor.UserData.StatusBarVisible;
                    RedrawAllUserData();
                }
            }
            UpdateButtonsState();
        }

        private void RunConfigurationEditorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
            {
                if (File.Exists(UserDataAccessor.UserData.ConfigEditorPath))
                {
                    System.Diagnostics.Process.Start(UserDataAccessor.UserData.ConfigEditorPath);
                }
                else
                {
                    MessageBox.Show("The configuration editor cannot be run (was it deleted ?). Please set it in the preferences.", RunConfigurationEditorButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GameEditConfigurationButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
            {
                Game selectedGame = GetSelectedGame();
                System.Diagnostics.Process.Start(UserDataAccessor.UserData.ConfigEditorPath, selectedGame.DBConfPath + " " + UserDataAccessor.UserData.ConfigEditorAdditionalParameters);
            }
        }

        private void LargeIconViewButton_Click(object sender, EventArgs e)
        {
            ChangeTabDisplayMode(View.LargeIcon);
        }

        private void SmallIconViewButton_Click(object sender, EventArgs e)
        {
            ChangeTabDisplayMode(View.SmallIcon);
        }

        private void TileViewButton_Click(object sender, EventArgs e)
        {
            ChangeTabDisplayMode(View.Tile);
        }

        private void ListViewButton_Click(object sender, EventArgs e)
        {
            ChangeTabDisplayMode(View.List);
        }

        private void DetailsViewButton_Click(object sender, EventArgs e)
        {
            ChangeTabDisplayMode(View.Details);
        }

        private void ChangeTabDisplayMode(View mode)
        {
            var selectedIndex = TabControl.SelectedIndex;
            GetSelectedCategory().ViewMode = mode;
            RedrawAllUserData();
            TabControl.SelectedIndex = selectedIndex;
        }

        private void MenuBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (menuStrip.Visible == true)
            {
                _menuBarMenuItem.Checked = false;
                menuStrip.Visible = false;
            }
            else
            {
                _menuBarMenuItem.Checked = true;
                menuStrip.Visible = true;
            }
            UserDataAccessor.UserData.MenuBarVisible = menuStrip.Visible;
        }

        private void ToolBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (toolStrip.Visible == true)
            {
                _toolBarMenuItem.Checked = false;
                toolStrip.Visible = false;
            }
            else
            {
                _toolBarMenuItem.Checked = true;
                toolStrip.Visible = true;
            }
            UserDataAccessor.UserData.ToolBarVisible = toolStrip.Visible;
        }

        private void StatusBar_ContextMenu_Click(object sender, EventArgs e)
        {
            if (statusStrip.Visible == true)
            {
                _statusBarMenuItem.Checked = false;
                statusStrip.Visible = false;
            }
            else
            {
                _statusBarMenuItem.Checked = true;
                statusStrip.Visible = true;
            }
            UserDataAccessor.UserData.StatusBarVisible = statusStrip.Visible;
        }

        /// <summary>
        /// EventHandler for when the window is moved
        /// </summary>
        private void AmpShell_LocationChanged(object sender, EventArgs e)
        {
            if (UserDataAccessor.UserData.RememberWindowPosition == true && WindowState != FormWindowState.Minimized)
            {
                UserDataAccessor.UserData.X = Location.X;
                UserDataAccessor.UserData.Y = Location.Y;
            }
        }

        private void CurrentListView_ColumnWidthChanged(object sender, EventArgs e)
        {
            var category = GetSelectedCategory();
            if (category.ViewMode != View.Details || SelectedListView.Columns.Count == 0)
            {
                return;
            }
            category.NameColumnWidth = SelectedListView.Columns["NameColumn"].Width;
            category.ExecutableColumnWidth = SelectedListView.Columns["ExecutableColumn"].Width;
            category.CMountColumnWidth = SelectedListView.Columns["CMountColumn"].Width;
            category.SetupExecutableColumnWidth = SelectedListView.Columns["SetupExecutableColumn"].Width;
            category.CustomConfigurationColumnWidth = SelectedListView.Columns["CustomConfigurationColumn"].Width;
            category.DMountColumnWidth = SelectedListView.Columns["DMountColumn"].Width;
            category.MountingOptionsColumnWidth = SelectedListView.Columns["MountingOptionsColumn"].Width;
            category.AdditionnalCommandsColumnWidth = SelectedListView.Columns["AdditionalCommandsColumn"].Width;
            category.NoConsoleColumnWidth = SelectedListView.Columns["NoConsoleColumn"].Width;
            category.FullscreenColumnWidth = SelectedListView.Columns["FullscreenColumn"].Width;
            category.QuitOnExitColumnWidth = SelectedListView.Columns["QuitOnExitColumn"].Width;
        }

        private void UpdateButtonsState()
        {
            LargeIconViewButton.Enabled = false;
            LargeIconToolStripMenuItem.Enabled = false;
            SmallIconToolStripMenuItem.Enabled = false;
            SmallIconViewButton.Enabled = false;
            TilesViewButton.Enabled = false;
            TileToolStripMenuItem.Enabled = false;
            DetailsViewButton.Enabled = false;
            DetailsToolStripMenuItem.Enabled = false;
            ListViewButton.Enabled = false;
            ListToolStripMenuItem.Enabled = false;
            NewGameToolStripMenuItem.Enabled = false;
            _addGameMenuItem.Enabled = false;
            GameAddButton.Enabled = false;
            RunGameButton.Enabled = false;
            _runGameMenuItem.Enabled = false;
            RunGameToolStripMenuItem.Enabled = false;
            GameEditButton.Enabled = false;
            EditSelectedgameToolStripMenuItem.Enabled = false;
            _editGameMenuItem.Enabled = false;
            RunGameSetupButton.Enabled = false;
            RunGameSetupToolStripMenuItem.Enabled = false;
            _runGameSetupMenuItem.Enabled = false;
            CategoryEditButton.Enabled = false;
            EditSelectedcategoryToolStripMenuItem.Enabled = false;
            _editCategoryMenuMenuItem.Enabled = false;
            CategoryDeleteButton.Enabled = false;
            DeleteSelectedCategoryToolStripMenuItem.Enabled = false;
            _deleteCategoryMenuMenuItem.Enabled = false;
            _editGameConfigurationMenuItem.Enabled = false;
            GameEditConfigurationButton.Enabled = false;
            EditConfigToolStripMenuItem.Enabled = false;
            RunConfigurationEditorButton.Enabled = false;
            RunConfigurationEditorToolStripMenuItem.Enabled = false;
            RunDOSBoxButton.Enabled = false;
            RunDOSBoxToolStripMenuItem.Enabled = false;
            EditDefaultConfigurationToolStripMenuItem.Enabled = false;
            EditDefaultConfigurationButton.Enabled = false;
            _makeGameConfigurationMenuItem.Enabled = false;
            MakeConfigButton.Enabled = false;
            MakeConfigurationFileToolStripMenuItem.Enabled = false;
            if (TabControl.HasChildren != false)
            {
                LargeIconViewButton.Enabled = true;
                LargeIconToolStripMenuItem.Enabled = true;
                SmallIconToolStripMenuItem.Enabled = true;
                SmallIconViewButton.Enabled = true;
                TilesViewButton.Enabled = true;
                TileToolStripMenuItem.Enabled = true;
                DetailsViewButton.Enabled = true;
                DetailsToolStripMenuItem.Enabled = true;
                ListViewButton.Enabled = true;
                ListToolStripMenuItem.Enabled = true;
                NewGameToolStripMenuItem.Enabled = true;
                _addGameMenuItem.Enabled = true;
                GameAddButton.Enabled = true;
                if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBPath) == false)
                {
                    RunGameButton.Enabled = true;
                    _runGameMenuItem.Enabled = true;
                    RunGameToolStripMenuItem.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                    RunGameSetupToolStripMenuItem.Enabled = true;
                    _runGameSetupMenuItem.Enabled = true;
                    RunDOSBoxButton.Enabled = true;
                    RunDOSBoxToolStripMenuItem.Enabled = true;
                }
                CategoryEditButton.Enabled = true;
                _editCategoryMenuMenuItem.Enabled = true;
                EditSelectedcategoryToolStripMenuItem.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                DeleteSelectedCategoryToolStripMenuItem.Enabled = true;
                _deleteCategoryMenuMenuItem.Enabled = true;
                GameEditButton.Enabled = true;
                if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false)
                {
                    RunConfigurationEditorButton.Enabled = true;
                    RunConfigurationEditorToolStripMenuItem.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false)
                {
                    EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                    EditDefaultConfigurationButton.Enabled = true;
                    _makeGameConfigurationMenuItem.Enabled = true;
                    MakeConfigButton.Enabled = true;
                    MakeConfigurationFileToolStripMenuItem.Enabled = true;
                }
                CurrentListView_ItemSelectionChanged(this, EventArgs.Empty);
            }
        }

        private void DisplayHelpMessage(string toolTipText)
        {
            AdditionalCommandsLabel.Text = string.Empty;
            ExecutablePathLabel.Text = string.Empty;
            CMountLabel.Text = string.Empty;
            DMountLabel.Text = string.Empty;
            CustomConfigurationLabel.Text = string.Empty;
            QuitOnExitLabel.Text = string.Empty;
            FullscreenLabel.Text = string.Empty;
            NoConsoleLabel.Text = string.Empty;
            SetupPathLabel.Text = string.Empty;
            ExecutablePathLabel.Text = toolTipText;
        }

        private void EditDefaultConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserDataAccessor.UserData.DBDefaultConfFilePath) == false && File.Exists(UserDataAccessor.UserData.DBDefaultConfFilePath) && string.IsNullOrWhiteSpace(UserDataAccessor.UserData.ConfigEditorPath) == false && UserDataAccessor.UserData.ConfigEditorPath != "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found." && File.Exists(UserDataAccessor.UserData.ConfigEditorPath))
            {
                System.Diagnostics.Process.Start(UserDataAccessor.UserData.ConfigEditorPath, UserDataAccessor.UserData.DBDefaultConfFilePath);
            }
            else
            {
                MessageBox.Show("Default configuration or configuration editor missing. Please set them in the preferences.");
            }
        }

        private void MakeConfigButton_Click(object sender, EventArgs e)
        {
            var selectedGame = GetSelectedGame();
            if ((!File.Exists(selectedGame.Directory + "\\" + Path.GetFileName(UserDataAccessor.UserData.DBDefaultConfFilePath))) || (MessageBox.Show(this, "'" + selectedGame.Directory + "\\" + Path.GetFileName(UserDataAccessor.UserData.DBDefaultConfFilePath) + "'" + "already exists, do you want to overwrite it ?", MakeConfigButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                File.Copy(UserDataAccessor.UserData.DBDefaultConfFilePath, selectedGame.Directory + "\\" + Path.GetFileName(UserDataAccessor.UserData.DBDefaultConfFilePath), true);
                selectedGame.DBConfPath = selectedGame.Directory + "\\" + Path.GetFileName(UserDataAccessor.UserData.DBDefaultConfFilePath);
            }
        }

        private void FileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(FileToolStripMenuItem.ToolTipText);
        }

        private void EditToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(EditToolStripMenuItem.ToolTipText);
        }

        private void ToolsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(ToolsToolStripMenuItem.ToolTipText);
        }

        private void ViewToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(ViewToolStripMenuItem.ToolTipText);
        }

        private void HelpToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(HelpToolStripMenuItem.ToolTipText);
        }

        private void CategoryAddButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(CategoryAddButton.ToolTipText);
        }

        private void GameAddButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(GameAddButton.ToolTipText);
        }

        private void RunGameButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(RunGameButton.ToolTipText);
        }

        private void RunGameSetupButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(RunGameSetupButton.ToolTipText);
        }

        private void GameEditButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(GameEditButton.ToolTipText);
        }

        private void GameEditConfigurationButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(GameEditConfigurationButton.ToolTipText);
        }

        private void CategoryEditButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(CategoryEditButton.ToolTipText);
        }

        private void GameDeleteButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(GameDeleteButton.ToolTipText);
        }

        private void CategoryDeleteButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(CategoryDeleteButton.ToolTipText);
        }

        private void RunDOSBoxButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(RunDOSBoxButton.ToolTipText);
        }

        private void RunConfigurationEditorButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(RunConfigurationEditorButton.ToolTipText);
        }

        private void LargeIconViewButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(LargeIconViewButton.ToolTipText);
        }

        private void SmallIconViewButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(SmallIconViewButton.ToolTipText);
        }

        private void TilesViewButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(TilesViewButton.ToolTipText);
        }

        private void ListViewButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(ListViewButton.ToolTipText);
        }

        private void DetailsViewButton_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(DetailsViewButton.ToolTipText);
        }

        private void PreferencesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(PreferencesToolStripMenuItem.ToolTipText);
        }

        private void AboutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(AboutToolStripMenuItem.ToolTipText);
        }

        private void QuitterToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(QuitterToolStripMenuItem.ToolTipText);
        }

        private void EditDefaultConfigurationToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(EditDefaultConfigurationToolStripMenuItem.ToolTipText);
        }

        private void MakeConfigurationFileToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(MakeConfigurationFileToolStripMenuItem.ToolTipText);
        }
    }
}