using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert den Namen des Programms, mit dem der Feed erstellt wurde mit zusätzlichen Parametern für Version und eine Internetadresse.
    /// </summary>
    public class AtomFeedGenerator
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedGenerator Klasse. Alle Eigenschaften werden auf einen Standartwert gesetzt.
        /// </summary>
        public AtomFeedGenerator()
        {
            Url = "";
            Version = new Version(0, 0, 0, 0);
            Name = "";
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedGenerator Klasse. Alle Eigenschaften werden auf einen Standartwert gesetzt.
        /// </summary>
        /// <param name="name">Der Name des Generators.</param>
        public AtomFeedGenerator(string name)
        {
            Url = "";
            Version = new Version(-1, -1, -1, -1);
            Name = name;
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedGenerator Klasse. Alle Eigenschaften werden auf einen Standartwert gesetzt.
        /// </summary>
        /// <param name="name">Der Name des Generators.</param>
        /// <param name="url">Die Internetadresse des Herstellers bzw. des Programms.</param>
        public AtomFeedGenerator(string name, string url)
        {
            Url = "";
            Version = new Version(-1, -1, -1, -1);
            Name = name;
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedGenerator Klasse. Alle Eigenschaften werden auf einen Standartwert gesetzt.
        /// </summary>
        /// <param name="name">Der Name des Generators.</param>
        /// <param name="version">Die Version des Programms.</param>
        public AtomFeedGenerator(string name, Version version)
        {
            Url = "";
            Version = version;
            Name = name;
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedGenerator Klasse. Alle Eigenschaften werden auf einen Standartwert gesetzt.
        /// </summary>
        /// <param name="name">Der Name des Generators.</param>
        /// <param name="url">Die Internetadresse des Herstellers bzw. des Programms.</param>
        /// <param name="version">Die Version des Programms.</param>
        public AtomFeedGenerator(string name, string url, Version version)
        {
            Url = "";
            Version = version;
            Name = name;
        }

        /// <summary>
        /// Ruft die Url des Generators bzw. die des Herstellers ab oder legt diese fest.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Ruft die Version des Generators ab oder legt diese fest.
        /// </summary>
        public Version Version { get; set; }
        /// <summary>
        /// Ruft den Namen des Generators ab oder legt diesen fest.
        /// </summary>
        public string Name { get; set; }

        #region override object

        /// <summary>
        /// Liefert den Namen des Generators.
        /// </summary>
        /// <returns>Der Name des Generators.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Liefert den HashCode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return Url.GetHashCode() ^ Version.GetHashCode() ^ Name.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein object mit dieser Instanz anhand des HashCodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn object dieser Instanz entspricht, andernfalls false.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeed && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
