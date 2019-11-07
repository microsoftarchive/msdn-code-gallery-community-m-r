using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parsing_html.Resources;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;

namespace Parsing_html
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        List<Product> listproduct = new List<Product>();
        public MainPage()
        {
            InitializeComponent();

            Parsing(" http://www.mytek.tn/");
        }

        private async void Parsing(string website)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(website);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);

                List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
                (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("block_content"))).ToList();

                var li = toftitle[6].Descendants("li").ToList();
                foreach (var item in li)
                {
                    var link = item.Descendants("a").ToList()[0].GetAttributeValue("href", null);
                    var img = item.Descendants("img").ToList()[0].GetAttributeValue("src", null);
                    var title = item.Descendants("h5").ToList()[0].InnerText;

                    listproduct.Add(new Product()
                    {
                        Img = img,
                        Title = title,
                        Link = link
                    });
                }

                listboxproduct.DataContext = listproduct;

            }
            catch (Exception)
            {

                MessageBox.Show("Network Problem!");
            }

        }

     
    }
}