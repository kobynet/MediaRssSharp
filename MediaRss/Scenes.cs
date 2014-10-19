using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "scenes", Namespace = "http://search.yahoo.com/mrss/")]
	public class Scenes : MediaRssBase, IXmlSerializable
	{
		internal const string ELEMENT_NAME = "scenes";

		//public List<Scene> SceneItems { get; set; }
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
					ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
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

			foreach (XElement element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}

	[XmlRoot(ElementName = "scene", Namespace = "http://search.yahoo.com/mrss/")]
	public class Scene : MediaRssBase, IXmlSerializable
	{

		public void ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		public void WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}