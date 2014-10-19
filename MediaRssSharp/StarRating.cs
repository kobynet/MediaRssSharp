using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "starRating", Namespace = "http://search.yahoo.com/mrss/")]
	public class StarRating : MediaRssBase, IXmlSerializable
	{
		#region const
		internal const string ELEMENT_NAME = "starRating";
		#endregion
		
		#region public properties
		public Double Average { get; set; }
		public Int32 Count { get; set; }
		public Double Min { get; set; }
		public Double Max { get; set; }
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
							case "average":
								Average = Double.Parse(reader.Value);
								break;
							case "min":
								Min = Double.Parse(reader.Value);
								break;
							case "max":
								Max = Double.Parse(reader.Value);
								break;
							case "count":
								Count = Int32.Parse(reader.Value);
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

			writer.WriteAttributeString("average", null, Average.ToString());
			writer.WriteAttributeString("min", null, Min.ToString());
			writer.WriteAttributeString("max", null, Max.ToString());
			writer.WriteAttributeString("count", null, Count.ToString());

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