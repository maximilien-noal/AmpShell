using Prism.Commands;
using System.Windows.Input;

namespace AmpShell.ViewModels
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.Games;
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;
    using Prism.Commands;
    using System;
    using System.Diagnostics;

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
            NewCategory = new DelegateCommand(NewCategoryMethod, () => SelectedCategory != null);
            UseLargeIcons = new DelegateCommand(() => { ChangeViewMode(View.LargeIcon); }, () => SelectedCategory != null);
            UseSmallIcons = new DelegateCommand(() => { ChangeViewMode(View.SmallIcon); }, () => SelectedCategory != null);
            UseTiles = new DelegateCommand(() => { ChangeViewMode(View.Tile); }, () => SelectedCategory != null);
            UseList = new DelegateCommand(() => { ChangeViewMode(View.List); }, () => SelectedCategory != null);
            UseDetails = new DelegateCommand(() => { ChangeViewMode(View.Details); }, () => SelectedCategory != null);
        }

        public DelegateCommand NewCategory { get; }

        public DelegateCommand RunDOSBox { get; }

        public Category? SelectedCategory { get => _selectedCategory; set { Set(ref _selectedCategory, value); } }

        public Game? SelectedGame { get => _selectedGame; set { Set(ref _selectedGame, value); } }

        public DelegateCommand UseDetails { get; }

        public DelegateCommand UseLargeIcons { get; }

        public DelegateCommand UseList { get; }

        public Preferences UserData { get; private set; }

        public DelegateCommand UseSmallIcons { get; }

        public DelegateCommand UseTiles { get; }

        public Process? RunSelectedGame() => SelectedGame?.Run(UserData);

        public void SaveUserData() => _dal.SaveUserData();

        private void ChangeViewMode(View viewMode)
        {
            if (SelectedCategory != null)
            {
                SelectedCategory.ViewMode = viewMode;
            }
        }

        private void NewCategoryMethod()
        {
        }
    }
}