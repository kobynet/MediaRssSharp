﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "subTitle", Namespace = "http://search.yahoo.com/mrss/")]
	public class SubTitle : MediaRssBase, IXmlSerializable
	{
		internal const String ELEMENT_NAME = "subTitle";
	
		public String Type { get; set; }
		public String Lang { get; set; }
		public Uri Href{ get; set; }

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
						else if (reader.LocalName == "lang")
						{
							Lang = reader.Value;
						}
						else if (reader.LocalName == "href")
						{
							Href = new Uri(reader.Value);
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

		public override String ToString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("Type: {0}\n", Type);
			builder.AppendFormat("Lang: {0}\n", Lang);
			builder.AppendFormat("Href: {0}\n", Href);
			return builder.ToString();
		}


		public void WriteXml(XmlWriter writer)
		{
			if (Type != null)
			{
				writer.WriteAttributeString("type", null, Type);
			}
			if (Type != null)
			{
				writer.WriteAttributeString("lang", null, Lang);
			}
			if (Type != null)
			{
				writer.WriteAttributeString("Href", null, Href.ToString());
			}
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}
		}
		#endregion
	}
}