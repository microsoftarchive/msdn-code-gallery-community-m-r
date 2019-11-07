using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert eine Person, welche in einem Atom Feed eingetragen werden kann.
    /// </summary>
    public class AtomFeedArticlePerson
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedArticlePerson Klasse.
        /// Allen Eigenschaften ist danach ein Standartwert zugewiesen.
        /// </summary>
        public AtomFeedArticlePerson()
        {
            Name = "";
            Uri = "";
            EMail = "";
        }
        /// <summary>
        /// Ruft den Namend er Person ab oder legt diesen fest.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ruft die Webseite der Person ab oder legt diese fest.
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// Ruft die E-Mail dieser Person ab oder legt diese fest.
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// Liefert den Namend er Person.
        /// </summary>
        /// <returns>Der Name der Person.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode der Instanz.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Uri.GetHashCode() ^ EMail.GetHashCode();
        }
        /// <summary>
        /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is AtomFeedArticlePerson && obj.GetHashCode() == this.GetHashCode();
        }
    }
}
