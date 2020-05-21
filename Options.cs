/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell
{
    using CommandLine;

#pragma warning disable CA1812 // Avoid never-instantiated public classes (it is instantiated by the CommandLine package)

    public class Options
    {
        [Option('g', "game", Required = true, HelpText = "Game to launch silently. The game's name (ie. 'DOOM') or main executable path (ie. 'C:\\games\\Doom.exe') is required.")]
        public string Game { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Verbose mode. AmpShell will tell what it is doing on the standard output.")]
        public bool Verbose { get; set; }

        [Option('s', "setup", Required = false, HelpText = "Run the game's setup executable.")]
        public bool Setup { get; set; }
    }

#pragma warning restore CA1812 // Avoid never-instantiated public classes (it is instantiated by the CommandLine package)
}