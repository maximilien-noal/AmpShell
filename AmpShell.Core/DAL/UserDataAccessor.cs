﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Core.DAL
{
    using AmpShell.Core.AutoConfig;
    using AmpShell.Core.Model;
    using AmpShell.Core.Platform;
    using AmpShell.Core.Serialization;

    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    public class UserDataAccessor
    {
        private const string AppPathPlaceHolder = "AppPath";

        /// <summary> Gets object to load and save user data through XML (de)serialization. </summary>
        private Preferences _userData;

        public UserDataAccessor()
        {
            _userData = new Preferences();
            DeserializeUserData();
        }

        public void AddCategory(Category category) => _userData.AddChild(category);

        public void DeleteCategory(Category category) => _userData.RemoveChild(category);

        public void DisableDOSBoxUsage() => _userData.GamesUseDOSBox = false;

        public string GetAnUniqueSignature() => Guid.NewGuid().ToString();

        public Category GetCategoryWithSignature(string signature) => _userData.ListChildren.Cast<Category>().FirstOrDefault(x => x.Signature == signature);

        public string GetConfigEditorPath()
        {
            if (PlatformDetector.IsWindows() && StringExt.IsNullOrWhiteSpace(_userData.ConfigEditorPath) == false && Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)), "NOTEPAD.EXE").ToUpperInvariant() == _userData.ConfigEditorPath.ToUpperInvariant())
            {
                return Path.GetFileName(_userData.ConfigEditorPath).ToLowerInvariant();
            }
            return _userData.ConfigEditorPath;
        }

        public Game GetFirstGameWithName(string name)
        {
            if (StringExt.IsNullOrWhiteSpace(name))
            {
                return new Game();
            }
            var game = _userData.ListChildren.Cast<Category>().SelectMany(x => x.ListChildren.Cast<Game>()).FirstOrDefault(x => StringExt.IsNullOrWhiteSpace(x.Name) == false && x.Name.Trim().ToUpperInvariant() == name.Trim().ToUpperInvariant());
            if (game is null)
            {
                return new Game();
            }
            return game;
        }

        public Game GetGameWithMainExecutable(string executablePath)
        {
            if (StringExt.IsNullOrWhiteSpace(executablePath))
            {
                return new Game();
            }
            var game = _userData.ListChildren.Cast<Category>().SelectMany(x => x.ListChildren.Cast<Game>()).FirstOrDefault(x => StringExt.IsNullOrWhiteSpace(x.DOSEXEPath) == false && x.DOSEXEPath.Trim().ToUpperInvariant() == executablePath.Trim().ToUpperInvariant());
            if (game is null)
            {
                return new Game()
                {
                    DOSEXEPath = executablePath
                };
            }
            return game;
        }

        public Game GetGameWithSignature(string signature) => _userData.ListChildren.Cast<Category>().SelectMany(x => x.ListChildren.Cast<Game>()).FirstOrDefault(x => x.Signature == signature);

        public Preferences GetUserData() => _userData;

        public bool ImportGamesAndCategories(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                throw new FileNotFoundException(fileName);
            }
            if (fileName.ToUpperInvariant().Trim() == GetDataFilePath().ToUpper().Trim())
            {
                throw new InvalidOperationException("Can't import data from the current in-use file. Nothing to import.");
            }
            var dataImported = false;
            var importData = ObjectSerializer.Deserialize<Preferences>(fileName);
            foreach (var category in importData.ListChildren.Cast<Category>())
            {
                _userData.AddChild(category);
            }
            foreach (var category in _userData.ListChildren.Cast<Category>())
            {
                foreach (var game in category.ListChildren.Cast<Game>())
                {
                    game.Signature = GetAnUniqueSignature();
                }
                category.Signature = GetAnUniqueSignature();
                dataImported = true;
            }
            return dataImported;
        }

        public bool IsThisTheFirstRun() => File.Exists(GetDataFilePath());

        public void SaveUserData()
        {
            if (_userData.PortableMode)
            {
                ObjectSerializer.Serialize(GetDataFilePath(), _userData);
                return;
            }
            for (int i = 0; i < _userData.ListChildren.Count; i++)
            {
                Category category = (Category)_userData.ListChildren[i];
                for (int j = 0; j < category.ListChildren.Count; j++)
                {
                    Game game = (Game)category.ListChildren[j];
                    game.DOSEXEPath = game.DOSEXEPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.DBConfPath = game.DBConfPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.AdditionalCommands = game.AdditionalCommands.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.Directory = game.Directory.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.CDPath = game.CDPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.SetupEXEPath = game.SetupEXEPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                    game.Icon = game.Icon.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
                }
            }
            _userData.DBDefaultConfFilePath = _userData.DBDefaultConfFilePath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
            _userData.DBDefaultLangFilePath = _userData.DBDefaultLangFilePath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
            _userData.DBPath = _userData.DBPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
            _userData.ConfigEditorPath = _userData.ConfigEditorPath.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
            _userData.ConfigEditorAdditionalParameters = _userData.ConfigEditorAdditionalParameters.Replace(PathFinder.GetStartupPath(), AppPathPlaceHolder);
            ObjectSerializer.Serialize(Path.Combine(PathFinder.GetStartupPath(), "AmpShell.xml"), _userData);
        }

        public void UpdateDOSBoxPath(string dosboxPath) => _userData.DBPath = dosboxPath;

        public void UpdateGlobalUserPreferences(Preferences userData)
        {
            UpdateDOSBoxPath(userData.DBPath);
            _userData.ListChildren = userData.ListChildren;
            _userData.GamesUseDOSBox = userData.GamesUseDOSBox;
            _userData.CategoriesDefaultViewMode = _userData.CategoriesDefaultViewMode;
            _userData.PortableMode = userData.PortableMode;
            _userData.DBDefaultConfFilePath = userData.DBDefaultConfFilePath;
            _userData.DBDefaultLangFilePath = userData.DBDefaultLangFilePath;
            _userData.CategoryDeletePrompt = userData.CategoryDeletePrompt;
            _userData.CDsDefaultDir = userData.CDsDefaultDir;
            _userData.ConfigEditorAdditionalParameters = _userData.ConfigEditorAdditionalParameters;
            _userData.ConfigEditorPath = userData.ConfigEditorPath;
            _userData.DefaultIconViewOverride = userData.DefaultIconViewOverride;
            _userData.GameDeletePrompt = userData.GameDeletePrompt;
            _userData.GamesUseDOSBox = userData.GamesUseDOSBox;
            _userData.GamesDefaultDir = userData.GamesDefaultDir;
            _userData.GamesAdditionalCommands = userData.GamesAdditionalCommands;
            _userData.GamesInFullScreen = userData.GamesInFullScreen;
            _userData.GamesNoConsole = userData.GamesNoConsole;
            _userData.GamesQuitOnExit = userData.GamesQuitOnExit;
            _userData.RememberWindowPosition = userData.RememberWindowPosition;
            _userData.RememberWindowSize = userData.RememberWindowSize;
            _userData.CategoryDeletePrompt = userData.CategoryDeletePrompt;
            _userData.MenuBarVisible = userData.MenuBarVisible;
            _userData.ToolBarVisible = userData.ToolBarVisible;
            _userData.StatusBarVisible = userData.StatusBarVisible;
        }

        public void UpdateIsMenuBarVisible(bool isVisible) => _userData.MenuBarVisible = isVisible;

        public void UpdateIsWindowFullscreen(bool isFullscreen) => _userData.Fullscreen = isFullscreen;

        public void UpdateStatusBarVisibility(bool isVisible) => _userData.StatusBarVisible = isVisible;

        public void UpdateToolBarVisibility(bool isVisible) => _userData.ToolBarVisible = isVisible;

        public void UpdateWindowLocation(Point location)
        {
            if (_userData.RememberWindowPosition == false)
            {
                return;
            }
            _userData.X = location.X;
            _userData.Y = location.Y;
        }

        public void UpdateWindowSize(int width, int height)
        {
            if (_userData.RememberWindowSize == false)
            {
                return;
            }
            _userData.Width = width;
            _userData.Height = height;
        }

        /// <summary> Returns the absolute the path to the user data file (AmpShell.xml). </summary>
        /// <returns> The absolute path to the user data file. </returns>
        internal string GetDataFilePath()
        {
            var appDataFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AmpShell\\AmpShell.xml");
            if (StringExt.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AmpShellDebug")) == false)
            {
                return appDataFile;
            }
            if (FileFinder.HasWriteAccessToAssemblyLocationFolder() == false)
            {
                var appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AmpShell");
                if (Directory.Exists(appDataDir) == false)
                {
                    Directory.CreateDirectory(appDataDir);
                }
                return appDataFile;
            }
            else
            {
                var portableAppDataFile = Path.Combine(PathFinder.GetStartupPath(), "AmpShell.xml");
                if (File.Exists(portableAppDataFile))
                {
                    return portableAppDataFile;
                }
                else
                {
                    return appDataFile;
                }
            }
        }

        private void DeserializeUserData()
        {
            _userData = new Preferences();
            string dataFilePath = GetDataFilePath();
            if (File.Exists(dataFilePath))
            {
                _userData = ObjectSerializer.Deserialize<Preferences>(dataFilePath);
            }
            if (_userData.PortableMode)
            {
                for (int i = 0; i < _userData.ListChildren.Count; i++)
                {
                    Category concernedCategory = (Category)_userData.ListChildren[i];
                    for (int j = 0; j < concernedCategory.ListChildren.Count; j++)
                    {
                        Game concernedGame = (Game)concernedCategory.ListChildren[j];
                        concernedGame.DOSEXEPath = concernedGame.DOSEXEPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.DBConfPath = concernedGame.DBConfPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.AdditionalCommands = concernedGame.AdditionalCommands.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.Directory = concernedGame.Directory.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.CDPath = concernedGame.CDPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.SetupEXEPath = concernedGame.SetupEXEPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                        concernedGame.Icon = concernedGame.Icon.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                    }
                }
                _userData.DBDefaultConfFilePath = _userData.DBDefaultConfFilePath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                _userData.DBDefaultLangFilePath = _userData.DBDefaultLangFilePath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                _userData.DBPath = _userData.DBPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                _userData.ConfigEditorPath = _userData.ConfigEditorPath.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
                _userData.ConfigEditorAdditionalParameters = _userData.ConfigEditorAdditionalParameters.Replace(AppPathPlaceHolder, PathFinder.GetStartupPath());
            }

            var fileFinder = new FileFinder(_userData);
            if (StringExt.IsNullOrWhiteSpace(_userData.DBPath) || File.Exists(_userData.DBPath) == false)
            {
                _userData.DBPath = fileFinder.SearchDOSBox(dataFilePath);
            }
            if (StringExt.IsNullOrWhiteSpace(_userData.ConfigEditorPath) || File.Exists(_userData.ConfigEditorPath) == false)
            {
                _userData.ConfigEditorPath = FileFinder.SearchCommonTextEditor();
            }

            if (StringExt.IsNullOrWhiteSpace(_userData.DBDefaultConfFilePath) || File.Exists(_userData.DBDefaultConfFilePath) == false)
            {
                _userData.DBDefaultConfFilePath = FileFinder.SearchDOSBoxConf(dataFilePath, _userData.DBPath);
            }

            if (StringExt.IsNullOrWhiteSpace(_userData.DBDefaultLangFilePath) || File.Exists(_userData.DBDefaultLangFilePath) == false)
            {
                _userData.DBDefaultLangFilePath = FileFinder.SearchDOSBoxLanguageFile(dataFilePath, _userData.DBPath);
            }
        }
    }
}