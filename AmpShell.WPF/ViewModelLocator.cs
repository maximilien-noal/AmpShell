namespace AmpShell.WPF
{
    using AmpShell.ViewModels;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ViewModelLocator
    {
        private static MainViewModel _mainViewModel = new MainViewModel();

        public static MainViewModel MainViewModel => _mainViewModel;
    }
}