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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AmpShell
{
    public partial class MainWindow : Form
    {
        private bool AmpShellShown;
        private int HoveredTabIndex;
        private ImageList GamesLargeImageList = new ImageList();
        private ImageList GamesSmallImageList = new ImageList();
        private ImageList GamesMediumImageList = new ImageList();
        private Serializer XMLSerializer = new Serializer();
        /// <summary>
        /// Window instance used mainly to do load and save user data through XML (de)serialization
        /// </summary>
        private Window Amp = new Window();
        /// <summary>
        /// path to AmpShell.xml
        /// </summary>
        private String UserDataPath;
        /// <summary>
        /// ListView instance used mainly to retrieve the current ListView (in tabcontrol.SelectedTab["GamesListView"])
        /// </summary>
        private ListView ltview = new CustomListView();
        /// <summary>
        /// //Contextual pop-up menu (right click)
        /// </summary>
        private ContextMenuStrip ltview_ContextMenuStrip = new ContextMenuStrip();
        /// <summary>
        /// The items of the context pop-up menu
        /// </summary>
        private ContextMenuStrip Categories_ContextMenuStrip = new ContextMenuStrip();
        private ToolStripMenuItem AddCategory = new ToolStripMenuItem();
        private ToolStripMenuItem DeleteCategory = new ToolStripMenuItem();
        private ToolStripMenuItem EditCategory = new ToolStripMenuItem();
        private ToolStripMenuItem AddCategory_CategoriesCMS = new ToolStripMenuItem();
        private ToolStripMenuItem DeleteCategory_CategoriesCMS = new ToolStripMenuItem();
        private ToolStripMenuItem EditCategory_CategoriesCMS = new ToolStripMenuItem();
        private ToolStripMenuItem AddGame = new ToolStripMenuItem();
        private ToolStripMenuItem DeleteGame = new ToolStripMenuItem();
        private ToolStripMenuItem EditGame = new ToolStripMenuItem();
        private ToolStripMenuItem EditGameConfiguration = new ToolStripMenuItem();
        private ToolStripMenuItem MakeGameConfiguration = new ToolStripMenuItem();
        private ToolStripMenuItem RunGame = new ToolStripMenuItem();
        private ToolStripMenuItem RunGameSetup = new ToolStripMenuItem();
        private ContextMenuStrip AmpCMS = new ContextMenuStrip();
        private ToolStripMenuItem MenuBar_AmpCMS = new ToolStripMenuItem("Menu bar");
        private ToolStripMenuItem ToolBar_AmpCMS = new ToolStripMenuItem("Tool bar");
        private ToolStripMenuItem StatusBar_AmpCMS = new ToolStripMenuItem("Details bar");

        public MainWindow()
        {
            InitializeComponent();
            GamesLargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            GamesMediumImageList.ColorDepth = ColorDepth.Depth32Bit;
            GamesSmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            MenuBar_AmpCMS.Click += new EventHandler(MenuBar_AmpCMS_Click);
            ToolBar_AmpCMS.Click += new EventHandler(ToolBar_AmpCMS_Click);
            StatusBar_AmpCMS.Click += new EventHandler(StatusBar_AmpCMS_Click);
            AmpCMS.Items.Add(MenuBar_AmpCMS);
            AmpCMS.Items.Add(ToolBar_AmpCMS);
            AmpCMS.Items.Add(StatusBar_AmpCMS);
            ContextMenuStrip = AmpCMS;
            tabControl.AllowDrop = true;
            //adding text, images, and EventHandlers to the context pop-up menu
            AddGame.Image = GameAddButton.Image;
            AddGame.Text = GameAddButton.Text;
            AddGame.Click += new EventHandler(GameAddButton_Click);
            AddGame.MouseEnter += new EventHandler(GameAddButton_MouseEnter);
            AddGame.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            ltview_ContextMenuStrip.Items.Add(AddGame);
            RunGame.Image = RunGameButton.Image;
            RunGame.Text = RunGameButton.Text;
            RunGame.Click += new EventHandler(Ltview_ItemActivate);
            RunGame.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            RunGame.MouseEnter += new EventHandler(RunGameButton_MouseEnter);
            //Only Enabled when a game is selected
            RunGame.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(RunGame);
            RunGameSetup.Image = RunGameSetupButton.Image;
            RunGameSetup.Text = RunGameSetupButton.Text;
            RunGameSetup.Click += new EventHandler(RunGameSetupButton_Click);
            RunGameSetup.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            RunGameSetup.MouseEnter += new EventHandler(RunGameSetupButton_MouseEnter);
            //Only Enabled when a game is selected
            RunGameSetup.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(RunGameSetup);
            DeleteGame.Image = GameDeleteButton.Image;
            DeleteGame.Text = GameDeleteButton.Text;
            DeleteGame.Click += new EventHandler(GameDeleteButton_Click);
            DeleteGame.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            DeleteGame.MouseEnter += new EventHandler(GameDeleteButton_MouseEnter);
            //Only Enabled when a game is selected
            DeleteGame.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(DeleteGame);
            EditGame.Image = GameEditButton.Image;
            EditGame.Text = GameEditButton.Text;
            EditGame.Click += new EventHandler(GameEditButton_Click);
            EditGame.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            EditGame.MouseEnter += new EventHandler(GameEditButton_MouseEnter);
            //Only Enabled when a game is selected
            EditGame.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(EditGame);
            EditGameConfiguration.Image = GameEditConfigurationButton.Image;
            EditGameConfiguration.Text = GameEditConfigurationButton.Text;
            EditGameConfiguration.Click += new EventHandler(GameEditConfigurationButton_Click);
            EditGameConfiguration.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            EditGameConfiguration.MouseEnter += new EventHandler(GameEditConfigurationButton_MouseEnter);
            //Only Enabled when a game is selected
            EditGameConfiguration.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(EditGameConfiguration);
            MakeGameConfiguration.Image = MakeConfigButton.Image;
            MakeGameConfiguration.Text = MakeConfigButton.Text;
            MakeGameConfiguration.Click += new EventHandler(MakeConfigButton_Click);
            MakeGameConfiguration.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            MakeGameConfiguration.MouseEnter += new EventHandler(MakeConfigurationFileToolStripMenuItem_MouseEnter);
            //Only Enabled when a game is selected
            MakeGameConfiguration.Enabled = false;
            ltview_ContextMenuStrip.Items.Add(MakeGameConfiguration);
            ToolStripSeparator ltview_ContextMenuStripSeparator = new ToolStripSeparator();
            ltview_ContextMenuStrip.Items.Add(ltview_ContextMenuStripSeparator);
            //The Categories are the tabs inside the TabControl. Each tab has only one ListView.
            //They are _all_ named "GamesListView" for casting (retrieving their reference into ltview)
            //The tag propriety of the ListView object could have been used instead of naming + casting...
            AddCategory.Image = CategoryAddButton.Image;
            AddCategory.Text = CategoryAddButton.Text;
            AddCategory.Click += new EventHandler(CategoryAddButton_Click);
            AddCategory.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            AddCategory.MouseEnter += new EventHandler(CategoryAddButton_MouseEnter);
            AddCategory_CategoriesCMS.Image = CategoryAddButton.Image;
            AddCategory_CategoriesCMS.Text = CategoryAddButton.Text;
            AddCategory_CategoriesCMS.Click += new EventHandler(CategoryAddButton_Click);
            AddCategory_CategoriesCMS.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            AddCategory_CategoriesCMS.MouseEnter += new EventHandler(CategoryAddButton_MouseEnter);
            ltview_ContextMenuStrip.Items.Add(AddCategory);
            Categories_ContextMenuStrip.Items.Add(AddCategory_CategoriesCMS);
            EditCategory.Image = CategoryEditButton.Image;
            EditCategory.Text = CategoryEditButton.Text;
            EditCategory.Click += new EventHandler(CategoryEditButton_Click);
            EditCategory.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            EditCategory.MouseEnter += new EventHandler(CategoryEditButton_MouseEnter);
            EditCategory_CategoriesCMS.Image = CategoryEditButton.Image;
            EditCategory_CategoriesCMS.Text = CategoryEditButton.Text;
            EditCategory_CategoriesCMS.Click += new EventHandler(CategoryEditButton_Click);
            EditCategory_CategoriesCMS.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            EditCategory_CategoriesCMS.MouseEnter += new EventHandler(CategoryEditButton_MouseEnter);
            ltview_ContextMenuStrip.Items.Add(EditCategory);
            Categories_ContextMenuStrip.Items.Add(EditCategory_CategoriesCMS);
            DeleteCategory.Image = CategoryDeleteButton.Image;
            DeleteCategory.Text = CategoryDeleteButton.Text;
            DeleteCategory.Click += new EventHandler(CategoryDeleteButton_Click);
            DeleteCategory.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            DeleteCategory.MouseEnter += new EventHandler(CategoryDeleteButton_MouseEnter);
            DeleteCategory_CategoriesCMS.Image = CategoryDeleteButton.Image;
            DeleteCategory_CategoriesCMS.Text = CategoryDeleteButton.Text;
            DeleteCategory_CategoriesCMS.Click += new EventHandler(CategoryDeleteButton_Click);
            DeleteCategory_CategoriesCMS.MouseLeave += new EventHandler(Ltview_ItemSelectionChanged);
            DeleteCategory_CategoriesCMS.MouseEnter += new EventHandler(CategoryDeleteButton_MouseEnter);
            ltview_ContextMenuStrip.Items.Add(DeleteCategory);
            Categories_ContextMenuStrip.Items.Add(DeleteCategory_CategoriesCMS);
            tabControl.ContextMenuStrip = Categories_ContextMenuStrip;
            ltview.ColumnWidthChanged += new ColumnWidthChangedEventHandler(Ltview_ColumnWidthChanged);
        }

        private void AmpShell_Load(object sender, EventArgs e)
        {
            //If the file named AmpShell.xml doesn't exists inside the directory AmpShell uses the one in the user's profile Application Data directory
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml") == false && File.Exists(Application.StartupPath + "/AmpShell.xml") == false)
            {
                //take the Windows Height and Width (saved on close with XML serializing)
                Width = 640;
                Height = 400;
                Amp.Width = Width;
                Amp.Height = Height;
                //Setup the whole directory path
                if (Directory.GetDirectoryRoot(Application.StartupPath) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(Application.StartupPath) == Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)")
                {
                    UserDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell";
                    //create the directory
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell") == false)
                    {
                        Directory.CreateDirectory(UserDataPath);
                        UserDataPath = UserDataPath + "/AmpShell.xml";
                    }
                }
                else
                    UserDataPath = Application.StartupPath + "/AmpShell.xml";
                //Serializing the data inside Amp for the first run
                XMLSerializer.Serialize(UserDataPath, Amp, typeof(AmpShell));
                Amp = (Window)XMLSerializer.Deserialize(UserDataPath, typeof(AmpShell));
            }
            //if the file named AmpShell.xml exists inside that directory
            else
            {
                //then, deserialize it in Amp.
                if (File.Exists(Application.StartupPath + "/AmpShell.xml"))
                    UserDataPath = Application.StartupPath + "/AmpShell.xml";
                else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml"))
                    UserDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/AmpShell/AmpShell.xml";
                Amp = (Window)XMLSerializer.Deserialize(UserDataPath, typeof(AmpShell)); //CfgPath : Path to AmpShell.xml
                foreach (Category ConcernedCategory in Amp.ListChildren)
                {
                    foreach (Game ConcernedGame in ConcernedCategory.ListChildren)
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
                Amp.DBDefaultConfFilePath = Amp.DBDefaultConfFilePath.Replace("AppPath", Application.StartupPath);
                Amp.DBDefaultLangFilePath = Amp.DBDefaultLangFilePath.Replace("AppPath", Application.StartupPath);
                Amp.DBPath = Amp.DBPath.Replace("AppPath", Application.StartupPath);
                Amp.ConfigEditorPath = Amp.ConfigEditorPath.Replace("AppPath", Application.StartupPath);
                Amp.ConfigEditorAdditionalParameters = Amp.ConfigEditorAdditionalParameters.Replace("AppPath", Application.StartupPath);
            }
            if (string.IsNullOrWhiteSpace(Amp.DBPath))
                Amp.DBPath = SearchDOSBox();
            else if (File.Exists(Amp.DBPath) == false)
            {
                Amp.DBPath = SearchDOSBox();
                if (File.Exists(Amp.DBPath))
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
            if (string.IsNullOrWhiteSpace(Amp.ConfigEditorPath))
                Amp.ConfigEditorPath = SearchCommonTextEditor();
            else if (File.Exists(Amp.ConfigEditorPath) == false)
                Amp.ConfigEditorPath = SearchCommonTextEditor();
            else
            {
                RunConfigurationEditorButton.Enabled = true;
                runConfigurationEditorToolStripMenuItem.Enabled = true;
            }
            if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath))
                Amp.DBDefaultConfFilePath = SearchDOSBoxConf(Amp.DBPath);
            else if (File.Exists(Amp.DBDefaultConfFilePath) == false)
                Amp.DBDefaultConfFilePath = SearchDOSBoxConf(Amp.DBPath);
            if (string.IsNullOrWhiteSpace(Amp.DBDefaultLangFilePath) == false)
                Amp.DBDefaultLangFilePath = SearchDOSBoxLang(Amp.DBPath);
            else if (File.Exists(Amp.DBDefaultLangFilePath) == false)
                Amp.DBDefaultLangFilePath = SearchDOSBoxLang(Amp.DBPath);
            if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false && string.IsNullOrWhiteSpace(Amp.ConfigEditorPath) == false)
            {
                EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                EditDefaultConfigurationButton.Enabled = true;
            }
            //Create the TabPages (categories) ListViews, and games inside the ListViews with DisplayUserData 
            DisplayUserData(Amp, Amp.OnlyNames);
        }

        private String SearchCommonTextEditor()
        {
            String confEditorPath = String.Empty;
            if (File.Exists("/usr/bin/mousepad"))
                confEditorPath = "/usr/bin/mousepad";
            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists("/usr/bin/gedit"))
                    confEditorPath = "/usr/bin/gedit";
            }
            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists("/usr/bin/kate"))
                    confEditorPath = "/usr/bin/kate";
            }
            if (string.IsNullOrWhiteSpace(confEditorPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe"))
                    confEditorPath = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, Environment.GetFolderPath(Environment.SpecialFolder.System).Length - 8).ToString() + "notepad.exe";
            }
            return confEditorPath;
        }

        private String SearchDOSBoxConf(String DOSBoxExecutablePath)
        {
            String confPath = String.Empty; //returned String
            //search for dosbox.conf
            //first, if the user is using GNU/Linux : test if ~/dosbox.conf (~ = /home/<username>) exists
            //Ubuntu case (dosbox.conf in ~)
            if (UserDataPath == Application.StartupPath + "/AmpShell.xml")
            {
                if (Directory.GetFiles((Application.StartupPath), "*.conf").Length > 0)
                    confPath = Directory.GetFiles((Application.StartupPath), "*.conf")[0];
            }
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dosbox.conf"))
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/dosbox.conf";
            }
            //DOSBox ver0.72 case (~/.dosboxrc)
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosboxrc"))
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosboxrc";
            }
            //DOSBox ver0.73 and newer case (~/.dosbox/dosbox.conf)
            if (string.IsNullOrWhiteSpace(confPath))
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox/dosbox.conf"))
                    confPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox/dosbox.conf";
            }
            //if ConfPath is _still_ empty, Windows test cases take place.
            if (string.IsNullOrWhiteSpace(confPath))
            {
                //if Local Settings/Application Data/DOSBox exists
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox"))
                {
                    //then, the DOSBox.conf file inside it becomes the default one. 
                    if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*dosbox*.conf").Length > 0)
                        confPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*dosbox*.conf")[0];
                }
                else
                {
                    //if dosbox.conf has been generated by DOSBox in the same directory as dosbox.exe
                    //(behavior of DOSBox versions prior to DOSBox version 0.73)
                    if (string.IsNullOrWhiteSpace(DOSBoxExecutablePath) == false)
                    {
                        if (File.Exists(Directory.GetParent(DOSBoxExecutablePath).FullName + "/dosbox.conf"))
                            confPath = DOSBoxExecutablePath + "/dosbox.conf";
                    }
                }
            }
            return confPath;
        }

        private String SearchDOSBoxLang(String DOSBoxExecutablePath)
        {
            //returned string
            String langPath = String.Empty;
            //search for a DOSBox' language file
            //first, if the user is using GNU/Linux : test if ~/*.lng (~ = /home/<username>) exists
            //Ubuntu case (*.lng in ~)
            if (UserDataPath == Application.StartupPath + "/AmpShell.xml")
            {
                if (Directory.GetFiles(Application.StartupPath, "*.lng").Length > 0)
                    langPath = Directory.GetFiles(Application.StartupPath, "*.lng")[0];
            }
            else
            {
                if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.lng").Length > 0)
                    langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.lng")[0];
                //(~/.dosbox/dosbox.lng search case)
                if (string.IsNullOrWhiteSpace(langPath))
                {
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox"))
                    {
                        if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox", "*.lng").Length > 0)
                            langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/.dosbox", "*.lng")[0];
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
                            langPath = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/DOSBox", "*.lng")[0];
                    }
                    else
                    {
                        //if dosbox.conf has been generated by DOSBox in the same directory as dosbox.exe
                        //(behavior of DOSBox versions prior to DOSBox version 0.73)
                        if (string.IsNullOrWhiteSpace(DOSBoxExecutablePath) == false)
                        {
                            if (Directory.GetFiles(Directory.GetParent(DOSBoxExecutablePath).FullName, "*.lng").Length > 0)
                                langPath = Directory.GetFiles(Directory.GetParent(DOSBoxExecutablePath).FullName, "*.lng")[0];
                        }
                    }
                }
            }
            return langPath;
        }

        private String SearchDOSBox()
        {
            String DOSBoxPath;
            DOSBoxPath = String.Empty;
            if (UserDataPath == Application.StartupPath + "/AmpShell.xml" && Amp.PortableMode)
            {
                if (File.Exists(Application.StartupPath + "/dosbox.exe"))
                    DOSBoxPath = Application.StartupPath + "/dosbox.exe";
                else if (File.Exists(Application.StartupPath + "/dosbox"))
                    DOSBoxPath = Application.StartupPath + "/dosbox";
            }
            else
            {
                //test if the user is using GNU/Linux
                if (File.Exists("/usr/bin/dosbox"))
                    DOSBoxPath = "/usr/bin/dosbox";
                else
                {
                    //test if DOSBox is in Program Files/DOSBox-?.?? (Windows x86)
                    if (Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DOSBox*", SearchOption.TopDirectoryOnly).GetLength(0) != 0)
                    {
                        DOSBoxPath = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DOSBox*", SearchOption.TopDirectoryOnly)[0];
                        if (File.Exists(DOSBoxPath + "/dosbox.exe"))
                            DOSBoxPath = DOSBoxPath + "/dosbox.exe";
                    }
                    else
                    {
                        //test if the user is using Windows x64
                        //in this case, DOSBox's installation directory is most likely in "Program Files (x86)"
                        if (Directory.Exists(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)"))
                        {
                            if (Directory.GetDirectories(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)", "DOSBox*", SearchOption.TopDirectoryOnly).GetLength(0) != 0)
                            {
                                DOSBoxPath = Directory.GetDirectories(Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)", "DOSBox*", SearchOption.TopDirectoryOnly)[0];
                                if (File.Exists(DOSBoxPath + "/dosbox.exe"))
                                    DOSBoxPath = DOSBoxPath + "/dosbox.exe";
                            }
                        }
                    }
                }
            }
            //if DOSBoxPath is still empty, say to the user that dosbox's executable cannot be found
            if (string.IsNullOrWhiteSpace(DOSBoxPath))
            {
                switch (MessageBox.Show("AmpShell cannot find DOSBox, do you want to indicate DOSBox's executable location now ? Choose 'Cancel' to quit.", "Cannot find DOSBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Cancel:
                        DOSBoxPath = String.Empty;
                        Environment.Exit(0);
                        break;
                    case DialogResult.Yes:
                        OpenFileDialog DBexeFD = new OpenFileDialog
                        {
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                            Title = "Please indicate DosBox's executable location...",
                            Filter = "DosBox executable (dosbox*)|dosbox*"
                        };
                        if (DBexeFD.ShowDialog(this) == DialogResult.OK)
                        {
                            //retrieve the selected dosbox.exe path into Amp.DBPath
                            DOSBoxPath = DBexeFD.FileName;
                        }
                        else
                            DOSBoxPath = String.Empty;
                        break;
                    case DialogResult.No:
                        DOSBoxPath = String.Empty;
                        break;
                }
            }
            return DOSBoxPath;
        }

        /// <summary>
        /// Create the TabPages (categories) ListViews, and games inside the ListViews
        /// </summary>
        /// <param name="Amp"></param>
        /// <param name="NoIcons"></param>
        private void DisplayUserData(Window Amp, bool NoIcons)
        {
            //applying the Height and Width previously saved.
            tabControl.Controls.Clear();
            if (Amp.RememberWindowSize != false)
            {
                Width = Amp.Width;
                Height = Amp.Height;
                if (Amp.Fullscreen == true)
                    WindowState = FormWindowState.Maximized;
            }
            if (Amp.RememberWindowPosition != false)
                SetDesktopLocation(Amp.X, Amp.Y);
            menuStrip.Visible = Amp.MenuBarVisible;
            MenuBar_AmpCMS.Checked = Amp.MenuBarVisible;
            toolStrip.Visible = Amp.ToolBarVisible;
            ToolBar_AmpCMS.Checked = Amp.ToolBarVisible;
            statusStrip.Visible = Amp.StatusBarVisible;
            StatusBar_AmpCMS.Checked = Amp.StatusBarVisible;
            //for each Category, create a ListView instance.
            foreach (Category CategoryToDisplay in Amp.ListChildren)
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
                tabltview.Columns.Add("NameColumn", "Name", CategoryToDisplay.NameColumnWidth);
                tabltview.Columns.Add("ExecutableColumn", "Executable", CategoryToDisplay.ExecutableColumnWidth);
                tabltview.Columns.Add("CMountColumn", "C: Mount", CategoryToDisplay.CMountColumnWidth);
                tabltview.Columns.Add("SetupExecutableColumn", "Setup executable", CategoryToDisplay.SetupExecutableColumnWidth);
                tabltview.Columns.Add("CustomConfigurationColumn", "Custom configuration", CategoryToDisplay.CustomConfigurationColumnWidth);
                tabltview.Columns.Add("DMountColumn", "D: Mount", CategoryToDisplay.DMountColumnWidth);
                tabltview.Columns.Add("MountingOptionsColumn", "Mounting options", CategoryToDisplay.MountingOptionsColumnWidth);
                tabltview.Columns.Add("AdditionnalCommandsColumn", "Additionnal commands", CategoryToDisplay.AdditionnalCommandsColumnWidth);
                tabltview.Columns.Add("NoConsoleColumn", "No Console ?", CategoryToDisplay.NoConsoleColumnWidth);
                tabltview.Columns.Add("FullscreenColumn", "Fullscreen ?", CategoryToDisplay.FullscreenColumnWidth);
                tabltview.Columns.Add("QuitOnExitColumn", "Quit on exit ?", CategoryToDisplay.QuitOnExitColumnWidth);
                //for each game, create a ListViewItem instance.
                foreach (Game GameToDisplay in CategoryToDisplay.ListChildren)
                {
                    ListViewItem gameforlt = new ListViewItem(GameToDisplay.Name)
                    {
                        //take the game's signature into the ListViewItem .Name proprety
                        Name = GameToDisplay.Signature
                    };
                    if (NoIcons == false)
                    {
                        tabltview.SmallImageList = GamesSmallImageList;
                        GamesSmallImageList.ImageSize = new Size(16, 16);
                        tabltview.LargeImageList = GamesLargeImageList;
                        GamesLargeImageList.ImageSize = new Size(Amp.LargeViewModeSize, Amp.LargeViewModeSize);
                        GamesMediumImageList.ImageSize = new Size(32, 32);
                        GamesLargeImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application.GetThumbnailImage(Amp.LargeViewModeSize, Amp.LargeViewModeSize, null, IntPtr.Zero));
                        GamesMediumImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application1.GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        GamesSmallImageList.Images.Add("DefaultIcon", global::AmpShell.Properties.Resources.Generic_Application1.GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        if (string.IsNullOrWhiteSpace(GameToDisplay.Icon) == false && File.Exists(GameToDisplay.Icon))
                        {
                            GamesLargeImageList.Images.Add(GameToDisplay.Signature, Image.FromFile(GameToDisplay.Icon, true).GetThumbnailImage(Amp.LargeViewModeSize, Amp.LargeViewModeSize, null, IntPtr.Zero));
                            GamesMediumImageList.Images.Add(GameToDisplay.Signature, Image.FromFile(GameToDisplay.Icon, true).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                            GamesSmallImageList.Images.Add(GameToDisplay.Signature, Image.FromFile(GameToDisplay.Icon, true).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                            gameforlt.ImageKey = GameToDisplay.Signature;
                        }
                        else
                            gameforlt.ImageKey = "DefaultIcon";
                    }
                    ListViewItem.ListViewSubItem GameDOSEXEPathLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.DOSEXEPath
                    };
                    gameforlt.SubItems.Add(GameDOSEXEPathLVSubItem);
                    ListViewItem.ListViewSubItem GameCMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.Directory
                    };
                    gameforlt.SubItems.Add(GameCMountLVSubItem);
                    ListViewItem.ListViewSubItem GameSetupLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.SetupEXEPath
                    };
                    gameforlt.SubItems.Add(GameSetupLVSubItem);
                    ListViewItem.ListViewSubItem GameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Name = "GameCustomConfiguration"
                    };
                    if (GameToDisplay.NoConfig == true)
                        GameCustomConfigurationLVSubItem.Text = "None at all";
                    else
                        GameCustomConfigurationLVSubItem.Text = GameToDisplay.DBConfPath;
                    gameforlt.SubItems.Add(GameCustomConfigurationLVSubItem);
                    ListViewItem.ListViewSubItem GameDMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.CDPath
                    };
                    gameforlt.SubItems.Add(GameDMountLVSubItem);
                    ListViewItem.ListViewSubItem GameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                    if (GameToDisplay.UseIOCTL == true)
                        GameMountingOptionsLVSubItem.Text = "Use IOCTL";
                    else if (GameToDisplay.MountAsFloppy == true)
                        GameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                    else
                        GameMountingOptionsLVSubItem.Text = "None";
                    gameforlt.SubItems.Add(GameMountingOptionsLVSubItem);
                    ListViewItem.ListViewSubItem GameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.AdditionalCommands
                    };
                    gameforlt.SubItems.Add(GameAdditionnalCommandsLVSubItem);
                    ListViewItem.ListViewSubItem GameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.NoConsole.ToString()
                    };
                    gameforlt.SubItems.Add(GameNoConsoleLVSubItem);
                    ListViewItem.ListViewSubItem GameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.InFullScreen.ToString()
                    };
                    gameforlt.SubItems.Add(GameFullscreenLVSubItem);
                    ListViewItem.ListViewSubItem GameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = GameToDisplay.QuitOnExit.ToString()
                    };
                    gameforlt.SubItems.Add(GameQuitOnExitLVSubItem);
                    //add the game to the ListView
                    tabltview.Items.Add(gameforlt);
                }
                //the context menu of the ListView created earlier is the same for all of them.
                tabltview.ContextMenuStrip = ltview_ContextMenuStrip;
                //Name property used only inside the code. Never displayed.
                tabltview.Name = "GamesListView";
                //fill the TabPage
                tabltview.Dock = DockStyle.Fill;
                if (Amp.DefaultIconViewOverride == false)
                    tabltview.View = CategoryToDisplay.ViewMode;
                else
                    tabltview.View = Amp.CategoriesDefaultViewMode;
                if (tabltview.View == View.Tile)
                    tabltview.LargeImageList = GamesMediumImageList;
                if (tabltview.View == View.Details && tabltview.Columns.Count > 0)
                    tabltview.Columns[0].Width = CategoryToDisplay.NameColumnWidth;
                tabltview.ColumnWidthChanged += new ColumnWidthChangedEventHandler(Ltview_ColumnWidthChanged);
                //when an item is double-clicked on activated by the Enter key.
                tabltview.ItemActivate += new EventHandler(Ltview_ItemActivate);
                //EventHandler when the selected ListViewItem has changed.
                tabltview.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(Ltview_ItemSelectionChanged);
                //EventHandler used for the "delete" key (the selected game will be deleted)
                tabltview.KeyDown += new KeyEventHandler(Ltview_KeyDown);
                //make the ListView size equal to it's parent control (tabControl) so it will fill it.
                tabltview.Width = tabControl.Width;
                tabltview.Height = tabControl.Height;
                //add the Category by ading a TabPages wich has it's title
                tabControl.TabPages.Add(CategoryToDisplay.Title);
                //select the last TabPage (the one we just created)
                tabControl.SelectTab(tabControl.TabPages.Count - 1);
                tabControl.SelectedTab.Name = CategoryToDisplay.Signature;
                tabControl.DragOver += new DragEventHandler(SelectedTab_DragOver);
                //EventHandler binding for drag&drop (DragEnter is the event for the control where the drop will occur)
                tabControl.DragEnter += new DragEventHandler(TabControl_DragEnter);
                tabControl.DragDrop += new DragEventHandler(TabControl_DragDrop);
                //add the ListView, named "GamesListView", and now filled with it's games (ListViewItems), to it.
                tabControl.SelectedTab.Controls.Add(tabltview);
                //the ltview private field reference will be the selected TabPage's ListView
                //this is where the .tag property of the ListView tabltview could have been used.
                ltview = (ListView)tabControl.SelectedTab.Controls["GamesListView"];
                //drag&drop begins with the ItemDrag eventhandler
                ltview.ItemDrag += new ItemDragEventHandler(Ltview_ItemDrag);
                //if the reference is not null
                if (ltview != null)
                {
                    //sort the items (by their names in alphabetical order)
                    GameAddButton.Enabled = true;
                    NewGameToolStripMenuItem.Enabled = true;
                    ltview.Sort();
                }
            }
            if (tabControl.HasChildren)
                tabControl.SelectTab(0);
            //EventHandler when a TabPage is selected by the user
            tabControl.Selected += new TabControlEventHandler(Tabcontrol_Selected);
        }

        private void SelectedTab_DragOver(object sender, DragEventArgs e)
        {
            Point pos = tabControl.PointToClient(MousePosition);
            for (int ix = 0; ix < tabControl.TabCount; ix++)
            {
                if (tabControl.GetTabRect(ix).Contains(pos))
                {
                    HoveredTabIndex = ix;
                    break;
                }
            }
        }

        /// <summary>
        /// EventHandler for when a drag&drop is initiated (drag)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltview_ItemDrag(object sender, EventArgs e)
        {
            if (ltview.FocusedItem != null)
                ltview.DoDragDrop(ltview.FocusedItem.Text, DragDropEffects.Move);
        }

        /// <summary>
        /// EventHandler for when a drop begins (drag&drop)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.UnicodeText))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private Category GetSelectedCat()
        {
            foreach (Category ConcernedCat in Amp.ListChildren)
            {
                if (ConcernedCat.Signature == tabControl.SelectedTab.Name)
                    return ConcernedCat;
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
            Category ConcernedCat = GetSelectedCat();
            foreach (Game ConcernedGame in ConcernedCat.ListChildren)
            {
                foreach (ListViewItem SelectedItem in ltview.SelectedItems)
                {
                    if (ConcernedGame.Signature == SelectedItem.Name)
                    {
                        foreach (Category TargetCat in Amp.ListChildren)
                        {
                            if (TargetCat.Signature == tabControl.TabPages[HoveredTabIndex].Name)
                                TargetCat.AddChild(ConcernedGame);
                        }
                        ConcernedCat.RemoveChild(ConcernedGame);
                    }
                }
            }
            ListView CurrentListView = new CustomListView();
            CurrentListView = ltview;
            tabControl.SelectTab(HoveredTabIndex);
            foreach (ListViewItem ItemToMove in CurrentListView.SelectedItems)
            {
                ListViewItem ClonedItem = new ListViewItem();
                ClonedItem = (ListViewItem)ItemToMove.Clone();
                ClonedItem.Name = ItemToMove.Name;
                ClonedItem.ImageKey = ItemToMove.ImageKey;
                CurrentListView.Items.Remove(ItemToMove);
                ltview.Items.Add(ClonedItem);
            }
        }

        private Game GetSelectedGame()
        {
            Category ConcernedCat = GetSelectedCat();
            foreach (Game ConcernedGame in ConcernedCat.ListChildren)
            {
                if (ConcernedGame.Signature == ltview.FocusedItem.Name)
                    return ConcernedGame;
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
            Game ConcernedGame = GetSelectedGame();
            //Make an instance of GameForm with the alternate constructor
            GameForm GameEdit = new GameForm(ConcernedGame, Amp);
            String OldIconSave = ConcernedGame.Icon;
            //show GameEdit
            if (GameEdit.ShowDialog(this) == DialogResult.OK)
            {
                ltview.FocusedItem.Text = ConcernedGame.Name;
                ltview.FocusedItem.SubItems[1].Text = ConcernedGame.DOSEXEPath;
                ltview.FocusedItem.SubItems[2].Text = ConcernedGame.Directory;
                if (ltview.FocusedItem.SubItems.Count > 3)
                {
                    ltview.FocusedItem.SubItems[3].Text = ConcernedGame.SetupEXEPath;
                    if (ConcernedGame.NoConfig == true)
                        ltview.FocusedItem.SubItems[4].Text = "None at all";
                    else
                        ltview.FocusedItem.SubItems[4].Text = ConcernedGame.DBConfPath;
                    ltview.FocusedItem.SubItems[5].Text = ConcernedGame.CDPath;
                    if (ConcernedGame.UseIOCTL == true)
                        ltview.FocusedItem.SubItems[6].Text = "Use IOCTL";
                    else if (ConcernedGame.MountAsFloppy == true)
                        ltview.FocusedItem.SubItems[6].Text = "Mount as a floppy disk (A:)";
                    else
                        ltview.FocusedItem.SubItems[6].Text = "None";
                    ltview.FocusedItem.SubItems[7].Text = ConcernedGame.AdditionalCommands;
                    ltview.FocusedItem.SubItems[8].Text = ConcernedGame.NoConsole.ToString();
                    ltview.FocusedItem.SubItems[9].Text = ConcernedGame.InFullScreen.ToString();
                    ltview.FocusedItem.SubItems[10].Text = ConcernedGame.QuitOnExit.ToString();
                }
                if (Amp.OnlyNames == false)
                {
                    if (string.IsNullOrWhiteSpace(OldIconSave) == false)
                    {
                        GamesLargeImageList.Images.RemoveByKey(ConcernedGame.Signature);
                        GamesMediumImageList.Images.RemoveByKey(ConcernedGame.Signature);
                        GamesSmallImageList.Images.RemoveByKey(ConcernedGame.Signature);
                    }
                    if (string.IsNullOrWhiteSpace(ConcernedGame.Icon) == false)
                    {
                        GamesSmallImageList.Images.Add(ConcernedGame.Signature, Image.FromFile(ConcernedGame.Icon).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                        GamesMediumImageList.Images.Add(ConcernedGame.Signature, Image.FromFile(ConcernedGame.Icon).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                        GamesLargeImageList.Images.Add(ConcernedGame.Signature, Image.FromFile(ConcernedGame.Icon).GetThumbnailImage(Amp.LargeViewModeSize, Amp.LargeViewModeSize, null, IntPtr.Zero));
                        ltview.FocusedItem.ImageKey = ConcernedGame.Signature;
                    }
                    else
                        ltview.FocusedItem.ImageKey = "DefaultIcon";
                }
                //if the game setup executable location has been changed and is now empty
                if (string.IsNullOrWhiteSpace(ConcernedGame.SetupEXEPath))
                {
                    RunGameSetup.Enabled = false;
                    RunGameSetupButton.Enabled = false;
                }
                else
                {
                    RunGameSetup.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(ConcernedGame.DBConfPath) == false)
                {
                    GameEditConfigurationButton.Enabled = true;
                    EditGameConfiguration.Enabled = true;
                }
                else
                {
                    GameEditConfigurationButton.Enabled = false;
                    EditGameConfiguration.Enabled = false;
                }
                ltview.Sort();
            }
        }

        /// <summary>
        /// EventHandler for when lvtview (the current tab's ListView) item selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltview_ItemSelectionChanged(object sender, EventArgs e)
        {
            AdditionnalCommandsLabel.Text = String.Empty;
            ExecutablePathLabel.Text = String.Empty;
            CMountLabel.Text = String.Empty;
            SetupPathLabel.Text = String.Empty;
            DMountLabel.Text = String.Empty;
            CustomConfigurationLabel.Text = String.Empty;
            QuitOnExitLabel.Text = String.Empty;
            FullscreenLabel.Text = String.Empty;
            NoConsoleLabel.Text = String.Empty;
            //several games can be selected at once, but it is only meant for drag&drop between categories
            //Besides, running more than one game (one DOSBox instance) at once can be CPU intensive...
            //if 1 game has been selected
            if (ltview.SelectedItems.Count == 1)
            {
                DeleteGame.Enabled = true;
                deleteSelectedGameToolStripMenuItem.Enabled = true;
                GameDeleteButton.Enabled = true;
                EditGame.Enabled = true;
                editSelectedgameToolStripMenuItem.Enabled = true;
                GameEditButton.Enabled = true;
                MakeConfigButton.Enabled = true;
                MakeConfigurationFileToolStripMenuItem.Enabled = true;
                MakeGameConfiguration.Enabled = true;
                RunGameToolStripMenuItem.Enabled = true;
                RunGame.Enabled = true;
                RunGameButton.Enabled = true;
                Game ConcernedGame = GetSelectedGame();
                //if the selected game has a setup executable
                if (string.IsNullOrWhiteSpace(ConcernedGame.SetupEXEPath) == false)
                {
                    RunGameSetupToolStripMenuItem.Enabled = true;
                    RunGameSetup.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                    SetupPathLabel.Text = "Setup : " + ConcernedGame.SetupEXEPath;
                }
                else
                {
                    RunGameSetupToolStripMenuItem.Enabled = false;
                    RunGameSetup.Enabled = false;
                    RunGameSetupButton.Enabled = false;
                    SetupPathLabel.Text = "Setup : none";
                }
                if (string.IsNullOrWhiteSpace(ConcernedGame.DOSEXEPath) == false)
                    ExecutablePathLabel.Text = "Executable : " + ConcernedGame.DOSEXEPath;
                else
                    ExecutablePathLabel.Text = "Executable : none";
                if (string.IsNullOrWhiteSpace(ConcernedGame.Directory) == false)
                    CMountLabel.Text = "'C:' mount : " + ConcernedGame.Directory;
                else
                    CMountLabel.Text = "'C:' mount : none";
                if (ConcernedGame.NoConfig == false)
                {
                    if (string.IsNullOrWhiteSpace(ConcernedGame.DBConfPath) == false)
                    {
                        CustomConfigurationLabel.Text = "Configuration : " + ConcernedGame.DBConfPath;
                        EditGameConfiguration.Enabled = true;
                        GameEditConfigurationButton.Enabled = true;
                        editConfigToolStripMenuItem.Enabled = true;
                    }
                    else if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false)
                    {
                        CustomConfigurationLabel.Text = "Configuration : default";
                        EditGameConfiguration.Enabled = false;
                        GameEditConfigurationButton.Enabled = false;
                        editConfigToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        CustomConfigurationLabel.Text = "Configuration : none at all";
                        EditGameConfiguration.Enabled = false;
                        GameEditConfigurationButton.Enabled = false;
                        editConfigToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    CustomConfigurationLabel.Text = "Configuration : none at all";
                    EditGameConfiguration.Enabled = false;
                    GameEditConfigurationButton.Enabled = false;
                    editConfigToolStripMenuItem.Enabled = false;
                }
                if (string.IsNullOrWhiteSpace(ConcernedGame.CDPath) == false)
                {
                    if (ConcernedGame.MountAsFloppy == false)
                    {
                        DMountLabel.Text = "'D:' mount :" + ConcernedGame.CDPath;
                        if (ConcernedGame.UseIOCTL)
                            DMountLabel.Text = DMountLabel.Text + " (IOCTL in use)";
                    }
                    else
                        DMountLabel.Text = "'A:' mount :" + ConcernedGame.CDPath;
                }
                else
                {
                    if (ConcernedGame.MountAsFloppy == false)
                        DMountLabel.Text = "'D:' mount : none";
                    if (ConcernedGame.MountAsFloppy)
                        DMountLabel.Text = "'A:' mount : none.";
                }
                if (ConcernedGame.NoConsole == true)
                    NoConsoleLabel.Text = "No console : " + "yes";
                else
                    NoConsoleLabel.Text = "No console : " + "no";
                if (ConcernedGame.InFullScreen == true)
                    FullscreenLabel.Text = "Fullscreen : " + "yes";
                else
                    FullscreenLabel.Text = "Fullscreen : " + "no";
                if (ConcernedGame.QuitOnExit == true)
                    QuitOnExitLabel.Text = "Quit on exit : " + "yes";
                else
                    QuitOnExitLabel.Text = "Quit on exit : " + "no";
                if (string.IsNullOrWhiteSpace(ConcernedGame.AdditionalCommands) == false)
                    AdditionnalCommandsLabel.Text = "Additionnal commands : " + ConcernedGame.AdditionalCommands;
                else
                    AdditionnalCommandsLabel.Text = "Additionnal commands : none";
            }
            //if more than one game have been selected
            else if (ltview.SelectedItems.Count > 1)
            {
                //make all the game buttons disabled (except the ones for deleting games)
                EditGame.Enabled = false;
                editSelectedgameToolStripMenuItem.Enabled = false;
                GameEditButton.Enabled = false;
                RunGameToolStripMenuItem.Enabled = false;
                RunGameSetupButton.Enabled = false;
                RunGameSetupToolStripMenuItem.Enabled = false;
                RunGameSetup.Enabled = false;
                RunGame.Enabled = false;
                RunGameButton.Enabled = false;
                EditGameConfiguration.Enabled = false;
                GameEditConfigurationButton.Enabled = false;
                editConfigToolStripMenuItem.Enabled = false;
                MakeConfigButton.Enabled = true;
                MakeConfigurationFileToolStripMenuItem.Enabled = true;
                MakeGameConfiguration.Enabled = true;
            }
            //if no game has been selected
            else if (ltview.SelectedItems.Count == 0)
            {
                DeleteGame.Enabled = false;
                deleteSelectedGameToolStripMenuItem.Enabled = false;
                GameDeleteButton.Enabled = false;
                EditGame.Enabled = false;
                editSelectedgameToolStripMenuItem.Enabled = false;
                GameEditButton.Enabled = false;
                RunGameToolStripMenuItem.Enabled = false;
                RunGameSetupButton.Enabled = false;
                RunGameSetupToolStripMenuItem.Enabled = false;
                RunGameSetup.Enabled = false;
                RunGame.Enabled = false;
                RunGameButton.Enabled = false;
                EditGameConfiguration.Enabled = false;
                GameEditConfigurationButton.Enabled = false;
                editConfigToolStripMenuItem.Enabled = false;
                MakeConfigButton.Enabled = false;
                MakeConfigurationFileToolStripMenuItem.Enabled = false;
                MakeGameConfiguration.Enabled = false;
            }
        }

        /// <summary>
        /// EventHandler when a TabPage (a category) is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tabcontrol_Selected(object sender, EventArgs e)
        {
            if (tabControl.Controls.Count > 0)
            {
                if (tabControl.SelectedTab.Controls.Count > 0)
                {
                    ltview = (ListView)tabControl.SelectedTab.Controls["GamesListView"];
                    ltview.AllowDrop = true;
                    tabControl.SelectedTab.AllowDrop = true;
                    ltview.ItemDrag += new ItemDragEventHandler(Ltview_ItemDrag);
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
            CategoryForm NewCatForm = new CategoryForm();
            String NewCatSignature = String.Empty;
            do
            {
                Random Rand = new Random();
                NewCatSignature = Rand.Next(1048576).ToString();
            }
            while (IsItUnique(NewCatSignature, Amp) == false);
            NewCatForm.Cat.Signature = NewCatSignature;
            //displaying the CategoryForm prompting the user for the Category's title.
            if (NewCatForm.ShowDialog(this) == DialogResult.OK)
            {
                //if a proper has been entered
                //create the category (in Amp for the data and in tabControl for the display)
                Amp.AddChild(NewCatForm.Cat);
                tabControl.TabPages.Add(NewCatForm.Cat.Title);
                ListView Newltview = new CustomListView();
                Newltview.Columns.Add("NameColumn", "Name", NewCatForm.Cat.NameColumnWidth);
                Newltview.Columns.Add("ExecutableColumn", "Executable", NewCatForm.Cat.ExecutableColumnWidth);
                Newltview.Columns.Add("CMountColumn", "C: Mount", NewCatForm.Cat.CMountColumnWidth);
                Newltview.Columns.Add("SetupExecutableColumn", "Setup executable", NewCatForm.Cat.SetupExecutableColumnWidth);
                Newltview.Columns.Add("CustomConfigurationColumn", "Custom configuration", NewCatForm.Cat.CustomConfigurationColumnWidth);
                Newltview.Columns.Add("DMountColumn", "D: Mount", NewCatForm.Cat.DMountColumnWidth);
                Newltview.Columns.Add("MountingOptionsColumn", "Mounting options", NewCatForm.Cat.MountingOptionsColumnWidth);
                Newltview.Columns.Add("AdditionnalCommandsColumn", "Additionnal commands", NewCatForm.Cat.AdditionnalCommandsColumnWidth);
                Newltview.Columns.Add("NoConsoleColumn", "No Console ?", NewCatForm.Cat.NoConsoleColumnWidth);
                Newltview.Columns.Add("FullscreenColumn", "Fullscreen ?", NewCatForm.Cat.FullscreenColumnWidth);
                Newltview.Columns.Add("QuitOnExitColumn", "Quit on exit ?", NewCatForm.Cat.QuitOnExitColumnWidth);
                Newltview.Dock = DockStyle.Fill;
                Newltview.View = Amp.CategoriesDefaultViewMode;
                if (Amp.OnlyNames == false)
                {
                    if (Amp.CategoriesDefaultViewMode == View.LargeIcon)
                        Newltview.LargeImageList = GamesLargeImageList;
                    else if (Amp.CategoriesDefaultViewMode == View.Tile)
                        Newltview.LargeImageList = GamesMediumImageList;
                    Newltview.SmallImageList = GamesSmallImageList;
                }
                Newltview.ContextMenuStrip = ltview_ContextMenuStrip;
                Newltview.ColumnWidthChanged += new ColumnWidthChangedEventHandler(Ltview_ColumnWidthChanged);
                Newltview.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(Ltview_ItemSelectionChanged);
                Newltview.ItemActivate += new EventHandler(Ltview_ItemActivate);
                Newltview.KeyDown += new KeyEventHandler(Ltview_KeyDown);
                Newltview.Width = Width;
                Newltview.Height = Height;
                Newltview.Name = "GamesListView";
                tabControl.TabPages[tabControl.TabPages.Count - 1].Controls.Add(Newltview);
                //the last created category is selected.
                tabControl.SelectTab(tabControl.TabPages.Count - 1);
                tabControl.SelectedTab.Name = NewCatForm.Cat.Signature;
                tabControl.SelectedTab.AllowDrop = true;
                //make the Category buttons available.
                CategoryEditButton.Enabled = true;
                editSelectedcategoryToolStripMenuItem.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                deleteSelectedCategoryToolStripMenuItem.Enabled = true;
                DeleteCategory.Enabled = true;
                NewGameToolStripMenuItem.Enabled = true;
                GameAddButton.Enabled = true;
            }
        }

        private String BuildArgs(bool Setup)
        {
            //Arguments string for DOSBox.exe
            String Arguments = String.Empty;
            if (string.IsNullOrWhiteSpace(Amp.DBPath) == false && Amp.DBPath != "dosbox.exe isn't is the same directory as AmpShell.exe!" && File.Exists(Amp.DBPath))
            {
                Game ConcernedGame = GetSelectedGame();
                String qt = char.ToString('"');
                if (ConcernedGame.Directory[0] != '/')
                    qt = "'";
                //string for the Game's configuration file.
                String DBCfgPath = String.Empty;
                //if the "do not use any config file at all" has not been checked
                if (ConcernedGame.NoConfig == false)
                {
                    //use at first the game's custom config file
                    if (string.IsNullOrWhiteSpace(ConcernedGame.DBConfPath) == false)
                        DBCfgPath = ConcernedGame.DBConfPath;
                    //if not, use the default dosbox.conf file
                    else if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false && Amp.DBDefaultConfFilePath != "No configuration file (*.conf) found in AmpShell's directory.")
                        DBCfgPath = Amp.DBDefaultConfFilePath;
                }
                //The arguments for DOSBox begins with the game executable (.exe, .bat, or .com)
                if (string.IsNullOrWhiteSpace(ConcernedGame.DOSEXEPath) == false)
                {
                    if (!Setup)
                        Arguments = '"' + ConcernedGame.DOSEXEPath + '"';
                    else
                        Arguments = '"' + ConcernedGame.SetupEXEPath + '"';
                }
                //the game directory mounted as C (if the DOSEXEPath is specified, the DOSEXEPath parent directory will be mounted as C: by DOSBox
                //hence the "else if" instead of "if".
                else if (string.IsNullOrWhiteSpace(ConcernedGame.Directory) == false)
                    Arguments = " -c " + '"' + "mount c " + qt + ConcernedGame.Directory + qt + '"';
                //puting DBCfgPath and Arguments together
                if (string.IsNullOrWhiteSpace(DBCfgPath) == false)
                    Arguments = Arguments + " -conf " + '"' + DBCfgPath + '"';
                //Path for the default language file used for DOSBox and specified by the user in the Tools menu
                if (string.IsNullOrWhiteSpace(Amp.DBDefaultLangFilePath) == false && Amp.DBDefaultLangFilePath != "No language file (*.lng) found in AmpShell's directory.")
                    Arguments = Arguments + " -lang " + '"' + Amp.DBDefaultLangFilePath + '"';
                //Path for the game's CD image (.bin, .cue, or .iso) mounted as D:
                if (string.IsNullOrWhiteSpace(ConcernedGame.CDPath) == false)
                {
                    //put ' and _not_ " after imgmount (or else the path will be misunderstood by DOSBox). Paths with spaces will NOT work either way on GNU/Linux!
                    if (ConcernedGame.CDIsAnImage == true)
                    {
                        Arguments = Arguments + " -c " + '"' + "imgmount";
                        if (ConcernedGame.MountAsFloppy == true)
                            Arguments = Arguments + " a " + qt + ConcernedGame.CDPath + qt + " -t floppy" + '"';
                        else
                            Arguments = Arguments + " d " + qt + ConcernedGame.CDPath + qt + " -t iso" + '"';
                    }
                    else
                    {
                        if (ConcernedGame.UseIOCTL == true)
                            Arguments = Arguments + " -c " + '"' + "mount d " + qt + ConcernedGame.CDPath + qt + " -t cdrom -usecd 0 -ioctl" + '"';
                        else if (ConcernedGame.MountAsFloppy == true)
                            Arguments = Arguments + " -c " + '"' + "mount a " + qt + ConcernedGame.CDPath + qt + " -t floppy" + '"';
                        else
                            Arguments = Arguments + " -c " + '"' + "mount d " + qt + ConcernedGame.CDPath + qt;
                    }
                }
                //Additionnal user commands for the game
                if (string.IsNullOrWhiteSpace(ConcernedGame.AdditionalCommands) == false)
                    Arguments = Arguments + " " + ConcernedGame.AdditionalCommands;
                //corresponds to the Fullscreen checkbox in GameForm
                if (ConcernedGame.InFullScreen == true)
                    Arguments = Arguments + " -fullscreen";
                //corresponds to the "no console" checkbox in the GameForm
                if (ConcernedGame.NoConsole == true)
                    Arguments = Arguments + " -noconsole";
                //corresponds to the "quit on exit (only for .exe)" checkbox in the GameForm
                if (ConcernedGame.QuitOnExit == true)
                    Arguments = Arguments + " -exit";
                return Arguments;
            }
            else
            {
                MessageBox.Show(this, "DOSBox cannot be run (was it deleted ?) !", RunGame.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// EventHandler for when a game is double-clicked (activated), or activated by the Enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltview_ItemActivate(object sender, EventArgs e)
        {
            StartDOSBox(BuildArgs(false));
        }

        /// <summary>
        /// EventHandler for when a key is pressed while ltview has focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ltview_KeyDown(object sender, KeyEventArgs e)
        {
            //if it was the delete key
            if (e.KeyCode == Keys.Delete)
            {
                //search for the selected category
                foreach (Category ConcernedCategory in Amp.ListChildren)
                {
                    if (ConcernedCategory.Signature == tabControl.SelectedTab.Name)
                    {
                        //search for the selected game
                        foreach (Game ConcernedGame in ConcernedCategory.ListChildren)
                        {
                            //delete the game data
                            foreach (ListViewItem ConcernedItem in ltview.SelectedItems)
                            {
                                if (ConcernedGame.Signature == ConcernedItem.Name)
                                {
                                    if (Amp.GameDeletePrompt == true)
                                    {
                                        if (MessageBox.Show(this, "Do you really want to delete this game : " + ConcernedGame.Name + " ?", GameDeleteButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            ConcernedCategory.RemoveChild(ConcernedGame);
                                            //delete the corresponding ListViewItem
                                            ltview.Items.Remove(ConcernedItem);
                                        }
                                    }
                                    else
                                    {
                                        ConcernedCategory.RemoveChild(ConcernedGame);
                                        //delete the corresponding ListViewItem
                                        ltview.Items.Remove(ConcernedItem);
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
            if (!Amp.PortableMode)
                XMLSerializer.Serialize(UserDataPath, Amp, typeof(AmpShell));
            else
            {
                foreach (Category ConcernedCategory in Amp.ListChildren)
                {
                    foreach (Game ConcernedGame in ConcernedCategory.ListChildren)
                    {
                        ConcernedGame.DOSEXEPath = ConcernedGame.DOSEXEPath.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.DBConfPath = ConcernedGame.DBConfPath.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.AdditionalCommands = ConcernedGame.AdditionalCommands.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.Directory = ConcernedGame.Directory.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.CDPath = ConcernedGame.CDPath.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.SetupEXEPath = ConcernedGame.SetupEXEPath.Replace(Application.StartupPath, "AppPath");
                        ConcernedGame.Icon = ConcernedGame.Icon.Replace(Application.StartupPath, "AppPath");
                    }
                }
                Amp.DBDefaultConfFilePath = Amp.DBDefaultConfFilePath.Replace(Application.StartupPath, "AppPath");
                Amp.DBDefaultLangFilePath = Amp.DBDefaultLangFilePath.Replace(Application.StartupPath, "AppPath");
                Amp.DBPath = Amp.DBPath.Replace(Application.StartupPath, "AppPath");
                Amp.ConfigEditorPath = Amp.ConfigEditorPath.Replace(Application.StartupPath, "AppPath");
                Amp.ConfigEditorAdditionalParameters = Amp.ConfigEditorAdditionalParameters.Replace(Application.StartupPath, "AppPath");
                XMLSerializer.Serialize(Application.StartupPath + "/AmpShell.xml", Amp, typeof(AmpShell));
            }
        }

        /// <summary>
        /// EventHandler for the ? -> About button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) { AboutBox AbtBox = new AboutBox(); AbtBox.ShowDialog(this); }

        /// <summary>
        /// EventHandler for when the delete button game is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameDeleteButton_Click(object sender, EventArgs e)
        { KeyEventArgs k = new KeyEventArgs(Keys.Delete); Ltview_KeyDown(sender, k); }

        /// <summary>
        /// EventHandler for when the Category delete button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryDeleteButton_Click(object sender, EventArgs e)
        {
            Category ConcernedCategory = GetSelectedCat();
            if (Amp.CategoryDeletePrompt != true || MessageBox.Show(this, "Do you really want to delete " + "'" + tabControl.SelectedTab.Text + "'" + " and all the games inside it ?", DeleteCategory.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //prompt the user if he really wants to delete it.
                //remove the data (the game will be deleted also. List<AmpShell> is provided by RootAmpShell.cs.
                //All the other classes derive from AmpShell, so it makes a tree).
                Amp.RemoveChild(ConcernedCategory);
                //remove the corresponding displayed TabPage
                tabControl.TabPages.Remove(tabControl.SelectedTab);
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
            if (string.IsNullOrWhiteSpace(Amp.DBPath) == false)
            {
                //check first for the lang file
                string Lang = String.Empty;
                if (string.IsNullOrWhiteSpace(Amp.DBDefaultLangFilePath) == false)
                    Lang = " -lang " + '"' + Amp.DBDefaultLangFilePath + '"';
                //then for the conf file
                if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false)
                    StartDOSBox(" -conf " + '"' + Amp.DBDefaultConfFilePath + '"' + Lang);
                else
                    StartDOSBox(Lang);
            }
        }

        private void StartDOSBox(string args)
        {
            var psi = new System.Diagnostics.ProcessStartInfo(Amp.DBPath);
            if(string.IsNullOrWhiteSpace(args) == false)
            {
                psi.Arguments = args;
            }
            var proc = System.Diagnostics.Process.Start(Amp.DBPath, args);
            if(proc != null)
            {
                proc.EnableRaisingEvents = true;
                this.BeginInvoke(new Action(() =>
                {
                    this.WindowState = FormWindowState.Minimized;
                }));
                proc.Exited += OnDOSBoxExit;
            }
        }

        private void OnDOSBoxExit(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.WindowState = FormWindowState.Normal;
            }));
        }

        /// <summary>
        /// EventHandler for when AmpShell is shown (happens after AmpShell_Load)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_Shown(object sender, EventArgs e)
        {
            AmpShellShown = true;
            //select the first TabPage of tabcontrol 
            //(if it exists, because AmpShell_Shown is called _automatically_,
            //whether the user has created a category or none!)
            if (tabControl.HasChildren != false)
            {
                //select the first TabPage
                tabControl.SelectedTab = tabControl.TabPages[0];
                //make the Category edit & delete buttons Enabled
                CategoryEditButton.Enabled = true;
                editSelectedcategoryToolStripMenuItem.Enabled = true;
                EditCategory.Enabled = true;
                DeleteCategory.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                deleteSelectedCategoryToolStripMenuItem.Enabled = true;
                //reference the selected TabPage's ListView into ltview (with a cast)
                ltview = (ListView)tabControl.SelectedTab.Controls["GamesListView"];
            }
            //if tabcontrol has no children, then it has no TabPages (categories)
            //so we prompt the user for the title of the first category.
            else
                CategoryAddButton_Click(sender, e);
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
            GameForm NewGameForm = new GameForm(Amp);
            String NewGameSignature = String.Empty;
            do
            {
                Random rand = new Random();
                NewGameSignature = rand.Next(1048576).ToString();
            }
            while (IsItUnique(NewGameSignature, Amp) == false);
            NewGameForm.GameInstance.Signature = NewGameSignature;
            if (NewGameForm.ShowDialog(this) == DialogResult.OK)
            {
                Category ConcernedCategory = GetSelectedCat();
                ConcernedCategory.AddChild(NewGameForm.GameInstance);
                if (string.IsNullOrWhiteSpace(NewGameForm.GameInstance.Icon) == false)
                {
                    GamesLargeImageList.Images.Add(NewGameForm.GameInstance.Signature, Image.FromFile(NewGameForm.GameInstance.Icon).GetThumbnailImage(Amp.LargeViewModeSize, Amp.LargeViewModeSize, null, IntPtr.Zero));
                    GamesMediumImageList.Images.Add(NewGameForm.GameInstance.Signature, Image.FromFile(NewGameForm.GameInstance.Icon).GetThumbnailImage(32, 32, null, IntPtr.Zero));
                    GamesSmallImageList.Images.Add(NewGameForm.GameInstance.Signature, Image.FromFile(NewGameForm.GameInstance.Icon).GetThumbnailImage(16, 16, null, IntPtr.Zero));
                }
                ltview = (ListView)tabControl.SelectedTab.Controls["GamesListView"];
                //add the ListViewItem corresponding to the new game.
                ListViewItem gameforlt = new ListViewItem(NewGameForm.GameInstance.Name)
                {
                    Name = NewGameForm.GameInstance.Signature
                };
                ListViewItem.ListViewSubItem GameDOSEXEPathLVSubItem = new ListViewItem.ListViewSubItem
                {
                    Text = NewGameForm.GameInstance.DOSEXEPath
                };
                gameforlt.SubItems.Add(GameDOSEXEPathLVSubItem);
                ListViewItem.ListViewSubItem GameCMountLVSubItem = new ListViewItem.ListViewSubItem
                {
                    Text = NewGameForm.GameInstance.Directory
                };
                gameforlt.SubItems.Add(GameCMountLVSubItem);
                if (ltview.View != View.Tile)
                {
                    ListViewItem.ListViewSubItem GameSetupLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.SetupEXEPath
                    };
                    gameforlt.SubItems.Add(GameSetupLVSubItem);
                    ListViewItem.ListViewSubItem GameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem();
                    if (NewGameForm.GameInstance.NoConfig == true)
                        GameCustomConfigurationLVSubItem.Text = "None at all";
                    else
                        GameCustomConfigurationLVSubItem.Text = NewGameForm.GameInstance.DBConfPath;
                    gameforlt.SubItems.Add(GameCustomConfigurationLVSubItem);
                    ListViewItem.ListViewSubItem GameDMountLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.CDPath
                    };
                    gameforlt.SubItems.Add(GameDMountLVSubItem);
                    ListViewItem.ListViewSubItem GameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                    if (NewGameForm.GameInstance.UseIOCTL == true)
                        GameMountingOptionsLVSubItem.Text = "Use IOCTL";
                    else if (NewGameForm.GameInstance.MountAsFloppy == true)
                        GameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                    else
                        GameMountingOptionsLVSubItem.Text = "None";
                    gameforlt.SubItems.Add(GameMountingOptionsLVSubItem);
                    ListViewItem.ListViewSubItem GameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.AdditionalCommands
                    };
                    gameforlt.SubItems.Add(GameAdditionnalCommandsLVSubItem);
                    ListViewItem.ListViewSubItem GameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.NoConsole.ToString()
                    };
                    gameforlt.SubItems.Add(GameNoConsoleLVSubItem);
                    ListViewItem.ListViewSubItem GameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.InFullScreen.ToString()
                    };
                    gameforlt.SubItems.Add(GameFullscreenLVSubItem);
                    ListViewItem.ListViewSubItem GameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                    {
                        Text = NewGameForm.GameInstance.QuitOnExit.ToString()
                    };
                    gameforlt.SubItems.Add(GameQuitOnExitLVSubItem);
                }
                ltview.Items.Add(gameforlt);
                if (!Amp.OnlyNames)
                {
                    if (string.IsNullOrWhiteSpace(NewGameForm.GameInstance.Icon) == false)
                        gameforlt.ImageKey = NewGameForm.GameInstance.Signature;
                    else
                        gameforlt.ImageKey = "DefaultIcon";
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
            if (Amp.RememberWindowSize == true)
            {
                Amp.Height = Height;
                Amp.Width = Width;
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
            Category ConcernedCat = GetSelectedCat();
            CategoryForm CatEdit = new CategoryForm(ConcernedCat)
            {
                Text = "Editing " + ConcernedCat.Title + "..."
            };
            if (CatEdit.ShowDialog(this) == DialogResult.OK)
            {
                //modify the displayed category (TabPage) text
                tabControl.SelectedTab.Text = ConcernedCat.Title;
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
            String Arguments = BuildArgs(true);
            //start DOSBox (Amp.DBPath) with the arguments we've just build up.
            StartDOSBox(Arguments);
        }

        /// <summary>
        /// EventHandler for when the window is (un)maximized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_Resize(object sender, EventArgs e)
        {
            if (AmpShellShown == true)
            {
                if (WindowState == FormWindowState.Maximized)
                    Amp.Fullscreen = true;
                else
                    Amp.Fullscreen = false;
            }
        }

        /// <summary>
        /// Used when a new Category is created : it's signature must be unique
        /// so AmpShell can recognize it instantly
        /// </summary>
        /// <param name="SignatureToTest"></param>
        /// <param name="Amp"></param>
        /// <returns></returns>
        private bool IsItUnique(String SignatureToTest, Window Amp)
        {
            foreach (Category OtherCat in Amp.ListChildren)
            {
                if (OtherCat.Signature != SignatureToTest)
                {
                    if (OtherCat.ListChildren.Length != 0)
                    {
                        foreach (Game OtherGame in OtherCat.ListChildren)
                        {
                            if (OtherGame.Signature == SignatureToTest)
                                return false;
                        }
                    }
                }
                else
                    return false;
            }
            return true;
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main_Prefs Prefs = new Main_Prefs(Amp);
            if (Prefs.ShowDialog(this) == DialogResult.OK)
            {
                GamesLargeImageList.ImageSize = new Size(Amp.LargeViewModeSize, Amp.LargeViewModeSize);
                if (Amp.PortableMode)
                    XMLSerializer.Serialize(Application.StartupPath + "/AmpShell.xml", Amp, typeof(AmpShell));
                menuStrip.Visible = Prefs.AmpInstance.MenuBarVisible;
                MenuBar_AmpCMS.Checked = Prefs.AmpInstance.MenuBarVisible;
                toolStrip.Visible = Prefs.AmpInstance.ToolBarVisible;
                ToolBar_AmpCMS.Checked = Prefs.AmpInstance.ToolBarVisible;
                statusStrip.Visible = Prefs.AmpInstance.StatusBarVisible;
                StatusBar_AmpCMS.Checked = Prefs.AmpInstance.StatusBarVisible;
                Amp.ListChildren = Prefs.AmpInstance.ListChildren;
                Amp.X = Location.X;
                Amp.Y = Location.Y;
                DisplayUserData(Amp, Amp.OnlyNames);
            }
            UpdateButtonsState();
        }

        private void RunConfigurationEditorButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Amp.ConfigEditorPath) == false)
            {
                if (File.Exists(Amp.ConfigEditorPath))
                    System.Diagnostics.Process.Start(Amp.ConfigEditorPath);
                else
                    MessageBox.Show("The configuration editor cannot be run (was it deleted ?). Please set it in the preferences.", RunConfigurationEditorButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void GameEditConfigurationButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Amp.ConfigEditorPath) == false)
            {
                Game ConcernedGame = GetSelectedGame();
                System.Diagnostics.Process.Start(Amp.ConfigEditorPath, ConcernedGame.DBConfPath + " " + Amp.ConfigEditorAdditionalParameters);
            }
        }

        private void LargeIconViewButton_Click(object sender, EventArgs e)
        {
            ltview.View = View.LargeIcon;
            ltview.LargeImageList = GamesLargeImageList;
            Category Cat = GetSelectedCat();
            Cat.ViewMode = ltview.View;
        }

        private void SmallIconViewButton_Click(object sender, EventArgs e)
        {
            ltview.View = View.SmallIcon;
            Category Cat = GetSelectedCat();
            Cat.ViewMode = ltview.View;
        }

        private void TileViewButton_Click(object sender, EventArgs e)
        {
            ltview.View = View.Tile;
            ltview.LargeImageList = GamesMediumImageList;
            Category Cat = GetSelectedCat();
            Cat.ViewMode = ltview.View;
            foreach (ListViewItem ltviewitem in ltview.Items)
            {
                while (ltviewitem.SubItems.Count > 3)
                    ltviewitem.SubItems.RemoveAt(ltviewitem.SubItems.Count - 1);
            }
        }

        private void ListViewButton_Click(object sender, EventArgs e)
        {
            ltview.View = View.List;
            ltview.Columns[0].Width = ltview.Width;
            Category Cat = GetSelectedCat();
            Cat.ViewMode = ltview.View;
        }

        private void DetailsViewButton_Click(object sender, EventArgs e)
        {
            if (ltview.Columns.Count > 0)
            {
                ltview.View = View.Details;
                Category Cat = GetSelectedCat();
                Cat.ViewMode = ltview.View;
                ltview.Columns["NameColumn"].Width = Cat.NameColumnWidth;
                ltview.Columns["ExecutableColumn"].Width = Cat.ExecutableColumnWidth;
                ltview.Columns["CMountColumn"].Width = Cat.CMountColumnWidth;
                ltview.Columns["SetupExecutableColumn"].Width = Cat.SetupExecutableColumnWidth;
                ltview.Columns["CustomConfigurationColumn"].Width = Cat.CustomConfigurationColumnWidth;
                ltview.Columns["DMountColumn"].Width = Cat.DMountColumnWidth;
                ltview.Columns["MountingOptionsColumn"].Width = Cat.MountingOptionsColumnWidth;
                ltview.Columns["AdditionnalCommandsColumn"].Width = Cat.AdditionnalCommandsColumnWidth;
                ltview.Columns["NoConsoleColumn"].Width = Cat.NoConsoleColumnWidth;
                ltview.Columns["FullscreenColumn"].Width = Cat.FullscreenColumnWidth;
                ltview.Columns["QuitOnExitColumn"].Width = Cat.QuitOnExitColumnWidth;
                foreach (ListViewItem ltviewitem in ltview.Items)
                {
                    if (ltview.Name == Cat.Signature)
                    {
                        foreach (Game ConcernedGame in Amp.ListChildren)
                        {
                            if (ltviewitem.Name == ConcernedGame.Signature)
                            {
                                if (ltviewitem.SubItems.Count == 2)
                                {
                                    ListViewItem.ListViewSubItem GameSetupLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.SetupEXEPath
                                    };
                                    ltviewitem.SubItems.Add(GameSetupLVSubItem);
                                    ListViewItem.ListViewSubItem GameCustomConfigurationLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Name = "GameCustomConfiguration"
                                    };
                                    if (ConcernedGame.NoConfig == true)
                                        GameCustomConfigurationLVSubItem.Text = "None at all";
                                    else
                                        GameCustomConfigurationLVSubItem.Text = ConcernedGame.DBConfPath;
                                    ltviewitem.SubItems.Add(GameCustomConfigurationLVSubItem);
                                    ListViewItem.ListViewSubItem GameDMountLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.CDPath
                                    };
                                    ltviewitem.SubItems.Add(GameDMountLVSubItem);
                                    ListViewItem.ListViewSubItem GameMountingOptionsLVSubItem = new ListViewItem.ListViewSubItem();
                                    if (ConcernedGame.UseIOCTL == true)
                                        GameMountingOptionsLVSubItem.Text = "Use IOCTL";
                                    else if (ConcernedGame.MountAsFloppy == true)
                                        GameMountingOptionsLVSubItem.Text = "Mount as a floppy disk (A:)";
                                    else
                                        GameMountingOptionsLVSubItem.Text = "None";
                                    ltviewitem.SubItems.Add(GameMountingOptionsLVSubItem);
                                    ListViewItem.ListViewSubItem GameAdditionnalCommandsLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.AdditionalCommands
                                    };
                                    ltviewitem.SubItems.Add(GameAdditionnalCommandsLVSubItem);
                                    ListViewItem.ListViewSubItem GameNoConsoleLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.NoConsole.ToString()
                                    };
                                    ltviewitem.SubItems.Add(GameNoConsoleLVSubItem);
                                    ListViewItem.ListViewSubItem GameFullscreenLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.InFullScreen.ToString()
                                    };
                                    ltviewitem.SubItems.Add(GameFullscreenLVSubItem);
                                    ListViewItem.ListViewSubItem GameQuitOnExitLVSubItem = new ListViewItem.ListViewSubItem
                                    {
                                        Text = ConcernedGame.QuitOnExit.ToString()
                                    };
                                    ltviewitem.SubItems.Add(GameQuitOnExitLVSubItem);
                                }
                                else
                                    break;
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
                MenuBar_AmpCMS.Checked = false;
                menuStrip.Visible = false;
            }
            else
            {
                MenuBar_AmpCMS.Checked = true;
                menuStrip.Visible = true;
            }
            Amp.MenuBarVisible = menuStrip.Visible;
        }

        private void ToolBar_AmpCMS_Click(object sender, EventArgs e)
        {
            if (toolStrip.Visible == true)
            {
                ToolBar_AmpCMS.Checked = false;
                toolStrip.Visible = false;
            }
            else
            {
                ToolBar_AmpCMS.Checked = true;
                toolStrip.Visible = true;
            }
            Amp.ToolBarVisible = toolStrip.Visible;
        }

        private void StatusBar_AmpCMS_Click(object sender, EventArgs e)
        {
            if (statusStrip.Visible == true)
            {
                StatusBar_AmpCMS.Checked = false;
                statusStrip.Visible = false;
            }
            else
            {
                StatusBar_AmpCMS.Checked = true;
                statusStrip.Visible = true;
            }
            Amp.StatusBarVisible = statusStrip.Visible;
        }

        /// <summary>
        /// EventHandler for when the window is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AmpShell_LocationChanged(object sender, EventArgs e)
        {
            if (Amp.RememberWindowPosition == true && this.WindowState != FormWindowState.Minimized)
            {
                Amp.X = Location.X;
                Amp.Y = Location.Y;
            }
        }

        private void Ltview_ColumnWidthChanged(object sender, EventArgs e)
        {
            foreach (Category ConcernedCategory in Amp.ListChildren)
            {
                if (ConcernedCategory.ViewMode == View.Details)
                {
                    if (tabControl.SelectedTab.Name == ConcernedCategory.Signature && ltview.Columns.Count > 0)
                    {
                        ConcernedCategory.NameColumnWidth = ltview.Columns["NameColumn"].Width;
                        ConcernedCategory.ExecutableColumnWidth = ltview.Columns["ExecutableColumn"].Width;
                        ConcernedCategory.CMountColumnWidth = ltview.Columns["CMountColumn"].Width;
                        ConcernedCategory.SetupExecutableColumnWidth = ltview.Columns["SetupExecutableColumn"].Width;
                        ConcernedCategory.CustomConfigurationColumnWidth = ltview.Columns["CustomConfigurationColumn"].Width;
                        ConcernedCategory.DMountColumnWidth = ltview.Columns["DMountColumn"].Width;
                        ConcernedCategory.MountingOptionsColumnWidth = ltview.Columns["MountingOptionsColumn"].Width;
                        ConcernedCategory.AdditionnalCommandsColumnWidth = ltview.Columns["AdditionnalCommandsColumn"].Width;
                        ConcernedCategory.NoConsoleColumnWidth = ltview.Columns["NoConsoleColumn"].Width;
                        ConcernedCategory.FullscreenColumnWidth = ltview.Columns["FullscreenColumn"].Width;
                        ConcernedCategory.QuitOnExitColumnWidth = ltview.Columns["QuitOnExitColumn"].Width;
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
            tileToolStripMenuItem.Enabled = false;
            DetailsViewButton.Enabled = false;
            detailsToolStripMenuItem.Enabled = false;
            ListViewButton.Enabled = false;
            listToolStripMenuItem.Enabled = false;
            NewGameToolStripMenuItem.Enabled = false;
            AddGame.Enabled = false;
            GameAddButton.Enabled = false;
            RunGameButton.Enabled = false;
            RunGame.Enabled = false;
            RunGameToolStripMenuItem.Enabled = false;
            GameEditButton.Enabled = false;
            editSelectedgameToolStripMenuItem.Enabled = false;
            EditGame.Enabled = false;
            RunGameSetupButton.Enabled = false;
            RunGameSetupToolStripMenuItem.Enabled = false;
            RunGameSetup.Enabled = false;
            CategoryEditButton.Enabled = false;
            editSelectedcategoryToolStripMenuItem.Enabled = false;
            EditCategory.Enabled = false;
            CategoryDeleteButton.Enabled = false;
            deleteSelectedCategoryToolStripMenuItem.Enabled = false;
            DeleteCategory.Enabled = false;
            EditGameConfiguration.Enabled = false;
            GameEditConfigurationButton.Enabled = false;
            editConfigToolStripMenuItem.Enabled = false;
            RunConfigurationEditorButton.Enabled = false;
            runConfigurationEditorToolStripMenuItem.Enabled = false;
            RunDOSBoxButton.Enabled = false;
            RunDOSBoxToolStripMenuItem.Enabled = false;
            EditDefaultConfigurationToolStripMenuItem.Enabled = false;
            EditDefaultConfigurationButton.Enabled = false;
            MakeGameConfiguration.Enabled = false;
            MakeConfigButton.Enabled = false;
            MakeConfigurationFileToolStripMenuItem.Enabled = false;
            if (tabControl.HasChildren != false)
            {
                LargeIconViewButton.Enabled = true;
                LargeIconToolStripMenuItem.Enabled = true;
                SmallIconToolStripMenuItem.Enabled = true;
                SmallIconViewButton.Enabled = true;
                TilesViewButton.Enabled = true;
                tileToolStripMenuItem.Enabled = true;
                DetailsViewButton.Enabled = true;
                detailsToolStripMenuItem.Enabled = true;
                ListViewButton.Enabled = true;
                listToolStripMenuItem.Enabled = true;
                NewGameToolStripMenuItem.Enabled = true;
                AddGame.Enabled = true;
                GameAddButton.Enabled = true;
                if (string.IsNullOrWhiteSpace(Amp.DBPath) == false)
                {
                    RunGameButton.Enabled = true;
                    RunGame.Enabled = true;
                    RunGameToolStripMenuItem.Enabled = true;
                    RunGameSetupButton.Enabled = true;
                    RunGameSetupToolStripMenuItem.Enabled = true;
                    RunGameSetup.Enabled = true;
                    RunDOSBoxButton.Enabled = true;
                    RunDOSBoxToolStripMenuItem.Enabled = true;
                }
                CategoryEditButton.Enabled = true;
                EditCategory.Enabled = true;
                editSelectedcategoryToolStripMenuItem.Enabled = true;
                CategoryDeleteButton.Enabled = true;
                deleteSelectedCategoryToolStripMenuItem.Enabled = true;
                DeleteCategory.Enabled = true;
                GameEditButton.Enabled = true;
                if (string.IsNullOrWhiteSpace(Amp.ConfigEditorPath) == false)
                {
                    RunConfigurationEditorButton.Enabled = true;
                    runConfigurationEditorToolStripMenuItem.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false)
                {
                    EditDefaultConfigurationToolStripMenuItem.Enabled = true;
                    EditDefaultConfigurationButton.Enabled = true;
                    MakeGameConfiguration.Enabled = true;
                    MakeConfigButton.Enabled = true;
                    MakeConfigurationFileToolStripMenuItem.Enabled = true;
                }
                Ltview_ItemSelectionChanged(this, EventArgs.Empty);
            }
        }

        private void DisplayHelpMessage(String Tooltiptext)
        {
            AdditionnalCommandsLabel.Text = String.Empty;
            ExecutablePathLabel.Text = String.Empty;
            CMountLabel.Text = String.Empty;
            DMountLabel.Text = String.Empty;
            CustomConfigurationLabel.Text = String.Empty;
            QuitOnExitLabel.Text = String.Empty;
            FullscreenLabel.Text = String.Empty;
            NoConsoleLabel.Text = String.Empty;
            SetupPathLabel.Text = String.Empty;
            ExecutablePathLabel.Text = Tooltiptext;
        }

        private void EditDefaultConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false && File.Exists(Amp.DBDefaultConfFilePath) && string.IsNullOrWhiteSpace(Amp.ConfigEditorPath) == false && Amp.ConfigEditorPath != "No text editor (Notepad in Windows' directory, or TextEditor.exe in AmpShell's directory) found." && File.Exists(Amp.ConfigEditorPath))
                System.Diagnostics.Process.Start(Amp.ConfigEditorPath, Amp.DBDefaultConfFilePath);
            else
                MessageBox.Show("Default configuration or configuration editor missing. Please set them in the preferences.");
        }

        private void MakeConfigButton_Click(object sender, EventArgs e)
        {
            Category ConcernedCategory = GetSelectedCat();
            foreach (ListViewItem LtViewItem in ltview.SelectedItems)
            {
                foreach (Game ConcernedGame in ConcernedCategory.ListChildren)
                {
                    if (ConcernedGame.Signature == LtViewItem.Name &&
                        string.IsNullOrWhiteSpace(Amp.DBDefaultConfFilePath) == false)
                    {
                        if ((!File.Exists(ConcernedGame.Directory + "/" + Path.GetFileName(Amp.DBDefaultConfFilePath))) || (MessageBox.Show(this, "'" + ConcernedGame.Directory + "/" + Path.GetFileName(Amp.DBDefaultConfFilePath) + "'" + "already exists, do you want to overwrite it ?", MakeConfigButton.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            File.Copy(Amp.DBDefaultConfFilePath, ConcernedGame.Directory + "/" + Path.GetFileName(Amp.DBDefaultConfFilePath), true);
                            ConcernedGame.DBConfPath = ConcernedGame.Directory + "/" + Path.GetFileName(Amp.DBDefaultConfFilePath);
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
            DisplayHelpMessage(preferencesToolStripMenuItem.ToolTipText);
        }

        private void AboutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(AboutToolStripMenuItem.ToolTipText);
        }

        private void QuitterToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            DisplayHelpMessage(quitterToolStripMenuItem.ToolTipText);
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