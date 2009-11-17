using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using Facebook.Web;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content if the logged in user can see the specified what attribute of the specified user. 
    /// You can use these settings to provide content or links with relevance to Facebook privacy or to implement your own privacy using Facebook's current controls.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-can-see" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfCanSee runat=\"server\"></{0}:IfCanSee>")]
    [DefaultProperty("Uid")]
    public class IfCanSee : ConditionalControl
    {
        private UserProfilePrivacySetting _what = UserProfilePrivacySetting.Search;

        /// <summary>
        /// The user ID to check
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID to check")]
        [DefaultValue(0)]
        [FbmlRequired(IsRequired = true)]
        public long Uid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the privacy setting to check for the specified user.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">Thrown if the property is set to a value not defined by the <see>UserProfilePrivacySetting</see>
        /// enumeration.</exception>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies the privacy setting to check.")]
        [DefaultValue(UserProfilePrivacySetting.Search)]
        public UserProfilePrivacySetting CanSeeWhat
        {
            get { return _what; }
            set
            {
                if (!Enum.IsDefined(typeof(UserProfilePrivacySetting), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(UserProfilePrivacySetting));
                _what = value;
            }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Uid == 0)
                throw new MissingRequiredAttribute("Uid", Uid);
            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            if (CanSeeWhat != UserProfilePrivacySetting.Search)
            {
                if (CanSeeWhat == UserProfilePrivacySetting.NotLimited)
                    writer.AddAttribute("what", "not_limited");
                else
                    writer.AddAttribute("what", CanSeeWhat.ToString().ToLowerInvariant());
            }
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_CAN_SEE; }
        }
    }
}
