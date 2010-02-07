using System;

namespace Facebook.Winforms.Forms
{
    /// <summary>
    /// This class is only needed because the ToString method in the original
    /// URI class prints Unicode characters instead of their http-escaped
    /// versions. Since the .Net Compact Framework can only support the URI
    /// constructor for the WebBrowser control, we have to use this new URI
    /// class. 
    /// </summary>
    public class UnicodeUri : Uri
    {
        /// <summary> 
        /// Constructor
        /// </summary>
        public UnicodeUri(string url)
            : base(url)
        {
        }

        /// <summary> 
        /// Overridden to provide a specific version of the url
        /// </summary>
        public override string ToString()
        {
            return Host + PathAndQuery;
        }
    }
}