using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Koopakiller.NewsFeed
{
        /// <summary>
        /// Eine Basisklasse für einen Newsfeed.
        /// </summary>
        public abstract class FeedBase
        {
            /// <summary>
            /// Initialisiert eine neue Instanz der FeedBase Klasse. 
            /// Alle Eigenschaften werden somit auf einen Standartwert gesetzt.
            /// </summary>
            public FeedBase()
            {
                Copyrights = "";
                Published = DateTime.MinValue;
                Language = CultureInfo.InvariantCulture;
                Articles = new List<FeedArticleBase>();
            }

            /// <summary>
            /// Eine überladene Methode, welche einen NewsFeed einließt.
            /// </summary>
            /// <param name="path">Der Pfad zum Newsfeed. Der Newsfeed kann auch im internet liegen.</param>
            public abstract void Load(string path);
            /// <summary>
            /// Eine überladene Methode, welche einen NewsFeed einließt.
            /// </summary>
            /// <param name="path">Der Stream aus dem der Newsfeed gelesen werden soll.</param>
            public abstract void Load(Stream path);
            /// <summary>
            /// Eine überladene Methode, welche einen NewsFeed einließt.
            /// </summary>
            /// <param name="path">Der TextReader aus dem der Newsfeed gelesen werden soll.</param>
            public abstract void Load(TextReader path);
            /// <summary>
            /// Eine überladene Methode, welche einen NewsFeed einließt.
            /// </summary>
            /// <param name="path">Der XmlReader aus dem der Newsfeed gelesen werden soll.</param>
            public abstract void Load(XmlReader path);
            /// <summary>
            /// Eine überladene Methode, welche einen Newsfeed abspeichert.
            /// </summary>
            /// <param name="path">Der Pfad zum zu speichernden Newfeed.</param>
            public abstract void Save(string path);
            /// <summary>
            /// Eine überladene Methode, welche einen Newsfeed abspeichert.
            /// </summary>
            /// <param name="path">Der Stream, in den der Newsfeed gespeichert werden soll.</param>
            public abstract void Save(Stream path);
            /// <summary>
            /// Eine überladene Methode, welche einen Newsfeed abspeichert.
            /// </summary>
            /// <param name="path">Der TextWriter, in den der Newsfeed gespeichert werden soll.</param>
            public abstract void Save(TextWriter path);
            /// <summary>
            /// Eine überladene Methode, welche einen Newsfeed abspeichert.
            /// </summary>
            /// <param name="path">Der XmlWriter, in den der Newsfeed gespeichert werden soll.</param>
            public abstract void Save(XmlWriter path);

            #region Eigenschaften

            /// <summary>
            /// Ruft die Coprights des Newfeed ab oder legt sie fest.
            /// </summary>
            public string Copyrights { get; set; }
            /// <summary>
            /// Ruft das Veröffentlichungsdatum des Newsfeed ab oder legt es fest.
            /// </summary>
            public DateTime Published { get; set; }
            /// <summary>
            /// Ruft die Sprache des Newsfeed ab oder legt diese fest.
            /// </summary>
            public CultureInfo Language { get; set; }

            /// <summary>
            /// Ruft eine Liste der NewsFeed Artikel ab oder legt diese fest.
            /// </summary>
            public List<FeedArticleBase> Articles { get; set; }

            #endregion

            #region override object

            /// <summary>
            /// Diese Methode wird überladen, sollte jedoch den Titel und die Beschreibung des Feeds, getrennt durch einen Zeilenumbruch zurück geben.
            /// </summary>
            /// <returns>Den Titel und die Beschreibung des Feeds, getrennt durch einen Zeilenumbruch.</returns>
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
