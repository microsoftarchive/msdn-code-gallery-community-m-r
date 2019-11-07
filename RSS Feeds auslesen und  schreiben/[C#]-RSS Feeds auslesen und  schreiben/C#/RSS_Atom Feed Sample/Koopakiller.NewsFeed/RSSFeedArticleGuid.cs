using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert den Wert der Eigenschaft GUID in einem RSS Feed Artikel mit zugehörigen Eigenschaften(Attributen).
    /// </summary>
    public class RSSFeedArticleGuid
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSFeedArticleGuid Klasse.
        /// Allen Eigenschaften ist danach ein Standartwert zugewiesen.
        /// </summary>
        public RSSFeedArticleGuid()
        {
            Guid = "";
            IsPermaLink = true;
        }
        /// <summary>
        /// Ruft die GUID ab oder legt diese fest.
        /// Hinweis: Es muss sich um keine gültige GUID handeln.
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// Ruft einen Wert ab, der angibt ob das Attribut IsPermaLink des Elements GUID true oder false ist oder legt diesen fest.
        /// Hinweis: Der Standartwert der W3C ist true.
        /// </summary>
        public bool IsPermaLink { get; set; }

        #region override

        /// <summary>
        /// Gibt die Guid zurück.
        /// </summary>
        /// <returns>Die Guid.</returns>
        public override string ToString()
        {
            return Guid;
        }
        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return Guid.GetHashCode() ^ IsPermaLink.GetHashCode();
        }
        /// <summary>
        /// Vergleicht ein object mit dieser Instanz.
        /// </summary>
        /// <param name="obj">Das zu vergleichende Objekt</param>
        /// <returns>True, wenn die Objekte übereinstimmen, andernfalls false.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedArticleGuid && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
