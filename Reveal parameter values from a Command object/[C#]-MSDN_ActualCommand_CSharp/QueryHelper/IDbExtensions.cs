using System;
using System.Data;
using System.Linq;
using System.Text;


namespace QueryHelper
{
    /// <summary>
    /// Contains language extension methods for assisting with parameterized command statements
    /// </summary>
    public static class IDbExtensions
    {
        /// <summary>
        /// Used to show an SQL statement with actual values
        /// </summary>
        /// <param name="sender">Command object</param>
        /// <returns>Command object command text with parameter values</returns>
        /// <example>
        /// <code source="CodeExamples\ActualCommandDemo.vb" language="vbnet" title="VB.NET Examples"/>
        /// </example>
        public static string ActualCommandText(this IDbCommand sender)
        {
            StringBuilder sb = new StringBuilder(sender.CommandText);
            IDataParameter EmptyParameterNames = (from T in sender.Parameters.Cast<IDataParameter>() where string.IsNullOrWhiteSpace(T.ParameterName) select T).FirstOrDefault();

            if (EmptyParameterNames != null)
            {
                return sender.CommandText;
            }

            foreach (IDataParameter p in sender.Parameters)
            {
                if ((p.DbType == DbType.AnsiString) || (p.DbType == DbType.AnsiStringFixedLength) || (p.DbType == DbType.Date) || (p.DbType == DbType.DateTime) || (p.DbType == DbType.DateTime2) || (p.DbType == DbType.Guid) || (p.DbType == DbType.String) || (p.DbType == DbType.StringFixedLength) || (p.DbType == DbType.Time) || (p.DbType == DbType.Xml))
                {
                    if (p.ParameterName.Substring(0, 1) == "@")
                    {
                        if (p.Value == null)
                        {
                            throw new Exception("no value given for parameter '" + p.ParameterName + "'");
                        }
                        sb = sb.Replace(p.ParameterName, string.Format("'{0}'", p.Value.ToString().Replace("'", "''")));
                    }
                    else
                    {
                        sb = sb.Replace(string.Concat("@", p.ParameterName), string.Format("'{0}'", p.Value.ToString().Replace("'", "''")));
                    }
                }
                else
                {
                    sb = sb.Replace(p.ParameterName, p.Value.ToString());
                }
            }

            return sb.ToString();
        }
    }
}