using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert die Enclosure Eigenschaft eines RSS Feed Artikels mit den dazugehöhrigen Eigenschaften.
    /// </summary>
    public class RSSFeedArticleEnclosure
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSEnclosure Klasse. Alle Eigenschaften haben danach einen Standartwert.
        /// </summary>
        public RSSFeedArticleEnclosure()
        {
            Url = "";
            Type = "";
            Length = 0;
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSEnclosure mit Ihren Angaben als Wert.
        /// </summary>
        /// <param name="url">Der Pfad zum Medienanhang, welcher zum Artikel gehört.</param>
        /// <param name="mime">Der MIME Typ des Medienanhangs.</param>
        /// <param name="length">Die Größe in Byte des Medienanhangs.</param>
        public RSSFeedArticleEnclosure(string url, string mime, ulong length)
        {
            Url = url;
            Type = mime;
            Length = length;
        }
        /// <summary>
        /// Ruft einen Pfad zu einem Medienanhang, welches zu dem Artikel gehört ab oder legt diesen fest.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Ruft den MIME-Typ des Medienanhangs, welches zum Artikel gehört ab oder legt diesen fest.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Ruft die Länge des Medienanhangs in Byte ab oder legt diese fest.
        /// </summary>
        public ulong Length { get; set; }

        #region override object

        /// <summary>
        /// Liefert die Url des Enclosures.
        /// </summary>
        /// <returns>Die Url des Enclosures.</returns>
        public override string ToString()
        {
            return Url;
        }
        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return Length.GetHashCode() ^ Type.GetHashCode() ^ Url.GetHashCode();
        }
        /// <summary>
        /// Vergleicht ein object mit dieser Instanz auf gleichheit.
        /// </summary>
        /// <param name="obj">Das zu verglichende object.</param>
        /// <returns>True, wenn das object mit dieser Instanz übereinstimmt, anderfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedArticleEnclosure && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
