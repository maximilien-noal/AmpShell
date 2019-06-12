/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using AmpShell.ViewModel;
using System;
using System.Windows.Forms;

namespace AmpShell.WinForms
{
    public partial class CategoryForm : Form
    {
        public CategoryViewModel ViewModel = new CategoryViewModel();

        public CategoryForm()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeComponent();
            this.NameTextBox.DataBindings.Add("Text", this.ViewModel, "Name");
        }

        public CategoryForm(string editedCategorySignature)
        {
            ViewModel = new CategoryViewModel(editedCategorySignature);
            Initialize();
            NameTextBox.Text = ViewModel.Name;
            OK.Text = "&Save and apply";
            OK.Width = 102;
            OK.Location = new System.Drawing.Point(Cancel.Location.X - 107, 41);
            OK.Image = Properties.Resources.saveHS;
            Cancel.Text = "&Don't save";
            Text = "Editing " + ViewModel.Name + "...";
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if(ViewModel.IsDataValid())
            {
                ViewModel.CreateCategory();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("You must enter a name for the category.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}