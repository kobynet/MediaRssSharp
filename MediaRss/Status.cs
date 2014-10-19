using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "status", Namespace = "http://search.yahoo.com/mrss/")]
	public class Status : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "status";
		
		public String State { get; set; }
		
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
						if (reader.LocalName == "state")
						{
							State = reader.Value;
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
			builder.AppendLine("MediaStatusItem:");
			builder.AppendFormat("State: {0}\n", State);
		
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
			if (State != null)
			{
				writer.WriteAttributeString("state", null, State);
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