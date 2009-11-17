using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Facebook.BindingHelper
{
    /// <summary>
    /// Defines a collection of ActivityPost object
    /// </summary>
    public class ActivityPostCollection : FacebookDataCollection<ActivityPost>
    {

        internal ActivityPostCollection(IEnumerable<ActivityPost> posts)
            : base(posts)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityPostCollection()
            : base()
        {
        }


        /// <summary>
        /// Accessor to find items using a given key
        /// </summary>
        public ActivityPost this[string key]
        {
            get
            {
                foreach (var post in this)
                {
                    if (post.PostId == key)
                        return post;
                }
                return null;

            }
        }

    }
}
