using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert ein Bild für einen RSS Feed mit den zugehörigen Eigenschaften.
    /// </summary>
    public class RSSFeedImage
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSFeedImage Klasse. Alle Eigenschaften haben danach einen Standartwert zugewiesen.
        /// </summary>
        public RSSFeedImage()
        {
            Url = "";
            Title = "";
            Link = "";
            Width = 88;
            Height = 31;
        }

        /// <summary>
        /// Ruft den Internetspeicherort des Bildes für den RSS Feed ab oder legt diesen fest.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Ruft den Titel des Bildes des RSS Feeds ab oder legt diesen fest.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Ruft das Ziel des Bildes des RSS Feeds ab oder legt dieses fest.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Ruft die Breite des Bildes für den RSS Feed ab oder legt diese fest. Das Maximum beträgt 144 und der Standartwert ist 88.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Ruft die Höhe des Bildes für den RSS Feed ab oder legt diese fest. Das Maximum beträgt 400 und der Standartwert ist 31.
        /// </summary>
        public int Height { get; set; }

        #region override object

        /// <summary>
        /// Liefert die Url des Images.
        /// </summary>
        /// <returns>Die Url des Images.</returns>
        public override string ToString()
        {
            return Url;
        }

        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode der Instanz.</returns>
        public override int GetHashCode()
        {
            return Url.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Link.GetHashCode() ^ Title.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedImage && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
