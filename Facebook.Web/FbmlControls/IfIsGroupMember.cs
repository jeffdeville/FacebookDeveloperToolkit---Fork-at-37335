using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Facebook.Web;
using System.Globalization;
using System.Web.UI;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Displays the enclosed content only if the specified user is a member of the specified group.
    /// </summary>
    /// <remarks>
    /// <para>The Facebook Wiki documentation for this control can be found 
    /// <a href="http://wiki.developers.facebook.com/index.php/Fb:if-is-group-member" target="_blank">here</a>.</para>
    /// </remarks>
    [ToolboxData("<{0}:IfIsGroupMember runat=\"server\"></{0}:IfIsGroupMember>")]
    [DefaultProperty("Gid")]
    public class IfIsGroupMember : ConditionalControl
    {
        /// <summary>
        /// The group ID
        /// </summary>
        [FbmlRequired(IsRequired = true)]
        [Browsable(true)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Specifies the Group ID to check.")]
        [DefaultValue(0)]
        public long Gid
        {
            get;
            set;
        }

        /// <summary>
        /// The user ID to check. (Default value is loggedinuser.) 
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("The user ID to check. (Default value is loggedinuser.)")]
        [DefaultValue(0)]
        public long Uid
        {
            get;
            set;
        }

        private GroupMembershipRole _role = GroupMembershipRole.Member;
        /// <summary>
        /// Gets or sets the type of group membership role to check.
        /// </summary>
        [Browsable(true)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies the group membership role to check.")]
        [DefaultValue(GroupMembershipRole.Member)]
        public GroupMembershipRole Role
        {
            get { return _role; }
            set
            {
                if (!Enum.IsDefined(typeof(GroupMembershipRole), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(GroupMembershipRole));
                _role = value;
            }
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (Gid == 0)
                throw new MissingRequiredAttribute("Gid", Gid);
            writer.AddAttribute("gid", Gid.ToString(CultureInfo.InvariantCulture));
            if (Uid != 0)
                writer.AddAttribute("uid", Uid.ToString(CultureInfo.InvariantCulture));
            if (_role != GroupMembershipRole.Member)
                writer.AddAttribute("role", _role.ToString().ToLowerInvariant());
        }

        /// <inheritdoc />
        protected internal override string ElementName
        {
            get { return FbmlConstants.FB_IF_IS_GROUP_MEMBER; }
        }
    }
}
