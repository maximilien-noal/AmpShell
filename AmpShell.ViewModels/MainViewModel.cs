namespace AmpShell.ViewModels
{
    using AmpShell.Core.Games;
    using AmpShell.Core.Model;
    using Prism.Commands;

    public class MainViewModel : UserDataViewModel
    {
        public MainViewModel()
        {
            RunDOSBox = new DelegateCommand(
                () => GameProcessController.RunOnlyDOSBox(_userData),
                () => string.IsNullOrWhiteSpace(_userData.DBPath) == false);
        }

        public DelegateCommand RunDOSBox { get; }

        public Preferences UserData { get => _userData; }
    }
}