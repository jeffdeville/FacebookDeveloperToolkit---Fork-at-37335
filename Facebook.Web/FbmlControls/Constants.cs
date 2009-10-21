using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.Web.FbmlControls
{
    internal static class Constants
    {
        public const string FMT_MULTI_FRIEND_SELECT_FULL_USER_OPTION = @"
<li style=""float: left; height: 64px; margin: 3px; overflow: hidden; width: 134px; display: list-item;"">
    <a href=""#"" style=""display: block; height: 56px; padding: 4px; color: #3b5998; outline-style: none; text-decoration: none;"">
        <span class=""square"" style=""background-color: white; background-position: 2px 2px; background-repeat: no-repeat; border: 1px solid #e0e0e0; display: block; float: left; height: 50px; margin-right: 5px; padding: 2px; position: relative; width: 50px; z-index: 1; background-image: url(http://www.robpaveza.net/pub/fbpg/fb_default_img.png); text-decoration: none; border-style: none solid solid; list-style-type: none;"">
        <span style=""background-position: left bottom; background-repeat: no-repeat; display: block; height: 50px; left: 2px; position: absolute; top: 2px; width: 50px;""/>
        </span>
        <strong style=""margin-left: 55px; color: #222; display: block; float: left; font-size: 11px; font-weight: normal; margin-top: 2px; width: 65px;"">User's Name</strong>
        <span style=""margin-left: 55px; color: #666; font-size: 9px; margin-top: 3px; white-space: nowrap; display: block; float: left;"">User's network</span>
    </a>
</li>";

        public const string FMT_MULTI_FRIEND_SELECT_FULL_FRIENDS_UL = @"
<ul style=""height: 350px; border-color: #c1c1c1; border-style: none solid solid; border-width: medium 1px 1px; list-style-type: none; margin: 0; overflow: auto; padding: 0; z-index: 1; -moz-padding-start: 40px; display: block; font-family: 'Lucida Grande', Tahoma, Verdana, Arial, Sans-serif;"">
{0}
</ul>
";

        public const string FMT_MULTI_FRIEND_SELECT_FULL_BUTTONS = @"
<div class=""buttons"" style=""float: none; display: block;"">
    <input type=""button"" value=""Send TestForm Request"" style=""padding-bottom: 3px; padding-top: 3px; background: #3b5998 url('http://www.facebook.com/images/icons/request_button_icon.gif') no-repeat scroll 8px 6px; overflow: visible; padding: 3px 6px 3px 26px; border-color: #d9dfea #0e1f5b #0e1f5b #d9dfea; border-style: solid; border-width: 1px; color: white; font-family: 'Lucida Grande', Tahoma, Verdana, Arial, Sans-serif;""/>
    <input type=""button"" value=""Skip"" style=""padding-bottom: 3px; padding-top: 3px; background: #f0f0f0 none repeat scroll 0 0; overflow: visible; padding: 3px 6px 3px 26px; border-color: #d9dfea #0e1f5b #0e1f5b #d9dfea; border-style: solid; border-width: 1px; color: white; font-family: 'Lucida Grande', Tahoma, Verdana, Arial, Sans-serif;""/>
</div>";

        public const string FMT_MULTI_FRIEND_SELECT_FULL_FILTERS = @"
<div style=""border-bottom: 1px solid #c1c1c1; padding: 4px 0 0; display: block;"">
    <ul style=""float: left; list-style-type: none; color: #666; margin: 0; padding: 0; display: block;"">
        <li style=""background: white url('http://www.facebook.com/images/multi_friend_selector_view_bg.gif') repeat-x scroll center top; text-shadow: 2px 2px white; float: left; padding: 4px 7px; text-align: center; width: 100px; display: list-item;"">
            <a href=""#"" style=""color: #444;"">View All</a>
        </li>
        <li style=""float: left; padding: 4px 7px; text-align: center; width: 100px; display: list-item; color: #666;"">
            <a href=""#"" style=""color: #627aad; outline-style: none; text-decoration: none;"">
                Selected (<strong style=""margin: 0 1px; color: #3b5998;"">0</strong>)
            </a>
        </li>
        <li style=""float: left; padding: 4px 7px; text-align: center; width: 100px; display: list-item; color: #666;"">
            <a href=""#"" style=""color: #627aad; outline-style: none; text-decoration: none;"">Unselected</a>
        </li>
    </ul>
</div>";

        public const string FMT_MULTI_FRIEND_SELECT_FULL_FORM = @"
<div style=""background: white none repeat scroll 0 0; font-size: 11px; margin: 10px; position: relative; text-align: left; border: 0 none; border-spacing: 0; color: black; font-family: 'lucida grande', tahoma, verdana, arial, sans-serif; display: block;"">
<div style=""background: white none repeat scroll 0 0; padding: 12px; display: block;"">
<input style=""display: block; float: right; margin: 1px 0 0; padding: 3px; background: #f0f0f0 none repeat scroll 0 0; border-color: #e7e7e7 #666 #666 #e7e7e7; color: black;""/>
<h2 style=""color: #222; font-size: 15px; margin: 0; padding: 0 0 4px;"">{0}</h2>
<h3 style=""border-bottom: 1px solid #e0e0e0; color: #999; font-size: 11px; font-weight: normal; margin: 0; padding: 0 0 8px;"">
<span style=""color: #999; font-size: 11px; font-weight: normal;"">
Add
<strong style=""font-weight: normal;"">up to 16</strong>
of your friends by clicking on their pictures below.
</span>
</h3>
<div style=""color: #555; font-size: 13px; padding: 8px 0 14px 2px; position: relative; display: block;"">
<label>Find Friends:</label>
<input class=""inputtext DOMControl_placeholder"" type=""text"" value="""" placeholder=""Start Typing a Friend's Name"" autocomplete=""off"" size=""42""/>
</div>
{1}
<div style=""display: none;""></div>
{2}

<ul id=""mfs_pag_nav_links"" class=""pagerpro""/>
    {3}
</div>
</div>";
    }
}
