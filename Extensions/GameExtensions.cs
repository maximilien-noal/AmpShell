/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Extensions
{
    using System;
    using System.Diagnostics;
    using AmpShell.DAL;
    using AmpShell.DOSBox;
    using AmpShell.Model;

    internal static class GameExtensions
    {
        public static string GetDOSBoxPath(this Game game)
        {
            string dosboxPath = game.AlternateDOSBoxExePath;
            if (string.IsNullOrWhiteSpace(dosboxPath))
            {
                dosboxPath = UserDataAccessor.UserData.DBPath;
            }

            return dosboxPath;
        }

        public static Process Run(this Game game)
        {
            return DOSBoxController.StartDOSBox(game.GetDOSBoxPath(), DOSBoxController.BuildArgs(game, false, game.GetDOSBoxPath(), UserDataAccessor.UserData.DBDefaultConfFilePath, UserDataAccessor.UserData.DBDefaultLangFilePath), game.DBConfPath);
        }

        public static Process RunSetup(this Game game)
        {
            return DOSBoxController.StartDOSBox(game.GetDOSBoxPath(), DOSBoxController.BuildArgs(game, true, game.GetDOSBoxPath(), UserDataAccessor.UserData.DBDefaultConfFilePath, UserDataAccessor.UserData.DBDefaultLangFilePath), game.DBConfPath);
        }
    }
}