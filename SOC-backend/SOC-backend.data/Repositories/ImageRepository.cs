using SOC_backend.logic.Interfaces.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;


namespace SOC_backend.data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IConfiguration _config;
        private Cloudinary cloudinary;

        public ImageRepository(IConfiguration config)
        {
            _config = config;
            cloudinary = new Cloudinary(_config["Cloudinary:URL"]);
            cloudinary.Api.Secure = true;
        }

        public async Task DeleteImage(string imageURL)
        {
            Uri uri = new Uri(imageURL);
            string path = uri.AbsolutePath;
            var imageId = System.IO.Path.GetFileNameWithoutExtension(path);
            var deleteParams = new DelResParams()
            {
                PublicIds = new List<string> { imageId },
                Type = "upload",
                ResourceType = ResourceType.Image
            };
            var result = await cloudinary.DeleteResourcesAsync(deleteParams);
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(uniqueFileName, new MemoryStream(stream.ToArray())),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                if (uploadResult.SecureUrl != null)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
