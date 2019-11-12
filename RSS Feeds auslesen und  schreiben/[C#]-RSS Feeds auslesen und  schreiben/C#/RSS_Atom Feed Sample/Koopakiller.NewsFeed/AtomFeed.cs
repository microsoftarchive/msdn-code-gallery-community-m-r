using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Koopakiller.NewsFeed
{
        /// <summary>
        /// Stellt einen Newfeed im Atom Format dar.
        /// </summary>
        public class AtomFeed : FeedBase
        {
            /// <summary>
            /// Initialisiert eine neue Instanz der Atomeed Klasse.
            /// Alle Eigenschaften haben danach ihren Standartwert zugewiesen.
            /// </summary>
            public AtomFeed()
            {
                Title = new AtomFeedText();
                SubTitle = new AtomFeedText();
                ID = "";
                Links = new List<AtomFeedLink>();
                Generator = new AtomFeedGenerator();
                Logo = "";
                Authors = new List<AtomFeedArticlePerson>();
            }

            #region Load & Save

            /// <summary>
            /// Lädt den Inhalt eines Atom Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des Atom Feeds.
            /// </summary>
            /// <param name="path">Der Pfad der zu ladenden Datei.</param>
            public override void Load(string path)
            {
                XDocument doc = XDocument.Load(path);
                SetDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines Atom Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des Atom Feeds.
            /// </summary>
            /// <param name="stream">Ein Stream, aus welchem gelesen werden soll.</param>
            public override void Load(Stream stream)
            {
                XDocument doc = XDocument.Load(stream);
                SetDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines Atom Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des Atom Feeds.
            /// </summary>
            /// <param name="textReader">Ein TextReader, aus welchem gelesen werden soll.</param>
            public override void Load(TextReader textReader)
            {
                XDocument doc = XDocument.Load(textReader);
                SetDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines Atom Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des Atom Feeds.
            /// </summary>
            /// <param name="xmlReader">Ein XmlReader, aus welchem gelesen werden soll.</param>
            public override void Load(XmlReader xmlReader)
            {
                XDocument doc = XDocument.Load(xmlReader);
                SetDocument(doc);
            }

            /// <summary>
            /// Speichert den Atom Feed an dem angegeben Ort als Datei ab.
            /// </summary>
            /// <param name="path">Der Pfad, wo der RSS Feed gespeichert werden soll.</param>
            public override void Save(string path)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(path);
            }
            /// <summary>
            /// Schreibt den Atom Feed im angegebenen Stream.
            /// </summary>
            /// <param name="stream">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(Stream stream)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(stream);
            }
            /// <summary>
            /// Schreibt den Atom Feed im angegebenen Stream.
            /// </summary>
            /// <param name="xmlWriter">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(TextWriter xmlWriter)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(xmlWriter);
            }
            /// <summary>
            /// Schreibt den Atom Feed im angegebenen Stream.
            /// </summary>
            /// <param name="textWriter">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(XmlWriter textWriter)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(textWriter);
            }

            #endregion

            private void SetDocument(XDocument doc)
            {
                XNamespace ns = @"http://www.w3.org/2005/Atom";
                XElement el = doc.Descendants(ns + "feed").Count() == 1
                    ? doc.Descendants(ns + "feed").Single()
                    : null;

                if (el == null)
                    throw new InvalidCastException("Invalid Atom file");

                //<feed xml:lang="..." xmlns="...">...</feed>
                if (el.Attributes(XNamespace.Xml + "lang").Count() == 1)
                    Language = new CultureInfo(el.Attributes(XNamespace.Xml + "lang").Single().Value);

                //<title>...</title>
                if (el.Elements(ns + "title").Count() == 1)
                    Title = new AtomFeedText()
                    {
                        Text = el.Elements(ns + "title").Single().Value,
                        Type = el.Elements(ns + "title").Attributes("type").Count() == 1 ? el.Elements(ns + "title").Attributes("type").Single().Value : "",
                    };
                else
                    throw new InvalidCastException("Invalid Atom file.\title do not exists.");
                //<subtitle>...</subtitle>
                if (el.Elements(ns + "subtitle").Count() == 1)
                    SubTitle = new AtomFeedText()
                    {
                        Text = el.Elements(ns + "subtitle").Single().Value,
                        Type = el.Elements(ns + "subtitle").Attributes("type").Count() == 1 ? el.Elements(ns + "subtitle").Attributes("type").Single().Value : "",
                    };
                else
                    throw new InvalidCastException("Invalid Atom file.\nsubtitle do not exists.");
                //<updated>...</updated>
                if (el.Elements(ns + "updated").Count() == 1)
                    Published = DateTime.Parse(el.Elements(ns + "updated").Single().Value);
                //<id>MyFeed</id>
                if (el.Elements(ns + "id").Count() == 1)
                    ID = el.Elements(ns + "id").Single().Value;
                //<link>...</link>
                Links = (from x in el.Elements(ns + "link")
                         select new AtomFeedLink()
                         {
                             Target = x.Attributes("href").Count() == 1 ? x.Attributes("href").Single().Value : "",
                             TargetLanguage = x.Attributes("hreflang").Count() == 1 ? x.Attributes("hreflang").Single().Value : "",
                             Type = x.Attributes("type").Count() == 1 ? x.Attributes("type").Single().Value : "",
                             Relation = x.Attributes("rel").Count() == 1 ? x.Attributes("rel").Single().Value : "",
                             Length = x.Attributes("length").Count() == 1 ? ulong.Parse(x.Attributes("length").Single().Value) : ulong.MinValue,
                         }).ToList();
                //<rights>Copyright (c) 2012</rights>
                if (el.Elements(ns + "rights").Count() == 1)
                    Copyrights = el.Elements(ns + "rights").Single().Value;
                //<generator url="..." version="...">...</generator>
                if (el.Elements(ns + "generator").Count() == 1)
                    Generator = new AtomFeedGenerator()
                    {
                        Name = el.Elements(ns + "generator").Single().Value,
                        Version = el.Elements(ns + "generator").Single().Attributes("version").Count() == 1 ? Version.Parse(el.Elements(ns + "generator").Single().Attributes("version").Single().Value) : new Version(0, 0, 0, 0),
                        Url = el.Elements(ns + "generator").Single().Attributes("uri").Count() == 1 ? el.Elements(ns + "generator").Single().Attributes("uri").Single().Value : "",
                    };
                //<author><name>MyName</name></author>
                Authors = (from c in el.Elements(ns + "author")
                           select new AtomFeedArticlePerson()
                           {
                               EMail = c.Elements(ns + "email").Count() == 1 ? c.Elements(ns + "email").Single().Value : "",
                               Name = c.Elements(ns + "name").Count() == 1 ? c.Elements(ns + "name").Single().Value : "",
                               Uri = c.Elements(ns + "uri").Count() == 1 ? c.Elements(ns + "uri").Single().Value : "",
                           }).ToList();
                //<logo>MyName</logo>
                if (el.Elements(ns + "logo").Count() == 1)
                    Logo = el.Elements(ns + "logo").Single().Value;
                //<entry>...</entry>
                Articles = (from x in el.Elements(ns + "entry")
                            select (FeedArticleBase)new AtomFeedArticle()
                            {
                                Title = x.Elements(ns + "title").Count() == 1 ? x.Elements(ns + "title").Single().Value : "",
                                ID = x.Elements(ns + "id").Count() == 1 ? x.Elements(ns + "id").Single().Value : "",
                                Published = x.Elements(ns + "published").Count() == 1 ? DateTime.Parse(x.Elements(ns + "published").Single().Value) : DateTime.MinValue,
                                Updated = x.Elements(ns + "updated").Count() == 1 ? DateTime.Parse(x.Elements(ns + "updated").Single().Value) : DateTime.MinValue,
                                Authors = (from c in x.Elements(ns + "author")
                                           select new AtomFeedArticlePerson()
                                           {
                                               EMail = c.Elements(ns + "email").Count() == 1 ? c.Elements(ns + "email").Single().Value : "",
                                               Name = c.Elements(ns + "name").Count() == 1 ? c.Elements(ns + "name").Single().Value : "",
                                               Uri = c.Elements(ns + "uri").Count() == 1 ? c.Elements(ns + "uri").Single().Value : "",
                                           }).ToList(),
                                Contributors = (from c in x.Elements(ns + "contributor")
                                                 select new AtomFeedArticlePerson()
                                                 {
                                                     EMail = c.Elements(ns + "email").Count() == 1 ? c.Elements(ns + "email").Single().Value : "",
                                                     Name = c.Elements(ns + "name").Count() == 1 ? c.Elements(ns + "name").Single().Value : "",
                                                     Uri = c.Elements(ns + "uri").Count() == 1 ? c.Elements(ns + "uri").Single().Value : "",
                                                 }).ToList(),
                                Links = (from c in x.Elements(ns + "link")
                                         select new AtomFeedLink()
                                         {
                                             Target = c.Attributes("href").Count() == 1 ? c.Attributes("href").Single().Value : "",
                                             TargetLanguage = c.Attributes("hreflang").Count() == 1 ? c.Attributes("hreflang").Single().Value : "",
                                             Type = c.Attributes("type").Count() == 1 ? c.Attributes("type").Single().Value : "",
                                             Relation = c.Attributes("rel").Count() == 1 ? c.Attributes("rel").Single().Value : "",
                                             Length = c.Attributes("length").Count() == 1 ? ulong.Parse(c.Attributes("length").Single().Value) : ulong.MinValue,
                                         }).ToList(),
                                Categories = (from c in x.Elements(ns + "category")
                                              select new AtomFeedCategory()
                                              {
                                                  CategoryName = c.Attributes("label").Count() == 1 ? c.Attributes("label").Single().Value : "",
                                                  Term = c.Attributes("term").Count() == 1 ? c.Attributes("term").Single().Value : "",
                                              }).ToList(),
                                Content = new AtomFeedText()
                                {
                                    Type = x.Elements(ns + "content").Count() == 1 ? x.Elements(ns + "content").Single().Attributes("type").Count() == 1 ? x.Elements(ns + "content").Single().Attributes("type").Single().Value : "" : "",
                                    Text = x.Elements(ns + "content").Count() == 1
                                        //? x.Elements(ns + "content").Single().HasElements
                                            ? x.Elements(ns + "content").Single().FirstNode.NodeType == XmlNodeType.Text || x.Elements(ns + "content").Single().FirstNode.NodeType == XmlNodeType.CDATA
                                                ? x.Elements(ns + "content").Single().Value
                                                : string.Join("", x.Elements(ns + "content").Single().Nodes().Select(n => n.ToString()))
                                        //    : ""
                                        : "",
                                },
                                Summary = new AtomFeedText()
                                {
                                    Type = x.Elements(ns + "summary").Count() == 1 ? x.Elements(ns + "summary").Single().Attributes("type").Count() == 1 ? x.Elements(ns + "summary").Single().Attributes("type").Single().Value : "" : "",
                                    Text = x.Elements(ns + "summary").Count() == 1
                                        //? x.Elements(ns + "summary").Single().HasElements
                                            ? x.Elements(ns + "summary").Single().FirstNode.NodeType == XmlNodeType.Text || x.Elements(ns + "summary").Single().FirstNode.NodeType == XmlNodeType.CDATA
                                                ? x.Elements(ns + "summary").Single().Value
                                                : string.Join("", x.Elements(ns + "summary").Single().Nodes().Select(n => n.ToString()))
                                        //    : ""
                                        : "",
                                },
                            }).ToList();
            }

            private XDocument GetDocumentFromData()
            {
                XDocument doc = new XDocument();
                XElement feed = new XElement("feed");

                if (Copyrights != "")
                    feed.Add(new XElement("rights")
                    {
                        Value = Copyrights,
                    });
                if (Language != CultureInfo.InvariantCulture)
                    feed.Add(new XAttribute(XNamespace.Xml + "lang", Language.IetfLanguageTag));
                if (Published != DateTime.MinValue)
                    feed.Add(new XElement("updated")
                    {
                        Value = Published.ToString("R"),
                    });
                if (Title.Text != "")
                {
                    XElement el = new XElement("title");
                    el.Value = Title.Text;
                    if (Title.Type != "" && Title.Type != "text")
                        el.Add(new XAttribute("type", Title.Type));
                    feed.Add(el);
                }
                else
                    throw new ArgumentNullException("AtomFeed.Title", "A title is required in Atom documents.");

                if (SubTitle.Text != "")
                {
                    XElement el = new XElement("subtitle");
                    el.Value = SubTitle.Text;
                    if (SubTitle.Type != "" && SubTitle.Type != "text")
                        el.Add(new XAttribute("type", SubTitle.Type));
                    feed.Add(el);
                }
                else
                    throw new ArgumentNullException("AtomFeed.SubTitle", "A subtitle is required in Atom documents.");
                if (ID != "")
                    feed.Add(new XElement("id")
                    {
                        Value = ID,
                    });
                if (Logo != "")
                    feed.Add(new XElement("logo")
                    {
                        Value = Logo,
                    });
                if (Generator.Name != "")
                {
                    XElement el = new XElement("generator");
                    el.Value = Generator.Name;
                    if (Generator.Url != "")
                        el.Add(new XAttribute("url", Generator.Url));
                    if (Generator.Version != new Version(0,0,0,0))
                        el.Add(new XAttribute("version", Generator.Version));
                    feed.Add(el);
                }
                foreach (AtomFeedLink link in Links)
                {
                    XElement el = new XElement("link");
                    if (link.Length != 0)
                        el.Add(new XAttribute("length", link.Length));
                    if (link.Relation != "")
                        el.Add(new XAttribute("rel", link.Relation));
                    if (link.Target != "")
                        el.Add(new XAttribute("href", link.Target));
                    else
                        throw new ArgumentNullException("AtomFeed.Link.Target", "A target is required in links for Atom documents.");
                    if (link.TargetLanguage != "")
                        el.Add(new XAttribute("href", link.TargetLanguage));
                    if (link.Type != "")
                        el.Add(new XAttribute("type", link.Type));
                    feed.Add(el);
                }
                foreach (AtomFeedArticlePerson author in Authors)
                {
                    XElement el = new XElement("author");
                    if (author.Name != "")
                        el.Add(new XElement("name", author.Name));
                    else
                        throw new ArgumentNullException("AtomFeed.Author.Name", "A name is required for authors in Atom documents.");
                    if (author.Uri != "")
                        el.Add(new XElement("uri", author.Uri));
                    if (author.EMail != "")
                        el.Add(new XElement("email", author.EMail));
                    feed.Add(el);
                }

                foreach(AtomFeedArticle article in Articles)
                {
                    XElement el = new XElement("entry");
                    if (article.Title != "")
                        el.Add(new XElement("title", article.Title));
                    else
                        throw new ArgumentNullException("AtomFeed.Article.Title", "A title is required for articles in Atom documents.");
                    if (article.Content.Text != "")
                    {
                        XCData cd = new XCData(article.Content.Text);
                        XElement c = new XElement("content");
                        c.Add(cd);
                        if (article.Content.Type != "")
                            c.Add(new XAttribute("type", article.Content.Type));
                        el.Add(c);
                    }
                    if (article.Summary.Text != "")
                    {
                        XCData cd = new XCData(article.Summary.Text);
                        XElement c = new XElement("summary");
                        c.Add(cd);
                        if (article.Summary.Type != "")
                            c.Add(new XAttribute("type", article.Summary.Type));
                        el.Add(c);
                    }
                    foreach (AtomFeedArticlePerson con in article.Contributors)
                    {
                        XElement e = new XElement("contributor");
                        if (con.Name != "")
                            e.Add(new XElement("name", con.Name));
                        else
                            throw new ArgumentNullException("AtomFeedArticle.Contributor.Name", "A name is required in contributors for Atom document articles.");
                        if (con.EMail != "")
                            e.Add(new XElement("email", con.EMail));
                        if (con.Uri != "")
                            e.Add(new XElement("uri", con.Uri));
                        el.Add(e);
                    }
                    foreach (AtomFeedArticlePerson pers in article.Authors)
                    {
                        XElement e = new XElement("author");
                        if (pers.Name != "")
                            e.Add(new XElement("name", pers.Name));
                        else
                            throw new ArgumentNullException("AtomFeedArticle.Contributor.Name", "A name is required in contributors for Atom document articles.");
                        if (pers.EMail != "")
                            e.Add(new XElement("email", pers.EMail));
                        if (pers.Uri != "")
                            e.Add(new XElement("uri", pers.Uri));
                        el.Add(e);
                    }
                    foreach (AtomFeedCategory cat in article.Categories)
                    {
                        XElement e = new XElement("category");
                        if (cat.CategoryName != "")
                            e.Add(new XAttribute("label", cat.CategoryName));
                        else
                            throw new ArgumentNullException("AtomFeedArticle.Categrory.CategoryName", "A category name is required in category for Atom document articles.");
                        if (cat.Term != "")
                            e.Add(new XAttribute("term", cat.Term));
                        else
                            throw new ArgumentNullException("AtomFeedArticle.Categrory.Term", "A term is required in category for Atom document articles.");
                        el.Add(e);
                    }
                    if (article.ID != "")
                        el.Add(new XElement("id", article.ID));
                    foreach (AtomFeedLink link in article.Links)
                    {
                        XElement e = new XElement("link");
                        if (link.Length != 0)
                            e.Add(new XAttribute("length", link.Length));
                        if (link.Relation != "")
                            e.Add(new XAttribute("rel", link.Relation));
                        if (link.Target != "")
                            e.Add(new XAttribute("href", link.Target));
                        else
                            throw new ArgumentNullException("AtomFeedArticle.Link.Target", "A target is required in links for Atom document articles.");
                        if (link.TargetLanguage != "")
                            e.Add(new XAttribute("href", link.TargetLanguage));
                        if (link.Type != "")
                            e.Add(new XAttribute("type", link.Type));
                        el.Add(e);
                    }
                    if (article.Published != DateTime.MinValue)
                        el.Add(new XElement("published", article.Published.ToString("R")));
                    if (article.Updated != DateTime.MinValue)
                        el.Add(new XElement("updated", article.Updated.ToString("R")));
                    feed.Add(el);
                }
                
                doc.Add(feed);
                return doc;
            }

            #region Eigenschaften

            /// <summary>
            /// Ruft den Titel des Feeds ab oder legt diesen fest.
            /// </summary>
            public AtomFeedText Title { get; set; }
            /// <summary>
            /// Ruft die Beschreibung des Feeds ab oder legt diese fest.
            /// </summary>
            public AtomFeedText SubTitle { get; set; }
            /// <summary>
            /// Ruft eine eindeutige ID des Feeds ab oder legt diese fest.
            /// </summary>
            public string ID { get; set; }
            /// <summary>
            /// Ruft eine Auflistung von Links des Feeds ab oder legt diese fest.
            /// </summary>
            public List<AtomFeedLink> Links { get; set; }
            /// <summary>
            /// Ruft den Generator des Feeds ab oder legt diesen fest.
            /// </summary>
            AtomFeedGenerator Generator { get; set; }
            /// <summary>
            /// Ruft die Url des Logos des Feeds ab oder legt diese fest.
            /// </summary>
            public string Logo { get; set; }
            /// <summary>
            /// Ruft den Author des Atom Feeds ab oder legt diesen fest.
            /// </summary>
            public List<AtomFeedArticlePerson> Authors { get; set; }

            #endregion

            #region override object

            /// <summary>
            /// Liefert den Titel und die Beschreibung des Atom Feeds, getrennt durch einen Zeilenumbruch.
            /// </summary>
            /// <returns>Der Titel und die Beschreibung des Atom Feeds, getrennt durch einen Zeilenumbruch.</returns>
            public override string ToString()
            {
                return Title + Environment.NewLine + SubTitle;
            }

            /// <summary>
            /// Liefert den Hashcode dieser Instanz.
            /// </summary>
            /// <returns>Der Hashcode der Instanz.</returns>
            public override int GetHashCode()
            {
                return Title.GetHashCode() ^ SubTitle.GetHashCode() ^ ID.GetHashCode() ^ Links.GetHashCode()
                    ^ Generator.GetHashCode() ^ Logo.GetHashCode() ^ Authors.GetHashCode()
                    ^ Copyrights.GetHashCode() ^ Published.GetHashCode() ^ Language.GetHashCode() ^ Articles.GetHashCode();
            }

            /// <summary>
            /// Vergleicht ein objkect mit dieser Instanz anhand des Hashcodes.
            /// </summary>
            /// <param name="obj">Das zu vergleichende object.</param>
            /// <returns>True, wenn der Type und der Hashcode des objects und dieser Instanz übereinstimmen, andernfalls False.</returns>
            public override bool Equals(object obj)
            {
                return obj is AtomFeed & obj.GetHashCode() == this.GetHashCode();
            }

            #endregion
        }
}
