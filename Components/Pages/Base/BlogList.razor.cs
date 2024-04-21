using Microsoft.AspNetCore.Components;

namespace TrustyPortfolio.Components.Pages.Base {
    public partial class BlogList {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
    }
}