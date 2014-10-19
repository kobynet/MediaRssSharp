using System;
using System.Collections.Generic;
using System.Xml;

namespace MediaRss.Primary
{
	class MediaRssHelper
	{
		public static bool TryParseOptionalElement(MrssOptionalElements optionalElements, XmlReader reader)
		{
			var parsed = false;

			try
			{
				switch (reader.LocalName)
				{
					case Community.ELEMENT_NAME:
						optionalElements.CommunityNode = new Community();
						optionalElements.CommunityNode.ReadXml(reader);
						parsed = true;
						break;
					case Comments.ELEMENT_NAME:
						optionalElements.CommentsNode = new Comments();
						optionalElements.CommentsNode.ReadXml(reader);
						parsed = true;
						break;
					case Responses.ELEMENT_NAME:
						optionalElements.ResponsesNode = new Responses();
						optionalElements.ResponsesNode.ReadXml(reader);
						parsed = true;
						break;
					case Backlinks.ELEMENT_NAME:
						optionalElements.BacklinksNode = new Backlinks();
						optionalElements.BacklinksNode.ReadXml(reader);
						parsed = true;
						break;
					case Embed.ELEMENT_NAME:
						var embedNode = new Embed();
						embedNode.ReadXml(reader);
						if (optionalElements.EmbedNodes == null)
							optionalElements.EmbedNodes = new List<Embed>();
						optionalElements.EmbedNodes.Add(embedNode);
						parsed = true;
						break;
					case Price.ELEMENT_NAME:
						optionalElements.PriceNode = new Price();
						optionalElements.PriceNode.ReadXml(reader);
						parsed = true;
						break;
					case License.ELEMENT_NAME:
						optionalElements.LicenseNode = new License();
						optionalElements.LicenseNode.ReadXml(reader);
						parsed = true;
						break;
					case PeerLink.ELEMENT_NAME:
						optionalElements.PeerLinkNode = new PeerLink();
						optionalElements.PeerLinkNode.ReadXml(reader);
						parsed = true;
						break;
					case SubTitle.ELEMENT_NAME:
						optionalElements.SubTitleNode = new SubTitle();
						optionalElements.SubTitleNode.ReadXml(reader);
						parsed = true;
						break;
					case Restriction.ELEMENT_NAME:
						optionalElements.RestrictionNode = new Restriction();
						optionalElements.RestrictionNode.ReadXml(reader);
						parsed = true;
						break;
					case Status.ELEMENT_NAME:
						optionalElements.StatusNode = new Status();
						optionalElements.StatusNode.ReadXml(reader);
						parsed = true;
						break;
					case Scenes.ELEMENT_NAME:
						optionalElements.ScenesNode = new Scenes();
						optionalElements.ScenesNode.ReadXml(reader);
						parsed = true;
						break;
					case Description.ELEMENT_NAME:
						optionalElements.DescriptionNode = new Description();
						optionalElements.DescriptionNode.ReadXml(reader);
						parsed = true;
						break;
				}

				return parsed;
			}
			catch (Exception)
			{
				//TODO Log error
				return false;
			}
		}

		public static void WriteOptionalElements(MrssOptionalElements optionalElements, XmlWriter writer)
		{
			if (optionalElements.CommunityNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Community.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.CommunityNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.CommentsNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Comments.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.CommentsNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.ResponsesNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Responses.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.ResponsesNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.BacklinksNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Backlinks.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.BacklinksNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.EmbedNodes != null)
			{
				foreach (var embedNode in optionalElements.EmbedNodes)
				{
					writer.WriteStartElement(MediaRssBase.PREFIX, Embed.ELEMENT_NAME, MediaRssBase.NS_URI);
					embedNode.WriteXml(writer);
					writer.WriteEndElement();
				}
			}
			if (optionalElements.SubTitleNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, SubTitle.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.SubTitleNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.PeerLinkNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, PeerLink.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.PeerLinkNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.LicenseNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, License.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.LicenseNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.PriceNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Price.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.PriceNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.RestrictionNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Restriction.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.RestrictionNode.WriteXml(writer);
				writer.WriteEndElement();
			}
			if (optionalElements.StatusNode != null)
			{
				writer.WriteStartElement(MediaRssBase.PREFIX, Status.ELEMENT_NAME, MediaRssBase.NS_URI);
				optionalElements.StatusNode.WriteXml(writer);
				writer.WriteEndElement();
			}
		}
	}
}
