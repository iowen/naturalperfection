using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IPhotoAlbumRepository
    {
        List<PhotoAlbum> getAllPhotoAlbums();
        List<PhotoAlbum> getFeaturedPhotoAlbums();

        PhotoAlbum getPhotoAlbum(int id);

        int addPhotoAlbum(PhotoAlbum album);

        void updatePhotoAlbum(PhotoAlbum album);
    }
}
