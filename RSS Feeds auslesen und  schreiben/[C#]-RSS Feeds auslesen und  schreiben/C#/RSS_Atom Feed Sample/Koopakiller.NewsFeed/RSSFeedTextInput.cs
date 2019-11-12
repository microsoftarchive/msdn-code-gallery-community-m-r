using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{

    /// <summary>
    /// Repräsentiert ein Optionales InputFields für einen RSS Feed.
    /// </summary>
    public class RSSFeedTextInput
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSFeedTextInput Klasse. Alle Eigenschaften haben danach einen Standartwert zugewiesen.
        /// </summary>
        public RSSFeedTextInput()
        {
            Description = "";
            Name = "";
            Link = "";
            Title = "";
        }

        /// <summary>
        /// Ruft die Beschreibung des Optionalen InputFields ab oder legt diese fest.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Ruft den Namen des Optionalen InputFields ab oder legt diese fest.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ruft die Url der CGI des Optionalen InputFields ab oder legt diese fest.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Ruft die Beschriftung des Übernehmen-Buttons des Optionalen InputFields ab oder legt diese fest.
        /// </summary>
        public string Title { get; set; }

        #region override object

        /// <summary>
        /// Liefert den Namen des Inputfields, sowie die Beschreibung des Optionalen InputFields, getrennt durch einen Zeilenumbruch.
        /// </summary>
        /// <returns>Der Name des Inputfields, sowie die Beschreibung des Optionalen InputFields, getrennt durch einen Zeilenumbruch.</returns>
        public override string ToString()
        {
            return Name + Environment.NewLine + Description;
        }

        /// <summary>
        /// Liefert den HashCode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return Description.GetHashCode() ^ Name.GetHashCode() ^ Title.GetHashCode() ^ Link.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein object mit dieser Instanz anhand des HashCodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn object dieser Instanz entspricht, andernfalls false.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedTextInput && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
