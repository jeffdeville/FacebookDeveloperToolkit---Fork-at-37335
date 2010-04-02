using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Facebook.Schema;
using Facebook.Session;
using Facebook.Utility;

namespace Facebook.Rest
{
    /// <summary>
    /// Facebook Marketplace API methods.
    /// </summary>
    public class Marketplace : BaseAuthenticatedService, Facebook.Rest.IMarketplace
    {
        #region Methods

        #region Constructor

        /// <summary>
        /// Public constructor for facebook.Marketplace
        /// </summary>
        /// <param name="session">Needs a connected Facebook Session object for making requests</param>
        public Marketplace(IFacebookNetworkWrapper networkWrapper, IFacebookSession session)
            : base(networkWrapper, session)
        {
        }

        #endregion Constructor

        #region Public Methods

#if !SILVERLIGHT

        #region Synchronous Methods

        /// <summary>
        /// This method is deprecated. Returns all the Marketplace categories.
        /// </summary>
        /// <returns>This method returns a list of categories for use in Marketplace.</returns>
        [Obsolete("facebook deprecated")]
        public IList<string> GetCategories()
        {
            return GetCategories(false, null, null);
        }

        /// <summary>
        /// This method is deprecated. Returns the Marketplace subcategories for a particular category.
        /// </summary>
        /// <param name="category">Filter by category. If this is not a valid category, no subcategories get returned.</param>
        /// <returns>A list of subcategories for use in Marketplace. </returns>
        [Obsolete("facebook deprecated")]
        public IList<string> GetSubCategories(string category)
        {
            return GetSubCategories(category, false, null, null);
        }

        /// <summary>
        /// This method is deprecated. Return all Marketplace listings either by listing ID or by user.
        /// </summary>
        /// /// <param name="uids">Filter by a list of users. If you leave this blank, then the list is filtered only for listing IDs.</param>
        /// <param name="listing_ids">Filter by listing IDs. If you leave this blank, then the list is filtered only for user IDs.</param>
        /// <returns>This method returns all visible listings matching the criteria given. If no matching listings are found, the method returns an empty element.</returns>
        [Obsolete("facebook deprecated")]
        public IList<listing> GetListings(List<long> listing_ids, Collection<string> uids)
        {
            return GetListings(listing_ids, uids, false, null, null);
        }

        /// <summary>
        /// This method is deprecated. Search Marketplace for listings filtering by category, subcategory and a query string.
        /// </summary>
        /// <param name="category">Optional - the category to restirct search to, as returned by getCategories.</param>
        /// <param name="subcategory">Optional - the subcategory to restrict search to, as returned by getSubcategories. If a subcategory is provided, a category is also necessary.</param>
        /// <param name="query">Optional - the textual query to search the listings data.</param>
        /// <returns>Marketplace listings.</returns>
        [Obsolete("facebook deprecated")]
        public IList<listing> Search(string category, string subcategory, string query)
        {
            return Search(category, subcategory, query, false, null, null);
        }

        /// <summary>
        /// This method is deprecated. Create or modify a listing in Marketplace.
        /// </summary>
        /// <param name="listing_id">The listing ID to modify, or 0 if the user is creating a new listing.</param>
        /// <param name="show_on_profile">A privacy control indicating whether to display the listing on the poster's profile.</param>
        /// <param name="listing_attrs">Collection of Marketplace Listing Attributes.</param>
        /// <returns>This method returns the listing ID of the modified/created listing. If you are modifying a listing, it is the same as the listing ID provided to the method.</returns>
        [Obsolete("facebook deprecated")]
        public long CreateListing(long listing_id, bool show_on_profile, Dictionary<string, string> listing_attrs)
        {
            return CreateListing(listing_id, show_on_profile, listing_attrs, false, null, null);
        }

        /// <summary>
        /// This method is deprecated. Remove a listing from Marketplace.
        /// </summary>
        /// <param name="listing_id">The listing ID to remove.</param>
        /// <param name="status">Removal status for tracking whether a Marketplace listing resulted in a successful transaction: "SUCCESS", "NOT_SUCCESS", or "DEFAULT".</param>
        /// <returns>True on success, error on failure. </returns>
        /// <remarks>The listing must be owned by loggedinuser.</remarks>
        [Obsolete("facebook deprecated")]
        public bool RemoveListing(long listing_id, string status)
        {
            return RemoveListing(listing_id, status, false, null, null);
        }

        #endregion Synchronous Methods

#endif

        #endregion Public Methods
        
        #region Private Methods
        
        [Obsolete("facebook deprecated")]
        private IList<string> GetCategories(bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.getCategories" } };

            if (isAsync)
            {
                return null;
            }

            return SendRequest<marketplace_getCategories_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey)).marketplace_category;
        }

        [Obsolete("facebook deprecated")]
        private IList<string> GetSubCategories(string category, bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.getSubCategories" } };
            Utilities.AddRequiredParameter(parameterList, "category", category);

            if (isAsync)
            {
                return null;
            }

            return SendRequest<marketplace_getSubCategories_response>(parameterList, !string.IsNullOrEmpty(Session.SessionKey)).marketplace_subcategory;
        }

        [Obsolete("facebook deprecated")]
        private IList<listing> GetListings(List<long> listing_ids, Collection<string> uids, bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.getListings" } };
            Utilities.AddList(parameterList, "listing_ids", listing_ids);
            Utilities.AddCollection(parameterList, "uids", uids);

            if (isAsync)
            {
                return null;
            }

            return SendRequest<marketplace_getListings_response>(parameterList).listing;
        }

        [Obsolete("facebook deprecated")]
        private IList<listing> Search(string category, string subcategory, string query, bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.search" } };
            Utilities.AddOptionalParameter(parameterList, "category", category);
            Utilities.AddOptionalParameter(parameterList, "subcategory", subcategory);
            Utilities.AddOptionalParameter(parameterList, "query", query);

            if (isAsync)
            {
                return null;
            }

            return SendRequest<marketplace_search_response>(parameterList).listing;
        }

        [Obsolete("facebook deprecated")]
        private long CreateListing(long listing_id, bool show_on_profile, Dictionary<string, string> listing_attrs,
            bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.createListing" } };
            Utilities.AddRequiredParameter(parameterList, "listing_id", listing_id);
            Utilities.AddParameter(parameterList, "subcategory", show_on_profile);
            Utilities.AddJSONAssociativeArray(parameterList, "listing_attrs", listing_attrs);

            if (isAsync)
            {
                return 0;
            }

            return SendRequest<marketplace_createListing_response>(parameterList).TypedValue;
        }

        [Obsolete("facebook deprecated")]
        private bool RemoveListing(long listing_id, string status, bool isAsync, Video.GetUploadLimitsCallback callback, Object state)
        {
            var parameterList = new Dictionary<string, string> { { "method", "facebook.marketplace.removeListing" } };
            Utilities.AddRequiredParameter(parameterList, "listing_id", listing_id);
            Utilities.AddRequiredParameter(parameterList, "status", status);

            if (isAsync)
            {
                return true;
            }

            return SendRequest<marketplace_removeListing_response>(parameterList).TypedValue;
        }

        #endregion Private Methods

        #endregion Methods
    }
}