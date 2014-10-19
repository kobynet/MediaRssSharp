using System;
using System.Xml;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "description", Namespace = "http://search.yahoo.com/mrss/")]
	public class Description : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "description";
		public string DescriptionText { get; set; }

		public string Type { get; set; }

		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			var isEmpty = reader.IsEmptyElement;

			if (reader.HasAttributes)
			{
				for (var i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToNextAttribute();

					if (reader.NamespaceURI == "")
					{
						if (reader.LocalName == "type")
						{
							Type = reader.Value;
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
				DescriptionText = reader.ReadContentAsString();
			}

			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(PREFIX, ELEMENT_NAME, NS_URI);
			
			foreach (var kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			if (!String.IsNullOrEmpty(DescriptionText))
			{
				writer.WriteString(DescriptionText);
			}
			writer.WriteEndElement();
			
			foreach (var element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}
}
