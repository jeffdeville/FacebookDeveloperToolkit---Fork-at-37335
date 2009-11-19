using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Lets you tailor the enclosed content to display to specific ages, locations, or content types. 
    /// You should use this tag with fb:else so you can provide alternate content in case the viewing user doesn't meet the demographic requirements. Also, if the user hides their age for privacy reasons, Facebook cannot determine whether or not the content is visible to the user, and thus it cannot be displayed. In this case, your fb:else clause should contain content that any user who doesn't necessarily meet these restrictions could see. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:restricted-to" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:RestrictedTo runat=\"server\" />")]
    public class RestrictedTo : ConditionalControl
    {
        /// <summary>
        /// The valid age or age range that can see the content. To specify multiple age ranges, separate each entry with a comma. Every specified age must be an integer. You can use plus (+) and minus (-) to restrict content to that age or older/younger (like 21+ for 21 and older or 18- for younger than 19). You can also specify a range of ages, like 18-35 so anyone between the ages of 18 and 35 (inclusive) can see the content. You can also specify multiple age ranges (like 19-,30+ -- if you want to exclude people in their 20s, for example). Every specified age must be an integer, and only the integer part of a user's age is taken into account. So for example, a user who is 17.9 years old will be treated as 17, and "18-35" includes anyone 18 or older but under 36. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The valid age or age range that can see the content. To specify multiple age ranges, separate each entry with a comma. Every specified age must be an integer. You can use plus (+) and minus (-) to restrict content to that age or older/younger (like 21+ for 21 and older or 18- for younger than 19). You can also specify a range of ages, like 18-35 so anyone between the ages of 18 and 35 (inclusive) can see the content. You can also specify multiple age ranges (like 19-,30+ -- if you want to exclude people in their 20s, for example). Every specified age must be an integer, and only the integer part of a user's age is taken into account. So for example, a user who is 17.9 years old will be treated as 17, and \"18-35\" includes anyone 18 or older but under 36. ")]
        public string Age
        {
            get;
            set;
        }
        /// <summary>
        /// The type of content being filtered. You can specify alcohol at this time. Before rendering your FBML, we run a check against the type on Facebook, and if the user meets the age and location requirements, we'll render the content.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The type of content being filtered. You can specify alcohol at this time. Before rendering your FBML, we run a check against the type on Facebook, and if the user meets the age and location requirements, we'll render the content.")]
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// The country or countries that can see the content. To specify multiple countries, separate each entry with a comma. When specifying the location, specify the country or countries (using a comma-separated list) from the ISO 3166 alpha 2 code list. This list is not necessarily the same as the IANA ccTLD (country code top level domain) list. For example, the ISO 3166 entry for England is .gb, while the IANA entry is .uk. 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The country or countries that can see the content. To specify multiple countries, separate each entry with a comma. When specifying the location, specify the country or countries (using a comma-separated list) from the ISO 3166 alpha 2 code list. This list is not necessarily the same as the IANA ccTLD (country code top level domain) list. For example, the ISO 3166 entry for England is .gb, while the IANA entry is .uk. ")]
        public string Location
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Age))
                writer.AddAttribute("age", Age.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(Type))
                writer.AddAttribute("type", Type.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(Location))
                writer.AddAttribute("location", Location.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_RESTRICTED_TO; }
        }
    }
}
