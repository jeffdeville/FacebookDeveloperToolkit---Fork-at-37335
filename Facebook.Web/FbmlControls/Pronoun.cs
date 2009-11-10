using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using Facebook.Web;
using System.ComponentModel;
using System.Globalization;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Renders a pronoun for a specific user. If you include no additional parameters, then you is displayed if the user with uid is viewing the page. If another user is the viewer, then he or she is displayed if the gender is known; otherwise, they is displayed. 
    /// </summary>
    /// <remarks>
    /// <para>The Facebook documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:pronoun" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:Pronoun runat=\"server\" />")]
    public class Pronoun : FbmlControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Pronoun() { UseYou = true; UseThey = true; }
        /// <summary>
        /// The user ID for whom to generate the pronoun. You can specify user IDs for multiple users by separating them with a comma, as in: uid="1234, 5678". You can substitute actor for the user ID so you can more easily aggregate Feed stories. 
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("The user ID for whom to generate the pronoun. You can specify user IDs for multiple users by separating them with a comma, as in: uid=\"1234, 5678\". You can substitute actor for the user ID so you can more easily aggregate Feed stories. ")]
        [DefaultValue("0")]
        public string Uid
        {
            get;
            set;
        }

        /// <summary>
        /// Use the possessive form (his/her/your/their). (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use the possessive form (his/her/your/their). (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Possessive { get; set; }

        /// <summary>
        /// Use the reflexive form (himself/herself/yourself/themself). (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use the reflexive form (himself/herself/yourself/themself). (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Reflexive { get; set; }

        /// <summary>
        /// Use the objective form (him/her/you/them). (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use the objective form (him/her/you/them). (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Objective { get; set; }

        /// <summary>
        /// Use the word "you" if uid is viewing the page. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use the word \"you\" if uid is viewing the page. (Default value is true.)")]
        [DefaultValue(true)]
        public bool UseYou { get; set; }

        /// <summary>
        /// Use "they" if gender is not specified. (Default value is true.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Use \"they\" if gender is not specified. (Default value is true.)")]
        [DefaultValue(true)]
        public bool UseThey { get; set; }

        /// <summary>
        /// Force a capital letter for the pronoun. (Default value is false.) 
        /// </summary>
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Force a capital letter for the pronoun. (Default value is false.) ")]
        [DefaultValue(false)]
        public bool Capitalize { get; set; }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_PRONOUN; }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (Uid == "0")
                throw new MissingRequiredAttribute("Uid", Uid);

            writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));

            if (Possessive)
                writer.AddAttribute("possessive", FbmlConstants.TRUE);
            if (Reflexive)
                writer.AddAttribute("reflexive", FbmlConstants.TRUE);
            if (Objective)
                writer.AddAttribute("objective", FbmlConstants.TRUE);

            if (!UseYou)
                writer.AddAttribute("useyou", FbmlConstants.FALSE);
            if (!UseThey)
                writer.AddAttribute("usethey", FbmlConstants.FALSE);

            if (Capitalize)
                writer.AddAttribute("capitalize", FbmlConstants.TRUE);

            base.AddAttributesToRender(writer);
        }
    }
}
