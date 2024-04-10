using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Models.Domain;

namespace TrustyPortfolio.Components.Components {
    public partial class ProjectCard {
        [Parameter] public Project Project { get; set; }
        string GetURLHandle() => $"/Projects/{Project.UrlHandle}";
    }
}
