using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using Facebook.Web;

namespace Facebook.Web.FbmlControls
{
    /// <summary>
    /// Provides common functionality for the <see>Error</see>, <see>Success</see>, and <see>Explanation</see> controls.
    /// </summary>
    [DefaultProperty("Message")]
    public abstract class StatusMessageBase : FbmlControl
    {
        /// <summary>
        /// Gets or sets the error message displayed to the user.  This property is required.
        /// </summary>
        [DefaultValue(null)]
        [Category(FbmlConstants.REQUIRED)]
        [Description("Specifies the primary message to display to the user.")]
        [FbmlRequired(IsRequired = true)]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets descriptive information about the error.
        /// </summary>
        [DefaultValue(null)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies additional information about the message.")]
        public string Description
        {
            get;
            set;
        }

        /// <inheritdoc />
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            if (string.IsNullOrEmpty(Message))
                throw new MissingRequiredAttribute("Message", Message);

            writer.AddAttribute("message", Message, true);
            if (DecorationType != StatusMessageDecorationType.Standard)
            {
                switch (DecorationType)
                {
                    case StatusMessageDecorationType.NoBottomPadding:
                        writer.AddAttribute("decoration", "shorten");
                        break;
                    case StatusMessageDecorationType.NoTopPadding:
                        writer.AddAttribute("decoration", "no_padding");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <inheritdoc />
        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(Description))
            {
                base.Render(writer);
            }
            else
            {
                AddAttributesToRender(writer);
                writer.RenderBeginTag(ElementName);
                writer.WriteEncodedText(Description);
                writer.RenderEndTag();
            }
        }

        private StatusMessageDecorationType _deco = StatusMessageDecorationType.Standard;

        /// <summary>
        /// Gets or sets the padding style used for the element.
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException">Thrown if <paramref name="value"/> is not a member of the <see>StatusMessageDecorationType</see>
        ///  enumeration.</exception>
        [DefaultValue(StatusMessageDecorationType.Standard)]
        [Category(FbmlConstants.OPTIONAL)]
        [Description("Specifies a padding style for the element.")]
        public StatusMessageDecorationType DecorationType
        {
            get { return _deco; }
            set
            {
                if (!Enum.IsDefined(typeof(StatusMessageDecorationType), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(StatusMessageDecorationType));
                _deco = value;
            }
        }

    }
}
