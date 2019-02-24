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
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AmpShell.UserData
{
    public class ObjectSerializer
    {
        public ObjectSerializer()
        {
        }

        public object Deserialize(string xmlPath, Type targetObjectType)
        {
            XmlSerializer deserializer = new XmlSerializer(targetObjectType);
            TextReader reader;
            object targetObjectInstance;
            reader = new StreamReader(xmlPath, Encoding.Unicode);
            targetObjectInstance = deserializer.Deserialize(reader);
            reader.Close();
            return targetObjectInstance;
        }

        public void Serialize(string xmlPath, object objectToSerialize, Type typeOfObjectToSerialize)
        {
            XmlSerializer serializer = new XmlSerializer(typeOfObjectToSerialize);
            TextWriter writer;
            writer = new StreamWriter(xmlPath, false, Encoding.Unicode);
            serializer.Serialize(writer, objectToSerialize);
            writer.Close();
        }
    }
}