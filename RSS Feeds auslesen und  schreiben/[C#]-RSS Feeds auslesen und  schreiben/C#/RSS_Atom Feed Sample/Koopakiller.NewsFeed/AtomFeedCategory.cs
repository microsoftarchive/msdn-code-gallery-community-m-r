using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert eine Kategorie in einem Atom Feed bzw. einem Artikel in einem Atom Feed.
    /// </summary>
    public class AtomFeedCategory : FeedCategoryBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedCategory Klasse.
        /// Allen Eigenschaften isrt danach ein Standartwert zugewiesen.
        /// </summary>
        public AtomFeedCategory()
        {
            Term = "";
        }
        /// <summary>
        /// Ruft den Term, nach dem kategorisiert werden soll ab oder legt diesen fest.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Liefert den Namen der Kategorie.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return CategoryName;
        }

        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode der Instanz.</returns>
        public override int GetHashCode()
        {
            return CategoryName.GetHashCode() ^ Term.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }
    }
}