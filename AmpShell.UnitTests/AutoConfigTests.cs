namespace AmpShell.UnitTests
{
    using AmpShell.Core.DAL;
    using FluentAssertions;
    using System.IO;
    using Xunit;

    public class AutoConfigTests
    {
        [Fact]
        public void FindsDOSBoxIfInstalled()
        {
            var userDataAccessor = new UserDataAccessor();
            var dosboxPath = userDataAccessor.GetUserData().DBPath;
            File.Exists(dosboxPath).Should().BeTrue();
        }

        [Fact]
        public void FindsTextEditor()
        {
            var userDataAccessor = new UserDataAccessor();
            var dosboxPath = userDataAccessor.GetUserData().ConfigEditorPath;
            File.Exists(dosboxPath).Should().BeTrue();
        }
    }
}