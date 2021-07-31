namespace AmpShell.ViewModels
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.Games;
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;
    using Prism.Commands;
    using System;

    public class MainViewModel : PropertyChangedNotifier
    {
        private readonly UserDataAccessor _dal = new UserDataAccessor();

        private Category? _selectedCategory = null;

        private Game? _selectedGame = null;

        public MainViewModel()
        {
            UserData = _dal.GetUserData();
            RunDOSBox = new DelegateCommand(
                () => GameProcessController.RunOnlyDOSBox(UserData),
                () => string.IsNullOrWhiteSpace(UserData.DBPath) == false);
            RunSelectedGame = new DelegateCommand(RunSelectedGameMethod, () => SelectedGame != null);
        }

        public DelegateCommand RunDOSBox { get; }

        public DelegateCommand RunSelectedGame { get; }

        public Category? SelectedCategory { get => _selectedCategory; set { Set(ref _selectedCategory, value); } }

        public Game? SelectedGame { get => _selectedGame; set { Set(ref _selectedGame, value); } }

        public Preferences UserData { get; private set; }

        private void RunSelectedGameMethod() => SelectedGame?.Run(_dal.GetUserData());
    }
}