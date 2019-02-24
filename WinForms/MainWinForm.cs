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
using AmpShell.WinForms.UserControls;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.WinForms
{
    public partial class MainWinForm : Form
    {
        private bool _ampShellShown;
        private int _hoveredTabIndex;
        private ImageList _gamesLargeImageList = new ImageList();
        private ImageList _gamesSmallImageList = new ImageList();
        private ImageList _gamesMediumImageList = new ImageList();
        private ObjectSerializer _xmlSerializer = new ObjectSerializer();
        /// <summary>
        /// Window instance used mainly to do load and save user data through XML (de)serialization
        /// </summary>
        private UserPrefs _userPrefs = new UserPrefs();
        /// <summary>
        /// path to AmpShell.xml
        /// </summary>
        private string _userConfigDataPath;
        /// <summary>
        /// ListView instance used mainly to retrieve the current ListView (in tabcontrol.SelectedTab["GamesListView"])
        /// </summary>
        private ListView _currentListView = new CustomListView();
        /// <summary>
        /// //Contextual pop-up menu (right click)
        /// </summary>
        private ContextMenuStrip _currentListViewContextMenuStrip = new ContextMenuStrip();
        /// <summary>
        /// The items of the context pop-up menu
        /// </summary>
        private ContextMenuStrip _tabContextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _addCategoryMenuMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _deleteCategoryMenuMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _editCategoryMenuMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _addCategoryMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _deleteCategoryMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _editCategoryMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _addGameMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _deleteGameMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _editGameMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _editGameConfigurationMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _makeGameConfigurationMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _runGameMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem _runGameSetupMenuItem = new ToolStripMenuItem();
        private ContextMenuStrip _windowContextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _menuBarMenuItem = new ToolStripMenuItem("Menu bar");
        private ToolStripMenuItem _toolBarMenuItem = new ToolStripMenuItem("Tool bar");
        private ToolStripMenuItem _statusBarMenuItem = new ToolStripMenuItem("Details bar");

        public MainWinForm()
        {
            InitializeComponent();
            _gamesLargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            _gamesMediumImageList.ColorDepth = ColorDepth.Depth32Bit;
            _gamesSmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            _menuBarMenuItem.Click += new EventHandler(MenuBar_AmpCMS_Click);
            _toolBarMenuItem.Click += new EventHandler(ToolBar_AmpCMS_Click);
            _statusBarMenuItem.Click += new EventHandler(StatusBar_AmpCMS_Click);
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
            //They are _all_ named "GamesListView" for casting (retrieving their reference into ltview)
            //The tag propriety of the ListView object could have been used instead of naming + casting...
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
            _currentListView.ColumnWidthChanged += new ColumnWidthChangedEventHandler(CurrentListView_ColumnWidthChanged);
        }

        private void AmpShell_Load(object sender, EventArgs e)
        {
            //If the file named AmpShell.xml doesn't exists inside the directory AmpShell uses the one in the user's profile Application Data directory
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml") == false && File.Exists(Application.StartupPath + "/AmpShell.xml") == false)
            {
                //take the Windows Height and Width (saved on close with XML serializing)
                Width = 640;
                Height = 400;
                _userPrefs.Width = Width;
                _userPrefs.Height = Height;
                //Setup the whole directory path
                if (Directory.GetDirectoryRoot(Application.StartupPath) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(Application.StartupPath) == Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)")
                {
                    _userConfigDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell";
                    //create the directory
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell") == false)
                    {
                        Directory.CreateDirectory(_userConfigDataPath);
                        _userConfigDataPath = _userConfigDataPath + "/AmpShell.xml";
                    }
                }
                else
                {
                    _userConfigDataPath = Application.StartupPath + "/AmpShell.xml";
                }
                //Serializing the data inside Amp for the first run
                _xmlSerializer.Serialize(_userConfigDataPath, _userPrefs, typeof(UserDataRoot));
                _userPrefs = (UserPrefs)_xmlSerializer.Deserialize(_userConfigDataPath, typeof(UserDataRoot));
            }
            //if the file named AmpShell.xml exists inside that directory
            else
            {
                //then, deserialize it in Amp.
                if (File.Exists(Application.StartupPath + "/AmpShell.xml"))
                {
                    _userConfigDataPath = Application.StartupPath + "/AmpShell.xml";
                }
                else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml"))
                {
                    _userConfigDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml";
                }

                _userPrefs = (UserPrefs)_xmlSerializer.Deserialize(_userConfigDataPath, typeof(UserDataRoot)); //CfgPath : Path to AmpShell.xml
                foreach (UserCategory ConcernedCategory in _userPrefs.ListChildren)
                {
                    foreach (UserGame ConcernedGame in ConcernedCategory.ListChildren)
                    {
                        ConcernedGame.DOSEXEPath = ConcernedGame.DOSEXEPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.DBConfPath = ConcernedGame.DBConfPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.AdditionalCommands = ConcernedGame.AdditionalCommands.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.Directory = ConcernedGame.Directory.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.CDPath = ConcernedGame.CDPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.SetupEXEPath = ConcernedGame.SetupEXEPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.Icon = ConcernedGame.Icon.Replace("AppPath", Application.StartupPath);

                    }
                }
                _userPrefs.DBDefaultConfFilePath = _userPrefs.DBDefaultConfFilePath.Replace("AppPath", Application.StartupPath);
                _userPrefs.DBDefaultLangFilePath = _userPrefs.DBDefaultLangFilePath.Replace("AppPath", Application.StartupPath);
                _userPrefs.DBPath = _userPrefs.DBPath.Replace("AppPath", Application.StartupPath);
                _userPrefs.ConfigEditorPath = _userPrefs.ConfigEditorPath.Replace("AppPath", Application.StartupPath);
                _userPrefs.ConfigEditorAdditionalParameters = _userPrefs.ConfigEditorAdditionalParameters.Replace("AppPath", Application.StartupPath);
            }
            if (string.IsNullOrWhiteSpace(_userPrefs.DBPath))
            {
                _userPrefs.DBPath = SearchDOSBox();
            }
            else if (File.Exists(_userPrefs.DBPath) == false)
            {
                _userPrefs.DBPath = SearchDOSBox();
                if (File.Exists(_userPrefs.DBPath))
                {
                    RunDOSBoxToolStripMenuItem.Enabled = true;
                    RunDOSBoxButton.Enabled = true;
                }
            }
            else
            {
                RunDOSBoxToolStripMenuItem.Enabled = true;
                RunDOSBoxButton.Enabled = true;
            }
            if (string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath))
            {
                _userPrefs.ConfigEditorPath = SearchCommonTextEditor();
            }
            else if (File.Exists(_userPrefs.ConfigEditorPath) == false)
            {
                _userPrefs.ConfigEditorPath = SearchCommonTextEditor();
            }
            else
            {
                RunConfigurationEditorButton.Enabled = true;
                RunConfigurationEditorToolStripMenuItem.Enabled = true;
            }
            if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath))
            {
                _userPrefs.DBDefaultConfFilePath = SearchDOSBoxConf(_userPrefs.DBPath);
            }
            else if (File.Exists(_userPrefs.DBDefaultConfFilePath) == false)
            {
                _userPrefs.DBDefaultConfFilePath = SearchDOSBoxConf(_userPrefs.DBPath);
            }

            if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultLangFilePath) == false)
            {
                _userPrefs.DBDefaultLangFilePath = SearchDOSBoxLang(_userPrefs.DBPath);
            }
            else if (File.Exists(_userPrefs.DBDefaultLangFilePath) == false)
            {
                _userPrefs.DBDefaultLangFilePath = SearchDOSBoxLang(_userPrefs.DBPath);
            }

            if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false && string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath) == false)
            {
                EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                EditDefaultConfigurationButton.Enabled = true;
            }
            //Create the TabPages (categories) ListViews, and games inside the ListViews with DisplayUserData 
            DisplayUserData(_userPrefs, _userPrefs.OnlyNames);
        }

        private string SearchCommonTextEditor()
        {
            string confEditorPath = string.Empty;
            if (File.Exists("/usr/bin/mousepad"))
            {
                confEditorPath = "/usr/bin/mousepad";
            }

            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists("/usr/bin/gedit"))
                {
                    confEditorPath = "/usr/bin/gedit";
                }
            }
            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists("/usr/bin/kate"))
                {
                    confEditorPath = "/usr/bin/kate";
                }
            }
            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe"))
                {
                    confEditorPath = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe";
                }
            }
            return confEditorPath;
        }

        private string SearchDOSBoxConf(string DOSBoxExecutablePath)
        {
            string confPath = string.Empty; //returned String
            //search for dosbox.conf
            //first, if the user is using GNU/Linux : test if ~/dosbox.conf (~ = /home/<username>) exists
            //Ubuntu case (dosbox.conf in ~)
            if (_userConfigDataPath == Application.StartupPath + "/AmpShell.xml")
            {
                if (Directory.GetFiles((Application.StartupPath), "*.conf").Length > 0)
                {
                    confPath = Directory.GetFiles((Application.StartupPath), "*.conf")[0];
                }
            }
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dosbox.conf"))
                {
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dosbox.conf";
                }
            }
            //DOSBox ver0.72 case (~/.dosboxrc)
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosboxrc"))
                {
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosboxrc";
                }
            }
            //DOSBox ver0.73 and newer case (~/.dosbox/dosbox.conf)
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox/dosbox.conf"))
                {
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox/dosbox.conf";
                }
            }
            //if ConfPath is _still_ empty, Windows test cases take place.
            if (string.IsNullOrWhiteSpace(confPath))
            {
                //if Local Settings/Application Data/DOSBox exists
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox"))
                {
                    //then, the DOSBox.conf file inside it becomes the default one. 
                    if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*dosbox*.conf").Length > 0)
                    {
                        confPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*dosbox*.conf")[0];
                    }
                }
                else
                {
                    //if dosbox.conf has been generated by DOSBox in the same directory as dosbox.exe
                    //(behavior of DOSBox versions prior to DOSBox version 0.73)
                    if (string.IsNullOrWhiteSpace(DOSBoxExecutablePath) == false)
                    {
                        if (File.Exists(Directory.GetParent(DOSBoxExecutablePath).FullName + "/dosbox.conf"))
                        {
                            confPath = DOSBoxExecutablePath + "/dosbox.conf";
                        }
                    }
                }
            }
            return confPath;
        }

        private string SearchDOSBoxLang(string dosboxExecutablePath)
        {
            //returned string
            string langPath = string.Empty;
            //search for a DOSBox' language file
            //first, if the user is using GNU/Linux : test if ~/*.lng (~ = /home/<username>) exists
            //Ubuntu case (*.lng in ~)
            if (_userConfigDataPath == Application.StartupPath + "/AmpShell.xml")
            {
                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0)
                {
                    langPath = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
                }
            }
            else
            {
                if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.lng").Length > 0)
                {
                    langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.lng")[0];
                }
                //(~/.dosbox/dosbox.lng search case)
                if (string.IsNullOrWhiteSpace(langPath))
                {
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox"))
                    {
                        if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox", "*.lng").Length > 0)
                        {
                            langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox", "*.lng")[0];
                        }
                    }
                }
                //if LangPath is _still_ empty, Windows test cases take place.
                if (string.IsNullOrWhiteSpace(langPath))
                {
                    //if Local Settings/Application Data/DOSBox exists
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox"))
                    {
                        //then, the DOSBox.conf file inside it becomes the default one.
                        if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*.lng").Length > 0)
                        {
                            langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*.lng")[0];
                        }
                    }
                    else
                    {
                        //if dosbox.conf has been generated by DOSBox in the same directory as dosbox.exe
                        //(behavior of DOSBox versions prior to DOSBox version 0.73)
                        if (string.IsNullOrWhiteSpace(dosboxExecutablePath) == false)
                        {
                            if (Directory.GetFiles(Directory.GetParent(dosboxExecutablePath).FullName, "*.lng").Length > 0)
                            {
                                langPath = Directory.GetFiles(Directory.GetParent(dosboxExecutablePath).FullName, "*.lng")[0];
                            }
                        }
                    }
                }
            }
            return langPath;
        }

        private string SearchDOSBox()
        {
            string dosboxPath = string.Empty;
            if (_userConfigDataPath == Application.StartupPath + "/AmpShell.xml" && _userPrefs.PortableMode)
            {
                if (File.Exists(Application.StartupPath + "/dosbox.exe"))
                {
                    dosboxPath = Application.StartupPath + "/dosbox.exe";
                }
                else if (File.Exists(Application.StartupPath + "/dosbox"))
                {
                    dosboxPath = Application.StartupPath + "/dosbox";
                }
            }
            else
            {
                //test if the user is using GNU/Linux
                if (File.Exists("/usr/bin/dosbox"))
                {
                    dosboxPath = "/usr/bin/dosbox";
                }
                else
                {
                    //test if DOSBox is in Program Files/DOSBox-?.?? (Windows x86)
                    if (Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DOSBox*", SearchOption.TopDirectoryOnly).GetLength(0) != 0)
                    {
                        dosboxPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DOSBox*", SearchOption.TopDirectoryOnly)[0];
                        if (File.Exists(dosboxPath + "/dosbox.exe"))
                        {
                            dosboxPath = dosboxPath + "/dosbox.exe";
                        }
                    }
                    else
                    {
                        //test if the user is using Windows x64
                        //in this case, DOSBox's installation directory is most likely in "Program Files (x86)"
                        if (Directory.Exists(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)"))
                        {
                            if (Directory.GetDirectories(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)", "DOSBox*", SearchOption.TopDirectoryOnly).GetLength(0) != 0)
                            {
                                dosboxPath = Directory.GetDirectories(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)", "DOSBox*", SearchOption.TopDirectoryOnly)[0];
                                if (File.Exists(dosboxPath + "/dosbox.exe"))
                                {
                                    dosboxPath = dosboxPath + "/dosbox.exe";
                                }
                            }
                        }
                    }
                }
            }
            //if DOSBoxPath is still empty, say to the user that dosbox's executable cannot be found
            if (string.IsNullOrWhiteSpace(dosboxPath))
            {
                switch (MessageBox.Show("AmpShell cannot find DOSBox, do you want to indicate DOSBox's executable location now ? Choose 'Cancel' to quit.", "Cannot find DOSBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        dosboxPath = string.Empty;
                        Environment.Exit(0);
                        break;
                    case DialogResult.Yes:
                        OpenFileDialog dosboxExeFileDialog = new OpenFileDialog
                        {
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                            Title = "Please indicate DOSBox's executable location...",
                            Filter = "DOSBox executable (dosbox*)|dosbox*"
                        };
                        if (dosboxExeFileDialog.ShowDialog(this) == DialogResult.OK)
                        {
                            //retrieve the selected dosbox.exe path into Amp.DBPath
                            dosboxPath = dosboxExeFileDialog.FileName;
                        }
                        else
                        {
                            dosboxPath = string.Empty;
                        }

                        break;
                    case DialogResult.No:
                        dosboxPath = string.Empty;
                        break;
                }
            }
            return dosboxPath;
        }

        /// <summary>
        /// Create the TabPages (categories) ListViews, and games inside the ListViews
        /// </summary>
        /// <param name="userPrefs">The main user data class instance</param>
        /// <param name="noIcons">whether we display icons for games or not at all</param>
        private void DisplayUserData(UserPrefs userPrefs, bool noIcons)
        {
            //applying the Height and Width previously saved.
            TabControl.Controls.Clear();
            if (userPrefs.RememberWindowSize != false)
            {
                Width = userPrefs.Width;
                Height = userPrefs.Height;
                if (userPrefs.Fullscreen == true)
                {
                    WindowState = FormWindowState.Maximized;
                }
            }
            if (userPrefs.RememberWindowPosition != false)
            {
                SetDesktopLocation(userPrefs.X, userPrefs.Y);
            }

            menuStrip.Visible = userPrefs.MenuBarVisible;
            _menuBarMenuItem.Checked = userPrefs.MenuBarVisible;
            toolStrip.Visible = userPrefs.ToolBarVisible;
            _toolBarMenuItem.Checked = userPrefs.ToolBarVisible;
            statusStrip.Visible = userPrefs.StatusBarVisible;
            _statusBarMenuItem.Checked = userPrefs.StatusBarVisible;
            //for each Category, create a ListView instance.
            foreach (UserCategory categoryToDisplay in userPrefs.ListChildren)
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
                tabltview.Columns.Add("AdditionnalCommandsColumn", "Additionnal commands", categoryToDisplay.AdditionnalCommandsColumnWidth);
                tabltview.Columns.Add("NoConsoleColumn", "No Console ?", categoryToDisplay.NoConsoleColumnWidth);
                tabltview.Columns.Add("FullscreenColumn", "Fullscreen ?", categoryToDisplay.FullscreenColumnWidth);
                tabltview.Columns.Add("QuitOnExitColumn", "Quit on exit ?", categoryToDisplay.QuitOnExitColumnWidth);
                //for each game, create a ListViewItem instance.
                foreach (UserGame gameToDisplay in categoryToDisplay.ListChildren)
                {
                    ListViewItem gameforlt = new ListViewItem(gameToDisplay.Name)
                    {
                        //take the game's signature into the ListViewItem .Name proprety
                        Name = gameToDisplay.Signature
                    };
                    if (noIcons == false)
                    {
                        tabltview.SmallImageList = _gamesSmallImageList;
                        _gamesSmallImageList.ImageSize = new Size(16, 16);
                        tabltview.LargeImageList = _gamesLargeImageList;
                        _gamesLargeImageList.ImageSize = new Size(userPrefs.LargeViewModeSize, userPrefs.LargeViewModeSize);
                        _gamesMediumImageList.ImageSize = new Size(32, 32);
                        _gamesLargeImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application.GetThumbnailImage(userPrefs.LargeViewModeSize, userPrefs.LargeViewModeSize, null, IntPtr.Zero));
                        _gamesMediumImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application1.GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        _gamesSmallImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application1.GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        if (string.IsNullOrWhiteSpace(gameToDisplay.Icon) == false && File.Exists(gameToDisplay.Icon))
                        {
                            _gamesLargeImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(userPrefs.LargeViewModeSize, userPrefs.LargeViewModeSize, null, IntPtr.Zero));
                            _gamesMediumImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                            _gamesSmallImageList.Images.Add(gameToDisplay.Signature, Image.FromFile(gameToDisplay.Icon, true).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                            gameforlt.ImageKey = gameToDisplay.Signature;
                        }
                        else
                        {
                            gameforlt.ImageKey = "DefaultIcon";
                        }
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
                    ListViewItem.ListViewSubItem gameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.AdditionalCommands
                    };
                    gameforlt.SubItems.Add(gameAdditionnalCommandsLVSubItem);
                    ListViewItem.ListViewSubItem gameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.NoConsole.ToString()
                    };
                    gameforlt.SubItems.Add(gameNoConsoleLVSubItem);
                    ListViewItem.ListViewSubItem gameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.InFullScreen.ToString()
                    };
                    gameforlt.SubItems.Add(gameFullscreenLVSubItem);
                    ListViewItem.ListViewSubItem gameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = gameToDisplay.QuitOnExit.ToString()
                    };
                    gameforlt.SubItems.Add(gameQuitOnExitLVSubItem);
                    //add the game to the ListView
                    tabltview.Items.Add(gameforlt);
                }
                //the context menu of the ListView created earlier is the same for all of them.
                tabltview.ContextMenuStrip = _currentListViewContextMenuStrip;
                //Name property used only inside the code. Never displayed.
                tabltview.Name = "GamesListView";
                //fill the TabPage
                tabltview.Dock = DockStyle.Fill;
                if (userPrefs.DefaultIconViewOverride == false)
                {
                    tabltview.View = categoryToDisplay.ViewMode;
                }
                else
                {
                    tabltview.View = userPrefs.CategoriesDefaultViewMode;
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
                //when an item is double-clicked on activated by the Enter key.
                tabltview.ItemActivate += new EventHandler(CurrentListView_ItemActivate);
                //EventHandler when the selected ListViewItem has changed.
                tabltview.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(CurrentListView_ItemSelectionChanged);
                //EventHandler used for the "delete" key (the selected game will be deleted)
                tabltview.KeyDown += new KeyEventHandler(CurrentListView_KeyDown);
                //make the ListView size equal to it's parent control (tabControl) so it will fill it.
                tabltview.Width = TabControl.Width;
                tabltview.Height = TabControl.Height;
                //add the Category by ading a TabPages wich has it's title
                TabControl.TabPages.Add(categoryToDisplay.Title);
                //select the last TabPage (the one we just created)
                TabControl.SelectTab(TabControl.TabPages.Count - 1);
                TabControl.SelectedTab.Name = categoryToDisplay.Signature;
                TabControl.DragOver += new DragEventHandler(SelectedTab_DragOver);
                //EventHandler binding for drag&drop (DragEnter is the event for the control where the drop will occur)
                TabControl.DragEnter += new DragEventHandler(TabControl_DragEnter);
                TabControl.DragDrop += new DragEventHandler(TabControl_DragDrop);
                //add the ListView, named "GamesListView", and now filled with it's games (ListViewItems), to it.
                TabControl.SelectedTab.Controls.Add(tabltview);
                //the ltview private field reference will be the selected TabPage's ListView
                //this is where the .tag property of the ListView tabltview could have been used.
                _currentListView = (ListView)TabControl.SelectedTab.Controls["GamesListView"];
                //drag&drop begins with the ItemDrag eventhandler
                _currentListView.ItemDrag += new ItemDragEventHandler(CurrentListView_ItemDrag);
                //if the reference is not null
                if (_currentListView != null)
                {
                    //sort the items (by their names in alphabetical order)
                    GameAddButton.Enabled = true;
                    NewGameToolStripMenuItem.Enabled = true;
                    _currentListView.Sort();
                }
            }
            if (TabControl.HasChildren)
            {
                TabControl.SelectTab(0);
            }
            //EventHandler when a TabPage is selected by the user
            TabControl.Selected += new TabControlEventHandler(Tabcontrol_Selected);
        }

        private void SelectedTab_DragOver(object sender, DragEventArgs e)
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
        /// EventHandler for when a drag&drop is initiated (drag)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentListView_ItemDrag(object sender, EventArgs e)
        {
            if (_currentListView.FocusedItem != null)
            {
                _currentListView.DoDragDrop(_currentListView.FocusedItem.Text, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// EventHandler for when a drop begins (drag&drop)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private UserCategory GetSelectedCategory()
        {
            foreach (UserCategory selectedCategory in _userPrefs.ListChildren)
            {
                if (selectedCategory.Signature == TabControl.SelectedTab.Name)
                {
                    return selectedCategory;
                }
            }
            return null;
        }

        /// <summary>
        /// EventHandler for drag&drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_DragDrop(object sender, DragEventArgs e)
        {
            UserCategory selectedCategory = GetSelectedCategory();
            foreach (UserGame selectedGame in selectedCategory.ListChildren)
            {
                foreach (ListViewItem SelectedItem in _currentListView.SelectedItems)
                {
                    if (selectedGame.Signature == SelectedItem.Name)
                    {
                        foreach (UserCategory targetCategory in _userPrefs.ListChildren)
                        {
                            if (targetCategory.Signature == TabControl.TabPages[_hoveredTabIndex].Name)
                            {
                                targetCategory.AddChild(selectedGame);
                            }
                        }
                        selectedCategory.RemoveChild(selectedGame);
                    }
                }
            }
            TabControl.SelectTab(_hoveredTabIndex);
            foreach (ListViewItem itemToMove in _currentListView.SelectedItems)
            {
                ListViewItem clonedItem = new ListViewItem();
                clonedItem = (ListViewItem)itemToMove.Clone();
                clonedItem.Name = itemToMove.Name;
                clonedItem.ImageKey = itemToMove.ImageKey;
                _currentListView.Items.Remove(itemToMove);
                _currentListView.Items.Add(clonedItem);
            }
        }

        private UserGame GetSelectedGame()
        {
            UserCategory selectedCategory = GetSelectedCategory();
            foreach (UserGame selectedGame in selectedCategory.ListChildren)
            {
                if (selectedGame.Signature == _currentListView.FocusedItem.Name)
                {
                    return selectedGame;
                }
            }
            return null;
        }

        /// <summary>
        /// Called when the user wants to edit an existing game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEditButton_Click(object sender, EventArgs e)
        {
            UserGame selectedGame = GetSelectedGame();
            //Make an instance of GameForm with the alternate constructor
            GameForm gameEditForm = new GameForm(selectedGame, _userPrefs);
            string oldIconSave = selectedGame.Icon;
            //show GameEdit
            if (gameEditForm.ShowDialog(this) == DialogResult.OK)
            {
                _currentListView.FocusedItem.Text = selectedGame.Name;
                _currentListView.FocusedItem.SubItems[1].Text = selectedGame.DOSEXEPath;
                _currentListView.FocusedItem.SubItems[2].Text = selectedGame.Directory;
                if (_currentListView.FocusedItem.SubItems.Count > 3)
                {
                    _currentListView.FocusedItem.SubItems[3].Text = selectedGame.SetupEXEPath;
                    if (selectedGame.NoConfig == true)
                    {
                        _currentListView.FocusedItem.SubItems[4].Text = "None at all";
                    }
                    else
                    {
                        _currentListView.FocusedItem.SubItems[4].Text = selectedGame.DBConfPath;
                    }

                    _currentListView.FocusedItem.SubItems[5].Text = selectedGame.CDPath;
                    if (selectedGame.UseIOCTL == true)
                    {
                        _currentListView.FocusedItem.SubItems[6].Text = "Use IOCTL";
                    }
                    else if (selectedGame.MountAsFloppy == true)
                    {
                        _currentListView.FocusedItem.SubItems[6].Text = "Mount as a floppy disk (A:)";
                    }
                    else
                    {
                        _currentListView.FocusedItem.SubItems[6].Text = "None";
                    }

                    _currentListView.FocusedItem.SubItems[7].Text = selectedGame.AdditionalCommands;
                    _currentListView.FocusedItem.SubItems[8].Text = selectedGame.NoConsole.ToString();
                    _currentListView.FocusedItem.SubItems[9].Text = selectedGame.InFullScreen.ToString();
                    _currentListView.FocusedItem.SubItems[10].Text = selectedGame.QuitOnExit.ToString();
                }
                if (_userPrefs.OnlyNames == false)
                {
                    if (string.IsNullOrWhiteSpace(oldIconSave) == false)
                    {
                        _gamesLargeImageList.Images.RemoveByKey(selectedGame.Signature);
                        _gamesMediumImageList.Images.RemoveByKey(selectedGame.Signature);
                        _gamesSmallImageList.Images.RemoveByKey(selectedGame.Signature);
                    }
                    if (string.IsNullOrWhiteSpace(selectedGame.Icon) == false)
                    {
                        _gamesSmallImageList.Images.Add(selectedGame.Signature, Image.FromFile(selectedGame.Icon).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        _gamesMediumImageList.Images.Add(selectedGame.Signature, Image.FromFile(selectedGame.Icon).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        _gamesLargeImageList.Images.Add(selectedGame.Signature, Image.FromFile(selectedGame.Icon).GetThumbnailImage(_userPrefs.LargeViewModeSize, _userPrefs.LargeViewModeSize, null, IntPtr.Zero));
                        _currentListView.FocusedItem.ImageKey = selectedGame.Signature;
                    }
                    else
                    {
                        _currentListView.FocusedItem.ImageKey = "DefaultIcon";
                    }
                }
                //if the game setup executable location has been changed and is now empty
                if (string.IsNullOrWhiteSpace(selectedGame.SetupEXEPath))
                {
                    _runGameSetupMenuItem.Enabled = false;
                    RunGameSetupButton.Enabled = false;
                }
                else
                {
                    _runGameSetupMenuItem.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(selectedGame.DBConfPath) == false)
                {
                    GameEditConfigurationButton.Enabled = true;
                    _editGameConfigurationMenuItem.Enabled = true;
                }
                else
                {
                    GameEditConfigurationButton.Enabled = false;
                    _editGameConfigurationMenuItem.Enabled = false;
                }
                _currentListView.Sort();
            }
        }

        /// <summary>
        /// EventHandler for when lvtview (the current tab's ListView) item selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentListView_ItemSelectionChanged(object sender, EventArgs e)
        {
            AdditionnalCommandsLabel.Text = string.Empty;
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
            if (_currentListView.SelectedItems.Count == 1)
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
                UserGame selectedGame = GetSelectedGame();
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
                    else if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false)
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
                            DMountLabel.Text = DMountLabel.Text + " (IOCTL in use)";
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
                    AdditionnalCommandsLabel.Text = "Additionnal commands : " + selectedGame.AdditionalCommands;
                }
                else
                {
                    AdditionnalCommandsLabel.Text = "Additionnal commands : none";
                }
            }
            //if more than one game have been selected
            else if (_currentListView.SelectedItems.Count > 1)
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
            else if (_currentListView.SelectedItems.Count == 0)
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
        /// EventHandler when a TabPage (a category) is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tabcontrol_Selected(object sender, EventArgs e)
        {
            if (TabControl.Controls.Count > 0)
            {
                if (TabControl.SelectedTab.Controls.Count > 0)
                {
                    _currentListView = (ListView)TabControl.SelectedTab.Controls["GamesListView"];
                    _currentListView.AllowDrop = true;
                    TabControl.SelectedTab.AllowDrop = true;
                    _currentListView.ItemDrag += new ItemDragEventHandler(CurrentListView_ItemDrag);
                }
            }
        }

        /// <summary>
        /// EventHandler when a Category (a TabPage) is added (created)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryAddButton_Click(object sender, EventArgs e)
        {
            CategoryForm newCategoryForm = new CategoryForm();
            string newCategorySignature = string.Empty;
            do
            {
                Random randNumber = new Random();
                newCategorySignature = randNumber.Next(1048576).ToString();
            }
            while (IsItUnique(newCategorySignature, _userPrefs) == false);
            newCategoryForm.Category.Signature = newCategorySignature;
            //displaying the CategoryForm prompting the user for the Category's title.
            if (newCategoryForm.ShowDialog(this) == DialogResult.OK)
            {
                //if a proper has been entered
                //create the category (in Amp for the data and in tabControl for the display)
                _userPrefs.AddChild(newCategoryForm.Category);
                TabControl.TabPages.Add(newCategoryForm.Category.Title);
                ListView newListView = new CustomListView();
                newListView.Columns.Add("NameColumn", "Name", newCategoryForm.Category.NameColumnWidth);
                newListView.Columns.Add("ExecutableColumn", "Executable", newCategoryForm.Category.ExecutableColumnWidth);
                newListView.Columns.Add("CMountColumn", "C: Mount", newCategoryForm.Category.CMountColumnWidth);
                newListView.Columns.Add("SetupExecutableColumn", "Setup executable", newCategoryForm.Category.SetupExecutableColumnWidth);
                newListView.Columns.Add("CustomConfigurationColumn", "Custom configuration", newCategoryForm.Category.CustomConfigurationColumnWidth);
                newListView.Columns.Add("DMountColumn", "D: Mount", newCategoryForm.Category.DMountColumnWidth);
                newListView.Columns.Add("MountingOptionsColumn", "Mounting options", newCategoryForm.Category.MountingOptionsColumnWidth);
                newListView.Columns.Add("AdditionnalCommandsColumn", "Additionnal commands", newCategoryForm.Category.AdditionnalCommandsColumnWidth);
                newListView.Columns.Add("NoConsoleColumn", "No Console ?", newCategoryForm.Category.NoConsoleColumnWidth);
                newListView.Columns.Add("FullscreenColumn", "Fullscreen ?", newCategoryForm.Category.FullscreenColumnWidth);
                newListView.Columns.Add("QuitOnExitColumn", "Quit on exit ?", newCategoryForm.Category.QuitOnExitColumnWidth);
                newListView.Dock = DockStyle.Fill;
                newListView.View = _userPrefs.CategoriesDefaultViewMode;
                if (_userPrefs.OnlyNames == false)
                {
                    if (_userPrefs.CategoriesDefaultViewMode == View.LargeIcon)
                    {
                        newListView.LargeImageList = _gamesLargeImageList;
                    }
                    else if (_userPrefs.CategoriesDefaultViewMode == View.Tile)
                    {
                        newListView.LargeImageList = _gamesMediumImageList;
                    }

                    newListView.SmallImageList = _gamesSmallImageList;
                }
                newListView.ContextMenuStrip = _currentListViewContextMenuStrip;
                newListView.ColumnWidthChanged += new ColumnWidthChangedEventHandler(CurrentListView_ColumnWidthChanged);
                newListView.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(CurrentListView_ItemSelectionChanged);
                newListView.ItemActivate += new EventHandler(CurrentListView_ItemActivate);
                newListView.KeyDown += new KeyEventHandler(CurrentListView_KeyDown);
                newListView.Width = Width;
                newListView.Height = Height;
                newListView.Name = "GamesListView";
                TabControl.TabPages[TabControl.TabPages.Count - 1].Controls.Add(newListView);
                //the last created category is selected.
                TabControl.SelectTab(TabControl.TabPages.Count - 1);
                TabControl.SelectedTab.Name = newCategoryForm.Category.Signature;
                TabControl.SelectedTab.AllowDrop = true;
                //make the Category buttons available.
                CategoryEditButton.Enabled = true;
                EditSelectedcategoryToolStripMenuItem.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                DeleteSelectedCategoryToolStripMenuItem.Enabled = true;
                _deleteCategoryMenuMenuItem.Enabled = true;
                NewGameToolStripMenuItem.Enabled = true;
                GameAddButton.Enabled = true;
            }
        }

        private string BuildArgs(bool forSetupExe)
        {
            //Arguments string for DOSBox.exe
            string dosboxArgs = string.Empty;
            if (string.IsNullOrWhiteSpace(_userPrefs.DBPath) == false && _userPrefs.DBPath != "dosbox.exe isn't is the same directory as AmpShell.exe!" && File.Exists(_userPrefs.DBPath))
            {
                UserGame selectedGame = GetSelectedGame();
                string qt = char.ToString('"');
                if (selectedGame.Directory[0] != '/')
                {
                    qt = "'";
                }
                //string for the Game's configuration file.
                string dosboxConfigPath = string.Empty;
                //if the "do not use any config file at all" has not been checked
                if (selectedGame.NoConfig == false)
                {
                    //use at first the game's custom config file
                    if (string.IsNullOrWhiteSpace(selectedGame.DBConfPath) == false)
                    {
                        dosboxConfigPath = selectedGame.DBConfPath;
                    }
                    //if not, use the default dosbox.conf file
                    else if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false && _userPrefs.DBDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                    {
                        dosboxConfigPath = _userPrefs.DBDefaultConfFilePath;
                    }
                }
                //The arguments for DOSBox begins with the game executable (.exe, .bat, or .com)
                if (string.IsNullOrWhiteSpace(selectedGame.DOSEXEPath) == false)
                {
                    if (!forSetupExe)
                    {
                        dosboxArgs = '"' + selectedGame.DOSEXEPath + '"';
                    }
                    else
                    {
                        dosboxArgs = '"' + selectedGame.SetupEXEPath + '"';
                    }
                }
                //the game directory mounted as C (if the DOSEXEPath is specified, the DOSEXEPath parent directory will be mounted as C: by DOSBox
                //hence the "else if" instead of "if".
                else if (string.IsNullOrWhiteSpace(selectedGame.Directory) == false)
                {
                    dosboxArgs = " -c " + '"' + "mount c " + qt + selectedGame.Directory + qt + '"';
                }
                //puting DBCfgPath and Arguments together
                if (string.IsNullOrWhiteSpace(dosboxConfigPath) == false)
                {
                    dosboxArgs = dosboxArgs + " -conf " + '"' + dosboxConfigPath + '"';
                }
                //Path for the default language file used for DOSBox and specified by the user in the Tools menu
                if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultLangFilePath) == false && _userPrefs.DBDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
                {
                    dosboxArgs = dosboxArgs + " -lang " + '"' + _userPrefs.DBDefaultLangFilePath + '"';
                }
                //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
                if (string.IsNullOrWhiteSpace(selectedGame.CDPath) == false)
                {
                    //put ' and _not_ " after imgmount (or else the path will be misunderstood by DOSBox). Paths with spaces will NOT work either way on GNU/Linux!
                    if (selectedGame.CDIsAnImage == true)
                    {
                        dosboxArgs = dosboxArgs + " -c " + '"' + "imgmount";
                        if (selectedGame.MountAsFloppy == true)
                        {
                            dosboxArgs = dosboxArgs + " a " + qt + selectedGame.CDPath + qt + " -t floppy" + '"';
                        }
                        else
                        {
                            dosboxArgs = dosboxArgs + " d " + qt + selectedGame.CDPath + qt + " -t iso" + '"';
                        }
                    }
                    else
                    {
                        if (selectedGame.UseIOCTL == true)
                        {
                            dosboxArgs = dosboxArgs + " -c " + '"' + "mount d " + qt + selectedGame.CDPath + qt + " -t cdrom -usecd 0 -ioctl" + '"';
                        }
                        else if (selectedGame.MountAsFloppy == true)
                        {
                            dosboxArgs = dosboxArgs + " -c " + '"' + "mount a " + qt + selectedGame.CDPath + qt + " -t floppy" + '"';
                        }
                        else
                        {
                            dosboxArgs = dosboxArgs + " -c " + '"' + "mount d " + qt + selectedGame.CDPath + qt;
                        }
                    }
                }
                //Additionnal user commands for the game
                if (string.IsNullOrWhiteSpace(selectedGame.AdditionalCommands) == false)
                {
                    dosboxArgs = dosboxArgs + " " + selectedGame.AdditionalCommands;
                }
                //corresponds to the Fullscreen checkbox in GameForm
                if (selectedGame.InFullScreen == true)
                {
                    dosboxArgs = dosboxArgs + " -fullscreen";
                }
                //corresponds to the "no console" checkbox in the GameForm
                if (selectedGame.NoConsole == true)
                {
                    dosboxArgs = dosboxArgs + " -noconsole";
                }
                //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
                if (selectedGame.QuitOnExit == true)
                {
                    dosboxArgs = dosboxArgs + " -exit";
                }

                return dosboxArgs;
            }
            else
            {
                MessageBox.Show(this, "DOSBox cannot be run (was it deleted ?) !", _runGameMenuItem.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// EventHandler for when a game is double-clicked (activated), or activated by the Enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentListView_ItemActivate(object sender, EventArgs e)
        {
            StartDOSBox(BuildArgs(false));
        }

        /// <summary>
        /// EventHandler for when a key is pressed while ltview has focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentListView_KeyDown(object sender, KeyEventArgs e)
        {
            //if it was the delete key
            if (e.KeyCode == Keys.Delete)
            {
                //search for the selected category
                foreach (UserCategory ConcernedCategory in _userPrefs.ListChildren)
                {
                    if (ConcernedCategory.Signature == TabControl.SelectedTab.Name)
                    {
                        //search for the selected game
                        foreach (UserGame ConcernedGame in ConcernedCategory.ListChildren)
                        {
                            //delete the game data
                            foreach (ListViewItem ConcernedItem in _currentListView.SelectedItems)
                            {
                                if (ConcernedGame.Signature == ConcernedItem.Name)
                                {
                                    if (_userPrefs.GameDeletePrompt == true)
                                    {
                                        if (MessageBox.Show(this, "Do you really want to delete this game : " + ConcernedGame.Name + " ?", GameDeleteButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            ConcernedCategory.RemoveChild(ConcernedGame);
                                            //delete the corresponding ListViewItem
                                            _currentListView.Items.Remove(ConcernedItem);
                                        }
                                    }
                                    else
                                    {
                                        ConcernedCategory.RemoveChild(ConcernedGame);
                                        //delete the corresponding ListViewItem
                                        _currentListView.Items.Remove(ConcernedItem);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// EventHandler for when AmpShell is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            //saves the data inside Amp by serliazing it in AmpShell.xml
            if (!_userPrefs.PortableMode)
            {
                _xmlSerializer.Serialize(_userConfigDataPath, _userPrefs, typeof(UserDataRoot));
            }
            else
            {
                foreach (UserCategory category in _userPrefs.ListChildren)
                {
                    foreach (UserGame game in category.ListChildren)
                    {
                        game.DOSEXEPath = game.DOSEXEPath.Replace(Application.StartupPath, "AppPath");
                        game.DBConfPath = game.DBConfPath.Replace(Application.StartupPath, "AppPath");
                        game.AdditionalCommands = game.AdditionalCommands.Replace(Application.StartupPath, "AppPath");
                        game.Directory = game.Directory.Replace(Application.StartupPath, "AppPath");
                        game.CDPath = game.CDPath.Replace(Application.StartupPath, "AppPath");
                        game.SetupEXEPath = game.SetupEXEPath.Replace(Application.StartupPath, "AppPath");
                        game.Icon = game.Icon.Replace(Application.StartupPath, "AppPath");
                    }
                }
                _userPrefs.DBDefaultConfFilePath = _userPrefs.DBDefaultConfFilePath.Replace(Application.StartupPath, "AppPath");
                _userPrefs.DBDefaultLangFilePath = _userPrefs.DBDefaultLangFilePath.Replace(Application.StartupPath, "AppPath");
                _userPrefs.DBPath = _userPrefs.DBPath.Replace(Application.StartupPath, "AppPath");
                _userPrefs.ConfigEditorPath = _userPrefs.ConfigEditorPath.Replace(Application.StartupPath, "AppPath");
                _userPrefs.ConfigEditorAdditionalParameters = _userPrefs.ConfigEditorAdditionalParameters.Replace(Application.StartupPath, "AppPath");
                _xmlSerializer.Serialize(Application.StartupPath + "/AmpShell.xml", _userPrefs, typeof(UserDataRoot));
            }
        }

        /// <summary>
        /// EventHandler for the ? -> About button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) { AboutBox aboutBox = new AboutBox(); aboutBox.ShowDialog(this); }

        /// <summary>
        /// EventHandler for when the delete button game is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDeleteButton_Click(object sender, EventArgs e) { KeyEventArgs k = new KeyEventArgs(Keys.Delete); CurrentListView_KeyDown(sender, k); }

        /// <summary>
        /// EventHandler for when the Category delete button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryDeleteButton_Click(object sender, EventArgs e)
        {
            UserCategory selectedCategory = GetSelectedCategory();
            if (_userPrefs.CategoryDeletePrompt != true ||
                MessageBox.Show(this, "Do you really want to delete " + "'" + TabControl.SelectedTab.Text + "'" + " and all the games inside it ?",
                _deleteCategoryMenuMenuItem.Text,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //prompt the user if he really wants to delete it.
                //remove the data (the game will be deleted also. List<AmpShell> is provided by RootAmpShell.cs.
                //All the other classes derive from AmpShell, so it makes a tree).
                _userPrefs.RemoveChild(selectedCategory);
                //remove the corresponding displayed TabPage
                TabControl.TabPages.Remove(TabControl.SelectedTab);
            }
            UpdateButtonsState();
        }

        /// <summary>
        /// EventHandler when the user clicks on Tools -> Run DOSBox
        /// wich runs DOSBox only with the default .conf (configuration) and .lng (language) files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunDOSBox_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_userPrefs.DBPath) == false)
            {
                //check first for the lang file
                string languageFile = string.Empty;
                if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultLangFilePath) == false)
                {
                    languageFile = " -lang " + '"' + _userPrefs.DBDefaultLangFilePath + '"';
                }
                //then for the conf file
                if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false)
                {
                    StartDOSBox(" -conf " + '"' + _userPrefs.DBDefaultConfFilePath + '"' + languageFile);
                }
                else
                {
                    StartDOSBox(languageFile);
                }
            }
        }

        private void StartDOSBox(string args)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(_userPrefs.DBPath);
            if (string.IsNullOrWhiteSpace(args) == false)
            {
                psi.Arguments = args;
            }
            System.Diagnostics.Process dosboxProcess = System.Diagnostics.Process.Start(_userPrefs.DBPath, args);
            if (dosboxProcess != null)
            {
                dosboxProcess.EnableRaisingEvents = true;
                BeginInvoke(new Action(() =>
                {
                    WindowState = FormWindowState.Minimized;
                }));
                dosboxProcess.Exited += OnDOSBoxExit;
            }
        }

        private void OnDOSBoxExit(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                WindowState = FormWindowState.Normal;
            }));
        }

        /// <summary>
        /// EventHandler for when AmpShell is shown (happens after AmpShell_Load)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_Shown(object sender, EventArgs e)
        {
            _ampShellShown = true;
            //select the first TabPage of tabcontrol 
            //(if it exists, because AmpShell_Shown is called _automatically_,
            //whether the user has created a category or none!)
            if (TabControl.HasChildren != false)
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
                //reference the selected TabPage's ListView into ltview (with a cast)
                _currentListView = (ListView)TabControl.SelectedTab.Controls["GamesListView"];
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        /// <summary>
        /// EventHandler for when the user has clicked on the GameAddButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameAddButton_Click(object sender, EventArgs e)
        {
            GameForm newGameForm = new GameForm(_userPrefs);
            string newGameSignature = string.Empty;
            do
            {
                Random rand = new Random();
                newGameSignature = rand.Next(1048576).ToString();
            }
            while (IsItUnique(newGameSignature, _userPrefs) == false);
            newGameForm.GameInstance.Signature = newGameSignature;
            if (newGameForm.ShowDialog(this) == DialogResult.OK)
            {
                UserCategory concernedCategory = GetSelectedCategory();
                concernedCategory.AddChild(newGameForm.GameInstance);
                if (string.IsNullOrWhiteSpace(newGameForm.GameInstance.Icon) == false)
                {
                    _gamesLargeImageList.Images.Add(newGameForm.GameInstance.Signature, Image.FromFile(newGameForm.GameInstance.Icon).GetThumbnailImage(_userPrefs.LargeViewModeSize, _userPrefs.LargeViewModeSize, null, IntPtr.Zero));
                    _gamesMediumImageList.Images.Add(newGameForm.GameInstance.Signature, Image.FromFile(newGameForm.GameInstance.Icon).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                    _gamesSmallImageList.Images.Add(newGameForm.GameInstance.Signature, Image.FromFile(newGameForm.GameInstance.Icon).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                }
                _currentListView = (ListView)TabControl.SelectedTab.Controls["GamesListView"];
                //add the ListViewItem corresponding to the new game.
                ListViewItem gameToAdd = new ListViewItem(newGameForm.GameInstance.Name)
                {
                    Name = newGameForm.GameInstance.Signature
                };
                ListViewItem.ListViewSubItem GameDOSEXEPathLVSubItem = new ListViewItem.ListViewSubItem
                {
                    Text = newGameForm.GameInstance.DOSEXEPath
                };
                gameToAdd.SubItems.Add(GameDOSEXEPathLVSubItem);
                ListViewItem.ListViewSubItem GameCMountLVSubItem = new ListViewItem.ListViewSubItem
                {
                    Text = newGameForm.GameInstance.Directory
                };
                gameToAdd.SubItems.Add(GameCMountLVSubItem);
                if (_currentListView.View != View.Tile)
                {
                    ListViewItem.ListViewSubItem GameSetupLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.SetupEXEPath
                    };
                    gameToAdd.SubItems.Add(GameSetupLVSubItem);
                    ListViewItem.ListViewSubItem GameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem();
                    if (newGameForm.GameInstance.NoConfig == true)
                    {
                        GameCustomConfigurationLVSubItem.Text = "None at all";
                    }
                    else
                    {
                        GameCustomConfigurationLVSubItem.Text = newGameForm.GameInstance.DBConfPath;
                    }

                    gameToAdd.SubItems.Add(GameCustomConfigurationLVSubItem);
                    ListViewItem.ListViewSubItem GameDMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.CDPath
                    };
                    gameToAdd.SubItems.Add(GameDMountLVSubItem);
                    ListViewItem.ListViewSubItem GameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                    if (newGameForm.GameInstance.UseIOCTL == true)
                    {
                        GameMountingOptionsLVSubItem.Text = "Use IOCTL";
                    }
                    else if (newGameForm.GameInstance.MountAsFloppy == true)
                    {
                        GameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                    }
                    else
                    {
                        GameMountingOptionsLVSubItem.Text = "None";
                    }

                    gameToAdd.SubItems.Add(GameMountingOptionsLVSubItem);
                    ListViewItem.ListViewSubItem GameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.AdditionalCommands
                    };
                    gameToAdd.SubItems.Add(GameAdditionnalCommandsLVSubItem);
                    ListViewItem.ListViewSubItem GameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.NoConsole.ToString()
                    };
                    gameToAdd.SubItems.Add(GameNoConsoleLVSubItem);
                    ListViewItem.ListViewSubItem GameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.InFullScreen.ToString()
                    };
                    gameToAdd.SubItems.Add(GameFullscreenLVSubItem);
                    ListViewItem.ListViewSubItem GameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = newGameForm.GameInstance.QuitOnExit.ToString()
                    };
                    gameToAdd.SubItems.Add(GameQuitOnExitLVSubItem);
                }
                _currentListView.Items.Add(gameToAdd);
                if (!_userPrefs.OnlyNames)
                {
                    if (string.IsNullOrWhiteSpace(newGameForm.GameInstance.Icon) == false)
                    {
                        gameToAdd.ImageKey = newGameForm.GameInstance.Signature;
                    }
                    else
                    {
                        gameToAdd.ImageKey = "DefaultIcon";
                    }
                }
            }
        }

        /// <summary>
        /// EventHandler for when the user has finished resizing the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_Resized(object sender, EventArgs e)
        {
            //change the data about the Window's dimensions (restored on next session).
            if (_userPrefs.RememberWindowSize == true)
            {
                _userPrefs.Height = Height;
                _userPrefs.Width = Width;
            }
        }

        /// <summary>
        /// EventHandler for when a category is edited (CategoryEditButton has been clicked)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryEditButton_Click(object sender, EventArgs e)
        {
            //search the selected category
            UserCategory selectedCategory = GetSelectedCategory();
            CategoryForm catEditForm = new CategoryForm(selectedCategory)
            {
                Text = "Editing " + selectedCategory.Title + "..."
            };
            if (catEditForm.ShowDialog(this) == DialogResult.OK)
            {
                //modify the displayed category (TabPage) text
                TabControl.SelectedTab.Text = selectedCategory.Title;
            }
        }

        /// <summary>
        /// EventHandler for when the RunGameSetupButton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunGameSetupButton_Click(object sender, EventArgs e)
        {
            //Game arguments for DOSBox
            string dosboxArgs = BuildArgs(true);
            //start DOSBox (Amp.DBPath) with the arguments we've just build up.
            StartDOSBox(dosboxArgs);
        }

        /// <summary>
        /// EventHandler for when the window is (un)maximized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_Resize(object sender, EventArgs e)
        {
            if (_ampShellShown == true)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    _userPrefs.Fullscreen = true;
                }
                else
                {
                    _userPrefs.Fullscreen = false;
                }
            }
        }

        /// <summary>
        /// Used when a new Category is created : it's signature must be unique
        /// so AmpShell can recognize it instantly
        /// </summary>
        /// <param name="signatureToTest"></param>
        /// <param name="userPrefs"></param>
        /// <returns></returns>
        private bool IsItUnique(string signatureToTest, UserPrefs userPrefs)
        {
            foreach (UserCategory otherCat in userPrefs.ListChildren)
            {
                if (otherCat.Signature != signatureToTest)
                {
                    if (otherCat.ListChildren.Length != 0)
                    {
                        foreach (UserGame otherGame in otherCat.ListChildren)
                        {
                            if (otherGame.Signature == signatureToTest)
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main_Prefs prefsForm = new Main_Prefs(_userPrefs);
            if (prefsForm.ShowDialog(this) == DialogResult.OK)
            {
                _gamesLargeImageList.ImageSize = new Size(_userPrefs.LargeViewModeSize, _userPrefs.LargeViewModeSize);
                if (_userPrefs.PortableMode)
                {
                    _xmlSerializer.Serialize(Application.StartupPath + "/AmpShell.xml", _userPrefs, typeof(UserDataRoot));
                }

                menuStrip.Visible = prefsForm.SavedUserPrefs.MenuBarVisible;
                _menuBarMenuItem.Checked = prefsForm.SavedUserPrefs.MenuBarVisible;
                toolStrip.Visible = prefsForm.SavedUserPrefs.ToolBarVisible;
                _toolBarMenuItem.Checked = prefsForm.SavedUserPrefs.ToolBarVisible;
                statusStrip.Visible = prefsForm.SavedUserPrefs.StatusBarVisible;
                _statusBarMenuItem.Checked = prefsForm.SavedUserPrefs.StatusBarVisible;
                _userPrefs.ListChildren = prefsForm.SavedUserPrefs.ListChildren;
                _userPrefs.X = Location.X;
                _userPrefs.Y = Location.Y;
                DisplayUserData(_userPrefs, _userPrefs.OnlyNames);
            }
            UpdateButtonsState();
        }

        private void RunConfigurationEditorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath) == false)
            {
                if (File.Exists(_userPrefs.ConfigEditorPath))
                {
                    System.Diagnostics.Process.Start(_userPrefs.ConfigEditorPath);
                }
                else
                {
                    MessageBox.Show("The configuration editor cannot be run (was it deleted ?). Please set it in the preferences.", RunConfigurationEditorButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GameEditConfigurationButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath) == false)
            {
                UserGame selectedGame = GetSelectedGame();
                System.Diagnostics.Process.Start(_userPrefs.ConfigEditorPath, selectedGame.DBConfPath + " " + _userPrefs.ConfigEditorAdditionalParameters);
            }
        }

        private void LargeIconViewButton_Click(object sender, EventArgs e)
        {
            _currentListView.View = View.LargeIcon;
            _currentListView.LargeImageList = _gamesLargeImageList;
            UserCategory selectedCategory = GetSelectedCategory();
            selectedCategory.ViewMode = _currentListView.View;
        }

        private void SmallIconViewButton_Click(object sender, EventArgs e)
        {
            _currentListView.View = View.SmallIcon;
            UserCategory selectedCategory = GetSelectedCategory();
            selectedCategory.ViewMode = _currentListView.View;
        }

        private void TileViewButton_Click(object sender, EventArgs e)
        {
            _currentListView.View = View.Tile;
            _currentListView.LargeImageList = _gamesMediumImageList;
            UserCategory selectedCategory = GetSelectedCategory();
            selectedCategory.ViewMode = _currentListView.View;
            foreach (ListViewItem ltviewitem in _currentListView.Items)
            {
                while (ltviewitem.SubItems.Count > 3)
                {
                    ltviewitem.SubItems.RemoveAt(ltviewitem.SubItems.Count - 1);
                }
            }
        }

        private void ListViewButton_Click(object sender, EventArgs e)
        {
            _currentListView.View = View.List;
            _currentListView.Columns[0].Width = _currentListView.Width;
            UserCategory selectedCategory = GetSelectedCategory();
            selectedCategory.ViewMode = _currentListView.View;
        }

        private void DetailsViewButton_Click(object sender, EventArgs e)
        {
            if (_currentListView.Columns.Count > 0)
            {
                _currentListView.View = View.Details;
                UserCategory selectedCategory = GetSelectedCategory();
                selectedCategory.ViewMode = _currentListView.View;
                _currentListView.Columns["NameColumn"].Width = selectedCategory.NameColumnWidth;
                _currentListView.Columns["ExecutableColumn"].Width = selectedCategory.ExecutableColumnWidth;
                _currentListView.Columns["CMountColumn"].Width = selectedCategory.CMountColumnWidth;
                _currentListView.Columns["SetupExecutableColumn"].Width = selectedCategory.SetupExecutableColumnWidth;
                _currentListView.Columns["CustomConfigurationColumn"].Width = selectedCategory.CustomConfigurationColumnWidth;
                _currentListView.Columns["DMountColumn"].Width = selectedCategory.DMountColumnWidth;
                _currentListView.Columns["MountingOptionsColumn"].Width = selectedCategory.MountingOptionsColumnWidth;
                _currentListView.Columns["AdditionnalCommandsColumn"].Width = selectedCategory.AdditionnalCommandsColumnWidth;
                _currentListView.Columns["NoConsoleColumn"].Width = selectedCategory.NoConsoleColumnWidth;
                _currentListView.Columns["FullscreenColumn"].Width = selectedCategory.FullscreenColumnWidth;
                _currentListView.Columns["QuitOnExitColumn"].Width = selectedCategory.QuitOnExitColumnWidth;
                foreach (ListViewItem listViewItem in _currentListView.Items)
                {
                    if (_currentListView.Name == selectedCategory.Signature)
                    {
                        foreach (UserGame game in _userPrefs.ListChildren)
                        {
                            if (listViewItem.Name == game.Signature)
                            {
                                if (listViewItem.SubItems.Count == 2)
                                {
                                    ListViewItem.ListViewSubItem gameSetupLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.SetupEXEPath
                                    };
                                    listViewItem.SubItems.Add(gameSetupLVSubItem);
                                    ListViewItem.ListViewSubItem gameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Name = "GameCustomConfiguration"
                                    };
                                    if (game.NoConfig == true)
                                    {
                                        gameCustomConfigurationLVSubItem.Text = "None at all";
                                    }
                                    else
                                    {
                                        gameCustomConfigurationLVSubItem.Text = game.DBConfPath;
                                    }

                                    listViewItem.SubItems.Add(gameCustomConfigurationLVSubItem);
                                    ListViewItem.ListViewSubItem gameDMountLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.CDPath
                                    };
                                    listViewItem.SubItems.Add(gameDMountLVSubItem);
                                    ListViewItem.ListViewSubItem gameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                                    if (game.UseIOCTL == true)
                                    {
                                        gameMountingOptionsLVSubItem.Text = "Use IOCTL";
                                    }
                                    else if (game.MountAsFloppy == true)
                                    {
                                        gameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                                    }
                                    else
                                    {
                                        gameMountingOptionsLVSubItem.Text = "None";
                                    }

                                    listViewItem.SubItems.Add(gameMountingOptionsLVSubItem);
                                    ListViewItem.ListViewSubItem gameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.AdditionalCommands
                                    };
                                    listViewItem.SubItems.Add(gameAdditionnalCommandsLVSubItem);
                                    ListViewItem.ListViewSubItem gameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.NoConsole.ToString()
                                    };
                                    listViewItem.SubItems.Add(gameNoConsoleLVSubItem);
                                    ListViewItem.ListViewSubItem gameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.InFullScreen.ToString()
                                    };
                                    listViewItem.SubItems.Add(gameFullscreenLVSubItem);
                                    ListViewItem.ListViewSubItem gameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = game.QuitOnExit.ToString()
                                    };
                                    listViewItem.SubItems.Add(gameQuitOnExitLVSubItem);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void MenuBar_AmpCMS_Click(object sender, EventArgs e)
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
            _userPrefs.MenuBarVisible = menuStrip.Visible;
        }

        private void ToolBar_AmpCMS_Click(object sender, EventArgs e)
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
            _userPrefs.ToolBarVisible = toolStrip.Visible;
        }

        private void StatusBar_AmpCMS_Click(object sender, EventArgs e)
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
            _userPrefs.StatusBarVisible = statusStrip.Visible;
        }

        /// <summary>
        /// EventHandler for when the window is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_LocationChanged(object sender, EventArgs e)
        {
            if (_userPrefs.RememberWindowPosition == true && WindowState != FormWindowState.Minimized)
            {
                _userPrefs.X = Location.X;
                _userPrefs.Y = Location.Y;
            }
        }

        private void CurrentListView_ColumnWidthChanged(object sender, EventArgs e)
        {
            foreach (UserCategory category in _userPrefs.ListChildren)
            {
                if (category.ViewMode == View.Details)
                {
                    if (TabControl.SelectedTab.Name == category.Signature && _currentListView.Columns.Count > 0)
                    {
                        category.NameColumnWidth = _currentListView.Columns["NameColumn"].Width;
                        category.ExecutableColumnWidth = _currentListView.Columns["ExecutableColumn"].Width;
                        category.CMountColumnWidth = _currentListView.Columns["CMountColumn"].Width;
                        category.SetupExecutableColumnWidth = _currentListView.Columns["SetupExecutableColumn"].Width;
                        category.CustomConfigurationColumnWidth = _currentListView.Columns["CustomConfigurationColumn"].Width;
                        category.DMountColumnWidth = _currentListView.Columns["DMountColumn"].Width;
                        category.MountingOptionsColumnWidth = _currentListView.Columns["MountingOptionsColumn"].Width;
                        category.AdditionnalCommandsColumnWidth = _currentListView.Columns["AdditionnalCommandsColumn"].Width;
                        category.NoConsoleColumnWidth = _currentListView.Columns["NoConsoleColumn"].Width;
                        category.FullscreenColumnWidth = _currentListView.Columns["FullscreenColumn"].Width;
                        category.QuitOnExitColumnWidth = _currentListView.Columns["QuitOnExitColumn"].Width;
                        break;
                    }
                }
            }
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
                if (string.IsNullOrWhiteSpace(_userPrefs.DBPath) == false)
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
                if (string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath) == false)
                {
                    RunConfigurationEditorButton.Enabled = true;
                    RunConfigurationEditorToolStripMenuItem.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false)
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
            AdditionnalCommandsLabel.Text = string.Empty;
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
            if (string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false && File.Exists(_userPrefs.DBDefaultConfFilePath) && string.IsNullOrWhiteSpace(_userPrefs.ConfigEditorPath) == false && _userPrefs.ConfigEditorPath != "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found." && File.Exists(_userPrefs.ConfigEditorPath))
            {
                System.Diagnostics.Process.Start(_userPrefs.ConfigEditorPath, _userPrefs.DBDefaultConfFilePath);
            }
            else
            {
                MessageBox.Show("Default configuration or configuration editor missing. Please set them in the preferences.");
            }
        }

        private void MakeConfigButton_Click(object sender, EventArgs e)
        {
            UserCategory selectedCategory = GetSelectedCategory();
            foreach (ListViewItem selecedViewItem in _currentListView.SelectedItems)
            {
                foreach (UserGame selectedGame in selectedCategory.ListChildren)
                {
                    if (selectedGame.Signature == selecedViewItem.Name &&
                        string.IsNullOrWhiteSpace(_userPrefs.DBDefaultConfFilePath) == false)
                    {
                        if ((!File.Exists(selectedGame.Directory + "/" + Path.GetFileName(_userPrefs.DBDefaultConfFilePath))) || (MessageBox.Show(this, "'" + selectedGame.Directory + "/" + Path.GetFileName(_userPrefs.DBDefaultConfFilePath) + "'" + "already exists, do you want to overwrite it ?", MakeConfigButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            File.Copy(_userPrefs.DBDefaultConfFilePath, selectedGame.Directory + "/" + Path.GetFileName(_userPrefs.DBDefaultConfFilePath), true);
                            selectedGame.DBConfPath = selectedGame.Directory + "/" + Path.GetFileName(_userPrefs.DBDefaultConfFilePath);
                        }
                    }
                }
            }
        }

        #region HelpDisplayMessages
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

        #endregion
    }
}