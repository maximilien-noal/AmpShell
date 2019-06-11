/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Model
{
    public static class RootModelQuery
    {
        /// <summary>
        /// Used when a new Category or Game is created : it's signature must be unique
        /// so AmpShell can recognize it instantly
        /// </summary>
        /// <param name="signatureToTest">A Category's or Game's signature</param>
        /// <param name="userData">The UserData, containing all the categories and games and their signatures</param>
        /// <returns>Whether the signature equals none of the other ones, or not</returns>
        public static bool IsItAUniqueSignature(this RootModel userData, string signatureToTest)
        {
            foreach (Category otherCat in userData.ListChildren)
            {
                if (otherCat.Signature != signatureToTest)
                {
                    if (otherCat.ListChildren.Length != 0)
                    {
                        foreach (Game otherGame in otherCat.ListChildren)
                        {
                            if (otherGame.Signature == signatureToTest)
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}