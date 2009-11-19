using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Provides common functionality for the conditional controls, including <see>IsInNetwork</see>, <see>IfCanSee</see>, <see>IfCanSeePhoto</see>, 
    /// <see>IfIsAppUser</see>, <see>IfIsFriendsWithViewer</see>, <see>IfIsGroupMember</see>, <see>IfIsUser</see>, and <see>IfSectionNotAdded</see>.
    /// </summary>
    [ParseChildren(ChildrenAsProperties = true)]
    [PersistChildren(false)]
    public abstract class ConditionalControl : FbmlControl
    {
        private ITemplate _yesTemplate, _noTemplate;
        private ConditionalTemplateContainer _yesContainer, _noContainer;

        /// <summary>
        /// Gets or sets the template that should be displayed when the test's conditions are met.  This property is required.
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [TemplateInstance(TemplateInstance.Single)]
        [TemplateContainer(typeof(ConditionalTemplateContainer))]
        [FbmlRequired(IsRequired = true)]
        public ITemplate IsValidTemplate
        {
            get { return _yesTemplate; }
            set
            {
                _yesTemplate = value;
                if (_yesTemplate != null)
                    CreateContents();
            }
        }

        /// <summary>
        /// Gets or sets the template that should be displayed when the test's conditions are not met.
        /// </summary>
        [Browsable(false)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DefaultValue(null)]
        [TemplateInstance(TemplateInstance.Single)]
        [TemplateContainer(typeof(ConditionalTemplateContainer))]
        public ITemplate IsNotValidTemplate
        {
            get { return _noTemplate; }
            set
            {
                _noTemplate = value;
                if (_noTemplate != null)
                    CreateContents();
            }
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (_yesContainer == null)
                throw new MissingRequiredAttribute("The <IsValidTemplate> template is missing and is required.");

            CreateContents();

            AddAttributesToRender(writer);
            writer.RenderBeginTag(ElementName);
            _yesContainer.RenderControl(writer);
            if (_noTemplate != null)
            {
                writer.RenderBeginTag(FbmlConstants.FB_ELSE);
                _noContainer.RenderControl(writer);
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        private void CreateContents()
        {
            ClearContents();

            if (_yesContainer == null)
            {
                _yesContainer = new ConditionalTemplateContainer(this);
                if (_yesTemplate != null)
                {
                    _yesTemplate.InstantiateIn(_yesContainer);
                }
                this.Controls.Add(_yesContainer);
            }
            else if (_yesTemplate != null)
            {
                this._yesTemplate.InstantiateIn(this._yesContainer);
            }

            if (_noContainer == null)
            {
                _noContainer = new ConditionalTemplateContainer(this);
                if (_noTemplate != null)
                    this._noTemplate.InstantiateIn(this._noContainer);
                Controls.Add(_noContainer);
            }
            else if (_noTemplate != null)
            {
                this._noTemplate.InstantiateIn(this._noContainer);
            }
        }

        internal void ClearContents()
        {
            if (_yesContainer != null)
                _yesContainer.Controls.Clear();

            _yesContainer = null;

            if (_noContainer != null)
                _noContainer.Controls.Clear();

            _noContainer = null;

            Controls.Clear();
        }

    }
}
