using System.Collections.Generic;
using System.Runtime.Serialization;
using Facebook.BindingHelper;
using Facebook.Schema;

namespace Facebook.Utility
{
    /// <summary>
    /// Contains Stream information  
    /// </summary>
    [DataContract]
    public class FacebookStreamData
    {
        /// <summary>
        /// Contains all posts in the stream
        /// </summary>
        [DataMember(Name = "posts")]
        public FacebookStreamPostCollection Posts
        {
            get;
            set;
        }

        /// <summary>
        /// Contains profile information for all users in the stream
        /// </summary>
        [DataMember(Name = "profiles")]
        public FacebookProfileCollection Profiles
        {
            get;
            set;
        }

        /// <summary>
        /// Contains albums information for all albums in the stream
        /// </summary>
        [DataMember(Name = "albums")]
        public IList<album> Albums
        {
            get;
            set;
        }

        /// <summary>
        /// Method called after deserialization of the class completes.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Dictionary<string, FacebookProfile> profileCache = new Dictionary<string, FacebookProfile>();

            foreach (FacebookProfile p in Profiles)
            {
                if (!profileCache.ContainsKey(p.name))
                {
                    profileCache.Add(p.name, p);
                }
            }

            foreach (FacebookStreamPost post in Posts)
            {
                if (profileCache.ContainsKey(post.ActorProfile.name))
                {
                    post.ActorProfile = profileCache[post.ActorProfile.name];
                }

                if (post.StreamComments.Comments != null)
                {
                    foreach (FacebookComment comment in post.StreamComments.Comments)
                    {
                        if (profileCache.ContainsKey(comment.FromUser.name))
                        {
                            comment.FromUser = profileCache[comment.FromUser.name];
                        }
                    }
                }
            }
        }

    }

}
