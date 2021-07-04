/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Core.AutoConfig
{
    using System;
    using System.IO;
    using System.Linq;

    public static class FileFinder
    {
        public static bool HasWriteAccessToAssemblyLocationFolder()
        {
            try
            {
                if (Directory.GetDirectoryRoot(PathFinder.GetStartupPath()) == Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles))
                {
                    return false;
                }
                string tmpFilePath = Path.Combine(PathFinder.GetStartupPath(), Path.GetRandomFileName());
                while (File.Exists(tmpFilePath) == true)
                {
                    tmpFilePath = Path.Combine(PathFinder.GetStartupPath(), Path.GetRandomFileName());
                }
                File.Create(tmpFilePath, 1, FileOptions.DeleteOnClose).Close();
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        internal static string SearchCommonTextEditor()
        {
            string notepadPath = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)), "notepad.exe");
            if (File.Exists(notepadPath))
            {
                return notepadPath;
            }
            return string.Empty;
        }

        internal static string SearchDOSBox(string userConfigDataPath, bool portableMode)
        {
            var windrive = Path.GetPathRoot(Environment.SystemDirectory);
            var programFilesX86Path = Path.Combine(windrive, "Program Files (x86)");
            if (userConfigDataPath == Path.Combine(PathFinder.GetStartupPath(), "AmpShell.xml") && portableMode)
            {
                var localDOSBox = Path.Combine(PathFinder.GetStartupPath(), "dosbox.exe");
                if (File.Exists(localDOSBox))
                {
                    return localDOSBox;
                }
            }
            else if (Directory.Exists(programFilesX86Path))
            {
                string[] dosboxNamedDirs = Directory.GetDirectories(programFilesX86Path, "DOSBox*", SearchOption.TopDirectoryOnly);
                if (dosboxNamedDirs.GetLength(0) != 0)
                {
                    var systemDOSBox = Path.Combine(dosboxNamedDirs[0], "dosbox.exe");
                    if (File.Exists(systemDOSBox))
                    {
                        return systemDOSBox;
                    }
                }
            }
            else
            {
                string[] dosboxNamedDirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "DOSBox*", SearchOption.TopDirectoryOnly);
                if (dosboxNamedDirs.GetLength(0) != 0)
                {
                    var systemDOSBox = Path.Combine(dosboxNamedDirs[0], "dosbox.exe");
                    if (File.Exists(systemDOSBox))
                    {
                        return systemDOSBox;
                    }
                }
            }
            return string.Empty;
        }

        internal static string SearchDOSBoxConf(string userConfigDataPath, string dosboxExecutablePath) => SearchFileWithExtension(userConfigDataPath, dosboxExecutablePath, "*.conf");

        internal static string SearchDOSBoxLanguageFile(string userConfigDataPath, string dosboxExecutablePath) => SearchFileWithExtension(userConfigDataPath, dosboxExecutablePath, "*.lng");

        private static string SearchFileWithExtension(string userConfigDataPath, string dosboxExecutablePath, string extension)
        {
            if (userConfigDataPath == Path.Combine(PathFinder.GetStartupPath(), "AmpShell.xml"))
            {
                var localConfFiles = Directory.GetFiles(PathFinder.GetStartupPath(), extension);
                if (localConfFiles.Length > 0)
                {
                    return localConfFiles[0];
                }
            }
            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DOSBox")))
            {
                string[] appDataLangFiles = Directory.GetFiles(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DOSBox"), extension);
                if (appDataLangFiles.Length > 0)
                {
                    var bestCandidate = appDataLangFiles.FirstOrDefault(x => dosboxExecutablePath.ToUpperInvariant().Contains(Path.GetFileNameWithoutExtension(x.ToUpperInvariant())));
                    if (bestCandidate != null)
                    {
                        return bestCandidate;
                    }
                    return appDataLangFiles[0];
                }
            }
            else
            {
                if (StringExt.IsNullOrWhiteSpace(dosboxExecutablePath) == false && Directory.Exists(Path.GetDirectoryName(dosboxExecutablePath)))
                {
                    var langFilesBesidesDOSBox = Directory.GetFiles(Path.GetDirectoryName(dosboxExecutablePath), extension);
                    if (langFilesBesidesDOSBox.Length > 0)
                    {
                        return langFilesBesidesDOSBox[0];
                    }
                }
            }
            return string.Empty;
        }
    }
}