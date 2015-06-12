using natp.DataRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace natp.Controllers
{
     [Authorize(Roles = "SuperAdmin")]
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetDashboard(string type)
        {
            switch (type)
            {
                case "photo":
                    {
                        var palb = new PhotoAlbumRepository(new npDataContext ());
                        ViewBag.photoAlbums = palb.getAllPhotoAlbums();

                        return PartialView("_ManagePhotoPartial");
                    }
            }

            return PartialView();
        }
    }
}