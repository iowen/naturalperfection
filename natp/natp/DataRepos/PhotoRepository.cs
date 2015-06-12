using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class PhotoRepository : IPhotoRepository
    {
         private npDataContext db;

        public PhotoRepository(npDataContext _db)
        {
            db = _db;
        }

        public List<Photo> getAllPhotos()
        {
            var result = new List<Photo>();
            try
            {
                result = db.Photos.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Photo>();
            }
            return result;
        }

        public Photo getPhoto(int id)
        {
            var result = new Photo();
            try
            {
                result = db.Photos.First(p => p.PhotoId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Photo();
            }
            return result;
        }

        public int addPhoto(Photo photo)
        {
            try
            {
                this.db.Photos.InsertOnSubmit(photo);
                this.db.SubmitChanges();
                return photo.PhotoId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public void updatePhoto(Photo photo)
        {
            try
            {
                var old = db.Photos.First(p => p.PhotoId == photo.PhotoId);
                old.Location = photo.Location;
                this.db.SubmitChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }
    }
}
