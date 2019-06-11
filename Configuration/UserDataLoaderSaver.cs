/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.Model;
using AmpShell.Serialization;

using System;
using System.IO;

namespace AmpShell.Configuration
{
    public static class UserDataLoaderSaver
    {
        static UserDataLoaderSaver()
        {
            UserPrefs = new RootModel();
        }

        /// <summary>
        /// Path to the user data file (AmpShell.xml)
        /// </summary>
        public static string UserConfigFileDataPath { get; private set; }

        /// <summary>
        /// Object to load and save user data through XML (de)serialization
        /// </summary>
        public static RootModel UserPrefs { get; private set; }

        public static void SaveUserSettings()
        {
            //saves the data inside Amp by serliazing it in AmpShell.xml
            if (!UserPrefs.PortableMode)
            {
                ObjectSerializer.Serialize(UserDataLoaderSaver.UserConfigFileDataPath, UserPrefs, typeof(ModelWithChildren));
            }
            else
            {
                foreach (Category category in UserPrefs.ListChildren)
                {
                    foreach (Game game in category.ListChildren)
                    {
                        game.DOSEXEPath = game.DOSEXEPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.DBConfPath = game.DBConfPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.AdditionalCommands = game.AdditionalCommands.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.Directory = game.Directory.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.CDPath = game.CDPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.SetupEXEPath = game.SetupEXEPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                        game.Icon = game.Icon.Replace(PathFinder.GetStartupPath(), "AppPath");
                    }
                }
                UserPrefs.DBDefaultConfFilePath = UserPrefs.DBDefaultConfFilePath.Replace(PathFinder.GetStartupPath(), "AppPath");
                UserPrefs.DBDefaultLangFilePath = UserPrefs.DBDefaultLangFilePath.Replace(PathFinder.GetStartupPath(), "AppPath");
                UserPrefs.DBPath = UserPrefs.DBPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                UserPrefs.ConfigEditorPath = UserPrefs.ConfigEditorPath.Replace(PathFinder.GetStartupPath(), "AppPath");
                UserPrefs.ConfigEditorAdditionalParameters = UserPrefs.ConfigEditorAdditionalParameters.Replace(PathFinder.GetStartupPath(), "AppPath");
                ObjectSerializer.Serialize(PathFinder.GetStartupPath() + "\\AmpShell.xml", UserPrefs, typeof(ModelWithChildren));
            }
        }

        public static void LoadUserSettings()
        {
            //If the file named AmpShell.xml doesn't exists inside the directory AmpShell uses the one in the user's profile Application Data directory
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml") == false && File.Exists(PathFinder.GetStartupPath() + "\\AmpShell.xml") == false)
            {
                //take the Windows Height and Width (saved on close with XML serializing)
                UserPrefs.Width = 640;
                UserPrefs.Height = 400;
                //Setup the whole directory path
                if (Directory.GetDirectoryRoot(PathFinder.GetStartupPath()) == Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) || Directory.GetDirectoryRoot(PathFinder.GetStartupPath()) == Environment.SystemDirectory.Substring(0, 3) + "Program Files (x86)")
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
                    UserConfigFileDataPath = PathFinder.GetStartupPath() + "\\AmpShell.xml";
                }
                //Serializing the data inside Amp for the first run
                ObjectSerializer.Serialize(UserConfigFileDataPath, UserPrefs, typeof(ModelWithChildren));
                UserPrefs = (RootModel)ObjectSerializer.Deserialize(UserConfigFileDataPath, typeof(ModelWithChildren));
            }
            //if the file named AmpShell.xml exists inside that directory
            else
            {
                //then, deserialize it in Amp.
                if (File.Exists(PathFinder.GetStartupPath() + "\\AmpShell.xml"))
                {
                    UserConfigFileDataPath = PathFinder.GetStartupPath() + "\\AmpShell.xml";
                }
                else if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml"))
                {
                    UserConfigFileDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\AmpShell\\AmpShell.xml";
                }

                UserPrefs = (RootModel)ObjectSerializer.Deserialize(UserConfigFileDataPath, typeof(ModelWithChildren));
                foreach (Category ConcernedCategory in UserPrefs.ListChildren)
                {
                    foreach (Game ConcernedGame in ConcernedCategory.ListChildren)
                    {
                        ConcernedGame.DOSEXEPath = ConcernedGame.DOSEXEPath.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.DBConfPath = ConcernedGame.DBConfPath.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.AdditionalCommands = ConcernedGame.AdditionalCommands.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.Directory = ConcernedGame.Directory.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.CDPath = ConcernedGame.CDPath.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.SetupEXEPath = ConcernedGame.SetupEXEPath.Replace("AppPath", PathFinder.GetStartupPath());
                        ConcernedGame.Icon = ConcernedGame.Icon.Replace("AppPath", PathFinder.GetStartupPath());
                    }
                }
                UserPrefs.DBDefaultConfFilePath = UserPrefs.DBDefaultConfFilePath.Replace("AppPath", PathFinder.GetStartupPath());
                UserPrefs.DBDefaultLangFilePath = UserPrefs.DBDefaultLangFilePath.Replace("AppPath", PathFinder.GetStartupPath());
                UserPrefs.DBPath = UserPrefs.DBPath.Replace("AppPath", PathFinder.GetStartupPath());
                UserPrefs.ConfigEditorPath = UserPrefs.ConfigEditorPath.Replace("AppPath", PathFinder.GetStartupPath());
                UserPrefs.ConfigEditorAdditionalParameters = UserPrefs.ConfigEditorAdditionalParameters.Replace("AppPath", PathFinder.GetStartupPath());
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