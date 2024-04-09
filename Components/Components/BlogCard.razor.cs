using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class BlogCard {
        [Parameter] public BlogPost BlogPost { get; set; }
    }
}
