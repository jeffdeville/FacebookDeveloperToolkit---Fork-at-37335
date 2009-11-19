using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web;

namespace Facebook.Web
{
    /// <summary> 
    /// Class used to render user controls as FBML.  This is a helpful technique when building fbml applications
    /// </summary>
    public class FBMLControlRenderer
    {
        /// <summary> 
        /// Render a control as FBML
        /// </summary>
        /// <param name="path">path to the control</param>
        public static string RenderFBML(string path)
        {
            return RenderFBML<string>(path, null);
        }
        /// <summary> 
        /// Render a control as FBML
        /// </summary>
        /// <param name="path">path to the control</param>
        /// <param name="dataToBind">context specific data to bind to dynamic fbml content in the control</param>
        public static string RenderFBML<D>(string path, D dataToBind)
        {
            Page pageHolder = new Page();
            UserControl control = (UserControl)pageHolder.LoadControl(path);
            if (control is IRenderableFBML<D>)
            {
                if (dataToBind != null)
                {
                    ((IRenderableFBML<D>)control).PopulateData(dataToBind);
                }
            }
            pageHolder.Controls.Add(control);

            StringWriter output = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, output, false);

            return output.ToString();
        }
    }
}

