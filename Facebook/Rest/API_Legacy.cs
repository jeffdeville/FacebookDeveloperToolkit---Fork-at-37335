using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Facebook.Entity;
using Facebook.Exceptions;
using Facebook.Parser;

namespace Facebook.API
{
	public partial class FacebookAPI
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="title">The title or header of the application info section.</param>
		/// <param name="type">Specify 1 for a text-only field-item configuration or 5 for a thumbnail configuration.</param>
		/// <param name="info_fields">A JSON-encoded array of elements comprising an application 
		/// info section, including the field (the title of the field) and an array of 
		/// info_item objects (each object has a label and a link, and optionally contains image, description, and sublabel fields.</param>
		/// <param name="uid"></param>
		/// <returns></returns>
		public string setInfo(string title, int type, Dictionary<string, string> info_fields, string uid)
		{
			var parameterList = new Dictionary<string, string>(5) { { "method", "facebook.profile.setInfo" } };
			AddParameter(parameterList, "title", title);
			AddParameter(parameterList, "type", type.ToString());
			AddParameterJSON(parameterList, "info_fields", info_fields);
			AddParameterCulture(parameterList, "uid", uid);

			return GetSingleNode(ExecuteApiCallString(parameterList, true), "profile_setInfo_response");
		}
	}
}