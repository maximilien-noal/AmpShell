using System;
using System.Collections;
using System.Windows.Forms;

namespace AmpShell.Views.UserControls
{
    class CustomListViewItemSorter : IComparer
    {
        private readonly int col;
        public CustomListViewItemSorter()
        {
            col = 0;
        }
        public CustomListViewItemSorter(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            if(x is null || y is null)
            {
                return 1;
            }
            if(DateTime.TryParse(((ListViewItem)x).SubItems[col].Text, out DateTime dateX) && DateTime.TryParse(((ListViewItem)y).SubItems[col].Text, out DateTime datey))
            {
                return DateTime.Compare(dateX, datey);
            }
            return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text, StringComparison.CurrentCulture);
        }
    }
}
