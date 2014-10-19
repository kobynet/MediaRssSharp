using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "comments", Namespace = "http://search.yahoo.com/mrss/")]
	public class Comments : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "comments";
		
		public List<Comment> CommentItems { get; set; }
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
					if (reader.LocalName == Comment.ELEMENT_NAME)
					{
						if (CommentItems == null)
						{
							CommentItems = new List<Comment>();
						}
						var comment = new Comment();
						comment.ReadXml(reader);

						CommentItems.Add(comment);
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

			if (CommentItems != null && CommentItems.Count > 0)
			{
				foreach (var comment in CommentItems)
				{
					comment.WriteXml(writer);
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