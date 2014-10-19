using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
using MediaRss.Formatter;
using MediaRss.Primary;
using NUnit.Framework;

namespace MediaRss.Tests
{
	[TestFixture]
	public class MediaRssSerializationTests
	{

		private MediaRssFeed _feed;

		#region helper methods

		[SetUp]
		public void Init()
		{
			var reader = XmlReader.Create(@"SampleMedia.rss");
			_feed = SyndicationFeed.Load<MediaRssFeed>(reader);
		}

		private MediaRssItem GetFirstItemFromFeed()
		{
			//MediaRssFeed feed = LoadFeed();
			return _feed.Items.AsMediaRssItems().First();
		}

		[Test]
		public void LoadFeed()
		{
			Assert.NotNull(_feed);
			Assert.AreEqual("Song Site", _feed.Title.Text);
			Assert.AreEqual("Media RSS example with new fields added in v1.5.0", _feed.Description.Text);
			Assert.True(_feed.Items.Any());
		}
		#endregion

		#region Community Node
		[Test]
		public void CommunityNodeNotNull()
		{
			var firstItem = GetFirstItemFromFeed();

			Assert.NotNull(firstItem.OptionalElements.CommunityNode);
			Assert.NotNull(firstItem.OptionalElements.CommunityNode.Rating);
			Assert.NotNull(firstItem.OptionalElements.CommunityNode.Stats);
			Assert.NotNull(firstItem.OptionalElements.CommunityNode.TagItems);

			Assert.AreEqual(3.5, firstItem.OptionalElements.CommunityNode.Rating.Average);
		}

		[Test]
		public void CommunityNodeStarRating()
		{
			StarRating rating = GetFirstItemFromFeed().OptionalElements.CommunityNode.Rating;
			Assert.NotNull(rating);

			Assert.AreEqual(3.5, rating.Average);
			Assert.AreEqual(20, rating.Count);
			Assert.AreEqual(1, rating.Min);
			Assert.AreEqual(10, rating.Max);
		}

		[Test]
		public void CommunityNodeStatistics()
		{
			Statistics stats = GetFirstItemFromFeed().OptionalElements.CommunityNode.Stats;
			Assert.NotNull(stats);

			Assert.AreEqual(5, stats.Views);
			Assert.AreEqual(5, stats.Favorites);
		}

		[Test]
		public void CommunityNodeTags()
		{
			var tags = GetFirstItemFromFeed().OptionalElements.CommunityNode.TagItems;
			Assert.NotNull(tags);

			Assert.NotNull(tags.TagCollection);
			Assert.AreEqual(2, tags.TagCollection.Count());

			KeyValuePair<string, int> newsItem = tags.TagCollection.ElementAt(0);

			Assert.AreEqual("news", newsItem.Key);
			Assert.AreEqual(5, newsItem.Value);

			KeyValuePair<string, int> abcItem = tags.TagCollection.ElementAt(1);

			Assert.AreEqual("abc", abcItem.Key);
			Assert.AreEqual(3, abcItem.Value);
		}
		#endregion

		#region Comments Node
		[Test]
		public void CommentsNodeNotNull()
		{
			Comments comments = GetFirstItemFromFeed().OptionalElements.CommentsNode;

			Assert.NotNull(comments);
			Assert.NotNull(comments.CommentItems);
		}

		[Test]
		public void CommentsItems()
		{
			Comments comments = GetFirstItemFromFeed().OptionalElements.CommentsNode;

			Assert.AreEqual(2, comments.CommentItems.Count());

			Comment firstComment = comments.CommentItems.ElementAt(0);

			Assert.AreEqual("comment1", firstComment.CommentText);
			
			Comment secondComment = comments.CommentItems.ElementAt(1);
			Assert.AreEqual("comment2", secondComment.CommentText);
		}
		
		#endregion

		#region Embed Node
		[Test]
		public void EmbedNodeNotNull()
		{
			var embed  = GetFirstItemFromFeed().OptionalElements.EmbedNodes.First();

			Assert.NotNull(embed);
			Assert.NotNull(embed.ParameterItems);
		}

		[Test]
		public void EmbedAttributes()
		{
			var embed = GetFirstItemFromFeed().OptionalElements.EmbedNodes.First();

			Assert.AreEqual(@"http://www.foo.com/player.swf", embed.Url.AbsoluteUri);

			Assert.AreEqual(512, embed.Width);
			Assert.AreEqual(323, embed.Height);
		}

		[Test]
		public void EmbedParameterCollection()
		{
			Collection<Parameter> parameterItems = GetFirstItemFromFeed().OptionalElements.EmbedNodes.First().ParameterItems;
			Assert.AreEqual(5, parameterItems.Count());

			Parameter typeParam = parameterItems.ElementAt(0);
			Assert.AreEqual("type", typeParam.Name);
			Assert.AreEqual("application/x-shockwave-flash", typeParam.Content);

			Parameter widthParam = parameterItems.ElementAt(1);
			Assert.AreEqual("width", widthParam.Name);
			Assert.AreEqual("512", widthParam.Content);
			
			Parameter heightParam = parameterItems.ElementAt(2);
			Assert.AreEqual("height", heightParam.Name);
			Assert.AreEqual("323", heightParam.Content);
			
			Parameter allowFullScreenParam = parameterItems.ElementAt(3);
			Assert.AreEqual("allowFullScreen", allowFullScreenParam.Name);
			Assert.AreEqual("true", allowFullScreenParam.Content);
			
			Parameter flashVarsParam = parameterItems.ElementAt(4);
			Assert.AreEqual("flashVars", flashVarsParam.Name);
			Assert.AreEqual(@"id=12345&vid=678912i&lang=en-us&intl=us&thumbUrl=http://www.foo.com/thumbnail.jpg", flashVarsParam.Content);
		}

		[Test]
		public void GroupContentCountTest()
		{
			var firstITem = GetFirstItemFromFeed();
			Assert.AreEqual(5, firstITem.GroupNodes.First().ContentNodes.Count);
		}

		[Test]
		public void GroupDescriptionTest()
		{
			var firstITem = GetFirstItemFromFeed();
			Assert.AreEqual("This is test description",
				firstITem.GroupNodes.First().OptionalElements.DescriptionNode.DescriptionText);

		}

		#endregion

		[Test]
		public void SerializeInMemory()
		{
			var reader = XmlReader.Create("SampleMedia.rss");
			var formatter = new MediaRssFeedFormatter(new MediaRssFeed());
			formatter.ReadFrom(reader);

			var settings = new XmlWriterSettings
			{
				NewLineOnAttributes = true,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
				CloseOutput = true,
				Indent = true
			};

			var writer = XmlWriter.Create("TestOutput.rss", settings);
			formatter.WriteTo(writer);

			Debug.WriteLine(writer.ToString());
			writer.Close();
		}

		[Test]
		public void CreateNewRssFile()
		{
			var myFeed = new MediaRssFeed
			{
				Copyright = new TextSyndicationContent("Copyright 2010-2020 @ Because it looks good"),
				Description = new TextSyndicationContent("This is a sample media feed"),
				Title = new TextSyndicationContent("Hello Media Feed World!")
			};

			var feedItems = new List<MediaRssItem>();

			var item = new MediaRssItem();
			var rating = new StarRating {Min = 1, Max = 5};
			item.OptionalElements.CommunityNode = new Community { Rating = rating};

			feedItems.Add(item);

			myFeed.Items = feedItems;

			var settings = new XmlWriterSettings
			{
				NewLineOnAttributes = true,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
				CloseOutput = true,
				Indent = true
			};

			using (var writer = XmlWriter.Create("TestOutput.rss", settings))
			{
				var formatter = new MediaRssFeedFormatter(myFeed);
				formatter.WriteTo(writer);
			}
		}

	}
}
