using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class FeaturedProjectCard {
        [Parameter] public Project Project { get; set; }
        string GetURLHandle() => $"/Projects/{Project.UrlHandle}";
    }
}
