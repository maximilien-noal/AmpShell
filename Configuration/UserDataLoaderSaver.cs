/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
using AmpShell.UserData;

using System;
using System.IO;
using System.Windows.Forms;

namespace AmpShell.Configuration
{
    public static class UserDataLoaderSaver
    {
        static UserDataLoaderSaver()
        {
            UserPrefs = new UserPrefs();
        }

        /// <summary>
        /// Path to the user data file (AmpShell.xml)
        /// </summary>
        public static string UserConfigFileDataPath { get; private set; }

        /// <summary>
        /// Object to load and save user data through XML (de)serialization
        /// </summary>
        public static UserPrefs UserPrefs { get; private set; }

        public static void SaveUserSettings()
        {
            //saves the data inside Amp by serliazing it in AmpShell.xml
            if (!UserPrefs.PortableMode)
            {
                ObjectSerializer.Serialize(UserDataLoaderSaver.UserConfigFileDataPath, UserPrefs, typeof(UserDataRoot));
            }
            else
            {
                foreach (UserCategory category in UserPrefs.ListChildren)
                {
                    foreach (UserGame game in category.ListChildren)
                    {
                        game.DOSEXEPath = game.DOSEXEPath.Replace(Application.StartupPath, "AppPath");
                        game.DBConfPath = game.DBConfPath.Replace(Application.StartupPath, "AppPath");
                        game.AdditionalCommands = game.AdditionalCommands.Replace(Application.StartupPath, "AppPath");
                        game.Directory = game.Directory.Replace(Application.StartupPath, "AppPath");
                        game.CDPath = game.CDPath.Replace(Application.StartupPath, "AppPath");
                        game.SetupEXEPath = game.SetupEXEPath.Replace(Application.StartupPath, "AppPath");
                        game.Icon = game.Icon.Replace(Application.StartupPath, "AppPath");
                    }
                }
                UserPrefs.DBDefaultConfFilePath = UserPrefs.DBDefaultConfFilePath.Replace(Application.StartupPath, "AppPath");
                UserPrefs.DBDefaultLangFilePath = UserPrefs.DBDefaultLangFilePath.Replace(Application.StartupPath, "AppPath");
                UserPrefs.DBPath = UserPrefs.DBPath.Replace(Application.StartupPath, "AppPath");
                UserPrefs.ConfigEditorPath = UserPrefs.ConfigEditorPath.Replace(Application.StartupPath, "AppPath");
                UserPrefs.ConfigEditorAdditionalParameters = UserPrefs.ConfigEditorAdditionalParameters.Replace(Application.StartupPath, "AppPath");
                ObjectSerializer.Serialize(Application.StartupPath + "\\AmpShell.xml", UserPrefs, typeof(UserDataRoot));
            }
        }

        public static void LoadUserSettings()
        {
            //If the file named AmpShell.xml doesn't exists inside the directory AmpShell uses the one in the user's profile Application Data directory
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml") == false && File.Exists(Application.StartupPath + "\\AmpShell.xml") == false)
            {
                //take the Windows Height and Width (saved on close with XML serializing)
                UserPrefs.Width = 640;
                UserPrefs.Height = 400;
                //Setup the whole directory path
                if (Directory.GetDirectoryRoot(Application.StartupPath) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(Application.StartupPath) == Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)")
                {
                    UserConfigFileDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell";
                    //create the directory
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell") == false)
                    {
                        Directory.CreateDirectory(UserConfigFileDataPath);
                        UserConfigFileDataPath += "\\AmpShell.xml";
                    }
                }
                else
                {
                    UserConfigFileDataPath = Application.StartupPath + "\\AmpShell.xml";
                }
                //Serializing the data inside Amp for the first run
                ObjectSerializer.Serialize(UserConfigFileDataPath, UserPrefs, typeof(UserDataRoot));
                UserPrefs = (UserPrefs)ObjectSerializer.Deserialize(UserConfigFileDataPath, typeof(UserDataRoot));
            }
            //if the file named AmpShell.xml exists inside that directory
            else
            {
                //then, deserialize it in Amp.
                if (File.Exists(Application.StartupPath + "\\AmpShell.xml"))
                {
                    UserConfigFileDataPath = Application.StartupPath + "\\AmpShell.xml";
                }
                else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml"))
                {
                    UserConfigFileDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml";
                }

                UserPrefs = (UserPrefs)ObjectSerializer.Deserialize(UserConfigFileDataPath, typeof(UserDataRoot));
                foreach (UserCategory ConcernedCategory in UserPrefs.ListChildren)
                {
                    foreach (UserGame ConcernedGame in ConcernedCategory.ListChildren)
                    {
                        ConcernedGame.DOSEXEPath = ConcernedGame.DOSEXEPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.DBConfPath = ConcernedGame.DBConfPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.AdditionalCommands = ConcernedGame.AdditionalCommands.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.Directory = ConcernedGame.Directory.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.CDPath = ConcernedGame.CDPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.SetupEXEPath = ConcernedGame.SetupEXEPath.Replace("AppPath", Application.StartupPath);
                        ConcernedGame.Icon = ConcernedGame.Icon.Replace("AppPath", Application.StartupPath);
                    }
                }
                UserPrefs.DBDefaultConfFilePath = UserPrefs.DBDefaultConfFilePath.Replace("AppPath", Application.StartupPath);
                UserPrefs.DBDefaultLangFilePath = UserPrefs.DBDefaultLangFilePath.Replace("AppPath", Application.StartupPath);
                UserPrefs.DBPath = UserPrefs.DBPath.Replace("AppPath", Application.StartupPath);
                UserPrefs.ConfigEditorPath = UserPrefs.ConfigEditorPath.Replace("AppPath", Application.StartupPath);
                UserPrefs.ConfigEditorAdditionalParameters = UserPrefs.ConfigEditorAdditionalParameters.Replace("AppPath", Application.StartupPath);
            }
            if (string.IsNullOrWhiteSpace(UserPrefs.DBPath))
            {
                UserPrefs.DBPath = FileFinder.SearchDOSBox(UserConfigFileDataPath, UserPrefs.PortableMode);
            }
            else if (File.Exists(UserPrefs.DBPath) == false)
            {
                UserPrefs.DBPath = FileFinder.SearchDOSBox(UserConfigFileDataPath, UserPrefs.PortableMode);
            }
            if (string.IsNullOrWhiteSpace(UserPrefs.ConfigEditorPath))
            {
                UserPrefs.ConfigEditorPath = FileFinder.SearchCommonTextEditor();
            }
            else if (File.Exists(UserPrefs.ConfigEditorPath) == false)
            {
                UserPrefs.ConfigEditorPath = FileFinder.SearchCommonTextEditor();
            }
            
            if (string.IsNullOrWhiteSpace(UserPrefs.DBDefaultConfFilePath))
            {
                UserPrefs.DBDefaultConfFilePath = FileFinder.SearchDOSBoxConf(UserConfigFileDataPath, UserPrefs.DBPath);
            }
            else if (File.Exists(UserPrefs.DBDefaultConfFilePath) == false)
            {
                UserPrefs.DBDefaultConfFilePath = FileFinder.SearchDOSBoxConf(UserConfigFileDataPath, UserPrefs.DBPath);
            }

            if (string.IsNullOrWhiteSpace(UserPrefs.DBDefaultLangFilePath) == false)
            {
                UserPrefs.DBDefaultLangFilePath = FileFinder.SearchDOSBoxLanguageFile(UserPrefs.DBPath);
            }
            else if (File.Exists(UserPrefs.DBDefaultLangFilePath) == false)
            {
                UserPrefs.DBDefaultLangFilePath = FileFinder.SearchDOSBoxLanguageFile(UserPrefs.DBPath);
            }
        }
    }
}
