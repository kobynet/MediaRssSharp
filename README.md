MediaRssSharp
=============

Media Rss parser according to http://www.rssboard.org/media-rss

This parser is a fork of Foovanadil [MediaRss](http://mediarss.codeplex.com) alpha release.

The project is based on the syndication model object in .NET and is a set of libraries to read/write Media Rss files.

```
Basic usage: 
var reader = XmlReader.Create("SampleMedia.rss");
var feed = SyndicationFeed.Load<MediaRssFeed>(reader);
```
The project currently supports most of the MediaRss objects/rules.
I will be more than happy to recieve pull requests/suggestions about this project.

Media RSS is specification originally developed by Yahoo for describing rich media content for us in their search engine. It has since been adopted by the community at large and has now been taken over by the Rss Advisory board http://www.rssboard.org/.

The current spec under review by the Rss Advisory board (and what this library is based off of) can be found here: version 1.5.1 http://www.rssboard.org/media-rss

Special thanks goes to Foovanadil for his great work creating MediaRss parser.
