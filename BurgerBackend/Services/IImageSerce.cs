using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace BurgerBackend.Services
{
    public interface IImageSerce
    {
        public void UploadImage(string restaurantName, IFormFile file);
        public IEnumerable<string> GetImageIDs(string restaurantName);
        public MemoryStream GetImageOfRestaurant(string restaurantName, string imageID);
        public IEnumerable<MemoryStream> GetImages(string restaurantName);
    }
}
