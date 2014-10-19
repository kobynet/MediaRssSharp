using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "community", Namespace = "http://search.yahoo.com/mrss/")]
	public class Community : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "community";
	
		public StarRating Rating { get; set; }
		public Statistics Stats { get; set; }
		public Tags TagItems { get; set; }
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
					switch (reader.LocalName)
					{
						case StarRating.ELEMENT_NAME:
							Rating = new StarRating();
							Rating.ReadXml(reader);
							break;
						case Statistics.ELEMENT_NAME:
							Stats = new Statistics();
							Stats.ReadXml(reader);
							break;
						case Tags.ELEMENT_NAME:
							TagItems = new Tags();
							TagItems.ReadXml(reader);
							break;
						default:
							ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
							break;
					}

				}
			}
			reader.ReadEndElement();
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("StarRating: {0}\n", Rating);
			builder.AppendFormat("Statistics: {0}\n", Stats);
			builder.AppendFormat("Tags: {0}\n", TagItems);
			
			return builder.ToString();
		}


		public void WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}
			
			if (Rating != null)
			{
				Rating.WriteXml(writer);
			}
			if (Stats != null)
			{
				Stats.WriteXml(writer);
			}
			if (TagItems != null)
			{
				TagItems.WriteXml(writer);
			}
			
			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}
}