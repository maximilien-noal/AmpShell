namespace AmpShell.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AmpShell.Core.DAL;
    using AmpShell.Core.DOSBox;
    using AmpShell.Core.Model;

    using DeepCopy;

    using ReactiveUI;

    public class PreferencesViewModel : ReactiveObject
    {
        private readonly Preferences _userData = DeepCopier.Copy(UserDataAccessor.UserData);
        public Preferences UserData { get => _userData; }

        public PreferencesViewModel()
        {
        }
    }
}