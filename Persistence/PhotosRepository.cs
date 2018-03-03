using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class PhotosRepository : IPhotosRepository
    {
        private readonly DashboardDbContext context;
        public PhotosRepository(DashboardDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos
                .Where(p => p.VehicleId == vehicleId).ToListAsync();
        }

        public void DeletePhoto(int photoId, string uploadFolderPath)
        {
            var photo = context.Photos
            .Where(p => p.Id == photoId).SingleOrDefault();

            if (photo != null)
            {
                string fileName = photo.PhotoName;
                context.Remove(photo);
                context.SaveChanges();

                if (Directory.Exists(uploadFolderPath) && File.Exists(fileName))
                    File.Delete(fileName);
            }


        }
    }
}