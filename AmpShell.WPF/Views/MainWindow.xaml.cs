namespace AmpShell.WPF.Views
{
    using AmpShell.ViewModels;
    using Prism.Commands;
    using System.ComponentModel;
    using System.Windows;

    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ShowAboutWindow = new DelegateCommand(() => new AboutWindow(this).ShowDialog());
            ShowPreferences = new DelegateCommand(() => new PreferencesWindow(this).ShowDialog());
            Quit = new DelegateCommand(() => Application.Current.MainWindow.Close());
            ViewModel = new MainViewModel();
            RunSelectedGame = new DelegateCommand(() => RunGame(), () => ViewModel.SelectedGame != null);
            RunGameSetup = new DelegateCommand(() => RunGame(true), () => ViewModel.SelectedGame != null && string.IsNullOrWhiteSpace(ViewModel.SelectedGame.SetupEXEPath) == false);
            InitializeComponent();
        }

        public DelegateCommand Quit { get; }

        public DelegateCommand RunGameSetup { get; }

        public DelegateCommand RunSelectedGame { get; }

        public DelegateCommand ShowAboutWindow { get; }

        public DelegateCommand ShowPreferences { get; }

        public MainViewModel ViewModel { get; }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ViewModel.SaveUserData();
        }

        private void RunGame(bool runSetup = false)
        {
            var process = ViewModel.RunSelectedGame();
            if (process != null)
            {
                SetCurrentValue(WindowStateProperty, WindowState.Minimized);
                process.Exited += (s, e) => Dispatcher.Invoke(() => { if (WindowState == WindowState.Minimized) { SystemCommands.RestoreWindow(this); } });
            }
        }
    }
}