/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell
{
    using System;
    using System.Linq;

    internal class Options
    {
        private readonly CommandLineOption game = new CommandLineOption() { ShortName = "-g", LongName = "--game", Required = true, Value = string.Empty, HelpText = "Game to launch silently. The game's name (ie. 'DOOM') or main executable path (ie. 'C:\\games\\Doom.exe') is required." };
        private readonly CommandLineOption verbose = new CommandLineOption() { ShortName = "-v", LongName = "--verbose", Required = false, Value = string.Empty, HelpText = "Verbose mode. AmpShell will tell what it is doing on the standard output." };
        private readonly CommandLineOption setup = new CommandLineOption() { ShortName = "-s", LongName = "--setup", Required = false, Value = string.Empty, HelpText = "Run the game's setup executable." };

        public Options()
        {
        }

        public Options(string[] args)
        {
            this.verbose.IsProvided = args.Contains(this.verbose.ShortName) || args.Contains(this.verbose.LongName);
            this.setup.IsProvided = args.Contains(this.setup.ShortName) || args.Contains(this.setup.LongName);
            this.game.IsProvided = args.Contains(this.game.ShortName) || args.Contains(this.game.LongName);

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (StringExt.IsNullOrWhiteSpace(arg) == false)
                {
                    arg = arg.Trim();
                    if (arg == this.game.ShortName || arg == this.game.LongName)
                    {
                        var index = i + 1;
                        if (args.Length > index)
                        {
                            this.Game.Value = args[index];
                            break;
                        }
                    }
                }
            }
        }

        public CommandLineOption Game { get => this.game; }

        public CommandLineOption Verbose { get => this.verbose; }

        public CommandLineOption Setup { get => this.setup; }
    }
}