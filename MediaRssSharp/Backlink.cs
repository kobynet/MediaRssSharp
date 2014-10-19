using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "backLink", Namespace = "http://search.yahoo.com/mrss/")]
	public class Backlink : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "backLink";
		public Uri BacklinkUri { get; set; }

		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			bool isEmpty = reader.IsEmptyElement;

			AddAttributeExtensions(reader);

			reader.ReadStartElement();

			if (!isEmpty)
			{
				BacklinkUri = new Uri(reader.ReadContentAsString());
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

			if (BacklinkUri != null)
			{
				writer.WriteString(BacklinkUri.ToString());
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