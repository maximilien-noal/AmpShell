namespace AmpShell.WPF.Views
{
    using AmpShell.ViewModels;
    using ReactiveUI;
    using System.Reactive;
    using System.Windows;

    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ShowAboutWindow = new SimpleViewCommand(() => new AboutWindow(this).ShowDialog());
            ShowPreferences = new SimpleViewCommand(() => new PreferencesWindow(this).ShowDialog());
            ViewModel = new MainViewModel();
            InitializeComponent();
        }

        public SimpleViewCommand ShowAboutWindow { get; }

        public SimpleViewCommand ShowPreferences { get; }

        public MainViewModel ViewModel { get; }
    }
}