namespace AmpShell.ViewModels
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;

    public class UserDataViewModel : PropertyChangedNotifier
    {
        protected readonly Preferences _userData;

        protected readonly UserDataAccessor _userDataAccessor = new UserDataAccessor();

        public UserDataViewModel()
        {
            _userData = _userDataAccessor.GetUserData();
        }
    }
}