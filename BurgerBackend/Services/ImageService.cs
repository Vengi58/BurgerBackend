using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerBackend.Services
{
    public interface IImageSerce
    {
        public void UploadImage(string restaurantName, IFormFile file);
        public IEnumerable<MemoryStream> GetImages(string restaurantName);
    }

    public class ImageService : IImageSerce
    {
        private readonly string DestinationFolder;
        public ImageService(string destinationFolder)
        {
            DestinationFolder = destinationFolder;
        }

        public IEnumerable<MemoryStream> GetImages(string restaurantName)
        {
            var path = Path.Combine(
                           DestinationFolder,
                           restaurantName);
            var files = Directory.GetFiles(path);

            foreach (var f in files)
            {
                MemoryStream memory = new MemoryStream();
                using var stream = new FileStream(f, FileMode.Open);
                stream.CopyTo(memory);
                yield return memory;
            }
        }

        public void UploadImage(string restaurantName, IFormFile file)
        {
            var directoryPath = Path.Combine(DestinationFolder, restaurantName);
            var path = Path.Combine(directoryPath, new Guid() + "_" + file.FileName);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyToAsync(stream);
        }
    }
}
