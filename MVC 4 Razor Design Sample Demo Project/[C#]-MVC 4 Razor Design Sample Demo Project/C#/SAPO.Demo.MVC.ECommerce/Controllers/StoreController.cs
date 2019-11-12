using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAPO.Demo.MVC.ECommerce.Models;

namespace SAPO.Demo.MVC.ECommerce.Controllers
{
    public class StoreController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View;
        //}
        
        // GET: /Store/
        //public string Index()
        //{
        //    return "You are now at Store.Index";
        //}
        
        /*
            // GET: /Store/Browse
            public string Browse()
            {
                return "You are now at Store.Browse";
            }
        */

        //Get /store/browse?genre=Rock
        //public string Browse(string genre)
        //{
        //    string message = HttpUtility.HtmlEncode("Store.Browse,genre=" + genre);
        //    return message;
        //}        

          /*
        // GET: /Store/Details
        public string Details()
        {
            return "You are now at Store.Details";
        }
           */
        // GET: /Store/Details
        //public string Details(int ID)
        //{
        //    //store/details/10
        //    //store/details?id=10
        //   // string message = HttpUtility.HtmlEncode("Store.Details,ID=" + ID);
        //    string message = "Store.Details,ID=" + ID;
        //    return message;
        //}

        // GET: /Store/Details
        public ActionResult Details()
        {
            var albums = new List<Album> 
            { 
                new Album {ID=1,Title = "Album 1" ,SubTitle="Classic",Description="This is Elvis Prisley",URL="../img/music.png",IsBookMarked=true}, 
                new Album {ID=2,Title = "Album 2" ,SubTitle="Musical",Description="This is Michael Jackson",URL="../img/music.png",IsBookMarked=false}, 
                new Album {ID=3, Title = "Album 3" ,SubTitle="Jazz",Description="Bryan Adam",URL="../img/music.png",IsBookMarked=false} 
            };           
            return View(albums); 
        }
        //Get Method call
        public ActionResult Browse(string genre)
        {
            var genreModel = new Genre { Name = genre };
            return View(genreModel); 

        }
        //Get Method call
        public ActionResult Index()
        {
            var genres = new List<Genre> 
            { 
                new Genre { Name = "Disco"}, 
                new Genre { Name = "Jazz"}, 
                new Genre { Name = "Rock"}, 
                new Genre { Name = "Pop"},
                new Genre { Name = "Opera"}
            };
            return View(genres); 
        }
        
        //This is called from Jquery Ajax handler
        public ActionResult OnRadioSelectChange(int ID)
        {

            var albums = new List<Album> 
            { 
                new Album {ID=1,Title = "Album1" ,SubTitle="Disco",Description="This is Elvis Prisley",URL="../img/music.png",IsBookMarked=false}, 
                new Album {ID=2,Title = "Album2" ,SubTitle="Pop",Description="This is Michael Jackson",URL="../img/music.png",IsBookMarked=true}, 
                new Album {ID=3, Title = "Album3" ,SubTitle="Rock",Description="Bryan Adam",URL="../img/music.png",IsBookMarked=false} 
            };
           // return View(albums);
            return RedirectToAction("Details");
        }
        //This is called from Jquery Ajax Handler.
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Details");
        }

    }
}
