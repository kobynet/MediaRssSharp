using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "statistics", Namespace = "http://search.yahoo.com/mrss/")]
	public class Statistics : MediaRssBase, IXmlSerializable
	{
		#region const
		internal const string ELEMENT_NAME = "statistics";
		#endregion

		#region public properties
		public Int32 Views { get; set; }
		public Int32 Favorites { get; set; }
		#endregion

		#region IXmlSerializable
		public void ReadXml(XmlReader reader)
		{
			Boolean isEmpty = reader.IsEmptyElement;
			if (reader.HasAttributes)
			{
				for (int i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToNextAttribute();

					if (reader.NamespaceURI == "")
					{
						switch (reader.LocalName)
						{
							case "views":
								Views = Int32.Parse(reader.Value);
								break;
							case "favorites":
								Favorites = Int32.Parse(reader.Value);
								break;
							default:
								AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								break;
						}
					}
				}
			}
			reader.ReadStartElement();

			AddElementExtensions(reader, isEmpty);
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(PREFIX, ELEMENT_NAME, NS_URI);

			writer.WriteAttributeString("views", null, Views.ToString());
			writer.WriteAttributeString("favorites", null, Favorites.ToString());
			
			//Write out any addtional attributes
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}
			writer.WriteEndElement();

			//write out any addtional nested elements
			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}
}
