using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "param", Namespace = "http://search.yahoo.com/mrss/")]
	public class Parameter : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "param";
	
		public String Name { get; set; }
		public String Content { get; set; }

		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			bool isEmpty = reader.IsEmptyElement;

			if (reader.HasAttributes)
			{
				for (int i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToNextAttribute();

					if (reader.NamespaceURI == "")
					{
						if (reader.LocalName == "name")
						{
							Name = reader.Value;
						}
						else
						{
							AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
						}
					}
				}
			}

			reader.ReadStartElement();

			if (!isEmpty)
			{
				Content = reader.ReadContentAsString().Trim();
			}

			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("media", "param", NS_URI);
			if (!String.IsNullOrEmpty(Name))
			{
				writer.WriteAttributeString("name", null, Name);
			}

			if (!String.IsNullOrEmpty(Content))
			{
				writer.WriteString(Content);
			}

			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}

			writer.WriteEndElement();
		}
		#endregion
	}
}