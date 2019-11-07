using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Koopakiller.NewsFeed
{
    /// <summary>
    /// Beschreibt einen Artikel in einem RSS Newsfeed.
    /// </summary>
    public class RSSFeedArticle : FeedArticleBase
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der RSSFeedArticle Klasse. Alle Eigenschaften haben danach einen Standartwert.
        /// </summary>
        public RSSFeedArticle()
        {
            Categories = new List<RSSFeedCategory>();
            Guid = new RSSFeedArticleGuid();
            Comments = "";
            Enclosure = new RSSFeedArticleEnclosure();
            Source = new RSSFeedArticleSource();
            Title = "";//Der Titel des Artikels
            Author = "";//Autor
            ArticleUrl = "";//Link zum Vollständigen Artikel
            Content = "";//Inhalt des Artikels
        }

        #region Eigenschaften

        /// <summary>
        /// Ruft den Inhalt des Artikels ab oder legt diesen fest.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Ruft den Link zum vollständigen Inhalt ab oder legt diesen fest.
        /// </summary>
        public string ArticleUrl { get; set; }
        /// <summary>
        /// Ruft den Autor des Artikels ab oder legt diesen fest.
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Ruft den Titel des Artikels ab oder legt diesen fest.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Ruft die Kategorien des Artikels ab oder legt diese fest.
        /// </summary>
        public List<RSSFeedCategory> Categories { get; set; }
        /// <summary>
        /// Ruft die GUID des Artikels ab oder legt diese fest.
        /// </summary>
        public RSSFeedArticleGuid Guid { get; set; }
        /// <summary>
        /// Ruft einen Pfad ab, der angibt wo Kommentare zu dem Artikel zu sehen sind oder legt diesen fest.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Ruft einen Pfad zu einem Medienanhang, welches zu dem Artikel gehört ab oder legt diesen fest.
        /// </summary>
        public RSSFeedArticleEnclosure Enclosure { get; set; }
        /// <summary>
        /// Ruft die Source Eigenschaft des RSS Feed Artikels ab oder legt diese fest.
        /// </summary>
        public RSSFeedArticleSource Source { get; set; }

        #endregion

        #region override object

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
        /// <returns>Der Hashcode dieser Instanz.</returns>
        public override int GetHashCode()
        {
            return ArticleUrl.GetHashCode() ^ Author.GetHashCode() ^ Categories.GetHashCode() ^ Comments.GetHashCode()
                ^ Content.GetHashCode() ^ Enclosure.GetHashCode() ^ Guid.GetHashCode() ^ Guid.IsPermaLink.GetHashCode()
                ^ Published.GetHashCode() ^ Source.GetHashCode() ^ Title.GetHashCode();
        }
        /// <summary>
        /// Vergleicht ein object mit dieser Instanz auf gleichheit.
        /// </summary>
        /// <param name="obj">Das zu verglichende object.</param>
        /// <returns>True, wenn das object mit dieser Instanz übereinstimmt, anderfalls False.</returns>
        public override bool Equals(object obj)
        {
            return obj is RSSFeedArticle && obj.GetHashCode() == this.GetHashCode();
        }

        #endregion
    }
}
