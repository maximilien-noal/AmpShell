namespace AmpShell.WPF.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using AmpShell.ViewModels;

    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
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
    }
}