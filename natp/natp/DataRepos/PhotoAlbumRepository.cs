using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class PhotoAlbumRepository : IPhotoAlbumRepository
    {
        private npDataContext db;

        public PhotoAlbumRepository(npDataContext _db)
        {
            db = _db;
        }

        public List<PhotoAlbum> getAllPhotoAlbums()
        {
            var result = new List<PhotoAlbum>();
            try
            {
                result = db.PhotoAlbums.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<PhotoAlbum>();
            }
            return result;
        }

        public List<PhotoAlbum> getFeaturedPhotoAlbums()
        {
            var result = new List<PhotoAlbum>();
            try
            {
                result = db.PhotoAlbums.Where(p => p.IsFeatured == true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<PhotoAlbum>();
            }
            return result;
        }

        public PhotoAlbum getPhotoAlbum(int id)
        {
            var result = new PhotoAlbum();
            try
            {
                result = db.PhotoAlbums.First(p => p.PhotoAlbumId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new PhotoAlbum();
            }
            return result;
        }

        public int addPhotoAlbum(PhotoAlbum album)
        {
            try
            {
                this.db.PhotoAlbums.InsertOnSubmit(album);
                this.db.SubmitChanges();
                return album.PhotoAlbumId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public void updatePhotoAlbum(PhotoAlbum album)
        {
            try
            {
                var old = db.PhotoAlbums.First(p => p.PhotoAlbumId == album.PhotoAlbumId);
                old.IsFeatured = album.IsFeatured;
                old.isSiteAlbum = album.isSiteAlbum;
                old.Title = album.Title;
                old.Description = album.Description;
                old.PhotoAlbumItems = album.PhotoAlbumItems;
                this.db.SubmitChanges();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }
}
