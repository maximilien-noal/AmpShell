﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2021 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.WinForms.ViewModel
{
    using System;

    using AmpShell.Core.DAL;
    using AmpShell.Core.Model;
    using AmpShell.Core.Notification;

    public class CategoryViewModel : PropertyChangedNotifier
    {
        private readonly string editedCategorySignature;

        private string name = string.Empty;

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(string editedCategorySignature)
        {
            this.editedCategorySignature = editedCategorySignature;
            this.Name = Program.UserDataAccessorInstance.GetCategoryWithSignature(editedCategorySignature).Title;
        }

        public string Name
        {
            get => this.name;
            set => this.Set(ref this.name, value);
        }

        public void CreateCategory()
        {
            if (StringExt.IsNullOrWhiteSpace(this.editedCategorySignature))
            {
                var category = new Category(this.Name, Program.UserDataAccessorInstance.GetAnUniqueSignature());
                Program.UserDataAccessorInstance.AddCategory(category);
            }
            else
            {
                Program.UserDataAccessorInstance.GetCategoryWithSignature(this.editedCategorySignature).Title = this.Name;
            }
        }

        public bool IsDataValid() => StringExt.IsNullOrWhiteSpace(this.Name) == false;
    }
}