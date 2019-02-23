/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AmpShell
{
    /// <summary>
    /// Root node for the xml file
    /// </summary>
    [XmlRoot("AmpShell")]
    public class AmpShell
    {
        /// <summary>
        /// List that will build up the tree of categories and games through the AddChild and RemoveChild and ListChildren methods
        /// </summary>
        private List<AmpShell> _children;
        public AmpShell()
        {
            _children=new List<AmpShell>();
        }
        [XmlElement("Window", typeof(Window))]
        [XmlElement("Category", typeof(Category))]
        [XmlElement("Game", typeof(Game))]
        public AmpShell[] ListChildren
        {
            get { return _children.ToArray(); }
            set
            {
                _children.Clear();
                if (value != _children.ToArray())
                {
                    if (value != null)
                        _children.AddRange(value);
                }
            }
        }

        public void AddChild(AmpShell child)
        {
            _children.Add(child);
        }

        public void MoveChildToFirst(AmpShell child)
        {
            if(_children.Contains(child))
            {
                _children.Remove(child);
                _children.Insert(0, child);
            }
        }

        public void MoveChildToPosition(AmpShell child, int index)
        {
            if (_children.Contains(child))
            {
                _children.Remove(child);
                _children.Insert(index, child);
            }
        }

        public void MoveChildToLast(AmpShell child)
        {
            if (_children.Contains(child))
            {
                _children.Remove(child);
                _children.Insert(_children.Count, child);
            }
        }

        public void RemoveChild(AmpShell child)
        {
            _children.Remove(child);
        }
    }
}