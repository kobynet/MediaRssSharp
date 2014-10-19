using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "responses", Namespace = "http://search.yahoo.com/mrss/")]
	public class Responses : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "responses";

		public List<Response> ResponseItems { get; set; }
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
					if (reader.LocalName == Response.ELEMENT_NAME)
					{
						if (ResponseItems == null)
						{
							ResponseItems = new List<Response>();
						}
						var response = new Response();
						response.ReadXml(reader);

						ResponseItems.Add(response);
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


			if (ResponseItems != null && ResponseItems.Count > 0)
			{
				foreach (var response in ResponseItems)
				{
					response.WriteXml(writer);
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