using System;
namespace Facebook.Rest
{
	public interface IMarketplace : IRestBase
	{
		long CreateListing(long listing_id, bool show_on_profile, System.Collections.Generic.Dictionary<string, string> listing_attrs);
		System.Collections.Generic.IList<string> GetCategories();
		System.Collections.Generic.IList<Facebook.Schema.listing> GetListings(System.Collections.Generic.List<long> listing_ids, System.Collections.ObjectModel.Collection<string> uids);
		System.Collections.Generic.IList<string> GetSubCategories(string category);
		bool RemoveListing(long listing_id, string status);
		System.Collections.Generic.IList<Facebook.Schema.listing> Search(string category, string subcategory, string query);
	}
}
