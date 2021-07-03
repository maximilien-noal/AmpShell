namespace AmpShell.ViewModels
{
    using AmpShell.Core.Model;
    using Prism.Commands;

    /// <summary> TODO: Invoke through a template, not the view. </summary>
    public class PreferencesViewModel : UserDataViewModel
    {
        public PreferencesViewModel()
        {
            Validate = new DelegateCommand(ValidateMethod);
        }

        public Preferences UserData { get => _userData; }

        public DelegateCommand Validate { get; private set; }

        private void ValidateMethod()
        {
            _userDataAccessor.UpdatePreferences(_userData);
        }
    }
}