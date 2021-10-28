using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Uploads an image to AWS bucket
        /// </summary>
        /// <param name="email">The email of the user associated with the image</param>
        /// <param name="image">The image (as bytes)</param>
        /// <param name="bucketName"></param>
        /// <returns>The URL of the uploaded image</returns>
        Task<string> UploadImageToBucket(string email, IFormFile image);
    }
}