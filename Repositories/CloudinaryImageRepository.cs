using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace TrustyPortfolio.Repositories {
    public class CloudinaryImageRepository : IImageRepository {
        readonly Account cloudinaryAccount;

        public CloudinaryImageRepository(IConfiguration config) {
            string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string cloudAccount;
            string apiKey;
            string apiSecret;

            if (string.Equals("Development", currentEnvironment)) {
                cloudAccount = config["CloudinaryAccount"];
                apiKey = config["CloudinaryApiKey"];
                apiSecret = config["CloudinaryApiSecret"];
            } else {
                SecretClientOptions options = new SecretClientOptions() {
                    Retry =
                        {
                        Delay = TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };
                var client = new SecretClient(
                new Uri("https://trustyportfoliovault.vault.azure.net/"),
                new DefaultAzureCredential(), options);

                var temp = client.GetSecret("CloudinaryAccount");
                cloudAccount = temp.Value.Value;
                temp = client.GetSecret("PortfolioDbConnectionString");
                apiKey = temp.Value.Value; 
                temp = client.GetSecret("PortfolioDbConnectionString");
                apiSecret = temp.Value.Value;
            }

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
