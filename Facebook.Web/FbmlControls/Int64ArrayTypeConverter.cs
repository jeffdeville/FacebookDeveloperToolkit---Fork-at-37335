using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using Facebook.Utility;

namespace Facebook.Web
{
    internal sealed class Int64ArrayTypeConverter : ArrayConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string source = value as string;
            if (source == null)
                return null;
            string[] input = source.Split(',');
            long[] output = new long[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = long.Parse(input[i], CultureInfo.InvariantCulture);
            }
            return output;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return StringHelper.ConvertToCommaSeparated((long[])value);
        }
    }
}
