namespace AmpShell.Core.Config
{
    using AmpShell.Core.DAL;
    using AmpShell.Core.Model;
    using System;
    using System.Diagnostics;
    using System.IO;

    public static class ConfigEditorRunner
    {
        public static bool CanOpenDefaultConfigFile(Preferences userData) => CanRunConfigEditor(userData) && StringExt.IsNullOrWhiteSpace(userData.ConfigEditorPath) == false && File.Exists(userData.ConfigEditorPath);

        public static bool CanRunConfigEditor(Preferences userData) => StringExt.IsNullOrWhiteSpace(userData.ConfigEditorPath) == false && File.Exists(userData.ConfigEditorPath);

        public static void EditDefaultConfigFile(UserDataAccessor dal, Preferences preferences) => Process.Start(dal.GetConfigEditorPath(), $"{preferences.DBDefaultConfFilePath} {preferences.ConfigEditorAdditionalParameters}");

        public static void RunConfigEditor(UserDataAccessor dal, Preferences preferences) => Process.Start(dal.GetConfigEditorPath(), preferences.ConfigEditorAdditionalParameters);
    }
}