using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TrustyPortfolio.Repositories {
    public class CloudinaryImageRepository : IImageRepository {
        readonly Account cloudinaryAccount;

        public CloudinaryImageRepository() {

            SecretClientOptions options = new SecretClientOptions() {
                Retry =
                        {
                            Delay= TimeSpan.FromSeconds(2),
                            MaxDelay = TimeSpan.FromSeconds(16),
                            MaxRetries = 5,
                            Mode = RetryMode.Exponential
                         }
            };
            var client = new SecretClient(
                new Uri("https://trustyportfoliovault.vault.azure.net/"),
                new DefaultAzureCredential(), options);

            var cloudAccount = client.GetSecret("CloudinaryCloudName").Value.Value;
            var apiKey = client.GetSecret("CloudinaryApiKey").Value.Value;
            var apiSecret = client.GetSecret("CloudinaryApiSecret").Value.Value;

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
