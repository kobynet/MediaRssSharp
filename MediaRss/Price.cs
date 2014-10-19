using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "price", Namespace = "http://search.yahoo.com/mrss/")]
	public class Price : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "price";
	
		public String Type { get; set; }
		public Double PriceValue { get; set; }
		public String Currency { get; set; }

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
						if (reader.LocalName == "type")
						{
							Type = reader.Value;
						}
						else if (reader.LocalName == "price")
						{
							//TODO: Should this be TryParse or should we enforce document conformance with a XSD?
							PriceValue = Double.Parse(reader.Value);
						}
						else if (reader.LocalName == "currency")
						{
							Currency = reader.Value;
						}
						else
						{
							AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
						}
					}
				}
			}
			reader.ReadStartElement();


			AddElementExtensions(reader, isEmpty);
			
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("Type: {0}\n", Type);
			builder.AppendFormat("Price: {0}\n", PriceValue);
			builder.AppendFormat("Currency: {0}\n", Currency);

			return builder.ToString();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (!String.IsNullOrEmpty(Type))
			{
				writer.WriteAttributeString("type", null, Type);
			}

			writer.WriteAttributeString("price", null, Type);
			
			if (Currency != null)
			{
				writer.WriteAttributeString("currency", null, Type);
			}
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}
		}
		#endregion
	}
}