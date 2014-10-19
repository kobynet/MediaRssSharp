using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using MediaRss.Primary;

namespace MediaRss
{
	public class MediaRssFeed : SyndicationFeed
	{
		protected override SyndicationItem CreateItem()
		{
			return new MediaRssItem();
		}
	}

	public static class MediaRssExtensions
	{
		public static IEnumerable<MediaRssItem> AsMediaRssItems(this IEnumerable<SyndicationItem> items)
		{
			return items.Cast<MediaRssItem>();

		}

		public static MediaRssItem AsMediaRssItem(this SyndicationItem item)
		{
			return item as MediaRssItem;
		}
	}
}