using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController (IImageRepository imageRepository) : ControllerBase {
        readonly IImageRepository imageRepository = imageRepository;

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file) {
            var resultURL = await imageRepository.UploadAsync(file);

            if (resultURL == null) {
                return Problem("Image upload was unsuccessful..", null, (int)HttpStatusCode.InternalServerError);
            } else {
                return new JsonResult(resultURL);
            }
        }
    }
}
