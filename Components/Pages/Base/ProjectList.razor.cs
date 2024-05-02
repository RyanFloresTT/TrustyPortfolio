using Microsoft.AspNetCore.Components;

namespace TrustyPortfolio.Components.Pages.Base {
    public partial class ProjectList {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
    }
}