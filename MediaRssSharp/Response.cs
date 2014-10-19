using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "response", Namespace = "http://search.yahoo.com/mrss/")]
	public class Response : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "response";
		public Uri ResponseLink { get; set; }

		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			bool isEmpty = reader.IsEmptyElement;

			AddAttributeExtensions(reader);

			reader.ReadStartElement();

			if (!isEmpty)
			{
				ResponseLink = new Uri(reader.ReadContentAsString());
			}

			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(PREFIX, ELEMENT_NAME, NS_URI);

			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			if (ResponseLink != null)
			{
				writer.WriteString(ResponseLink.ToString());
			}
			writer.WriteEndElement();

			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			} 
		}
		#endregion
	}
}