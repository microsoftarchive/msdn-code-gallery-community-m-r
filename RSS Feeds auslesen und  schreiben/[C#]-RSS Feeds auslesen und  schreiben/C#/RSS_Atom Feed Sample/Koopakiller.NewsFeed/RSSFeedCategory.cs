using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert eine Kategorie eines RSS Feeds bzw. eines RSS Feed Artikels.
    /// </summary>
    public class RSSFeedCategory : FeedCategoryBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSFeedCategory Klasse. Allen Eigenschaften wird ein Standartwert zugewiesen.
        /// </summary>
        public RSSFeedCategory()
        {
            Domain = "";
        }

        /// <summary>
        /// Ruft die Gruppe der Kategorie ab oder legt diese fest.
        /// </summary>
        public string Domain { get; set; }

        #region override object

        /// <summary>
        /// Liefert den Namen der Kategorie, sowie die Domain in geschweiften klammern hinter dem Namen, getrennt durch ein Leerzeichen.
        /// </summary>
        /// <returns>Der Name der Kategorie, sowie die Domain in geschweiften klammern hinter dem Namen, getrennt durch ein Leerzeichen.</returns>
        public override string ToString()
        {
            return CategoryName + " {" + Domain + "}";
        }

        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode der Instanz.</returns>
        public override int GetHashCode()
        {
            return CategoryName.GetHashCode() ^ Domain.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedCategory && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion

    }
}
