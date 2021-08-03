namespace AmpShell.WPF.Views
{
    using AmpShell.Core.Interfaces;
    using AmpShell.ViewModels;
    using Prism.Commands;
    using System.ComponentModel;
    using System.Windows;

    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window, IWindow
    {
        public MainWindow()
        {
            ShowAboutWindow = new DelegateCommand(() => new AboutWindow(this).ShowDialog());
            ShowPreferences = new DelegateCommand(() => new PreferencesWindow(this).ShowDialog());
            Quit = new DelegateCommand(() => Application.Current.MainWindow.Close());
            ViewModel = new MainViewModel(this);
            InitializeComponent();
        }

        public DelegateCommand Quit { get; }

        public DelegateCommand ShowAboutWindow { get; }

        public DelegateCommand ShowPreferences { get; }

        public MainViewModel ViewModel { get; }

        public void Minimize()
        {
            Dispatcher.Invoke(() => { if (WindowState != WindowState.Minimized) { SystemCommands.MinimizeWindow(this); } });
        }

        public void Restore()
        {
            Dispatcher.Invoke(() => { if (WindowState == WindowState.Minimized) { SystemCommands.RestoreWindow(this); } });
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ViewModel.SaveUserData();
            base.OnClosing(e);
        }
    }
}