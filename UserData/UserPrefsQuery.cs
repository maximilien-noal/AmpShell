using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmpShell.UserData
{
    public static class UserPrefsQuery
    {
        /// <summary>
        /// Used when a new Category or Game is created : it's signature must be unique
        /// so AmpShell can recognize it instantly
        /// </summary>
        /// <param name="signatureToTest">A Category's or Game's signature</param>
        /// <param name="userPrefs">The UserData, containing all the categories and games and their signatures</param>
        /// <returns>Whether the signature equals none of the other ones, or not</returns>
        public static bool IsItAUniqueSignature(this UserPrefs userPrefs, string signatureToTest)
        {
            foreach (UserCategory otherCat in userPrefs.ListChildren)
            {
                if (otherCat.Signature != signatureToTest)
                {
                    if (otherCat.ListChildren.Length != 0)
                    {
                        foreach (UserGame otherGame in otherCat.ListChildren)
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
