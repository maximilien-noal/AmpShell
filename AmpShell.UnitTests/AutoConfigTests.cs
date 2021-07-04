namespace AmpShell.UnitTests
{
    using AmpShell.Core.AutoConfig;
    using AmpShell.Core.DAL;
    using FluentAssertions;
    using System.IO;
    using Xunit;

    public class AutoConfigTests
    {
        [Fact]
        public void FindsDOSBoxConfigIfInstalled()
        {
            var userDataAccessor = new UserDataAccessor();
            var dosboxConfigPath = userDataAccessor.GetUserData().DBDefaultConfFilePath;
            File.Exists(dosboxConfigPath).Should().BeTrue();
        }

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

        [Fact]
        public void HasWriteAccesInBinFolder() => FileFinder.HasWriteAccessToAssemblyLocationFolder().Should().BeTrue();
    }
}