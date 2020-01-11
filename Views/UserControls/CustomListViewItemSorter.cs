/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Views.UserControls
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    internal class CustomListViewItemSorter : IComparer
    {
        private readonly int col;

        public CustomListViewItemSorter()
        {
            this.col = 0;
        }

        public CustomListViewItemSorter(int column)
        {
            this.col = column;
        }

        public int Compare(object x, object y)
        {
            if (x is null || y is null)
            {
                return 1;
            }
            if (DateTime.TryParse(((ListViewItem)x).SubItems[this.col].Text, out DateTime dateX) && DateTime.TryParse(((ListViewItem)y).SubItems[this.col].Text, out DateTime datey))
            {
                return DateTime.Compare(dateX, datey);
            }
            return string.Compare(((ListViewItem)x).SubItems[this.col].Text, ((ListViewItem)y).SubItems[this.col].Text, StringComparison.CurrentCulture);
        }
    }
}