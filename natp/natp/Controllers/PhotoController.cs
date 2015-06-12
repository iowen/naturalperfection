using natp.DataRepos;
using natp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace natp.Controllers
{
    public class PhotoController : Controller
    {
        // GET: Photo
        public ActionResult Index()
        {
            var pAlb = new PhotoAlbumRepository(new npDataContext());
            ViewBag.albums = pAlb.getAllPhotoAlbums();
            return View();
        }
        public ActionResult Album(int id)
        {
            var pAlb = new PhotoAlbumRepository(new npDataContext());
            ViewBag.album = pAlb.getPhotoAlbum(id);
            return View();
        }
        
        public ActionResult HandleUpload(HttpPostedFileBase FileData)
        {
            JsonResult jsonResult = new JsonResult();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileData.FileName);
            string path = MediaNameGen.GetRandomMediaName() + Path.GetExtension(FileData.FileName);
            string text = Path.Combine(Server.MapPath("~/npMedia/Photos/Uploads/Temp"), path);
            FileData.SaveAs(text);
            string[,] array = new string[1, 2];
            int num = 0;
            array[num, 0] = Url.Content(text);
            array[num, 1] = fileNameWithoutExtension;
            jsonResult.Data = array;
            return jsonResult;
        }

        public ActionResult Processupload(string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            new PhotoRepository(new npDataContext());
            int num = int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string title = DateTime.UtcNow.ToString("MMMM dd, yyyy");
            IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new npDataContext());
            PhotoAlbum photoAlbum;
            try
            {
                photoAlbum = photoAlbumRepository.getAllPhotoAlbums().First(a => a.isSiteAlbum == true && a.Title.Trim().ToLower().Equals(title.Trim().ToLower()) && a.AccountId == num);
            }
            catch (Exception ex)
            {
                int photoAlbumId = photoAlbumRepository.addPhotoAlbum(new PhotoAlbum
                {
                    AccountId = num,
                    Title = title,
                    Description = "",
                    isSiteAlbum = true,
                    IsFeatured = false,
                    DateCreatedUtc = DateTime.UtcNow
                });
                photoAlbum = photoAlbumRepository.getPhotoAlbum(photoAlbumId);
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(array[i]))
                {
                    string[] array2 = array[i].Split(new char[]
					{
						','
					});
                    string text = array2[0];
                    text = text.Replace("/temp", "");
                    string text2 = Server.MapPath(array2[0]);
                    string destFileName = text2.Replace("\\temp", "");
                    try
                    {
                        System.IO.File.Move(text2, destFileName);
                        Photo photo = new Photo
                        {
                            AccountId = num,
                            Location = text,
                            DateCreatedUtc = DateTime.UtcNow

                        };
                        photoAlbum.PhotoAlbumItems.Add(new PhotoAlbumItem
                        {
                            Photo = photo,
                            PhotoAlbumId = photoAlbum.PhotoAlbumId,
                            IsCover = false,
                            
                        });
                        photoAlbumRepository.updatePhotoAlbum(photoAlbum);
                    }
                    catch
                    {
                    }
                }
            }
            return View();
        }
        public ActionResult Processalbum(string meta, string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            string[] array2 = meta.Split(new char[]
			{
				'~'
			});
            IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new npDataContext());
            int num =  (array2[2].Trim().ToLower().Equals("site")) ? -1 :  int.Parse(User.Identity.GetUserId());
            PhotoAlbum photoAlbum;
            if(num > 0)
            {
                try
                {
                    photoAlbum = photoAlbumRepository.getAllPhotoAlbums().First(a => a.AccountId == num && a.Title.Trim().ToLower().Equals(array2[0].Trim().ToLower()));
                    
                }
                catch (Exception ex)
                {
                    PhotoAlbum album = new PhotoAlbum
                    {
                        AccountId = num,
                        Title = array2[0],
                        Description = array2[1],
                        isSiteAlbum = false,
                        IsFeatured = false,
                        DateCreatedUtc = DateTime.UtcNow
                    };
                    int photoAlbumId = photoAlbumRepository.addPhotoAlbum(album);
                    photoAlbum = photoAlbumRepository.getPhotoAlbum(photoAlbumId);
                }
            }
            else
            {
                num = 1;
                try
                {
                photoAlbum = photoAlbumRepository.getAllPhotoAlbums().First(a => a.isSiteAlbum == true && a.Title.Trim().ToLower().Equals(array2[0].Trim().ToLower()));
                }
                catch (Exception ex)
                {
                    PhotoAlbum album = new PhotoAlbum
                    {
                        AccountId = num,
                        Title = array2[0],
                        Description = array2[1],
                        isSiteAlbum = true,
                        IsFeatured = false,
                        DateCreatedUtc = DateTime.UtcNow
                    };
                    int photoAlbumId = photoAlbumRepository.addPhotoAlbum(album);
                    photoAlbum = photoAlbumRepository.getPhotoAlbum(photoAlbumId);
                }
            }
           
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(array[i]))
                {
                    string[] array3 = array[i].Split(new char[]
					{
						','
					});
                    string text = array3[0];
                    text = text.Replace("/temp", "");
                    string text2 = Server.MapPath(array3[0]);
                    string destFileName = text2.Replace("\\temp", "");
                    try
                    {
                        System.IO.File.Move(text2, destFileName);
                        Photo photo = new Photo
                        {
                            AccountId = num,
                            Location = text,
                            DateCreatedUtc = DateTime.UtcNow
                        };
                        photoAlbum.PhotoAlbumItems.Add(new PhotoAlbumItem
                        {
                            Photo = photo,
                        IsCover = (i == 0) ? true : false,
                            PhotoAlbumId = photoAlbum.PhotoAlbumId,
                            
                        });
                        photoAlbumRepository.updatePhotoAlbum(photoAlbum);
                    }
                    catch
                    {
                    }
                }
            }
            var responseStatus =  "Success" ;
            var response = new GenericResponse() { Status = responseStatus };
            return new JsonResult() { Data = response };
        }
    }
}