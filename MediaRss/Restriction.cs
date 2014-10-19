using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "restriction", Namespace = "http://search.yahoo.com/mrss/")]
	public class Restriction : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "restriction";
	
		public String Type { get; set; }
		public String Relationship { get; set; }

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
						else if (reader.LocalName == "relationship")
						{
							Relationship = reader.Value;
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
			builder.AppendFormat("Relationship: {0}\n", Relationship);

			if (ElementExtensions.Count > 0)
			{
				foreach (XElement elt in ElementExtensions)
				{
					builder.AppendLine(elt.ToString());
				}
			}
			return builder.ToString();
		}


		public void WriteXml(XmlWriter writer)
		{
			if (Type != null)
			{
				writer.WriteAttributeString("type", null, Type);
			}
			if (Relationship != null)
			{
				writer.WriteAttributeString("relationship", null, Relationship);
			}
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}
}