using System;
using System.Globalization;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace MediaRss.Formatter
{
	public class MediaRssFeedFormatter : Rss20FeedFormatter
	{
		private static readonly XmlQualifiedName Rss20Domain;
		
		static MediaRssFeedFormatter()
		{
			Rss20Domain = new XmlQualifiedName("domain", string.Empty);
		}

		public MediaRssFeedFormatter(): base()
		{
		}

		public MediaRssFeedFormatter(MediaRssFeed feed): base(feed)
		{
		}

		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartElement("rss", "");
			writer.WriteAttributeString("xmlns", MediaRssBase.PREFIX, null, MediaRssBase.NS_URI);
			WriteFeed(writer);
			writer.WriteEndElement();
		}

		#region Writer Helper Methods
		private static void WriteAlternateLink(XmlWriter writer, SyndicationLink link, Uri baseUri)
		{
			writer.WriteStartElement("link", "");
			Uri baseUriToWrite = GetBaseUriToWrite(baseUri, link.BaseUri);
			if (baseUriToWrite != null)
			{
				writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", GetUriString(baseUriToWrite));
			}
			writer.WriteString(GetUriString(link.Uri));
			writer.WriteEndElement();
		}

		internal static Uri GetBaseUriToWrite(Uri rootBase, Uri currentBase)
		{
			if ((rootBase == currentBase) || (currentBase == null))
			{
				return null;
			}
			if ((rootBase != null) && ((rootBase.IsAbsoluteUri && currentBase.IsAbsoluteUri) && rootBase.IsBaseOf(currentBase)))
			{
				return rootBase.MakeRelativeUri(currentBase);
			}
			return currentBase;
		}

		internal static string GetUriString(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsoluteUri;
			}
			return uri.ToString();
		}

		private void WritePerson(XmlWriter writer, string elementTag, SyndicationPerson person)
		{
			writer.WriteStartElement(elementTag, "");
			SyndicationFeedFormatter.WriteAttributeExtensions(writer, person, this.Version);
			writer.WriteString(person.Email);
			writer.WriteEndElement();
		}

		private static string DateTimeAsString(DateTimeOffset dateTime)
		{
			StringBuilder builder = new StringBuilder(dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture));
			builder.Remove(builder.Length - 3, 1);
			return builder.ToString();
		}

		private void WriteCategory(XmlWriter writer, SyndicationCategory category)
		{
			if (category != null)
			{
				writer.WriteStartElement("category", "");
				SyndicationFeedFormatter.WriteAttributeExtensions(writer, category, this.Version);
				if (!string.IsNullOrEmpty(category.Scheme) && !category.AttributeExtensions.ContainsKey(Rss20Domain))
				{
					writer.WriteAttributeString("domain", "", category.Scheme);
				}
				writer.WriteString(category.Name);
				writer.WriteEndElement();
			}
		} 
		#endregion

		private void WriteFeed(XmlWriter writer)
		{
			if (Feed == null)
			{
				throw new InvalidOperationException("FeedFormatter Does Not Have Feed");
			}

			writer.WriteAttributeString("version", "2.0");
			writer.WriteStartElement("channel", "");

			SyndicationFeedFormatter.WriteAttributeExtensions(writer, base.Feed, this.Version);
			string str = (base.Feed.Title != null) ? base.Feed.Title.Text : string.Empty;
			writer.WriteElementString("title", "", str);
			SyndicationLink link = null;
			for (int i = 0; i < base.Feed.Links.Count; i++)
			{
				if (base.Feed.Links[i].RelationshipType == "alternate")
				{
					link = base.Feed.Links[i];
					WriteAlternateLink(writer, link, base.Feed.BaseUri);
					break;
				}
			}
			string str2 = (base.Feed.Description != null) ? base.Feed.Description.Text : string.Empty;
			writer.WriteElementString("description", "", str2);
			if (base.Feed.Language != null)
			{
				writer.WriteElementString("language", base.Feed.Language);
			}
			if (base.Feed.Copyright != null)
			{
				writer.WriteElementString("copyright", "", base.Feed.Copyright.Text);
			}
			if ((base.Feed.Authors.Count == 1) && (base.Feed.Authors[0].Email != null))
			{
				this.WritePerson(writer, "managingEditor", base.Feed.Authors[0]);
			}
			if (base.Feed.LastUpdatedTime > DateTimeOffset.MinValue)
			{
				writer.WriteStartElement("lastBuildDate");
				writer.WriteString(DateTimeAsString(base.Feed.LastUpdatedTime));
				writer.WriteEndElement();
			}

			for (int j = 0; j < base.Feed.Categories.Count; j++)
			{
				this.WriteCategory(writer, base.Feed.Categories[j]);
			}
			if (!string.IsNullOrEmpty(base.Feed.Generator))
			{
				writer.WriteElementString("generator", base.Feed.Generator);
			}
			
			if (base.Feed.ImageUrl != null)
			{
				writer.WriteStartElement("image");
				writer.WriteElementString("url", GetUriString(base.Feed.ImageUrl));
				writer.WriteElementString("title", "", str);
				string str3 = (link != null) ? GetUriString(link.Uri) : string.Empty;
				writer.WriteElementString("link", "", str3);
				writer.WriteEndElement();
			}
			
			SyndicationFeedFormatter.WriteElementExtensions(writer, base.Feed, this.Version);
			this.WriteItems(writer, base.Feed.Items, base.Feed.BaseUri);
			writer.WriteEndElement();
		}

		protected override SyndicationFeed CreateFeedInstance()
		{
			return new MediaRssFeed();
		}
	}
}