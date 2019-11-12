using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Beschreibt einen Artikel in einem Atom Newsfeed.
    /// </summary>
    public class AtomFeedArticle : FeedArticleBase
    {
        /// <summary>
        /// Ruft den Titel des Artikels ab oder legt diesen fest.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Ruft die eindeutige ID des Artikels ab oder legt diese fest.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Ruft das Datum der letzten aktualisierung des Artikels ab oder legt dieses fest.
        /// </summary>
        public DateTime Updated { get; set; }
        /// <summary>
        /// Ruft die Autoren des Artikels ab oder legt diese fest.
        /// </summary>
        public List<AtomFeedArticlePerson> Authors { get; set; }
        /// <summary>
        /// Ruft die Contributoren des Artikels ab oder legt diese fest.
        /// </summary>
        public List<AtomFeedArticlePerson> Contributors { get; set; }
        /// <summary>
        /// Ruft die Links des Artikels ab oder legt diese fest.
        /// </summary>
        public List<AtomFeedLink> Links { get; set; }
        /// <summary>
        /// Ruftz die Kategorien des Artikels ab oder legt diese fest.
        /// </summary>
        public List<AtomFeedCategory> Categories { get; set; }
        /// <summary>
        /// Ruft den Content (inhalt-formatiert) des Artikels ab oder legt diesen fest.
        /// </summary>
        public AtomFeedText Content { get; set; }
        /// <summary>
        /// Ruft den Content (inhalt-plain) des Artikels ab oder legt diesen fest.
        /// </summary>
        public AtomFeedText Summary { get; set; }

        /// <summary>
        /// Liefert den Titel des Artikels.
        /// </summary>
        /// <returns>Der Titel des Artikels.</returns>
        public override string ToString()
        {
            return Title;
        }

        /// <summary>
        /// Liefert den Hashcode dieser Instanz.
        /// </summary>
        /// <returns>Der Hashcode der Instanz.</returns>
        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ ID.GetHashCode() ^ Updated.GetHashCode() ^ Authors.GetHashCode()
                ^ Contributors.GetHashCode() ^ Links.GetHashCode() ^ Categories.GetHashCode()
                ^ Content.GetHashCode() ^ Summary.GetHashCode() ^ Published.GetHashCode();
        }

        /// <summary>
        /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
        /// </summary>
        /// <param name="obj">Das zu vergleichende object.</param>
        /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is AtomFeedArticle && obj.GetHashCode() == this.GetHashCode();
        }
    }
}
