using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Microsoft.Samples.Synchronization.ClientServices.Formatters
{
    static class FormatterUtilities
    {
        public static string GetEdmType(Type type)
        {
            if (type.IsGenericType)
            {
                return GetEdmType(type.GetGenericArguments()[0]);
            }
            switch (type.Name)
            {
                case "Boolean":
                    return "Edm.Boolean";
                case "Byte":
                    return "Edm.Byte";
                case "Char":
                case "String":
                    return "Edm.String";
                case "DBNull":
                    return "null";
                case "DateTime":
                    return "Edm.DateTime";
                case "Decimal":
                    return "Edm.Decimal";
                case "Double":
                    return "Edm.Double";
                case "Int16":
                    return "Edm.Int16";
                case "Int32":
                    return "Edm.Int32";
                case "Int64":
                    return "Edm.Int64";
                case "SByte":
                    return "Edm.SByte";
                case "Single":
                    return "Edm.Single";
                case "Byte[]":
                    return "Edm.Binary";
                case "Guid":
                    return "Edm.Guid";
                case "TimeSpan":
                    return "Edm.Time";
                case "DateTimeOffset":
                    return "Edm.DateTimeOffset";
                default:
                    throw new NotSupportedException("TypeCode " + Type.GetTypeCode(type) + " is not a supported type.");
            }
        }

        /// <summary>
        /// Looks at passed in Type and calls the appropriate Date functions for Atom
        /// </summary>
        /// <param name="objValue">Actual value</param>
        /// <param name="type">Type coverting from</param>
        /// <returns>Atom representation</returns>
        public static object ConvertDateTimeForType_Atom(object objValue, Type type)
        {
            if (type == FormatterConstants.DateTimeType)
            {
                return ConvertDateTimeToAtom((DateTime)objValue);
            }

            return ConvertTimeToAtom((TimeSpan)objValue);
        }

        /// <summary>
        /// Converts DateTime to OData Atom format as specified in http://www.odata.org/developers/protocols/atom-format#PrimitiveTypes for DateTime
        /// Format is"yyyy-MM-ddThh:mm:ss.fffffff"
        /// <param name="date">DateTime to convert</param>
        /// </summary>
        /// <returns>Atom representation of DateTime</returns>
        public static string ConvertDateTimeToAtom(DateTime date)
        {
            return date.ToString(FormatterConstants.AtomDateTimeLexicalRepresentation);
        }

        /// <summary>
        /// Converts a TimeSpan to OData atom format as specified in http://www.odata.org/developers/protocols/atom-format#PrimitiveTypes for Time
        /// Actual lexical representation is time'hh:mm:ss.fffffff'
        /// </summary>
        /// <param name="t">Timespan to convert</param>
        /// <returns>Atom representation of Timespan</returns>
        public static string ConvertTimeToAtom(TimeSpan t)
        {
            return t.ToString();
        }

        internal static object ParseDateTimeFromString(string value, Type type)
        {
            try
            {
                // Its an Atom string
                return ParseAtomString(value, type);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException(string.Format("Invalid Date/Time value received. Unable to parse value {0} to type {1}.", value, type.Name));
            }
        }

        private static object ParseAtomString(string value, Type type)
        {
            if (FormatterConstants.DateTimeType.IsAssignableFrom(type))
            {
                return XmlConvert.ToDateTime(value, FormatterConstants.AtomDateTimeLexicalRepresentation);
            }
            else
            {
                // Its a TimeSpan
                return TimeSpan.Parse(value);
            }
        }

    }
}
