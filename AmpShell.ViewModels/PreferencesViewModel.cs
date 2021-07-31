namespace AmpShell.ViewModels
{
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;
    using Prism.Commands;

    /// <summary> TODO: Invoke through a template, not the view. </summary>
    public class PreferencesViewModel : PropertyChangedNotifier
    {
        public PreferencesViewModel()
        {
            OK = new DelegateCommand(OkMethod);
        }

        public DelegateCommand OK { get; private set; }

        private void OkMethod()
        {
        }
    }
}