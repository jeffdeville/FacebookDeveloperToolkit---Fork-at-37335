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
    /// Inserts appropriate Google Analytics code into a canvas page. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:google-analytics" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:GoogleAnalytics runat=\"server\" />")]
    [DefaultProperty("AccountID")]
    public class GoogleAnalytics : FbmlControl
    {
        /// <summary>
        /// Creates a new <see>GoogleAnalytics</see>.
        /// </summary>
        public GoogleAnalytics()
        {
            ClientInfoEnabled = true;
            DomainCookie = GoogleAnalyticsDomainCookieType.Auto;
            UniqueDomainHashEnabled = true;
            InactiveSessionTimeout = 1800;
            TrackerImagePath = "/__utm.gif";
            TransactionFieldSeparator = "|";
            FlashDetectionEnabled = true;
            TitleDetectionEnabled = true;
            TrackingCookiePath = "/";
            SamplePercentageTracked = 100;
            CampaignTrackingEnabled = true;
            _ucto = 15768000; // 6 months
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_GOOGLE_ANALYTICS; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(AccountID))
                throw new MissingRequiredAttribute("AccountID", AccountID);

            writer.AddAttribute("uacct", AccountID);
            if (!string.IsNullOrEmpty(PageName))
                writer.AddAttribute("page", PageName, true);
            if (!ClientInfoEnabled)
                writer.AddAttribute("ufsc", FbmlConstants.ZERO);
            if (_udn != GoogleAnalyticsDomainCookieType.Auto)
                writer.AddAttribute("udn", DomainCookie.ToString().ToLowerInvariant());
            if (!UniqueDomainHashEnabled)
                writer.AddAttribute("uhash", FbmlConstants.OFF);
            if (InactiveSessionTimeout != 1800)
                writer.AddAttribute("utimeout", _utimeout.ToString(CultureInfo.InvariantCulture));
            if (!string.Equals(TrackerImagePath, "/__utm.gif", StringComparison.Ordinal))
                writer.AddAttribute("ugifpath", TrackerImagePath, true);
            if (!string.Equals(TransactionFieldSeparator, "|", StringComparison.Ordinal))
                writer.AddAttribute("utsp", TransactionFieldSeparator, true);
            if (!FlashDetectionEnabled)
                writer.AddAttribute("uflash", FbmlConstants.ZERO);
            if (!TitleDetectionEnabled)
                writer.AddAttribute("utitle", FbmlConstants.ZERO);
            if (LinkerEnabled)
                writer.AddAttribute("ulink", FbmlConstants.ONE);
            if (CampaignAnchorsEnabled)
                writer.AddAttribute("uanchor", FbmlConstants.ONE);
            if (!string.Equals(TrackingCookiePath, "/", StringComparison.Ordinal))
                writer.AddAttribute("utcp", TrackingCookiePath, true);
            if (SamplePercentageTracked != 100)
                writer.AddAttribute("usample", SamplePercentageTracked.ToString(CultureInfo.InvariantCulture));
            if (!CampaignTrackingEnabled)
                writer.AddAttribute("uctm", FbmlConstants.ZERO);
            if (_ucto != 15768000)
                writer.AddAttribute("ucto", _ucto.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(CampaignName))
                writer.AddAttribute("uccn", CampaignName, true);
            if (_ucmd != GoogleAnalyticsCampaignMedium.Default)
                writer.AddAttribute("ucmd", _ucmd.ToString().ToLowerInvariant());
            if (!string.IsNullOrEmpty(CampaignSource))
                writer.AddAttribute("ucsr", CampaignSource, true);
            if (!string.IsNullOrEmpty(CampaignKeyword))
                writer.AddAttribute("uctr", CampaignKeyword, true);
            if (!string.IsNullOrEmpty(CampaignContent))
                writer.AddAttribute("ucct", CampaignContent, true);
            if (CampaignID != 0)
                writer.AddAttribute("ucid", CampaignID.ToString(CultureInfo.InvariantCulture));
            if (OverrideCampaign)
                writer.AddAttribute("ucno", "ut_override");

            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// Gets or sets the account ID used to track with Google Analytics.
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Description("Google Analytics account ID, such as \"UA-9999999-9\".")]
        [Category(FbmlConstants.REQUIRED)]
        [DefaultValue(null)]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the real or virtual page name passed to the <c>urchinTracker()</c> function.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The real or virtual page passed to the urchinTracker() function.")]
        [Category(FbmlConstants.OPTIONAL)]
        [DefaultValue(null)]
        public string PageName
        {
            get;
            set;
        }

        #region user settings
        /// <summary>
        /// Gets or sets whether client information is tracked.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether client information is tracked.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(true)]
        public bool ClientInfoEnabled
        {
            get;
            set;
        }

        private GoogleAnalyticsDomainCookieType _udn = GoogleAnalyticsDomainCookieType.Auto;
        /// <summary>
        /// Gets or sets the type of domain cookie to be used.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">Thrown if <paramref name="value" /> is not a member of the 
        /// <see>GoogleAnalyticsDomainCookieType</see> enumeration.</exception>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies the domain name for cookies.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(GoogleAnalyticsDomainCookieType.Auto)]
        public GoogleAnalyticsDomainCookieType DomainCookie
        {
            get { return _udn; }
            set
            {
                if (!Enum.IsDefined(typeof(GoogleAnalyticsDomainCookieType), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(GoogleAnalyticsDomainCookieType));
                _udn = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the unique domain hash for cookies is enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether the unique domain hash for cookies is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(true)]
        public bool UniqueDomainHashEnabled
        {
            get;
            set;
        }

        private int _utimeout;
        /// <summary>
        /// Gets or sets the inactive session timeout in seconds.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than or equal to 0.</exception>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies the inactive session timeout in seconds.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(1800)]
        public int InactiveSessionTimeout
        {
            get { return _utimeout; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "Value must be nonzero/nonnegative integer.");
                _utimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the real or virtual page passed to the <c>urchinTracker()</c> function.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The real or virtual page passed to the urchinTracker() function.")]
        [Category(FbmlConstants.OPTIONAL)]
        [DefaultValue("/__utm.gif")]
        public string TrackerImagePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character that should be used to separate transaction fields.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies the character that should separate transaction fields.")]
        [Category(FbmlConstants.OPTIONAL)]
        [DefaultValue("|")]
        public string TransactionFieldSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether Flash detection is enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether Flash version detection is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(true)]
        public bool FlashDetectionEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether document title detection is enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether document title detection is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(true)]
        public bool TitleDetectionEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether linker functionality is enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether linker functionality is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(false)]
        public bool LinkerEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether campaign anchors are enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether the use of anchors for campaigns is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(false)]
        public bool CampaignAnchorsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path that the tracking cookie should use.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies the cookie path for tracking.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue("/")]
        public string TrackingCookiePath
        {
            get;
            set;
        }

        private int _usample;
        /// <summary>
        /// Gets or sets the sample percentage tracked.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than 1 or greater than 100.</exception>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether the unique domain hash for cookies is enabled.")]
        [Category("Optional Urchin User Settings")]
        [DefaultValue(100)]
        public int SamplePercentageTracked
        {
            get { return _usample; }
            set
            {
                if (value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException("value", value, "Value must be not less than 1 and not greater than 100.");
                _usample = value;
            }
        }
        #endregion

        #region campaign settings
        /// <summary>
        /// Gets or sets whether campaign tracking is enabled.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies whether the campaign tracking module is enabled.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(true)]
        public bool CampaignTrackingEnabled
        {
            get;
            set;
        }

        private int _ucto;
        /// <summary>
        /// Gets or sets the inactive campaign timeout, in seconds.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than or equal to 0.</exception>
        [FbmlRequired(IsRequired = false)]
        [Description("Specifies the inactive campaign timeout in seconds.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(15768000)]
        public int InactiveCampaignTimeout
        {
            get { return _ucto; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "Value must be nonzero/nonnegative integer.");
                _utimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the campaign.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The name of the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(null)]
        public string CampaignName
        {
            get;
            set;
        }

        private GoogleAnalyticsCampaignMedium _ucmd;
        /// <summary>
        /// Gets or sets the campaign medium.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">Thrown if <paramref name="value"/> is not a member of the 
        /// <see>GoogleAnalyticsCampaignMedium</see> enumeration.</exception>
        [Description("Specifies the campaign medium.")]
        [Category("Optional Urchin campaign settings.")]
        [DefaultValue(GoogleAnalyticsCampaignMedium.Default)]
        public GoogleAnalyticsCampaignMedium CampaignMedium
        {
            get { return _ucmd; }
            set
            {
                if (!Enum.IsDefined(typeof(GoogleAnalyticsCampaignMedium), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(GoogleAnalyticsCampaignMedium));
                _ucmd = value;
            }
        }

        /// <summary>
        /// Gets or sets the campaign source.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The source of the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(null)]
        public string CampaignSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the campaign keyword.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The term or keyword of the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(null)]
        public string CampaignKeyword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the campaign content.
        /// </summary>
        [FbmlRequired(IsRequired = false)]
        [Description("The content of the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(null)]
        public string CampaignContent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the campaign ID.
        /// </summary>
        [Description("The ID number of the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(0)]
        public int CampaignID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to override the default campaign settings.
        /// </summary>
        [Description("Specifies whether to override the campaign.")]
        [Category("Optional Urchin Campaign Settings")]
        [DefaultValue(false)]
        public bool OverrideCampaign
        {
            get;
            set;
        }
        #endregion
    }
}
