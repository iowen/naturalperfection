using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IPhotoAlbumItemRepository
    {
        List<PhotoAlbumItem> getAllPhotoAlbumItems();
        List<PhotoAlbumItem> getPhotoAlbumItemsForAlbum(int id);

        PhotoAlbumItem getPhotoAlbumItem(int id);

        int addPhotoAlbumItem(PhotoAlbumItem item);

        void updatePhotoAlbumItem(PhotoAlbumItem item);
    }
}
