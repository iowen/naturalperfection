using natp.DataRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace natp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var photoRepo = new PhotoAlbumRepository(new npDataContext());
            var featured = photoRepo.getFeaturedPhotoAlbums();
            ViewBag.featuredPhotoAlbums = featured;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}