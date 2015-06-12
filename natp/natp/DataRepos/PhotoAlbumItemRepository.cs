using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class PhotoAlbumItemRepository :IPhotoAlbumItemRepository
    {
          private npDataContext db;

        public PhotoAlbumItemRepository(npDataContext _db)
        {
            db = _db;
        }

        public List<PhotoAlbumItem> getAllPhotoAlbumItems()
        {
            var result = new List<PhotoAlbumItem>();
            try
            {
                result = db.PhotoAlbumItems.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<PhotoAlbumItem>();
            }
            return result;
        }


        public List<PhotoAlbumItem> getPhotoAlbumItemsForAlbum(int id)
        {
            var result = new List<PhotoAlbumItem>();
            try
            {
                result = db.PhotoAlbumItems.Where(p => p.PhotoAlbumId == id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<PhotoAlbumItem>();
            }
            return result;
        }

        public PhotoAlbumItem getPhotoAlbumItem(int id)
        {
            var result = new PhotoAlbumItem();
            try
            {
                result = db.PhotoAlbumItems.First(p => p.PhotoAlbumItemId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new PhotoAlbumItem();
            }
            return result;
        }

        public int addPhotoAlbumItem(PhotoAlbumItem item)
        {
            try
            {
                this.db.PhotoAlbumItems.InsertOnSubmit(item);
                this.db.SubmitChanges();
                return item.PhotoAlbumItemId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public void updatePhotoAlbumItem(PhotoAlbumItem item)
        {
            try
            {
                var old = db.PhotoAlbumItems.First(p => p.PhotoAlbumItemId == item.PhotoAlbumItemId);
                old.IsCover = item.IsCover;
                old.Title = item.Title;
                old.Description = item.Description;
                this.db.SubmitChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }
}
