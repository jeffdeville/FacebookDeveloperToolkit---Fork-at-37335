using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.Web
{
    /// <summary> 
    /// Interface used by controls that can be rendered in FBML
    /// </summary>
    public interface IRenderableFBML<T>
    {
        /// <summary> 
        /// Used to update the fbml in the control with context specific data.
        /// </summary>
        void PopulateData(T data);
    }
}

