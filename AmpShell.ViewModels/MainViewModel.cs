namespace AmpShell.ViewModels
{
    using AmpShell.Core.Config;
    using AmpShell.Core.DAL;
    using AmpShell.Core.Games;
    using AmpShell.Core.Interfaces;
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;
    using Prism.Commands;
    using System.Diagnostics;

    public class MainViewModel : PropertyChangedNotifier
    {
        private readonly UserDataAccessor _dal = new UserDataAccessor();

        private readonly IWindow? _mainWindow;

        private Category? _selectedCategory = null;

        private Game? _selectedGame = null;

        /// <summary> For the WPF Designer </summary>
        public MainViewModel()
        {
        }

        public MainViewModel(IWindow mainWindow)
        {
            _mainWindow = mainWindow;
            UserData = _dal.GetUserData();
            RunSelectedGame = new DelegateCommand(() => RunGame(), () => SelectedGame != null && SelectedGame.CanRun(UserData));
            RunGameSetup = new DelegateCommand(() => RunGame(true), () => SelectedGame != null && SelectedGame.CanRunSetup(UserData));
            RunDOSBox = new DelegateCommand(() => GameProcessController.RunOnlyDOSBox(UserData), () => GameProcessController.CanRunMainDOSBoxVariant(UserData));
            NewCategory = new DelegateCommand(NewCategoryMethod, () => IsSelectedCategoryNotNull());
            UseLargeIcons = new DelegateCommand(() => { ChangeViewMode(View.LargeIcon); }, () => IsSelectedCategoryNotNull());
            UseSmallIcons = new DelegateCommand(() => { ChangeViewMode(View.SmallIcon); }, () => IsSelectedCategoryNotNull());
            UseTiles = new DelegateCommand(() => { ChangeViewMode(View.Tile); }, () => IsSelectedCategoryNotNull());
            UseList = new DelegateCommand(() => { ChangeViewMode(View.List); }, () => IsSelectedCategoryNotNull());
            UseDetails = new DelegateCommand(() => { ChangeViewMode(View.Details); }, () => IsSelectedCategoryNotNull());
            EditDefaultConfigFile = new DelegateCommand(() => ConfigEditorRunner.EditDefaultConfigFile(_dal, UserData), () => ConfigEditorRunner.CanRunConfigEditor(UserData));
            RunConfigEditor = new DelegateCommand(() => ConfigEditorRunner.RunConfigEditor(_dal, UserData), () => ConfigEditorRunner.CanOpenDefaultConfigFile(UserData));
            NewGame = new DelegateCommand(() => { }, () => { return IsSelectedCategoryNotNull(); });
            OpenGameFolder = new DelegateCommand(() => { if (SelectedGame != null) { SelectedGame.OpenGameFolder(); } }, () => SelectedGame != null && SelectedGame.HasAGameFolder());
            EditSelectedGameConfigFile = new DelegateCommand(() => { if (SelectedGame != null) { _ = SelectedGame.EditConfigFile(_dal); } }, () => SelectedGame != null && SelectedGame.HasACustomConfigFile());
            EditSelectedGame = new DelegateCommand(() => { });
            MakeConfigFileForSelectedGame = new DelegateCommand(() => { });
            EditSelectedCategory = new DelegateCommand(() => { });
            DeleteSelectedGame = new DelegateCommand(() => { });
            DeleteSelectedCategory = new DelegateCommand(() => { });
            ImportAmpShellData = new DelegateCommand(() => { });
            OrderByName = new DelegateCommand(() => { });
            OrderByNameReversed = new DelegateCommand(() => { });
            OrderByReleaseDate = new DelegateCommand(() => { });
        }

        public DelegateCommand? DeleteSelectedCategory { get; }

        public DelegateCommand? DeleteSelectedGame { get; }

        public DelegateCommand? EditDefaultConfigFile { get; }

        public DelegateCommand? EditSelectedCategory { get; }

        public DelegateCommand? EditSelectedGame { get; }

        public DelegateCommand? EditSelectedGameConfigFile { get; }

        public DelegateCommand? ImportAmpShellData { get; }

        public DelegateCommand? MakeConfigFileForSelectedGame { get; }

        public DelegateCommand? NewCategory { get; }

        public DelegateCommand? NewGame { get; }

        public DelegateCommand? OpenGameFolder { get; }

        public DelegateCommand? OrderByName { get; }

        public DelegateCommand? OrderByNameReversed { get; }

        public DelegateCommand? OrderByReleaseDate { get; }

        public DelegateCommand? RunConfigEditor { get; }

        public DelegateCommand? RunDOSBox { get; }

        public DelegateCommand? RunGameSetup { get; }

        public DelegateCommand? RunSelectedGame { get; }

        public Category? SelectedCategory { get => _selectedCategory; set { Set(ref _selectedCategory, value); } }

        public Game? SelectedGame { get => _selectedGame; set { Set(ref _selectedGame, value); } }

        public DelegateCommand? UseDetails { get; }

        public DelegateCommand? UseLargeIcons { get; }

        public DelegateCommand? UseList { get; }

        public Preferences? UserData { get; private set; }

        public DelegateCommand? UseSmallIcons { get; }

        public DelegateCommand? UseTiles { get; }

        public void SaveUserData() => _dal.SaveUserData();

        private void ChangeViewMode(View viewMode)
        {
            if (SelectedCategory != null)
            {
                SelectedCategory.ViewMode = viewMode;
            }
        }

        private bool IsSelectedCategoryNotNull() => SelectedCategory != null;

        private void NewCategoryMethod()
        {
        }

        private void RunGame(bool runSetup = false)
        {
            if (SelectedGame is null)
            {
                return;
            }
            var process = runSetup ? SelectedGame.RunSetup(UserData) : SelectedGame.Run(UserData);
            if (process != null)
            {
                _mainWindow?.Minimize();
                process.Exited += (s, e) => _mainWindow?.Restore();
            }
        }
    }
}