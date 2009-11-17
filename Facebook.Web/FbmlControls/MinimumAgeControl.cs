using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;
using System.Web.UI.WebControls;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Provides common functionality for the <see>Age18Plus</see> and <see>Age21Plus</see> controls.
    /// </summary>
    [ParseChildren(ChildrenAsProperties = true)]
    [PersistChildren(false)]
    public abstract class MinimumAgeControl : FbmlControl, INamingContainer
    {
        private AgeTemplateContainer _overContainer, _notContainer;
        private ITemplate _isTemplate, _notTemplate;

        private void CreateContents()
        {
            ClearContents();

            if (_overContainer == null)
            {
                _overContainer = new AgeTemplateContainer(this);
                if (_isTemplate != null)
                {
                    _isTemplate.InstantiateIn(_overContainer);
                }
                this.Controls.Add(_overContainer);
            }
            else if (_isTemplate != null)
            {
                this._isTemplate.InstantiateIn(this._overContainer);
            }

            if (_notContainer == null)
            {
                _notContainer = new AgeTemplateContainer(this);
                if (_notTemplate != null)
                    this._notTemplate.InstantiateIn(this._notContainer);
                Controls.Add(_notContainer);
            }
            else if (_notTemplate != null)
            {
                this._notTemplate.InstantiateIn(this._notContainer);
            }
        }

        internal void ClearContents()
        {
            if (_overContainer != null)
                _overContainer.Controls.Clear();

            _overContainer = null;

            if (_notContainer != null)
                _notContainer.Controls.Clear();

            _notContainer = null;

            Controls.Clear();
        }

        /// <summary>
        /// Gets or sets the template that should be used when the user is old enough to view the content protected by the control.  This property is required.
        /// </summary>
        [TemplateContainer(typeof(AgeTemplateContainer))]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate IsOfAgeTemplate
        {
            get { return _isTemplate; }
            set
            {
                _isTemplate = value;
                if (_isTemplate != null)
                {
                    CreateContents();
                }
            }
        }

        /// <summary>
        /// Gets or sets the template that should be used when the user is too young to view the content protected by the control.
        /// </summary>
        [TemplateContainer(typeof(AgeTemplateContainer))]
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate IsNotOfAgeTemplate
        {
            get { return _notTemplate; }
            set
            {
                _notTemplate = value;
                if (_notTemplate != null)
                    CreateContents();
            }
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (_overContainer == null)
                throw new MissingRequiredAttribute("The <IsOfAgeTemplate> template is missing and is required.");

            CreateContents();

            writer.RenderBeginTag(ElementName);
            _overContainer.RenderControl(writer);
            if (_notTemplate != null)
            {
                writer.RenderBeginTag(FbmlConstants.FB_ELSE);
                _notContainer.RenderControl(writer);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }
    }
}
