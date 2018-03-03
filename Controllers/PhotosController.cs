using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core;
using dashboard.Core.Models;
using dashboard.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dashboard.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly PhotoSettings photosSettings;
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        private readonly IPhotosRepository photosRepository;

        public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> option, IPhotosRepository photosRepository)
        {
            this.photosRepository = photosRepository;
            this.photosSettings = option.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.host = host;

        }


        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photosRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)//IFormCollection for multiple
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            if (file == null) return BadRequest("Null File Error");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > photosSettings.MaxBytes) return BadRequest("File Too Big");
            if (!photosSettings.isSupported(file.FileName)) return BadRequest("Invalid File Type");


            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //System.Drawing for creating thumbnails
            var photo = new Photo { PhotoName = fileName };
            vehicle.Photos.Add(photo);

            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));

        }

        [HttpDelete("{Id}")]
        public IActionResult DeletePhoto(int Id)
        {
            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            photosRepository.DeletePhoto(Id, uploadFolderPath);

            return Ok("OK");
        }
    }
}