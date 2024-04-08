using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TrustyPortfolio.Repositories {
    public class CloudinaryImageRepository : IImageRepository {
        readonly Account cloudinaryAccount;

        public CloudinaryImageRepository(IConfiguration config) {
            string cloudAccount = config["CloudinaryCloudName"];
            string apiKey = config["CloudinaryApiKey"];
            string apiSecret = config["CloudinaryApiSecret"];
            cloudinaryAccount = new(cloudAccount, apiKey, apiSecret);
        }

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
