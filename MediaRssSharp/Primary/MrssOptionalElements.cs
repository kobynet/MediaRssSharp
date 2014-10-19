using System.Collections.Generic;

namespace MediaRss.Primary
{
	public class MrssOptionalElements
	{
		public Community CommunityNode { get; set; }
		public Comments CommentsNode { get; set; }
		public Responses ResponsesNode { get; set; }
		public Backlinks BacklinksNode { get; set; }
		public List<Embed> EmbedNodes { get; set; }
		public SubTitle SubTitleNode { get; set; }
		public PeerLink PeerLinkNode { get; set; }
		public License LicenseNode { get; set; }
		public Price PriceNode { get; set; }
		public Restriction RestrictionNode { get; set; }
		public Status StatusNode { get; set; }
		public Scenes ScenesNode { get; set; }
		public Description DescriptionNode { get; set; }
	}
}
