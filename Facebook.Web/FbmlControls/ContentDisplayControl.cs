using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Represents the base functionality for a control that can render child content.
    /// </summary>
    [PersistChildren(false)]
    [ParseChildren(ChildrenAsProperties = true)]
    public abstract class ContentDisplayControl : FbmlControl
    {
        private ITemplate _contentTemplate;
        private ConditionalTemplateContainer _contentContainer;

        /// <summary>
        /// Gets or sets the template displayed when the control's requirements are met.
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [TemplateInstance(TemplateInstance.Single)]
        [TemplateContainer(typeof(ConditionalTemplateContainer))]
        [FbmlRequired(IsRequired = true)]
        public ITemplate ContentTemplate
        {
            get { return _contentTemplate; }
            set
            {
                _contentTemplate = value;
                if (_contentTemplate != null)
                    CreateContents();
            }
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (_contentContainer == null)
                throw new MissingRequiredAttribute("The <ContentTemplate> template is missing and is required.");

            CreateContents();

            AddAttributesToRender(writer);
            writer.RenderBeginTag(ElementName);
            _contentContainer.RenderControl(writer);
            writer.RenderEndTag();
        }

        private void CreateContents()
        {
            ClearContents();

            if (_contentContainer == null)
            {
                _contentContainer = new ConditionalTemplateContainer(this);
                if (_contentTemplate != null)
                {
                    _contentTemplate.InstantiateIn(_contentContainer);
                }
                this.Controls.Add(_contentContainer);
            }
            else if (_contentTemplate != null)
            {
                this._contentTemplate.InstantiateIn(this._contentContainer);
            }
        }

        private void ClearContents()
        {
            if (_contentContainer != null)
                _contentContainer.Controls.Clear();

            _contentContainer = null;

            Controls.Clear();
        }
    }
}

