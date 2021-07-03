namespace AmpShell.WPF.Views
{
    using System.Windows;

    /// <summary> Interaction logic for AboutWindow.xaml </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        public AboutWindow(Window mainWindow)
        {
            InitializeComponent();
            this.Owner = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}