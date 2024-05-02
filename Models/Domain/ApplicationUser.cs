using Microsoft.AspNetCore.Identity;

namespace TrustyPortfolio.Models.Domain {
    public class ApplicationUser : IdentityUser {
        public string ProfilePictureURL { get; set; } = "https://d3544la1u8djza.cloudfront.net/APHI/Blog/2021/07-06/small+white+fluffy+dog+smiling+at+the+camera+in+close-up-min.jpg";
    }

}
