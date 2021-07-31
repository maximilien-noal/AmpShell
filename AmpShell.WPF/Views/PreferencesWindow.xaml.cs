namespace AmpShell.WPF.Views
{
    using AmpShell.ViewModels;
    using System.Windows;

    /// <summary> Interaction logic for PreferencesWindow.xaml </summary>
    public partial class PreferencesWindow : Window
    {
        public PreferencesWindow()
        {
            InitializeComponent();
            this.DataContext = new PreferencesViewModel();
        }

        public PreferencesWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.Owner = mainWindow;
            this.DataContext = new PreferencesViewModel();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e) => this.Close();

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ((PreferencesViewModel)this.DataContext).OK.Execute();
            this.Close();
        }
    }
}