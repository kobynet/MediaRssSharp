using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace MediaRss.Primary
{
	public class MediaRssItem : SyndicationItem, IMrssPrimaryElement
	{
		#region private fields

		#endregion

		#region constructor
		public MediaRssItem()
		{
			Init();
		}

		public MediaRssItem(string title, string content, Uri itemAlternateLink, string id, DateTimeOffset lastUpdatedTime) :
			base(title, content, itemAlternateLink, id, lastUpdatedTime)
		{
			Init();
		}

		private void Init()
		{
			ContentNodes = new List<MediaRssContent>();
			GroupNodes = new List<MediaRssGroup>();
			OptionalElements = new MrssOptionalElements();
		}

		#endregion

		#region public properties

		#region primary elements

		public List<MediaRssContent> ContentNodes { get; set; }
		public List<MediaRssGroup> GroupNodes { get; set; }

		#endregion

		#region optional parameters
		public MrssOptionalElements OptionalElements { get; set; }

		#endregion

		#endregion

		protected override bool TryParseElement(XmlReader reader, string version)
		{
			var parsed = false;
			try
			{
				if (version == SyndicationVersions.Rss20 && reader.NamespaceURI == MediaRssBase.NS_URI)
				{
					switch (reader.LocalName)
					{
						case MediaRssContent.ELEMENT_NAME:
							var contentNode = new MediaRssContent();
							contentNode.ReadXml(reader);
							ContentNodes.Add(contentNode);
							parsed = true;
							break;
						case MediaRssGroup.ELEMENT_NAME:
							var groupNode = new MediaRssGroup();
							groupNode.ReadXml(reader);
							GroupNodes.Add(groupNode);
							parsed = true;
							break;
						default:
							parsed = MediaRssHelper.TryParseOptionalElement(OptionalElements, reader);
							if (!parsed)
								parsed = base.TryParseElement(reader, version);
							break;
					}
				}
				return parsed;
			}
			catch (Exception)
			{
				//TODO Log error
				return false;
			}
		}


		protected override void WriteElementExtensions(XmlWriter writer, string version)
		{
			if (version == SyndicationVersions.Rss20)
			{
				foreach (var contentNode in ContentNodes)
				{
					writer.WriteStartElement(MediaRssBase.PREFIX, Primary.MediaRssContent.ELEMENT_NAME, MediaRssBase.NS_URI);
					contentNode.WriteXml(writer);
					writer.WriteEndElement();
				}
				MediaRssHelper.WriteOptionalElements(OptionalElements, writer);
			}
			base.WriteElementExtensions(writer, version);
		}
	}
}