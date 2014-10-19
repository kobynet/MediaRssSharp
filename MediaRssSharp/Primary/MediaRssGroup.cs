using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss.Primary
{
	[XmlRoot(ElementName = "group", Namespace = "http://search.yahoo.com/mrss/")]
	public class MediaRssGroup : MediaRssBase, IMrssPrimaryElement, IXmlSerializable
	{

		internal const string ELEMENT_NAME = "group";

		public List<MediaRssContent> ContentNodes { get; set; }

		#region Optional elements

		public MrssOptionalElements OptionalElements { get; set; }

		#endregion

		#region

		public MediaRssGroup()
		{
			OptionalElements = new MrssOptionalElements();
			ContentNodes = new List<MediaRssContent>();
		}

		#endregion 

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
					if (reader.LocalName == MediaRssContent.ELEMENT_NAME)
					{
						var contentNode = new MediaRssContent();
						contentNode.ReadXml(reader);
						ContentNodes.Add(contentNode);
					}
					else if (MediaRssHelper.TryParseOptionalElement(OptionalElements, reader) == false)
						ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
				}
				reader.ReadEndElement();
			}
		}

		public override string ToString()
		{
			var builder = new StringBuilder();

			foreach (var mediaRssContent in ContentNodes)
			{
				builder.Append(mediaRssContent);
			}

			if (ElementExtensions.Count > 0)
			{
				foreach (var elt in ElementExtensions)
				{
					builder.AppendLine(elt.ToString());
				}
			}
			return builder.ToString();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (var kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			foreach (var mediaRssContent in ContentNodes)
			{
				mediaRssContent.WriteXml(writer);
			}

			foreach (var element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}


		#endregion
	}
}