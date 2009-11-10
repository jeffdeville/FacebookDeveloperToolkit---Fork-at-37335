using System;
using System.Xml.Linq;

namespace Facebook.Utility
{
    ///<summary>
    ///</summary>
    public class TypeHelper
    {
        ///<summary>
        ///</summary>
        ///<param name="response"></param>
        ///<returns></returns>
        public static Type getResponseObjectType(string response)
        {
            XDocument doc = XDocument.Parse(response);
            return doc.Root == null ? null : Type.GetType("Facebook.Schema." + doc.Root.Name.LocalName);
        }
    }
}
