using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MediaRss
{
	public class MediaRssBase
	{
		internal const string NS_URI = "http://search.yahoo.com/mrss/";
		internal const string PREFIX = "media";

		private Dictionary<XmlQualifiedName, string> attributeExtensions;
		private Collection<XElement> elementExtensions;

		public MediaRssBase()
		{
			elementExtensions = new Collection<XElement>();
			attributeExtensions = new Dictionary<XmlQualifiedName, string>();
		}

		protected void AddAttributeExtensions(XmlReader reader)
		{
			if (reader.HasAttributes)
			{
				for (int i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToNextAttribute();

					if (reader.NamespaceURI == "")
					{
						AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
					}
				}
			}
		}
		
		protected void AddElementExtensions(XmlReader reader, bool isEmpty)
		{
			if (!isEmpty)
			{
				while (reader.IsStartElement())
				{
					ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
				}
				reader.ReadEndElement();
			}
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public Dictionary<XmlQualifiedName, string> AttributeExtensions { get { return attributeExtensions; } }
		public Collection<XElement> ElementExtensions { get { return elementExtensions; } }

	}
}