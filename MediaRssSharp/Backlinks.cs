using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "backLinks", Namespace = "http://search.yahoo.com/mrss/")]
	public class Backlinks : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "backLinks";
		public List<Backlink> BacklinkItems { get; set; }
		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			bool isEmpty = reader.IsEmptyElement;

			AddAttributeExtensions(reader);

			reader.ReadStartElement();

			if (!isEmpty)
			{
				while (reader.IsStartElement())
				{
					if (reader.LocalName == Backlink.ELEMENT_NAME)
					{
						if (BacklinkItems == null)
						{
							BacklinkItems = new List<Backlink>();
						}
						var backlink = new Backlink();
						backlink.ReadXml(reader);

						BacklinkItems.Add(backlink);
					}
					else
					{
						ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
					}
				}
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			if (BacklinkItems != null && BacklinkItems.Count > 0)
			{
				foreach (var backlink in BacklinkItems)
				{
					backlink.WriteXml(writer);
				}
			}

			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}

		}
		#endregion
	}
}