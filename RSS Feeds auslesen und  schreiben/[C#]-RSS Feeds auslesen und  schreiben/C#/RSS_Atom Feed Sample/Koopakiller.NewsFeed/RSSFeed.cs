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
        /// Stellt einen Newsfeed im RSS 1.0 oder 2.0 Format dar.
        /// Hinweis: Der &lt;cloud&gt; wird (noch) nicht unterstützt.
        /// </summary>
        public class RSSFeed : FeedBase
        {
            /// <summary>
            /// Initialisiert eine neue Instanz der RSSFeed Klasse.
            /// Allen Eigenschaften wird ein Standartwert zugewiesen.
            /// </summary>
            public RSSFeed()
            {
                Description = "";
                Image = new RSSFeedImage();
                ImageDescription = "";
                Categories = new List<RSSFeedCategory>();
                Docs = "";
                Updated = DateTime.MinValue;
                SkipDays = new List<string>();
                SkipHours = new List<byte>();
                TimeToLive = int.MinValue;
                WebMaster = "";
                TextInput = new RSSFeedTextInput();
                ArticleUrl = "";
                Title = "";
                Generator = "";
                Version = "2.0";
                Author = "";
            }

            #region Load & Save

            /// <summary>
            /// Lädt den Inhalt eines RSS Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des RSS Feeds.
            /// </summary>
            /// <param name="path">Der Pfad der zu ladenden Datei.</param>
            public override void Load(string path)
            {
                XDocument doc = XDocument.Load(path);
                SetDataFromDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines RSS Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des RSS Feeds.
            /// </summary>
            /// <param name="stream">Ein Stream, aus welchem gelesen werden soll.</param>
            public override void Load(Stream stream)
            {
                XDocument doc = XDocument.Load(stream);
                SetDataFromDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines RSS Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des RSS Feeds.
            /// </summary>
            /// <param name="xmlReader">Ein XmlReader, aus welchem gelesen werden soll.</param>
            public override void Load(XmlReader xmlReader)
            {
                XDocument doc = XDocument.Load(xmlReader);
                SetDataFromDocument(doc);
            }
            /// <summary>
            /// Lädt den Inhalt eines RSS Feeds.
            /// Die Eigenschaften enthalten danach die Eigenschaften des RSS Feeds.
            /// </summary>
            /// <param name="textReader">Ein TextReader, aus welchem gelesen werden soll.</param>
            public override void Load(TextReader textReader)
            {
                XDocument doc = XDocument.Load(textReader);
                SetDataFromDocument(doc);
            }

            /// <summary>
            /// Speichert den RSS Feed an dem angegeben Ort als Datei ab.
            /// </summary>
            /// <param name="path">Der Pfad, wo der RSS Feed gespeichert werden soll.</param>
            public override void Save(string path)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(path);
            }
            /// <summary>
            /// Schreibt den RSS Feed im angegebenen Stream.
            /// </summary>
            /// <param name="stream">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(Stream stream)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(stream);
            }
            /// <summary>
            /// Schreibt den RSS Feed im angegebenen Stream.
            /// </summary>
            /// <param name="xmlWriter">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(XmlWriter xmlWriter)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(xmlWriter);
            }
            /// <summary>
            /// Schreibt den RSS Feed im angegebenen Stream.
            /// </summary>
            /// <param name="textWriter">Der Stream, in den geschrieben werden soll.</param>
            public override void Save(TextWriter textWriter)
            {
                XDocument doc = GetDocumentFromData();
                doc.Save(textWriter);
            }

            #endregion

            /// <summary>
            /// Liefert ein XDocument, welches die Daten des Feeds im RSS Format beinhaltet.
            /// </summary>
            /// <returns>Ein XDokument mit den Daten des Feeds im RSS Format.</returns>
            private XDocument GetDocumentFromData()
            {
                XDocument doc = new XDocument();
                XElement root = doc.Root;
                XElement rss = new XElement("rss");
                XElement channel = new XElement("channel");

                if (Categories.Count > 0)
                {
                    foreach (RSSFeedCategory cat in Categories)
                    {
                        XElement el = new XElement("category")
                        {
                            Value = cat.CategoryName,
                        };
                        if (cat.Domain != "")
                        {
                            XAttribute at = new XAttribute("domain", cat.Domain);
                            el.Add(at);
                        }

                        channel.Add(el);
                    }
                }

                if (Copyrights != "")
                {
                    channel.Add(new XElement("copyright")
                    {
                        Value = Copyrights,
                    });
                }

                if (Description != "")
                {
                    channel.Add(new XElement("description")
                    {
                        Value = Description,
                    });
                }
                else
                {
                    throw new ArgumentNullException("RSSFeed.Description", "A description is required in RSS documents.");
                }

                if (Docs != "")
                {
                    channel.Add(new XElement("docs")
                    {
                        Value = Docs,
                    });
                }

                if (Generator != "")
                {
                    channel.Add(new XElement("generator")
                    {
                        Value = Generator,
                    });
                }

                if (Image.Url != "")
                {
                    if (Image.Link == "")
                        throw new ArgumentNullException("RSSFeed.ImageLink", "A link is required in RSS documents width image.");
                    if (Image.Title == "")
                        throw new ArgumentNullException("RSSFeed.ImageTitle", "A title is required in RSS documents width image.");
                    if (Image.Url == "")
                        throw new ArgumentNullException("RSSFeed.ImageUrl", "A url is required in RSS documents width image.");

                    XElement el = new XElement("image");
                    if (Image.Height != 31)
                    {
                        XAttribute imgHeight = new XAttribute("height", Image.Height.ToString());
                        el.Add(imgHeight);
                    }
                    if (Image.Width != 88)
                    {
                        XAttribute imgWidth = new XAttribute("width", Image.Width.ToString());
                        el.Add(imgWidth);
                    }
                    if (Description != "")
                    {
                        XAttribute imgDescription = new XAttribute("description", ImageDescription);
                        el.Add(imgDescription);
                    }

                    XAttribute imgLink = new XAttribute("link", Image.Link);
                    el.Add(imgLink);
                    XAttribute imgTitle = new XAttribute("title", Image.Title);
                    el.Add(imgTitle);
                    XAttribute imgUrl = new XAttribute("url", Image.Url);
                    el.Add(imgUrl);
                    channel.Add(el);
                }

                if (Language != CultureInfo.InvariantCulture)
                {
                    channel.Add(new XElement("language")
                    {
                        Value = Language.Name,
                    });
                }

                if (Updated != DateTime.MinValue)
                {
                    channel.Add(new XElement("lastBuildDate")
                    {
                        Value = Updated.ToString("R"),
                    });
                }

                if (ArticleUrl != "")
                {
                    channel.Add(new XElement("link")
                    {
                        Value = ArticleUrl,
                    });
                }
                else
                {
                    throw new ArgumentNullException("RSSFeed.ArticleUrl", "A url to the articel is required in RSS documents.");
                }

                if (Author != "")
                {
                    channel.Add(new XElement("managingEditor")
                    {
                        Value = Author,
                    });
                }

                if (Published != DateTime.MinValue)
                {
                    channel.Add(new XElement("pubDate")
                    {
                        Value = Updated.ToString("R"),
                    });
                }

                if (SkipDays.Count > 0)
                {
                    XElement e = new XElement("skipDays");
                    foreach (string s in SkipDays)
                    {
                        XElement el = new XElement("day")
                        {
                            Value = s,
                        };

                        e.Add(el);
                    }
                    channel.Add(e);
                }

                if (SkipHours.Count > 0)
                {
                    XElement e = new XElement("skipHours");
                    foreach (byte s in SkipHours)
                    {
                        XElement el = new XElement("hour")
                        {
                            Value = s.ToString(),
                        };

                        e.Add(el);
                    }
                    channel.Add(e);
                }

                if (TextInput.Description != "")
                {
                    if (TextInput.Description == "")
                        throw new ArgumentNullException("RSSFeed.TextInputDescription", "A description is required in RSS documents width TextInput field.");
                    if (TextInput.Link == "")
                        throw new ArgumentNullException("RSSFeed.TextInputLink", "A link is required in RSS documents width TextInput field.");
                    if (TextInput.Name == "")
                        throw new ArgumentNullException("RSSFeed.TextInputName", "A name is required in RSS documents width TextInput field.");
                    if (TextInput.Title == "")
                        throw new ArgumentNullException("RSSFeed.TextInputTitle", "A title is required in RSS documents width TextInput field.");

                    XElement el = new XElement("textinput");

                    XAttribute tifDescription = new XAttribute("description", TextInput.Description);
                    el.Add(tifDescription);
                    XAttribute tifLink = new XAttribute("link", TextInput.Link);
                    el.Add(tifLink);
                    XAttribute tifName = new XAttribute("name", TextInput.Name);
                    el.Add(tifName);
                    XAttribute tifTitle = new XAttribute("title", TextInput.Title);
                    el.Add(tifTitle);

                    channel.Add(el);
                }

                if (Title != "")
                {
                    channel.Add(new XElement("title")
                    {
                        Value = Title,
                    });
                }
                else
                {
                    throw new ArgumentNullException("RSSFeed.Title", "A title is required in RSS documents.");
                }

                if (TimeToLive != int.MinValue)
                {
                    channel.Add(new XElement("ttl")
                    {
                        Value = TimeToLive.ToString(),
                    });
                }

                if (WebMaster != "")
                {
                    channel.Add(new XElement("webMaster")
                    {
                        Value = WebMaster,
                    });
                }

                if (Articles.Count > 0)
                {
                    foreach (RSSFeedArticle art in Articles)
                    {
                        XElement el = new XElement("item");

                        if (art.Author != "")
                            el.Add(new XElement("author")
                            {
                                Value = art.Author,
                            });

                        if (art.Comments != "")
                            el.Add(new XElement("comments")
                            {
                                Value = art.Comments,
                            });

                        if (art.Title != "")
                            el.Add(new XElement("title")
                            {
                                Value = art.Title,
                            });
                        else
                            throw new ArgumentNullException("RSSFeed.Item.Title", "A title is required in RSS articles.");

                        if (art.ArticleUrl != "")
                            el.Add(new XElement("link")
                            {
                                Value = art.ArticleUrl,
                            });
                        else
                            throw new ArgumentNullException("RSSFeed.Item.ArticleUrl", "A url to complete article is required in RSS articles.");

                        if (art.Content != "")
                        {
                            XElement e = new XElement("description");
                            e.Add(new XCData(art.Content));
                            el.Add(e);
                        }
                        else
                            throw new ArgumentNullException("RSSFeed.Item.Description", "A description is required in RSS articles.");

                        if (art.Published != DateTime.MinValue)
                            el.Add(new XElement("pubDate")
                            {
                                Value = art.Published.ToString("R"),
                            });

                        if (art.Author != "")
                            el.Add(new XElement("author")
                            {
                                Value = art.Author,
                            });

                        if (art.Source.Source != "")
                        {
                            XElement source = new XElement("source")
                            {
                                Value = art.Source.Source,
                            };
                            source.Add(new XAttribute("url", art.Source.Uri));
                            el.Add(source);
                        }

                        if (art.Guid.Guid != "")
                        {
                            XElement guid = new XElement("guid")
                            {
                                Value = art.Guid.Guid,
                            };
                            if (art.Guid.IsPermaLink == false)
                                guid.Add(new XAttribute("isPermaLink", art.Guid.IsPermaLink));
                            el.Add(guid);
                        }

                        if (art.Enclosure.Url != "")
                        {
                            if (art.Enclosure.Length == 0)
                                throw new ArgumentNullException("RSSFeed.Item.Enclosure.Length", "The length of enclosure is required in RSS articles with enclosure.");
                            if (art.Enclosure.Type == "")
                                throw new ArgumentNullException("RSSFeed.Item.Enclosure.Type", "The type of enclosure is required in RSS articles with enclosure.");
                            XElement enclosure = new XElement("enclosure");
                            enclosure.Add(new XAttribute("url", art.Enclosure.Url));
                            enclosure.Add(new XAttribute("length", art.Enclosure.Length));
                            enclosure.Add(new XAttribute("type", art.Enclosure.Type));
                            el.Add(enclosure);
                        }

                        if (art.Categories.Count > 0)
                        {
                            foreach (RSSFeedCategory cat in art.Categories)
                            {
                                XElement e = new XElement("category")
                                {
                                    Value = cat.CategoryName,
                                };
                                if (cat.Domain != "")
                                {
                                    XAttribute at = new XAttribute("domain", cat.Domain);
                                    e.Add(at);
                                }

                                channel.Add(el);
                            }
                        }

                        channel.Add(el);
                    }
                }

                rss.Add(new XAttribute("version", Version), channel);
                doc.Add(rss);
                return doc;
            }

            /// <summary>
            /// Lädt die Daten aus dem XDocument (im RSS Format) in die Eigenschaften dieser Instanz.
            /// </summary>
            /// <param name="doc">Das zu ladende RSS Dokument.</param>
            private void SetDataFromDocument(XDocument doc)
            {
                XElement el = doc.Descendants("rss").Count() == 1
                    ? doc.Descendants("rss").Descendants("channel").Count() == 1
                        ? doc.Descendants("rss").Descendants("channel").Single()
                        : null
                    : null;

                if (el == null)
                    throw new InvalidCastException("Invalid RSS file");

                //version="x.y"
                Version = doc.Descendants("rss").Attributes("version").Count() == 1 ? doc.Descendants("rss").Attributes("version").Single().Value : "2.0";

                //<category>...</categorie>
                Categories = (from c in el.Elements("category")
                              select new RSSFeedCategory()
                              {
                                  CategoryName = c.Value,
                                  Domain = c.Attributes("domain").Count() == 1 ? c.Attributes("domain").Single().Value : "",
                              }).ToList();
                //<skypedays>...</skipdays>
                SkipDays = (from c in el.Elements("skipDays").Elements("day")
                            select c.Value).ToList();
                //<skypehours>...</skypehours>
                SkipHours = (from c in el.Elements("skipHours").Elements("hour")
                             select byte.Parse(c.Value)).ToList();
                //<link>test.htm</link>
                if(el.Elements("link").Count() != 1)
                    throw new InvalidCastException("Invalid RSS file");
                ArticleUrl = el.Elements("link").Count() == 1 ? el.Elements("link").Single().Value : "";
                //<docs>rss.htm</docs>
                Docs = el.Elements("docs").Count() == 1 ? el.Elements("docs").Single().Value : "";
                //<webMaster>rss.htm</webMaster>
                WebMaster = el.Elements("webMaster").Count() == 1 ? el.Elements("webMaster").Single().Value : "";
                //<title>Title</title>
                if(el.Elements("title").Count() != 1)
                    throw new InvalidCastException("Invalid RSS file");
                Title = el.Elements("title").Count() == 1 ? el.Elements("title").Single().Value : "";
                //<description>Kurze Beschreibung</description>
                Description = el.Elements("description").Count() == 1 ? el.Elements("description").Single().Value : "";
                //<copyright>Copright</copyright>
                Copyrights = el.Elements("copyright").Count() == 1 ? el.Elements("copyright").Single().Value : "";
                //<pubDate>Sat, 20 Oct 2012 11:43:20 GMT</pubDate>
                Published = el.Elements("pubDate").Count() == 1 ? DateTime.Parse(el.Elements("pubDate").Single().Value) : DateTime.MinValue;
                //<ttl>60</ttl>
                TimeToLive = el.Elements("ttl").Count() == 1 ? int.Parse(el.Elements("ttl").Single().Value) : int.MinValue;
                //<pubDate>Sat, 20 Oct 2012 11:43:20 GMT</pubDate>
                Updated = el.Elements("lastBuildDate").Count() == 1 ? DateTime.Parse(el.Elements("lastBuildDate").Single().Value) : DateTime.MinValue;
                //<generator>Koopakiller.NewsFeed Classes</generator>
                Generator = el.Elements("generator").Count() == 1 ? el.Elements("generator").Single().Value : "";
                //<managingEditor>Autor</managingEditor>
                Author = el.Elements("managingEditor").Count() == 1 ? el.Elements("managingEditor").Single().Value : "";
                //<image>...</image>
                if (el.Elements("image").Count() == 1)
                {
                    //<url>http://koopakiller.ko.ohost.de/images/bg.png</url>
                    Image.Url = el.Elements("image").Descendants("url").Count() == 1 ? el.Elements("image").Descendants("url").Single().Value : "";
                    //<title>Title</title>
                    Image.Title = el.Elements("image").Descendants("title").Count() == 1 ? el.Elements("image").Descendants("title").Single().Value : "";
                    //<link>test.htm</link>
                    Image.Link = el.Elements("image").Descendants("link").Count() == 1 ? el.Elements("image").Descendants("link").Single().Value : "";
                    //<height>90</height>
                    Image.Height = el.Elements("image").Descendants("height").Count() == 1 ? int.Parse(el.Elements("image").Descendants("height").Single().Value) : 31;
                    //<width>90</width>
                    Image.Width = el.Elements("image").Descendants("width").Count() == 1 ? int.Parse(el.Elements("image").Descendants("width").Single().Value) : 88;
                    //<description>Beschreibung...</description>
                    ImageDescription = el.Elements("image").Descendants("description").Count() == 1 ? el.Elements("image").Descendants("description").Single().Value : "";
                }
                //<textinput>...</textinput>
                if (el.Elements("textinput").Count() == 1)
                {
                    TextInput = new RSSFeedTextInput();
                    //<description>Beschreibung</description>
                    TextInput.Description = el.Elements("textinput").Descendants("description").Count() == 1 ? el.Elements("textinput").Descendants("description").Single().Value : "";
                    //<name>...</name>
                    TextInput.Name = el.Elements("textinput").Descendants("name").Count() == 1 ? el.Elements("textinput").Descendants("name").Single().Value : "";
                    //<link>...</link>
                    TextInput.Link = el.Elements("textinput").Descendants("link").Count() == 1 ? el.Elements("textinput").Descendants("link").Single().Value : "";
                    //<title>test.htm</title>
                    TextInput.Title = el.Elements("textinput").Descendants("title").Count() == 1 ? el.Elements("textinput").Descendants("title").Single().Value : "";
                }
                //<language>de-de</language>
                Language = el.Elements("language").Count() == 1 ? CultureInfo.CreateSpecificCulture(el.Elements("language").Single().Value) : CultureInfo.InvariantCulture;
                //<item>...</item>
                Articles = (from x in el.Elements("item")
                            select (FeedArticleBase)new RSSFeedArticle()
                            {
                                Categories = (from c in x.Elements("catagory")
                                              select new RSSFeedCategory()
                                              {
                                                  CategoryName = c.Value,
                                                  Domain = c.Attributes("domain").Count() == 1 ? c.Attributes("domain").Single().Value : "",
                                              }).ToList(),
                                ArticleUrl = x.Elements("link").Count() == 1 ? x.Elements("link").Single().Value : "",
                                Author = x.Elements("author").Count() == 1 ? x.Elements("author").Single().Value : "",
                                Content = x.Elements("description").Count() == 1 ? x.Elements("description").Single().Value : "",
                                Title = x.Elements("title").Count() == 1 ? x.Elements("title").Single().Value : "",
                                Guid = new RSSFeedArticleGuid()
                                {
                                    Guid = x.Elements("guid").Count() == 1 ? x.Elements("guid").Single().Value : "",
                                    IsPermaLink = x.Elements("link").Count() == 1 ? x.Elements("link").Attributes("isPermaLink").Count() == 1 ? bool.Parse(x.Elements("link").Attributes("isPermaLink").Single().Value) : false : false,
                                },
                                Comments = x.Elements("comments").Count() == 1 ? x.Elements("comments").Single().Value : "",
                                Published = x.Elements("pubDate").Count() == 1 ? DateTime.Parse(x.Elements("pubDate").Single().Value) : DateTime.MinValue,
                                Enclosure = new RSSFeedArticleEnclosure()
                                {
                                    Url = x.Elements("enclosure").Count() == 1 ? x.Elements("enclosure").Attributes("url").Count() == 1 ? x.Elements("enclosure").Attributes("url").Single().Value : "" : "",
                                    Type = x.Elements("enclosure").Count() == 1 ? x.Elements("enclosure").Attributes("type").Count() == 1 ? x.Elements("enclosure").Attributes("type").Single().Value : "" : "",
                                    Length = x.Elements("enclosure").Count() == 1 ? x.Elements("enclosure").Attributes("length").Count() == 1 ? ulong.Parse(x.Elements("enclosure").Attributes("length").Single().Value) : ulong.MinValue : ulong.MinValue,
                                },
                                Source = new RSSFeedArticleSource()
                                {
                                    Source = x.Elements("source").Count() == 1 ? x.Elements("source").Single().Value : "",
                                    Uri = x.Elements("source").Count() == 1 ? x.Elements("source").Attributes("url").Count() == 1 ? x.Elements("source").Attributes("url").Single().Value : "" : "",
                                },
                            }).ToList();
            }

            #region Eigenschaften

            /// <summary>
            /// Ruft die Beschreibung des Newfeeds ab oder legt sie fest.
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// Ruft die Url zum vollständigen Inhalt ab oder legt sie fest.
            /// </summary>
            public string ArticleUrl { get; set; }
            /// <summary>
            /// Ruft den Titel des Newsfeeds ab oder legt ihn fest.
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// Ruft den Generator, mit dem der Newfeed erstellt wurde ab oder legt ihn fest.
            /// </summary>
            public string Generator { get; set; }
            /// <summary>
            /// Ruft das Image, welches zu dem RSS Feed gehört ab oder legtdieses fest.
            /// </summary>
            public RSSFeedImage Image { get; set; }
            /// <summary>
            /// Ruft eine Beschreibung für das Ziel des Hyperlinks des Bildes für den RSS Feed ab oder legt diese fest.
            /// </summary>
            public string ImageDescription { get; set; }
            /// <summary>
            /// Ruft das eine Liste der Kategorien dieses RSS Feeds ab oder legt diese fest.
            /// </summary>
            public List<RSSFeedCategory> Categories { get; set; }
            /// <summary>
            /// Ruft eine Url auf die Dokumentation des verwendeten Formats ab oder legt diese fest.
            /// </summary>
            public string Docs { get; set; }
            /// <summary>
            /// Ruft das Datum der letzten Aktualisierung ab oder legt dieses fest.
            /// </summary>
            public DateTime Updated { get; set; }
            /// <summary>
            /// Ruft die Tage, an denen der RSS Feed nicht aktualisiert werden braucht ab oder legt diese fest. 
            /// Die Wochentage müssen im Englischen Namen angegeben werden. Beispielsweise: Saturday, Sunday.
            /// </summary>
            public List<string> SkipDays { get; set; }
            /// <summary>
            /// Ruft die Stunden, an denen der RSS Feed nicht aktualisiert werden braucht, ab oder left diese fest.
            /// Die Stunden müssen als Zahlen angegeben werden. Beispielsweise 0, 1, 2, 3, 4, 5
            /// 0 ist Mitternacht, höchstens ist 23 (11 Uhr Abends) erlaubt.
            /// </summary>
            public List<byte> SkipHours { get; set; }
            /// <summary>
            /// Ruft ein optionales InputField des RSS Feeds ab oder legt dieses fest.
            /// </summary>
            public RSSFeedTextInput TextInput { get; set; }
            /// <summary>
            /// Ruft die Anzahl der Minuten die gewartet werden bis der Artikel aktualisiert wird ab oder legt diese fest.
            /// </summary>
            public int TimeToLive { get; set; }
            /// <summary>
            /// Ruft den Webmaster (E-Mail Adresse) ab oder legt ihn fest.
            /// </summary>
            public string WebMaster { get; set; }
            /// <summary>
            /// Ruft den Versionsstring des RSS Dokuments ab oder legt diesen fest.
            /// </summary>
            public string Version { get; set; }
            /// <summary>
            /// Ruft den Author (E-Mail Adresse) des RSS Feeds ab oder legt diesen fest.
            /// </summary>
            public string Author { get; set; }

            #endregion

            #region override object

            /// <summary>
            /// Liefert den Titel und die Beschreibung des RSS Feeds, getrennt durch einen Zeilenumbruch.
            /// </summary>
            /// <returns>Der Titel und die Beschreibung des RSS Feeds, getrennt durch einen Zeilenumbruch.</returns>
            public override string ToString()
            {
                return Title + Environment.NewLine + Description;
            }

            /// <summary>
            /// Liefert den Hashcode dieser Instanz.
            /// </summary>
            /// <returns>Der Hashcode dieser Instanz.</returns>
            public override int GetHashCode()
            {
                return Articles.GetHashCode() ^ ArticleUrl.GetHashCode() ^ Author.GetHashCode() ^ ImageDescription.GetHashCode()
                    ^ Image.GetHashCode() ^ Language.GetHashCode() ^ Published.GetHashCode()
                    ^ SkipDays.GetHashCode() ^ SkipHours.GetHashCode() ^ TextInput.GetHashCode() ^ TimeToLive.GetHashCode()
                    ^ Title.GetHashCode() ^ Updated.GetHashCode() ^ Version.GetHashCode() ^ WebMaster.GetHashCode();
            }

            /// <summary>
            /// Vergleicht ein object mit dieser Instanz.
            /// </summary>
            /// <param name="obj">Das zu vergleichende object.</param>
            /// <returns>True, wenn der Hashcode dieser Instanz mit dem Hashcode des objects übereinstimmen, andernfalls False.</returns>
            public override bool Equals(object obj)
            {
                return obj is RSSFeed && obj.GetHashCode() == this.GetHashCode();
            }

            #endregion
        }
}
