namespace AmpShell.WPF.Views
{
    using AmpShell.ViewModels;
    using Prism.Commands;
    using System.Windows;

    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ShowAboutWindow = new DelegateCommand(() => new AboutWindow(this).ShowDialog());
            ShowPreferences = new DelegateCommand(() => new PreferencesWindow(this).ShowDialog());
            ViewModel = new MainViewModel();
            InitializeComponent();
        }

        public DelegateCommand ShowAboutWindow { get; }

        public DelegateCommand ShowPreferences { get; }

        public MainViewModel ViewModel { get; }
    }
}