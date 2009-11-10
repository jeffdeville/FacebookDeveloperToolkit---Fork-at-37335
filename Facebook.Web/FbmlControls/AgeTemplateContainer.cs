using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Facebook.Web.FbmlControls
{
    internal class AgeTemplateContainer : FbmlTemplateControlContainer
    {
        private MinimumAgeControl _parent;

        public AgeTemplateContainer(MinimumAgeControl parent)
        {
            _parent = parent;
        }

        public MinimumAgeControl TemplateParent
        {
            get { return _parent; }
        }
    }
}
