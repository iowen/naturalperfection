using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IPhotoRepository
    {
        List<Photo> getAllPhotos();

        Photo getPhoto(int id);

        int addPhoto(Photo photo);

        void updatePhoto(Photo photo);
    }
}
