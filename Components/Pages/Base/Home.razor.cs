using Microsoft.AspNetCore.Components;

namespace TrustyPortfolio.Components.Pages.Base {
    public partial class Home {
        [CascadingParameter] PortfolioData PortfolioData { get; set; }
    }
}