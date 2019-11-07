using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert einen Link (&lt;link&gt;-Tag) in einem Atom Feed.
    /// </summary>
    public class AtomFeedLink
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedLink Klasse. 
        /// Allen Eigenschaften wird ein Standartwert zugewiesen.
        /// </summary>
        public AtomFeedLink()
        {
            Target = "";
            Type = "";
            TargetLanguage = "";
            Relation = "";
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedLink Klasse. 
        /// Allen Eigenschaften wird ein Standartwert zugewiesen.
        /// </summary>
        /// <param name="target">Das Ziel des Links.</param>
        public AtomFeedLink(string target)
        {
            Target = target;
            Type = "";
            TargetLanguage = "";
            Relation = "";
        }
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedLink Klasse. 
        /// Allen Eigenschaften wird ein Standartwert zugewiesen.
        /// </summary>
        /// <param name="target">Das Ziel des Links.</param>
        /// <param name="type">Der Typ des Ziels.</param>
        /// <param name="targetlanguage">Die Sprache des Ziels.</param>
        /// <param name="relation">Die Relation, wo das Ziel gefunden werden soll.</param>
        public AtomFeedLink(string target, string type, string targetlanguage, string relation)
        {
            Target = target;
            Type = type;
            TargetLanguage = targetlanguage;
            Relation = relation;
        }
        /// <summary>
        /// Ruft einen Wert der dem href-Attribut entspricht ab oder legt diesen fest.
        /// </summary>
        /// <example>http://msdn.microsoft.com/</example>
        public string Target { get; set; }
        /// <summary>
        /// Ruft einen Wert der dem type-Attribute entspricht ab oder legt diesen fest.
        /// </summary>
        /// <example>text/html</example>
        public string Type { get; set; }
        /// <summary>
        /// Ruft einen Wert der dem hreflang-Attribute entspricht ab oder legt diesen fest.
        /// </summary>
        /// <example>de-DE</example>
        public string TargetLanguage { get; set; }
        /// <summary>
        /// Ruft einen Wert der dem rel-Attribut entspricht ab oder legt diesen fest.
        /// </summary>
        /// <example>self, alternate</example>
        public string Relation { get; set; }
        /// <summary>
        /// Ruft einen Wert der dem length-Attribut entspricht ab oder legt diesen fest.
        /// </summary>
        public ulong Length { get; set; }
    }
}
