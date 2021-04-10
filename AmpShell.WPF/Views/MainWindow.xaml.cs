namespace AmpShell.WPF.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    using AmpShell.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e) => new AboutWindow(this).ShowDialog();

        private void PreferencesMenuItem_Click(object sender, RoutedEventArgs e) => new PreferencesWindow(this).ShowDialog();
    }
}