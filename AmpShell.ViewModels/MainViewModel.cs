namespace AmpShell.ViewModels
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.Games;
    using AmpShell.Core.Model;

    using Prism.Commands;

    public class MainViewModel : UserDataViewModel
    {
        private readonly UserDataAccessor _dal = new UserDataAccessor();

        public MainViewModel()
        {
            UserData = _dal.GetUserData();
            RunDOSBox = new DelegateCommand(
                () => GameProcessController.RunOnlyDOSBox(_userData),
                () => string.IsNullOrWhiteSpace(_userData.DBPath) == false);
        }

        public DelegateCommand RunDOSBox { get; }

        public Preferences UserData { get; private set; }
    }
}