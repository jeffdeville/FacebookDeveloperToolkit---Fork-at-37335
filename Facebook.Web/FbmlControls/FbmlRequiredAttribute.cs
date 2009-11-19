using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web
{
    /// <summary>
    /// Indicates that a property is required by FBML.  This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class FbmlRequiredAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new <see>FbmlRequiredAttribute</see>.
        /// </summary>
        public FbmlRequiredAttribute() { }

        /// <summary>
        /// Gets or sets whether the property is required by FBML.
        /// </summary>
        public bool IsRequired { get; set; }
    }
}
