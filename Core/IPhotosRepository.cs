using dashboard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dashboard.Core
{
    public interface IPhotosRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);

        void DeletePhoto(int photoId, string uploadFolderPath);
       
    }
}