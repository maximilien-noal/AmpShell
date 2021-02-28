/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Legacy.Views
{
    using System;
    using System.Windows.Forms;

    using AmpShell.Legacy.ViewModel;

    /// <summary>
    /// Form to add or modify a category (tab).
    /// </summary>
    public partial class CategoryForm : Form
    {
        public CategoryForm() => this.Initialize();

        public CategoryForm(string editedCategorySignature)
        {
            this.ViewModel = new CategoryViewModel(editedCategorySignature);
            this.Initialize();
            this.ModifyViewForEditing();
        }

        public CategoryViewModel ViewModel { get; private set; } = new CategoryViewModel();

        private void Initialize()
        {
            this.InitializeComponent();
            this.NameTextBox.DataBindings.Add("Text", this.ViewModel, "Name");
        }

        private void ModifyViewForEditing()
        {
            this.OK.Text = "&Save and apply";
            this.OK.Width = 102;
            this.OK.Location = new System.Drawing.Point(this.Cancel.Location.X - 107, 41);
            this.OK.Image = Properties.Resources.saveHS;
            this.Cancel.Text = "&Don't save";
            this.Text = $"Editing {this.ViewModel.Name} ...";
        }

        private void Cancel_Click(object sender, EventArgs e) => this.Close();

        private void OK_Click(object sender, EventArgs e)
        {
            if (this.ViewModel.IsDataValid())
            {
                this.ViewModel.CreateCategory();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("You must enter a name for the category.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}