﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Core.Platform
{
    using System;
    using System.IO;

    internal static class PlatformDetector
    {
        public static bool IsNix() => Directory.Exists("/");

        public static bool IsWindows() => Directory.Exists(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)));

        internal static bool IsMacOs() => Directory.Exists("/System/Library");
    }
}