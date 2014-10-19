using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "tags", Namespace = "http://search.yahoo.com/mrss/")]
	public class Tags : MediaRssBase, IXmlSerializable
	{
		#region const
		internal const String ELEMENT_NAME = "tags";
		#endregion

		#region private fields
		private readonly Dictionary<String, Int32> tagCollection;
		#endregion

		#region public properties
		public Dictionary<String, Int32> TagCollection { get { return tagCollection; } }
		#endregion

		#region constructor
		public Tags()
		{
			tagCollection = new Dictionary<String, Int32>();
		}
		#endregion

		#region IXmlSerializable
		
		public void ReadXml(XmlReader reader)
		{
			Boolean isEmpty = reader.IsEmptyElement;

			AddAttributeExtensions(reader);

			reader.ReadStartElement();
			if (!isEmpty)
			{
				//Read the text content of the element
				String rawValue = reader.ReadContentAsString();
				if (!String.IsNullOrEmpty(rawValue))
				{
					//Split the raw string on commas to get the individual items
					String[] unSplitItems = rawValue.Split(',');
					foreach (var unSplitItem in unSplitItems)
					{
						//Split the items on the colon to seperate the tag from the weight
						String[] splitItem = unSplitItem.Split(':');
						TagCollection.Add(splitItem[0].Trim(), Int32.Parse(splitItem[1].Trim()));
					}
				}
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement(PREFIX, ELEMENT_NAME, NS_URI);

			//Write out any addtional attributes
			foreach (KeyValuePair<XmlQualifiedName, string> kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}
		
			if (TagCollection != null && TagCollection.Count > 0)
			{
				var rawContent = new StringBuilder();
				foreach (var tag in TagCollection)
				{
					rawContent.Append(String.Format("{0}: {1},", tag.Key, tag.Value));
				}

				if (rawContent.Length > 0)
				{
					writer.WriteString(rawContent.ToString().Remove(rawContent.Length -1));
				}
			}

			writer.WriteEndElement();
		}
		#endregion
	}
}