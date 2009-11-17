using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders the name of the user specified, optionally linked to his or her profile. 
    /// This also works for Facebook Pages with the ID of the Page passed as the uid parameter. 
    /// You can use this tag for both the subject and the object of a sentence describing an action. For example, if a user with the user ID $tagger tags a photo of a user with the user ID $tagee, you could say: 
    /// &lt; fb:name uid="$tagger" capitalize="true" /&gt; tagged a photo of &lt;fb:name subjectid="$tagger" uid="$tagee" /&gt;
    /// User names and profile links follow standard Facebook privacy rules for other viewing users. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:name" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Name runat=\"server\" />")]
    public class Name : FbmlControl
    {
        private bool _firstOnly, _lastOnly;

        /// <summary>
        /// Creates a new <see>Name</see> control.
        /// </summary>
        public Name()
        {
            Linked = true;
            UseYou = true;
            DisplayIfUserIsInaccessible = string.Empty;
        }

        /// <summary>
        /// The ID of the user or Page whose name you want to show. You can also use "profileowner" on a user's profile or an application canvas page; you can use "loggedinuser" only on canvas pages
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The ID of the user or Page whose name you want to show. You can also use \"profileowner\" on a user's profile or an application canvas page; you can use \"loggedinuser\" only on canvas pages")]
        [DefaultValue("0")]
        public string Uid
        {
            get;
            set;
        }

        /// <summary>
        /// Show only the user's first name. (Default value is false.) 
        /// </summary>
        /// <remarks>
        /// <para>Setting this property to <see langword="true" /> will reset the <see>LastNameOnly</see>
        /// property to <see langword="false" />.  If both are set to <see langword="true" /> with declarative 
        /// syntax, this property takes precedence.</para>
        /// </remarks>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Show only the user's first name. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool FirstNameOnly
        {
            get { return _firstOnly; }
            set
            {
                _firstOnly = value;
                if (value) _lastOnly = false;
            }
        }

        /// <summary>
        /// Link to the user's profile. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Link to the user's profile. (Default value is true.)")]
        [DefaultValue(true)]
        public bool Linked { get; set; }

        /// <summary>
        /// Show only the user's last name. (Default value is false.) 
        /// </summary>
        /// <remarks>
        /// <para>Setting this property to <see langword="true" /> will reset the <see>FirstNameOnly</see>
        /// property to <see langword="false" />.  If both are set to <see langword="true" /> with declarative 
        /// syntax, the <see>FirstNameOnly</see> property takes precedence.</para>
        /// </remarks>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Show only the user's last name. (Default value is false.)")]
        [DefaultValue(false)]
        public bool LastNameOnly
        {
            get { return _lastOnly; }
            set
            {
                _lastOnly = value;
                if (value) _firstOnly = false;
            }
        }

        /// <summary>
        /// Make the user's name possessive (e.g. Joe's instead of Joe). (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Make the user's name possessive (e.g. Joe's instead of Joe). (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Possessive { get; set; }

        /// <summary>
        /// Use "yourself" if useyou is true. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use \"yourself\" if useyou is true. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Reflexive { get; set; }

        /// <summary>
        /// Displays the primary network for the uid. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Displays the primary network for the uid. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool ShowNetwork { get; set; }

        /// <summary>
        /// Use "you" if uid matches the logged in user. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use \"you\" if uid matches the logged in user. (Default value is true.)")]
        [DefaultValue(true)]
        public bool UseYou { get; set; }

        /// <summary>
        /// Alternate text to display if the logged in user cannot access the user specified. To specify an empty string instead of the default, use ifcantsee="". (Default value is Facebook User.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Alternate text to display if the logged in user cannot access the user specified. To specify an empty string instead of the default, use ifcantsee=\"\". (Default value is Facebook User.)")]
        [DefaultValue("")]
        public string DisplayIfUserIsInaccessible { get; set; }

        /// <summary>
        /// Capitalize the text if useyou==true and loggedinuser==uid. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Capitalize the text if useyou==true and loggedinuser==uid. (Default value is false.)")]
        [DefaultValue(false)]
        public bool CapitalizeYou { get; set; }
        
        /// <summary>
        /// The Facebook ID of the subject of the sentence where this name is the object of the verb of the sentence. Will use the reflexive when appropriate. When subjectid is used, uid is considered to be the object and uid's name is produced. 
        /// </summary> 
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The Facebook ID of the subject of the sentence where this name is the object of the verb of the sentence. Will use the reflexive when appropriate. When subjectid is used, uid is considered to be the object and uid's name is produced.")]
        [DefaultValue(false)]
        public long SubjectId { get; set; }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_NAME; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            long uid = 0;
            long.TryParse(Uid, out uid);

            if (Uid == "0" && Uid != "profileowner" && Uid != "loggedinuser" )
                throw new MissingRequiredAttribute("FbUserID", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
            if (!Linked)
                writer.AddAttribute("linked", FbmlConstants.FALSE);

            if (FirstNameOnly)
                writer.AddAttribute("firstnameonly", FbmlConstants.TRUE);
            else if (LastNameOnly)
                writer.AddAttribute("lastnameonly", FbmlConstants.TRUE);

            if (Possessive)
                writer.AddAttribute("possessive", FbmlConstants.TRUE);
            if (Reflexive)
                writer.AddAttribute("reflexive", FbmlConstants.TRUE);
            if (ShowNetwork)
                writer.AddAttribute("shownetwork", FbmlConstants.TRUE);
            if (!UseYou)
                writer.AddAttribute("useyou", FbmlConstants.FALSE);
            if (!string.IsNullOrEmpty(DisplayIfUserIsInaccessible))
                writer.AddAttribute("ifcantsee", DisplayIfUserIsInaccessible, true);
            if (CapitalizeYou)
                writer.AddAttribute("capitalize", FbmlConstants.TRUE);
            if (SubjectId != 0)
                writer.AddAttribute("subjectid", SubjectId.ToString(CultureInfo.InvariantCulture));

            base.AddAttributesToRender(writer);
        }
    }
}
