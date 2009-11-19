using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;
using System.Security.Permissions;

namespace Facebook.Web
{
    /// <summary>
    /// Specifies that a required FBML attribute was not set.
    /// </summary>
    [Serializable]
    public class MissingRequiredAttribute : Exception
    {
        private string _propName;
        private object _defValue;

        /// <summary>
        /// Creates a new <see>MissingRequiredAttribute</see>.
        /// </summary>
        public MissingRequiredAttribute() { }
        /// <summary>
        /// Creates a new <see>MissingRequiredAttribute</see>.
        /// </summary>
        /// <param name="message">Informational text about the exception.</param>
        public MissingRequiredAttribute(string message) : base(message) { }
        /// <summary>
        /// Creates a new <see>MissingRequiredAttribute</see>.
        /// </summary>
        /// <param name="message">Informational text about the exception.</param>
        /// <param name="inner">An inner exception wrapped by this exception.</param>
        public MissingRequiredAttribute(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Deserializes a <see>MissingRequiredAttribute</see>.
        /// </summary>
        /// <param name="info">Serialization data.</param>
        /// <param name="context">The serialization streaming context.</param>
        [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
        protected MissingRequiredAttribute(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _propName = info.GetString("PropertyName");
            _defValue = info.GetValue("InvalidValue", typeof(object));
        }

        /// <summary>
        /// Creates a new <see>MissingRequiredAttribute</see>.
        /// </summary>
        /// <param name="propertyName">The name of the property that was at fault.</param>
        /// <param name="invalidValue">The invalid value, if any, that was the default or set value.</param>
        public MissingRequiredAttribute(string propertyName, object invalidValue) 
            : base(string.Format(CultureInfo.InvariantCulture, "FBML required property \"{0}\" was not set.  The default/invalid value was \"{1}\".", propertyName, invalidValue ?? (object)"{null}"))
        {
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            _propName = propertyName;
            _defValue = invalidValue;

        }

        /// <inheritdoc />
        [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("PropertyName", _propName);
            info.AddValue("InvalidValue", _defValue, typeof(object));
        }

        /// <summary>
        /// The property that was not set or set invalidly.
        /// </summary>
        public string PropertyName
        {
            get { return _propName; }
        }

        /// <summary>
        /// The invalid or default value of the property.
        /// </summary>
        public object InvalidValue
        {
            get { return _defValue; }
        }
    }
}
