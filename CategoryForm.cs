/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
using System;
using System.Windows.Forms;

namespace AmpShell
{
    public partial class CategoryForm : Form
    {
        private Category _Category;
        public CategoryForm()
        {
            InitializeComponent();
            Cat = new Category();
        }
        public CategoryForm(Category EditedCat)
        {
            InitializeComponent();
            Cat = EditedCat;
            Cat.Title = EditedCat.Title;
            Cat.Signature = EditedCat.Signature;
            textBox.Text = Cat.Title;
            OK.Text = "&Save and apply";
            OK.Width=102;
            OK.Location = new System.Drawing.Point(Cancel.Location.X-107, 41);
            OK.Image = global::AmpShell.Properties.Resources.saveHS;
            Cancel.Text = "&Don't save";
        }
        public Category Cat
        {
            get { return _Category; }
            set { _Category = value; }
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
			Close();
        }
        private void OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) == false)
			{
				Cat.Title = textBox.Text;
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				MessageBox.Show("You must enter a name for the category.",this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
