/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

namespace AmpShell.Model
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using AmpShell.Notification;

    /// <summary>
    /// Root node for the xml file.
    /// </summary>
    [XmlRoot("AmpShell")]
    public class ModelWithChildren : PropertyChangedNotifier
    {
        /// <summary>
        /// List that will build up the tree of categories and games through the AddChild and RemoveChild and ListChildren methods.
        /// </summary>
        private readonly List<object> children = new List<object>();

        public ModelWithChildren()
        {
            this.children = new List<object>();
        }

        [XmlElement("Window", typeof(Preferences))]
        [XmlElement("Category", typeof(Category))]
        [XmlElement("Game", typeof(Game))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "User Data Compatibility")]
        public List<object> ListChildren
        {
            get => this.children;

            set
            {
                this.children.Clear();
                if (value != null && value != this.children)
                {
                    this.children.AddRange(value);
                }
            }
        }

        public void AddChild(object child)
        {
            this.children.Add(child);
        }

        public void MoveChildToPosition(object child, int index)
        {
            if (this.children.Contains(child))
            {
                this.children.Remove(child);
                this.children.Insert(index, child);
            }
        }

        public void RemoveChild(object child)
        {
            this.children.Remove(child);
        }
    }
}