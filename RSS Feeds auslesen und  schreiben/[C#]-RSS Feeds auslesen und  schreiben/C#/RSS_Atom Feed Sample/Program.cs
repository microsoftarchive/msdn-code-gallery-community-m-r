using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Koopakiller.NewsFeed;
using System.IO;
using System.Globalization;

namespace RSS_Atom_Feed_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Was möchten Sie tun?");
            Console.WriteLine(" 1   RSS Feed auslesen");
            Console.WriteLine(" 2   RSS Feed speichern");
            Console.WriteLine(" 3   Atom Feed auslesen");
            Console.WriteLine(" 4   Atom Feed speichern");

            string path = "";
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.D1: //RSS lesen
                    path = Console.ReadLine();
                    if (!File.Exists(path))
                        path = @"http://social.msdn.microsoft.com/Forums/de-DE/wpfde/threads?outputAs=rss";
                    RSSFeed rss_read = new RSSFeed();
                    rss_read.Load(path);
                    Console.WriteLine("Author:         " + rss_read.Author);
                    Console.WriteLine("Author:         " + rss_read.Language);
                    Console.WriteLine("Author:         " + rss_read.Title);
                    Console.WriteLine("Author:         " + rss_read.Updated);
                    Console.WriteLine("Author:         " + rss_read.Version);
                    Console.WriteLine("Author:         " + rss_read.WebMaster);
                    Console.WriteLine();
                    foreach (RSSFeedArticle feed in rss_read.Articles)
                        Console.WriteLine(feed.Title);
                    break;
                case ConsoleKey.D2: //RSS schreiben
                    RSSFeed rss_write = new RSSFeed();
                    rss_write.Author = "Autor";
                    rss_write.Description = "Ein Newsfeed...";
                    rss_write.Generator = "Koopakiller.NewsFeed classes";
                    rss_write.Title = "Newsfeed";
                    rss_write.Language = CultureInfo.InvariantCulture;
                    rss_write.Articles.Add(new RSSFeedArticle()
                    {
                        Author = "Autor",
                        Content = "<h1>title</h1><br/>text <em>kursiv</em>",
                        Title = "Title",
                        ArticleUrl = "http://www.example.org",
                    });
                    path = Console.ReadLine();
                    rss_write.Save(path);
                    break;
                case ConsoleKey.D3: //Atom lesen
                    path = Console.ReadLine();
                    if (!File.Exists(path))
                        path = @"http://social.msdn.microsoft.com/Forums/de-DE/wpfde/threads?outputAs=rss";
                    AtomFeed atom_read = new AtomFeed();
                    atom_read.Load(path);
                    Console.WriteLine("Author[0]:         " + (atom_read.Authors.Count >= 1 ? atom_read.Authors[0].Name : ""));
                    Console.WriteLine("Author:         " + atom_read.Language);
                    Console.WriteLine("Author:         " + atom_read.Title);
                    Console.WriteLine("Published:         " + atom_read.Published);
                    Console.WriteLine("SubTitle:         " + atom_read.SubTitle);
                    Console.WriteLine();
                    foreach (AtomFeedArticle feed in atom_read.Articles)
                        Console.WriteLine(feed.Title);
                    break;
                case ConsoleKey.D4: //Atom schreiben
                    AtomFeed atom_write = new AtomFeed();
                    atom_write.Authors.Add(new AtomFeedArticlePerson()
                        {
                            Name = "Autor Name",
                            EMail = "autor@example.org",
                            Uri = "http://www.example.org",
                        });
                    atom_write.SubTitle = new AtomFeedText()
                    {
                        Text = "Ein Newsfeed...",
                        Type = "text", //Standart ist "text", kann siomit weg gelassenw erden.
                    };
                    atom_write.Title = new AtomFeedText()
                    {
                        Text = "Newsfeed",
                    };
                    atom_write.Language = CultureInfo.InvariantCulture;
                    atom_write.Articles.Add(new AtomFeedArticle()
                    {
                        Content = new AtomFeedText()
                        {
                            Text = "<h1>title</h1><br/>text <em>kursiv</em>",
                            Type = "html",
                        },
                        Title = "Title",
                    });
                    ((AtomFeedArticle) atom_write.Articles[0]).Authors.Add(new AtomFeedArticlePerson()
                    {
                            Name = "Autor Name",
                            EMail = "autor@example.org",
                            Uri = "http://www.example.org",
                    });
                    ((AtomFeedArticle)atom_write.Articles[0]).Links.Add(new AtomFeedLink()
                    {
                        Target = "http://www.example.org",
                    });
                    path = Console.ReadLine();
                    atom_write.Save(path);
                    break;
            }

            Console.ReadKey();
        }
    }
}
