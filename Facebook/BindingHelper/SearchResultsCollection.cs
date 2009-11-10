using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.BindingHelper
{
#if !SILVERLIGHT

    /// <summary>
    /// Data class used to hold the Search Results for XAML Databbinding
    /// </summary>
    public class SearchResult : IEquatable<SearchResult>
    {
    #region IEquatable<SearchResult> Members

        /// <summary>
        /// Overriden operator to compare two SearchResults
        /// </summary>
        public bool Equals(SearchResult other)
        {
            return (other != null && other.Equals(this));
        }

        #endregion
    };

    /// <summary>
    /// Collection of SearchResult objects
    /// </summary>
    public class SearchResultsCollection : FacebookDataCollection<SearchResult>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SearchResultsCollection(IList<SearchResult> results, string searchText)
            : base(results)
        {
            SearchText = searchText;
        }

        /// <summary>
        /// the text to search with
        /// </summary>
        public string SearchText { get; private set; }


    }
#endif
}
