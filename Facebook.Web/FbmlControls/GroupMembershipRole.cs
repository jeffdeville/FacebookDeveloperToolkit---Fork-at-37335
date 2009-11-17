using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Specifies the roles a user may have in a group.
    /// </summary>
    public enum GroupMembershipRole
    {
        /// <summary>
        /// Specifies that the user is a standard member of the specified group.
        /// </summary>
        Member,
        /// <summary>
        /// Specifies that the user is an officer of the specified group.
        /// </summary>
        Officer,
        /// <summary>
        /// Specifies that the user is an admin of the specified group.
        /// </summary>
        Admin,
    }
}
