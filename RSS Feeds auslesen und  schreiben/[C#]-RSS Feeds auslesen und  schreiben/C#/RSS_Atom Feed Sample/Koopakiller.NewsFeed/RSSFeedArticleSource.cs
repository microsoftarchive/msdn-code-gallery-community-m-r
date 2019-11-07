using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert die Eigenschaft SOURCE in einem RSS Feed Artikel mit zugehörigen Eigenschaften.
    /// </summary>
    public class RSSFeedArticleSource
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSSource Uri Klasse. Alle Eigenschaften besitzen danach einen Standartwert.
        /// </summary>
        public RSSFeedArticleSource()
        {
            Source = "";
            Uri = "";
        }
        /// <summary>
        /// Ruft den Namen einer ThirdParty Quelle ab oder legt diese fest.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Ruft die Url einer ThirdParty Quelle ab oder legt diese fest.
        /// </summary>
        public string Uri { get; set; }

        #region override object

        /// <summary>
        /// Liefert die Source- und die Uri Eigenschaft, getrennt durch einen Zeilenumbruch.
        /// </summary>
        /// <returns>Die Source- und die Uri Eigenschaft, getrennt durch einen Zeilenumbruch.</returns>
        public override string ToString()
        {
            return Source + Environment.NewLine + Uri;
        }
        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return Source.GetHashCode() ^ Uri.GetHashCode();
        }
        /// <summary>
        /// Vergleicht ein object mit dieser Instanz auf gleichheit.
        /// </summary>
        /// <param name="obj">Das zu verglichende object.</param>
        /// <returns>True, wenn das object mit dieser Instanz übereinstimmt, anderfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedArticle && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
