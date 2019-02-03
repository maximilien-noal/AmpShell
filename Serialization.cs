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

namespace AmpShell
{
    public class Serializer
    {
        //constructor
        public Serializer()
        {
        }
        //Deserialization method
        public Object Deserialize(string XmlPath, Type TargetObjectType)
        {
			XmlSerializer deserializer = new XmlSerializer(TargetObjectType);
			TextReader reader;
			Object Instance;
			reader = new StreamReader(XmlPath, Encoding.Unicode);
			//cast (casting is a code line like : object = (type)MethodCall) the data into AmpInstance.
			Instance = deserializer.Deserialize(reader);
			reader.Close();
			return Instance;
        }
        //Serialization Method
        public void Serialize(string XmlPath, Object ObjectToSerialize, Type TypeOfObjectToSerialize)
        {
			XmlSerializer serializer = new XmlSerializer(TypeOfObjectToSerialize);
			TextWriter writer;
			//using Unicode because Unicode is cool. And Unicode makes the application usable worldwide
			writer = new StreamWriter(XmlPath, false, Encoding.Unicode);
			serializer.Serialize(writer, ObjectToSerialize);
			writer.Close();        
		}
    }
}