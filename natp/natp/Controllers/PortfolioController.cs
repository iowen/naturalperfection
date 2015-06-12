using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using natp.DataRepos;
using natp.Models;

namespace natp.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new npDataContext());
            ViewBag.albums = photoAlbumRepository.getAllPhotoAlbums();
            return View();
        }
    }
}