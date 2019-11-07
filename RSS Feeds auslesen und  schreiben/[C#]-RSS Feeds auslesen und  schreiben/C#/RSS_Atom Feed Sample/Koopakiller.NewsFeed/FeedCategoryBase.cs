using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert eine Basisklasse mit den Grundlegendsten Eigenschaften für eine Kategorie eines NewFeeds bzw. Eines Artikels in einem NewsFeed.
    /// </summary>
    public abstract class FeedCategoryBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der FeedCategroieBase Klasse. Es werden allen Eigenschaften Standartwerte zugewiesen.
        /// Hinweis: Diese Methode kann nicht von außen aufgerufen werden, nur von Kindern dieser Klasse.
        /// </summary>
        protected FeedCategoryBase()
        {
            CategoryName = "";
        }

        /// <summary>
        /// Ruft den Namen der Kategorie ab oder legt diesen fest.
        /// </summary>
        public string CategoryName { get; set; }

        #region override object

        /// <summary>
        /// Diese Methode wird überladen, sollte jedoch den Namen der Kategroie zurück geben.
        /// </summary>
        /// <returns>Der Name der Kategorie.</returns>
        public override abstract string ToString();
        /// <summary>
        /// Liefert den HashCode dieses Objekts.
        /// </summary>
        /// <returns>Der Hashcode des Objekts.</returns>
        public override abstract int GetHashCode();
        /// <summary>
        /// Vergleicht ein Objekt mit dieser Instanz.
        /// </summary>
        /// <param name="obj">Das zu vergleichende Objekt.</param>
        /// <returns>True, wenn obj die selben Werte in den Eigenschaften hat wie diese Instanz, andernfalls False.</returns>
        public override abstract bool Equals(object obj);

        #endregion
    }
}
