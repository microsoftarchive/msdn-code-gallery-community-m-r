using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Repräsentiert eine Eigenschaft, vom Typ Text, bei welcher der Text sowie der Typ angegeben werden kann. Der Standartwert ist "text" für den Typ.
    /// </summary>
    public class AtomFeedText
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der AtomFeedText Klasse.
        /// Allen Eigenschaften ist danach ein Standartwert zugewiesen.
        /// </summary>
        public AtomFeedText()
        {
            Text = "";
            Type = "text";
        }
        /// <summary>
        /// Ruft dn Text des Elements ab oder legt diesen fest.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Ruft den Typ des Textformats ab oder legt diesen fest.
        /// </summary>
        /// <example>text</example>
        /// <example>html</example>
        /// <example>xhtml</example>
        public string Type { get; set; }
    }
}
