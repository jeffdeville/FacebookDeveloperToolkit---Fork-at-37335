using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Schema;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Represents a collection of photo tags
    /// </summary>
    public sealed class FacebookPhotoTagCollection : FacebookDataCollection<FacebookPhotoTag>
    {
                /// <summary>
        /// Initializes FacebookContactCollection object
        /// </summary>
        public FacebookPhotoTagCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes FacebookContactCollection from contacts list
        /// </summary>
        /// <param name="tags">list of tags</param>
        public FacebookPhotoTagCollection(IEnumerable<photo_tag> tags)
            : base(from t in tags select new FacebookPhotoTag(t))
        {
        }
    }
}
