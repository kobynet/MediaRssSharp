using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MediaRss
{
	[XmlRoot(ElementName = "embed", Namespace = "http://search.yahoo.com/mrss/")]
	public class Embed : MediaRssBase, IXmlSerializable
	{
		private int _width;
		private int _height;
		private Uri _url;
		internal const string ELEMENT_NAME = "embed";

		public Uri Url
		{
			get { return _url; }
			set { _url = value; }
		}

		public Int32 Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public Int32 Height
		{
			get { return _height; }
			set { _height = value; }
		}

		public Collection<Parameter> ParameterItems { get; set; }
		
		public Embed()
		{
			ParameterItems = new Collection<Parameter>();
		}

		#region IXmlSerializable Members

		public void ReadXml(XmlReader reader)
		{
			var isEmpty = reader.IsEmptyElement;

			if (reader.HasAttributes)
			{
				for (var i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToNextAttribute();

					if (reader.NamespaceURI == "")
					{
						switch (reader.LocalName)
						{
							case "url":
								Uri.TryCreate(reader.Value, UriKind.Absolute, out _url);
								break;
							case "width":
								Int32.TryParse(reader.Value, out _width);
								break;
							case "height":
								Int32.TryParse(reader.Value, out _height);
								break;
							default:
								AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								break;
						}
					}
				}
			}

			reader.ReadStartElement();

			if (!isEmpty)
			{
				while (reader.IsStartElement())
				{
					switch (reader.LocalName)
					{
						case Parameter.ELEMENT_NAME:
							var param = new Parameter();
							param.ReadXml(reader);
							ParameterItems.Add(param);
							break;
						default:
							ElementExtensions.Add((XElement)XNode.ReadFrom(reader));
							break;
					} 
				}
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			if (Url != null)
			{
				writer.WriteAttributeString("url", null, Url.ToString());
			}

			writer.WriteAttributeString("width", null, Width.ToString());
			writer.WriteAttributeString("height", null, Height.ToString());

			if (ParameterItems != null && ParameterItems.Count > 0)
			{
				foreach(var param in ParameterItems)
				{
					param.WriteXml(writer);
				}
			}

			foreach (var kvp in AttributeExtensions)
			{
				writer.WriteAttributeString(kvp.Key.Name, kvp.Key.Namespace, kvp.Value);
			}

			foreach (var element in ElementExtensions)
			{
				element.WriteTo(writer);
			}
		}
		#endregion
	}
}