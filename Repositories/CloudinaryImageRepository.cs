
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Data.SqlTypes;

namespace TrustyPortfolio.Repositories {
    public class CloudinaryImageRepository (IConfiguration config) : IImageRepository {
        readonly IConfiguration config = config;
        readonly Account cloudinaryAccount = new(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]);

        public async Task<string> UploadAsync(IFormFile file) {
            var client = new Cloudinary(cloudinaryAccount);

            var result = await client.UploadAsync(new ImageUploadParams() {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            });

            if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK) {
                return result.SecureUrl.ToString();
            }

            return null;
        }
    }
}
