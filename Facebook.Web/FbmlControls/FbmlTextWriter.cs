using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Globalization;

namespace Facebook.Web
{
    internal class FbmlTextWriter : HtmlTextWriter
    {
        private List<FbmlAttribute> _atts = new List<FbmlAttribute>();
        private HtmlTextWriter _real;

        public FbmlTextWriter(HtmlTextWriter writer) : base(new WriterSink())
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            _real = writer;
        }

        private class FbmlAttribute
        {
            public string Name, Value;
            public bool Encode;
        }

        private class WriterSink : TextWriter
        {
            public WriterSink() : base(CultureInfo.InvariantCulture) { }

            public override Encoding Encoding
            {
                get { return Encoding.Default; }
            }
        }

        public override void AddAttribute(string name, string value)
        {
            _atts.Add(new FbmlAttribute { Name = name, Value = value });
        }

        public override void AddAttribute(string name, string value, bool fEncode)
        {
            _atts.Add(new FbmlAttribute { Name = name, Value = value, Encode = fEncode });
        }

        public void RenderFullTag(string tagName, bool selfClose)
        {
            _real.Write(HtmlTextWriter.TagLeftChar);
            _real.Write(tagName);

            foreach (FbmlAttribute att in _atts)
            {
                _real.Write(SpaceChar);
                _real.Write(att.Name);
                _real.Write(EqualsDoubleQuoteString);
                if (att.Encode)
                    _real.WriteEncodedText(att.Value);
                else
                    _real.Write(att.Value);
                _real.Write(DoubleQuoteChar);
            }

            if (selfClose)
                _real.Write(SelfClosingTagEnd);
            else
            {
                _real.Write(TagRightChar);
                _real.Write(TagLeftChar);
                _real.Write(SlashChar);
                _real.Write(tagName);
                _real.Write(TagRightChar);
            }
        }
        public void RenderTagWithContents(string tagName, string contents)
        {
            _real.Write(HtmlTextWriter.TagLeftChar);
            _real.Write(tagName);

            foreach (FbmlAttribute att in _atts)
            {
                _real.Write(SpaceChar);
                _real.Write(att.Name);
                _real.Write(EqualsDoubleQuoteString);
                if (att.Encode)
                    _real.WriteEncodedText(att.Value);
                else
                    _real.Write(att.Value);
                _real.Write(DoubleQuoteChar);
            }

            _real.Write(TagRightChar);
            _real.Write(contents);
            _real.Write(TagLeftChar);
            _real.Write(SlashChar);
            _real.Write(tagName);
            _real.Write(TagRightChar);
        }
    }
}
