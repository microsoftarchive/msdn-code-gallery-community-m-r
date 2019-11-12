using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Stellt eine Basis für Newfeed Artikel dar.
    /// </summary>
    public abstract class FeedArticleBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der FeedArticleBase Klasse.
        /// Alle Eigenschaften werden somit auf einen Standartwert gesetzt.
        /// Hinweis: Diese Methode kann nicht von außen aufgerufen werden, nur von Kindern dieser Klasse.
        /// </summary>
        protected FeedArticleBase()
        {
            //Eigenschaften auf Standartwert setzen.
            Published = DateTime.MinValue;//Veröffentlichungsdatum
        }

        #region Eigenschaften

        /// <summary>
        /// Ruft das Veröffentlichungsdatum des Artikels ab oder legt dieses fest.
        /// </summary>
        public DateTime Published { get; set; }

        #endregion

        #region override object

        /// <summary>
        /// Diese Methode wird überladen, sollte jedoch den Titel und den Autor des Artikels, getrennt durch einen Zeilenumbruch zurück geben.
        /// </summary>
        /// <returns>Liefert den Titel des Artikels sowie den Autor, getrennt durch einen Zeilenumbruch.</returns>
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
